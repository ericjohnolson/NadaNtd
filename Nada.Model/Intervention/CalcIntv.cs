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
        public override List<KeyValuePair<string, string>> GetMetaData(IEnumerable<string> fields, int adminLevel, DateTime start, DateTime end)
        {
            string errors = "";
            var relatedValues = new Dictionary<string, string>();
            AdminLevelDemography recentDemo = GetDemography(adminLevel, start, end);
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();
            foreach (string field in fields)
                results.Add(GetCalculatedValue(field, relatedValues, recentDemo, start, end, ref errors));
            return results;
        }

        public override List<KeyValuePair<string, string>> GetCalculatedValues(List<string> fields, Dictionary<string, string> relatedValues, int adminLevel, DateTime start, DateTime end)
        {
            string errors = "";
            AdminLevelDemography recentDemo = GetDemography(adminLevel, start, end);
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();
            foreach (string field in fields)
                results.Add(GetCalculatedValue(field, relatedValues, recentDemo, start, end, ref errors));
            return results;
        }

        public override KeyValuePair<string, string> GetCalculatedValue(string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime start, DateTime end, ref string errors)
        {
            try
            {
                switch (field)
                {
                    case "8PercentCoverageBu":
                        if (demo != null)
                            return new KeyValuePair<string, string>(Translations.PercentCoverageBu, GetPercentage(GetValueOrDefault("8NumFacilitiesProvidingBu", relatedValues), demo.PopAdult.ToString()));
                        break;
                    case "4NumImported":
                        return new KeyValuePair<string, string>(Translations.NumImported, GetDifference(GetValueOrDefault("4NumClinical", relatedValues), GetValueOrDefault("4NumIndigenous", relatedValues)));
                    case "4PercentVas":
                        return new KeyValuePair<string, string>(Translations.PercentVas, GetPercentage(GetValueOrDefault("4VasReporting", relatedValues), GetValueOrDefault("4NumVas", relatedValues)));
                    case "4PercentIdsr":
                        return new KeyValuePair<string, string>(Translations.PercentIdsr, GetPercentage(GetValueOrDefault("4NumIdsrReporting", relatedValues), GetValueOrDefault("4NumIdsr", relatedValues)));
                    case "4PercentRumorsInvestigated":
                        return new KeyValuePair<string, string>(Translations.PercentRumorsInvestigated, GetPercentage(GetValueOrDefault("4NumRumorsInvestigated", relatedValues), GetValueOrDefault("4NumRumors", relatedValues)));
                    case "4PercentCasesContained":
                        return new KeyValuePair<string, string>(Translations.PercentCasesContained, GetPercentage(GetValueOrDefault("4NumCasesContained", relatedValues), GetValueOrDefault("4NumClinical", relatedValues)));
                    case "4PercentEndemicReporting":
                        return new KeyValuePair<string, string>(Translations.PercentEndemicReporting, GetPercentage(GetValueOrDefault("4NumEndemicVillagesReporting", relatedValues), GetValueOrDefault("4NumEndemicVillages", relatedValues)));
                    case "7DetectRate100kLeish":
                        if (demo != null)
                            return new KeyValuePair<string, string>(Translations.DetectRate100kLeish, GetPercentage(GetTotal(GetValueOrDefault("7NumClCases", relatedValues), GetValueOrDefault("7NumLabVlCases", relatedValues), GetValueOrDefault("7NumClVlCases", relatedValues)),
                                demo.TotalPopulation.ToString(), 100000));
                        break;
                    case "7PercentLabConfirm":
                        return new KeyValuePair<string, string>(Translations.PercentLabConfirm, GetPercentage(GetTotal(GetValueOrDefault("7NumLabClCases", relatedValues), GetValueOrDefault("7NumLabVlCases", relatedValues)),
                            GetTotal(GetValueOrDefault("7NumClCases", relatedValues), GetValueOrDefault("7NumLabVlCases", relatedValues), GetValueOrDefault("7NumClVlCases", relatedValues))));
                    case "7PercentCl":
                        return new KeyValuePair<string, string>(Translations.PercentCl, GetPercentage(GetValueOrDefault("7NumClCases", relatedValues),
                            GetTotal(GetValueOrDefault("7NumClCases", relatedValues), GetValueOrDefault("7NumLabVlCases", relatedValues), GetValueOrDefault("7NumClVlCases", relatedValues))));
                    case "7PercentVl":
                        return new KeyValuePair<string, string>(Translations.PercentVl, GetPercentage(GetValueOrDefault("7NumLabVlCases", relatedValues),
                            GetTotal(GetValueOrDefault("7NumClCases", relatedValues), GetValueOrDefault("7NumLabVlCases", relatedValues), GetValueOrDefault("7NumClVlCases", relatedValues))));
                    case "7PercentClVl":
                        return new KeyValuePair<string, string>(Translations.PercentClVl, GetPercentage(GetValueOrDefault("7NumClVlCases", relatedValues),
                            GetTotal(GetValueOrDefault("7NumClCases", relatedValues), GetValueOrDefault("7NumLabVlCases", relatedValues), GetValueOrDefault("7NumClVlCases", relatedValues))));
                    case "7PercentCasesActiveLeish":
                        return new KeyValuePair<string, string>(Translations.PercentCasesActiveLeish, GetPercentage(GetValueOrDefault("7NumCasesFoundActively", relatedValues),
                            GetTotal(GetValueOrDefault("7NumClCases", relatedValues), GetValueOrDefault("7NumLabVlCases", relatedValues), GetValueOrDefault("7NumClVlCases", relatedValues))));
                    case "6PercentLabConfirmed":
                        return new KeyValuePair<string, string>(Translations.PercentLabConfirmed, GetPercentage(GetValueOrDefault("6NumLabCases", relatedValues), GetValueOrDefault("6NumClinicalCasesHat", relatedValues)));
                    case "6PercentTGamb":
                        return new KeyValuePair<string, string>(Translations.PercentTGamb, GetPercentage(GetValueOrDefault("6NumTGamb", relatedValues), GetValueOrDefault("6NumLabCases", relatedValues)));
                    case "6PercentTRhod":
                        return new KeyValuePair<string, string>(Translations.PercentTRhod, GetPercentage(GetValueOrDefault("6NumTRhod", relatedValues), GetValueOrDefault("6NumLabCases", relatedValues)));
                    case "6PercentTGambTRhod":
                        return new KeyValuePair<string, string>(Translations.PercentTGambTRhod, GetPercentage(GetValueOrDefault("6NumTGambTRhod", relatedValues), GetValueOrDefault("6NumClinicalCasesHat", relatedValues)));
                    case "6PercentCasesActivelyFound":
                        return new KeyValuePair<string, string>(Translations.PercentCasesActivelyFound, GetPercentage(GetValueOrDefault("6NumProspection", relatedValues), GetValueOrDefault("6NumClinicalCasesHat", relatedValues)));
                    case "6CureRate":
                        return new KeyValuePair<string, string>(Translations.CureRate, GetPercentage(GetValueOrDefault("6NumCasesCured", relatedValues), GetValueOrDefault("6NumCasesTreated", relatedValues)));
                    case "6PercentTreatmentFailure":
                        return new KeyValuePair<string, string>(Translations.PercentTreatmentFailure, GetPercentage(GetValueOrDefault("6NumTreatmentFailures", relatedValues), GetValueOrDefault("6NumCasesTreated", relatedValues)));
                    case "6PercentSae":
                        return new KeyValuePair<string, string>(Translations.PercentSae, GetPercentage(GetValueOrDefault("6NumCasesSaes", relatedValues), GetValueOrDefault("6NumCasesTreated", relatedValues)));
                    case "6FatalityRate":
                        return new KeyValuePair<string, string>(Translations.FatalityRate, GetPercentage(GetValueOrDefault("6NumDeaths", relatedValues), GetValueOrDefault("6NumClinicalCasesHat", relatedValues)));
                    case "6DetectionRatePer100k":
                        if (demo != null)
                            return new KeyValuePair<string, string>(Translations.DetectionRatePer100k, GetPercentage(GetValueOrDefault("6NumClinicalCasesHat", relatedValues), demo.TotalPopulation.ToString(), 100000));
                        break;
                    case "9PercentTreatedYw":
                        return new KeyValuePair<string, string>(Translations.PercentTreatedYw, GetPercentage(GetValueOrDefault("9NumContactsTreatedYw", relatedValues),
                            GetTotal(GetValueOrDefault("9NumContactsTreatedYw", relatedValues), GetValueOrDefault("9NumCasesTreatedYaws", relatedValues))));
                    case "26LeishAnnIntvOfNewVLCasesCuredOutOfNewCasesFollowedUp":
                        return new KeyValuePair<string, string>(
                            Translations.LeishAnnIntvOfNewVLCasesCuredOutOfNewCasesFollowedUp,
                            GetPercentage(
                                GetValueOrDefault("26LeishAnnIntvNumberOfNewVLCasesCuredAfterFollowUpOfAtLeast6Months", relatedValues),
                                GetValueOrDefault("26LeishAnnIntvNumberOfNewVLCasesFollowedUpAtLeast6Months", relatedValues)
                            ));
                    case "26LeishAnnIntvOfNewCLCasesCuredOutOfNewCasesFollowedUp":
                        return new KeyValuePair<string, string>(
                            Translations.LeishAnnIntvOfNewVLCasesCuredOutOfNewCasesFollowedUp,
                            GetPercentage(
                                GetValueOrDefault("26LeishAnnIntvNumberOfNewCLCasesCuredAfterFollowUpOfAtLeast6Months", relatedValues),
                                GetValueOrDefault("26LeishAnnIntvNumberOfNewCLCasesFollowedUpAtLeast6Months", relatedValues)
                            ));
                    case "26LeishAnnIntvOfVLRelapseCasesOutOfTotalNewCasesFollowedUp":
                        return new KeyValuePair<string, string>(
                            Translations.LeishAnnIntvOfNewVLCasesCuredOutOfNewCasesFollowedUp,
                            GetPercentage(
                                GetValueOrDefault("26LeishAnnIntvNumberOfVLRelapseCases", relatedValues),
                                GetValueOrDefault("26LeishAnnIntvNumberOfNewVLCasesFollowedUpAtLeast6Months", relatedValues)
                            ));
                    case "26LeishAnnIntvOfCLRelapseCasesOutOfTotalNewCasesFollowedUp":
                        return new KeyValuePair<string, string>(
                            Translations.LeishAnnIntvOfNewVLCasesCuredOutOfNewCasesFollowedUp,
                            GetPercentage(
                                GetValueOrDefault("26LeishAnnIntvNumberOfCLRelapseCases", relatedValues),
                                GetValueOrDefault("26LeishAnnIntvNumberOfNewCLCasesFollowedUpAtLeast6Months", relatedValues)
                            ));
                    case "27LeishMontIntvDetectionRatePer100000":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvDetectionRatePer100000, "--");
                    case "27LeishMontIntvPrcntOfLabConfirmedCases":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfLabConfirmedCases,
                            GetPercentage(
                                GetTotal(new string[3]
                                {
                                    GetValueOrDefault("27LeishMontIntvNumberOfLabConfirmedCLCases", relatedValues),
                                    GetValueOrDefault("27LeishMontIntvNumberOfVLCasesDiagnosedByAPositiveRapidDiagnosticTestsRDT", relatedValues),
                                    GetValueOrDefault("27LeishMontIntvNumberOfLabConfirmedVisceralLeishmaniasisVLCases", relatedValues)
                                }),
                                GetTotal(new string[2]
                                {
                                    GetValueOrDefault("27LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues),
                                    GetValueOrDefault("27LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                                }
                            )));
                    case "27LeishMontIntvPrcntCasesActivelyFound":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntCasesActivelyFound,
                            GetPercentage(
                                GetValueOrDefault("27LeishMontIntvNumberOfCasesFoundActively", relatedValues),
                                GetTotal(new string[6]
                                {
                                    GetValueOrDefault("27LeishMontIntvNumberOfPeopleScreenedActivelyForVL", relatedValues),
                                    GetValueOrDefault("27LeishMontIntvNumberOfPeopleScreenedPassivelyForVL", relatedValues),
                                    GetValueOrDefault("27LeishMontIntvNumberOfPeopleScreenedActivelyForCL", relatedValues),
                                    GetValueOrDefault("27LeishMontIntvNumberOfPeopleScreenedPassivelyForCL", relatedValues),
                                    GetValueOrDefault("27LeishMontIntvNumberOfPeopleScreenedActivelyForPKDL", relatedValues),
                                    GetValueOrDefault("27LeishMontIntvNumberOfPeopleScreenedPassivelyForPKDL", relatedValues)
                                }
                            )));
                    case "27LeishMontIntvPrcntOfVLHIVCoInfectedCasesOfTheTotalNewVLCases":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfVLHIVCoInfectedCasesOfTheTotalNewVLCases,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfLabConfirmedVisceralLeishmaniasisVLCases", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfPKDLHIVCoInfectedCasesOfTheTotalNewPKDLCases":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfPKDLHIVCoInfectedCasesOfTheTotalNewPKDLCases,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewPKDLCasesHIVCoInfection", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfVLCasesDiagnosedByRDT":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfVLCasesDiagnosedByRDT,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfVLCasesDiagnosedByAPositiveRapidDiagnosticTestsRDT", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfPostitiveRDT":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfPostitiveRDT,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfVLCasesDiagnosedByAPositiveRapidDiagnosticTestsRDT", relatedValues),
                                GetValueOrDefault("27LeishMontIntvNumberOfVLSuspectsTestedWithRapidDiagnosticTests", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfVLParasitologicallyConfirmedCases":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfVLParasitologicallyConfirmedCases,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfLabConfirmedVisceralLeishmaniasisVLCases", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfCLParasitologicallyConfirmedCases":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfCLParasitologicallyConfirmedCases,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfLabConfirmedCLCases", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfParasitologicallyConfirmedVLSamples":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfParasitologicallyConfirmedVLSamples,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfLabConfirmedVisceralLeishmaniasisVLCases", relatedValues),
                                GetValueOrDefault("27LeishMontIntvNumberOfVLCasesTestedByDirectExamParasitology", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfParasitologicallyConfirmedCLSamples":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfParasitologicallyConfirmedCLSamples,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfLabConfirmedCLCases", relatedValues),
                                GetValueOrDefault("27LeishMontIntvNumberOfCLCasesTestedByDirectExamParasitology", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForVL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForVL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewVLCasesWithInitialCure", relatedValues),
                                GetValueOrDefault("27LeishMontIntvNumberOfNewVLCasesTreated", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForCL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForCL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewCLCasesWithInitialCure", relatedValues),
                                GetValueOrDefault("27LeishMontIntvNumberOfNewCLCasesTreated", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForVL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForVL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfFailureCasesVL", relatedValues),
                                GetValueOrDefault("27LeishMontIntvNumberOfNewVLCasesTreated", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForCL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForCL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfFailureCasesCL", relatedValues),
                                GetValueOrDefault("27LeishMontIntvNumberOfNewCLCasesTreated", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntCaseFatalityRateForVL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntCaseFatalityRateForVL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfDeathsForNewVLCases", relatedValues),
                                GetValueOrDefault("27LeishMontIntvNumberOfNewVLCasesTreated", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntCaseFatalityRateForCL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntCaseFatalityRateForCL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfDeathsForNewCLCases", relatedValues),
                                GetValueOrDefault("27LeishMontIntvNumberOfNewCLCasesTreated", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfCLPatientsWithLesionsGrtrThnOrEqlTo4Cm":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfCLPatientsWithLesionsGrtrThnOrEqlTo4Cm,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfCLPatientsWithLesionsGrtrThnOrEqlTo4Cm", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfCLPatientsWithGrtrThnOrEqlTo4Lesions":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfCLPatientsWithGrtrThnOrEqlTo4Lesions,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfCLPatientsWithGrtrThnOrEqlTo4Lesions", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfCLPatientsWithLesionsOnFaceEars":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfCLPatientsWithLesionsOnFaceEars,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfCLPatientsWithLesionsOnFaceOrEars", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfCLPatientsUndergoingSystemicTreatment":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfCLPatientsUndergoingSystemicTreatment,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfCLPatientsUndergoingSystemicTreatment", relatedValues),
                                GetValueOrDefault("27LeishMontIntvNumberOfNewCLCasesTreated", relatedValues)
                            ));
                    case "27LeishMontIntvVLIncidenceRate10000PeopleYear":
                        if (demo != null)
                        {
                            return new KeyValuePair<string, string>(Translations.LeishMontIntvVLIncidenceRate10000PeopleYear,
                                GetPercentage(
                                    GetValueOrDefault("27LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues),
                                    demo.TotalPopulation.ToString(),
                                    10000
                                ));
                        }
                        break;
                    case "27LeishMontIntvCLIncidenceRate10000PeopleYear":
                        if (demo != null)
                        {
                            return new KeyValuePair<string, string>(Translations.LeishMontIntvCLIncidenceRate10000PeopleYear,
                                GetPercentage(
                                    GetValueOrDefault("27LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues),
                                    demo.TotalPopulation.ToString(),
                                    10000
                                ));
                        }
                        break;
                    case "27LeishMontIntvPrcntFemaleVL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntFemaleVL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewVLFemaleCases", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntFemaleCL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntFemaleCL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewCLFemaleCases", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntFemalePKDL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntFemalePKDL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewPKDLFemaleCases", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntFemaleMCL":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntFemaleMCL,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewMCLFemaleCases", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewMCLCasesDiagnosed", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfNewVLCasesInChildrenLssThn5Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewVLCasesInChildrenLssThn5Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewVLCasesInChildrenLssThn5Years", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfNewVLCasesInChildren5To14Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewVLCasesInChildren5To14Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewVLCasesInChildren5To14Years", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfNewVLCasesInAdultsGrtrThn14Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewVLCasesInAdultsGrtrThn14Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewVLCasesInAdultsGrtrThn14Years", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfNewCLCasesInChildrenLssThn5Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewCLCasesInChildrenLssThn5Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewCLCasesInChildrenLssThn5Years", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfNewCLCasesInChildren5To14Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewCLCasesInChildren5To14Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewCLCasesInChildren5To14Years", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfNewCLCasesInAdultsGrtrThn14Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewCLCasesInAdultsGrtrThn14Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewCLCasesInAdultsGrtrThn14Years", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfNewPKDLCasesInChildrenLssThn5Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewPKDLCasesInChildrenLssThn5Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewPKDLCasesInChildrenLssThn5Years", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfNewPKDLCasesInChildren5To14Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewPKDLCasesInChildren5To14Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewPKDLCasesInChildren5To14Years", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfNewPKDLCasesInAdultsGrtrThn14Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewPKDLCasesInAdultsGrtrThn14Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewPKDLCasesInAdultsGrtrThn14Years", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfNewMCLCasesInChildrenLssThn5Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewMCLCasesInChildrenLssThn5Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewMCLCasesInChildrenLssThn5Years", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewMCLCasesDiagnosed", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfNewMCLCasesInChildren5To14Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewMCLCasesInChildren5To14Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewMCLCasesInChildren5To14Years", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewMCLCasesDiagnosed", relatedValues)
                            ));
                    case "27LeishMontIntvPrcntOfNewMCLCasesInAdultsGrtrThn14Years":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvPrcntOfNewMCLCasesInAdultsGrtrThn14Years,
                            GetPercentageWithRatio(
                                GetValueOrDefault("27LeishMontIntvNumberOfNewMCLCasesInAdultsGrtrThn14Years", relatedValues),
                                GetValueOrDefault("27LeishMontIntvTotalNumberOfNewMCLCasesDiagnosed", relatedValues)
                            ));
                    case "27LeishMontIntvMonthlyConsumptionRate1StLineTreatmentUnits":
                        return new KeyValuePair<string, string>(Translations.LeishMontIntvMonthlyConsumptionRate1StLineTreatmentUnits,
                            GetDifference(
                                GetValueOrDefault("27LeishMontIntvNumberOfUnitsVialsFor1StLineTreatmentAtTheBeginningOfTheMonth", relatedValues),
                                GetValueOrDefault("27LeishMontIntvNumberOfUnitsVialsFor1StLineTreatmentAtTheEndOfTheMonth", relatedValues)
                            ));
                    default:
                        return CheckEachIntvType(relatedValues, field, demo.AdminLevelId, start, end, ref errors);
                }
            }
            catch (Exception)
            {
            }

            return new KeyValuePair<string, string>(field, Translations.CalculationError);
        }

        private KeyValuePair<string, string> CheckEachIntvType(Dictionary<string, string> relatedValues, string field, int adminLevelId, DateTime start, DateTime end, ref string errors)
        {
            for (int i = 10; i < 24; i++)
            {
                KeyValuePair<string, string> result = CheckPcIntvCalculations(relatedValues, field, i, adminLevelId, start, end, ref errors);
                if (result.Key != null)
                    return result;
            }
            return new KeyValuePair<string, string>(field, Translations.NA);
        }

        private KeyValuePair<string, string> CheckPcIntvCalculations(Dictionary<string, string> relatedValues, string field, int intvTypeId, int adminLevelId, DateTime start, DateTime end, ref string errors)
        {
            if (field == intvTypeId + "PcIntvProgramCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvProgramCoverage, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumIndividualsTreated", relatedValues), GetValueOrDefault(intvTypeId + "PcIntvNumEligibleIndividualsTargeted", relatedValues)));
            if (field == intvTypeId + "PcIntvFemalesTreatedProportion")
                return new KeyValuePair<string, string>(Translations.PcIntvFemalesTreatedProportion, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumFemalesTreated", relatedValues), GetValueOrDefault(intvTypeId + "PcIntvNumIndividualsTreated", relatedValues)));
            if (field == intvTypeId + "PcIntvMalesTreatedProportion")
                return new KeyValuePair<string, string>(Translations.PcIntvMalesTreatedProportion, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumMalesTreated", relatedValues), GetValueOrDefault(intvTypeId + "PcIntvNumIndividualsTreated", relatedValues)));
            if (field == intvTypeId + "PcIntvPsacCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvPsacCoverage, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvPsacTreated", relatedValues), GetValueOrDefault(intvTypeId + "PcIntvNumPsacTargeted", relatedValues)));
            if (field == intvTypeId + "PcIntvSacCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvSacCoverage, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumSacTreated", relatedValues), GetValueOrDefault(intvTypeId + "PcIntvNumSacTargeted", relatedValues)));
            // EPI Meta data
            if (field == intvTypeId + "PcIntvSthPopReqPc")
                return new KeyValuePair<string, string>(Translations.PcIntvSthPopReqPc, GetRecentDistroIndicator(adminLevelId, "DDSTHPopulationRequiringPc", DiseaseType.STH, start, end, ref errors));
            if (field == intvTypeId + "PcIntvSthPsacAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvSthPsacAtRisk, GetRecentDistroIndicator(adminLevelId, "DDSTHPsacAtRisk", DiseaseType.STH, start, end, ref errors));
            if (field == intvTypeId + "PcIntvSthSacAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvSthSacAtRisk, GetRecentDistroIndicator(adminLevelId, "DDSTHSacAtRisk", DiseaseType.STH, start, end, ref errors));
            if (field == intvTypeId + "PcIntvSthAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvSthAtRisk, GetRecentDistroIndicator(adminLevelId, "DDSTHPopulationAtRisk", DiseaseType.STH, start, end, ref errors));
            if (field == intvTypeId + "PcIntvLfAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvLfAtRisk, GetRecentDistroIndicator(adminLevelId, "DDLFPopulationAtRisk", DiseaseType.Lf, start, end, ref errors));
            if (field == intvTypeId + "PcIntvLfPopRecPc")
                return new KeyValuePair<string, string>(Translations.PcIntvLfPopRecPc, GetRecentDistroIndicator(adminLevelId, "DDLFPopulationRequiringPc", DiseaseType.Lf, start, end, ref errors));
            if (field == intvTypeId + "PcIntvOnchoAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvOnchoAtRisk, GetRecentDistroIndicator(adminLevelId, "DDOnchoPopulationAtRisk", DiseaseType.Oncho, start, end, ref errors));
            if (field == intvTypeId + "PcIntvOnchoPopReqPc")
                return new KeyValuePair<string, string>(Translations.PcIntvOnchoPopReqPc, GetRecentDistroIndicator(adminLevelId, "DDOnchoPopulationRequiringPc", DiseaseType.Oncho, start, end, ref errors));
            if (field == intvTypeId + "PcIntvSchAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvSchAtRisk, GetRecentDistroIndicator(adminLevelId, "DDSchistoPopulationAtRisk", DiseaseType.Schisto, start, end, ref errors));
            if (field == intvTypeId + "PcIntvSchPopReqPc")
                return new KeyValuePair<string, string>(Translations.PcIntvSchPopReqPc, GetRecentDistroIndicator(adminLevelId, "DDSchistoPopulationRequiringPc", DiseaseType.Schisto, start, end, ref errors));
            if (field == intvTypeId + "PcIntvSchSacAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvSchSacAtRisk, GetRecentDistroIndicator(adminLevelId, "DDSchistoSacAtRisk", DiseaseType.Schisto, start, end, ref errors));
            if (field == intvTypeId + "PcIntvTraAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvTraAtRisk, GetRecentDistroIndicator(adminLevelId, "DDTraPopulationAtRisk", DiseaseType.Trachoma, start, end, ref errors));

            // epi calcs
            if (field == intvTypeId + "PcIntvSthPsacEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvSthPsacEpiCoverage, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvPsacTreated", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDSTHPsacAtRisk", DiseaseType.STH, start, end, ref errors)));
            if (field == intvTypeId + "PcIntvSthSacEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvSthSacEpiCoverage, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumSacTreated", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDSTHSacAtRisk", DiseaseType.STH, start, end, ref errors)));
            if (field == intvTypeId + "PcIntvSthEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvSthEpiCoverage, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumIndividualsTreated", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDSTHPopulationAtRisk", DiseaseType.STH, start, end, ref errors)));
            if (field == intvTypeId + "PcIntvLfEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvLfEpiCoverage, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumIndividualsTreated", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDLFPopulationAtRisk", DiseaseType.Lf, start, end, ref errors)));
            if (field == intvTypeId + "PcIntvOnchoEpiCoverageOfOncho")
                return new KeyValuePair<string, string>(Translations.PcIntvOnchoEpiCoverageOfOncho, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvOfTotalTreatedForOncho", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDOnchoPopulationAtRisk", DiseaseType.Oncho, start, end, ref errors)));
            if (field == intvTypeId + "PcIntvOnchoEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvOnchoEpiCoverage, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumIndividualsTreated", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDOnchoPopulationAtRisk", DiseaseType.Oncho, start, end, ref errors)));
            if (field == intvTypeId + "PcIntvOnchoProgramCov")
                return new KeyValuePair<string, string>(Translations.PcIntvOnchoProgramCov, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvOfTotalTreatedForOncho", relatedValues), GetValueOrDefault(intvTypeId + "PcIntvOfTotalTargetedForOncho", relatedValues)));
            if (field == intvTypeId + "PcIntvSchSacEpi")
                return new KeyValuePair<string, string>(Translations.PcIntvSchSacEpi, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumSacTreated", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDSchistoSacAtRisk", DiseaseType.Schisto, start, end, ref errors)));
            if (field == intvTypeId + "PcIntvSchEpi")
                return new KeyValuePair<string, string>(Translations.PcIntvSchEpi, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumIndividualsTreated", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDSchistoPopulationAtRisk", DiseaseType.Schisto, start, end, ref errors)));
            if (field == intvTypeId + "PcIntvTraEpi")
                return new KeyValuePair<string, string>(Translations.PcIntvTraEpi,
                    GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumIndividualsTreated", relatedValues), GetRecentDistroIndicator(adminLevelId, "DDTraPopulationAtRisk", DiseaseType.Trachoma, start, end, ref errors)));

            return new KeyValuePair<string, string>(null, null);
        }
    }
}
