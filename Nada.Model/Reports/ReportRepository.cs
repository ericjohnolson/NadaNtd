using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.DA;
using Nada.Globalization;
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
            Action<CreateAggParams> addStaticIndicators, bool isCalcRelated)
        {
            command = new OleDbCommand(cmdText, connection);

            FillDictionary(command, connection, dic, options, getKey, getName, getColTypeName, addStaticIndicators, isCalcRelated);
        }

        private void FillDictionary(OleDbCommand command, OleDbConnection connection, Dictionary<int, AdminLevelIndicators> dic, ReportOptions options,
            Func<OleDbDataReader, bool, ReportOptions, string> getKey, Func<OleDbDataReader, string> getName, Func<OleDbDataReader, string> getColTypeName,
            Action<CreateAggParams> addStaticIndicators, bool isCalcRelated)
        {
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string indicatorName = getName(reader);
                    string indicatorKey = getKey(reader, options.IsNoAggregation, options) + isCalcRelated;
                    int adminLevelId = reader.GetValueOrDefault<int>("AID");
                    int dataType = reader.GetValueOrDefault<int>("DataTypeId");
                    string value = GetDynamicValue(reader);
                    if (!dic.ContainsKey(adminLevelId))
                        continue;
                    var newIndicator = new AggregateIndicator
                    {
                        IndicatorId = reader.GetValueOrDefault<int>("IndicatorId"),
                        Name = indicatorName,
                        Key = reader.GetValueOrDefault<string>("IndicatorName"),
                        DataType = dataType,
                        Value = value,
                        AggType = reader.GetValueOrDefault<int>("AggTypeId"),
                        Year = Util.GetYearReported(options.MonthYearStarts, reader.GetValueOrDefault<DateTime>("DateReported")),
                        ReportedDate = reader.GetValueOrDefault<DateTime>("DateReported"),
                        IsCalcRelated = isCalcRelated,
                        ColumnTypeName = getColTypeName(reader),
                        TypeId = reader.GetValueOrDefault<int>("Tid"),
                        TypeName = reader.GetValueOrDefault<string>("TName"),
                        FormId = reader.GetValueOrDefault<int>("ID")
                    };
                    // if the adminlevel already contains the indicator for that year, aggregate
                    if (dic[adminLevelId].Indicators.ContainsKey(indicatorKey))
                        dic[adminLevelId].Indicators[indicatorKey] = IndicatorAggregator.Aggregate(newIndicator, dic[adminLevelId].Indicators[indicatorKey]);
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
                            FROM ((AdminLevelDemography a INNER JOIN AdminLevels on a.AdminLevelId = AdminLevels.ID)
                                INNER JOIN AdminLevelTypes t on t.Id = AdminLevels.AdminLevelTypeId)
                            WHERE a.IsDeleted = 0 " + CreateYearFilter(options, "DateDemographyData") +
                            adminFilter
                        , connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow dr = dt.NewRow();
                            dr[Translations.ID] = reader.GetValueOrDefault<int>("aID");
                            dr[Translations.Location] = reader.GetValueOrDefault<string>("DisplayName");
                            dr[Translations.Type] = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("TName"), reader.GetValueOrDefault<string>("TName"));
                            dr[Translations.Year] = Util.GetYearReported(options.MonthYearStarts, reader.GetValueOrDefault<DateTime>("DateReported"));
                            if (keys.ContainsKey("YearCensus")) dr[Translations.YearCensus] = reader.GetValueOrDefault<Nullable<int>>("YearCensus");
                            if (keys.ContainsKey("YearProjections")) dr[Translations.YearProjections] = reader.GetValueOrDefault<Nullable<int>>("YearProjections");
                            if (keys.ContainsKey("GrowthRate")) dr[Translations.GrowthRate] = reader.GetValueOrDefault<Nullable<double>>("GrowthRate");
                            if (keys.ContainsKey("PercentRural")) dr[Translations.PercentRural] = reader.GetValueOrDefault<Nullable<double>>("PercentRural");
                            if (keys.ContainsKey("TotalPopulation")) dr[Translations.TotalPopulation] = reader.GetValueOrDefault<Nullable<double>>("TotalPopulation");
                            if (keys.ContainsKey("Pop0Month")) dr[Translations.Pop0Month] = reader.GetValueOrDefault<Nullable<double>>("Pop0Month");
                            if (keys.ContainsKey("PopPsac")) dr[Translations.PopPsac] = reader.GetValueOrDefault<Nullable<double>>("PopPsac");
                            if (keys.ContainsKey("PopSac")) dr[Translations.PopSac] = reader.GetValueOrDefault<Nullable<double>>("PopSac");
                            if (keys.ContainsKey("Pop5yo")) dr[Translations.Pop5yo] = reader.GetValueOrDefault<Nullable<double>>("Pop5yo");
                            if (keys.ContainsKey("PopAdult")) dr[Translations.PopAdult] = reader.GetValueOrDefault<Nullable<double>>("PopAdult");
                            if (keys.ContainsKey("PopFemale")) dr[Translations.PopFemale] = reader.GetValueOrDefault<Nullable<double>>("PopFemale");
                            if (keys.ContainsKey("PopMale")) dr[Translations.PopMale] = reader.GetValueOrDefault<Nullable<double>>("PopMale");
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

        private string GetDynamicValue(OleDbDataReader reader)
        {
            int dataType = reader.GetValueOrDefault<int>("DataTypeId");
            string value = reader.GetValueOrDefault<string>("DynamicValue");
            if (value == null)
                return null;
            if (dataType == (int)IndicatorDataType.Multiselect || dataType == (int)IndicatorDataType.DiseaseMultiselect)
                value = value.Replace("|", ", ");
            else if (dataType == (int)IndicatorDataType.Partners)
            {
                List<string> names = new List<string>();
                string[] vals = value.Split('|');
                foreach (var partner in partners.Where(v => vals.Contains(v.Id.ToString())))
                    names.Add(partner.DisplayName);
                value = String.Join(", ", names.ToArray());
            }
            else if (dataType == (int)IndicatorDataType.EcologicalZone)
            {
                List<string> names = new List<string>();
                string[] vals = value.Split('|');
                foreach (var ez in ezs.Where(v => vals.Contains(v.Id.ToString())))
                    names.Add(ez.DisplayName);
                value = String.Join(", ", names.ToArray());
            }
            else if (dataType == (int)IndicatorDataType.EvaluationUnit)
            {
                List<string> names = new List<string>();
                string[] vals = value.Split('|');
                foreach (var eu in eus.Where(v => vals.Contains(v.Id.ToString())))
                    names.Add(eu.DisplayName);
                value = String.Join(", ", names.ToArray());
            }
            else if (dataType == (int)IndicatorDataType.EvalSubDistrict)
            {
                List<string> names = new List<string>();
                string[] vals = value.Split('|');
                foreach (var sd in subdistricts.Where(v => vals.Contains(v.Id.ToString())))
                    names.Add(sd.DisplayName);
                value = String.Join(", ", names.ToArray());
            }
            else if (dataType == (int)IndicatorDataType.EvaluationSite)
            {
                List<string> names = new List<string>();
                string[] vals = value.Split('|');
                foreach (var es in evalsites.Where(v => vals.Contains(v.Id.ToString())))
                    names.Add(es.DisplayName);
                value = String.Join(", ", names.ToArray());
            }
            return value;
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
            var pc = new ReportIndicator { Name = Translations.PcNtds, IsCategory = true };
            var cm = new ReportIndicator { Name = Translations.OtherNtds, IsCategory = true };
            indicators.Add(pc);
            indicators.Add(cm);
            foreach (var t in types.Where(i => i.DiseaseType == Translations.PC).OrderBy(t => t.IntvTypeName))
            {
                var cat = new ReportIndicator { Name = t.IntvTypeName, IsCategory = true };
                var instance = repo.CreateIntv(t.Id);
                foreach (var i in instance.IntvType.Indicators)
                    cat.Children.Add(CreateReportIndicator(t.Id, i));
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                pc.Children.Add(cat);
            }
            foreach (var t in types.Where(i => i.DiseaseType == Translations.CM).OrderBy(t => t.IntvTypeName))
            {
                var cat = new ReportIndicator { Name = t.IntvTypeName, IsCategory = true };
                var instance = repo.CreateIntv(t.Id);
                foreach (var i in instance.IntvType.Indicators)
                    cat.Children.Add(CreateReportIndicator(t.Id, i));
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                cm.Children.Add(cat);
            }

            return indicators;
        }

        public List<ReportIndicator> GetSurveyIndicators()
        {
            List<ReportIndicator> indicators = new List<ReportIndicator>();
            SurveyRepository repo = new SurveyRepository();
            var types = repo.GetSurveyTypes();
            var pc = new ReportIndicator { Name = Translations.PcNtds, IsCategory = true };
            var cm = new ReportIndicator { Name = Translations.OtherNtds, IsCategory = true };
            indicators.Add(pc);
            indicators.Add(cm);
            foreach (var t in types.Where(i => i.DiseaseType == Translations.PC).OrderBy(t => t.SurveyTypeName))
            {
                var cat = new ReportIndicator { Name = t.SurveyTypeName, IsCategory = true, ID = t.Id };
                var instance = repo.CreateSurvey(t.Id);
                if (t.Id == (int)StaticSurveyType.LfSentinel || t.Id == (int)StaticSurveyType.SchistoSentinel || t.Id == (int)StaticSurveyType.SthSentinel)
                {
                    cat.Children.Add(new ReportIndicator { Name = Translations.IndSentinelSiteName });
                    cat.Children.Add(new ReportIndicator { Name = Translations.IndSentinelSiteLat });
                    cat.Children.Add(new ReportIndicator { Name = Translations.IndSentinelSiteLng });
                    cat.Children.Add(new ReportIndicator { Name = Translations.IndSpotCheckName });
                    cat.Children.Add(new ReportIndicator { Name = Translations.IndSpotCheckLat });
                    cat.Children.Add(new ReportIndicator { Name = Translations.IndSpotCheckLng });
                    cat.Children.Add(new ReportIndicator { Name = Translations.SiteType });
                }

                foreach (var i in instance.TypeOfSurvey.Indicators)
                    if (i.Value.DataTypeId != (int)IndicatorDataType.SentinelSite)
                        cat.Children.Add(CreateReportIndicator(t.Id, i));
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                pc.Children.Add(cat);
            }
            foreach (var t in types.Where(i => i.DiseaseType == Translations.CM).OrderBy(t => t.SurveyTypeName))
            {
                var cat = new ReportIndicator { Name = t.SurveyTypeName, IsCategory = true };
                var instance = repo.CreateSurvey(t.Id);
                foreach (var i in instance.TypeOfSurvey.Indicators)
                    cat.Children.Add(CreateReportIndicator(t.Id, i));
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
            var pc = new ReportIndicator { Name = Translations.PcNtds, IsCategory = true };
            var cm = new ReportIndicator { Name = Translations.OtherNtds, IsCategory = true };
            types.RemoveAll(t => t.TypeName == Translations.SAEs);
            indicators.Add(pc);
            indicators.Add(cm);
            foreach (var t in types.Where(i => i.DiseaseType == Translations.PC).OrderBy(t => t.TypeName))
            {
                var cat = new ReportIndicator { Name = t.TypeName, IsCategory = true };
                var instance = repo.Create(t.Id);
                foreach (var i in instance.ProcessType.Indicators)
                    cat.Children.Add(CreateReportIndicator(t.Id, i));
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                pc.Children.Add(cat);
            }
            foreach (var t in types.Where(i => i.DiseaseType == Translations.CM).OrderBy(t => t.TypeName))
            {
                var cat = new ReportIndicator { Name = t.TypeName, IsCategory = true };
                var instance = repo.Create(t.Id);
                foreach (var i in instance.ProcessType.Indicators)
                    cat.Children.Add(CreateReportIndicator(t.Id, i));
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                cm.Children.Add(cat);
            }

            // SAEs
            ProcessBase saes = repo.Create(9);
            var saeCat = new ReportIndicator { Name = saes.ProcessType.TypeName, IsCategory = true };
            foreach (var i in saes.ProcessType.Indicators)
                saeCat.Children.Add(CreateReportIndicator(saes.ProcessType.Id, i));
            saeCat.Children = saeCat.Children.OrderBy(c => c.Name).ToList();
            indicators.Add(saeCat);

            return indicators;
        }

        public List<ReportIndicator> GetDiseaseDistroIndicators()
        {
            List<ReportIndicator> indicators = new List<ReportIndicator>();
            DiseaseRepository repo = new DiseaseRepository();
            var types = repo.GetSelectedDiseases();
            var pc = new ReportIndicator { Name = Translations.PcNtds, IsCategory = true };
            var cm = new ReportIndicator { Name = Translations.OtherNtds, IsCategory = true };
            indicators.Add(pc);
            indicators.Add(cm);
            foreach (var t in types.Where(i => i.DiseaseType == Translations.PC).OrderBy(t => t.DisplayName))
            {
                var cat = new ReportIndicator { Name = t.DisplayName, IsCategory = true };
                DiseaseDistroPc dd = repo.Create((DiseaseType)t.Id);
                foreach (var i in dd.Indicators)
                    cat.Children.Add(CreateReportIndicator(t.Id, i));
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                pc.Children.Add(cat);
            }
            foreach (var t in types.Where(i => i.DiseaseType == Translations.CM).OrderBy(t => t.DisplayName))
            {
                var cat = new ReportIndicator { Name = t.DisplayName, IsCategory = true };
                DiseaseDistroCm dd = repo.CreateCm((DiseaseType)t.Id);
                foreach (var i in dd.Indicators)
                    cat.Children.Add(CreateReportIndicator(t.Id, i));
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

            return indicators.OrderBy(r => r.Name).ToList();
        }

        public static ReportIndicator CreateReportIndicator(int typeId, KeyValuePair<string, Indicator> i)
        {
            string name = i.Value.DisplayName;
            if (!i.Value.IsDisplayed)
                name = TranslationLookup.GetValue(i.Value.DisplayName, i.Value.DisplayName);

            return new ReportIndicator
            {
                ID = i.Value.Id,
                Name = name,
                DataTypeId = i.Value.DataTypeId,
                IsCalculated = i.Value.IsCalculated,
                IsStatic = !i.Value.IsEditable,
                TypeId = typeId,
                Key = i.Value.DisplayName
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


    }
}
