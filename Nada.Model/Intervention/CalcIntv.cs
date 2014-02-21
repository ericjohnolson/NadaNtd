using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Repositories;

namespace Nada.Model.Intervention
{
    public class CalcIntv : CalcBase, ICalcIndicators
    {

        public override List<KeyValuePair<string, string>> GetCalculatedValues(List<string> fields, Dictionary<string, string> relatedValues, int adminLevel)
        {
            AdminLevelDemography recentDemo = null;
            var demography = demoRepo.GetAdminLevelDemography(adminLevel);
            var recentDemogInfo = demography.OrderByDescending(d => d.DateReported).FirstOrDefault();
            if (recentDemogInfo != null)
                recentDemo = demoRepo.GetDemoById(recentDemogInfo.Id);
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();

            foreach (string field in fields)
                results.Add(GetCalculatedValue(field, relatedValues, recentDemo));
            return results;
        }

        public override KeyValuePair<string, string> GetCalculatedValue(string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo)
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
                    //DiseaseDistroCm distro = distros.GetDistroByAdminLevelYear(adminLevel, yearOfReporting, (int)DiseaseType.Leprosy));
                    //if (distro != null)
                    //{
                    //    L5 = Util.ParseIndicatorDouble(inds, "TotalNumNewCases"));
                    //    L6 = Util.ParseIndicatorDouble(inds, "TotalNumMbCases"));
                    //    L9 = Util.ParseIndicatorDouble(inds, "PrevalenceBeginningYear"));
                    //    L10 = Util.ParseIndicatorDouble(inds, "MbCasesRegisteredMdtBeginning"));
                    //    L12 = Util.ParseIndicatorDouble(inds, "MbCasesRegisteredMdtEnd"));
                    //}
                    case "5PercentNewGrade2":
                        return new KeyValuePair<string, string>(Translations.PercentNewGrade2, Translations.CalculationError); //L48	% of New Grade II	PERCENT	calc: L23/L5
                    case "5PrevalenceDetectionRatio":
                        return new KeyValuePair<string, string>(Translations.PrevalenceDetectionRatio, Translations.CalculationError); //L49	Prevalence detection ratio	number	calc: L12/L5
                    case "5PercentCureRateMb":
                        return new KeyValuePair<string, string>(Translations.PercentCureRateMb, Translations.CalculationError); //L51	Cure rate of previous year MB cases	PERCENT	calc: L40/L10
                    case "5PercentCureRatePb":
                        return new KeyValuePair<string, string>(Translations.PercentCureRatePb, Translations.CalculationError); //L52	PB Cured rate during the current year  	PERCENT	calc: L41/(((L5-L6)/2)+(L9-l10))
                    case "5PercentCoverageMdt":
                        return new KeyValuePair<string, string>(Translations.PercentCoverageMdt, Translations.CalculationError); //L50	% of Health facility coverage for MDT	PERCENT	calc: L28/number of health facilities (from demography)
                    case "9PercentPsacYw":
                        return new KeyValuePair<string, string>(Translations.PercentPsacYw, Translations.CalculationError); //Y41	% of pre-school age cases	percent		Y7/Y5
                    case "9PercentSacYw":
                        return new KeyValuePair<string, string>(Translations.PercentSacYw, Translations.CalculationError); //Y42	% of school-age cases	percent		Y8/Y5
                    case "9PercentScreenedYw":
                        return new KeyValuePair<string, string>(Translations.PercentScreenedYw, Translations.CalculationError); //Y45	% cases among screened populations (per 10,000)	percent		Y5*100)/Y18
                    case "9PercentTreatedAmongDetected":
                        return new KeyValuePair<string, string>(Translations.PercentTreatedAmongDetected, Translations.CalculationError); //Y46	% treated among detected cases	percent		Y21/Y5
                    case "9DetectRate100kYw":
                        return new KeyValuePair<string, string>(Translations.DetectRate100kYw, Translations.CalculationError); //Y39	Detection rate per 100 000	number		Y5/total population (demography)*100000)
                    case "9PercentNewCasesLabYw":
                        return new KeyValuePair<string, string>(Translations.PercentNewCasesLabYw, Translations.CalculationError);  //Y40	% of new cases confirmed by lab test	percent		Y5.5/total population (demography)*100000)
                    case "9PercentTreatedYw":
                        return new KeyValuePair<string, string>(Translations.PercentTreatedYw, GetPercentage(GetValueOrDefault("9NumContactsTreatedYw", relatedValues),
                            GetTotal(GetValueOrDefault("9NumContactsTreatedYw", relatedValues), GetValueOrDefault("9NumCasesTreatedYaws", relatedValues))));
                    default:
                        return CheckEachIntvType(relatedValues, field);
                }
            }
            catch (Exception)
            {
            }
                return new KeyValuePair<string,string>(field, Translations.CalculationError);
        }

        private KeyValuePair<string, string> CheckEachIntvType(Dictionary<string, string> relatedValues, string field)
        {
            for (int i = 10; i < 24; i++)
            {
                KeyValuePair<string, string> result = CheckPcIntvCalculations(relatedValues, field, i);
                if (result.Key != null)
                    return result;
            }
            return new KeyValuePair<string, string>(field, Translations.NA);
        }

        private KeyValuePair<string, string> CheckPcIntvCalculations(Dictionary<string, string> relatedValues, string field, int intvTypeId)
        {
            if (field == intvTypeId + "PcIntvProgramCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvProgramCoverage, GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumIndividualsTreated", relatedValues), GetValueOrDefault(intvTypeId + "PcIntvNumEligibleIndividualsTargeted", relatedValues)));
            if (field == intvTypeId + "PcIntvEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvEpiCoverage, Translations.CalculationError);
                //Note need to fix this row in the indicators calcs table	
                //ID IndicatorId	EntityTypeId	RelatedIndicatorId	RelatedEntityTypeId
                //181	253	2	238	2
                //# individuals treated/# individuals at-risk (comes from disease distribution
                //return GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumIndividualsTreated", relatedValues), GetValueOrDefault(intvTypeId + "x", relatedValues)); // # individuals at-risk
            if (field == intvTypeId + "PcIntvFemalesTreatedProportion")
                return new KeyValuePair<string, string>(Translations.PcIntvFemalesTreatedProportion,GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumFemalesTreated", relatedValues), GetValueOrDefault(intvTypeId + "PcIntvNumIndividualsTreated", relatedValues)));
            if (field == intvTypeId + "PcIntvMalesTreatedProportion")
                return new KeyValuePair<string, string>(Translations.PcIntvMalesTreatedProportion,GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumMalesTreated", relatedValues), GetValueOrDefault(intvTypeId + "PcIntvNumIndividualsTreated", relatedValues)));
            if (field == intvTypeId + "PcIntvPsacCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvPsacCoverage,GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvPsacTreated", relatedValues), GetValueOrDefault(intvTypeId + "PcIntvNumPsacTargeted", relatedValues)));
            if (field == intvTypeId + "PcIntvSacCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvSacCoverage,GetPercentage(GetValueOrDefault(intvTypeId + "PcIntvNumSacTreated", relatedValues), GetValueOrDefault(intvTypeId + "PcIntvNumSacTargeted", relatedValues)));
            return new KeyValuePair<string, string>(null, null);
        }
    }
}
