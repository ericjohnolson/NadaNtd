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
                results.Add(GetCalculatedValues(field, relatedValues));
            return results;
        }

        private KeyValuePair<string, string> GetCalculatedValues(string field, Dictionary<string, string> relatedValues)
        {
            try
            {
                switch (field)
                {
                    case "LFSurPositive":
                        return new KeyValuePair<string, string>(Translations.LFSurPositive, GetPercentage(relatedValues["LFSurNumberOfIndividualsPositive"], relatedValues["LFSurNumberOfIndividualsExamined"]));
                    case "PercentNewCasesPcrCm":
                        return new KeyValuePair<string, string>(Translations.PercentNewCasesPcrCm, GetPercentage(relatedValues["NumCasesPcrCm"], relatedValues["NumCasesDiagnosedCm"]));
                    case "OnchoSurAttendanceRate":
                        return new KeyValuePair<string, string>(Translations.OnchoSurAttendanceRate, GetPercentage(relatedValues["OnchoSurNumberOfIndividualsExamined"], relatedValues["OnchoSurRegistrationPopulation"]));
                    case "OnchoSurPercentPositive":
                        return new KeyValuePair<string, string>(Translations.OnchoSurPercentPositive, GetPercentage(relatedValues["OnchoSurNumberOfIndividualsPositive"], relatedValues["OnchoSurNumberOfIndividualsExamined"]));
                    case "OnchoSurIfTestTypeIsNpPerDep":
                        return new KeyValuePair<string, string>(Translations.OnchoSurIfTestTypeIsNpPerDep, GetPercentage(relatedValues["OnchoSurIfTestTypeIsNpNumDep"], relatedValues["OnchoSurNumberOfIndividualsExamined"]));
                    case "OnchoSurIfTestTypeIsNpPerNod":
                        return new KeyValuePair<string, string>(Translations.OnchoSurIfTestTypeIsNpPerNod, GetPercentage(relatedValues["OnchoSurIfTestTypeIsNpNumNod"], relatedValues["OnchoSurNumberOfIndividualsExamined"]));
                    case "OnchoSurIfTestTypeIsNpPerWri":
                        return new KeyValuePair<string, string>(Translations.OnchoSurIfTestTypeIsNpPerWri, GetPercentage(relatedValues["OnchoSurIfTestTypeIsNpNumWri"], relatedValues["OnchoSurNumberOfIndividualsExamined"]));
                    case "SCHSurPrevalenceOfIntestinalSchistosomeI":
                        return new KeyValuePair<string, string>(Translations.SCHSurPrevalenceOfIntestinalSchistosomeI, GetPercentage(relatedValues["SCHSurNumberOfIndividualsPositiveForInte"], relatedValues["SCHSurNumberOfIndividualsExaminedForInte"]));
                    case "SCHSurPrevalenceOfAnyHaemuaturiaOrParasi":
                        return new KeyValuePair<string, string>(Translations.SCHSurPrevalenceOfAnyHaemuaturiaOrParasi, GetPercentage(relatedValues["SCHSurNumberOfIndividualsPositiveForHaem"], relatedValues["SCHSurNumberOfIndividualsExaminedForUrin"]));
                    case "STHSurPositiveOverall":
                        return new KeyValuePair<string, string>(Translations.STHSurPositiveOverall, GetPercentage(relatedValues["STHSurNumberOfIndividualsPositiveOverall"], relatedValues["STHSurNumberOfIndividualsExaminedOverall"]));
                    case "STHSurPositiveTrichuris":
                        return new KeyValuePair<string, string>(Translations.STHSurPositiveTrichuris, GetPercentage(relatedValues["STHSurNumberOfIndividualsPositiveTrichur"], relatedValues["STHSurNumberOfIndividualsExaminedTrichur"]));
                    case "STHSurPositiveHookworm":
                        return new KeyValuePair<string, string>(Translations.STHSurPositiveHookworm, GetPercentage(relatedValues["STHSurNumberOfIndividualsPositiveHookwor"], relatedValues["STHSurNumberOfIndividualsExaminedHookwor"]));
                    case "STHSurPositiveAscaris":
                        return new KeyValuePair<string, string>(Translations.STHSurPositiveAscaris, GetPercentage(relatedValues["STHSurNumberOfIndividualsPositiveAscaris"], relatedValues["STHSurNumberOfIndividualsExaminedAscaris"]));
                    case "TraSurWithClinicalSignTf":
                        return new KeyValuePair<string, string>(Translations.TraSurWithClinicalSignTf, GetPercentage(relatedValues["TraSurNumberOfIndividualsWithCSTF"], relatedValues["TraSurNumberOfIndividualsExaminedTf"]));
                    case "TraSurWithClinicalSignTi":
                        return new KeyValuePair<string, string>(Translations.TraSurWithClinicalSignTi, GetPercentage(relatedValues["TraSurNumberOfIndividualsWithCSTI"], relatedValues["TraSurNumberOfIndividualsExaminedTi"]));
                    case "TraSurWithClinicalSignTs":
                        return new KeyValuePair<string, string>(Translations.TraSurWithClinicalSignTs, GetPercentage(relatedValues["TraSurNumberOfIndividualsWithCSTS"], relatedValues["TraSurNumberOfIndividualsExaminedTs"]));
                    case "TraSurWithClinicalSignCo":
                        return new KeyValuePair<string, string>(Translations.TraSurWithClinicalSignCo, GetPercentage(relatedValues["TraSurNumberOfIndividualsWithClinicalSig"], relatedValues["TraSurNumberOfIndividualsExaminedCo"]));
                    case "TraSurWithClinicalSignTt":
                        return new KeyValuePair<string, string>(Translations.TraSurWithClinicalSignTt, GetPercentage(relatedValues["TraSurNumberOfIndividualsWithCSTT"], relatedValues["TraSurNumberOfIndividualsExaminedTt"]));
                    case "TASActualSampleSizeTotal":
                        return new KeyValuePair<string, string>(Translations.TASActualSampleSizeTotal, GetTotal(relatedValues["TASActualSampleSizeNegative"], relatedValues["TASActualSampleSizePositive"]));
                    case "LFMapSurPositive":
                        return new KeyValuePair<string, string>(Translations.LFMapSurPositive, GetPercentage(relatedValues["LFMapSurNumberOfIndividualsPositive"], relatedValues["LFMapSurNumberOfIndividualsExamined"]));
                    case "OnchoMapSurAttendanceRate":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurAttendanceRate, GetPercentage(relatedValues["OnchoMapSurNumberOfIndividualsExamined"], relatedValues["OnchoMapSurRegistrationPopulation"]));
                    case "OnchoMapSurPercentPositive":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurPercentPositive, GetPercentage(relatedValues["OnchoMapSurNumberOfIndividualsPositive"], relatedValues["OnchoMapSurNumberOfIndividualsExamined"]));
                    case "OnchoMapSurIfTestTypeIsNpPerDep":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurIfTestTypeIsNpPerDep, GetPercentage(relatedValues["OnchoSurIfTestTypeIsNpPerDep"], relatedValues["OnchoMapSurNumberOfIndividualsExamined"]));
                    case "OnchoMapSurIfTestTypeIsNpPerNod":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurIfTestTypeIsNpPerNod, GetPercentage(relatedValues["OnchoSurIfTestTypeIsNpPerNod"], relatedValues["OnchoMapSurNumberOfIndividualsExamined"]));
                    case "OnchoMapSurIfTestTypeIsNpPerWri":
                        return new KeyValuePair<string, string>(Translations.OnchoMapSurIfTestTypeIsNpPerWri, GetPercentage(relatedValues["OnchoSurIfTestTypeIsNpPerWri"], relatedValues["OnchoMapSurNumberOfIndividualsExamined"]));
                    case "SCHMapSurPrevalenceOfAnyHaemuaturiaOrPar":
                        return new KeyValuePair<string, string>(Translations.SCHMapSurPrevalenceOfAnyHaemuaturiaOrPar, GetPercentage(relatedValues["SCHMapSurNumberOfIndividualsPositiveForH"], relatedValues["SCHMapSurNumberOfIndividualsExaminedForU"]));
                    case "SCHMapSurPrevalenceOfIntestinalSchistoso":
                        return new KeyValuePair<string, string>(Translations.SCHMapSurPrevalenceOfIntestinalSchistoso, GetPercentage(relatedValues["SCHMapSurNumberOfIndividualsPositiveForI"], relatedValues["SCHMapSurNumberOfIndividualsExaminedForI"]));
                    case "STHMapSurSurPerPositiveOverall":
                        return new KeyValuePair<string, string>(Translations.STHMapSurSurPerPositiveOverall, GetPercentage(relatedValues["STHMapSurSurNumberOfIndividualsPositiveOverall"], relatedValues["STHMapSurSurNumberOfIndividualsExaminedOverall"]));
                    case "STHMapSurSurPerPositiveTrichuris":
                        return new KeyValuePair<string, string>(Translations.STHMapSurSurPerPositiveTrichuris, GetPercentage(relatedValues["STHMapSurSurNumberOfIndividualsPositiveTrichur"], relatedValues["STHMapSurSurNumberOfIndividualsExaminedTrichur"]));
                    case "STHMapSurSurPerPositiveHookworm":
                        return new KeyValuePair<string, string>(Translations.STHMapSurSurPerPositiveHookworm, GetPercentage(relatedValues["STHMapSurSurNumberOfIndividualsPositiveHookwor"], relatedValues["STHMapSurSurNumberOfIndividualsExaminedHookwor"]));
                    case "STHMapSurSurPerPositiveAscaris":
                        return new KeyValuePair<string, string>(Translations.STHMapSurSurPerPositiveAscaris, GetPercentage(relatedValues["STHMapSurSurNumberOfIndividualsPositiveAscaris"], relatedValues["STHMapSurSurNumberOfIndividualsExaminedAscaris"]));
                    case "TraMapSurPerWithClinicalSignTf":
                        return new KeyValuePair<string, string>(Translations.TraMapSurPerWithClinicalSignTf, GetPercentage(relatedValues["TraMapSurNumberOfIndividualsWithClTF"], relatedValues["TraMapSurNumberOfIndividualsExaminedTf"]));
                    case "TraMapSurPerWithClinicalSignTt":
                        return new KeyValuePair<string, string>(Translations.TraMapSurPerWithClinicalSignTt, GetPercentage(relatedValues["TraMapSurNumberOfIndividualsWithClTT"], relatedValues["TraMapSurNumberOfIndividualsExaminedTt"]));
                    case "TraMapSurPerWithClinicalSignTi":
                        return new KeyValuePair<string, string>(Translations.TraMapSurPerWithClinicalSignTi, GetPercentage(relatedValues["TraMapSurNumberOfIndividualsWithClTI"], relatedValues["TraMapSurNumberOfIndividualsExaminedTi"]));
                    case "TraMapSurPerWithClinicalSignTs":
                        return new KeyValuePair<string, string>(Translations.TraMapSurPerWithClinicalSignTs, GetPercentage(relatedValues["TraMapSurNumberOfIndividualsWithClTS"], relatedValues["TraMapSurNumberOfIndividualsExaminedTs"]));
                    case "TraMapSurPerWithClinicalSignCo":
                        return new KeyValuePair<string, string>(Translations.TraMapSurPerWithClinicalSignCo, GetPercentage(relatedValues["TraMapSurNumberOfIndividualsWithClinical"], relatedValues["TraMapSurNumberOfIndividualsExamined"]));
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
