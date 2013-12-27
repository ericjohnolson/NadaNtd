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
        
        public override List<KeyValuePair<string, string>> GetCalculatedValues(List<string> fields, Dictionary<string, string> relatedValues, int adminLevel)
        {
            AdminLevelDemography recentDemo = null;
            var demography = demoRepo.GetAdminLevelDemography(adminLevel);
            var recentDemogInfo = demography.OrderByDescending(d => d.Year).FirstOrDefault();
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
                    case "11PercentNewChildren":
                        return new KeyValuePair<string,string>(Translations.TotalNumChildNewCases,
                            GetPercentage(GetValueOrDefault("11TotalNumChildNewCases", relatedValues), GetValueOrDefault("11TotalNumNewCases", relatedValues)));
                    case "11PercentNewFemales":
                        return new KeyValuePair<string,string>(Translations.PercentNewFemales,  GetPercentage(GetValueOrDefault("11TotalNumFemaleNewCases", relatedValues), GetValueOrDefault("11TotalNumNewCases", relatedValues)));
                    case "7PercentNewChildren":
                        return new KeyValuePair<string,string>(Translations.PercentNewChildren,  GetPercentage(GetValueOrDefault("7TotalNumNewCases", relatedValues), GetValueOrDefault("7TotalNumNewCases", relatedValues)));
                    case "7PercentNewFemales":
                        return new KeyValuePair<string,string>(Translations.PercentNewFemales,  GetPercentage(GetValueOrDefault("7TotalNumNewCases", relatedValues), GetValueOrDefault("7TotalNumNewCases", relatedValues)));
                    case "7TotalNumCasesRegistered":
                        //if (L5 >= 0 && L24 >= 0 && L25 >= 0 && l9 >= 0)
                        //    calcs.Add(new KeyValuePair<string, string>("TotalNumCasesRegistered", string.Format("{0}", L5 + L24 + L25 + l9).ToString())));
                        return new KeyValuePair<string,string>(Translations.TotalNumCasesRegistered,  Translations.CalculationError);
                    case "7PercentNewMb":
                        return new KeyValuePair<string,string>(Translations.PercentNewMb,  GetPercentage(GetValueOrDefault("7TotalNumMbCases", relatedValues), GetValueOrDefault("7TotalNumNewCases", relatedValues)));
                    case "7PrevalenceRateEndOfYear":
                        if (demo != null)
                            return new KeyValuePair<string,string>(Translations.PrevalenceRateEndOfYear,  GetPercentage(GetValueOrDefault("7MbCasesRegisteredMdtEnd", relatedValues), demo.TotalPopulation.ToString(), 100000));
                        break;
                    case "7RateNewGrade2":
                        //L15	Rate of New Grade II per 1 million population	number	CALC: L23 (Total number of new cases with Grade II disabilities)/total population
                        return new KeyValuePair<string,string>(Translations.RateNewGrade2,  Translations.CalculationError);
                    case "7DetectionRate100k":
                        if (demo != null)
                            return new KeyValuePair<string, string>(Translations.DetectionRate100k, GetPercentage(GetValueOrDefault("7TotalNumNewCases", relatedValues), demo.TotalPopulation.ToString(), 100000));
                        break;
                    case "9PercentNewChildren":
                        return new KeyValuePair<string, string>(Translations.PercentNewChildren, GetPercentage(GetValueOrDefault("9TotalNumChildNewCases", relatedValues), GetValueOrDefault("9TotalNumNewCases", relatedValues)));
                    case "9PercentNewFemales":
                        return new KeyValuePair<string, string>(Translations.PercentNewFemales, GetPercentage(GetValueOrDefault("9TotalNumFemaleNewCases", relatedValues), GetValueOrDefault("9TotalNumNewCases", relatedValues)));
                    case "8PercentCasesHat2":
                        return new KeyValuePair<string, string>(Translations.PercentCasesHat2, GetPercentage(GetValueOrDefault("8TotalNumHat2", relatedValues), GetValueOrDefault("8TotalNumHat", relatedValues)));
                    case "8PercentNewChildren":
                        return new KeyValuePair<string, string>(Translations.PercentNewChildren, GetPercentage(GetValueOrDefault("8TotalNumHatChild", relatedValues), GetValueOrDefault("8TotalNumHat", relatedValues)));
                    case "8PercentNewFemales":
                        return new KeyValuePair<string, string>(Translations.PercentNewFemales, GetPercentage(GetValueOrDefault("8TotalNumHatFemale", relatedValues), GetValueOrDefault("8TotalNumHat", relatedValues)));
                    case "10PercentNewChildren":
                        return new KeyValuePair<string, string>(Translations.PercentNewChildren, GetPercentage(GetValueOrDefault("10TotalNumChildNewCases", relatedValues), GetValueOrDefault("10TotalNumNewCases", relatedValues)));
                    case "10PercentNewFemales":
                        return new KeyValuePair<string, string>(Translations.PercentNewFemales, GetPercentage(GetValueOrDefault("10TotalNumFemaleNewCases", relatedValues), GetValueOrDefault("10TotalNumNewCases", relatedValues)));
                    case "10PercentUlcerativeCases":
                        return new KeyValuePair<string, string>(Translations.PercentUlcerativeCases, GetPercentage(GetValueOrDefault("10TotalUlcerativeCases", relatedValues), GetValueOrDefault("10TotalNumNewCases", relatedValues)));
                    case "10PercentCatICases":
                        return new KeyValuePair<string, string>(Translations.PercentCatICases, GetPercentage(GetValueOrDefault("10TotalCat1Cases", relatedValues), GetValueOrDefault("10TotalNumNewCases", relatedValues)));
                    case "10PercentCatIICases":
                        return new KeyValuePair<string, string>(Translations.PercentCatIICases, GetPercentage(GetValueOrDefault("10TotalCat3Cases", relatedValues), GetValueOrDefault("10TotalNumNewCases", relatedValues)));
                    case "10PercentPcrCases":
                        return new KeyValuePair<string, string>(Translations.PercentPcrCases, GetPercentage(GetValueOrDefault("10TotalCasesConfirmedPcr", relatedValues), GetValueOrDefault("10TotalNumNewCases", relatedValues)));
                    case "10DetectionRate100k":
                        if(demo != null)
                            return new KeyValuePair<string, string>(Translations.DetectionRate100k, GetPercentage(GetValueOrDefault("10TotalNumNewCases", relatedValues), demo.TotalPopulation.ToString(), 100000));
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
