using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.DA;
using Nada.Globalization;
using Nada.Model.Base;
using Nada.Model.Diseases;
using Nada.Model.Intervention;

namespace Nada.Model.Repositories
{
    public class AdminLevelIndicators
    {
        public AdminLevelIndicators()
        {
            Children = new List<AdminLevelIndicators>();
            Indicators = new Dictionary<string, AggregateIndicator>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentId { get; set; }
        public AdminLevelIndicators Parent { get; set; }
        public List<AdminLevelIndicators> Children { get; set; }
        public int LevelNumber { get; set; }
        public bool IsDistrict { get; set; }
        public Dictionary<string, AggregateIndicator> Indicators { get; set; }
    }

    public class ExportRepository
    {
        public List<AdminLevelIndicators> GetDistrictIndicatorTrees(int interventionTypeId, int year, int diseaseId, Func<AggregateIndicator, object, object> customAggRule)
        {
            List<AdminLevelIndicators> list = new List<AdminLevelIndicators>();
            Dictionary<int, AdminLevelIndicators> dic = new Dictionary<int, AdminLevelIndicators>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                list = GetAdminLevels(command, connection);
                dic = list.ToDictionary(n => n.Id, n => n);
                AddIntvIndicators(interventionTypeId, year, dic, command, connection, customAggRule);
                AddSurveyIndicators(diseaseId, year, dic, command, connection, customAggRule);
                AddDistroIndicators(diseaseId, year, dic, command, connection, customAggRule);
            }

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
            return list.Where(a => a.IsDistrict).ToList();
        }


        private void AddIntvIndicators(int interventionTypeId, int year, Dictionary<int, AdminLevelIndicators> dic, OleDbCommand command,
            OleDbConnection connection, Func<AggregateIndicator, object, object> customAggRule)
        {
            List<string> interventionIds = new List<string>();
            command = new OleDbCommand(@"Select Interventions.ID as itemId FROM 
                        ((Interventions INNER JOIN InterventionIndicatorValues on InterventionIndicatorValues.InterventionId = Interventions.ID)
                            INNER JOIN InterventionIndicators on InterventionIndicators.ID = InterventionIndicatorValues.IndicatorId)
                        WHERE Interventions.InterventionTypeId=@InterventionTypeId AND InterventionIndicators.DisplayName = @YearName 
                        AND InterventionIndicatorValues.DynamicValue = @YearReporting and Interventions.IsDeleted = 0", connection);
            command.Parameters.Add(new OleDbParameter("@InterventionTypeId", interventionTypeId));
            command.Parameters.Add(new OleDbParameter("@YearName", "IntvYear"));
            command.Parameters.Add(new OleDbParameter("@YearReporting", year));
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    interventionIds.Add(reader.GetValueOrDefault<int>("itemId").ToString());
                }
                reader.Close();
            }

            if (interventionIds.Count == 0)
                return;

            command = new OleDbCommand(@"Select AdminLevels.ID as aid, InterventionIndicators.DataTypeId, InterventionIndicators.DisplayName as iname, 
                        InterventionIndicators.ID as iid, InterventionIndicatorValues.DynamicValue, InterventionIndicators.AggTypeId FROM 
                            ((((Interventions INNER JOIN InterventionIndicatorValues on InterventionIndicatorValues.InterventionId = Interventions.ID)
                            INNER JOIN InterventionIndicators on InterventionIndicators.ID = InterventionIndicatorValues.IndicatorId)
                            INNER JOIN AdminLevels on Interventions.AdminLevelId = AdminLevels.ID)
                            INNER JOIN AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.Id)
                        WHERE Interventions.ID in (" + String.Join(", ", interventionIds.ToArray()) +
                        ") ORDER BY InterventionIndicators.ID, AdminLevelTypes.AdminLevel", connection);
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                    AddIndicator(reader, dic, customAggRule);
                
                reader.Close();
            }

        }
       

        private void AddSurveyIndicators(int diseaseId, int year, Dictionary<int, AdminLevelIndicators> dic, OleDbCommand command,
            OleDbConnection connection, Func<AggregateIndicator, object, object> customAggRule)
        {
            List<string> surveyIds = new List<string>();
            command = new OleDbCommand(@"Select Surveys.ID as itemId FROM 
                        (((Surveys INNER JOIN SurveyIndicatorValues on SurveyIndicatorValues.SurveyId = Surveys.ID)
                            INNER JOIN SurveyIndicators on SurveyIndicators.ID = SurveyIndicatorValues.IndicatorId)
                            INNER JOIN SurveyTypes on SurveyTypes.ID = Surveys.SurveyTypeId)
                        WHERE SurveyTypes.DiseaseId=@DiseaseId AND SurveyIndicators.DisplayName = @YearName 
                        AND SurveyIndicatorValues.DynamicValue = @YearReporting and Surveys.IsDeleted = 0", connection);
            command.Parameters.Add(new OleDbParameter("@DiseaseId", diseaseId));
            command.Parameters.Add(new OleDbParameter("@YearName", "SurveyYear"));
            command.Parameters.Add(new OleDbParameter("@YearReporting", year));
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    surveyIds.Add(reader.GetValueOrDefault<int>("itemId").ToString());
                }
                reader.Close();
            }

            if (surveyIds.Count == 0)
                return;

            command = new OleDbCommand(@"Select AdminLevels.ID as aid, SurveyIndicators.DataTypeId, SurveyIndicators.DisplayName as iname, 
                        SurveyIndicators.ID as iid, SurveyIndicatorValues.DynamicValue, SurveyIndicators.AggTypeId FROM 
                            ((((Surveys INNER JOIN SurveyIndicatorValues on SurveyIndicatorValues.SurveyId = Surveys.ID)
                            INNER JOIN SurveyIndicators on SurveyIndicators.ID = SurveyIndicatorValues.IndicatorId)
                            INNER JOIN AdminLevels on Surveys.AdminLevelId = AdminLevels.ID)
                            INNER JOIN AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.Id)
                        WHERE Surveys.ID in (" + String.Join(", ", surveyIds.ToArray()) +
                        ") ORDER BY SurveyIndicators.ID, AdminLevelTypes.AdminLevel", connection);
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                    AddIndicator(reader, dic, customAggRule);
                reader.Close();
            }
        }

        private void AddDistroIndicators(int diseaseId, int year, Dictionary<int, AdminLevelIndicators> dic, OleDbCommand command,
            OleDbConnection connection, Func<AggregateIndicator, object, object> customAggRule)
        {
            List<string> diseaseDistributionIds = new List<string>();
            command = new OleDbCommand(@"Select DiseaseDistributions.ID as itemId FROM 
                        ((DiseaseDistributions INNER JOIN DiseaseDistributionIndicatorValues on 
                            DiseaseDistributionIndicatorValues.DiseaseDistributionId = DiseaseDistributions.ID)
                            INNER JOIN DiseaseDistributionIndicators on DiseaseDistributionIndicators.ID = DiseaseDistributionIndicatorValues.IndicatorId)
                        WHERE DiseaseDistributions.DiseaseId=@DiseaseId AND DiseaseDistributionIndicators.DisplayName = @YearName 
                        AND DiseaseDistributionIndicatorValues.DynamicValue = @YearReporting and DiseaseDistributions.IsDeleted = 0", connection);
            command.Parameters.Add(new OleDbParameter("@DiseaseId", diseaseId));
            command.Parameters.Add(new OleDbParameter("@YearName", "DiseaseYear"));
            command.Parameters.Add(new OleDbParameter("@YearReporting", year));
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    diseaseDistributionIds.Add(reader.GetValueOrDefault<int>("itemId").ToString());
                }
                reader.Close();
            }

            if (diseaseDistributionIds.Count == 0)
                return;

            command = new OleDbCommand(@"Select AdminLevels.ID as aid, DiseaseDistributionIndicators.DataTypeId, DiseaseDistributionIndicators.DisplayName as iname, 
                        DiseaseDistributionIndicators.ID as iid, DiseaseDistributionIndicatorValues.DynamicValue, DiseaseDistributionIndicators.AggTypeId FROM 
                            ((((DiseaseDistributions INNER JOIN DiseaseDistributionIndicatorValues on 
                                DiseaseDistributionIndicatorValues.DiseaseDistributionId = DiseaseDistributions.ID)
                            INNER JOIN DiseaseDistributionIndicators on DiseaseDistributionIndicators.ID = DiseaseDistributionIndicatorValues.IndicatorId)
                            INNER JOIN AdminLevels on DiseaseDistributions.AdminLevelId = AdminLevels.ID)
                            INNER JOIN AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.Id)
                        WHERE DiseaseDistributions.ID in (" + String.Join(", ", diseaseDistributionIds.ToArray()) +
                        ") ORDER BY DiseaseDistributionIndicators.ID, AdminLevelTypes.AdminLevel", connection);
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                    AddIndicator(reader, dic, customAggRule);

                reader.Close();
            }
        }

        private List<AdminLevelIndicators> GetAdminLevels(OleDbCommand command, OleDbConnection connection)
        {
            List<AdminLevelIndicators> list = new List<AdminLevelIndicators>();
            command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, AdminLevelTypes.AdminLevel, AdminLevelTypes.IsDistrict
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE AdminLevels.IsDeleted = 0
                    ", connection);
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new AdminLevelIndicators
                    {
                        Id = reader.GetValueOrDefault<int>("ID"),
                        ParentId = reader.GetValueOrDefault<Nullable<int>>("ParentId"),
                        Name = reader.GetValueOrDefault<string>("DisplayName"),
                        LevelNumber = reader.GetValueOrDefault<int>("AdminLevel"),
                        IsDistrict = reader.GetValueOrDefault<bool>("IsDistrict")
                    });
                }
                reader.Close();
            }
            return list;
        }

        private void AddIndicator(OleDbDataReader reader, Dictionary<int, AdminLevelIndicators> dic, Func<AggregateIndicator, object, object> customAggRule)
        {
            int indicatorId = reader.GetValueOrDefault<int>("iid");
            string key = reader.GetValueOrDefault<string>("iname") + indicatorId;
            int adminLevelId = reader.GetValueOrDefault<int>("aid");
            int indicatorDataType = reader.GetValueOrDefault<int>("DataTypeId");
            int indicatorAggType = reader.GetValueOrDefault<int>("AggTypeId");
            string indicatorValue = reader.GetValueOrDefault<string>("DynamicValue");

            if (dic.ContainsKey(adminLevelId))
            {
                var newIndicator =  new AggregateIndicator
                    {
                        IndicatorId = indicatorId,
                        DataType = indicatorDataType,
                        Value = indicatorValue,
                        AggType = indicatorAggType
                    };
                if (dic[adminLevelId].Indicators.ContainsKey(key))
                {
                    object val = IndicatorAggregator.Aggregate(newIndicator, dic[adminLevelId].Indicators[key].Value, customAggRule);
                    dic[adminLevelId].Indicators[key].Value = val == null ? "" : val.ToString();
                }
                else
                    dic[adminLevelId].Indicators.Add(key, newIndicator);
            }
        }
    }
}
