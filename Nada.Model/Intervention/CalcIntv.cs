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
                    GetPercentage(GetTotal(GetValueOrDefault(intvTypeId + "PcIntvNumTreatedZx", relatedValues), GetValueOrDefault(intvTypeId + "PcIntvNumTreatedTeo", relatedValues), GetValueOrDefault(intvTypeId + "PcIntvNumTreatedZxPos", relatedValues)),
                        GetRecentDistroIndicator(adminLevelId, "DDTraPopulationAtRisk", DiseaseType.Trachoma, start, end, ref errors)));

            return new KeyValuePair<string, string>(null, null);
        }
    }
}
