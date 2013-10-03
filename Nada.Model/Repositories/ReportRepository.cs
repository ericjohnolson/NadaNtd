using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.Globalization;
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

        public DataTable CreateSurveyReport(ReportOptions options)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SurveyId"));
            dt.Columns.Add(new DataColumn(Translations.Location));
            dt.Columns.Add(new DataColumn(Translations.SurveyType));
            dt.Columns.Add(new DataColumn(Translations.StartDateSurvey));
            dt.Columns.Add(new DataColumn(Translations.EndDateSurvey));

            foreach (var i in options.SelectedIndicators)
                dt.Columns.Add(new DataColumn(i.Name));

            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select 
                        Surveys.ID, 
                        SurveyTypes.SurveyTypeName, 
                        Surveys.SurveyTypeId, 
                        Surveys.StartDate, 
                        Surveys.EndDate, 
                        Surveys.UpdatedAt, 
                        aspnet_Users.UserName, AdminLevels.DisplayName
                        FROM (((Surveys INNER JOIN SurveyTypes on Surveys.SurveyTypeId = SurveyTypes.ID)
                            INNER JOIN aspnet_Users on Surveys.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN AdminLevels on Surveys.AdminLevelId = AdminLevels.ID) 
                        WHERE Surveys.IsDeleted = 0 
                        ORDER BY Surveys.EndDate DESC", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                                DataRow dr = dt.NewRow();
                                dr["SurveyId"] = reader.GetValueOrDefault<int>("ID");
                                dr[Translations.Location] = reader.GetValueOrDefault<string>("DisplayName");
                                dr[Translations.SurveyType] = reader.GetValueOrDefault<string>("SurveyTypeName");
                                dr[Translations.StartDateSurvey] = reader.GetValueOrDefault<DateTime>("StartDate");
                                dr[Translations.EndDateSurvey] = reader.GetValueOrDefault<DateTime>("EndDate");
                                dt.Rows.Add(dr);
                        }
                        reader.Close();
                    }

                    foreach(DataRow dr in dt.Rows)
                    {
                        command = new OleDbCommand(@"Select SurveyIndicators.DisplayName as IndicatorName, 
                            SurveyIndicatorValues.DynamicValue
                        FROM SurveyIndicators INNER JOIN SurveyIndicatorValues on SurveyIndicators.ID = SurveyIndicatorValues.IndicatorId
                        WHERE SurveyIndicatorValues.SurveyId = @SurveyId and SurveyIndicators.Id in (" + String.Join(", ", options.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ")"
                            , connection);
                        command.Parameters.Add(new OleDbParameter("@SurveyId", dr["SurveyId"]));
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dr[TranslationLookup.GetValue(reader.GetValueOrDefault<string>("IndicatorName"))] = reader.GetValueOrDefault<string>("DynamicValue");
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
            dt.Columns.Remove("SurveyId");
            return dt;
        }

        public DataTable CreateIntvReport(ReportOptions options)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("InterventionId"));
            dt.Columns.Add(new DataColumn(Translations.Location));
            dt.Columns.Add(new DataColumn(Translations.InterventionType));
            dt.Columns.Add(new DataColumn(Translations.StartDateMda));
            dt.Columns.Add(new DataColumn(Translations.EndDateMda));

            foreach (var i in options.SelectedIndicators)
                dt.Columns.Add(new DataColumn(i.Name));

            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select 
                        Interventions.ID, 
                        InterventionTypes.InterventionTypeName, 
                        Interventions.InterventionTypeId, 
                        Interventions.StartDate, 
                        Interventions.EndDate, 
                        Interventions.UpdatedAt, 
                        aspnet_Users.UserName, AdminLevels.DisplayName
                        FROM (((Interventions INNER JOIN InterventionTypes on Interventions.InterventionTypeId = InterventionTypes.ID)
                            INNER JOIN aspnet_Users on Interventions.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN AdminLevels on Interventions.AdminLevelId = AdminLevels.ID) 
                        WHERE Interventions.IsDeleted = 0 
                        ORDER BY Interventions.EndDate DESC", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow dr = dt.NewRow();
                            dr["InterventionId"] = reader.GetValueOrDefault<int>("ID");
                            dr[Translations.Location] = reader.GetValueOrDefault<string>("DisplayName");
                            dr[Translations.InterventionType] = reader.GetValueOrDefault<string>("InterventionTypeName");
                            dr[Translations.StartDateMda] = reader.GetValueOrDefault<DateTime>("StartDate");
                            dr[Translations.EndDateMda] = reader.GetValueOrDefault<DateTime>("EndDate");
                            dt.Rows.Add(dr);
                        }
                        reader.Close();
                    }

                    foreach (DataRow dr in dt.Rows)
                    {
                        command = new OleDbCommand(@"Select InterventionIndicators.DisplayName as IndicatorName, 
                            InterventionIndicatorValues.DynamicValue
                        FROM InterventionIndicators INNER JOIN InterventionIndicatorValues on InterventionIndicators.ID = InterventionIndicatorValues.IndicatorId
                        WHERE InterventionIndicatorValues.InterventionId = @InterventionId and InterventionIndicators.Id in (" + String.Join(", ", options.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ")"
                            , connection);
                        command.Parameters.Add(new OleDbParameter("@InterventionId", dr["InterventionId"]));
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dr[TranslationLookup.GetValue(reader.GetValueOrDefault<string>("IndicatorName"))] = reader.GetValueOrDefault<string>("DynamicValue");
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
            dt.Columns.Remove("InterventionId");
            return dt;
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

        private DataTable FormatResultData(DataTable data)
        {
            DataTable resultsTable = data.Copy();
            foreach (DataRow dr in resultsTable.Rows)
            {
                List<string> surveyType = new List<string>();
                surveyType.Add(dr["TestType"].ToString());
                surveyType.Add(dr["TimingType"].ToString());
                surveyType.Add(dr["SiteType"].ToString());
                surveyType.Add(Convert.ToDateTime(dr["SurveyDate"]).ToString("MM/yyyy"));
                dr["TimingType"] = string.Join(", ", surveyType.Where(t => !string.IsNullOrEmpty(t)).ToArray());
            }
            resultsTable.Columns.Remove(resultsTable.Columns["TestType"]);
            resultsTable.Columns.Remove(resultsTable.Columns["SurveyDate"]);
            resultsTable.Columns.Remove(resultsTable.Columns["SiteType"]);
            resultsTable.Columns[1].ColumnName = "Survey Type";
            return resultsTable;
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

        public ReportIndicators GetReportIndicators()
        {
            ReportIndicators ri = new ReportIndicators();
            ri.SurveyIndicators.Add(new ReportIndicator { Name = "Rounds MDA", Key = "static1", IsStatic = true, DataTypeId = 2 });
            ri.SurveyIndicators.Add(new ReportIndicator { Name = "Examined", Key = "static2", IsStatic = true, DataTypeId = 2 });
            ri.SurveyIndicators.Add(new ReportIndicator { Name = "Positive", Key = "static3", IsStatic = true, DataTypeId = 2 });
            ri.SurveyIndicators.Add(new ReportIndicator { Name = "Mean Density", Key = "static4", IsStatic = true, DataTypeId = 2 });
            ri.SurveyIndicators.Add(new ReportIndicator { Name = "MF Count", Key = "static5", IsStatic = true, DataTypeId = 2 });
            ri.SurveyIndicators.Add(new ReportIndicator { Name = "MF Load", Key = "static6", IsStatic = true, DataTypeId = 2 });
            ri.SurveyIndicators.Add(new ReportIndicator { Name = "Sample Size", Key = "static7", IsStatic = true, DataTypeId = 2 });
            ri.SurveyIndicators.Add(new ReportIndicator { Name = "Age Range", Key = "static8", IsStatic = true, DataTypeId = 2 });
            
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName, DataTypeId
                        FROM SurveyIndicators 
                        WHERE IsDisabled=0 
                        ORDER BY DisplayName", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ri.SurveyIndicators.Add(new ReportIndicator
                            {
                                Key = "survey" + reader.GetValueOrDefault<int>("ID"),
                                Name = reader.GetValueOrDefault<string>("DisplayName"),
                                ID = reader.GetValueOrDefault<int>("ID"),
                                DataTypeId = reader.GetValueOrDefault<int>("DataTypeId")
                            });
                        }
                        reader.Close();
                    }

                    command = new OleDbCommand(@"Select ID, DisplayName, DataTypeId
                        FROM InterventionIndicators 
                        WHERE IsDisabled=0 
                        ORDER BY DisplayName", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ri.InterventionIndicators.Add(new ReportIndicator
                            {
                                Key = "intv" + reader.GetValueOrDefault<int>("ID"),
                                Name = reader.GetValueOrDefault<string>("DisplayName"),
                                ID = reader.GetValueOrDefault<int>("ID"),
                                DataTypeId = reader.GetValueOrDefault<int>("DataTypeId")
                            });
                        }
                        reader.Close();
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }

            return ri;
        }

        public List<ReportIndicator> GetIntvIndicators()
        {
            List<ReportIndicator> indicators = new List<ReportIndicator>();
            var cat1 = new ReportIndicator { Name = Translations.PC, IsCategory = true };
            var cat2 = new ReportIndicator { Name = Translations.MDAs, IsCategory = true };
            cat1.Children.Add(cat2);
            indicators.Add(cat1);

            // NON-DYNAMIC INDICATORS??? which ones?
            //indicators.Add(new ReportIndicator { Name = "Rounds MDA", Key = "static1", IsStatic = true, DataTypeId = 2 });

            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName, DataTypeId
                        FROM InterventionIndicators 
                        WHERE IsDisabled=0  and InterventionTypeId=@InterventionTypeId 
                        ORDER BY DisplayName", connection);
                    command.Parameters.Add(new OleDbParameter("@InterventionTypeId", (int)StaticIntvType.IvmAlbMda));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cat2.Children.Add(new ReportIndicator
                            {
                                Name = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("DisplayName")),
                                ID = reader.GetValueOrDefault<int>("ID"),
                                DataTypeId = reader.GetValueOrDefault<int>("DataTypeId")
                            });
                        }
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return indicators;
        }

        public List<ReportIndicator> GetSurveyIndicators()
        {
            List<ReportIndicator> indicators = new List<ReportIndicator>();
            var cat1 = new ReportIndicator { Name = Translations.Surveys, IsCategory = true };
            var cat2 = new ReportIndicator { Name = Translations.LF, IsCategory = true };
            var cat3 = new ReportIndicator { Name = Translations.SentinelSpotSurvey, IsCategory = true };
            cat2.Children.Add(cat3);
            cat1.Children.Add(cat2);
            indicators.Add(cat1);

            // NON-DYNAMIC INDICATORS??? which ones?
            //indicators.Add(new ReportIndicator { Name = "Rounds MDA", Key = "static1", IsStatic = true, DataTypeId = 2 });

            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName, DataTypeId
                        FROM SurveyIndicators 
                        WHERE IsDisabled=0 and SurveyTypeId=@SurveyTypeId 
                        ORDER BY DisplayName", connection);
                    command.Parameters.Add(new OleDbParameter("@SurveyTypeId", (int)StaticSurveyType.LfPrevalence));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cat3.Children.Add(new ReportIndicator
                            {
                                Name = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("DisplayName")),
                                ID = reader.GetValueOrDefault<int>("ID"),
                                DataTypeId = reader.GetValueOrDefault<int>("DataTypeId")
                            });
                        }
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return indicators;
        }
    }
}
