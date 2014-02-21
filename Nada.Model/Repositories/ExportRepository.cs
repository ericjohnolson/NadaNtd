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
using Nada.Model.Exports;
using Nada.Model.Intervention;

namespace Nada.Model.Repositories
{

    public class ExportRepository
    {
        public List<AdminLevelIndicators> GetDistrictIndicatorTrees(int interventionTypeId, int year, int diseaseId, Func<AggregateIndicator, object, object> customAggRule)
        {
            List<AdminLevelIndicators> list = new List<AdminLevelIndicators>();
            Dictionary<int, AdminLevelIndicators> dic = new Dictionary<int, AdminLevelIndicators>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
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
                        AND InterventionIndicatorValues.DynamicValue like @YearReporting and Interventions.IsDeleted = 0", connection);
            command.Parameters.Add(new OleDbParameter("@InterventionTypeId", interventionTypeId));
            command.Parameters.Add(new OleDbParameter("@YearName", "DateReported"));
            command.Parameters.Add(new OleDbParameter("@YearReporting", "%" + year));
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
                        AND SurveyIndicatorValues.DynamicValue like @YearReporting and Surveys.IsDeleted = 0", connection);
            command.Parameters.Add(new OleDbParameter("@DiseaseId", diseaseId));
            command.Parameters.Add(new OleDbParameter("@YearName", "DateReported"));
            command.Parameters.Add(new OleDbParameter("@YearReporting", "%" + year));
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
                            (((((Surveys INNER JOIN SurveyIndicatorValues on SurveyIndicatorValues.SurveyId = Surveys.ID)
                            INNER JOIN SurveyIndicators on SurveyIndicators.ID = SurveyIndicatorValues.IndicatorId)
                            INNER JOIN Surveys_to_adminlevels on Surveys_to_AdminLevels.SurveyId = Surveys.ID)
                            INNER JOIN AdminLevels on Surveys_to_adminlevels.AdminLevelId = AdminLevels.ID)
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
                        AND DiseaseDistributionIndicatorValues.DynamicValue like @YearReporting and DiseaseDistributions.IsDeleted = 0", connection);
            command.Parameters.Add(new OleDbParameter("@DiseaseId", diseaseId));
            command.Parameters.Add(new OleDbParameter("@YearName", "DateReported"));
            command.Parameters.Add(new OleDbParameter("@YearReporting", "%" + year));
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

        public static List<AdminLevelIndicators> GetAdminLevels(OleDbCommand command, OleDbConnection connection)
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
                var newIndicator = new AggregateIndicator
                    {
                        IndicatorId = indicatorId,
                        DataType = indicatorDataType,
                        Value = indicatorValue,
                        AggType = indicatorAggType
                    };
                if (dic[adminLevelId].Indicators.ContainsKey(key))
                {
                    object val = IndicatorAggregator.Aggregate(newIndicator, dic[adminLevelId].Indicators[key].Value);
                    dic[adminLevelId].Indicators[key].Value = val == null ? "" : val.ToString();
                }
                else
                    dic[adminLevelId].Indicators.Add(key, newIndicator);
            }
        }

        public ExportJrfQuestions GetExportQuestions()
        {
            ExportJrfQuestions questions = new ExportJrfQuestions();

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command = new OleDbCommand(@"Select ID, 
                    JrfYearReporting,
                    JrfEndemicLf,    
                    JrfEndemicOncho, 
                    JrfEndemicSth,   
                    JrfEndemicSch
                    FROM ExportJrfQuestions 
                    WHERE ID = 1
                    ", connection);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        questions.Id = reader.GetValueOrDefault<int>("ID");
                        questions.JrfYearReporting = reader.GetValueOrDefault<int>("JrfYearReporting");
                        questions.JrfEndemicLf = reader.GetValueOrDefault<string>("JrfEndemicLf");
                        questions.JrfEndemicOncho = reader.GetValueOrDefault<string>("JrfEndemicOncho");
                        questions.JrfEndemicSth = reader.GetValueOrDefault<string>("JrfEndemicSth");
                        questions.JrfEndemicSch = reader.GetValueOrDefault<string>("JrfEndemicSch");
                    }
                    reader.Close();
                }
            }
            return questions;
        }


        public void UpdateExportQuestions(ExportJrfQuestions questions)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Update 
                        ExportJrfQuestions
                        SET
                        JrfYearReporting=@JrfYearReporting,
                        JrfEndemicLf=@JrfEndemicLf,    
                        JrfEndemicOncho=@JrfEndemicOncho, 
                        JrfEndemicSth=@JrfEndemicSth,   
                        JrfEndemicSch=@JrfEndemicSch
                        WHERE ID = @id", connection);
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@JrfYearReporting", questions.JrfYearReporting));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@JrfEndemicLf", questions.JrfEndemicLf));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@JrfEndemicOncho", questions.JrfEndemicOncho));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@JrfEndemicSth", questions.JrfEndemicSth));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@JrfEndemicSch", questions.JrfEndemicSch));
                    command.Parameters.Add(new OleDbParameter("@id", questions.Id));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public ExportCmJrfQuestions GetCmExportQuestions()
        {
            ExportCmJrfQuestions questions = new ExportCmJrfQuestions();

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command = new OleDbCommand(@"Select ID, 
                    YearReporting
                    ,CmHaveMasterPlan
                    ,CmYearsMasterPlan
                    ,CmBuget
                    ,CmPercentFunded
                    ,CmHaveAnnualOpPlan
                    ,CmDiseaseSpecOrNtdIntegrated
                    ,CmBuHasPlan
                    ,CmGwHasPlan
                    ,CmHatHasPlan
                    ,CmLeishHasPlan
                    ,CmLeprosyHasPlan
                    ,CmYawsHasPlan
                    ,CmAnySupplyFunds
                    ,CmHasStorage
                    ,CmStorageNtdOrCombined
                    ,CmStorageSponsor1
                    ,CmStorageSponsor2
                    ,CmStorageSponsor3
                    ,CmStorageSponsor4
                    ,CmHasTaskForce
                    ,CmHasMoh
                    ,CmHasMosw
                    ,CmHasMot
                    ,CmHasMoe
                    ,CmHasMoc
                    ,CmHasUni
                    ,CmHasNgo
                    ,CmHasAnnualForum
                    ,CmForumHasRegions
                    ,CmForumHasTaskForce
                    ,CmHasNtdReviewMeetings
                    ,CmHasDiseaseSpecMeetings
                    ,CmHasGwMeeting
                    ,CmHasLeprosyMeeting
                    ,CmHasHatMeeting
                    ,CmHasLeishMeeting
                    ,CmHasBuMeeting
                    ,CmHasYawsMeeting
                    ,CmHasWeeklyMech
                    ,CmHasMonthlyMech
                    ,CmHasQuarterlyMech
                    ,CmHasSemesterMech
                    ,CmOtherMechs
                    FROM ExportCmJrfQuestions 
                    WHERE ID = 1
                    ", connection);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        questions.Id = reader.GetValueOrDefault<int>("ID");
                        questions.YearReporting = reader.GetValueOrDefault<Nullable<int>>("YearReporting");
                        questions.CmHaveMasterPlan = reader.GetValueOrDefault<bool>("CmHaveMasterPlan");
                        questions.CmYearsMasterPlan = reader.GetValueOrDefault<string>("CmYearsMasterPlan");
                        questions.CmBuget = reader.GetValueOrDefault<Nullable<int>>("CmBuget");
                        questions.CmPercentFunded = reader.GetNullableDouble("CmPercentFunded");
                        questions.CmHaveAnnualOpPlan = reader.GetValueOrDefault<bool>("CmHaveAnnualOpPlan");
                        questions.CmDiseaseSpecOrNtdIntegrated = reader.GetValueOrDefault<string>("CmDiseaseSpecOrNtdIntegrated");
                        questions.CmBuHasPlan = reader.GetValueOrDefault<bool>("CmBuHasPlan");
                        questions.CmGwHasPlan = reader.GetValueOrDefault<bool>("CmGwHasPlan");
                        questions.CmHatHasPlan = reader.GetValueOrDefault<bool>("CmHatHasPlan");
                        questions.CmLeishHasPlan = reader.GetValueOrDefault<bool>("CmLeishHasPlan");
                        questions.CmLeprosyHasPlan = reader.GetValueOrDefault<bool>("CmLeprosyHasPlan");
                        questions.CmYawsHasPlan = reader.GetValueOrDefault<bool>("CmYawsHasPlan");
                        questions.CmAnySupplyFunds = reader.GetValueOrDefault<bool>("CmAnySupplyFunds");
                        questions.CmHasStorage = reader.GetValueOrDefault<bool>("CmHasStorage");
                        questions.CmStorageNtdOrCombined = reader.GetValueOrDefault<string>("CmStorageNtdOrCombined");
                        questions.CmStorageSponsor1 = reader.GetValueOrDefault<string>("CmStorageSponsor1");
                        questions.CmStorageSponsor2 = reader.GetValueOrDefault<string>("CmStorageSponsor2");
                        questions.CmStorageSponsor3 = reader.GetValueOrDefault<string>("CmStorageSponsor3");
                        questions.CmStorageSponsor4 = reader.GetValueOrDefault<string>("CmStorageSponsor4");
                        questions.CmHasTaskForce = reader.GetValueOrDefault<bool>("CmHasTaskForce");
                        questions.CmHasMoh = reader.GetValueOrDefault<bool>("CmHasMoh");
                        questions.CmHasMosw = reader.GetValueOrDefault<bool>("CmHasMosw");
                        questions.CmHasMot = reader.GetValueOrDefault<bool>("CmHasMot");
                        questions.CmHasMoe = reader.GetValueOrDefault<bool>("CmHasMoe");
                        questions.CmHasMoc = reader.GetValueOrDefault<bool>("CmHasMoc");
                        questions.CmHasUni = reader.GetValueOrDefault<bool>("CmHasUni");
                        questions.CmHasNgo = reader.GetValueOrDefault<bool>("CmHasNgo");
                        questions.CmHasAnnualForum = reader.GetValueOrDefault<bool>("CmHasAnnualForum");
                        questions.CmForumHasRegions = reader.GetValueOrDefault<bool>("CmForumHasRegions");
                        questions.CmForumHasTaskForce = reader.GetValueOrDefault<bool>("CmForumHasTaskForce");
                        questions.CmHasNtdReviewMeetings = reader.GetValueOrDefault<bool>("CmHasNtdReviewMeetings");
                        questions.CmHasDiseaseSpecMeetings = reader.GetValueOrDefault<bool>("CmHasDiseaseSpecMeetings");
                        questions.CmHasGwMeeting = reader.GetValueOrDefault<bool>("CmHasGwMeeting");
                        questions.CmHasLeprosyMeeting = reader.GetValueOrDefault<bool>("CmHasLeprosyMeeting");
                        questions.CmHasHatMeeting = reader.GetValueOrDefault<bool>("CmHasHatMeeting");
                        questions.CmHasLeishMeeting = reader.GetValueOrDefault<bool>("CmHasLeishMeeting");
                        questions.CmHasBuMeeting = reader.GetValueOrDefault<bool>("CmHasBuMeeting");
                        questions.CmHasYawsMeeting = reader.GetValueOrDefault<bool>("CmHasYawsMeeting");
                        questions.CmHasWeeklyMech = reader.GetValueOrDefault<bool>("CmHasWeeklyMech");
                        questions.CmHasMonthlyMech = reader.GetValueOrDefault<bool>("CmHasMonthlyMech");
                        questions.CmHasQuarterlyMech = reader.GetValueOrDefault<bool>("CmHasQuarterlyMech");
                        questions.CmHasSemesterMech = reader.GetValueOrDefault<bool>("CmHasSemesterMech");
                        questions.CmOtherMechs = reader.GetValueOrDefault<string>("CmOtherMechs");
                    }
                    reader.Close();
                }

                command = new OleDbCommand(@"Select CmContactName, CmContactPost, CmContactTele, CmContactEmail
                                From ExportContacts", connection);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        questions.Contacts.Add(new ExportContact
                        {
                            CmContactName = reader.GetValueOrDefault<string>("CmContactName"),
                            CmContactEmail = reader.GetValueOrDefault<string>("CmContactEmail"),
                            CmContactPost = reader.GetValueOrDefault<string>("CmContactPost"),
                            CmContactTele = reader.GetValueOrDefault<string>("CmContactTele")
                        });
                    }
                    reader.Close();
                }
            }
            return questions;
        }

        public void UpdateCmExportQuestions(ExportCmJrfQuestions questions)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Update 
                        ExportCmJrfQuestions
                        SET
                        YearReporting=@YearReporting
                            ,CmHaveMasterPlan=@CmHaveMasterPlan
                            ,CmYearsMasterPlan=@CmYearsMasterPlan
                            ,CmBuget=@CmBuget
                            ,CmPercentFunded=@CmPercentFunded
                            ,CmHaveAnnualOpPlan=@CmHaveAnnualOpPlan
                            ,CmDiseaseSpecOrNtdIntegrated=@CmDiseaseSpecOrNtdIntegrated
                            ,CmBuHasPlan=@CmBuHasPlan
                            ,CmGwHasPlan=@CmGwHasPlan
                            ,CmHatHasPlan=@CmHatHasPlan
                            ,CmLeishHasPlan=@CmLeishHasPlan
                            ,CmLeprosyHasPlan=@CmLeprosyHasPlan
                            ,CmYawsHasPlan=@CmYawsHasPlan
                            ,CmAnySupplyFunds=@CmAnySupplyFunds
                            ,CmHasStorage=@CmHasStorage
                            ,CmStorageNtdOrCombined=@CmStorageNtdOrCombined
                            ,CmStorageSponsor1=@CmStorageSponsor1
                            ,CmStorageSponsor2=@CmStorageSponsor2
                            ,CmStorageSponsor3=@CmStorageSponsor3
                            ,CmStorageSponsor4=@CmStorageSponsor4
                            ,CmHasTaskForce=@CmHasTaskForce
                            ,CmHasMoh=@CmHasMoh
                            ,CmHasMosw=@CmHasMosw
                            ,CmHasMot=@CmHasMot
                            ,CmHasMoe=@CmHasMoe
                            ,CmHasMoc=@CmHasMoc
                            ,CmHasUni=@CmHasUni
                            ,CmHasNgo=@CmHasNgo
                            ,CmHasAnnualForum=@CmHasAnnualForum
                            ,CmForumHasRegions=@CmForumHasRegions
                            ,CmForumHasTaskForce=@CmForumHasTaskForce
                            ,CmHasNtdReviewMeetings=@CmHasNtdReviewMeetings
                            ,CmHasDiseaseSpecMeetings=@CmHasDiseaseSpecMeetings
                            ,CmHasGwMeeting=@CmHasGwMeeting
                            ,CmHasLeprosyMeeting=@CmHasLeprosyMeeting
                            ,CmHasHatMeeting=@CmHasHatMeeting
                            ,CmHasLeishMeeting=@CmHasLeishMeeting
                            ,CmHasBuMeeting=@CmHasBuMeeting
                            ,CmHasYawsMeeting=@CmHasYawsMeeting
                            ,CmHasWeeklyMech=@CmHasWeeklyMech
                            ,CmHasMonthlyMech=@CmHasMonthlyMech
                            ,CmHasQuarterlyMech=@CmHasQuarterlyMech
                            ,CmHasSemesterMech=@CmHasSemesterMech
                            ,CmOtherMechs=@CmOtherMechs               
                        WHERE ID = @id", connection);

                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@YearReporting", questions.YearReporting));
                    command.Parameters.Add(new OleDbParameter("@CmHaveMasterPlan", questions.CmHaveMasterPlan));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@CmYearsMasterPlan", questions.CmYearsMasterPlan));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@CmBuget", questions.CmBuget));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@CmPercentFunded", questions.CmPercentFunded));
                    command.Parameters.Add(new OleDbParameter("@CmHaveAnnualOpPlan", questions.CmHaveAnnualOpPlan));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@CmDiseaseSpecOrNtdIntegrated", questions.CmDiseaseSpecOrNtdIntegrated));
                    command.Parameters.Add(new OleDbParameter("@CmBuHasPlan", questions.CmBuHasPlan));
                    command.Parameters.Add(new OleDbParameter("@CmGwHasPlan", questions.CmGwHasPlan));
                    command.Parameters.Add(new OleDbParameter("@CmHatHasPlan", questions.CmHatHasPlan));
                    command.Parameters.Add(new OleDbParameter("@CmLeishHasPlan", questions.CmLeishHasPlan));
                    command.Parameters.Add(new OleDbParameter("@CmLeprosyHasPlan", questions.CmLeprosyHasPlan));
                    command.Parameters.Add(new OleDbParameter("@CmYawsHasPlan", questions.CmYawsHasPlan));
                    command.Parameters.Add(new OleDbParameter("@CmAnySupplyFunds", questions.CmAnySupplyFunds));
                    command.Parameters.Add(new OleDbParameter("@CmHasStorage", questions.CmHasStorage));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@CmStorageNtdOrCombined", questions.CmStorageNtdOrCombined));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@CmStorageSponsor1", questions.CmStorageSponsor1));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@CmStorageSponsor2", questions.CmStorageSponsor2));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@CmStorageSponsor3", questions.CmStorageSponsor3));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@CmStorageSponsor4", questions.CmStorageSponsor4));
                    command.Parameters.Add(new OleDbParameter("@CmHasTaskForce", questions.CmHasTaskForce));
                    command.Parameters.Add(new OleDbParameter("@CmHasMoh", questions.CmHasMoh));
                    command.Parameters.Add(new OleDbParameter("@CmHasMosw", questions.CmHasMosw));
                    command.Parameters.Add(new OleDbParameter("@CmHasMot", questions.CmHasMot));
                    command.Parameters.Add(new OleDbParameter("@CmHasMoe", questions.CmHasMoe));
                    command.Parameters.Add(new OleDbParameter("@CmHasMoc", questions.CmHasMoc));
                    command.Parameters.Add(new OleDbParameter("@CmHasUni", questions.CmHasUni));
                    command.Parameters.Add(new OleDbParameter("@CmHasNgo", questions.CmHasNgo));
                    command.Parameters.Add(new OleDbParameter("@CmHasAnnualForum", questions.CmHasAnnualForum));
                    command.Parameters.Add(new OleDbParameter("@CmForumHasRegions", questions.CmForumHasRegions));
                    command.Parameters.Add(new OleDbParameter("@CmForumHasTaskForce", questions.CmForumHasTaskForce));
                    command.Parameters.Add(new OleDbParameter("@CmHasNtdReviewMeetings", questions.CmHasNtdReviewMeetings));
                    command.Parameters.Add(new OleDbParameter("@CmHasDiseaseSpecMeetings", questions.CmHasDiseaseSpecMeetings));
                    command.Parameters.Add(new OleDbParameter("@CmHasGwMeeting", questions.CmHasGwMeeting));
                    command.Parameters.Add(new OleDbParameter("@CmHasLeprosyMeeting", questions.CmHasLeprosyMeeting));
                    command.Parameters.Add(new OleDbParameter("@CmHasHatMeeting", questions.CmHasHatMeeting));
                    command.Parameters.Add(new OleDbParameter("@CmHasLeishMeeting", questions.CmHasLeishMeeting));
                    command.Parameters.Add(new OleDbParameter("@CmHasBuMeeting", questions.CmHasBuMeeting));
                    command.Parameters.Add(new OleDbParameter("@CmHasYawsMeeting", questions.CmHasYawsMeeting));
                    command.Parameters.Add(new OleDbParameter("@CmHasWeeklyMech", questions.CmHasWeeklyMech));
                    command.Parameters.Add(new OleDbParameter("@CmHasMonthlyMech", questions.CmHasMonthlyMech));
                    command.Parameters.Add(new OleDbParameter("@CmHasQuarterlyMech", questions.CmHasQuarterlyMech));
                    command.Parameters.Add(new OleDbParameter("@CmHasSemesterMech", questions.CmHasSemesterMech));
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@CmOtherMechs", questions.CmOtherMechs));              
                    command.Parameters.Add(new OleDbParameter("@id", questions.Id));
                    command.ExecuteNonQuery();

                    command = new OleDbCommand(@"DELETE From ExportContacts", connection);
                    command.ExecuteNonQuery();

                    foreach (var contact in questions.Contacts)
                    {
                        command = new OleDbCommand(@"INSERT INTO ExportContacts (CmContactName, CmContactPost, CmContactTele, CmContactEmail) values
                                    (@CmContactName, @CmContactPost, @CmContactTele, @CmContactEmail)", connection);
                        command.Parameters.Add(OleDbUtil.CreateNullableParam("@CmContactName", contact.CmContactName));
                        command.Parameters.Add(OleDbUtil.CreateNullableParam("@CmContactPost", contact.CmContactPost));
                        command.Parameters.Add(OleDbUtil.CreateNullableParam("@CmContactTele", contact.CmContactTele));
                        command.Parameters.Add(OleDbUtil.CreateNullableParam("@CmContactEmail", contact.CmContactEmail));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
