﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Reports;

namespace Nada.Model.Repositories
{
    public class ReportRepository
    {
        public void GetReportData(ReportIndicators settings, DataTable table, DataTable chart)
        {
            // Create dictionary,
            Dictionary<string, DataRow> tableRows = new Dictionary<string, DataRow>();
            Dictionary<string, int> selectedIndicators = CreateIndicatorDictionary(settings);
            if (selectedIndicators.Count == 0)
                return;

            // if a location exists ADD indicator to column
            // if not add new row to new dictionary entry for location
            // for chart add row for each indicator value 
            // CreateChartRow(DataTable chartData, DataRow dr)

            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // Load LF MF Indicators
                    OleDbCommand command = new OleDbCommand();

                    if (settings.SurveyIndicators.Where(i => i.Selected && !i.IsStatic).Count() > 0)
                    {
                        command = new OleDbCommand(@"Select SurveyIndicators.DisplayName as IndicatorName, 
                            SurveyIndicatorValues.DynamicValue,
                            SurveyIndicators.DataTypeId,
                            SurveyIndicators.Id,
                            SurveyTypes.SurveyTypeName,
                            Surveys.SurveyDate,
                            AdminLevels.DisplayName as Location
                        FROM ((((SurveyIndicators INNER JOIN SurveyIndicatorValues on SurveyIndicators.ID = SurveyIndicatorValues.IndicatorId)
                            INNER JOIN SurveyTypes ON SurveyIndicators.SurveyTypeId = SurveyTypes.ID)
                            INNER JOIN Surveys ON SurveyIndicatorValues.SurveyId = Surveys.ID)
                            INNER JOIN AdminLevels ON Surveys.AdminLevelId = AdminLevels.ID)
                        WHERE SurveyIndicators.Id in (" + String.Join(", ", settings.SurveyIndicators.Where(i => i.Selected && !i.IsStatic).Select(s => s.ID.ToString()).ToArray()) + ")", connection);
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DataRow dr = table.NewRow();
                                dr["Location"] = reader.GetValueOrDefault<string>("Location");
                                dr["Type"] = reader.GetValueOrDefault<string>("SurveyTypeName");
                                dr["Year"] = reader.GetValueOrDefault<DateTime>("SurveyDate").ToString("yyyy");
                                dr[reader.GetValueOrDefault<string>("IndicatorName")] = reader.GetValueOrDefault<string>("DynamicValue");
                                table.Rows.Add(dr);

                                CreateChartRow(chart, dr["Location"].ToString(), dr["Year"].ToString(),
                                    reader.GetValueOrDefault<string>("IndicatorName"),
                                    reader.GetValueOrDefault<int>("Id"),
                                    reader.GetValueOrDefault<string>("DynamicValue"));
                            }
                            reader.Close();
                        }
                    }

                    if (settings.InterventionIndicators.Where(i => i.Selected && !i.IsStatic).Count() > 0)
                    {
                        command = new OleDbCommand(@"Select InterventionIndicators.DisplayName as IndicatorName, 
                            InterventionIndicatorValues.DynamicValue,
                            InterventionIndicators.DataTypeId,
                            InterventionIndicators.Id,
                            InterventionTypes.InterventionTypeName,
                            Interventions.InterventionDate,
                            AdminLevels.DisplayName as Location
                        FROM ((((InterventionIndicators INNER JOIN InterventionIndicatorValues on InterventionIndicators.ID = InterventionIndicatorValues.IndicatorId)
                            INNER JOIN InterventionTypes ON InterventionIndicators.InterventionTypeId = InterventionTypes.ID)
                            INNER JOIN Interventions ON InterventionIndicatorValues.InterventionId = Interventions.ID)
                            INNER JOIN AdminLevels ON Interventions.AdminLevelId = AdminLevels.ID)
                        WHERE InterventionIndicators.Id in (" + String.Join(", ", settings.InterventionIndicators.Where(i => i.Selected && !i.IsStatic).Select(s => s.ID.ToString()).ToArray()) + ")", connection);
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DataRow dr = table.NewRow();
                                dr["Location"] = reader.GetValueOrDefault<string>("Location");
                                dr["Type"] = reader.GetValueOrDefault<string>("InterventionTypeName");
                                dr["Year"] = reader.GetValueOrDefault<DateTime>("InterventionDate").ToString("yyyy");
                                dr[reader.GetValueOrDefault<string>("IndicatorName")] = reader.GetValueOrDefault<string>("DynamicValue");
                                table.Rows.Add(dr);

                                CreateChartRow(chart, dr["Location"].ToString(), dr["Year"].ToString(),
                                    reader.GetValueOrDefault<string>("IndicatorName"),
                                    reader.GetValueOrDefault<int>("Id"),
                                    reader.GetValueOrDefault<string>("DynamicValue"));
                            }
                            reader.Close();
                        }
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private Dictionary<string, int> CreateIndicatorDictionary(ReportIndicators settings)
        {
            Dictionary<string, int> inds = new Dictionary<string, int>();
            foreach (var i in settings.InterventionIndicators.Where(ii => ii.Selected))
                inds.Add(i.Key, i.ID);
            foreach (var i in settings.SurveyIndicators.Where(ii => ii.Selected))
                inds.Add(i.Key, i.ID);
            return inds;
        }

        private void CreateChartRow(DataTable chartData, string location, string year, string name, int id, string value)
        {
            // only numbers
            var newRow = chartData.NewRow();
            newRow["Location"] = location;
            newRow["Year"] = year;
            newRow["IndicatorName"] = name;
            newRow["IndicatorId"] = id;
            newRow["Value"] = value;
            chartData.Rows.Add(newRow);
        }

        #region ReportGenerators

        public void AddIndicatorsToAggregate(string cmdText, ReportOptions options, Dictionary<int, AdminLevelIndicators> dic, OleDbCommand command,
            OleDbConnection connection, Func<OleDbDataReader, string> getKey, Func<OleDbDataReader, string> getName)
        {
            command = new OleDbCommand(cmdText + " (" + String.Join(", ", options.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ") "
                             + CreateYearFilter(options)
                       , connection);

            FillDictionary(command, connection, dic, options, getKey, getName);
        }

        private void FillDictionary(OleDbCommand command, OleDbConnection connection, Dictionary<int, AdminLevelIndicators> dic, ReportOptions options,
            Func<OleDbDataReader, string> getKey, Func<OleDbDataReader, string> getName)
        {            
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string indicatorName = getName(reader);
                    string indicatorKey = getKey(reader);
                    int adminLevelId = reader.GetValueOrDefault<int>("AID");
                    int dataType = reader.GetValueOrDefault<int>("DataTypeId");
                    string value = dataType == (int)IndicatorDataType.Multiselect ? reader.GetValueOrDefault<string>("DynamicValue").Replace("|", ", ") : reader.GetValueOrDefault<string>("DynamicValue");
                    if (dic.ContainsKey(adminLevelId))
                    {
                        var newIndicator = new AggregateIndicator
                        {
                            IndicatorId = reader.GetValueOrDefault<int>("IndicatorId"),
                            Name = indicatorName,
                            Key = indicatorKey,
                            DataType = dataType,
                            Value = value,
                            AggType = reader.GetValueOrDefault<int>("AggTypeId"), 
                            Year = reader.GetValueOrDefault<int>("YearReported")
                        };
                        if (dic[adminLevelId].Indicators.ContainsKey(indicatorKey))
                        {
                            // TODO FIX AGGREGATION CUSTOM
                            object val = IndicatorAggregator.Aggregate(newIndicator, dic[adminLevelId].Indicators[indicatorKey].Value);
                            dic[adminLevelId].Indicators[indicatorKey].Value = val == null ? "" : val.ToString();
                        }
                        else
                            dic[adminLevelId].Indicators.Add(indicatorKey, newIndicator);
                        
                        // Add Column
                        if (!options.Columns.ContainsKey(indicatorKey))
                            options.Columns.Add(indicatorKey, indicatorName);
                    }
                }
                reader.Close();
            }
        }

        public DataTable CreateNonAggregatedReport(string cmdText, ReportOptions options, DataTable dt, Func<OleDbDataReader, string> getName)
        {
            Dictionary<int, DataRow> ids = new Dictionary<int, DataRow>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(cmdText + " (" + String.Join(", ", options.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ") "
                              + CreateYearFilter(options) + CreateAdminFilter(options)
                        , connection);

                    FillDataTable(command, connection, ids, dt, getName);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return dt;
        }

        private void FillDataTable(OleDbCommand command, OleDbConnection connection, Dictionary<int, DataRow> ids, DataTable dt, Func<OleDbDataReader, string> getName)
        {
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetValueOrDefault<int>("ID");
                    string columnName = getName(reader);

                    if (!dt.Columns.Contains(columnName))
                        dt.Columns.Add(new DataColumn(columnName));

                    if (!ids.ContainsKey(id))
                    {
                        DataRow dr = dt.NewRow();
                        dr[Translations.Location] = reader.GetValueOrDefault<string>("DisplayName");
                        dr[Translations.Type] = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("TName"));
                        dr[Translations.Year] = reader.GetValueOrDefault<int>("YearReported");
                        dt.Rows.Add(dr);
                        ids.Add(id, dr);
                    }

                    string val = reader.GetValueOrDefault<string>("DynamicValue");
                    ids[id][columnName] = string.IsNullOrEmpty(val) ? "" : val.Replace("|", ", ");
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
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    string adminFilter = "";
                    if (options.IsByLevelAggregation || (options.IsNoAggregation && !options.IsAllLocations) || options.IsCountryAggregation)
                        adminFilter = " and AdminLevels.Id in (" + String.Join(", ", options.SelectedAdminLevels.Select(a => a.Id.ToString()).ToArray()) + ") ";
                    OleDbCommand command = new OleDbCommand(@"Select 
                                a.ID, a.YearDemographyData, AdminLevel.DisplayName, t.DisplayName as TName
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
                            FROM ((AdminLevelDemography a INNER JOIN AdminLevel on a.AdminLevelId = AdminLevel.ID)
                                INNER JOIN AdminLevelTypes t on t.Id = AdminLevel.AdminLevelTypeId)
                            WHERE a.IsDeleted = 0 and YearDemographyData in (" + String.Join(", ", options.SelectedYears.Select(a => a.ToString()).ToArray()) + ") " +
                            adminFilter
                        , connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow dr = dt.NewRow();
                            dr[Translations.Location] = reader.GetValueOrDefault<string>("DisplayName");
                            dr[Translations.Type] = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("TName"));
                            dr[Translations.Year] = reader.GetValueOrDefault<int>("YearDemographyData");
                            if (keys.ContainsKey("YearCensus")) dr[Translations.YearCensus] = reader.GetValueOrDefault<Nullable<int>>("YearCensus");
                            if (keys.ContainsKey("YearProjections")) dr[Translations.YearProjections] = reader.GetValueOrDefault<Nullable<int>>("YearProjections");
                            if (keys.ContainsKey("GrowthRate")) dr[Translations.GrowthRate] = reader.GetValueOrDefault<Nullable<double>>("GrowthRate");
                            if (keys.ContainsKey("PercentRural")) dr[Translations.PercentRural] = reader.GetValueOrDefault<Nullable<double>>("PercentRural");
                            if (keys.ContainsKey("TotalPopulation")) dr[Translations.TotalPopulation] = reader.GetValueOrDefault<Nullable<int>>("TotalPopulation");
                            if (keys.ContainsKey("Pop0Month")) dr[Translations.Pop0Month] = reader.GetValueOrDefault<Nullable<int>>("Pop0Month");
                            if (keys.ContainsKey("PopPsac")) dr[Translations.PopPsac] = reader.GetValueOrDefault<Nullable<int>>("PopPsac");
                            if (keys.ContainsKey("PopSac")) dr[Translations.PopSac] = reader.GetValueOrDefault<Nullable<int>>("PopSac");
                            if (keys.ContainsKey("Pop5yo")) dr[Translations.Pop5yo] = reader.GetValueOrDefault<Nullable<int>>("Pop5yo");
                            if (keys.ContainsKey("PopAdult")) dr[Translations.PopAdult] = reader.GetValueOrDefault<Nullable<int>>("PopAdult");
                            if (keys.ContainsKey("PopFemale")) dr[Translations.PopFemale] = reader.GetValueOrDefault<Nullable<int>>("PopFemale");
                            if (keys.ContainsKey("PopMale")) dr[Translations.YearCensus] = reader.GetValueOrDefault<Nullable<int>>("PopMale");
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


        private string CreateYearFilter(ReportOptions options)
        {
            string filter = " and YearReported in (" + String.Join(", ", options.SelectedYears.Select(a => a.ToString()).ToArray()) + ") ";
            return filter;
        }
        private string CreateAdminFilter(ReportOptions options)
        {
            string filter = " ";
            if (options.IsByLevelAggregation || (options.IsNoAggregation && !options.IsAllLocations))
                filter += " and AdminLevels.Id in (" + String.Join(", ", options.SelectedAdminLevels.Select(a => a.Id.ToString()).ToArray()) + ") ";
            return filter;
        }
        #endregion

        #region Indicator List
        public List<ReportIndicator> GetIntvIndicators()
        {
            List<ReportIndicator> indicators = new List<ReportIndicator>();
            IntvRepository repo = new IntvRepository();
            var types = repo.GetAllTypes();
            var pc = new ReportIndicator { Name = TranslationLookup.GetValue("PC"), IsCategory = true };
            var cm = new ReportIndicator { Name = TranslationLookup.GetValue("CM"), IsCategory = true };
            indicators.Add(pc);
            indicators.Add(cm);
            foreach (var t in types.Where(i => i.DiseaseType == "PC"))
            {
                var cat = new ReportIndicator { Name = t.IntvTypeName, IsCategory = true };
                var instance = repo.CreateIntv(t.Id);
                foreach (var i in instance.IntvType.Indicators)
                    cat.Children.Add(new ReportIndicator { ID = i.Value.Id, Name = TranslationLookup.GetValue(i.Value.DisplayName), DataTypeId = i.Value.DataTypeId });
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                pc.Children.Add(cat);
            }
            foreach (var t in types.Where(i => i.DiseaseType == "CM"))
            {
                var cat = new ReportIndicator { Name = t.IntvTypeName, IsCategory = true };
                var instance = repo.CreateIntv(t.Id);
                foreach (var i in instance.IntvType.Indicators)
                    cat.Children.Add(new ReportIndicator { ID = i.Value.Id, Name = TranslationLookup.GetValue(i.Value.DisplayName), DataTypeId = i.Value.DataTypeId });
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                cm.Children.Add(cat);
            }

            return indicators;
        }

        public List<ReportIndicator> GetSurveyIndicators()
        {
            // TODO: only show indicators that are reportable? also need to add calculated indicators... :(
            List<ReportIndicator> indicators = new List<ReportIndicator>();
            SurveyRepository repo = new SurveyRepository();
            var types = repo.GetSurveyTypes();
            var pc = new ReportIndicator { Name = TranslationLookup.GetValue("PC"), IsCategory = true };
            var cm = new ReportIndicator { Name = TranslationLookup.GetValue("CM"), IsCategory = true };
            indicators.Add(pc);
            indicators.Add(cm);
            foreach (var t in types.Where(i => i.DiseaseType == "PC"))
            {
                var cat = new ReportIndicator { Name = t.SurveyTypeName, IsCategory = true };
                var instance = repo.CreateSurvey(t.Id);
                foreach (var i in instance.TypeOfSurvey.Indicators)
                    cat.Children.Add(new ReportIndicator { ID = i.Value.Id, Name = TranslationLookup.GetValue(i.Value.DisplayName), DataTypeId = i.Value.DataTypeId });
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                pc.Children.Add(cat);
            }
            foreach (var t in types.Where(i => i.DiseaseType == "CM"))
            {
                var cat = new ReportIndicator { Name = t.SurveyTypeName, IsCategory = true };
                var instance = repo.CreateSurvey(t.Id);
                foreach (var i in instance.TypeOfSurvey.Indicators)
                    cat.Children.Add(new ReportIndicator { ID = i.Value.Id, Name = TranslationLookup.GetValue(i.Value.DisplayName), DataTypeId = i.Value.DataTypeId });
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
            var pc = new ReportIndicator { Name = TranslationLookup.GetValue("PC"), IsCategory = true };
            var cm = new ReportIndicator { Name = TranslationLookup.GetValue("CM"), IsCategory = true };
            indicators.Add(pc);
            indicators.Add(cm);
            foreach (var t in types.Where(i => i.DiseaseType == "PC"))
            {
                var cat = new ReportIndicator { Name = t.TypeName, IsCategory = true };
                var instance = repo.Create(t.Id);
                foreach (var i in instance.ProcessType.Indicators)
                    cat.Children.Add(new ReportIndicator { ID = i.Value.Id, Name = TranslationLookup.GetValue(i.Value.DisplayName), DataTypeId = i.Value.DataTypeId });
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                pc.Children.Add(cat);
            }
            foreach (var t in types.Where(i => i.DiseaseType == "CM"))
            {
                var cat = new ReportIndicator { Name = t.TypeName, IsCategory = true };
                var instance = repo.Create(t.Id);
                foreach (var i in instance.ProcessType.Indicators)
                    cat.Children.Add(new ReportIndicator { ID = i.Value.Id, Name = TranslationLookup.GetValue(i.Value.DisplayName), DataTypeId = i.Value.DataTypeId });
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                cm.Children.Add(cat);
            }

            return indicators;
        }

        public List<ReportIndicator> GetDiseaseDistroIndicators()
        {
            List<ReportIndicator> indicators = new List<ReportIndicator>();
            DiseaseRepository repo = new DiseaseRepository();
            var types = repo.GetSelectedDiseases();
            var pc = new ReportIndicator { Name = TranslationLookup.GetValue("PC"), IsCategory = true };
            var cm = new ReportIndicator { Name = TranslationLookup.GetValue("CM"), IsCategory = true };
            indicators.Add(pc);
            indicators.Add(cm);
            foreach (var t in types.Where(i => i.DiseaseType == "PC"))
            {
                var cat = new ReportIndicator { Name = t.DisplayName, IsCategory = true };
                DiseaseDistroPc dd = repo.Create((DiseaseType)t.Id);
                foreach (var i in dd.Indicators)
                    cat.Children.Add(new ReportIndicator { ID = i.Value.Id, Name = TranslationLookup.GetValue(i.Value.DisplayName), DataTypeId = i.Value.DataTypeId });
                cat.Children = cat.Children.OrderBy(c => c.Name).ToList();
                pc.Children.Add(cat);
            }
            foreach (var t in types.Where(i => i.DiseaseType == "CM"))
            {
                var cat = new ReportIndicator { Name = t.DisplayName, IsCategory = true };
                DiseaseDistroCm dd = repo.CreateCm((DiseaseType)t.Id);
                foreach (var i in dd.Indicators)
                    cat.Children.Add(new ReportIndicator { ID = i.Value.Id, Name = TranslationLookup.GetValue(i.Value.DisplayName), DataTypeId = i.Value.DataTypeId });
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
        #endregion
    }
}
