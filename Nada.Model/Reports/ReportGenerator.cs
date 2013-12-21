using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Repositories;

namespace Nada.Model.Reports
{
    public class ReportGenerator
    {
        private ReportRepository repo = new ReportRepository();

        public ReportResult Run(ReportIndicators settings)
        {
            ReportResult result = new ReportResult();
            result.DataTableResults = CreateTable(settings);
            result.ChartData = CreateChart();
            repo.GetReportData(settings, result.DataTableResults, result.ChartData);
            result.ChartIndicators = GetChartIndicators(settings);
            return result;
        }

        private List<ReportIndicator> GetChartIndicators(ReportIndicators settings)
        {
            List<ReportIndicator> chartIndicators = new List<ReportIndicator>();
            chartIndicators.AddRange(settings.SurveyIndicators.Where(s => s.Selected && s.DataTypeId == 2));
            chartIndicators.AddRange(settings.InterventionIndicators.Where(s => s.Selected && s.DataTypeId == 2));
            return chartIndicators;
        }

        private DataTable CreateChart()
        {
            DataTable chartData = new DataTable();
            chartData.Columns.Add(new DataColumn("Location"));
            chartData.Columns.Add(new DataColumn("IndicatorName"));
            chartData.Columns.Add(new DataColumn("IndicatorId"));
            chartData.Columns.Add(new DataColumn("Year"));
            chartData.Columns.Add(new DataColumn("Value"));
            return chartData;
        }

        private DataTable CreateTable(ReportIndicators settings)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Location"));
            dt.Columns.Add(new DataColumn("Type"));
            dt.Columns.Add(new DataColumn("Year"));
            foreach (var i in settings.SurveyIndicators.Where(s => s.Selected))
                dt.Columns.Add(new DataColumn(i.Name));
            foreach (var i in settings.InterventionIndicators.Where(s => s.Selected))
                dt.Columns.Add(new DataColumn(i.Name));
            return dt;
        }
    }

    public interface IReportGenerator
    {
        ReportResult Run(ReportOptions options);
    }

    public class BaseReportGenerator : IReportGenerator
    {
        protected ReportRepository repo = new ReportRepository();
        protected ReportOptions opts = null;
        protected virtual string CmdText()
        {
            throw new NotImplementedException();
        }

        public virtual ReportResult Run(ReportOptions options)
        {
            opts = options;
            ReportResult result = new ReportResult();
            repo.LoadRelatedLists();

            if (options.IsNoAggregation)
                result.DataTableResults = CreateNonAggregatedReport(options);
            else
                result.DataTableResults = CreateAggregatedReport(options);

            return result;
        }
        
        public DataTable CreateNonAggregatedReport(ReportOptions options)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn(Translations.Location));
            dataTable.Columns.Add(new DataColumn(Translations.Type));
            dataTable.Columns.Add(new DataColumn(Translations.Year));
            repo.CreateNonAggregatedReport(CmdText(), options, dataTable, GetIndicatorColumnName, AddStaticIndicators);
            return dataTable;
        }

        public DataTable CreateAggregatedReport(ReportOptions options)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn(Translations.Location));
            dataTable.Columns.Add(new DataColumn(Translations.Type));
            dataTable.Columns.Add(new DataColumn(Translations.Year));
            List<AdminLevelIndicators> selectedLevels = new List<AdminLevelIndicators>();
            List<AdminLevelIndicators> list = new List<AdminLevelIndicators>();
            Dictionary<int, AdminLevelIndicators> dic = new Dictionary<int, AdminLevelIndicators>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                list = ExportRepository.GetAdminLevels(command, connection);
                dic = list.ToDictionary(n => n.Id, n => n);
                repo.AddIndicatorsToAggregate(CmdText(), options, dic, command, connection, GetIndicatorKey, GetIndicatorColumnName, AddStaticAggIndicators);
            }

            // CONVERT TO TREE
            var rootNodes = new List<AdminLevelIndicators>();
            foreach (var node in list)
            {
                if (node.ParentId.HasValue && node.ParentId.Value > 0)
                {
                    AdminLevelIndicators parent = dic[node.ParentId.Value];
                    node.Parent = parent;
                    parent.Children.Add(node);
                }
                else
                {
                    rootNodes.Add(node);
                }
            }

            // Aggregation type
            if (options.IsCountryAggregation)
                selectedLevels = list.Where(a => a.LevelNumber == 0).ToList();
            if (options.IsByLevelAggregation && options.SelectedAdminLevels.Count > 0)
                selectedLevels = list.Where(a => options.SelectedAdminLevels.Select(s => s.Id).Contains(a.Id)).ToList();

            // AGGREGATE INDICATORS, PUT IN TABLE
            Dictionary<string, DataRow> ids = new Dictionary<string, DataRow>();
            foreach (var level in selectedLevels)
            {
                foreach(var year in options.SelectedYears)
                {
                    foreach (KeyValuePair<string, string> columnDef in options.Columns)
                    {
                        string levelAndYear = level.Id + "_" + year;

                        // Aggregate value
                        object value = null;
                        if (level.Indicators.ContainsKey(columnDef.Key))
                        {
                            value = IndicatorAggregator.Aggregate(level.Indicators[columnDef.Key], null);
                        }
                        else // compute key value
                            value = IndicatorAggregator.AggregateChildren(level.Children, columnDef.Key, null);

                        if (value != null)
                        {
                            // add column
                            if (!dataTable.Columns.Contains(columnDef.Value))
                                dataTable.Columns.Add(new DataColumn(columnDef.Value));

                            // Add row
                            if (!ids.ContainsKey(levelAndYear))
                            {
                                DataRow dr = dataTable.NewRow();
                                dr[Translations.Location] = level.Name;
                                dr[Translations.Type] = Translations.NA;
                                dr[Translations.Year] = year;
                                dataTable.Rows.Add(dr);
                                ids.Add(levelAndYear, dr);
                            }

                            ids[levelAndYear][columnDef.Value] = value;
                        }
                    }
                }
            }

            return dataTable;
        }

        protected virtual string GetIndicatorKey(OleDbDataReader reader)
        {
            return reader.GetValueOrDefault<int>("IndicatorId").ToString() + "_" + reader.GetValueOrDefault<int>("YearReported") + "_" + reader.GetValueOrDefault<string>("TName");
        }

        protected virtual string GetIndicatorColumnName(OleDbDataReader reader)
        {
            if (TranslationLookup.GetValue(reader.GetValueOrDefault<string>("IndicatorName")) == Translations.NoTranslationFound)
                return null;

            return TranslationLookup.GetValue(reader.GetValueOrDefault<string>("IndicatorName")) + " - " + TranslationLookup.GetValue(reader.GetValueOrDefault<string>("TName"));
        }

        protected virtual void AddStaticAggIndicators(CreateAggParams param) { }
        protected virtual void AddStaticIndicators(CreateAggParams param) { }

        protected string GetValueOrBlank(string value, string separator)
        {
            if(string.IsNullOrEmpty(value))
                return "";
            return separator + value;
        }

        protected string GetValueOrBlank(int? value, string separator)
        {
            if (!value.HasValue)
                return "";
            return separator + value.Value.ToString();
        }
    }

    public class SurveyReportGenerator : BaseReportGenerator
    {
        protected override string CmdText()
        {
            string staticConditional = "";
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSpotCheckName) != null)
                staticConditional += "OR Surveys.SpotCheckName IS NOT NULL ";
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSpotCheckLat) != null)
                staticConditional += "OR Surveys.SpotCheckLat IS NOT NULL ";
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSpotCheckLng) != null)
                staticConditional += "OR Surveys.SpotCheckLng IS NOT NULL ";
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSentinelSiteName) != null)
                staticConditional += "OR SentinelSites.SiteName IS NOT NULL ";
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSentinelSiteLat) != null)
                staticConditional += "OR SentinelSites.Lat IS NOT NULL ";
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSentinelSiteLng) != null)
                staticConditional += "OR SentinelSites.Lng IS NOT NULL ";
            if (staticConditional.Length > 0)
                staticConditional = " AND ( " + staticConditional.Remove(0, 2) + " ) ";
        
            if (opts.SelectedIndicators.Where(i => i.ID > 0).Count() > 0)
                return @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        Surveys.ID, 
                        Surveys.YearReported, 
                        SurveyTypes.SurveyTypeName as TName,      
                        SurveyIndicators.ID as IndicatorId, 
                        SurveyIndicators.DisplayName as IndicatorName, 
                        SurveyIndicators.DataTypeId, 
                        SurveyIndicators.AggTypeId,    
                        SurveyIndicatorValues.DynamicValue,
                        Surveys.SpotCheckName as IndSpotCheckName,
                        Surveys.SpotCheckLat as IndSpotCheckLat,
                        Surveys.SpotCheckLng as IndSpotCheckLng,
                        SentinelSites.SiteName as IndSentinelSiteName,
                        SentinelSites.Lat as IndSentinelSiteLat,
                        SentinelSites.Lng as IndSentinelSiteLng
                        FROM ((((((Surveys INNER JOIN SurveyTypes on Surveys.SurveyTypeId = SurveyTypes.ID)
                            INNER JOIN SurveyIndicatorValues on Surveys.Id = SurveyIndicatorValues.SurveyId)
                            INNER JOIN Surveys_to_AdminLevels on Surveys_to_AdminLevels.SurveyId = Surveys.ID) 
                            INNER JOIN AdminLevels on Surveys_to_AdminLevels.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN SurveyIndicators on SurveyIndicators.ID = SurveyIndicatorValues.IndicatorId)
                            LEFT OUTER JOIN SentinelSites on Surveys.SentinelSiteId = SentinelSites.ID)
                        WHERE Surveys.IsDeleted = 0 AND SurveyIndicators.Id in " + " (" + 
                       String.Join(", ", opts.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ") " + 
                       staticConditional;
            else
                return @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        Surveys.ID, 
                        Surveys.YearReported, 
                        SurveyTypes.SurveyTypeName as TName,      
                        0 as IndicatorId, 
                        '' as IndicatorName, 
                        1 as DataTypeId, 
                        1 as AggTypeId,    
                        '' as DynamicValue,
                        Surveys.SpotCheckName as IndSpotCheckName,
                        Surveys.SpotCheckLat as IndSpotCheckLat,
                        Surveys.SpotCheckLng as IndSpotCheckLng,
                        SentinelSites.SiteName as IndSentinelSiteName,
                        SentinelSites.Lat as IndSentinelSiteLat,
                        SentinelSites.Lng as IndSentinelSiteLng
                        FROM ((((Surveys INNER JOIN SurveyTypes on Surveys.SurveyTypeId = SurveyTypes.ID)
                            INNER JOIN Surveys_to_AdminLevels on Surveys_to_AdminLevels.SurveyId = Surveys.ID) 
                            INNER JOIN AdminLevels on Surveys_to_AdminLevels.AdminLevelId = AdminLevels.ID) 
                            LEFT OUTER JOIN SentinelSites on Surveys.SentinelSiteId = SentinelSites.ID)
                        WHERE Surveys.IsDeleted = 0 " + staticConditional;
            
        }

        protected override void AddStaticIndicators(CreateAggParams param)
        {
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSpotCheckName) != null)
                AddIndicatorToTable<string>(Translations.IndSpotCheckName, "IndSpotCheckName", param);
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSpotCheckLat) != null)
                AddIndicatorToTable<Nullable<double>>(Translations.IndSpotCheckLat, "IndSpotCheckLat", param);
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSpotCheckLng) != null)
                AddIndicatorToTable<Nullable<double>>(Translations.IndSpotCheckLng, "IndSpotCheckLng", param);
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSentinelSiteName) != null)
                AddIndicatorToTable<string>(Translations.IndSentinelSiteName, "IndSentinelSiteName", param);
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSentinelSiteLat) != null)
                AddIndicatorToTable<Nullable<double>>(Translations.IndSentinelSiteLat, "IndSentinelSiteLat", param);
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSentinelSiteLng) != null)
                AddIndicatorToTable<Nullable<double>>(Translations.IndSentinelSiteLng, "IndSentinelSiteLng", param);
        }

        private void AddIndicatorToTable<T>(string displayName, string colName, CreateAggParams param)
        {
            if (!param.Table.Columns.Contains(displayName))
                param.Table.Columns.Add(new DataColumn(displayName));

            param.Row[displayName] = param.Reader.GetValueOrDefault<T>(colName);
        }

        protected override void AddStaticAggIndicators(CreateAggParams param)
        {
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSpotCheckName) != null)
                AddIndicatorAndColumn<string>("IndSpotCheckName", Translations.IndSpotCheckName,  param);
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSpotCheckLat) != null)
                AddIndicatorAndColumn<Nullable<double>>("IndSpotCheckLat", Translations.IndSpotCheckLat, param);
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSpotCheckLng) != null)
                AddIndicatorAndColumn<Nullable<double>>("IndSpotCheckLng", Translations.IndSpotCheckLng, param);
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSentinelSiteName) != null)
                AddIndicatorAndColumn<string>("IndSentinelSiteName", Translations.IndSentinelSiteName, param);
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSentinelSiteLat) != null)
                AddIndicatorAndColumn<Nullable<double>>("IndSentinelSiteLat", Translations.IndSentinelSiteLat, param);
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSentinelSiteLng) != null)
                AddIndicatorAndColumn<Nullable<double>>("IndSentinelSiteLng", Translations.IndSentinelSiteLng, param);
        }

        private void AddIndicatorAndColumn<T>(string columnName, string transName, CreateAggParams param)
        {
            string key = columnName + "_" + param.Reader.GetValueOrDefault<int>("YearReported") + "_" + param.Reader.GetValueOrDefault<string>("TName");
            string displayName = transName + " - " + TranslationLookup.GetValue(param.Reader.GetValueOrDefault<string>("TName"));
            T val = param.Reader.GetValueOrDefault<T>(columnName);
            if (!param.AdminLevel.Indicators.ContainsKey(key))
            {
                param.AdminLevel.Indicators.Add(key, new AggregateIndicator
                    {
                        Name = displayName,
                        Key = key,
                        DataType = (int)IndicatorDataType.Text,
                        Value = val == null ? null : val.ToString(),
                        AggType = (int)IndicatorAggType.None,
                        Year = param.Reader.GetValueOrDefault<int>("YearReported")
                    });

                // Add Column
                if (!param.Options.Columns.ContainsKey(key))
                    param.Options.Columns.Add(key, displayName);
            }
        }
    }

    public class IntvReportGenerator : BaseReportGenerator
    {
        protected override string CmdText()
        {
            return @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        Interventions.ID, 
                        Interventions.YearReported, 
                        Interventions.PcIntvRoundNumber, 
                        InterventionTypes.InterventionTypeName as TName,     
                        InterventionIndicators.ID as IndicatorId, 
                        InterventionIndicators.DisplayName as IndicatorName, 
                        InterventionIndicators.DataTypeId, 
                        InterventionIndicators.AggTypeId, 
                        InterventionIndicatorValues.DynamicValue
                        FROM ((((Interventions INNER JOIN InterventionTypes on Interventions.InterventionTypeId = InterventionTypes.ID)
                            INNER JOIN InterventionIndicatorValues on Interventions.Id = InterventionIndicatorValues.InterventionId)
                            INNER JOIN AdminLevels on Interventions.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN InterventionIndicators on InterventionIndicators.ID = InterventionIndicatorValues.IndicatorId)
                        WHERE Interventions.IsDeleted = 0 AND  
                              InterventionIndicators.Id in "
            + " (" + String.Join(", ", opts.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ") ";
        }

        protected override string GetIndicatorKey(OleDbDataReader reader)
        {
            return reader.GetValueOrDefault<int>("IndicatorId").ToString() + "_" + reader.GetValueOrDefault<int>("YearReported") + "_"  +
                reader.GetValueOrDefault<string>("TName") + GetValueOrBlank(reader.GetValueOrDefault<Nullable<int>>("PcIntvRoundNumber"), "_");
        }

        protected override string GetIndicatorColumnName(OleDbDataReader reader)
        {
            return TranslationLookup.GetValue(reader.GetValueOrDefault<string>("IndicatorName")) + " - " +
                TranslationLookup.GetValue(reader.GetValueOrDefault<string>("TName")) +
                GetValueOrBlank(reader.GetValueOrDefault<Nullable<int>>("PcIntvRoundNumber"), string.Format(" - {0} ", Translations.Round));
        }
    }

    public class ProcessReportGenerator : BaseReportGenerator
    {
        protected override string CmdText()
        {
            return @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        Processes.ID, 
                        Processes.YearReported, 
                        Processes.SCMDrug, 
                        Processes.PCTrainTrainingCategory, 
                        ProcessTypes.TypeName as TName,        
                        ProcessIndicators.ID as IndicatorId, 
                        ProcessIndicators.DisplayName as IndicatorName, 
                        ProcessIndicators.DataTypeId, 
                        ProcessIndicators.AggTypeId, 
                        ProcessIndicatorValues.DynamicValue
                        FROM ((((Processes INNER JOIN ProcessTypes on Processes.ProcessTypeId = ProcessTypes.ID)
                            INNER JOIN ProcessIndicatorValues on Processes.Id = ProcessIndicatorValues.ProcessId)
                            INNER JOIN AdminLevels on Processes.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN ProcessIndicators on ProcessIndicators.ID = ProcessIndicatorValues.IndicatorId)
                        WHERE Processes.IsDeleted = 0 AND  
                              ProcessIndicators.Id in "
            + " (" + String.Join(", ", opts.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ") ";
        }

        protected override string GetIndicatorKey(OleDbDataReader reader)
        {
            return reader.GetValueOrDefault<int>("IndicatorId").ToString() + "_" + reader.GetValueOrDefault<int>("YearReported") + "_" +
                reader.GetValueOrDefault<string>("TName") + GetValueOrBlank(reader.GetValueOrDefault<string>("SCMDrug"), "_") +
                GetValueOrBlank(reader.GetValueOrDefault<string>("PCTrainTrainingCategory"), "_");
        }

        protected override string GetIndicatorColumnName(OleDbDataReader reader)
        {
            return TranslationLookup.GetValue(reader.GetValueOrDefault<string>("IndicatorName")) + " - " +
                TranslationLookup.GetValue(reader.GetValueOrDefault<string>("TName")) +
                GetValueOrBlank(reader.GetValueOrDefault<string>("SCMDrug"), " - ") +
                GetValueOrBlank(reader.GetValueOrDefault<string>("PCTrainTrainingCategory"), " - ").Replace("|", ", ");
        }
    }

    public class DistributionReportGenerator : BaseReportGenerator
    {
        protected override string CmdText()
        {
            return @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        DiseaseDistributions.ID, 
                        DiseaseDistributions.YearReported, 
                        Diseases.DisplayName as TName,       
                        DiseaseDistributionIndicators.ID as IndicatorId, 
                        DiseaseDistributionIndicators.DisplayName as IndicatorName, 
                        DiseaseDistributionIndicators.DataTypeId, 
                        DiseaseDistributionIndicators.AggTypeId, 
                        DiseaseDistributionIndicatorValues.DynamicValue
                        FROM ((((DiseaseDistributions INNER JOIN Diseases on DiseaseDistributions.DiseaseId = Diseases.ID)
                            INNER JOIN DiseaseDistributionIndicatorValues on DiseaseDistributions.Id = DiseaseDistributionIndicatorValues.DiseaseDistributionId)
                            INNER JOIN AdminLevels on DiseaseDistributions.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN DiseaseDistributionIndicators on DiseaseDistributionIndicators.ID = DiseaseDistributionIndicatorValues.IndicatorId)
                        WHERE DiseaseDistributions.IsDeleted = 0 AND  
                              DiseaseDistributionIndicators.Id in "
            + " (" + String.Join(", ", opts.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ") ";
        }
    }

    public class DemoReportGenerator : BaseReportGenerator
    {
        public override ReportResult Run(ReportOptions options)
        {
            opts = options;
            ReportResult result = new ReportResult();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn(Translations.Location));
            dataTable.Columns.Add(new DataColumn(Translations.Type));
            dataTable.Columns.Add(new DataColumn(Translations.Year));
            foreach (var ind in options.SelectedIndicators)
                dataTable.Columns.Add(new DataColumn(ind.Name));
            result.DataTableResults = repo.CreateDemoReport(options, dataTable);
            return result;
        }
    }
}
