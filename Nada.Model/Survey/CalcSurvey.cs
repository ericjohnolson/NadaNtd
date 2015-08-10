using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Repositories;

namespace Nada.Model.Survey
{
    public class CalcSurvey : CalcBase, ICalcIndicators
    {
        public override List<KeyValuePair<string, string>> GetCalculatedValues(List<string> fields, Dictionary<string, string> relatedValues, int adminLevel, DateTime start, DateTime end)
        {
            string errors = "";
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();
            foreach (string field in fields)
                results.Add(GetCalculatedValue(field, relatedValues, null, start, end, ref errors));
            return results;
        }

        public override KeyValuePair<string, string> GetCalculatedValue(string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime start, DateTime end, ref string errors)
        {
            try
            {
                switch (field)
                {
                    case "SurLeishSurvey6aff65b1-ca6f-4bd8-9982-4f0527dd8a99":
                        return new KeyValuePair<string, string>(Translations._6aff65b1_ca6f_4bd8_9982_4f0527dd8a99, GetPercentage(GetValueOrDefault("SurLeishSurvey5f5b7326-b505-4321-90d8-ea69d6464801", relatedValues), GetValueOrDefault("SurLeishSurveye4ece583-91ce-4f0a-baf7-de45831c1135", relatedValues)));
                    case "SurLeishSurvey9dd22c4f-8130-4fbc-8251-607f65d3a7b2":
                        return new KeyValuePair<string, string>(Translations._9dd22c4f_8130_4fbc_8251_607f65d3a7b2, GetPercentage(GetValueOrDefault("SurLeishSurvey08bde675-17ca-4ed4-8dea-af284e15ba3d", relatedValues), GetValueOrDefault("SurLeishSurvey5f5b7326-b505-4321-90d8-ea69d6464801", relatedValues)));
                    case "SurLeishSurvey9ed458a6-2495-4ffb-adc0-1dfd5a2b6397":
                        return new KeyValuePair<string, string>(Translations._9ed458a6_2495_4ffb_adc0_1dfd5a2b6397, GetPercentage(GetValueOrDefault("SurLeishSurveyd61a5efa-4b2c-4f72-bf56-edab9025b6f2", relatedValues), GetValueOrDefault("SurLeishSurveya47f1af8-7399-4915-a541-ded4c2c9d739", relatedValues)));
                    case "SurLeishSurvey71bec938-836c-467e-8e58-5fab966b71ea":
                        return new KeyValuePair<string, string>(Translations._71bec938_836c_467e_8e58_5fab966b71ea, GetPercentage(GetValueOrDefault("SurLeishSurvey0d21bea5-9f29-4973-aa51-d3503bb284ac", relatedValues), GetValueOrDefault("SurLeishSurveyd61a5efa-4b2c-4f72-bf56-edab9025b6f2", relatedValues)));
                    case "SurLfSentinelSpotCheckSurveyLFSurPositive":
                        return new KeyValuePair<string, string>(Translations.LFSurPositive, GetPercentage(GetValueOrDefault("SurLfSentinelSpotCheckSurveyLFSurNumberOfIndividualsPositive", relatedValues), GetValueOrDefault("SurLfSentinelSpotCheckSurveyLFSurNumberOfIndividualsExamined", relatedValues)));
                    case "SurBuSurveyPercentNewCasesPcrCm":
                        return new KeyValuePair<string, string>(Translations.PercentNewCasesPcrCm, GetPercentage(GetValueOrDefault("SurBuSurveyNumCasesPcrCm", relatedValues), GetValueOrDefault("SurBuSurveyNumCasesDiagnosedCm", relatedValues)));
                    case "SurOnchoAssesmentsOnchoSurAttendanceRate":
                        return new KeyValuePair<string, string>(Translations.OnchoSurAttendanceRate, GetPercentage(GetValueOrDefault("SurOnchoAssesmentsOnchoSurNumberOfIndividualsExamined", relatedValues), GetValueOrDefault("SurOnchoAssesmentsOnchoSurRegistrationPopulation", relatedValues)));
                    case "SurOnchoAssesmentsOnchoSurPercentPositive":
                        return new KeyValuePair<string, string>(Translations.OnchoSurPercentPositive, GetPercentage(GetValueOrDefault("SurOnchoAssesmentsOnchoSurNumberOfIndividualsPositive", relatedValues), GetValueOrDefault("SurOnchoAssesmentsOnchoSurNumberOfIndividualsExamined", relatedValues)));
                    case "SurOnchoAssesmentsOnchoSurIfTestTypeIsNpPerDep":
                        return new KeyValuePair<string, string>(Translations.OnchoSurIfTestTypeIsNpPerDep, GetPercentage(GetValueOrDefault("SurOnchoAssesmentsOnchoSurIfTestTypeIsNpNumDep", relatedValues), GetValueOrDefault("SurOnchoAssesmentsOnchoSurNumberOfIndividualsExamined", relatedValues)));
                    case "SurOnchoAssesmentsOnchoSurIfTestTypeIsNpPerNod":
                        return new KeyValuePair<string, string>(Translations.OnchoSurIfTestTypeIsNpPerNod, GetPercentage(GetValueOrDefault("SurOnchoAssesmentsOnchoSurIfTestTypeIsNpNumNod", relatedValues), GetValueOrDefault("SurOnchoAssesmentsOnchoSurNumberOfIndividualsExamined", relatedValues)));
                    case "SurOnchoAssesmentsOnchoSurIfTestTypeIsNpPerWri":
                        return new KeyValuePair<string, string>(Translations.OnchoSurIfTestTypeIsNpPerWri, GetPercentage(GetValueOrDefault("SurOnchoAssesmentsOnchoSurIfTestTypeIsNpNumWri", relatedValues), GetValueOrDefault("SurOnchoAssesmentsOnchoSurNumberOfIndividualsExamined", relatedValues)));
                    case "SurSchSentinelSpotCheckSurveySCHSurPrevalenceOfIntestinalSchistosomeI":
                        return new KeyValuePair<string, string>(Translations.SCHSurPrevalenceOfIntestinalSchistosomeI, GetPercentage(GetValueOrDefault("SurSchSentinelSpotCheckSurveySCHSurNumberOfIndividualsPositiveForInte", relatedValues), GetValueOrDefault("SurSchSentinelSpotCheckSurveySCHSurNumberOfIndividualsExaminedForInte", relatedValues)));
                    case "SurSchSentinelSpotCheckSurveySCHSurPrevalenceOfAnyHaemuaturiaOrParasi":
                        return new KeyValuePair<string, string>(Translations.SCHSurPrevalenceOfAnyHaemuaturiaOrParasi, GetPercentage(GetValueOrDefault("SurSchSentinelSpotCheckSurveySCHSurNumberOfIndividualsPositiveForHaem", relatedValues), GetValueOrDefault("SurSchSentinelSpotCheckSurveySCHSurNumberOfIndividualsExaminedForUrin", relatedValues)));
                    case "SurSthSentinelSpotCheckSurveySTHSurPositiveOverall":
                        return new KeyValuePair<string, string>(Translations.STHSurPositiveOverall, GetPercentage(GetValueOrDefault("SurSthSentinelSpotCheckSurveySTHSurNumberOfIndividualsPositiveOverall", relatedValues), GetValueOrDefault("SurSthSentinelSpotCheckSurveySTHSurNumberOfIndividualsExaminedOverall", relatedValues)));
                    case "SurSthSentinelSpotCheckSurveySTHSurPositiveTrichuris":
                        return new KeyValuePair<string, string>(Translations.STHSurPositiveTrichuris, GetPercentage(GetValueOrDefault("SurSthSentinelSpotCheckSurveySTHSurNumberOfIndividualsPositiveTrichur", relatedValues), GetValueOrDefault("SurSthSentinelSpotCheckSurveySTHSurNumberOfIndividualsExaminedTrichur", relatedValues)));
                    case "SurSthSentinelSpotCheckSurveySTHSurPositiveHookworm":
                        return new KeyValuePair<string, string>(Translations.STHSurPositiveHookworm, GetPercentage(GetValueOrDefault("SurSthSentinelSpotCheckSurveySTHSurNumberOfIndividualsPositiveHookwor", relatedValues), GetValueOrDefault("SurSthSentinelSpotCheckSurveySTHSurNumberOfIndividualsExaminedHookwor", relatedValues)));
                    case "SurSthSentinelSpotCheckSurveySTHSurPositiveAscaris":
                        return new KeyValuePair<string, string>(Translations.STHSurPositiveAscaris, GetPercentage(GetValueOrDefault("SurSthSentinelSpotCheckSurveySTHSurNumberOfIndividualsPositiveAscaris", relatedValues), GetValueOrDefault("SurSthSentinelSpotCheckSurveySTHSurNumberOfIndividualsExaminedAscaris", relatedValues)));
                    case "SurImpactSurveyTraSurWithClinicalSignTf":
                        return new KeyValuePair<string, string>(Translations.TraSurWithClinicalSignTf, GetPercentage(GetValueOrDefault("SurImpactSurveyTraSurNumberOfIndividualsWithCSTF", relatedValues), GetValueOrDefault("SurImpactSurveyTraSurNumberOfIndividualsExaminedTf", relatedValues)));
                    case "SurImpactSurveyTraSurWithClinicalSignTi":
                        return new KeyValuePair<string, string>(Translations.TraSurWithClinicalSignTi, GetPercentage(GetValueOrDefault("SurImpactSurveyTraSurNumberOfIndividualsWithCSTI", relatedValues), GetValueOrDefault("SurImpactSurveyTraSurNumberOfIndividualsExaminedTi", relatedValues)));
                    case "SurImpactSurveyTraSurWithClinicalSignTs":
                        return new KeyValuePair<string, string>(Translations.TraSurWithClinicalSignTs, GetPercentage(GetValueOrDefault("SurImpactSurveyTraSurNumberOfIndividualsWithCSTS", relatedValues), GetValueOrDefault("SurImpactSurveyTraSurNumberOfIndividualsExaminedTs", relatedValues)));
                    case "SurImpactSurveyTraSurWithClinicalSignCo":
                        return new KeyValuePair<string, string>(Translations.TraSurWithClinicalSignCo, GetPercentage(GetValueOrDefault("SurImpactSurveyTraSurNumberOfIndividualsWithClinicalSig", relatedValues), GetValueOrDefault("SurImpactSurveyTraSurNumberOfIndividualsExaminedCo", relatedValues)));
                    case "SurImpactSurveyTraSurWithClinicalSignTt":
                        return new KeyValuePair<string, string>(Translations.TraSurWithClinicalSignTt, GetPercentage(GetValueOrDefault("SurImpactSurveyTraSurNumberOfIndividualsWithCSTT", relatedValues), GetValueOrDefault("SurImpactSurveyTraSurNumberOfIndividualsExaminedTt", relatedValues)));
                    case "SurTransAssessSurveyTASActualSampleSizeTotal":
                        return new KeyValuePair<string, string>(Translations.TASActualSampleSizeTotal, GetTotal(GetValueOrDefault("SurTransAssessSurveyTASActualSampleSizeNegative", relatedValues), GetValueOrDefault("SurTransAssessSurveyTASActualSampleSizePositive", relatedValues)));
                    case "SurLfMappingLFMapSurPositive":
                        return new KeyValuePair<string, string>(Translations.LFMapSurPositive, GetPercentage(GetValueOrDefault("SurLfMappingLFMapSurNumberOfIndividualsPositive", relatedValues), GetValueOrDefault("SurLfMappingLFMapSurNumberOfIndividualsExamined", relatedValues)));
                    case "SurOnchoMappingOnchoMapSurAttendanceRate":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurAttendanceRate, GetPercentage(GetValueOrDefault("SurOnchoMappingOnchoMapSurNumberOfIndividualsExamined", relatedValues), GetValueOrDefault("SurOnchoMappingOnchoMapSurRegistrationPopulation", relatedValues)));
                    case "SurOnchoMappingOnchoMapSurPercentPositive":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurPercentPositive, GetPercentage(GetValueOrDefault("SurOnchoMappingOnchoMapSurNumberOfIndividualsPositive", relatedValues), GetValueOrDefault("SurOnchoMappingOnchoMapSurNumberOfIndividualsExamined", relatedValues)));
                    case "SurOnchoMappingOnchoMapSurIfTestTypeIsNpPerDep":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurIfTestTypeIsNpPerDep, GetPercentage(GetValueOrDefault("SurOnchoMappingOnchoMapSurIfTestTypeIsNpNumDep", relatedValues), GetValueOrDefault("SurOnchoMappingOnchoMapSurNumberOfIndividualsExamined", relatedValues)));
                    case "SurOnchoMappingOnchoMapSurIfTestTypeIsNpPerNod":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurIfTestTypeIsNpPerNod, GetPercentage(GetValueOrDefault("SurOnchoMappingOnchoMapSurIfTestTypeIsNpNumNod", relatedValues), GetValueOrDefault("SurOnchoMappingOnchoMapSurNumberOfIndividualsExamined", relatedValues)));
                    case "SurOnchoMappingOnchoMapSurIfTestTypeIsNpPerWri":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurIfTestTypeIsNpPerWri, GetPercentage(GetValueOrDefault("SurOnchoMappingOnchoMapSurIfTestTypeIsNpNumWri", relatedValues), GetValueOrDefault("SurOnchoMappingOnchoMapSurNumberOfIndividualsExamined", relatedValues)));
                    case "SurSchMappingSCHMapSurPrevalenceOfAnyHaemuaturiaOrPar":
                        return new KeyValuePair<string, string>(Translations.SCHMapSurPrevalenceOfAnyHaemuaturiaOrPar, GetPercentage(GetValueOrDefault("SurSchMappingSCHMapSurNumberOfIndividualsPositiveForH", relatedValues), GetValueOrDefault("SurSchMappingSCHMapSurNumberOfIndividualsExaminedForU", relatedValues)));
                    case "SurSchMappingSCHMapSurPrevalenceOfIntestinalSchistoso":
                        return new KeyValuePair<string, string>(Translations.SCHMapSurPrevalenceOfIntestinalSchistoso, GetPercentage(GetValueOrDefault("SurSchMappingSCHMapSurNumberOfIndividualsPositiveForI", relatedValues), GetValueOrDefault("SurSchMappingSCHMapSurNumberOfIndividualsExaminedForI", relatedValues)));
                    case "SurSthMappingSTHMapSurSurPerPositiveOverall":
                        return new KeyValuePair<string, string>(Translations.STHMapSurSurPerPositiveOverall, GetPercentage(GetValueOrDefault("SurSthMappingSTHMapSurSurNumberOfIndividualsPositiveOverall", relatedValues), GetValueOrDefault("SurSthMappingSTHMapSurSurNumberOfIndividualsExaminedOverall", relatedValues)));
                    case "SurSthMappingSTHMapSurSurPerPositiveTrichuris":
                        return new KeyValuePair<string, string>(Translations.STHMapSurSurPerPositiveTrichuris, GetPercentage(GetValueOrDefault("SurSthMappingSTHMapSurSurNumberOfIndividualsPositiveTrichur", relatedValues), GetValueOrDefault("SurSthMappingSTHMapSurSurNumberOfIndividualsExaminedTrichur", relatedValues)));
                    case "SurSthMappingSTHMapSurSurPerPositiveHookworm":
                        return new KeyValuePair<string, string>(Translations.STHMapSurSurPerPositiveHookworm, GetPercentage(GetValueOrDefault("SurSthMappingSTHMapSurSurNumberOfIndividualsPositiveHookwor", relatedValues), GetValueOrDefault("SurSthMappingSTHMapSurSurNumberOfIndividualsExaminedHookwor", relatedValues)));
                    case "SurSthMappingSTHMapSurSurPerPositiveAscaris":
                        return new KeyValuePair<string, string>(Translations.STHMapSurSurPerPositiveAscaris, GetPercentage(GetValueOrDefault("SurSthMappingSTHMapSurSurNumberOfIndividualsPositiveAscaris", relatedValues), GetValueOrDefault("SurSthMappingSTHMapSurSurNumberOfIndividualsExaminedAscaris", relatedValues)));
                    case "SurTrachomaMappingTraMapSurPerWithClinicalSignTf":
                        return new KeyValuePair<string, string>(Translations.TraMapSurPerWithClinicalSignTf, GetPercentage(GetValueOrDefault("SurTrachomaMappingTraMapSurNumberOfIndividualsWithClTF", relatedValues), GetValueOrDefault("SurTrachomaMappingTraMapSurNumberOfIndividualsExaminedTf", relatedValues)));
                    case "SurTrachomaMappingTraMapSurPerWithClinicalSignTt":
                        return new KeyValuePair<string, string>(Translations.TraMapSurPerWithClinicalSignTt, GetPercentage(GetValueOrDefault("SurTrachomaMappingTraMapSurNumberOfIndividualsWithClTT", relatedValues), GetValueOrDefault("SurTrachomaMappingTraMapSurNumberOfIndividualsExaminedTt", relatedValues)));
                    case "SurTrachomaMappingTraMapSurPerWithClinicalSignTi":
                        return new KeyValuePair<string, string>(Translations.TraMapSurPerWithClinicalSignTi, GetPercentage(GetValueOrDefault("SurTrachomaMappingTraMapSurNumberOfIndividualsWithClTI", relatedValues), GetValueOrDefault("SurTrachomaMappingTraMapSurNumberOfIndividualsExaminedTi", relatedValues)));
                    case "SurTrachomaMappingTraMapSurPerWithClinicalSignTs":
                        return new KeyValuePair<string, string>(Translations.TraMapSurPerWithClinicalSignTs, GetPercentage(GetValueOrDefault("SurTrachomaMappingTraMapSurNumberOfIndividualsWithClTS", relatedValues), GetValueOrDefault("SurTrachomaMappingTraMapSurNumberOfIndividualsExaminedTs", relatedValues)));
                    case "SurTrachomaMappingTraMapSurPerWithClinicalSignCo":
                        return new KeyValuePair<string, string>(Translations.TraMapSurPerWithClinicalSignCo, GetPercentage(GetValueOrDefault("SurTrachomaMappingTraMapSurNumberOfIndividualsWithClinical", relatedValues), GetValueOrDefault("SurTrachomaMappingTraMapSurNumberOfIndividualsExamined", relatedValues)));
                    case "OnchoSurEntomologicalOnchoSurInfectionRate":
                        return new KeyValuePair<string, string>(Translations.OnchoSurInfectionRate, GetPercentage(GetValueOrDefault("OnchoSurEntomologicalOnchoSurParousFliesInfected", relatedValues), GetValueOrDefault("OnchoSurEntomologicalOnchoSurParousFliesDisected", relatedValues)));
                    case "OnchoSurEntomologicalOnchoSurParousRate":
                        return new KeyValuePair<string, string>(Translations.OnchoSurParousRate, GetPercentage(GetValueOrDefault("OnchoSurEntomologicalOnchoSurParousFlies", relatedValues), GetValueOrDefault("OnchoSurEntomologicalOnchoSurFliesDissected", relatedValues)));
                    case "OnchoSurEntomologicalOnchoSurInfectivityRate":
                        return new KeyValuePair<string, string>(Translations.OnchoSurInfectivityRate, GetPercentage(GetValueOrDefault("OnchoSurEntomologicalOnchoSurInfectiveFlies", relatedValues), GetValueOrDefault("OnchoSurEntomologicalOnchoSurNoFlies", relatedValues), 1000));
                    case "SurSchSentinelSpotCheckSurveySchSentinelPrevalenceEggsInUrine":
                        return new KeyValuePair<string, string>(Translations.SchSentinelPrevalenceEggsInUrine, GetPercentage(GetValueOrDefault("SurSchSentinelSpotCheckSurveySCHSurNumPosSchParasite", relatedValues), GetValueOrDefault("SurSchSentinelSpotCheckSurveySCHSurNumberOfIndividualsExaminedForUrin", relatedValues)));
                    case "SurSchMappingSchMappingPrevalenceEggsInUrine":
                        return new KeyValuePair<string, string>(Translations.SchMappingPrevalenceEggsInUrine, GetPercentage(GetValueOrDefault("SurSchMappingSCHMapSurNumPosSchParasite", relatedValues), GetValueOrDefault("SurSchMappingSCHMapSurNumberOfIndividualsExaminedForU", relatedValues)));
                    default:
                        return new KeyValuePair<string,string>(field, Translations.NA);
                }
            }
            catch (Exception)
            {
                return new KeyValuePair<string,string>(field, Translations.CalculationError);
            }
        }

    }
}
