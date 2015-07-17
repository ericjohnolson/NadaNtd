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
                    case "76aff65b1-ca6f-4bd8-9982-4f0527dd8a99":
                        return new KeyValuePair<string, string>(Translations._6aff65b1_ca6f_4bd8_9982_4f0527dd8a99, GetPercentage(GetValueOrDefault("75f5b7326-b505-4321-90d8-ea69d6464801", relatedValues), GetValueOrDefault("7e4ece583-91ce-4f0a-baf7-de45831c1135", relatedValues)));
                    case "79dd22c4f-8130-4fbc-8251-607f65d3a7b2":
                        return new KeyValuePair<string, string>(Translations._9dd22c4f_8130_4fbc_8251_607f65d3a7b2, GetPercentage(GetValueOrDefault("708bde675-17ca-4ed4-8dea-af284e15ba3d", relatedValues), GetValueOrDefault("75f5b7326-b505-4321-90d8-ea69d6464801", relatedValues)));
                    case "79ed458a6-2495-4ffb-adc0-1dfd5a2b6397":
                        return new KeyValuePair<string, string>(Translations._9ed458a6_2495_4ffb_adc0_1dfd5a2b6397, GetPercentage(GetValueOrDefault("7d61a5efa-4b2c-4f72-bf56-edab9025b6f2", relatedValues), GetValueOrDefault("7a47f1af8-7399-4915-a541-ded4c2c9d739", relatedValues)));
                    case "771bec938-836c-467e-8e58-5fab966b71ea":
                        return new KeyValuePair<string, string>(Translations._71bec938_836c_467e_8e58_5fab966b71ea, GetPercentage(GetValueOrDefault("70d21bea5-9f29-4973-aa51-d3503bb284ac", relatedValues), GetValueOrDefault("7d61a5efa-4b2c-4f72-bf56-edab9025b6f2", relatedValues)));
                    case "10LFSurPositive":
                        return new KeyValuePair<string, string>(Translations.LFSurPositive, GetPercentage(GetValueOrDefault("10LFSurNumberOfIndividualsPositive", relatedValues), GetValueOrDefault("10LFSurNumberOfIndividualsExamined", relatedValues)));
                    case "8PercentNewCasesPcrCm":
                        return new KeyValuePair<string, string>(Translations.PercentNewCasesPcrCm, GetPercentage(GetValueOrDefault("8NumCasesPcrCm", relatedValues), GetValueOrDefault("8NumCasesDiagnosedCm", relatedValues)));
                    case "13OnchoSurAttendanceRate":
                        return new KeyValuePair<string, string>(Translations.OnchoSurAttendanceRate, GetPercentage(GetValueOrDefault("13OnchoSurNumberOfIndividualsExamined", relatedValues), GetValueOrDefault("13OnchoSurRegistrationPopulation", relatedValues)));
                    case "13OnchoSurPercentPositive":
                        return new KeyValuePair<string, string>(Translations.OnchoSurPercentPositive, GetPercentage(GetValueOrDefault("13OnchoSurNumberOfIndividualsPositive", relatedValues), GetValueOrDefault("13OnchoSurNumberOfIndividualsExamined", relatedValues)));
                    case "13OnchoSurIfTestTypeIsNpPerDep":
                        return new KeyValuePair<string, string>(Translations.OnchoSurIfTestTypeIsNpPerDep, GetPercentage(GetValueOrDefault("13OnchoSurIfTestTypeIsNpNumDep", relatedValues), GetValueOrDefault("13OnchoSurNumberOfIndividualsExamined", relatedValues)));
                    case "13OnchoSurIfTestTypeIsNpPerNod":
                        return new KeyValuePair<string, string>(Translations.OnchoSurIfTestTypeIsNpPerNod, GetPercentage(GetValueOrDefault("13OnchoSurIfTestTypeIsNpNumNod", relatedValues), GetValueOrDefault("13OnchoSurNumberOfIndividualsExamined", relatedValues)));
                    case "13OnchoSurIfTestTypeIsNpPerWri":
                        return new KeyValuePair<string, string>(Translations.OnchoSurIfTestTypeIsNpPerWri, GetPercentage(GetValueOrDefault("13OnchoSurIfTestTypeIsNpNumWri", relatedValues), GetValueOrDefault("13OnchoSurNumberOfIndividualsExamined", relatedValues)));
                    case "11SCHSurPrevalenceOfIntestinalSchistosomeI":
                        return new KeyValuePair<string, string>(Translations.SCHSurPrevalenceOfIntestinalSchistosomeI, GetPercentage(GetValueOrDefault("11SCHSurNumberOfIndividualsPositiveForInte", relatedValues), GetValueOrDefault("11SCHSurNumberOfIndividualsExaminedForInte", relatedValues)));
                    case "11SCHSurPrevalenceOfAnyHaemuaturiaOrParasi":
                        return new KeyValuePair<string, string>(Translations.SCHSurPrevalenceOfAnyHaemuaturiaOrParasi, GetPercentage(GetValueOrDefault("11SCHSurNumberOfIndividualsPositiveForHaem", relatedValues), GetValueOrDefault("11SCHSurNumberOfIndividualsExaminedForUrin", relatedValues)));
                    case "12STHSurPositiveOverall":
                        return new KeyValuePair<string, string>(Translations.STHSurPositiveOverall, GetPercentage(GetValueOrDefault("12STHSurNumberOfIndividualsPositiveOverall", relatedValues), GetValueOrDefault("12STHSurNumberOfIndividualsExaminedOverall", relatedValues)));
                    case "12STHSurPositiveTrichuris":
                        return new KeyValuePair<string, string>(Translations.STHSurPositiveTrichuris, GetPercentage(GetValueOrDefault("12STHSurNumberOfIndividualsPositiveTrichur", relatedValues), GetValueOrDefault("12STHSurNumberOfIndividualsExaminedTrichur", relatedValues)));
                    case "12STHSurPositiveHookworm":
                        return new KeyValuePair<string, string>(Translations.STHSurPositiveHookworm, GetPercentage(GetValueOrDefault("12STHSurNumberOfIndividualsPositiveHookwor", relatedValues), GetValueOrDefault("12STHSurNumberOfIndividualsExaminedHookwor", relatedValues)));
                    case "12STHSurPositiveAscaris":
                        return new KeyValuePair<string, string>(Translations.STHSurPositiveAscaris, GetPercentage(GetValueOrDefault("12STHSurNumberOfIndividualsPositiveAscaris", relatedValues), GetValueOrDefault("12STHSurNumberOfIndividualsExaminedAscaris", relatedValues)));
                    case "14TraSurWithClinicalSignTf":
                        return new KeyValuePair<string, string>(Translations.TraSurWithClinicalSignTf, GetPercentage(GetValueOrDefault("14TraSurNumberOfIndividualsWithCSTF", relatedValues), GetValueOrDefault("14TraSurNumberOfIndividualsExaminedTf", relatedValues)));
                    case "14TraSurWithClinicalSignTi":
                        return new KeyValuePair<string, string>(Translations.TraSurWithClinicalSignTi, GetPercentage(GetValueOrDefault("14TraSurNumberOfIndividualsWithCSTI", relatedValues), GetValueOrDefault("14TraSurNumberOfIndividualsExaminedTi", relatedValues)));
                    case "14TraSurWithClinicalSignTs":
                        return new KeyValuePair<string, string>(Translations.TraSurWithClinicalSignTs, GetPercentage(GetValueOrDefault("14TraSurNumberOfIndividualsWithCSTS", relatedValues), GetValueOrDefault("14TraSurNumberOfIndividualsExaminedTs", relatedValues)));
                    case "14TraSurWithClinicalSignCo":
                        return new KeyValuePair<string, string>(Translations.TraSurWithClinicalSignCo, GetPercentage(GetValueOrDefault("14TraSurNumberOfIndividualsWithClinicalSig", relatedValues), GetValueOrDefault("14TraSurNumberOfIndividualsExaminedCo", relatedValues)));
                    case "14TraSurWithClinicalSignTt":
                        return new KeyValuePair<string, string>(Translations.TraSurWithClinicalSignTt, GetPercentage(GetValueOrDefault("14TraSurNumberOfIndividualsWithCSTT", relatedValues), GetValueOrDefault("14TraSurNumberOfIndividualsExaminedTt", relatedValues)));
                    case "15TASActualSampleSizeTotal":
                        return new KeyValuePair<string, string>(Translations.TASActualSampleSizeTotal, GetTotal(GetValueOrDefault("15TASActualSampleSizeNegative", relatedValues), GetValueOrDefault("15TASActualSampleSizePositive", relatedValues)));
                    case "16LFMapSurPositive":
                        return new KeyValuePair<string, string>(Translations.LFMapSurPositive, GetPercentage(GetValueOrDefault("16LFMapSurNumberOfIndividualsPositive", relatedValues), GetValueOrDefault("16LFMapSurNumberOfIndividualsExamined", relatedValues)));
                    case "19OnchoMapSurAttendanceRate":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurAttendanceRate, GetPercentage(GetValueOrDefault("19OnchoMapSurNumberOfIndividualsExamined", relatedValues), GetValueOrDefault("19OnchoMapSurRegistrationPopulation", relatedValues)));
                    case "19OnchoMapSurPercentPositive":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurPercentPositive, GetPercentage(GetValueOrDefault("19OnchoMapSurNumberOfIndividualsPositive", relatedValues), GetValueOrDefault("19OnchoMapSurNumberOfIndividualsExamined", relatedValues)));
                    case "19OnchoMapSurIfTestTypeIsNpPerDep":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurIfTestTypeIsNpPerDep, GetPercentage(GetValueOrDefault("19OnchoMapSurIfTestTypeIsNpNumDep", relatedValues), GetValueOrDefault("19OnchoMapSurNumberOfIndividualsExamined", relatedValues)));
                    case "19OnchoMapSurIfTestTypeIsNpPerNod":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurIfTestTypeIsNpPerNod, GetPercentage(GetValueOrDefault("19OnchoMapSurIfTestTypeIsNpNumNod", relatedValues), GetValueOrDefault("19OnchoMapSurNumberOfIndividualsExamined", relatedValues)));
                    case "19OnchoMapSurIfTestTypeIsNpPerWri":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurIfTestTypeIsNpPerWri, GetPercentage(GetValueOrDefault("19OnchoMapSurIfTestTypeIsNpNumWri", relatedValues), GetValueOrDefault("19OnchoMapSurNumberOfIndividualsExamined", relatedValues)));
                    case "17SCHMapSurPrevalenceOfAnyHaemuaturiaOrPar":
                        return new KeyValuePair<string, string>(Translations.SCHMapSurPrevalenceOfAnyHaemuaturiaOrPar, GetPercentage(GetValueOrDefault("17SCHMapSurNumberOfIndividualsPositiveForH", relatedValues), GetValueOrDefault("17SCHMapSurNumberOfIndividualsExaminedForU", relatedValues)));
                    case "17SCHMapSurPrevalenceOfIntestinalSchistoso":
                        return new KeyValuePair<string, string>(Translations.SCHMapSurPrevalenceOfIntestinalSchistoso, GetPercentage(GetValueOrDefault("17SCHMapSurNumberOfIndividualsPositiveForI", relatedValues), GetValueOrDefault("17SCHMapSurNumberOfIndividualsExaminedForI", relatedValues)));
                    case "18STHMapSurSurPerPositiveOverall":
                        return new KeyValuePair<string, string>(Translations.STHMapSurSurPerPositiveOverall, GetPercentage(GetValueOrDefault("18STHMapSurSurNumberOfIndividualsPositiveOverall", relatedValues), GetValueOrDefault("18STHMapSurSurNumberOfIndividualsExaminedOverall", relatedValues)));
                    case "18STHMapSurSurPerPositiveTrichuris":
                        return new KeyValuePair<string, string>(Translations.STHMapSurSurPerPositiveTrichuris, GetPercentage(GetValueOrDefault("18STHMapSurSurNumberOfIndividualsPositiveTrichur", relatedValues), GetValueOrDefault("18STHMapSurSurNumberOfIndividualsExaminedTrichur", relatedValues)));
                    case "18STHMapSurSurPerPositiveHookworm":
                        return new KeyValuePair<string, string>(Translations.STHMapSurSurPerPositiveHookworm, GetPercentage(GetValueOrDefault("18STHMapSurSurNumberOfIndividualsPositiveHookwor", relatedValues), GetValueOrDefault("18STHMapSurSurNumberOfIndividualsExaminedHookwor", relatedValues)));
                    case "18STHMapSurSurPerPositiveAscaris":
                        return new KeyValuePair<string, string>(Translations.STHMapSurSurPerPositiveAscaris, GetPercentage(GetValueOrDefault("18STHMapSurSurNumberOfIndividualsPositiveAscaris", relatedValues), GetValueOrDefault("18STHMapSurSurNumberOfIndividualsExaminedAscaris", relatedValues)));
                    case "20TraMapSurPerWithClinicalSignTf":
                        return new KeyValuePair<string, string>(Translations.TraMapSurPerWithClinicalSignTf, GetPercentage(GetValueOrDefault("20TraMapSurNumberOfIndividualsWithClTF", relatedValues), GetValueOrDefault("20TraMapSurNumberOfIndividualsExaminedTf", relatedValues)));
                    case "20TraMapSurPerWithClinicalSignTt":
                        return new KeyValuePair<string, string>(Translations.TraMapSurPerWithClinicalSignTt, GetPercentage(GetValueOrDefault("20TraMapSurNumberOfIndividualsWithClTT", relatedValues), GetValueOrDefault("20TraMapSurNumberOfIndividualsExaminedTt", relatedValues)));
                    case "20TraMapSurPerWithClinicalSignTi":
                        return new KeyValuePair<string, string>(Translations.TraMapSurPerWithClinicalSignTi, GetPercentage(GetValueOrDefault("20TraMapSurNumberOfIndividualsWithClTI", relatedValues), GetValueOrDefault("20TraMapSurNumberOfIndividualsExaminedTi", relatedValues)));
                    case "20TraMapSurPerWithClinicalSignTs":
                        return new KeyValuePair<string, string>(Translations.TraMapSurPerWithClinicalSignTs, GetPercentage(GetValueOrDefault("20TraMapSurNumberOfIndividualsWithClTS", relatedValues), GetValueOrDefault("20TraMapSurNumberOfIndividualsExaminedTs", relatedValues)));
                    case "20TraMapSurPerWithClinicalSignCo":
                        return new KeyValuePair<string, string>(Translations.TraMapSurPerWithClinicalSignCo, GetPercentage(GetValueOrDefault("20TraMapSurNumberOfIndividualsWithClinical", relatedValues), GetValueOrDefault("20TraMapSurNumberOfIndividualsExamined", relatedValues)));
                    case "21OnchoSurInfectionRate":
                        return new KeyValuePair<string, string>(Translations.OnchoSurInfectionRate, GetPercentage(GetValueOrDefault("21OnchoSurParousFliesInfected", relatedValues), GetValueOrDefault("21OnchoSurParousFliesDisected", relatedValues)));
                    case "21OnchoSurParousRate":
                        return new KeyValuePair<string, string>(Translations.OnchoSurParousRate, GetPercentage(GetValueOrDefault("21OnchoSurParousFlies", relatedValues), GetValueOrDefault("21OnchoSurFliesDissected", relatedValues)));
                    case "21OnchoSurInfectivityRate":
                        return new KeyValuePair<string, string>(Translations.OnchoSurInfectivityRate, GetPercentage(GetValueOrDefault("21OnchoSurInfectiveFlies", relatedValues), GetValueOrDefault("21OnchoSurNoFlies", relatedValues), 1000));
                    case "11SchSentinelPrevalenceEggsInUrine":
                        return new KeyValuePair<string, string>(Translations.SchSentinelPrevalenceEggsInUrine, GetPercentage(GetValueOrDefault("11SCHSurNumPosSchParasite", relatedValues), GetValueOrDefault("11SCHSurNumberOfIndividualsExaminedForUrin", relatedValues)));
                    case "17SchMappingPrevalenceEggsInUrine":
                        return new KeyValuePair<string, string>(Translations.SchMappingPrevalenceEggsInUrine, GetPercentage(GetValueOrDefault("17SCHMapSurNumPosSchParasite", relatedValues), GetValueOrDefault("17SCHMapSurNumberOfIndividualsExaminedForU", relatedValues)));
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
