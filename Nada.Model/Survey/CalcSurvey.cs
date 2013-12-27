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
        public override List<KeyValuePair<string, string>> GetCalculatedValues(List<string> fields, Dictionary<string, string> relatedValues, int adminLevel)
        {
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();

            foreach (string field in fields)
                results.Add(GetCalculatedValue(field, relatedValues, null));
            return results;
        }

        public override KeyValuePair<string, string> GetCalculatedValue(string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo)
        {
            try
            {
                switch (field)
                {
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
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurIfTestTypeIsNpPerDep, GetPercentage(GetValueOrDefault("19OnchoSurIfTestTypeIsNpPerDep", relatedValues), GetValueOrDefault("19OnchoMapSurNumberOfIndividualsExamined", relatedValues)));
                    case "19OnchoMapSurIfTestTypeIsNpPerNod":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurIfTestTypeIsNpPerNod, GetPercentage(GetValueOrDefault("19OnchoSurIfTestTypeIsNpPerNod", relatedValues), GetValueOrDefault("19OnchoMapSurNumberOfIndividualsExamined", relatedValues)));
                    case "19OnchoMapSurIfTestTypeIsNpPerWri":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurIfTestTypeIsNpPerWri, GetPercentage(GetValueOrDefault("19OnchoSurIfTestTypeIsNpPerWri", relatedValues), GetValueOrDefault("19OnchoMapSurNumberOfIndividualsExamined", relatedValues)));
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
