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
        private DemoRepository demoRepo = new DemoRepository();

        public override List<KeyValuePair<string, string>> GetCalculatedValues(List<string> fields, Dictionary<string, string> relatedValues, int adminLevel)
        {
            AdminLevelDemography recentDemo = null;
            var demography = demoRepo.GetAdminLevelDemography(adminLevel);
            var recentDemogInfo = demography.OrderByDescending(d => d.Year).FirstOrDefault();
            if (recentDemogInfo != null)
                recentDemo = demoRepo.GetDemoById(recentDemogInfo.Id);
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();

            foreach (string field in fields)
                results.Add(GetCalculatedValues(field, relatedValues, recentDemo));
            return results;
        }

        private KeyValuePair<string, string> GetCalculatedValues(string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo)
        {
            try
            {
                switch (field)
                {
                    case "8PercentCoverageBu":
                        if (demo != null)
                            return new KeyValuePair<string, string>(Translations.PercentCoverageBu, GetPercentage(relatedValues["8NumFacilitiesProvidingBu"], demo.PopAdult.ToString()));
                        break;
                    case "4NumImported":
                        return new KeyValuePair<string, string>(Translations.NumImported, GetDifference(relatedValues["4NumClinical"], relatedValues["4NumIndigenous"]));
                    case "4PercentVas":
                        return new KeyValuePair<string, string>(Translations.PercentVas, GetPercentage(relatedValues["4VasReporting"], relatedValues["4NumVas"]));
                    case "4PercentIdsr":
                        return new KeyValuePair<string, string>(Translations.PercentIdsr, GetPercentage(relatedValues["4NumIdsrReporting"], relatedValues["4NumIdsr"]));
                    case "4PercentRumorsInvestigated":
                        return new KeyValuePair<string, string>(Translations.PercentRumorsInvestigated, GetPercentage(relatedValues["4NumRumorsInvestigated"], relatedValues["4NumRumors"]));
                    case "4PercentCasesContained":
                        return new KeyValuePair<string, string>(Translations.PercentCasesContained, GetPercentage(relatedValues["4NumCasesContained"], relatedValues["4NumClinical"]));
                    case "4PercentEndemicReporting":
                        return new KeyValuePair<string, string>(Translations.PercentEndemicReporting, GetPercentage(relatedValues["4NumEndemicVillagesReporting"], relatedValues["4NumEndemicVillages"]));
                    case "7DetectRate100kLeish":
                        if (demo != null)
                            return new KeyValuePair<string, string>(Translations.DetectRate100kLeish, GetPercentage(GetTotal(relatedValues["7NumClCases"], relatedValues["7NumLabVlCases"], relatedValues["7NumClVlCases"]), 
                                demo.TotalPopulation.ToString(), 100000));
                        break;
                    case "7PercentLabConfirm":
                        return new KeyValuePair<string, string>(Translations.PercentLabConfirm, GetPercentage(GetTotal(relatedValues["7NumLabClCases"], relatedValues["7NumLabVlCases"]),
                            GetTotal(relatedValues["7NumClCases"], relatedValues["7NumLabVlCases"], relatedValues["7NumClVlCases"])));
                    case "7PercentCl":
                        return new KeyValuePair<string, string>(Translations.PercentCl, GetPercentage(relatedValues["7NumClCases"],
                            GetTotal(relatedValues["7NumClCases"], relatedValues["7NumLabVlCases"], relatedValues["7NumClVlCases"])));
                    case "7PercentVl":
                        return new KeyValuePair<string, string>(Translations.PercentVl, GetPercentage(relatedValues["7NumLabVlCases"],
                            GetTotal(relatedValues["7NumClCases"], relatedValues["7NumLabVlCases"], relatedValues["7NumClVlCases"])));
                    case "7PercentClVl":
                        return new KeyValuePair<string, string>(Translations.PercentClVl, GetPercentage(relatedValues["7NumClVlCases"],
                            GetTotal(relatedValues["7NumClCases"], relatedValues["7NumLabVlCases"], relatedValues["7NumClVlCases"])));
                    case "7PercentCasesActiveLeish":
                        return new KeyValuePair<string, string>(Translations.PercentCasesActiveLeish, GetPercentage(relatedValues["7NumCasesFoundActively"],
                            GetTotal(relatedValues["7NumClCases"], relatedValues["7NumLabVlCases"], relatedValues["7NumClVlCases"])));
                    case "6PercentLabConfirmed":
                        return new KeyValuePair<string, string>(Translations.PercentLabConfirmed, GetPercentage(relatedValues["6NumLabCases"], relatedValues["6NumClinicalCasesHat"]));
                    case "6PercentTGamb":
                        return new KeyValuePair<string, string>(Translations.PercentTGamb, GetPercentage(relatedValues["6NumTGamb"], relatedValues["6NumLabCases"]));
                    case "6PercentTRhod":
                        return new KeyValuePair<string, string>(Translations.PercentTRhod, GetPercentage(relatedValues["6NumTRhod"], relatedValues["6NumLabCases"]));
                    case "6PercentTGambTRhod":
                        return new KeyValuePair<string, string>(Translations.PercentTGambTRhod, GetPercentage(relatedValues["6NumTGambTRhod"], relatedValues["6NumClinicalCasesHat"]));
                    case "6PercentCasesActivelyFound":
                        return new KeyValuePair<string, string>(Translations.PercentCasesActivelyFound, GetPercentage(relatedValues["6NumProspection"], relatedValues["6NumClinicalCasesHat"]));
                    case "6CureRate":
                        return new KeyValuePair<string, string>(Translations.CureRate, GetPercentage(relatedValues["6NumCasesCured"], relatedValues["6NumCasesTreated"]));
                    case "6PercentTreatmentFailure":
                        return new KeyValuePair<string, string>(Translations.PercentTreatmentFailure, GetPercentage(relatedValues["6NumTreatmentFailures"], relatedValues["6NumCasesTreated"]));
                    case "6PercentSae":
                        return new KeyValuePair<string, string>(Translations.PercentSae, GetPercentage(relatedValues["6NumCasesSaes"], relatedValues["6NumCasesTreated"]));
                    case "6FatalityRate":
                        return new KeyValuePair<string, string>(Translations.FatalityRate, GetPercentage(relatedValues["6NumDeaths"], relatedValues["6NumClinicalCasesHat"]));
                    case "6DetectionRatePer100k":
                        if (demo != null)
                            return new KeyValuePair<string, string>(Translations.DetectionRatePer100k, GetPercentage(relatedValues["6NumClinicalCasesHat"], demo.TotalPopulation.ToString(), 100000));
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
                        return new KeyValuePair<string, string>(Translations.PercentTreatedYw, GetPercentage(relatedValues["9NumContactsTreatedYw"],
                            GetTotal(relatedValues["9NumContactsTreatedYw"], relatedValues["9NumCasesTreatedYaws"])));
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
                return new KeyValuePair<string, string>(Translations.PcIntvProgramCoverage, GetPercentage(relatedValues[intvTypeId + "PcIntvNumIndividualsTreated"], relatedValues[intvTypeId + "PcIntvNumEligibleIndividualsTargeted"]));
            if (field == intvTypeId + "PcIntvEpiCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvProgramCoverage,Translations.CalculationError);
                //Note need to fix this row in the indicators calcs table	
                //ID IndicatorId	EntityTypeId	RelatedIndicatorId	RelatedEntityTypeId
                //181	253	2	238	2
                //# individuals treated/# individuals at-risk (comes from disease distribution
                //return GetPercentage(relatedValues[intvTypeId + "PcIntvNumIndividualsTreated"], relatedValues[intvTypeId + "x"]); // # individuals at-risk
            if (field == intvTypeId + "PcIntvFemalesTreatedProportion")
                return new KeyValuePair<string, string>(Translations.PcIntvFemalesTreatedProportion,GetPercentage(relatedValues[intvTypeId + "PcIntvNumFemalesTreated"], relatedValues[intvTypeId + "PcIntvNumIndividualsTreated"]));
            if (field == intvTypeId + "PcIntvMalesTreatedProportion")
                return new KeyValuePair<string, string>(Translations.PcIntvMalesTreatedProportion,GetPercentage(relatedValues[intvTypeId + "PcIntvNumMalesTreated"], relatedValues[intvTypeId + "PcIntvNumIndividualsTreated"]));
            if (field == intvTypeId + "PcIntvPsacCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvPsacCoverage,GetPercentage(relatedValues[intvTypeId + "PcIntvPsacTreated"], relatedValues[intvTypeId + "PcIntvNumPsacTargeted"]));
            if (field == intvTypeId + "PcIntvSacCoverage")
                return new KeyValuePair<string, string>(Translations.PcIntvSacCoverage,GetPercentage(relatedValues[intvTypeId + "PcIntvNumSacTreated"], relatedValues[intvTypeId + "PcIntvNumSacTargeted"]));
            return new KeyValuePair<string, string>(null, null);
        }
    }
}
