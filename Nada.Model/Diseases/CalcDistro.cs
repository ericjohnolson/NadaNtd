using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Repositories;

namespace Nada.Model.Diseases
{
    public class CalcDistro : CalcBase, ICalcIndicators
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
                    case "11PercentNewChildren":
                        return new KeyValuePair<string,string>(Translations.TotalNumChildNewCases,
                            GetPercentage(relatedValues["11TotalNumChildNewCases"], relatedValues["11TotalNumNewCases"]));
                    case "11PercentNewFemales":
                        return new KeyValuePair<string,string>(Translations.PercentNewFemales,  GetPercentage(relatedValues["11TotalNumFemaleNewCases"], relatedValues["11TotalNumNewCases"]));
                    case "7PercentNewChildren":
                        return new KeyValuePair<string,string>(Translations.PercentNewChildren,  GetPercentage(relatedValues["7TotalNumNewCases"], relatedValues["7TotalNumNewCases"]));
                    case "7PercentNewFemales":
                        return new KeyValuePair<string,string>(Translations.PercentNewFemales,  GetPercentage(relatedValues["7TotalNumNewCases"], relatedValues["7TotalNumNewCases"]));
                    case "7TotalNumCasesRegistered":
                        //if (L5 >= 0 && L24 >= 0 && L25 >= 0 && l9 >= 0)
                        //    calcs.Add(new KeyValuePair<string, string>("TotalNumCasesRegistered", string.Format("{0}", L5 + L24 + L25 + l9).ToString())));
                        return new KeyValuePair<string,string>(Translations.TotalNumCasesRegistered,  Translations.CalculationError);
                    case "7PercentNewMb":
                        return new KeyValuePair<string,string>(Translations.PercentNewMb,  GetPercentage(relatedValues["7TotalNumMbCases"], relatedValues["7TotalNumNewCases"]));
                    case "7PrevalenceRateEndOfYear":
                        if (demo != null)
                            return new KeyValuePair<string,string>(Translations.PrevalenceRateEndOfYear,  GetPercentage(relatedValues["7MbCasesRegisteredMdtEnd"], demo.TotalPopulation.ToString(), 100000));
                        break;
                    case "7RateNewGrade2":
                        //L15	Rate of New Grade II per 1 million population	number	CALC: L23 (Total number of new cases with Grade II disabilities)/total population
                        return new KeyValuePair<string,string>(Translations.RateNewGrade2,  Translations.CalculationError);
                    case "7DetectionRate100k":
                        if (demo != null)
                            return new KeyValuePair<string, string>(Translations.DetectionRate100k, GetPercentage(relatedValues["7TotalNumNewCases"], demo.TotalPopulation.ToString(), 100000));
                        break;
                    case "9PercentNewChildren":
                        return new KeyValuePair<string, string>(Translations.PercentNewChildren, GetPercentage(relatedValues["9TotalNumChildNewCases"], relatedValues["9TotalNumNewCases"]));
                    case "9PercentNewFemales":
                        return new KeyValuePair<string, string>(Translations.PercentNewFemales, GetPercentage(relatedValues["9TotalNumFemaleNewCases"], relatedValues["9TotalNumNewCases"]));
                    case "8PercentCasesHat2":
                        return new KeyValuePair<string, string>(Translations.PercentCasesHat2, GetPercentage(relatedValues["8TotalNumHat2"], relatedValues["8TotalNumHat"]));
                    case "8PercentNewChildren":
                        return new KeyValuePair<string, string>(Translations.PercentNewChildren, GetPercentage(relatedValues["8TotalNumHatChild"], relatedValues["8TotalNumHat"]));
                    case "8PercentNewFemales":
                        return new KeyValuePair<string, string>(Translations.PercentNewFemales, GetPercentage(relatedValues["8TotalNumHatFemale"], relatedValues["8TotalNumHat"]));
                    case "10PercentNewChildren":
                        return new KeyValuePair<string, string>(Translations.PercentNewChildren, GetPercentage(relatedValues["10TotalNumChildNewCases"], relatedValues["10TotalNumNewCases"]));
                    case "10PercentNewFemales":
                        return new KeyValuePair<string, string>(Translations.PercentNewFemales, GetPercentage(relatedValues["10TotalNumFemaleNewCases"], relatedValues["10TotalNumNewCases"]));
                    case "10PercentUlcerativeCases":
                        return new KeyValuePair<string, string>(Translations.PercentUlcerativeCases, GetPercentage(relatedValues["10TotalUlcerativeCases"], relatedValues["10TotalNumNewCases"]));
                    case "10PercentCatICases":
                        return new KeyValuePair<string, string>(Translations.PercentCatICases, GetPercentage(relatedValues["10TotalCat1Cases"], relatedValues["10TotalNumNewCases"]));
                    case "10PercentCatIICases":
                        return new KeyValuePair<string, string>(Translations.PercentCatIICases, GetPercentage(relatedValues["10TotalCat3Cases"], relatedValues["10TotalNumNewCases"]));
                    case "10PercentPcrCases":
                        return new KeyValuePair<string, string>(Translations.PercentPcrCases, GetPercentage(relatedValues["10TotalCasesConfirmedPcr"], relatedValues["10TotalNumNewCases"]));
                    case "10DetectionRate100k":
                        if(demo != null)
                            return new KeyValuePair<string, string>(Translations.DetectionRate100k, GetPercentage(relatedValues["10TotalNumNewCases"], demo.TotalPopulation.ToString(), 100000));
                        break;
                    case "4DDOnchoTotalPopulation":
                        return new KeyValuePair<string, string>(Translations.DDOnchoTotalPopulation, demo.TotalPopulation.ToString());
                    case "3DDLFTotalPopulation":
                        return new KeyValuePair<string, string>(Translations.DDLFTotalPopulation, demo.TotalPopulation.ToString());
                    case "3DDLFPsacPopulation":
                        return new KeyValuePair<string, string>(Translations.DDLFPsacPopulation, demo.PopPsac.ToString());
                    case "3DDLFSacPopulation":
                        return new KeyValuePair<string, string>(Translations.DDLFSacPopulation, demo.PopSac.ToString());
                    case "3DDLFAdultPopulation":
                        return new KeyValuePair<string, string>(Translations.DDLFAdultPopulation, demo.PopAdult.ToString());
                    case "12DDSchistoTotalPopulation":
                        return new KeyValuePair<string, string>(Translations.DDSchistoTotalPopulation, demo.TotalPopulation.ToString());
                    case "12DDSchistoSacPopulation":
                        return new KeyValuePair<string, string>(Translations.DDSchistoSacPopulation, demo.PopSac.ToString());
                    case "12DDSchistoAdultPopulation":
                        return new KeyValuePair<string, string>(Translations.DDSchistoAdultPopulation, demo.PopAdult.ToString());
                    case "5DDSTHTotalPopulation":
                        return new KeyValuePair<string, string>(Translations.DDSTHTotalPopulation, demo.TotalPopulation.ToString());
                    case "5DDSTHPsacPopulation":
                        return new KeyValuePair<string, string>(Translations.DDSTHPsacPopulation, demo.PopPsac.ToString());
                    case "5DDSTHSacPopulation":
                        return new KeyValuePair<string, string>(Translations.DDSTHSacPopulation, demo.PopSac.ToString());
                    case "5DDSTHAdultPopulation":
                        return new KeyValuePair<string, string>(Translations.DDSTHAdultPopulation, demo.PopAdult.ToString());
                    case "13DDTraTotalPopulation":
                        return new KeyValuePair<string, string>(Translations.DDTraTotalPopulation, demo.TotalPopulation.ToString());
                    case "13DDTraSacPopulation":
                        return new KeyValuePair<string, string>(Translations.DDTraSacPopulation, demo.PopSac.ToString());
                    case "13DDTraAdultPopulation":
                        return new KeyValuePair<string, string>(Translations.DDTraAdultPopulation, demo.PopAdult.ToString());
                    default:
                        return new KeyValuePair<string,string>(field,  Translations.NA);
                }

            }
            catch (Exception)
            {
            }
            return new KeyValuePair<string,string>(field,  Translations.CalculationError);
        }

    }
}
