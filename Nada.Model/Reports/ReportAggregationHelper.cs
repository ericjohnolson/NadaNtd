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
    public static class ReportAggregationHelper
    {
        public static void IndicatorListToTree(List<AdminLevelIndicators> list, Dictionary<int, AdminLevelIndicators> dic)
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

        public static void AddRelatedCalcIndicators(ReportOptions options, Dictionary<int, AdminLevelIndicators> dic, OleDbCommand command,
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
            + " (" + String.Join(", ", options.SelectedIndicators.Select(s => s.ID.ToString()).ToArray()) + ")  AND IndicatorCalculations.EntityTypeId = " 
            + entityTypeId;

            repo.AddIndicatorsToAggregate(intv, options, dic, command, connection, getAggKey, getName,getType, sind, true);

            string dd = @"Select 
                        AdminLevels.ID as AID, 
                        AdminLevels.DisplayName,
                        DiseaseDistributions.ID, 
                        DiseaseDistributions.YearReported, 
                        Diseases.DisplayName as TName,       
                        Diseases.ID as Tid,   
                        DiseaseDistributionIndicators.ID as IndicatorId, 
                        DiseaseDistributionIndicators.DisplayName as IndicatorName, 
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
            + entityTypeId;

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
            + entityTypeId;

            repo.AddIndicatorsToAggregate(survey, options, dic, command, connection, getAggKey, getName, getType, sind, true); 
        }
    }
}
