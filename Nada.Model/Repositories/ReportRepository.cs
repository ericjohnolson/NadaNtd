using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
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
                    if (settings.SurveyIndicators.Where(i => i.Selected && i.IsStatic).Count() > 0)
                        QueryLfMfSurvey(selectedIndicators, command, connection, table, chart);

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
        
        private void QueryLfMfSurvey(Dictionary<string, int> selected, OleDbCommand command, OleDbConnection connection, DataTable table, DataTable chart)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select AdminLevels.DisplayName as Location, SurveyLfMf.TimingType, SurveyLfMf.TestType, SurveyLfMf.SiteType, SurveyLfMf.StartDate ");
            
            sb.Append(" FROM (SurveyLfMf INNER JOIN AdminLevels ON SurveyLfMf.AdminLevelId = AdminLevels.ID) ");
            
            command = new OleDbCommand(sb.ToString(), connection);
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    DataRow dr = table.NewRow();
                    dr["Location"] = reader.GetValueOrDefault<string>("Location");
                    dr["Type"] = reader.GetValueOrDefault<string>("TimingType") + ", " + 
                        reader.GetValueOrDefault<string>("TestType") + ", " + reader.GetValueOrDefault<string>("SiteType");
                    dr["Year"] = reader.GetValueOrDefault<DateTime>("StartDate").ToString("yyyy");
                    if (selected.ContainsKey("static1"))
                    {
                        dr["Rounds MDA"] = reader.GetValueOrDefault<Nullable<int>>("RoundsMda");
                        CreateChartRow(chart, dr["Location"].ToString(), dr["Year"].ToString(), "Rounds MDA", 0, dr["Rounds MDA"].ToString());
                    }
                    if (selected.ContainsKey("static2"))
                    {
                        dr["Examined"] = reader.GetValueOrDefault<Nullable<int>>("Examined");
                        CreateChartRow(chart, dr["Location"].ToString(), dr["Year"].ToString(), "Examined", 0, dr["Examined"].ToString());
                    }
                    if (selected.ContainsKey("static3"))
                    {
                        dr["Positive"] = reader.GetValueOrDefault<Nullable<int>>("Positive");
                        CreateChartRow(chart, dr["Location"].ToString(), dr["Year"].ToString(), "Positive", 0, dr["Positive"].ToString());
                    }
                    if (selected.ContainsKey("static4"))
                    {
                        dr["Mean Density"] = reader.GetValueOrDefault<Nullable<int>>("MeanDensity");
                        CreateChartRow(chart, dr["Location"].ToString(), dr["Year"].ToString(), "Mean Density", 0, dr["Mean Density"].ToString());
                    }
                    if (selected.ContainsKey("static5"))
                    {
                        dr["MF Count"] = reader.GetValueOrDefault<Nullable<int>>("MfCount");
                        CreateChartRow(chart, dr["Location"].ToString(), dr["Year"].ToString(), "MF Count", 0, dr["MF Count"].ToString());
                    }
                    if (selected.ContainsKey("static6"))
                    {
                        dr["MF Load"] = reader.GetValueOrDefault<Nullable<int>>("MfLoad");
                        CreateChartRow(chart, dr["Location"].ToString(), dr["Year"].ToString(), "MF Load", 0, dr["MF Load"].ToString());
                    }
                    if (selected.ContainsKey("static7"))
                    {
                        dr["Sample Size"] = reader.GetValueOrDefault<Nullable<int>>("SampleSize");
                        CreateChartRow(chart, dr["Location"].ToString(), dr["Year"].ToString(), "Sample Size", 0, dr["Sample Size"].ToString());
                    }
                    if (selected.ContainsKey("static8"))
                    {
                        dr["Age Range"] = reader.GetValueOrDefault<Nullable<int>>("AgeRange");
                        CreateChartRow(chart, dr["Location"].ToString(), dr["Year"].ToString(), "Age Range", 0, dr["Age Range"].ToString());
                    }

                    table.Rows.Add(dr);
                }
                reader.Close();
            }
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
    }
}
