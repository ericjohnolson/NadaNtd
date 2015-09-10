using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Repositories;

namespace Nada.Model.Intervention
{
    public class CalcIntv : CalcBase, ICalcIndicators
    {
        public override List<KeyValuePair<string, string>> GetMetaData(List<KeyValuePair<string, string>> fields, int adminLevel, DateTime start, DateTime end)
        {
            string errors = "";
            var relatedValues = new Dictionary<string, string>();
            AdminLevelDemography recentDemo = GetDemography(adminLevel, start, end);
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();
            foreach (KeyValuePair<string, string> field in fields) // Key is form translation key, value is indicator name
                results.Add(GetCalculatedValue(field.Key, field.Value, relatedValues, recentDemo, start, end, ref errors));
            return results;
        }

        public override List<KeyValuePair<string, string>> GetCalculatedValues(List<KeyValuePair<string, string>> fields, Dictionary<string, string> relatedValues, int adminLevel, DateTime start, DateTime end)
        {
            string errors = "";
            AdminLevelDemography recentDemo = GetDemography(adminLevel, start, end);
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();
            foreach (KeyValuePair<string, string> field in fields) // Key is form translation key, value is indicator name
                results.Add(GetCalculatedValue(field.Key, field.Value, relatedValues, recentDemo, start, end, ref errors));
            return results;
        }

        public override KeyValuePair<string, string> GetCalculatedValue(string formTranslationKey, string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime start, DateTime end, ref string errors)
        {
            try
            {
                // First check shared intervention calculations
                KeyValuePair<string, string> sharedIntvCalc = CheckPcIntvCalculations(relatedValues, field, formTranslationKey, demo.AdminLevelId, start, end, ref errors);
                if (sharedIntvCalc.Key != null && sharedIntvCalc.Value != null)
                    return sharedIntvCalc;

                // Determine the form name to avoid traversing the whole case statement
                if (formTranslationKey == "BuruliUlcerIntv")
                    return CalculateBuruliUlcerIntv(formTranslationKey, field, relatedValues, demo, start, end, ref errors);
                else if (formTranslationKey == "GuineaWormIntervention")
                    return CalculateGuineaWormIntv(formTranslationKey, field, relatedValues, demo, start, end, ref errors);
                else if (formTranslationKey == "HatIntervention")
                    return CalculateHatIntv(formTranslationKey, field, relatedValues, demo, start, end, ref errors);
                else if (formTranslationKey == "YawsIntervention")
                    return CalculateYawsIntv(formTranslationKey, field, relatedValues, demo, start, end, ref errors);
                else if (formTranslationKey == "LeishAnnualIntervention")
                    return CalculateLeishAnnualIntv(formTranslationKey, field, relatedValues, demo, start, end, ref errors);
                else if (formTranslationKey == "LeishMonthlyIntervention")
                    return CalculateLeishMonthlyIntv(formTranslationKey, field, relatedValues, demo, start, end, ref errors);
                else
                    return new KeyValuePair<string, string>(field, Translations.NA);

            }
            catch (Exception)
            {
            }

            return new KeyValuePair<string, string>(field, Translations.CalculationError);
        }

        private KeyValuePair<string, string> CalculateBuruliUlcerIntv(string formTranslationKey, string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime start, DateTime end, ref string errors)
        {
            // Combine the form translation key and field name
            string formNameFieldComposite = formTranslationKey + field;

            try
            {
                switch (formNameFieldComposite)
                {
                    case "BuruliUlcerIntvPercentCoverageBu":
                        if (demo != null)
                            return new KeyValuePair<string, string>(Translations.PercentCoverageBu, GetPercentage(GetValueOrDefault("BuruliUlcerIntvNumFacilitiesProvidingBu", relatedValues), demo.PopAdult.ToString()));
                        break;
                    default:
                        return new KeyValuePair<string, string>(formNameFieldComposite, Translations.NA);
                }
            }
            catch (Exception)
            {
            }

            return new KeyValuePair<string, string>(formNameFieldComposite, Translations.CalculationError);
        }

        private KeyValuePair<string, string> CalculateGuineaWormIntv(string formTranslationKey, string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime start, DateTime end, ref string errors)
        {
            // Combine the form translation key and field name
            string formNameFieldComposite = formTranslationKey + field;
            try
            {
                switch (formNameFieldComposite)
                {
                    case "GuineaWormInterventionNumImported":
                        return new KeyValuePair<string, string>(Translations.NumImported, GetDifference(GetValueOrDefault("GuineaWormInterventionNumClinical", relatedValues), GetValueOrDefault("GuineaWormInterventionNumIndigenous", relatedValues)));
                    case "GuineaWormInterventionPercentVas":
                        return new KeyValuePair<string, string>(Translations.PercentVas, GetPercentage(GetValueOrDefault("GuineaWormInterventionVasReporting", relatedValues), GetValueOrDefault("GuineaWormInterventionNumVas", relatedValues)));
                    case "GuineaWormInterventionPercentIdsr":
                        return new KeyValuePair<string, string>(Translations.PercentIdsr, GetPercentage(GetValueOrDefault("GuineaWormInterventionNumIdsrReporting", relatedValues), GetValueOrDefault("GuineaWormInterventionNumIdsr", relatedValues)));
                    case "GuineaWormInterventionPercentRumorsInvestigated":
                        return new KeyValuePair<string, string>(Translations.PercentRumorsInvestigated, GetPercentage(GetValueOrDefault("GuineaWormInterventionNumRumorsInvestigated", relatedValues), GetValueOrDefault("GuineaWormInterventionNumRumors", relatedValues)));
                    case "GuineaWormInterventionPercentCasesContained":
                        return new KeyValuePair<string, string>(Translations.PercentCasesContained, GetPercentage(GetValueOrDefault("GuineaWormInterventionNumCasesContained", relatedValues), GetValueOrDefault("GuineaWormInterventionNumClinical", relatedValues)));
                    case "GuineaWormInterventionPercentEndemicReporting":
                        return new KeyValuePair<string, string>(Translations.PercentEndemicReporting, GetPercentage(GetValueOrDefault("GuineaWormInterventionNumEndemicVillagesReporting", relatedValues), GetValueOrDefault("GuineaWormInterventionNumEndemicVillages", relatedValues)));
                    default:
                        return new KeyValuePair<string, string>(formNameFieldComposite, Translations.NA);
                }
            }
            catch (Exception)
            {
            }

            return new KeyValuePair<string, string>(formNameFieldComposite, Translations.CalculationError);
        }

        private KeyValuePair<string, string> CalculateHatIntv(string formTranslationKey, string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime start, DateTime end, ref string errors)
        {
            // Combine the form translation key and field name
            string formNameFieldComposite = formTranslationKey + field;

            try
            {
                switch (formNameFieldComposite)
                {
                    case "HatInterventionPercentLabConfirmed":
                        return new KeyValuePair<string, string>(Translations.PercentLabConfirmed, GetPercentage(GetValueOrDefault("HatInterventionNumLabCases", relatedValues), GetValueOrDefault("HatInterventionNumClinicalCasesHat", relatedValues)));
                    case "HatInterventionPercentTGamb":
                        return new KeyValuePair<string, string>(Translations.PercentTGamb, GetPercentage(GetValueOrDefault("HatInterventionNumTGamb", relatedValues), GetValueOrDefault("HatInterventionNumLabCases", relatedValues)));
                    case "HatInterventionPercentTRhod":
                        return new KeyValuePair<string, string>(Translations.PercentTRhod, GetPercentage(GetValueOrDefault("HatInterventionNumTRhod", relatedValues), GetValueOrDefault("HatInterventionNumLabCases", relatedValues)));
                    case "HatInterventionPercentTGambTRhod":
                        return new KeyValuePair<string, string>(Translations.PercentTGambTRhod, GetPercentage(GetValueOrDefault("HatInterventionNumTGambTRhod", relatedValues), GetValueOrDefault("HatInterventionNumClinicalCasesHat", relatedValues)));
                    case "HatInterventionPercentCasesActivelyFound":
                        return new KeyValuePair<string, string>(Translations.PercentCasesActivelyFound, GetPercentage(GetValueOrDefault("HatInterventionNumProspection", relatedValues), GetValueOrDefault("HatInterventionNumClinicalCasesHat", relatedValues)));
                    case "HatInterventionCureRate":
                        return new KeyValuePair<string, string>(Translations.CureRate, GetPercentage(GetValueOrDefault("HatInterventionNumCasesCured", relatedValues), GetValueOrDefault("HatInterventionNumCasesTreated", relatedValues)));
                    case "HatInterventionPercentTreatmentFailure":
                        return new KeyValuePair<string, string>(Translations.PercentTreatmentFailure, GetPercentage(GetValueOrDefault("HatInterventionNumTreatmentFailures", relatedValues), GetValueOrDefault("HatInterventionNumCasesTreated", relatedValues)));
                    case "HatInterventionPercentSae":
                        return new KeyValuePair<string, string>(Translations.PercentSae, GetPercentage(GetValueOrDefault("HatInterventionNumCasesSaes", relatedValues), GetValueOrDefault("HatInterventionNumCasesTreated", relatedValues)));
                    case "HatInterventionFatalityRate":
                        return new KeyValuePair<string, string>(Translations.FatalityRate, GetPercentage(GetValueOrDefault("HatInterventionNumDeaths", relatedValues), GetValueOrDefault("HatInterventionNumClinicalCasesHat", relatedValues)));
                    case "HatInterventionDetectionRatePer100k":
                        if (demo != null)
                            return new KeyValuePair<string, string>(Translations.DetectionRatePer100k, GetPercentage(GetValueOrDefault("HatInterventionNumClinicalCasesHat", relatedValues), demo.TotalPopulation.ToString(), 100000));
                        break;
                    default:
                        return new KeyValuePair<string, string>(formNameFieldComposite, Translations.NA);
                }
            }
            catch (Exception)
            {
            }

            return new KeyValuePair<string, string>(formNameFieldComposite, Translations.CalculationError);
        }

        private KeyValuePair<string, string> CalculateYawsIntv(string formTranslationKey, string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime start, DateTime end, ref string errors)
        {
            // Combine the form translation key and field name
            string formNameFieldComposite = formTranslationKey + field;

            try
            {
                switch (formNameFieldComposite)
                {
                    case "YawsInterventionPercentTreatedYw":
                        return new KeyValuePair<string, string>(Translations.PercentTreatedYw, GetPercentage(GetValueOrDefault("YawsInterventionNumContactsTreatedYw", relatedValues),
                            GetTotal(GetValueOrDefault("YawsInterventionNumContactsTreatedYw", relatedValues), GetValueOrDefault("YawsInterventionNumCasesTreatedYaws", relatedValues))));
                    default:
                        return new KeyValuePair<string, string>(formNameFieldComposite, Translations.NA);
                }
            }
            catch (Exception)
            {
            }

            return new KeyValuePair<string, string>(formNameFieldComposite, Translations.CalculationError);
        }

        private KeyValuePair<string, string> CalculateLeishAnnualIntv(string formTranslationKey, string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime start, DateTime end, ref string errors)
        {
            // Combine the form translation key and field name
            string formNameFieldComposite = formTranslationKey + field;

            try
            {
                switch (formNameFieldComposite)
                {
                    case "LeishAnnualInterventionLeishAnnIntvOfNewVLCasesCuredOutOfNewCasesFollowedUp":
                        return new KeyValuePair<string, string>(
                            Translations.LeishAnnIntvOfNewVLCasesCuredOutOfNewCasesFollowedUp,
                            GetPercentage(
                                GetValueOrDefault("LeishAnnualInterventionLeishAnnIntvNumberOfNewVLCasesCuredAfterFollowUpOfAtLeast6Months", relatedValues),
                                GetValueOrDefault("LeishAnnualInterventionLeishAnnIntvNumberOfNewVLCasesFollowedUpAtLeast6Months", relatedValues)
                            ));
                    case "LeishAnnualInterventionLeishAnnIntvOfNewCLCasesCuredOutOfNewCasesFollowedUp":
                        return new KeyValuePair<string, string>(
                            Translations.LeishAnnIntvOfNewCLCasesCuredOutOfNewCasesFollowedUp,
                            GetPercentage(
                                GetValueOrDefault("LeishAnnualInterventionLeishAnnIntvNumberOfNewCLCasesCuredAfterFollowUpOfAtLeast6Months", relatedValues),
                                GetValueOrDefault("LeishAnnualInterventionLeishAnnIntvNumberOfNewCLCasesFollowedUpAtLeast6Months", relatedValues)
                            ));
                    case "LeishAnnualInterventionLeishAnnIntvOfVLRelapseCasesOutOfTotalNewCasesFollowedUp":
                        return new KeyValuePair<string, string>(
                            Translations.LeishAnnIntvOfVLRelapseCasesOutOfTotalNewCasesFollowedUp,
                            GetPercentage(
                                GetValueOrDefault("LeishAnnualInterventionLeishAnnIntvNumberOfVLRelapseCases", relatedValues),
                                GetValueOrDefault("LeishAnnualInterventionLeishAnnIntvNumberOfNewVLCasesFollowedUpAtLeast6Months", relatedValues)
                            ));
                    case "LeishAnnualInterventionLeishAnnIntvOfCLRelapseCasesOutOfTotalNewCasesFollowedUp":
                        return new KeyValuePair<string, string>(
                            Translations.LeishAnnIntvOfCLRelapseCasesOutOfTotalNewCasesFollowedUp,
                            GetPercentage(
                                GetValueOrDefault("LeishAnnualInterventionLeishAnnIntvNumberOfCLRelapseCases", relatedValues),
                                GetValueOrDefault("LeishAnnualInterventionLeishAnnIntvNumberOfNewCLCasesFollowedUpAtLeast6Months", relatedValues)
                            ));
                    default:
                        return new KeyValuePair<string, string>(formNameFieldComposite, Translations.NA);
                }
            }
            catch (Exception)
            {
            }

            return new KeyValuePair<string, string>(formNameFieldComposite, Translations.CalculationError);
        }

        private KeyValuePair<string, string> CalculateLeishMonthlyIntv(string formTranslationKey, string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime start, DateTime end, ref string errors)
        {
            // Combine the form translation key and field name
            string formNameFieldComposite = formTranslationKey + field;

            try
            {
                switch (formNameFieldComposite)
                {
                    case "LeishMonthlyInterventionLeishMontIntvDetectionRatePer100000":
                        if (demo != null)
                        {
                            return new KeyValuePair<string, string>(Translations.LeishMontIntvDetectionRatePer100000,
                                GetPercentage(
                                    GetTotal(new string[2]
                                    {
                                        GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues),
                                        GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                                    }),
                                    demo.TotalPopulation.ToString(),
                                    10000
                                    ));
                        }
                        break;
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfLabConfirmedCases":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfLabConfirmedCases,
                            GetPercentage(
                                GetTotal(new string[3]
                                {
                                    GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfLabConfirmedCLCases", relatedValues),
                                    GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfVLCasesDiagnosedByAPositiveRapidDiagnosticTestsRDT", relatedValues),
                                    GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfLabConfirmedVisceralLeishmaniasisVLCases", relatedValues)
                                }),
                                GetTotal(new string[2]
                                {
                                    GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues),
                                    GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                                }
                            )));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntCasesActivelyFound":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntCasesActivelyFound,
                            GetPercentage(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfCasesFoundActively", relatedValues),
                                GetTotal(new string[6]
                                {
                                    GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfPeopleScreenedActivelyForVL", relatedValues),
                                    GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfPeopleScreenedPassivelyForVL", relatedValues),
                                    GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfPeopleScreenedActivelyForCL", relatedValues),
                                    GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfPeopleScreenedPassivelyForCL", relatedValues),
                                    GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfPeopleScreenedActivelyForPKDL", relatedValues),
                                    GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfPeopleScreenedPassivelyForPKDL", relatedValues)
                                }
                            )));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfVLHIVCoInfectedCasesOfTheTotalNewVLCases":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfVLHIVCoInfectedCasesOfTheTotalNewVLCases,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfVisceralLeishmaniasisHIVCoInfection", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfPKDLHIVCoInfectedCasesOfTheTotalNewPKDLCases":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfPKDLHIVCoInfectedCasesOfTheTotalNewPKDLCases,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewPKDLCasesHIVCoInfection", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfVLCasesDiagnosedByRDT":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfVLCasesDiagnosedByRDT,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfVLCasesDiagnosedByAPositiveRapidDiagnosticTestsRDT", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfPostitiveRDT":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfPostitiveRDT,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfVLCasesDiagnosedByAPositiveRapidDiagnosticTestsRDT", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfVLSuspectsTestedWithRapidDiagnosticTests", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfVLParasitologicallyConfirmedCases":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfVLParasitologicallyConfirmedCases,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfLabConfirmedVisceralLeishmaniasisVLCases", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfCLParasitologicallyConfirmedCases":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfCLParasitologicallyConfirmedCases,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfLabConfirmedCLCases", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfParasitologicallyConfirmedVLSamples":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfParasitologicallyConfirmedVLSamples,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfLabConfirmedVisceralLeishmaniasisVLCases", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfVLCasesTestedByDirectExamParasitology", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfParasitologicallyConfirmedCLSamples":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfParasitologicallyConfirmedCLSamples,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfLabConfirmedCLCases", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfCLCasesTestedByDirectExamParasitology", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForVL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForVL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewVLCasesWithInitialCure", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewVLCasesTreated", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForCL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForCL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewCLCasesWithInitialCure", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewCLCasesTreated", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForVL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForVL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfFailureCasesVL", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewVLCasesTreated", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForCL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForCL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfFailureCasesCL", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewCLCasesTreated", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntCaseFatalityRateForVL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntCaseFatalityRateForVL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfDeathsForNewVLCases", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewVLCasesTreated", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntCaseFatalityRateForCL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntCaseFatalityRateForCL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfDeathsForNewCLCases", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewCLCasesTreated", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfCLPatientsWithLesionsGrtrThnOrEqlTo4Cm":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfCLPatientsWithLesionsGrtrThnOrEqlTo4Cm,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfCLPatientsWithLesionsGrtrThnOrEqlTo4Cm", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfCLPatientsWithGrtrThnOrEqlTo4Lesions":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfCLPatientsWithGrtrThnOrEqlTo4Lesions,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfCLPatientsWithGrtrThnOrEqlTo4Lesions", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfCLPatientsWithLesionsOnFaceEars":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfCLPatientsWithLesionsOnFaceEars,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfCLPatientsWithLesionsOnFaceOrEars", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfCLPatientsUndergoingSystemicTreatment":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfCLPatientsUndergoingSystemicTreatment,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfCLPatientsUndergoingSystemicTreatment", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewCLCasesTreated", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvVLIncidenceRate10000PeopleYear":
                        if (demo != null)
                        {
                            return new KeyValuePair<string, string>(Translations.LeishMontIntvVLIncidenceRate10000PeopleYear,
                                GetPercentage(
                                    GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues),
                                    demo.TotalPopulation.ToString(),
                                    10000
                                ));
                        }
                        break;
                    case "LeishMonthlyInterventionLeishMontIntvCLIncidenceRate10000PeopleYear":
                        if (demo != null)
                        {
                            return new KeyValuePair<string, string>(Translations.LeishMontIntvCLIncidenceRate10000PeopleYear,
                                GetPercentage(
                                    GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues),
                                    demo.TotalPopulation.ToString(),
                                    10000
                                ));
                        }
                        break;
                    case "LeishMonthlyInterventionLeishMontIntvPrcntFemaleVL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntFemaleVL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewVLFemaleCases", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntFemaleCL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntFemaleCL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewCLFemaleCases", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntFemalePKDL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntFemalePKDL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewPKDLFemaleCases", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntFemaleMCL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntFemaleMCL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewMCLFemaleCases", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewMCLCasesDiagnosed", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfNewVLCasesInChildrenLssThn5Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewVLCasesInChildrenLssThn5Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewVLCasesInChildrenLssThn5Years", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfNewVLCasesInChildren5To14Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewVLCasesInChildren5To14Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewVLCasesInChildren5To14Years", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfNewVLCasesInAdultsGrtrThn14Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewVLCasesInAdultsGrtrThn14Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewVLCasesInAdultsGrtrThn14Years", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfNewCLCasesInChildrenLssThn5Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewCLCasesInChildrenLssThn5Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewCLCasesInChildrenLssThn5Years", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfNewCLCasesInChildren5To14Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewCLCasesInChildren5To14Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewCLCasesInChildren5To14Years", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfNewCLCasesInAdultsGrtrThn14Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewCLCasesInAdultsGrtrThn14Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewCLCasesInAdultsGrtrThn14Years", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfNewPKDLCasesInChildrenLssThn5Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewPKDLCasesInChildrenLssThn5Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewPKDLCasesInChildrenLssThn5Years", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfNewPKDLCasesInChildren5To14Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewPKDLCasesInChildren5To14Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewPKDLCasesInChildren5To14Years", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfNewPKDLCasesInAdultsGrtrThn14Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewPKDLCasesInAdultsGrtrThn14Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewPKDLCasesInAdultsGrtrThn14Years", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfNewMCLCasesInChildrenLssThn5Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewMCLCasesInChildrenLssThn5Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewMCLCasesInChildrenLssThn5Years", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewMCLCasesDiagnosed", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfNewMCLCasesInChildren5To14Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewMCLCasesInChildren5To14Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewMCLCasesInChildren5To14Years", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewMCLCasesDiagnosed", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvPrcntOfNewMCLCasesInAdultsGrtrThn14Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewMCLCasesInAdultsGrtrThn14Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfNewMCLCasesInAdultsGrtrThn14Years", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvTotalNumberOfNewMCLCasesDiagnosed", relatedValues)
                            ));
                    case "LeishMonthlyInterventionLeishMontIntvMonthlyConsumptionRate1StLineTreatmentUnits":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvMonthlyConsumptionRate1StLineTreatmentUnits,
                            GetDifference(
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfUnitsVialsFor1StLineTreatmentAtTheBeginningOfTheMonth", relatedValues),
                                GetValueOrDefault("LeishMonthlyInterventionLeishMontIntvNumberOfUnitsVialsFor1StLineTreatmentAtTheEndOfTheMonth", relatedValues)
                            ));
                    default:
                        return new KeyValuePair<string, string>(formNameFieldComposite, Translations.NA);
                }
            }
            catch (Exception)
            {
            }

            return new KeyValuePair<string, string>(formNameFieldComposite, Translations.CalculationError);
        }

        private KeyValuePair<string, string> CheckPcIntvCalculations(Dictionary<string, string> relatedValues, string field, string formTranslationKey, int adminLevelId, DateTime start, DateTime end, ref string errors)
        {
            // Combine the form translation key and field name
            string formNameFieldComposite = formTranslationKey + field;

            if (formNameFieldComposite == formTranslationKey + "PcIntvProgramCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvProgramCoverage, GetPercentage(GetValueOrDefault(formTranslationKey + "PcIntvNumIndividualsTreated", relatedValues), GetValueOrDefault(formTranslationKey + "PcIntvNumEligibleIndividualsTargeted", relatedValues)));
            if (formNameFieldComposite == formTranslationKey + "PcIntvFemalesTreatedProportion")
                return new KeyValuePair<string, string>(Translations.PcIntvFemalesTreatedProportion, GetPercentage(GetValueOrDefault(formTranslationKey + "PcIntvNumFemalesTreated", relatedValues), GetValueOrDefault(formTranslationKey + "PcIntvNumIndividualsTreated", relatedValues)));
            if (formNameFieldComposite == formTranslationKey + "PcIntvMalesTreatedProportion")
                return new KeyValuePair<string, string>(Translations.PcIntvMalesTreatedProportion, GetPercentage(GetValueOrDefault(formTranslationKey + "PcIntvNumMalesTreated", relatedValues), GetValueOrDefault(formTranslationKey + "PcIntvNumIndividualsTreated", relatedValues)));
            if (formNameFieldComposite == formTranslationKey + "PcIntvPsacCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvPsacCoverage, GetPercentage(GetValueOrDefault(formTranslationKey + "PcIntvPsacTreated", relatedValues), GetValueOrDefault(formTranslationKey + "PcIntvNumPsacTargeted", relatedValues)));
            if (formNameFieldComposite == formTranslationKey + "PcIntvSacCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvSacCoverage, GetPercentage(GetValueOrDefault(formTranslationKey + "PcIntvNumSacTreated", relatedValues), GetValueOrDefault(formTranslationKey + "PcIntvNumSacTargeted", relatedValues)));
            // EPI Meta data
            if (formNameFieldComposite == formTranslationKey + "PcIntvSthPopReqPc")
                return new KeyValuePair<string, string>(Translations.PcIntvSthPopReqPc, GetRecentDistroIndicator(adminLevelId, "DDSTHPopulationRequiringPc", DiseaseType.STH, start, end, ref errors));
            if (formNameFieldComposite == formTranslationKey + "PcIntvSthPsacAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvSthPsacAtRisk, GetRecentDistroIndicator(adminLevelId, "DDSTHPsacAtRisk", DiseaseType.STH, start, end, ref errors));
            if (formNameFieldComposite == formTranslationKey + "PcIntvSthSacAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvSthSacAtRisk, GetRecentDistroIndicator(adminLevelId, "DDSTHSacAtRisk", DiseaseType.STH, start, end, ref errors));
            if (formNameFieldComposite == formTranslationKey + "PcIntvSthAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvSthAtRisk, GetRecentDistroIndicator(adminLevelId, "DDSTHPopulationAtRisk", DiseaseType.STH, start, end, ref errors));
            if (formNameFieldComposite == formTranslationKey + "PcIntvLfAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvLfAtRisk, GetRecentDistroIndicator(adminLevelId, "DDLFPopulationAtRisk", DiseaseType.Lf, start, end, ref errors));
            if (formNameFieldComposite == formTranslationKey + "PcIntvLfPopRecPc")
                return new KeyValuePair<string, string>(Translations.PcIntvLfPopRecPc, GetRecentDistroIndicator(adminLevelId, "DDLFPopulationRequiringPc", DiseaseType.Lf, start, end, ref errors));
            if (formNameFieldComposite == formTranslationKey + "PcIntvOnchoAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvOnchoAtRisk, GetRecentDistroIndicator(adminLevelId, "DDOnchoPopulationAtRisk", DiseaseType.Oncho, start, end, ref errors));
            if (formNameFieldComposite == formTranslationKey + "PcIntvOnchoPopReqPc")
                return new KeyValuePair<string, string>(Translations.PcIntvOnchoPopReqPc, GetRecentDistroIndicator(adminLevelId, "DDOnchoPopulationRequiringPc", DiseaseType.Oncho, start, end, ref errors));
            if (formNameFieldComposite == formTranslationKey + "PcIntvSchAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvSchAtRisk, GetRecentDistroIndicator(adminLevelId, "DDSchistoPopulationAtRisk", DiseaseType.Schisto, start, end, ref errors));
            if (formNameFieldComposite == formTranslationKey + "PcIntvSchPopReqPc")
                return new KeyValuePair<string, string>(Translations.PcIntvSchPopReqPc, GetRecentDistroIndicator(adminLevelId, "DDSchistoPopulationRequiringPc", DiseaseType.Schisto, start, end, ref errors));
            if (formNameFieldComposite == formTranslationKey + "PcIntvSchSacAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvSchSacAtRisk, GetRecentDistroIndicator(adminLevelId, "DDSchistoSacAtRisk", DiseaseType.Schisto, start, end, ref errors));
            if (formNameFieldComposite == formTranslationKey + "PcIntvTraAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvTraAtRisk, GetRecentDistroIndicator(adminLevelId, "DDTraPopulationAtRisk", DiseaseType.Trachoma, start, end, ref errors));

            // epi calcs
            if (formNameFieldComposite == formTranslationKey + "PcIntvSthPsacEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvSthPsacEpiCoverage, GetPercentage(GetValueOrDefault(formTranslationKey + "PcIntvPsacTreated", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDSTHPsacAtRisk", DiseaseType.STH, start, end, ref errors)));
            if (formNameFieldComposite == formTranslationKey + "PcIntvSthSacEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvSthSacEpiCoverage, GetPercentage(GetValueOrDefault(formTranslationKey + "PcIntvNumSacTreated", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDSTHSacAtRisk", DiseaseType.STH, start, end, ref errors)));
            if (formNameFieldComposite == formTranslationKey + "PcIntvSthEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvSthEpiCoverage, GetPercentage(GetValueOrDefault(formTranslationKey + "PcIntvNumIndividualsTreated", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDSTHPopulationAtRisk", DiseaseType.STH, start, end, ref errors)));
            if (formNameFieldComposite == formTranslationKey + "PcIntvLfEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvLfEpiCoverage, GetPercentage(GetValueOrDefault(formTranslationKey + "PcIntvNumIndividualsTreated", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDLFPopulationAtRisk", DiseaseType.Lf, start, end, ref errors)));
            if (formNameFieldComposite == formTranslationKey + "PcIntvOnchoEpiCoverageOfOncho")
                return new KeyValuePair<string, string>(Translations.PcIntvOnchoEpiCoverageOfOncho, GetPercentage(GetValueOrDefault(formTranslationKey + "PcIntvOfTotalTreatedForOncho", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDOnchoPopulationAtRisk", DiseaseType.Oncho, start, end, ref errors)));
            if (formNameFieldComposite == formTranslationKey + "PcIntvOnchoEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvOnchoEpiCoverage, GetPercentage(GetValueOrDefault(formTranslationKey + "PcIntvNumIndividualsTreated", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDOnchoPopulationAtRisk", DiseaseType.Oncho, start, end, ref errors)));
            if (formNameFieldComposite == formTranslationKey + "PcIntvOnchoProgramCov")
                return new KeyValuePair<string, string>(Translations.PcIntvOnchoProgramCov, GetPercentage(GetValueOrDefault(formTranslationKey + "PcIntvOfTotalTreatedForOncho", relatedValues), GetValueOrDefault(formTranslationKey + "PcIntvOfTotalTargetedForOncho", relatedValues)));
            if (formNameFieldComposite == formTranslationKey + "PcIntvSchSacEpi")
                return new KeyValuePair<string, string>(Translations.PcIntvSchSacEpi, GetPercentage(GetValueOrDefault(formTranslationKey + "PcIntvNumSacTreated", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDSchistoSacAtRisk", DiseaseType.Schisto, start, end, ref errors)));
            if (formNameFieldComposite == formTranslationKey + "PcIntvSchEpi")
                return new KeyValuePair<string, string>(Translations.PcIntvSchEpi, GetPercentage(GetValueOrDefault(formTranslationKey + "PcIntvNumIndividualsTreated", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDSchistoPopulationAtRisk", DiseaseType.Schisto, start, end, ref errors)));
            if (formNameFieldComposite == formTranslationKey + "PcIntvTraEpi")
                return new KeyValuePair<string, string>(Translations.PcIntvTraEpi,
                    GetPercentage(GetValueOrDefault(formTranslationKey + "PcIntvNumIndividualsTreated", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDTraPopulationAtRisk", DiseaseType.Trachoma, start, end, ref errors)));

            return new KeyValuePair<string, string>(null, null);
        }
    }
}
