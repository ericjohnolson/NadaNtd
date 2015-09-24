using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Process;
using Nada.Model.Repositories;
using Nada.Model.Survey;

namespace Nada.Model.Reports
{
    public interface IReportGenerator
    {
        ReportResult Run(SavedReport report);
    }

    [Serializable]
    public class BaseReportGenerator : IReportGenerator
    {
        [NonSerialized]
        protected ICalcIndicators calc = null;
        [NonSerialized]
        protected ReportRepository repo = null;
        [NonSerialized]
        protected DemoRepository demo = null;
        [NonSerialized]
        protected ReportOptions opts = null;
        [NonSerialized]
        protected List<ReportIndicator> selectedCalcFields = null;
        [NonSerialized]
        protected bool hasCalculations = false;
        [NonSerialized]
        protected IndicatorParser indicatorParser = new IndicatorParser();
        [NonSerialized]
        protected List<IndicatorDropdownValue> dropdownOptions = new List<IndicatorDropdownValue>();
        protected virtual string CmdText() { throw new NotImplementedException(); }
        protected virtual int EntityTypeId { get { return 0; } }
        protected virtual bool IsDemoOrDistro { get { return false; } }

        public BaseReportGenerator() { }

        public virtual ReportResult Run(SavedReport report)
        {
            Initialize();
            return DoRun(report.ReportOptions);
        }

        protected void Initialize()
        {
            repo = new ReportRepository();
            demo = new DemoRepository();
            selectedCalcFields = new List<ReportIndicator>();
            dropdownOptions = repo.GetAllDropdownOptions();
            repo.LoadRelatedLists();
        }

        protected virtual ReportResult DoRun(ReportOptions options)
        {
            opts = options;
            selectedCalcFields = opts.SelectedIndicators.Where(c => c.DataTypeId == (int)IndicatorDataType.Calculated).ToList();
            hasCalculations = selectedCalcFields.Count > 0;
            Init();
            ReportResult result = CreateReport(options);
            result.ChartData = result.DataTableResults.Copy();
            return result;
        }

        protected virtual void AddStaticAggInd(CreateAggParams param) { }

        public ReportResult CreateReport(ReportOptions options)
        {
            List<AdminLevelIndicators> list = new List<AdminLevelIndicators>();
            Dictionary<int, AdminLevelIndicators> dic = new Dictionary<int, AdminLevelIndicators>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            indicatorParser.LoadRelatedLists();

            // Get all indicators
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                list = ExportRepository.GetAdminLevels(command, connection, options.IsAllLocations && options.ShowOnlyRedistrictedUnits);
                
                dic = list.ToDictionary(n => n.Id, n => n);
                repo.AddIndicatorsToAggregate(CmdText(), options, dic, command, connection, GetIndKey, GetColName, GetColTypeName, AddStaticAggInd, false, IsDemoOrDistro, EntityTypeId, dropdownOptions);
                if (hasCalculations)
                    AddRelatedCalcIndicators(options, dic, command, connection, GetIndKey, GetColName, GetColTypeName,
                        AddStaticAggInd, EntityTypeId);
            }

            // Create table
            ReportResult reportResult = new ReportResult();
            DataTable result = new DataTable();
            result.Columns.Add(new DataColumn("ID"));
            result.Columns.Add(new DataColumn(Translations.Location));
            result.Columns.Add(new DataColumn(Translations.Type));
            result.Columns.Add(new DataColumn(Translations.Year));
            result.Columns.Add(new DataColumn("YearNumber"));
            Dictionary<string, ReportRow> resultDic = new Dictionary<string, ReportRow>();
            if (options.IsNoAggregation)
            {
                foreach (var level in list) // Each admin level
                    foreach (var ind in level.Indicators) // each indicator
                        AddToTable(result, resultDic, level, ind.Value.Year, ind.Value, ind.Value.FormId.ToString() + level.Id, ind.Value, TranslationLookup.GetValue(ind.Value.TypeName, ind.Value.TypeName), options);
            }
            else
            {
                IndicatorListToTree(list, dic);

                List<AdminLevelIndicators> selectedLevels = new List<AdminLevelIndicators>();
                if (options.IsCountryAggregation)
                    selectedLevels = list.Where(a => a.LevelNumber == 0).ToList(); // Aggregate to country
                if (options.IsByLevelAggregation && options.SelectedAdminLevels.Count > 0)
                    selectedLevels = list.Where(a => options.SelectedAdminLevels.Select(s => s.Id).Contains(a.Id)).ToList();  // aggregate to level

                List<int> years = new List<int> { 0 };
                if(!options.IsGroupByRange)
                    years = GetSelectedYears(options);

                // AGGREGATE INDICATORS, PUT IN TABLE
                foreach (var level in selectedLevels) // Each admin level
                {
                    foreach (var year in years) // Each year
                    {
                        foreach (KeyValuePair<string, AggregateIndicator> columnDef in options.Columns) // each column
                        {
                            if (!options.IsGroupByRange && columnDef.Value.Year != year)
                                continue;

                            string levelAndYear = level.Id + "_" + year;

                            AggregateIndicator aggInd = null;
                            if (level.Indicators.ContainsKey(columnDef.Key))
                                aggInd = level.Indicators[columnDef.Key];
                            else
                                aggInd = IndicatorAggregator.AggregateChildren(level.Children, columnDef.Key, null, dropdownOptions); // level doesn't have it, aggregate children

                            if (aggInd == null)
                                continue;

                            AddToTable(result, resultDic, level, year, columnDef.Value, levelAndYear, aggInd, Translations.NA, options);
                        }
                    }
                }
            }

            // Do calculated fields
            if (hasCalculations)
            {
                string errors = "";
                var fields = selectedCalcFields.Select(i => i.TypeId + i.Key).ToList();
                foreach (ReportRow row in resultDic.Values)
                {
                    DateTime startDate = options.IsGroupByRange ? options.StartDate : new DateTime(row.Year, options.MonthYearStarts, 1);
                    DateTime yearEndDate = options.IsGroupByRange ? options.EndDate : new DateTime(row.Year, options.MonthYearStarts, 1).AddYears(1).AddDays(-1);
                    var adminLevelDemo = calc.GetAdminLevelDemo(row.AdminLevelId, startDate, yearEndDate);
                    if (adminLevelDemo.Id <= 0)
                        errors += string.Format(Translations.ReportsNoDemographyInDateRange, row.AdminLevelName, startDate.ToShortDateString(), yearEndDate.ToShortDateString()) + Environment.NewLine;
                    foreach (var field in selectedCalcFields)
                    {
                        Dictionary<string, Dictionary<string, string>> relatedByType = CreateCalcRelatedValueDic(row.CalcRelated.Where(i => i.TypeId == field.TypeId));

                        if (relatedByType.Count > 0)
                            foreach (var related in relatedByType)
                            {
                                var calcResult = calc.GetCalculatedValue(field.FormNameKey, field.Key, related.Value, adminLevelDemo, startDate, yearEndDate, ref errors);
                                if (!result.Columns.Contains(calcResult.Key + related.Key))
                                    result.Columns.Add(new DataColumn(calcResult.Key + related.Key));
                                row.Row[calcResult.Key + related.Key] = calcResult.Value;
                            }
                        else // no related values for the calculation, this is for metadata values.
                        {
                            var calcResult = calc.GetCalculatedValue(field.FormNameKey, field.Key, null, adminLevelDemo, startDate, yearEndDate, ref errors);
                            if (!result.Columns.Contains(calcResult.Key))
                                result.Columns.Add(new DataColumn(calcResult.Key));
                            row.Row[calcResult.Key] = calcResult.Value;
                        }
                    }
                }
                reportResult.MetaDataWarning = errors;
            }

            if (IsDemoOrDistro)
                reportResult.MetaDataWarning += GetMissingRowsErrors(result, false);

            result.Columns.Remove("YearNumber");
            reportResult.DataTableResults = result;
            return reportResult;
        }

        #region Shared Methods
        private void AddToTable(DataTable result, Dictionary<string, ReportRow> resultDic, AdminLevelIndicators level, int year, AggregateIndicator originalIndicator,
            string rowKey, AggregateIndicator indValue, string typeName, ReportOptions options)
        {
            // The AggregateIndicator originalIndicator passed in is a reference to the AggregateIndicator in the ReportOptions.Columns.
            // In some cases, the AggregateIndicator is modified below. To prevent it from modifying the original reference, the AggregateIndicator
            // is cloned
            AggregateIndicator indicator = Util.DeepClone(originalIndicator);

            object value = indicatorParser.Parse(indicator.DataType, indicator.IndicatorId, indValue.Value);

            // Add row if it doesn't exist
            if (!resultDic.ContainsKey(rowKey))
            {
                DataRow dr = result.NewRow();
                List<AdminLevel> parents = demo.GetAdminLevelParentNames(level.Id, options.ShowOnlyRedistrictedUnits);
                for (int i = 0; i < parents.Count; i++)
                {
                    if (!result.Columns.Contains(parents[i].LevelName))
                    {
                        DataColumn dc = new DataColumn(parents[i].LevelName);
                        result.Columns.Add(dc);
                        dc.SetOrdinal(i);
                    }
                    dr[parents[i].LevelName] = parents[i].Name;
                }
                dr["ID"] = level.Id;
                dr[Translations.Location] = level.Name;
                dr[Translations.Type] = typeName;
                dr["YearNumber"] = year;
                if (year > 0)
                {
                    DateTime startMonth = new DateTime(year, options.MonthYearStarts, 1);
                    dr[Translations.Year] = startMonth.ToString("MMM yyyy") + "-" + startMonth.AddYears(1).AddMonths(-1).ToString("MMM yyyy");
                }

                AddRedistrictingColumn(result, level, options, dr);
                AddTypeSpecificColumns(result, level, options, dr, indValue);

                // insert
                result.Rows.Add(dr);
                resultDic.Add(rowKey, new ReportRow { Row = dr, AdminLevelId = level.Id, AdminLevelName = level.Name, Year = year });
            }

            // add column if it doesn't exist
            if (indicator.Name != null && !result.Columns.Contains(indicator.Name) && !indicator.IsCalcRelated)
            {
                if (indicator.DataType == (int)IndicatorDataType.Number)
                    result.Columns.Add(new DataColumn(indicator.Name, typeof(double)));
                else
                    result.Columns.Add(new DataColumn(indicator.Name));
            }

            if (indicator.Name != null && !indicator.IsCalcRelated)
            {
                if (value == null || value.ToString().Length == 0)
                    return;

                resultDic[rowKey].Row[indicator.Name] = value;
            }
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
                    if (!dic[inds.Key].ContainsKey(ind.TypeName + ind.Key))
                        dic[inds.Key].Add(ind.TypeName + ind.Key, ind.Value);
            }

            return dic;
        }

        protected virtual void Init() { }

        protected virtual void AddTypeSpecificColumns(DataTable result, AdminLevelIndicators level, ReportOptions options, DataRow dr, AggregateIndicator indValue) { }

        private void AddRedistrictingColumn(DataTable result, AdminLevelIndicators level, ReportOptions options, DataRow dr)
        {
            if (options.ShowRedistrictEvents)
            {
                if (!result.Columns.Contains(Translations.RedistrictingNotes))
                    result.Columns.Add(new DataColumn(Translations.RedistrictingNotes));
                if (level.RedistrictIdForDaughter > 0)
                    dr[Translations.RedistrictingNotes] = demo.GetRedistrictingDaughterNote(level.RedistrictIdForDaughter);
                else if (level.RedistrictIdForMother > 0)
                    dr[Translations.RedistrictingNotes] = demo.GetRedistrictingMotherNote(level.RedistrictIdForMother);
            }
        }

        protected virtual string GetIndKey(OleDbDataReader reader, bool isNotAgg, ReportOptions options)
        {
            int year = options.IsGroupByRange ? 0 : Util.GetYearReported(options.MonthYearStarts, reader.GetValueOrDefault<DateTime>("DateReported"));
            string key = reader.GetValueOrDefault<int>("IndicatorId").ToString() + "_" + year + "_" + reader.GetValueOrDefault<string>("TName");
            if (isNotAgg)
                return reader.GetValueOrDefault<int>("ID").ToString() + "_" + key;
            return key;
        }

        protected virtual string GetColName(OleDbDataReader reader)
        {
            string name = reader.GetValueOrDefault<string>("IndicatorName");
            object IsEditable = reader["IsEditable"];
            if (!Convert.ToBoolean(IsEditable))
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
            OleDbConnection connection, Func<OleDbDataReader, bool, ReportOptions, string> getAggKey, Func<OleDbDataReader, string> getName, Func<OleDbDataReader, string> getType,
            Action<CreateAggParams> sind,
            int entityTypeId)
        {
            ReportRepository repo = new ReportRepository();
            string intv = @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        Interventions.ID, 
                        [DateReported], 
                        Interventions.PcIntvRoundNumber, 
                        InterventionTypes.InterventionTypeName as TName,     
                        InterventionTypes.ID as Tid,      
                        InterventionIndicators.ID as IndicatorId, 
                        InterventionIndicators.DisplayName as IndicatorName, 
                        InterventionIndicators.IsEditable, 
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
            + ReportRepository.CreateYearFilter(options, "DateReported") + ReportRepository.CreateAdminFilter(options)
                // Group by is so that when multiple calcs reference the same indicator we only get it once.
            + @"
                        GROUP BY 
                        AdminLevels.ID, 
                        AdminLevels.DisplayName,
                        Interventions.ID, 
                        [DateReported], 
                        Interventions.PcIntvRoundNumber, 
                        InterventionTypes.InterventionTypeName,     
                        InterventionTypes.ID,      
                        InterventionIndicators.ID, 
                        InterventionIndicators.DisplayName, 
                        InterventionIndicators.IsEditable, 
                        InterventionIndicators.DataTypeId, 
                        InterventionIndicators.AggTypeId, 
                        InterventionIndicatorValues.DynamicValue";

            repo.AddIndicatorsToAggregate(intv, options, dic, command, connection, getAggKey, getName, getType, sind, true, false, entityTypeId, dropdownOptions);

            string dd = @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        DiseaseDistributions.ID, 
                        [DateReported], 
                        Diseases.DisplayName as TName,       
                        Diseases.ID as Tid,   
                        DiseaseDistributionIndicators.ID as IndicatorId, 
                        DiseaseDistributionIndicators.DisplayName as IndicatorName, 
                        DiseaseDistributionIndicators.IsEditable, 
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
            + entityTypeId + ReportRepository.CreateYearFilter(options, "DateReported") + ReportRepository.CreateAdminFilter(options)
            + @"
                        GROUP BY  
                        AdminLevels.ID, 
                        AdminLevels.DisplayName,
                        DiseaseDistributions.ID, 
                        [DateReported], 
                        Diseases.DisplayName,       
                        Diseases.ID,   
                        DiseaseDistributionIndicators.ID, 
                        DiseaseDistributionIndicators.DisplayName, 
                        DiseaseDistributionIndicators.IsEditable, 
                        DiseaseDistributionIndicators.DataTypeId, 
                        DiseaseDistributionIndicators.AggTypeId, 
                        DiseaseDistributionIndicatorValues.DynamicValue";

            repo.AddIndicatorsToAggregate(dd, options, dic, command, connection, getAggKey, getName, getType, sind, true, true, entityTypeId, dropdownOptions);

            string survey = @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        Surveys.ID, 
                        [DateReported], 
                        SurveyTypes.SurveyTypeName as TName,        
                        SurveyTypes.ID as Tid,      
                        SurveyIndicators.ID as IndicatorId, 
                        SurveyIndicators.DisplayName as IndicatorName, 
                        SurveyIndicators.IsEditable, 
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
            + entityTypeId + ReportRepository.CreateYearFilter(options, "DateReported") + ReportRepository.CreateAdminFilter(options)
            + @"
                        GROUP BY 
                        AdminLevels.ID, 
                        AdminLevels.DisplayName,
                        Surveys.ID, 
                        [DateReported], 
                        SurveyTypes.SurveyTypeName,        
                        SurveyTypes.ID,      
                        SurveyIndicators.ID, 
                        SurveyIndicators.DisplayName, 
                        SurveyIndicators.IsEditable, 
                        SurveyIndicators.DataTypeId, 
                        SurveyIndicators.AggTypeId,    
                        SurveyIndicatorValues.DynamicValue";

            repo.AddIndicatorsToAggregate(survey, options, dic, command, connection, getAggKey, getName, getType, sind, true, false, entityTypeId, dropdownOptions);

            string process = @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        Processes.ID, 
                        [DateReported], 
                        Processes.SCMDrug, 
                        Processes.PCTrainTrainingCategory, 
                        ProcessTypes.TypeName as TName,        
                        ProcessTypes.ID as Tid,      
                        ProcessIndicators.ID as IndicatorId, 
                        ProcessIndicators.DisplayName as IndicatorName, 
                        ProcessIndicators.IsEditable, 
                        ProcessIndicators.DataTypeId, 
                        ProcessIndicators.AggTypeId,    
                        ProcessIndicatorValues.DynamicValue
                        FROM (((((Processes INNER JOIN ProcessTypes on Processes.ProcessTypeId = ProcessTypes.ID)
                            INNER JOIN ProcessIndicatorValues on Processes.Id = ProcessIndicatorValues.ProcessId)
                            INNER JOIN AdminLevels on Processes.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN ProcessIndicators on ProcessIndicators.ID = ProcessIndicatorValues.IndicatorId)
                            INNER JOIN IndicatorCalculations on ProcessIndicators.ID = IndicatorCalculations.RelatedIndicatorId) 
                        WHERE Processes.IsDeleted = 0 AND IndicatorCalculations.RelatedEntityTypeId = 4 AND 
                              IndicatorCalculations.IndicatorId in "
            + " (" + String.Join(", ", options.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ")  AND IndicatorCalculations.EntityTypeId = "
            + entityTypeId + ReportRepository.CreateYearFilter(options, "DateReported") + ReportRepository.CreateAdminFilter(options)
            + @"
                        GROUP BY 
                        AdminLevels.ID, 
                        AdminLevels.DisplayName,
                        Processes.ID, 
                        [DateReported], 
                        Processes.SCMDrug, 
                        Processes.PCTrainTrainingCategory, 
                        ProcessTypes.TypeName,        
                        ProcessTypes.ID,      
                        ProcessIndicators.ID, 
                        ProcessIndicators.DisplayName, 
                        ProcessIndicators.IsEditable, 
                        ProcessIndicators.DataTypeId, 
                        ProcessIndicators.AggTypeId,    
                        ProcessIndicatorValues.DynamicValue";

            repo.AddIndicatorsToAggregate(process, options, dic, command, connection, getAggKey, getName, getType, sind, true, false, entityTypeId, dropdownOptions);
        }

        protected static List<int> GetSelectedYears(ReportOptions options)
        {
            if (options.Years != null && options.Years.Count > 0)
                return options.Years;

            List<int> years = new List<int>();
            if (options.MonthYearStarts > options.StartDate.Month)
                years.Add(options.StartDate.Year - 1);
            for (int i = options.StartDate.Year; i < options.EndDate.Year; i++)
                years.Add(i);
            if (options.EndDate.Month >= options.MonthYearStarts)
                years.Add(options.EndDate.Year);
            return years;
        }

        protected string GetMissingRowsErrors(DataTable result, bool isDemo)
        {
            string warnings = "";
            List<int> years = GetSelectedYears(opts);
            Dictionary<string, KeyValuePair<int, AdminLevel>> missingDictionary = new Dictionary<string, KeyValuePair<int, AdminLevel>>();
            foreach (var unit in opts.SelectedAdminLevels)
                foreach (int year in years)
                    missingDictionary.Add(unit.Id.ToString() + year.ToString(), new KeyValuePair<int, AdminLevel>(year, unit));

            foreach (DataRow dr in result.Rows)
            {
                int id = Convert.ToInt32(dr["ID"]);
                if (missingDictionary.ContainsKey(id.ToString() + dr["YearNumber"]))
                    missingDictionary.Remove(id.ToString() + dr["YearNumber"]);
            }

            foreach (var missingUnit in missingDictionary.Values)
            {
                DateTime start = new DateTime(missingUnit.Key, opts.MonthYearStarts, 1);
                DateTime end = start.AddYears(1).AddDays(-1);
                string unitName = string.IsNullOrEmpty(missingUnit.Value.Name) ? Translations.Country : missingUnit.Value.Name;
                if(isDemo)
                    warnings += string.Format(Translations.ReportsNoDemographyInDateRange, unitName, start.ToShortDateString(), end.ToShortDateString()) + Environment.NewLine;
                else
                    warnings += string.Format(Translations.ReportsNoDdInDateRange, unitName, start.ToShortDateString(), end.ToShortDateString(), Translations.ReportChoosenDd) + Environment.NewLine;
            }
            return warnings;
        }

        protected void AddRequiredIndicator(int typeId, int indicatorId)
        {
            if (opts.SelectedIndicators.FirstOrDefault(i => i.TypeId == typeId) != null && opts.SelectedIndicators.FirstOrDefault(x => x.ID == indicatorId) == null)
                FindAndAddIndicator(indicatorId, typeId);
        }

        protected void FindAndAddIndicator(int indicatorId, int typeId)
        {
            var reportType = opts.AvailableIndicators[0].Children.FirstOrDefault(t => t.ID == typeId);
            if (reportType != null)
            {
                var ind = reportType.Children.FirstOrDefault(v => v.ID == indicatorId);
                if (ind != null)
                    opts.SelectedIndicators.Add(ind);
            }
        }
        #endregion
    }

    [Serializable]
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
                        [DateReported], 
                        Interventions.PcIntvRoundNumber, 
                        InterventionTypes.InterventionTypeName as TName, 
                        InterventionTypes.ID as Tid,      
                        InterventionIndicators.ID as IndicatorId, 
                        InterventionIndicators.DisplayName as IndicatorName, 
                        InterventionIndicators.IsEditable, 
                        InterventionIndicators.DataTypeId, 
                        InterventionIndicators.AggTypeId, 
                        InterventionIndicatorValues.DynamicValue, 
                        InterventionIndicatorValues.MemoValue
                        FROM ((((Interventions INNER JOIN InterventionTypes on Interventions.InterventionTypeId = InterventionTypes.ID)
                            INNER JOIN InterventionIndicatorValues on Interventions.Id = InterventionIndicatorValues.InterventionId)
                            INNER JOIN AdminLevels on Interventions.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN InterventionIndicators on InterventionIndicators.ID = InterventionIndicatorValues.IndicatorId)
                        WHERE Interventions.IsDeleted = 0 AND  
                              InterventionIndicators.Id in "
            + " (" + String.Join(", ", opts.SelectedIndicators.Select(s => s.ID.ToString()).ToArray())
            + ") AND InterventionTypes.ID in (" + String.Join(", ", opts.SelectedIndicators.Select(i => i.TypeId.ToString()).Distinct().ToArray()) + ") "
            + ReportRepository.CreateYearFilter(opts, "DateReported") + ReportRepository.CreateAdminFilter(opts)
            + " ORDER BY IsEditable DESC, InterventionIndicators.SortOrder";
        }

        protected override string GetIndKey(OleDbDataReader reader, bool isNotAgg, ReportOptions options)
        {
            int year = options.IsGroupByRange ? 0 : Util.GetYearReported(options.MonthYearStarts, reader.GetValueOrDefault<DateTime>("DateReported"));
            string key = reader.GetValueOrDefault<int>("IndicatorId").ToString() + "_" + year + "_" +
                    reader.GetValueOrDefault<string>("TName") + GetValueOrBlank(reader.GetValueOrDefault<Nullable<int>>("PcIntvRoundNumber"), "_");
            if (isNotAgg)
                return reader.GetValueOrDefault<int>("ID").ToString() + "_" + key;
            return key;
        }

        protected override string GetColName(OleDbDataReader reader)
        {
            string name = reader.GetValueOrDefault<string>("IndicatorName");
            if (!reader.GetValueOrDefault<bool>("IsEditable"))
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

    [Serializable]
    public class SurveyReportGenerator : BaseReportGenerator
    {
        SurveyRepository surveyRepo = new SurveyRepository();
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
                        [DateReported], 
                        SurveyTypes.SurveyTypeName as TName,            
                        SurveyTypes.ID as Tid,      
                        SurveyIndicators.ID as IndicatorId, 
                        SurveyIndicators.DisplayName as IndicatorName, 
                        SurveyIndicators.IsEditable, 
                        SurveyIndicators.DataTypeId, 
                        SurveyIndicators.AggTypeId,    
                        SurveyIndicatorValues.DynamicValue, 
                        SurveyIndicatorValues.MemoValue,
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
                       + ReportRepository.CreateYearFilter(opts, "DateReported") + ReportRepository.CreateAdminFilter(opts)
                        + " ORDER BY IsEditable DESC, SurveyIndicators.SortOrder";
            else
                return @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        Surveys.ID, 
                        [DateReported], 
                        SurveyTypes.SurveyTypeName as TName,           
                        SurveyTypes.ID as Tid,       
                        0 as IndicatorId, 
                        '' as IndicatorName, 
                        0 as IsEditable, 
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
                    + ReportRepository.CreateYearFilter(opts, "DateReported") + ReportRepository.CreateAdminFilter(opts)
                     + " ORDER BY IsEditable DESC";

        }

        protected override void AddStaticAggInd(CreateAggParams param)
        {
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSpotCheckName) != null)
                AddStaticIndicatorAndColumn<string>("IndSpotCheckName", Translations.IndSpotCheckName, param);
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSpotCheckLat) != null)
                AddStaticIndicatorAndColumn<Nullable<double>>("IndSpotCheckLat", Translations.IndSpotCheckLat, param);
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSpotCheckLng) != null)
                AddStaticIndicatorAndColumn<Nullable<double>>("IndSpotCheckLng", Translations.IndSpotCheckLng, param);
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSentinelSiteName) != null)
                AddStaticIndicatorAndColumn<string>("IndSentinelSiteName", Translations.IndSentinelSiteName, param);
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSentinelSiteLat) != null)
                AddStaticIndicatorAndColumn<Nullable<double>>("IndSentinelSiteLat", Translations.IndSentinelSiteLat, param);
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.IndSentinelSiteLng) != null)
                AddStaticIndicatorAndColumn<Nullable<double>>("IndSentinelSiteLng", Translations.IndSentinelSiteLng, param);
            if (opts.SelectedIndicators.FirstOrDefault(i => i.Name == Translations.SiteType) != null)
                AddStaticIndicatorAndColumn<string>("SiteType", Translations.SiteType, param);
        }

        private void AddStaticIndicatorAndColumn<T>(string columnName, string transName, CreateAggParams param)
        {
            string key = columnName + "_" + Util.GetYearReported(param.Options.MonthYearStarts, param.Reader.GetValueOrDefault<DateTime>("DateReported")) + "_" + param.Reader.GetValueOrDefault<string>("TName");
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
                    Year = Util.GetYearReported(param.Options.MonthYearStarts, param.Reader.GetValueOrDefault<DateTime>("DateReported")),
                    TypeName = param.Reader.GetValueOrDefault<string>("TName"),
                    IsCalcRelated = false,
                    FormId = param.Reader.GetValueOrDefault<int>("ID"),
                    EntityTypeId = (int)IndicatorEntityType.Survey
                };
                param.AdminLevel.Indicators.Add(key, ind);

                // Add Column
                if (!param.Options.Columns.ContainsKey(key))
                    param.Options.Columns.Add(key, ind);
            }
        }

        protected override void AddTypeSpecificColumns(DataTable result, AdminLevelIndicators level, ReportOptions options, DataRow dr, AggregateIndicator indValue)
        {
            var s = surveyRepo.GetById(indValue.FormId);
            if (s.AdminLevels.Count > 1)
            {
                if (!result.Columns.Contains(Translations.AdminUnitsSurveyed))
                    result.Columns.Add(new DataColumn(Translations.AdminUnitsSurveyed));
                dr[Translations.AdminUnitsSurveyed] = string.Join(", ", s.AdminLevels.Select(a => a.Name).ToArray());
            }
        }
    }

    [Serializable]
    public class DistributionReportGenerator : BaseReportGenerator
    {
        protected override bool IsDemoOrDistro
        {
            get
            {
                return true;
            }
        }
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
                        [DateReported], 
                        Diseases.DisplayName as TName,       
                        Diseases.ID as Tid,       
                        DiseaseDistributionIndicators.ID as IndicatorId, 
                        DiseaseDistributionIndicators.DisplayName as IndicatorName, 
                        DiseaseDistributionIndicators.IsEditable, 
                        DiseaseDistributionIndicators.DataTypeId, 
                        DiseaseDistributionIndicators.AggTypeId, 
                        DiseaseDistributionIndicatorValues.DynamicValue, 
                        DiseaseDistributionIndicatorValues.MemoValue
                        FROM ((((DiseaseDistributions INNER JOIN Diseases on DiseaseDistributions.DiseaseId = Diseases.ID)
                            INNER JOIN DiseaseDistributionIndicatorValues on DiseaseDistributions.Id = DiseaseDistributionIndicatorValues.DiseaseDistributionId)
                            INNER JOIN AdminLevels on DiseaseDistributions.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN DiseaseDistributionIndicators on DiseaseDistributionIndicators.ID = DiseaseDistributionIndicatorValues.IndicatorId)
                        WHERE DiseaseDistributions.IsDeleted = 0 AND  
                              DiseaseDistributionIndicators.Id in "
            + " (" + String.Join(", ", opts.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ") "
            + ReportRepository.CreateYearFilter(opts, "DateReported") + ReportRepository.CreateAdminFilter(opts)
             + " ORDER BY IsEditable DESC, DiseaseDistributionIndicators.SortOrder";
        }

        public AdminLevelIndicators GetRecentDiseaseDistribution(ReportOptions options)
        {
            opts = options;
            Initialize();
            List<AdminLevelIndicators> list = new List<AdminLevelIndicators>();
            Dictionary<int, AdminLevelIndicators> dic = new Dictionary<int, AdminLevelIndicators>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);

            // Get all indicators
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                list = ExportRepository.GetAdminLevels(command, connection, false);
                dic = list.ToDictionary(n => n.Id, n => n);
                repo.AddIndicatorsToAggregate(CmdText(), options, dic, command, connection, GetIndKey, GetColName, GetColTypeName, AddStaticAggInd, false, true, EntityTypeId, dropdownOptions);
            }
            
            IndicatorListToTree(list, dic);
            AdminLevelIndicators level = list.Where(a => options.SelectedAdminLevels.Select(s => s.Id).Contains(a.Id)).FirstOrDefault();
            
            if(level == null)
                return new AdminLevelIndicators();

            foreach (KeyValuePair<string, AggregateIndicator> columnDef in options.Columns) // each column
            {
                AggregateIndicator aggInd = null;
                if (level.Indicators.ContainsKey(columnDef.Key))
                    aggInd = level.Indicators[columnDef.Key];
                else
                    aggInd = IndicatorAggregator.AggregateChildren(level.Children, columnDef.Key, null, dropdownOptions); // level doesn't have it, aggregate children

                if (aggInd == null)
                    continue;

                if (!level.Indicators.ContainsKey(columnDef.Key))
                    level.Indicators.Add(columnDef.Key, aggInd);
                else
                    level.Indicators[columnDef.Key] = aggInd;
            }
            var vals = Util.DeepClone(level.Indicators);
            level.Indicators.Clear();
            foreach (var val in vals.Values)
                if(val.Key != null && !level.Indicators.ContainsKey(val.Key))
                   level.Indicators.Add(val.Key, val);
            return level;
        }
    }

    [Serializable]
    public class ProcessReportGenerator : BaseReportGenerator
    {
        protected override int EntityTypeId { get { return (int)IndicatorEntityType.Process; } }
        protected override void Init()
        {
            calc = new CalcProcess();
        }

        protected override string CmdText()
        {
            return @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        Processes.ID, 
                        [DateReported], 
                        Processes.SCMDrug, 
                        Processes.PCTrainTrainingCategory, 
                        ProcessTypes.TypeName as TName,           
                        ProcessTypes.ID as Tid,        
                        ProcessIndicators.ID as IndicatorId, 
                        ProcessIndicators.DisplayName as IndicatorName, 
                        ProcessIndicators.IsEditable, 
                        ProcessIndicators.DataTypeId, 
                        ProcessIndicators.AggTypeId, 
                        ProcessIndicatorValues.DynamicValue, 
                        ProcessIndicatorValues.MemoValue
                        FROM ((((Processes INNER JOIN ProcessTypes on Processes.ProcessTypeId = ProcessTypes.ID)
                            INNER JOIN ProcessIndicatorValues on Processes.Id = ProcessIndicatorValues.ProcessId)
                            INNER JOIN AdminLevels on Processes.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN ProcessIndicators on ProcessIndicators.ID = ProcessIndicatorValues.IndicatorId)
                        WHERE Processes.IsDeleted = 0 AND  
                              ProcessIndicators.Id in "
            + " (" + String.Join(", ", opts.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ") "
            + ReportRepository.CreateYearFilter(opts, "DateReported") + ReportRepository.CreateAdminFilter(opts)
             + " ORDER BY IsEditable DESC, ProcessIndicators.SortOrder";
        }

        protected override string GetIndKey(OleDbDataReader reader, bool isNotAgg, ReportOptions options)
        {
            int year = options.IsGroupByRange ? 0 : Util.GetYearReported(options.MonthYearStarts, reader.GetValueOrDefault<DateTime>("DateReported"));

            string key = reader.GetValueOrDefault<int>("IndicatorId").ToString() + "_" + year + "_" +
                reader.GetValueOrDefault<string>("TName") + GetValueOrBlank(reader.GetValueOrDefault<string>("SCMDrug"), "_") +
                GetValueOrBlank(reader.GetValueOrDefault<string>("PCTrainTrainingCategory"), "_");
            if (isNotAgg)
                return reader.GetValueOrDefault<int>("ID").ToString() + "_" + key;
            return key;
        }

        protected override string GetColName(OleDbDataReader reader)
        {
            string name = reader.GetValueOrDefault<string>("IndicatorName");
            if (!reader.GetValueOrDefault<bool>("IsEditable"))
                name = TranslationLookup.GetValue(name);
            string drug = reader.GetValueOrDefault<string>("SCMDrug");
            if (!string.IsNullOrEmpty(drug))
                drug = TranslationLookup.GetValue(drug, drug);
            string cats = reader.GetValueOrDefault<string>("PCTrainTrainingCategory");
            List<string> catList = new List<string>();
            if (!string.IsNullOrEmpty(cats))
                foreach(var c in cats.Split('|'))
                    if (!string.IsNullOrEmpty(c))
                        catList.Add(TranslationLookup.GetValue(c, c));

            return name + " - " +
                GetTypeName(reader) +
                GetValueOrBlank(drug, " - ") +
                GetValueOrBlank(string.Join(", ", catList.ToArray()), " - ");
        }

        protected override string GetColTypeName(OleDbDataReader reader)
        {
            string drug = reader.GetValueOrDefault<string>("SCMDrug");
            if (!string.IsNullOrEmpty(drug))
                drug = TranslationLookup.GetValue(drug, drug);
            string cats = reader.GetValueOrDefault<string>("PCTrainTrainingCategory");
            List<string> catList = new List<string>();
            if (!string.IsNullOrEmpty(cats))
                foreach (var c in cats.Split('|'))
                    if (!string.IsNullOrEmpty(c))
                        catList.Add(TranslationLookup.GetValue(c, c));
            return " - " + GetTypeName(reader) +
                GetValueOrBlank(drug, " - ") +
                GetValueOrBlank(string.Join(", ", catList.ToArray()), " - ");
        }
    }

    [Serializable]
    public class DemoReportGenerator : BaseReportGenerator
    {
        protected override bool IsDemoOrDistro
        {
            get
            {
                return true;
            }
        }

        protected override ReportResult DoRun(ReportOptions options)
        {
            opts = options;
            ReportResult result = new ReportResult();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID"));
            dataTable.Columns.Add(new DataColumn(Translations.Location));
            dataTable.Columns.Add(new DataColumn(Translations.Type));
            dataTable.Columns.Add(new DataColumn(Translations.Year));
            dataTable.Columns.Add(new DataColumn("YearNumber"));
            dataTable.Columns.Add(new DataColumn("DaughterId"));
            dataTable.Columns.Add(new DataColumn("MotherId"));
            foreach (var ind in options.SelectedIndicators)
                dataTable.Columns.Add(new DataColumn(ind.Name));
            
            result.DataTableResults = repo.CreateDemoReport(options, dataTable);

            foreach (DataRow dr in result.DataTableResults.Rows)
            {
                // add location breadcrumb
                int id = Convert.ToInt32(dr["ID"]);
                List<AdminLevel> parents = demo.GetAdminLevelParentNames(id);
                for (int i = 0; i < parents.Count; i++)
                {
                    if (!result.DataTableResults.Columns.Contains(parents[i].LevelName))
                    {
                        DataColumn dc = new DataColumn(parents[i].LevelName);
                        result.DataTableResults.Columns.Add(dc);
                        dc.SetOrdinal(i);
                    }
                    dr[parents[i].LevelName] = parents[i].Name;
                }
                // add redistricting notes
                if (options.ShowRedistrictEvents)
                {
                    if(!result.DataTableResults.Columns.Contains(Translations.RedistrictingNotes))
                        result.DataTableResults.Columns.Add(new DataColumn(Translations.RedistrictingNotes));
                    int daughterId = Convert.ToInt32(dr["DaughterId"]);
                    int motherId = Convert.ToInt32(dr["MotherId"]);
                    if (daughterId > 0)
                        dr[Translations.RedistrictingNotes] = demo.GetRedistrictingDaughterNote(daughterId);
                    else if (motherId > 0)
                        dr[Translations.RedistrictingNotes] = demo.GetRedistrictingMotherNote(motherId);
                }
            }

            result.MetaDataWarning = GetMissingRowsErrors(result.DataTableResults, true);
            result.DataTableResults.Columns.Remove("ID");
            result.DataTableResults.Columns.Remove("YearNumber");
            result.DataTableResults.Columns.Remove("DaughterId");
            result.DataTableResults.Columns.Remove("MotherId");
            result.ChartData = result.DataTableResults.Copy();
            result.DataTableResults.Columns.Remove(Translations.Location);
            return result;
        }
    }
}
