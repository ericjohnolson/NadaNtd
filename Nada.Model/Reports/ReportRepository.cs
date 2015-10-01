using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.DA;
using Nada.Globalization;
using Nada.Model.Demography;
using Nada.Model.Diseases;
using Nada.Model.Process;
using Nada.Model.Reports;

namespace Nada.Model.Repositories
{

    public class CreateAggParams
    {
        public AdminLevelIndicators AdminLevel { get; set; }
        public OleDbDataReader Reader { get; set; }
        public ReportOptions Options { get; set; }
        public DataRow Row { get; set; }
        public DataTable Table { get; set; }
    }

    public class ReportRow
    {
        public ReportRow()
        {
            CalcRelated = new List<AggregateIndicator>();
        }
        public DataRow Row { get; set; }
        public List<AggregateIndicator> CalcRelated { get; set; }
        public int AdminLevelId { get; set; }
        public string AdminLevelName { get; set; }
        public int Year { get; set; }
    }

    public class ReportRepository : RepositoryBase
    {
        private List<Partner> partners = new List<Partner>();
        private List<IndicatorDropdownValue> ezs = new List<IndicatorDropdownValue>();
        private List<IndicatorDropdownValue> eus = new List<IndicatorDropdownValue>();
        private List<IndicatorDropdownValue> subdistricts = new List<IndicatorDropdownValue>();
        private List<IndicatorDropdownValue> evalsites = new List<IndicatorDropdownValue>();

        #region ReportGenerators
        public void LoadRelatedLists()
        {
            SettingsRepository settings = new SettingsRepository();
            ezs = settings.GetEcologicalZones();
            eus = settings.GetEvaluationUnits();
            subdistricts = settings.GetEvalSubDistricts();
            evalsites = settings.GetEvalSites();
            IntvRepository repo = new IntvRepository();
            partners = repo.GetPartners();
        }

        public void AddIndicatorsToAggregate(string cmdText, ReportOptions options, Dictionary<int, AdminLevelIndicators> dic, OleDbCommand command,
            OleDbConnection connection, Func<OleDbDataReader, bool, ReportOptions, string> getKey, Func<OleDbDataReader, string> getName, Func<OleDbDataReader, string> getColTypeName,
            Action<CreateAggParams> addStaticIndicators, bool isCalcRelated, bool isDemoOrDistro, int entityTypeId, List<IndicatorDropdownValue> dropdownOptions)
        {
            command = new OleDbCommand(cmdText, connection);

            FillDictionary(command, connection, dic, options, getKey, getName, getColTypeName, addStaticIndicators, isCalcRelated, isDemoOrDistro, entityTypeId, dropdownOptions);
        }

        private void FillDictionary(OleDbCommand command, OleDbConnection connection, Dictionary<int, AdminLevelIndicators> dic, ReportOptions options,
            Func<OleDbDataReader, bool, ReportOptions, string> getKey, Func<OleDbDataReader, string> getName, Func<OleDbDataReader, string> getColTypeName,
            Action<CreateAggParams> addStaticIndicators, bool isCalcRelated, bool isDemoOrDistro, int entityTypeId, List<IndicatorDropdownValue> dropdownOptions)
        {
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string indicatorName = getName(reader);
                    string indicatorKey = getKey(reader, options.IsNoAggregation, options) + isCalcRelated;
                    int adminLevelId = reader.GetValueOrDefault<int>("AID");
                    int dataType = reader.GetValueOrDefault<int>("DataTypeId");
                    if (!dic.ContainsKey(adminLevelId))
                        continue;
                    string val = reader.GetValueOrDefault<string>("DynamicValue");
                    if (dataType == (int)IndicatorDataType.LargeText && !string.IsNullOrEmpty(reader.GetValueOrDefault<string>("MemoValue")))
                        val = reader.GetValueOrDefault<string>("MemoValue");

                    var newIndicator = new AggregateIndicator
                    {
                        IndicatorId = reader.GetValueOrDefault<int>("IndicatorId"),
                        Name = indicatorName,
                        Key = reader.GetValueOrDefault<string>("IndicatorName"),
                        DataType = dataType,
                        Value = val,
                        AggType = reader.GetValueOrDefault<int>("AggTypeId"),
                        Year = Util.GetYearReported(options.MonthYearStarts, reader.GetValueOrDefault<DateTime>("DateReported")),
                        ReportedDate = reader.GetValueOrDefault<DateTime>("DateReported"),
                        IsCalcRelated = isCalcRelated,
                        ColumnTypeName = getColTypeName(reader),
                        TypeId = reader.GetValueOrDefault<int>("Tid"),
                        TypeName = reader.GetValueOrDefault<string>("TName"),
                        FormId = reader.GetValueOrDefault<int>("ID"),
                        EntityTypeId = entityTypeId
                    };
                    // if the adminlevel already contains the indicator for that year, aggregate
                    if (dic[adminLevelId].Indicators.ContainsKey(indicatorKey) && !isDemoOrDistro)
                    {
                        dic[adminLevelId].Indicators[indicatorKey] = IndicatorAggregator.Aggregate(newIndicator, dic[adminLevelId].Indicators[indicatorKey], dropdownOptions);
                    }
                    else if (dic[adminLevelId].Indicators.ContainsKey(indicatorKey) && isDemoOrDistro)
                    {
                        if (newIndicator.ReportedDate > dic[adminLevelId].Indicators[indicatorKey].ReportedDate)
                            dic[adminLevelId].Indicators[indicatorKey] = newIndicator;
                    }
                    else
                    {
                        dic[adminLevelId].Indicators.Add(indicatorKey, newIndicator);

                        // Add Column
                        if (!options.Columns.ContainsKey(indicatorKey))
                            options.Columns.Add(indicatorKey, newIndicator);

                        if (!isCalcRelated)
                            addStaticIndicators(new CreateAggParams
                            {
                                AdminLevel = dic[adminLevelId],
                                Reader = reader,
                                Options = options,
                            });
                    }

                }
                reader.Close();
            }
        }

        /// <summary>
        /// Note aggregation is easy for demo, because it is pre aggregated
        /// </summary>
        /// <param name="options"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable CreateDemoReport(ReportOptions options, DataTable dt)
        {
            Dictionary<string, ReportIndicator> keys = new Dictionary<string, ReportIndicator>();
            foreach (var i in options.SelectedIndicators)
                keys.Add(i.Key, i);
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    string adminFilter = "";
                    if (options.IsByLevelAggregation || (options.IsNoAggregation && !options.IsAllLocations) || options.IsCountryAggregation)
                        adminFilter = " and AdminLevels.Id in (" + String.Join(", ", options.SelectedAdminLevels.Select(a => a.Id.ToString()).ToArray()) + ") ";
                    else if (options.IsAllLocations && options.ShowOnlyRedistrictedUnits)
                        adminFilter = " AND AdminLevels.RedistrictIdForMother > 0 ";
                    OleDbCommand command = new OleDbCommand(@"Select 
                                a.ID, 
                                AdminLevels.Id as aID, 
                                [DateDemographyData] as DateReported, 
                                AdminLevels.DisplayName, 
                                t.DisplayName as TName
                                ,YearCensus 
                                ,YearProjections 
                                ,GrowthRate 
                                ,PercentRural 
                                ,TotalPopulation 
                                ,Pop0Month 
                                ,PopPsac 
                                ,PopSac 
                                ,Pop5yo 
                                ,PopAdult 
                                ,PopFemale 
                                ,PopMale 
                                ,RedistrictIdForDaughter
                                ,RedistrictIdForMother
                                ,Notes
                            FROM ((AdminLevelDemography a INNER JOIN AdminLevels on a.AdminLevelId = AdminLevels.ID)
                                INNER JOIN AdminLevelTypes t on t.Id = AdminLevels.AdminLevelTypeId)
                            WHERE a.IsDeleted = 0 " + CreateYearFilter(options, "DateDemographyData") +
                            adminFilter +
                            "ORDER BY  t.AdminLevel, AdminLevels.SortOrder "
                        , connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = reader.GetValueOrDefault<int>("aID");
                            dr[TranslationLookup.GetValue("Location")] = reader.GetValueOrDefault<string>("DisplayName");
                            dr[TranslationLookup.GetValue("Type")] = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("TName"), reader.GetValueOrDefault<string>("TName"));
                            int year = Util.GetYearReported(options.MonthYearStarts, reader.GetValueOrDefault<DateTime>("DateReported")); 
                            DateTime startMonth = new DateTime(year, options.MonthYearStarts, 1);
                            dr["YearNumber"] = year;
                            dr["DaughterId"] = reader.GetValueOrDefault<int>("RedistrictIdForDaughter");
                            dr["MotherId"] = reader.GetValueOrDefault<int>("RedistrictIdForMother");
                            dr[TranslationLookup.GetValue("Year")] = startMonth.ToString("MMM yyyy") + "-" + startMonth.AddYears(1).AddMonths(-1).ToString("MMM yyyy");
                            if (keys.ContainsKey("YearCensus")) dr[TranslationLookup.GetValue("YearCensus")] = reader.GetValueOrDefault<Nullable<int>>("YearCensus");
                            if (keys.ContainsKey("YearProjections")) dr[TranslationLookup.GetValue("YearProjections")] = reader.GetValueOrDefault<Nullable<int>>("YearProjections");
                            if (keys.ContainsKey("GrowthRate")) dr[TranslationLookup.GetValue("GrowthRate")] = reader.GetValueOrDefault<Nullable<double>>("GrowthRate");
                            if (keys.ContainsKey("PercentRural")) dr[TranslationLookup.GetValue("PercentRural")] = reader.GetValueOrDefault<Nullable<double>>("PercentRural");
                            if (keys.ContainsKey("TotalPopulation")) dr[TranslationLookup.GetValue("TotalPopulation")] = reader.GetValueOrDefault<Nullable<int>>("TotalPopulation");
                            if (keys.ContainsKey("Pop0Month")) dr[TranslationLookup.GetValue("Pop0Month")] = reader.GetValueOrDefault<Nullable<int>>("Pop0Month");
                            if (keys.ContainsKey("PopPsac")) dr[TranslationLookup.GetValue("PopPsac")] = reader.GetValueOrDefault<Nullable<int>>("PopPsac");
                            if (keys.ContainsKey("PopSac")) dr[TranslationLookup.GetValue("PopSac")] = reader.GetValueOrDefault<Nullable<int>>("PopSac");
                            if (keys.ContainsKey("Pop5yo")) dr[TranslationLookup.GetValue("Pop5yo")] = reader.GetValueOrDefault<Nullable<int>>("Pop5yo");
                            if (keys.ContainsKey("PopAdult")) dr[TranslationLookup.GetValue("PopAdult")] = reader.GetValueOrDefault<Nullable<int>>("PopAdult");
                            if (keys.ContainsKey("PopFemale")) dr[TranslationLookup.GetValue("PopFemale")] = reader.GetValueOrDefault<Nullable<int>>("PopFemale");
                            if (keys.ContainsKey("PopMale")) dr[TranslationLookup.GetValue("PopMale")] = reader.GetValueOrDefault<Nullable<int>>("PopMale");
                            if (keys.ContainsKey("Notes")) dr[TranslationLookup.GetValue("Notes")] = reader.GetValueOrDefault<string>("Notes");
                            dt.Rows.Add(dr);
                        }
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return dt;
        }

        public static string CreateYearFilter(ReportOptions options, string dateName)
        {
            if(options.StartDate == DateTime.MinValue && options.EndDate == DateTime.MinValue)
                return "";

            if (options.StartDate == DateTime.MinValue)
            {
                return string.Format(" AND {0} <= cdate('{1}') ", dateName,
                    options.EndDate.ToShortDateString());
            }
            else if (options.EndDate == DateTime.MinValue)
            {
                return string.Format(" AND {0} >= cdate('{1}') ", dateName,
                    options.StartDate.ToShortDateString());
            }
            else
            {
                return string.Format(" AND {0} >= cdate('{1}') AND {0} <= cdate('{2}') ", dateName,
                    options.StartDate.ToShortDateString(),
                    options.EndDate.ToShortDateString());
            }
        }

        public static string CreateAdminFilter(ReportOptions options)
        {
            string filter = " ";
            if (options.IsNoAggregation && !options.IsAllLocations)
                filter += " and AdminLevels.Id in (" + String.Join(", ", options.SelectedAdminLevels.Select(a => a.Id.ToString()).ToArray()) + ") ";
            return filter;
        }
        #endregion

        #region Indicators
        public List<ReportIndicator> GetIntvIndicators()
        {
            List<ReportIndicator> indicators = new List<ReportIndicator>();
            IntvRepository repo = new IntvRepository();
            var types = repo.GetAllTypes();
            var pc = new ReportIndicator { Name = TranslationLookup.GetValue("PcNtds"), IsCategory = true };
            var cm = new ReportIndicator { Name = TranslationLookup.GetValue("OtherNtds"), IsCategory = true };
            indicators.Add(pc);
            indicators.Add(cm);
            foreach (var t in types.Where(i => i.DiseaseType == TranslationLookup.GetValue("PC")).OrderBy(t => t.IntvTypeName))
            {
                var cat = new ReportIndicator { Name = t.IntvTypeName, IsCategory = true };
                var instance = repo.CreateIntv(t.Id);
                foreach (var i in instance.IntvType.Indicators)
                    cat.Children.Add(CreateReportIndicator(t.Id, i, t.IntvTypeName, t.DisplayNameKey));
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                pc.Children.Add(cat);
            }
            foreach (var t in types.Where(i => i.DiseaseType == TranslationLookup.GetValue("CM")).OrderBy(t => t.IntvTypeName))
            {
                var cat = new ReportIndicator { Name = t.IntvTypeName, IsCategory = true };
                var instance = repo.CreateIntv(t.Id);
                foreach (var i in instance.IntvType.Indicators)
                    cat.Children.Add(CreateReportIndicator(t.Id, i, t.IntvTypeName, t.DisplayNameKey));
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                cm.Children.Add(cat);
            }

            return indicators;
        }

        public List<ReportIndicator> GetSurveyIndicators()
        {
            return GetSurveyIndicators(true);
        }

        public List<ReportIndicator> GetSurveyIndicators(bool addStaticFields)
        {
            List<ReportIndicator> indicators = new List<ReportIndicator>();
            SurveyRepository repo = new SurveyRepository();
            var types = repo.GetSurveyTypes();
            var pc = new ReportIndicator { Name = TranslationLookup.GetValue("PcNtds"), IsCategory = true };
            var cm = new ReportIndicator { Name = TranslationLookup.GetValue("OtherNtds"), IsCategory = true };
            indicators.Add(pc);
            indicators.Add(cm);
            foreach (var t in types.Where(i => i.DiseaseType == TranslationLookup.GetValue("PC")).OrderBy(t => t.SurveyTypeName))
            {
                var cat = new ReportIndicator { Name = t.SurveyTypeName, IsCategory = true, ID = t.Id };
                var instance = repo.CreateSurvey(t.Id);
                if (addStaticFields && (t.Id == (int)StaticSurveyType.LfSentinel || t.Id == (int)StaticSurveyType.SchistoSentinel || t.Id == (int)StaticSurveyType.SthSentinel))
                {
                    cat.Children.Add(new ReportIndicator { Name = TranslationLookup.GetValue("IndSentinelSiteName") });
                    cat.Children.Add(new ReportIndicator { Name = TranslationLookup.GetValue("IndSentinelSiteLat") });
                    cat.Children.Add(new ReportIndicator { Name = TranslationLookup.GetValue("IndSentinelSiteLng") });
                    cat.Children.Add(new ReportIndicator { Name = TranslationLookup.GetValue("IndSpotCheckName") });
                    cat.Children.Add(new ReportIndicator { Name = TranslationLookup.GetValue("IndSpotCheckLat") });
                    cat.Children.Add(new ReportIndicator { Name = TranslationLookup.GetValue("IndSpotCheckLng") });
                    cat.Children.Add(new ReportIndicator { Name = TranslationLookup.GetValue("SiteType") });
                }

                foreach (var i in instance.TypeOfSurvey.Indicators)
                    if (i.Value.DataTypeId != (int)IndicatorDataType.SentinelSite)
                        cat.Children.Add(CreateReportIndicator(t.Id, i, t.SurveyTypeName, t.DisplayNameKey));
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                pc.Children.Add(cat);
            }
            foreach (var t in types.Where(i => i.DiseaseType == TranslationLookup.GetValue("CM")).OrderBy(t => t.SurveyTypeName))
            {
                var cat = new ReportIndicator { Name = t.SurveyTypeName, IsCategory = true };
                var instance = repo.CreateSurvey(t.Id);
                foreach (var i in instance.TypeOfSurvey.Indicators)
                    cat.Children.Add(CreateReportIndicator(t.Id, i, t.SurveyTypeName, t.DisplayNameKey));
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                cm.Children.Add(cat);
            }
            return indicators;
        }

        public List<ReportIndicator> GetProcessIndicators()
        {
            List<ReportIndicator> indicators = new List<ReportIndicator>();
            ProcessRepository repo = new ProcessRepository();
            var types = repo.GetProcessTypes();
            var pc = new ReportIndicator { Name = TranslationLookup.GetValue("PcNtds"), IsCategory = true };
            var cm = new ReportIndicator { Name = TranslationLookup.GetValue("OtherNtds"), IsCategory = true };
            types.RemoveAll(t => t.TypeName == TranslationLookup.GetValue("SAEs"));
            indicators.Add(pc);
            indicators.Add(cm);
            foreach (var t in types.Where(i => i.DiseaseType == TranslationLookup.GetValue("PC")).OrderBy(t => t.TypeName))
            {
                var cat = new ReportIndicator { Name = t.TypeName, IsCategory = true };
                var instance = repo.Create(t.Id);
                foreach (var i in instance.ProcessType.Indicators)
                    cat.Children.Add(CreateReportIndicator(t.Id, i, t.TypeName, t.DisplayNameKey));
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                pc.Children.Add(cat);
            }
            foreach (var t in types.Where(i => i.DiseaseType == TranslationLookup.GetValue("CM")).OrderBy(t => t.TypeName))
            {
                var cat = new ReportIndicator { Name = t.TypeName, IsCategory = true };
                var instance = repo.Create(t.Id);
                foreach (var i in instance.ProcessType.Indicators)
                    cat.Children.Add(CreateReportIndicator(t.Id, i, t.TypeName, t.DisplayNameKey));
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                cm.Children.Add(cat);
            }

            // SAEs
            ProcessBase saes = repo.Create(9);
            var saeCat = new ReportIndicator { Name = saes.ProcessType.TypeName, IsCategory = true };
            foreach (var i in saes.ProcessType.Indicators)
                saeCat.Children.Add(CreateReportIndicator(saes.ProcessType.Id, i, saes.ProcessType.TypeName, saes.ProcessType.DisplayNameKey));
            saeCat.Children = saeCat.Children.OrderBy(c => c.Name).ToList();
            indicators.Add(saeCat);

            return indicators;
        }

        public List<ReportIndicator> GetDiseaseDistroIndicators()
        {
            List<ReportIndicator> indicators = new List<ReportIndicator>();
            DiseaseRepository repo = new DiseaseRepository();
            var types = repo.GetSelectedDiseases();
            var pc = new ReportIndicator { Name = TranslationLookup.GetValue("PcNtds"), IsCategory = true };
            var cm = new ReportIndicator { Name = TranslationLookup.GetValue("OtherNtds"), IsCategory = true };
            indicators.Add(pc);
            indicators.Add(cm);
            foreach (var t in types.Where(i => i.DiseaseType == TranslationLookup.GetValue("PC")).OrderBy(t => t.DisplayName))
            {
                var cat = new ReportIndicator { Name = t.DisplayName, IsCategory = true };
                DiseaseDistroPc dd = repo.Create((DiseaseType)t.Id);
                foreach (var i in dd.Indicators)
                    cat.Children.Add(CreateReportIndicator(t.Id, i, t.DisplayName, t.DisplayNameKey));
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                pc.Children.Add(cat);
            }
            foreach (var t in types.Where(i => i.DiseaseType == TranslationLookup.GetValue("CM")).OrderBy(t => t.DisplayName))
            {
                var cat = new ReportIndicator { Name = t.DisplayName, IsCategory = true };
                DiseaseDistroCm dd = repo.CreateCm((DiseaseType)t.Id);
                foreach (var i in dd.Indicators)
                    cat.Children.Add(CreateReportIndicator(t.Id, i, t.DisplayName, t.DisplayNameKey));
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                cm.Children.Add(cat);
            }

            return indicators;
        }

        public List<ReportIndicator> GetDemographyIndicators()
        {
            List<ReportIndicator> indicators = new List<ReportIndicator>();

            indicators.Add(new ReportIndicator { ID = -1, Name = TranslationLookup.GetValue("YearCensus"), DataTypeId = 6, Key = "YearCensus" });
            indicators.Add(new ReportIndicator { ID = -1, Name = TranslationLookup.GetValue("YearProjections"), DataTypeId = 6, Key = "YearProjections" });
            indicators.Add(new ReportIndicator { ID = -1, Name = TranslationLookup.GetValue("GrowthRate"), DataTypeId = 2, Key = "GrowthRate" });
            indicators.Add(new ReportIndicator { ID = -1, Name = TranslationLookup.GetValue("PercentRural"), DataTypeId = 2, Key = "PercentRural" });
            indicators.Add(new ReportIndicator { ID = -1, Name = TranslationLookup.GetValue("TotalPopulation"), DataTypeId = 2, Key = "TotalPopulation" });
            indicators.Add(new ReportIndicator { ID = -1, Name = TranslationLookup.GetValue("Pop0Month"), DataTypeId = 2, Key = "Pop0Month" });
            indicators.Add(new ReportIndicator { ID = -1, Name = TranslationLookup.GetValue("PopPsac"), DataTypeId = 2, Key = "PopPsac" });
            indicators.Add(new ReportIndicator { ID = -1, Name = TranslationLookup.GetValue("PopSac"), DataTypeId = 2, Key = "PopSac" });
            indicators.Add(new ReportIndicator { ID = -1, Name = TranslationLookup.GetValue("Pop5yo"), DataTypeId = 2, Key = "Pop5yo" });
            indicators.Add(new ReportIndicator { ID = -1, Name = TranslationLookup.GetValue("PopAdult"), DataTypeId = 2, Key = "PopAdult" });
            indicators.Add(new ReportIndicator { ID = -1, Name = TranslationLookup.GetValue("PopFemale"), DataTypeId = 2, Key = "PopFemale" });
            indicators.Add(new ReportIndicator { ID = -1, Name = TranslationLookup.GetValue("PopMale"), DataTypeId = 2, Key = "PopMale" });
            indicators.Add(new ReportIndicator { ID = -1, Name = TranslationLookup.GetValue("Notes"), DataTypeId = 15, Key = "Notes" });

            return indicators.OrderBy(r => r.Name).ToList();
        }

        public static ReportIndicator CreateReportIndicator(int typeId, KeyValuePair<string, Indicator> i)
        {
            return CreateReportIndicator(typeId, i.Value, "", "");
        }
        
        public static ReportIndicator CreateReportIndicator(int typeId, Indicator i)
        {
            return CreateReportIndicator(typeId, i, "", "");
        }

        public static ReportIndicator CreateReportIndicator(int typeId, KeyValuePair<string, Indicator> i, string formName)
        {
            return CreateReportIndicator(typeId, i.Value, formName, "");
        }

        public static ReportIndicator CreateReportIndicator(int typeId, KeyValuePair<string, Indicator> i, string formName, string formNameKey)
        {
            return CreateReportIndicator(typeId, i.Value, formName, formNameKey);
        }

        public static ReportIndicator CreateReportIndicator(int typeId, Indicator i, string formName, string formNameKey)
        {
            string name = i.DisplayName;
            if (!i.IsEditable)
                name = TranslationLookup.GetValue(i.DisplayName, i.DisplayName);

            return new ReportIndicator
            {
                ID = i.Id,
                Name = name,
                DataTypeId = i.DataTypeId,
                IsCalculated = i.IsCalculated,
                IsStatic = !i.IsEditable,
                TypeId = typeId,
                Key = i.DisplayName,
                AggregationRuleId = i.AggRuleId,
                SplitRule = i.RedistrictRuleId,
                MergeRule = i.MergeRuleId,
                IsDisabled = i.IsDisabled,
                IsRequired = i.IsRequired,
                FormName = formName,
                FormNameKey = formNameKey,
                SortOrder = i.SortOrder
            };
        }
        #endregion

        #region Custom Reports
        public List<SavedReport> GetCustomReports()
        {
            List<SavedReport> list = new List<SavedReport>();

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName, ReportOptions, aspnet_users.UserName, UpdatedAt, CreatedAt, c.UserName as CreatedBy from 
                        ((CustomReports INNER JOIN aspnet_users on CustomReports.UpdatedById = aspnet_users.userid)
                        INNER JOIN aspnet_users c on CustomReports.CreatedById = c.userid)
                        WHERE IsDeleted=0", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var report = new SavedReport
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                                UpdatedBy = GetAuditInfo(reader)
                            };
                            report.Deserialize(reader.GetValueOrDefault<string>("ReportOptions"));
                            list.Add(report);

                        }
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return list;
        }

        public void Save(SavedReport report, int userId)
        {
            bool transWasStarted = false;
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    OleDbCommand command = new OleDbCommand("BEGIN TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = true;

                    if (report.Id > 0)
                        command = new OleDbCommand(@"UPDATE CustomReports SET DisplayName=@DisplayName, ReportOptions=@ReportOptions,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO CustomReports (DisplayName, ReportOptions, UpdatedById, 
                            UpdatedAt, CreatedById, CreatedAt) values (@DisplayName, @ReportOptions, @UpdatedById, @UpdatedAt, @CreatedById,
                            @CreatedAt)", connection);

                    command.Parameters.Add(new OleDbParameter("@DisplayName", report.DisplayName));
                    command.Parameters.Add(new OleDbParameter("@ReportOptions", report.Serialize()));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (report.Id > 0)
                        command.Parameters.Add(new OleDbParameter("@id", report.Id));
                    else
                    {
                        command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
                    }

                    command.ExecuteNonQuery();
                    if (report.Id <= 0)
                    {
                        command = new OleDbCommand(@"SELECT Max(ID) FROM CustomReports", connection);
                        report.Id = (int)command.ExecuteScalar();
                    }

                    // COMMIT TRANS
                    command = new OleDbCommand("COMMIT TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = false;
                }
                catch (Exception)
                {
                    if (transWasStarted)
                    {
                        try
                        {
                            OleDbCommand cmd = new OleDbCommand("ROLLBACK TRANSACTION", connection);
                            cmd.ExecuteNonQuery();
                        }
                        catch { }
                    }
                    throw;
                }
            }

        }

        public void DeleteCustomReport(SavedReport report, int userId)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    OleDbCommand command = new OleDbCommand("BEGIN TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    // START TRANS
                    command =  new OleDbCommand(@"UPDATE CustomReports SET IsDeleted= Yes,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@ID", report.Id));
                    
                    command.ExecuteNonQuery();

                    command = new OleDbCommand("COMMIT TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        #endregion

        #region Redistricting Report
        public DataTable RunRedistrictingReport()
        {
            DemoRepository demo = new DemoRepository();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn(TranslationLookup.GetValue("RedistrictDate")));
            dataTable.Columns.Add(new DataColumn(TranslationLookup.GetValue("RedistOrigName")));
            dataTable.Columns.Add(new DataColumn(TranslationLookup.GetValue("RedistOrigPop")));
            dataTable.Columns.Add(new DataColumn(TranslationLookup.GetValue("RedistLevel")));
            dataTable.Columns.Add(new DataColumn(TranslationLookup.GetValue("RedistOrigParent")));
            dataTable.Columns.Add(new DataColumn(TranslationLookup.GetValue("RedistNewName")));
            dataTable.Columns.Add(new DataColumn(TranslationLookup.GetValue("RedistNewPop")));
            dataTable.Columns.Add(new DataColumn(TranslationLookup.GetValue("RedistNewParent")));
            dataTable.Columns.Add(new DataColumn(TranslationLookup.GetValue("RedistType")));
            dataTable.Columns.Add(new DataColumn(TranslationLookup.GetValue("RedistEventDate")));
            dataTable.Columns.Add(new DataColumn(TranslationLookup.GetValue("ID")));
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select ID, CreatedAt, RedistrictTypeId, RedistrictDate
                        FROM RedistrictEvents 
                        ORDER BY CreatedAt DESC", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetValueOrDefault<int>("ID");
                            Nullable<DateTime> redistrictDate = reader.GetValueOrDefault<Nullable<DateTime>>("RedistrictDate");
                            DateTime date = reader.GetValueOrDefault<DateTime>("CreatedAt");
                            SplittingType t = (SplittingType)reader.GetValueOrDefault<int>("RedistrictTypeId");
                            List<RedistrictedReportUnit> daughters = new List<RedistrictedReportUnit>();
                            List<RedistrictedReportUnit> mothers = new List<RedistrictedReportUnit>();
                            string typeName = "";

                            command = new OleDbCommand(@"Select a.ID as AID, a.DisplayName as AName, p.DisplayName as PName, u.RelationshipId, t.DisplayName as TName
                                FROM (((RedistrictUnits u INNER JOIN  AdminLevels a on a.ID = u.AdminLevelUnitId)
                                    INNER JOIN AdminLevels p on a.ParentId = p.ID)
                                    INNER JOIN AdminLevelTypes t on a.AdminLevelTypeId = t.ID)
                                WHERE u.RedistrictEventID = @id", connection);
                            command.Parameters.Add(new OleDbParameter("@id", id));
                            using (OleDbDataReader reader2 = command.ExecuteReader())
                            {
                                while (reader2.Read())
                                {
                                    typeName = reader2.GetValueOrDefault<string>("TName");
                                    if (RedistrictingRelationship.Daughter == (RedistrictingRelationship)reader2.GetValueOrDefault<int>("RelationshipId"))
                                        daughters.Add(new RedistrictedReportUnit { Id = reader2.GetValueOrDefault<int>("AID"), Name = reader2.GetValueOrDefault<string>("AName"), Parent = reader2.GetValueOrDefault<string>("PName") });
                                    else if (RedistrictingRelationship.Mother == (RedistrictingRelationship)reader2.GetValueOrDefault<int>("RelationshipId"))
                                        mothers.Add(new RedistrictedReportUnit { Id = reader2.GetValueOrDefault<int>("AID"), Name = reader2.GetValueOrDefault<string>("AName"), Parent = reader2.GetValueOrDefault<string>("PName") });
                                }
                            }

                            if (t == SplittingType.Split)
                            {
                                var source = mothers.FirstOrDefault();
                                source.Pop = GetRecentPopulation(source.Id, redistrictDate, connection, demo);
                                foreach (var dest in daughters)
                                {
                                    dest.Pop = GetRecentPopulation(dest.Id, redistrictDate, connection, demo);
                                    AddRedistrictingReportRow(dataTable, source, dest, TranslationLookup.GetValue("RedistrictTypeSplit"), date, typeName, id, redistrictDate);
                                }
                            }
                            else if (t == SplittingType.Merge)
                            {
                                var dest = daughters.FirstOrDefault();
                                dest.Pop = GetRecentPopulation(dest.Id, redistrictDate, connection, demo);
                                foreach (var source in mothers)
                                {
                                    source.Pop = GetRecentPopulation(source.Id, redistrictDate, connection, demo);
                                    AddRedistrictingReportRow(dataTable, source, dest, TranslationLookup.GetValue("RedistrictTypeMerge"), date, typeName, id, redistrictDate);
                                }
                            }
                            else // splitcomb
                            {
                                var dest = daughters.FirstOrDefault();
                                dest.Pop = GetRecentPopulation(dest.Id, redistrictDate, connection, demo);
                                foreach (var source in mothers)
                                {
                                    source.Pop = GetRecentPopulation(source.Id, redistrictDate, connection, demo);
                                    AddRedistrictingReportRow(dataTable, source, dest, TranslationLookup.GetValue("RedistrictTypeSplitCombine"), date, typeName, id, redistrictDate);
                                }
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }
            return dataTable;
        }

        private int GetRecentPopulation(int adminLevelId, DateTime? redistrictDate, OleDbConnection connection, DemoRepository demo)
        {
            var recentDemo = demo.GetRecentDemography(adminLevelId, null, redistrictDate, connection);
            if (recentDemo != null)
                if (recentDemo.TotalPopulation.HasValue)
                    return Convert.ToInt32(recentDemo.TotalPopulation.Value);
            return 0;
        }



        public class RedistrictedReportUnit
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Parent { get; set; }
            public int Pop { get; set; }
        }

        private void AddRedistrictingReportRow(DataTable report, RedistrictedReportUnit oldUnit, RedistrictedReportUnit newUnit, string typeName, 
            DateTime date, string levelName, int id, Nullable<DateTime> redistrictDate)
        {
            DataRow dr = report.NewRow();
            if(redistrictDate.HasValue)
                dr[TranslationLookup.GetValue("RedistrictDate")] = redistrictDate.Value.ToShortDateString();
            dr[TranslationLookup.GetValue("RedistOrigName")] = oldUnit.Name;
            dr[TranslationLookup.GetValue("RedistOrigPop")] = oldUnit.Pop;
            dr[TranslationLookup.GetValue("RedistLevel")] = levelName;
            dr[TranslationLookup.GetValue("RedistOrigParent")] = oldUnit.Parent;
            dr[TranslationLookup.GetValue("RedistNewName")] = newUnit.Name;
            dr[TranslationLookup.GetValue("RedistNewPop")] = newUnit.Pop;
            dr[TranslationLookup.GetValue("RedistNewParent")] = newUnit.Parent;
            dr[TranslationLookup.GetValue("RedistType")] = typeName;
            dr[TranslationLookup.GetValue("RedistEventDate")] = date.ToShortDateString();
            dr[TranslationLookup.GetValue("ID")] = id;

            report.Rows.Add(dr);
        }
        #endregion
    }
}
