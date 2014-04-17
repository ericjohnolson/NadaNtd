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
        public override List<KeyValuePair<string, string>> GetMetaData(IEnumerable<string> fields, int adminLevel)
        {
            var relatedValues = new Dictionary<string, string>();
            AdminLevelDemography recentDemo = GetDemography(adminLevel, null);
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();
            foreach (string field in fields)
                results.Add(GetCalculatedValue(field, relatedValues, recentDemo, null));
            return results;
        }

        public override List<KeyValuePair<string, string>> GetCalculatedValues(List<string> fields, Dictionary<string, string> relatedValues, int adminLevel, DateTime? end)
        {
            AdminLevelDemography recentDemo = GetDemography(adminLevel, null);
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();
            foreach (string field in fields)
                results.Add(GetCalculatedValue(field, relatedValues, recentDemo, end));
            return results;
        }

        public override KeyValuePair<string, string> GetCalculatedValue(string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime? end)
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
                        return CheckEachIntvType(relatedValues, field, demo.AdminLevelId, end);
                }
            }
            catch (Exception)
            {
            }

            return new KeyValuePair<string, string>(field, Translations.CalculationError);
        }

        private KeyValuePair<string, string> CheckEachIntvType(Dictionary<string, string> relatedValues, string field, int adminLevelId, DateTime? end)
        {
            for (int i = 10; i < 24; i++)
            {
                KeyValuePair<string, string> result = CheckPcIntvCalculations(relatedValues, field, i, adminLevelId, end);
                if (result.Key != null)
                    return result;
            }
            return new KeyValuePair<string, string>(field, Translations.NA);
        }

        private KeyValuePair<string, string> CheckPcIntvCalculations(Dictionary<string, string> relatedValues, string field, int intvTypeId, int adminLevelId, DateTime? endDate)
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
                return new KeyValuePair<string, string>(Translations.PcIntvSthPopReqPc, GetSthDd(adminLevelId, "DDSTHPopulationRequiringPc", endDate));
            if (field == intvTypeId + "PcIntvSthPsacAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvSthPsacAtRisk, GetSthDd(adminLevelId, "DDSTHPsacAtRisk", endDate));
            if (field == intvTypeId + "PcIntvSthSacAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvSthSacAtRisk, GetSthDd(adminLevelId, "DDSTHSacAtRisk", endDate));
            if (field == intvTypeId + "PcIntvSthAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvSthAtRisk, GetSthDd(adminLevelId, "DDSTHPopulationAtRisk", endDate));
            if (field == intvTypeId + "PcIntvLfAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvLfAtRisk, GetLfDd(adminLevelId, "DDLFPopulationAtRisk", endDate));
            if (field == intvTypeId + "PcIntvLfPopRecPc")
                return new KeyValuePair<string, string>(Translations.PcIntvLfPopRecPc, GetLfDd(adminLevelId, "DDLFPopulationRequiringPc", endDate));
            if (field == intvTypeId + "PcIntvOnchoAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvOnchoAtRisk, GetOnchoDd(adminLevelId, "DDOnchoPopulationAtRisk", endDate));
            if (field == intvTypeId + "PcIntvOnchoPopReqPc")
                return new KeyValuePair<string, string>(Translations.PcIntvOnchoPopReqPc, GetOnchoDd(adminLevelId, "DDOnchoPopulationRequiringPc", endDate));
            if (field == intvTypeId + "PcIntvSchAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvSchAtRisk, GetSchDd(adminLevelId, "DDSchistoPopulationAtRisk", endDate));
            if (field == intvTypeId + "PcIntvSchPopReqPc")
                return new KeyValuePair<string, string>(Translations.PcIntvSchPopReqPc, GetSchDd(adminLevelId, "DDSchistoPopulationRequiringPc", endDate));
            if (field == intvTypeId + "PcIntvSchSacAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvSchSacAtRisk, GetSchDd(adminLevelId, "DDSchistoSacAtRisk", endDate));
            if (field == intvTypeId + "PcIntvTraAtRisk")
                return new KeyValuePair<string, string>(Translations.PcIntvTraAtRisk, GetTrachomaDd(adminLevelId, "DDTraPopulationAtRisk", endDate));

            // epi calcs
            if (field == intvTypeId + "PcIntvSthPsacEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvSthPsacEpiCoverage, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvPsacTreated", relatedValues), GetSthDd(adminLevelId, "DDSTHPsacAtRisk", endDate)));
            if (field == intvTypeId + "PcIntvSthSacEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvSthSacEpiCoverage, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumSacTreated", relatedValues), GetSthDd(adminLevelId, "DDSTHSacAtRisk", endDate)));
            if (field == intvTypeId + "PcIntvSthEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvSthEpiCoverage, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumIndividualsTreated", relatedValues), GetSthDd(adminLevelId, "DDSTHPopulationAtRisk", endDate)));
            if (field == intvTypeId + "PcIntvLfEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvLfEpiCoverage, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumIndividualsTreated", relatedValues), GetLfDd(adminLevelId, "DDLFPopulationAtRisk", endDate)));
            if (field == intvTypeId + "PcIntvOnchoEpiCoverageOfOncho")
                return new KeyValuePair<string, string>(Translations.PcIntvOnchoEpiCoverageOfOncho, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvOfTotalTreatedForOncho", relatedValues), GetOnchoDd(adminLevelId, "DDOnchoPopulationAtRisk", endDate)));
            if (field == intvTypeId + "PcIntvOnchoEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvOnchoEpiCoverage, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumIndividualsTreated", relatedValues), GetOnchoDd(adminLevelId, "DDOnchoPopulationAtRisk", endDate)));
            if (field == intvTypeId + "PcIntvOnchoProgramCov")
                return new KeyValuePair<string, string>(Translations.PcIntvOnchoProgramCov, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvOfTotalTreatedForOncho", relatedValues), GetValueOrDefault(intvTypeId + "PcIntvOfTotalTargetedForOncho", relatedValues)));
            if (field == intvTypeId + "PcIntvSchSacEpi")
                return new KeyValuePair<string, string>(Translations.PcIntvSchSacEpi, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumSacTreated", relatedValues), GetSchDd(adminLevelId, "DDSchistoSacAtRisk", endDate)));
            if (field == intvTypeId + "PcIntvSchEpi")
                return new KeyValuePair<string, string>(Translations.PcIntvSchEpi, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumIndividualsTreated", relatedValues), GetSchDd(adminLevelId, "DDSchistoPopulationAtRisk", endDate)));
            if (field == intvTypeId + "PcIntvTraEpi")
                return new KeyValuePair<string, string>(Translations.PcIntvTraEpi,
                    GetPercentage(GetTotal(GetValueOrDefault(intvTypeId + "NumClCases", relatedValues), GetValueOrDefault(intvTypeId + "PcIntvNumTreatedTeo", relatedValues), GetValueOrDefault(intvTypeId + "PcIntvNumTreatedZxPos", relatedValues)),
                        GetTrachomaDd(adminLevelId, "DDTraPopulationAtRisk", endDate)));

            return new KeyValuePair<string, string>(null, null);
        }
    }
}
