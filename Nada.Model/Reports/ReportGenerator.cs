using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.Model.Survey;

namespace Nada.Model.Reports
{
    #region ChartGenerator
    //public class ChartGenerator
    //{
    //    private ReportRepository repo = new ReportRepository();

    //    public ReportResult Run(ReportIndicators settings)
    //    {
    //        ReportResult result = new ReportResult();
    //        result.ChartData = CreateChart();
    //        repo.GetReportData(settings, result.DataTableResults, result.ChartData);
    //        result.ChartIndicators = GetChartIndicators(settings);
    //        return result;
    //    }

    //    private List<ReportIndicator> GetChartIndicators(ReportIndicators settings)
    //    {
    //        List<ReportIndicator> chartIndicators = new List<ReportIndicator>();
    //        chartIndicators.AddRange(settings.SurveyIndicators.Where(s => s.Selected && s.DataTypeId == 2));
    //        chartIndicators.AddRange(settings.InterventionIndicators.Where(s => s.Selected && s.DataTypeId == 2));
    //        return chartIndicators;
    //    }

    //    private DataTable CreateChart()
    //    {
    //        DataTable chartData = new DataTable();
    //        chartData.Columns.Add(new DataColumn("Location"));
    //        chartData.Columns.Add(new DataColumn("IndicatorName"));
    //        chartData.Columns.Add(new DataColumn("IndicatorId"));
    //        chartData.Columns.Add(new DataColumn("Year"));
    //        chartData.Columns.Add(new DataColumn("Value"));
    //        return chartData;
    //    }
    //}

    #endregion

    public interface IReportGenerator
    {
        ReportResult Run(ReportOptions options);
    }

    public class BaseReportGenerator : IReportGenerator
    {
        protected ICalcIndicators calc = null;
        protected ReportRepository repo = new ReportRepository();
        protected ReportOptions opts = null;
        protected List<ReportIndicator> selectedCalcFields = new List<ReportIndicator>();
        protected bool hasCalculations = false;
        protected virtual string CmdText() { throw new NotImplementedException(); }
        protected virtual int EntityTypeId { get { return 0; } }

        public virtual ReportResult Run(ReportOptions options)
        {
            opts = options;
            selectedCalcFields = opts.SelectedIndicators.Where(c => c.IsCalculated).ToList();
            hasCalculations = selectedCalcFields.Count > 0;
            ReportResult result = new ReportResult();
            repo.LoadRelatedLists();
            Init();
            result.DataTableResults = CreateReport(options);
            return result;
        }
        
        protected virtual void AddStaticAggInd(CreateAggParams param) { }

        public DataTable CreateReport(ReportOptions options)
        {
            List<AdminLevelIndicators> list = new List<AdminLevelIndicators>();
            Dictionary<int, AdminLevelIndicators> dic = new Dictionary<int, AdminLevelIndicators>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);

            // Get all indicators
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                list = ExportRepository.GetAdminLevels(command, connection);
                dic = list.ToDictionary(n => n.Id, n => n);
                repo.AddIndicatorsToAggregate(CmdText(), options, dic, command, connection, GetIndKey, GetColName, GetColTypeName, AddStaticAggInd, false);
                if (hasCalculations)
                    AddRelatedCalcIndicators(options, dic, command, connection, GetIndKey, GetColName, GetColTypeName,
                        AddStaticAggInd, EntityTypeId);
            }

            // Create table
            DataTable result = new DataTable();
            result.Columns.Add(new DataColumn(Translations.Location));
            result.Columns.Add(new DataColumn(Translations.Type));
            result.Columns.Add(new DataColumn(Translations.Year));
            Dictionary<string, ReportRow> resultDic = new Dictionary<string, ReportRow>();
            if (options.IsNoAggregation)
            {
                foreach (var level in list) // Each admin level
                    foreach (var ind in level.Indicators) // each indicator
                        AddToTable(result, resultDic, level, ind.Value.Year, ind.Value, ind.Value.FormId.ToString() + level.Id, ind.Value.Value, TranslationLookup.GetValue(ind.Value.TypeName, ind.Value.TypeName));
            }
            else
            {
                IndicatorListToTree(list, dic);

                List<AdminLevelIndicators> selectedLevels = new List<AdminLevelIndicators>();
                if (options.IsCountryAggregation)
                    selectedLevels = list.Where(a => a.LevelNumber == 0).ToList(); // Aggregate to country
                if (options.IsByLevelAggregation && options.SelectedAdminLevels.Count > 0)
                    selectedLevels = list.Where(a => options.SelectedAdminLevels.Select(s => s.Id).Contains(a.Id)).ToList();  // aggregate to level

                // AGGREGATE INDICATORS, PUT IN TABLE
                foreach (var level in selectedLevels) // Each admin level
                {
                    foreach (var year in options.SelectedYears) // Each year
                    {
                        foreach (KeyValuePair<string, AggregateIndicator> columnDef in options.Columns) // each column
                        {
                            if (columnDef.Value.Year != year)
                                continue;

                            string levelAndYear = level.Id + "_" + year;

                            object value = null;
                            if (level.Indicators.ContainsKey(columnDef.Key)) // The level already has the indicator, don't aggregate
                                value = IndicatorAggregator.Aggregate(level.Indicators[columnDef.Key], null);
                            else
                                value = IndicatorAggregator.AggregateChildren(level.Children, columnDef.Key, null); // level doesn't have it, aggregate children

                            if (value == null)
                                continue;

                            AddToTable(result, resultDic, level, year, columnDef.Value, levelAndYear, value, Translations.NA);
                        }
                    }
                }


            }

            // Do calculated fields
            if (hasCalculations)
            {
                var fields = selectedCalcFields.Select(i => i.TypeId + i.Key).ToList();
                foreach (ReportRow row in resultDic.Values)
                {
                    var adminLevelDemo = calc.GetAdminLevelDemo(row.AdminLevelId, row.Year);
                    foreach (var field in selectedCalcFields)
                    {
                        Dictionary<string, Dictionary<string, string>> relatedByType = CreateCalcRelatedValueDic(row.CalcRelated.Where(i => i.TypeId == field.TypeId));
                        foreach (var related in relatedByType)
                        {
                            var calcResult = calc.GetCalculatedValue(field.TypeId + field.Key, related.Value, adminLevelDemo);
                            if (!result.Columns.Contains(calcResult.Key + related.Key))
                                result.Columns.Add(new DataColumn(calcResult.Key + related.Key));
                            row.Row[calcResult.Key + related.Key] = calcResult.Value;
                        }
                    }
                }
            }

            return result;
        }

        #region Shared Methods
        private static void AddToTable(DataTable result, Dictionary<string, ReportRow> resultDic, AdminLevelIndicators level, int year, AggregateIndicator indicator,
            string rowKey, object value, string typeName)
        {
            // Add row if it doesn't exist
            if (!resultDic.ContainsKey(rowKey))
            {
                DataRow dr = result.NewRow();
                dr[Translations.Location] = level.Name;
                dr[Translations.Type] = typeName;
                dr[Translations.Year] = year;
                result.Rows.Add(dr);
                resultDic.Add(rowKey, new ReportRow { Row = dr, AdminLevelId = level.Id, Year = year });
            }

            // add column if it doesn't exist
            if (indicator.Name != null && !result.Columns.Contains(indicator.Name) && !indicator.IsCalcRelated)
                result.Columns.Add(new DataColumn(indicator.Name));

            if (indicator.Name != null && !indicator.IsCalcRelated)
                resultDic[rowKey].Row[indicator.Name] = value;
            else // Related to a calculated field
            {
                indicator.Value = value == null ? "" : value.ToString();
                resultDic[rowKey].CalcRelated.Add(indicator);
            }
        }

        private Dictionary<string, Dictionary<string, string>> CreateCalcRelatedValueDic(IEnumerable<AggregateIndicator> list)
        {
            Dictionary<string, Dictionary<string, string>> dic = new Dictionary<string, Dictionary<string, string>>();
            foreach (var inds in list.GroupBy(i => i.ColumnTypeName))
            {
                dic.Add(inds.Key, new Dictionary<string, string>());
                foreach (var ind in inds)
                    if(!dic[inds.Key].ContainsKey(ind.TypeId + ind.Key))
                        dic[inds.Key].Add(ind.TypeId + ind.Key, ind.Value);
            }

            return dic;
        }

        protected virtual void Init() { }

        protected virtual string GetIndKey(OleDbDataReader reader, bool isNotAgg)
        {
            string key = reader.GetValueOrDefault<int>("IndicatorId").ToString() + "_" + reader.GetValueOrDefault<int>("YearReported") + "_" + reader.GetValueOrDefault<string>("TName");
            if (isNotAgg)
                return reader.GetValueOrDefault<int>("ID").ToString() + "_" + key;
            return key;
        }

        protected virtual string GetColName(OleDbDataReader reader)
        {
            string name = reader.GetValueOrDefault<string>("IndicatorName");
            object isDisplayed = reader["IsDisplayed"];
            if(!Convert.ToBoolean(isDisplayed))
                name = TranslationLookup.GetValue(name);
            if (name == Translations.NoTranslationFound || name.Length == 0)
                return null;

            return name + " - " + GetTypeName(reader);
        }

        protected virtual string GetColTypeName(OleDbDataReader reader)
        {
            return " - " + GetTypeName(reader);
        }

        protected string GetTypeName(OleDbDataReader reader)
        {
            var name = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("TName"),
                    reader.GetValueOrDefault<string>("TName"));
            return name;
        }

        protected string GetValueOrBlank(string value, string separator)
        {
            if (string.IsNullOrEmpty(value))
                return "";
            return separator + value;
        }

        protected string GetValueOrBlank(int? value, string separator)
        {
            if (!value.HasValue)
                return "";
            return separator + value.Value.ToString();
        }

        protected void AddIndicatorToTable<T>(string displayName, string colName, CreateAggParams param)
        {
            if (!param.Table.Columns.Contains(displayName))
                param.Table.Columns.Add(new DataColumn(displayName));

            param.Row[displayName] = param.Reader.GetValueOrDefault<T>(colName);
        }

        protected void AddCalcToTable(string displayName, string val, CreateAggParams param)
        {
            if (!param.Table.Columns.Contains(displayName))
                param.Table.Columns.Add(new DataColumn(displayName));

            param.Row[displayName] = val;
        }
        
        protected void IndicatorListToTree(List<AdminLevelIndicators> list, Dictionary<int, AdminLevelIndicators> dic)
        {
            var rootNodes = new List<AdminLevelIndicators>();
            foreach (var node in list)
                if (node.ParentId.HasValue && node.ParentId.Value > 0)
                {
                    AdminLevelIndicators parent = dic[node.ParentId.Value];
                    node.Parent = parent;
                    parent.Children.Add(node);
                }
                else
                    rootNodes.Add(node);
        }

        protected void AddRelatedCalcIndicators(ReportOptions options, Dictionary<int, AdminLevelIndicators> dic, OleDbCommand command,
            OleDbConnection connection, Func<OleDbDataReader, bool, string> getAggKey, Func<OleDbDataReader, string> getName, Func<OleDbDataReader, string> getType,
            Action<CreateAggParams> sind,
            int entityTypeId)
        {
            ReportRepository repo = new ReportRepository();
            string intv = @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        Interventions.ID, 
                        Interventions.YearReported, 
                        Interventions.PcIntvRoundNumber, 
                        InterventionTypes.InterventionTypeName as TName,     
                        InterventionTypes.ID as Tid,      
                        InterventionIndicators.ID as IndicatorId, 
                        InterventionIndicators.DisplayName as IndicatorName, 
                        InterventionIndicators.IsDisplayed, 
                        InterventionIndicators.DataTypeId, 
                        InterventionIndicators.AggTypeId, 
                        InterventionIndicatorValues.DynamicValue
                        FROM (((((Interventions INNER JOIN InterventionTypes on Interventions.InterventionTypeId = InterventionTypes.ID)
                            INNER JOIN InterventionIndicatorValues on Interventions.Id = InterventionIndicatorValues.InterventionId)
                            INNER JOIN AdminLevels on Interventions.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN InterventionIndicators on InterventionIndicators.ID = InterventionIndicatorValues.IndicatorId)
                            INNER JOIN IndicatorCalculations on InterventionIndicators.ID = IndicatorCalculations.RelatedIndicatorId) 
                        WHERE Interventions.IsDeleted = 0 AND IndicatorCalculations.RelatedEntityTypeId = 2 AND 
                              IndicatorCalculations.IndicatorId in "
            + " (" + String.Join(", ", options.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ")  AND IndicatorCalculations.EntityTypeId = " + entityTypeId
            + " AND InterventionTypes.ID in (" + String.Join(", ", options.SelectedIndicators.Select(i => i.TypeId.ToString()).Distinct().ToArray()) + ") "
            + ReportRepository.CreateYearFilter(options) + ReportRepository.CreateAdminFilter(options) + @"
                        GROUP BY 
                        AdminLevels.ID, 
                        AdminLevels.DisplayName,
                        Interventions.ID, 
                        Interventions.YearReported, 
                        Interventions.PcIntvRoundNumber, 
                        InterventionTypes.InterventionTypeName,     
                        InterventionTypes.ID,      
                        InterventionIndicators.ID, 
                        InterventionIndicators.DisplayName, 
                        InterventionIndicators.IsDisplayed, 
                        InterventionIndicators.DataTypeId, 
                        InterventionIndicators.AggTypeId, 
                        InterventionIndicatorValues.DynamicValue";

            repo.AddIndicatorsToAggregate(intv, options, dic, command, connection, getAggKey, getName, getType, sind, true);

            string dd = @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        DiseaseDistributions.ID, 
                        DiseaseDistributions.YearReported, 
                        Diseases.DisplayName as TName,       
                        Diseases.ID as Tid,   
                        DiseaseDistributionIndicators.ID as IndicatorId, 
                        DiseaseDistributionIndicators.DisplayName as IndicatorName, 
                        DiseaseDistributionIndicators.IsDisplayed, 
                        DiseaseDistributionIndicators.DataTypeId, 
                        DiseaseDistributionIndicators.AggTypeId, 
                        DiseaseDistributionIndicatorValues.DynamicValue
                        FROM (((((DiseaseDistributions INNER JOIN Diseases on DiseaseDistributions.DiseaseId = Diseases.ID)
                            INNER JOIN DiseaseDistributionIndicatorValues on DiseaseDistributions.Id = DiseaseDistributionIndicatorValues.DiseaseDistributionId)
                            INNER JOIN AdminLevels on DiseaseDistributions.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN DiseaseDistributionIndicators on DiseaseDistributionIndicators.ID = DiseaseDistributionIndicatorValues.IndicatorId)
                            INNER JOIN IndicatorCalculations on DiseaseDistributionIndicators.ID = IndicatorCalculations.RelatedIndicatorId) 
                        WHERE DiseaseDistributions.IsDeleted = 0 AND  IndicatorCalculations.RelatedEntityTypeId = 1 AND
                               IndicatorCalculations.IndicatorId in "
            + " (" + String.Join(", ", options.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ")  AND IndicatorCalculations.EntityTypeId = "
            + entityTypeId + ReportRepository.CreateYearFilter(options) + ReportRepository.CreateAdminFilter(options) + @"
                        GROUP BY  
                        AdminLevels.ID, 
                        AdminLevels.DisplayName,
                        DiseaseDistributions.ID, 
                        DiseaseDistributions.YearReported, 
                        Diseases.DisplayName,       
                        Diseases.ID,   
                        DiseaseDistributionIndicators.ID, 
                        DiseaseDistributionIndicators.DisplayName, 
                        DiseaseDistributionIndicators.IsDisplayed, 
                        DiseaseDistributionIndicators.DataTypeId, 
                        DiseaseDistributionIndicators.AggTypeId, 
                        DiseaseDistributionIndicatorValues.DynamicValue";

            repo.AddIndicatorsToAggregate(dd, options, dic, command, connection, getAggKey, getName, getType, sind, true);

            string survey = @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        Surveys.ID, 
                        Surveys.YearReported, 
                        SurveyTypes.SurveyTypeName as TName,        
                        SurveyTypes.ID as Tid,      
                        SurveyIndicators.ID as IndicatorId, 
                        SurveyIndicators.DisplayName as IndicatorName, 
                        SurveyIndicators.IsDisplayed, 
                        SurveyIndicators.DataTypeId, 
                        SurveyIndicators.AggTypeId,    
                        SurveyIndicatorValues.DynamicValue
                        FROM ((((((Surveys INNER JOIN SurveyTypes on Surveys.SurveyTypeId = SurveyTypes.ID)
                            INNER JOIN SurveyIndicatorValues on Surveys.Id = SurveyIndicatorValues.SurveyId)
                            INNER JOIN Surveys_to_AdminLevels on Surveys_to_AdminLevels.SurveyId = Surveys.ID) 
                            INNER JOIN AdminLevels on Surveys_to_AdminLevels.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN SurveyIndicators on SurveyIndicators.ID = SurveyIndicatorValues.IndicatorId)
                            INNER JOIN IndicatorCalculations on SurveyIndicators.ID = IndicatorCalculations.RelatedIndicatorId) 
                        WHERE Surveys.IsDeleted = 0 AND IndicatorCalculations.RelatedEntityTypeId = 3 AND 
                              IndicatorCalculations.IndicatorId in "
            + " (" + String.Join(", ", options.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ")  AND IndicatorCalculations.EntityTypeId = "
            + entityTypeId + ReportRepository.CreateYearFilter(options) + ReportRepository.CreateAdminFilter(options) + @"
                        GROUP BY 
                        AdminLevels.ID, 
                        AdminLevels.DisplayName,
                        Surveys.ID, 
                        Surveys.YearReported, 
                        SurveyTypes.SurveyTypeName,        
                        SurveyTypes.ID,      
                        SurveyIndicators.ID, 
                        SurveyIndicators.DisplayName, 
                        SurveyIndicators.IsDisplayed, 
                        SurveyIndicators.DataTypeId, 
                        SurveyIndicators.AggTypeId,    
                        SurveyIndicatorValues.DynamicValue";

            repo.AddIndicatorsToAggregate(survey, options, dic, command, connection, getAggKey, getName, getType, sind, true);
        }
        #endregion
    }

    public class IntvReportGenerator : BaseReportGenerator
    {
        protected override int EntityTypeId { get { return (int)IndicatorEntityType.Intervention; } }
        protected override void Init()
        {
            calc = new CalcIntv();
        }

        protected override string CmdText()
        {
            return @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        Interventions.ID, 
                        Interventions.YearReported, 
                        Interventions.PcIntvRoundNumber, 
                        InterventionTypes.InterventionTypeName as TName, 
                        InterventionTypes.ID as Tid,      
                        InterventionIndicators.ID as IndicatorId, 
                        InterventionIndicators.DisplayName as IndicatorName, 
                        InterventionIndicators.IsDisplayed, 
                        InterventionIndicators.DataTypeId, 
                        InterventionIndicators.AggTypeId, 
                        InterventionIndicatorValues.DynamicValue
                        FROM ((((Interventions INNER JOIN InterventionTypes on Interventions.InterventionTypeId = InterventionTypes.ID)
                            INNER JOIN InterventionIndicatorValues on Interventions.Id = InterventionIndicatorValues.InterventionId)
                            INNER JOIN AdminLevels on Interventions.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN InterventionIndicators on InterventionIndicators.ID = InterventionIndicatorValues.IndicatorId)
                        WHERE Interventions.IsDeleted = 0 AND  
                              InterventionIndicators.Id in "
            + " (" + String.Join(", ", opts.SelectedIndicators.Select(s => s.ID.ToString()).ToArray())
            + ") AND InterventionTypes.ID in (" + String.Join(", ", opts.SelectedIndicators.Select(i => i.TypeId.ToString()).Distinct().ToArray())  + ") "
            + ReportRepository.CreateYearFilter(opts) + ReportRepository.CreateAdminFilter(opts);
        }

        protected override string GetIndKey(OleDbDataReader reader, bool isNotAgg)
        {
            string key = reader.GetValueOrDefault<int>("IndicatorId").ToString() + "_" + reader.GetValueOrDefault<int>("YearReported") + "_" +
                    reader.GetValueOrDefault<string>("TName") + GetValueOrBlank(reader.GetValueOrDefault<Nullable<int>>("PcIntvRoundNumber"), "_");
            if (isNotAgg)
                return reader.GetValueOrDefault<int>("ID").ToString() + "_" + key;
            return key;
        }

        protected override string GetColName(OleDbDataReader reader)
        {
            string name = reader.GetValueOrDefault<string>("IndicatorName");
            if (!reader.GetValueOrDefault<bool>("IsDisplayed"))
                name = TranslationLookup.GetValue(name);

            return name + " - " +
                GetTypeName(reader) +
                GetValueOrBlank(reader.GetValueOrDefault<Nullable<int>>("PcIntvRoundNumber"), string.Format(" - {0} ", Translations.Round));
        }

        protected override string GetColTypeName(OleDbDataReader reader)
        {
            return " - " + GetTypeName(reader) +
                GetValueOrBlank(reader.GetValueOrDefault<Nullable<int>>("PcIntvRoundNumber"), string.Format(" - {0} ", Translations.Round));
        }
    }

    public class SurveyReportGenerator : BaseReportGenerator
    {
        protected override int EntityTypeId { get { return (int)IndicatorEntityType.Survey; } }
        protected override void Init()
        {
            calc = new CalcSurvey();
            AddRequiredIndicator(14, 466);
            AddRequiredIndicator(20, 467);
            AddRequiredIndicator(18, 420);
            AddRequiredIndicator(17, 419);
            AddRequiredIndicator(12, 418);
            AddRequiredIndicator(11, 417);
            AddRequiredIndicator(15, 415);
        }

        private void AddRequiredIndicator(int typeId, int indicatorId)
        {
            if (opts.SelectedIndicators.FirstOrDefault(i => i.TypeId == typeId) != null && opts.SelectedIndicators.FirstOrDefault(x => x.ID == indicatorId) == null)
                FindAndAddIndicator(indicatorId, typeId);
        }

        private void FindAndAddIndicator(int indicatorId, int typeId)
        {
            var reportType = opts.AvailableIndicators[0].Children.FirstOrDefault(t => t.ID == typeId);
            if(reportType != null)
            {
                var ind = reportType.Children.FirstOrDefault(v => v.ID == indicatorId);
                if(ind != null)
                    opts.SelectedIndicators.Add(ind);
            }
        }

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
                        SurveyTypes.ID as Tid,      
                        SurveyIndicators.ID as IndicatorId, 
                        SurveyIndicators.DisplayName as IndicatorName, 
                        SurveyIndicators.IsDisplayed, 
                        SurveyIndicators.DataTypeId, 
                        SurveyIndicators.AggTypeId,    
                        SurveyIndicatorValues.DynamicValue,
                        Surveys.SpotCheckName as IndSpotCheckName,
                        Surveys.SpotCheckLat as IndSpotCheckLat,
                        Surveys.SpotCheckLng as IndSpotCheckLng,
                        Surveys.SiteType, 
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
                       staticConditional
                       + ReportRepository.CreateYearFilter(opts) + ReportRepository.CreateAdminFilter(opts);
            else
                return @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        Surveys.ID, 
                        Surveys.YearReported, 
                        SurveyTypes.SurveyTypeName as TName,           
                        SurveyTypes.ID as Tid,       
                        0 as IndicatorId, 
                        '' as IndicatorName, 
                        0 as IsDisplayed, 
                        1 as DataTypeId, 
                        1 as AggTypeId,    
                        '' as DynamicValue,
                        Surveys.SpotCheckName as IndSpotCheckName,
                        Surveys.SpotCheckLat as IndSpotCheckLat,
                        Surveys.SpotCheckLng as IndSpotCheckLng,
                        Surveys.SiteType, 
                        SentinelSites.SiteName as IndSentinelSiteName,
                        SentinelSites.Lat as IndSentinelSiteLat,
                        SentinelSites.Lng as IndSentinelSiteLng
                        FROM ((((Surveys INNER JOIN SurveyTypes on Surveys.SurveyTypeId = SurveyTypes.ID)
                            INNER JOIN Surveys_to_AdminLevels on Surveys_to_AdminLevels.SurveyId = Surveys.ID) 
                            INNER JOIN AdminLevels on Surveys_to_AdminLevels.AdminLevelId = AdminLevels.ID) 
                            LEFT OUTER JOIN SentinelSites on Surveys.SentinelSiteId = SentinelSites.ID)
                        WHERE Surveys.IsDeleted = 0 "
                    + staticConditional
                    + ReportRepository.CreateYearFilter(opts) + ReportRepository.CreateAdminFilter(opts);

        }
        
        protected override void AddStaticAggInd(CreateAggParams param)
        {
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSpotCheckName) != null)
                AddIndicatorAndColumn<string>("IndSpotCheckName", Translations.IndSpotCheckName, param);
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
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.SiteType) != null)
                AddIndicatorAndColumn<string>("SiteType", Translations.SiteType, param);
        }

        private void AddIndicatorAndColumn<T>(string columnName, string transName, CreateAggParams param)
        {
            string key = columnName + "_" + param.Reader.GetValueOrDefault<int>("YearReported") + "_" + param.Reader.GetValueOrDefault<string>("TName");
            string displayName = transName + " - " + TranslationLookup.GetValue(param.Reader.GetValueOrDefault<string>("TName"));
            T val = param.Reader.GetValueOrDefault<T>(columnName);
            if (!param.AdminLevel.Indicators.ContainsKey(key))
            {
                var ind = new AggregateIndicator
                {
                    Name = displayName,
                    Key = key,
                    DataType = (int)IndicatorDataType.Text,
                    Value = val == null ? null : val.ToString(),
                    AggType = (int)IndicatorAggType.None,
                    Year = param.Reader.GetValueOrDefault<int>("YearReported"),
                    TypeName = param.Reader.GetValueOrDefault<string>("TName"),
                    IsCalcRelated = false,
                    FormId = param.Reader.GetValueOrDefault<int>("ID")
                };
                param.AdminLevel.Indicators.Add(key, ind);

                // Add Column
                if (!param.Options.Columns.ContainsKey(key))
                    param.Options.Columns.Add(key, ind);
            }
        }
    }

    public class DistributionReportGenerator : BaseReportGenerator
    {
        protected override int EntityTypeId { get { return (int)IndicatorEntityType.DiseaseDistribution; } }
        protected override void Init()
        {
            calc = new CalcDistro();
        }

        protected override string CmdText()
        {
            return @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        DiseaseDistributions.ID, 
                        DiseaseDistributions.YearReported, 
                        Diseases.DisplayName as TName,       
                        Diseases.ID as Tid,       
                        DiseaseDistributionIndicators.ID as IndicatorId, 
                        DiseaseDistributionIndicators.DisplayName as IndicatorName, 
                        DiseaseDistributionIndicators.IsDisplayed, 
                        DiseaseDistributionIndicators.DataTypeId, 
                        DiseaseDistributionIndicators.AggTypeId, 
                        DiseaseDistributionIndicatorValues.DynamicValue
                        FROM ((((DiseaseDistributions INNER JOIN Diseases on DiseaseDistributions.DiseaseId = Diseases.ID)
                            INNER JOIN DiseaseDistributionIndicatorValues on DiseaseDistributions.Id = DiseaseDistributionIndicatorValues.DiseaseDistributionId)
                            INNER JOIN AdminLevels on DiseaseDistributions.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN DiseaseDistributionIndicators on DiseaseDistributionIndicators.ID = DiseaseDistributionIndicatorValues.IndicatorId)
                        WHERE DiseaseDistributions.IsDeleted = 0 AND  
                              DiseaseDistributionIndicators.Id in "
            + " (" + String.Join(", ", opts.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ") "
            + ReportRepository.CreateYearFilter(opts) + ReportRepository.CreateAdminFilter(opts);
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
                        Diseases.ID as Tid,        
                        Diseases.ID as IndicatorId, 
                        ProcessIndicators.DisplayName as IndicatorName, 
                        ProcessIndicators.IsDisplayed, 
                        ProcessIndicators.DataTypeId, 
                        ProcessIndicators.AggTypeId, 
                        ProcessIndicatorValues.DynamicValue
                        FROM ((((Processes INNER JOIN ProcessTypes on Processes.ProcessTypeId = ProcessTypes.ID)
                            INNER JOIN ProcessIndicatorValues on Processes.Id = ProcessIndicatorValues.ProcessId)
                            INNER JOIN AdminLevels on Processes.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN ProcessIndicators on ProcessIndicators.ID = ProcessIndicatorValues.IndicatorId)
                        WHERE Processes.IsDeleted = 0 AND  
                              ProcessIndicators.Id in "
            + " (" + String.Join(", ", opts.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ") "
            + ReportRepository.CreateYearFilter(opts) + ReportRepository.CreateAdminFilter(opts);
        }

        protected override string GetIndKey(OleDbDataReader reader, bool isNotAgg)
        {
            string key = reader.GetValueOrDefault<int>("IndicatorId").ToString() + "_" + reader.GetValueOrDefault<int>("YearReported") + "_" +
                reader.GetValueOrDefault<string>("TName") + GetValueOrBlank(reader.GetValueOrDefault<string>("SCMDrug"), "_") +
                GetValueOrBlank(reader.GetValueOrDefault<string>("PCTrainTrainingCategory"), "_");
            if (isNotAgg)
                return reader.GetValueOrDefault<int>("ID").ToString() + "_" + key;
            return key;
        }

        protected override string GetColName(OleDbDataReader reader)
        {
            string name = reader.GetValueOrDefault<string>("IndicatorName");
            if (!reader.GetValueOrDefault<bool>("IsDisplayed"))
                name = TranslationLookup.GetValue(name);

            return name + " - " +
                GetTypeName(reader) +
                GetValueOrBlank(reader.GetValueOrDefault<string>("SCMDrug"), " - ") +
                GetValueOrBlank(reader.GetValueOrDefault<string>("PCTrainTrainingCategory"), " - ").Replace("|", ", ");
        }

        protected override string GetColTypeName(OleDbDataReader reader)
        {
            return " - " + GetTypeName(reader) +
                GetValueOrBlank(reader.GetValueOrDefault<string>("SCMDrug"), " - ") +
                GetValueOrBlank(reader.GetValueOrDefault<string>("PCTrainTrainingCategory"), " - ").Replace("|", ", ");
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
