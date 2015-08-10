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
                    case "YAWSPercentNewChildren":
                        return new KeyValuePair<string, string>(Translations.PercentNewChildren,
                            GetPercentage(GetValueOrDefault("YAWSTotalNumChildNewCases", relatedValues), GetValueOrDefault("YAWSTotalNumNewCases", relatedValues)));
                    case "YAWSPercentNewFemales":
                        return new KeyValuePair<string,string>(Translations.PercentNewFemales,  GetPercentage(GetValueOrDefault("YAWSTotalNumFemaleNewCases", relatedValues), GetValueOrDefault("YAWSTotalNumNewCases", relatedValues)));
                    case "LeprosyPercentNewChildren":
                        return new KeyValuePair<string, string>(Translations.PercentNewChildren, GetPercentage(GetValueOrDefault("LeprosyTotalNumChildNewCases", relatedValues), GetValueOrDefault("LeprosyTotalNumNewCases", relatedValues)));
                    case "LeprosyPercentNewFemales":
                        return new KeyValuePair<string, string>(Translations.PercentNewFemales, GetPercentage(GetValueOrDefault("LeprosyTotalNumFemaleNewCases", relatedValues), GetValueOrDefault("LeprosyTotalNumNewCases", relatedValues)));
                    case "LeprosyTotalNumCasesRegistered":
                        //if (L5 >= 0 && L24 >= 0 && L25 >= 0 && l9 >= 0)
                        //    calcs.Add(new KeyValuePair<string, string>("TotalNumCasesRegistered", string.Format("{0}", L5 + L24 + L25 + l9).ToString())));
                        return new KeyValuePair<string,string>(Translations.TotalNumCasesRegistered,  Translations.CalculationError);
                    case "LeprosyPercentNewMb":
                        return new KeyValuePair<string,string>(Translations.PercentNewMb,  GetPercentage(GetValueOrDefault("LeprosyTotalNumMbCases", relatedValues), GetValueOrDefault("LeprosyTotalNumNewCases", relatedValues)));
                    case "LeprosyPrevalenceRateEndOfYear":
                        if (demo != null)
                            return new KeyValuePair<string,string>(Translations.PrevalenceRateEndOfYear,  GetPercentage(GetValueOrDefault("LeprosyMbCasesRegisteredMdtEnd", relatedValues), demo.TotalPopulation.ToString(), 100000));
                        break;
                    case "LeprosyRateNewGrade2":
                        //L15	Rate of New Grade II per 1 million population	number	CALC: L23 (Total number of new cases with Grade II disabilities)/total population
                        return new KeyValuePair<string,string>(Translations.RateNewGrade2,  Translations.CalculationError);
                    case "LeprosyDetectionRate100k":
                        if (demo != null)
                            return new KeyValuePair<string, string>(Translations.DetectionRate100k, GetPercentage(GetValueOrDefault("LeprosyTotalNumNewCases", relatedValues), demo.TotalPopulation.ToString(), 100000));
                        break;
                    case "HATPercentCasesHat2":
                        return new KeyValuePair<string, string>(Translations.PercentCasesHat2, GetPercentage(GetValueOrDefault("HATTotalNumHat2", relatedValues), GetValueOrDefault("HATTotalNumHat", relatedValues)));
                    case "HATPercentNewChildren":
                        return new KeyValuePair<string, string>(Translations.PercentNewChildren, GetPercentage(GetValueOrDefault("HATTotalNumHatChild", relatedValues), GetValueOrDefault("HATTotalNumHat", relatedValues)));
                    case "HATPercentNewFemales":
                        return new KeyValuePair<string, string>(Translations.PercentNewFemales, GetPercentage(GetValueOrDefault("HATTotalNumHatFemale", relatedValues), GetValueOrDefault("HATTotalNumHat", relatedValues)));
                    case "BuruliUlcerPercentNewChildren":
                        return new KeyValuePair<string, string>(Translations.PercentNewChildren, GetPercentage(GetValueOrDefault("BuruliUlcerTotalNumChildNewCases", relatedValues), GetValueOrDefault("BuruliUlcerTotalNumNewCases", relatedValues)));
                    case "BuruliUlcerPercentNewFemales":
                        return new KeyValuePair<string, string>(Translations.PercentNewFemales, GetPercentage(GetValueOrDefault("BuruliUlcerTotalNumFemaleNewCases", relatedValues), GetValueOrDefault("BuruliUlcerTotalNumNewCases", relatedValues)));
                    case "BuruliUlcerPercentUlcerativeCases":
                        return new KeyValuePair<string, string>(Translations.PercentUlcerativeCases, GetPercentage(GetValueOrDefault("BuruliUlcerTotalUlcerativeCases", relatedValues), GetValueOrDefault("BuruliUlcerTotalNumNewCases", relatedValues)));
                    case "BuruliUlcerPercentCatICases":
                        return new KeyValuePair<string, string>(Translations.PercentCatICases, GetPercentage(GetValueOrDefault("BuruliUlcerTotalCat1Cases", relatedValues), GetValueOrDefault("BuruliUlcerTotalNumNewCases", relatedValues)));
                    case "BuruliUlcerPercentCatIICases":
                        return new KeyValuePair<string, string>(Translations.PercentCatIICases, GetPercentage(GetValueOrDefault("BuruliUlcerTotalCat3Cases", relatedValues), GetValueOrDefault("BuruliUlcerTotalNumNewCases", relatedValues)));
                    case "BuruliUlcerPercentPcrCases":
                        return new KeyValuePair<string, string>(Translations.PercentPcrCases, GetPercentage(GetValueOrDefault("BuruliUlcerTotalCasesConfirmedPcr", relatedValues), GetValueOrDefault("BuruliUlcerTotalNumNewCases", relatedValues)));
                    case "BuruliUlcerDetectionRate100k":
                        if(demo != null)
                            return new KeyValuePair<string, string>(Translations.DetectionRate100k, GetPercentage(GetValueOrDefault("BuruliUlcerTotalNumNewCases", relatedValues), demo.TotalPopulation.ToString(), 100000));
                        break;
                    case "OnchoDDOnchoTotalPopulation":
                        return new KeyValuePair<string, string>(Translations.DDOnchoTotalPopulation, demo.TotalPopulation.ToString());
                    case "LFDDLFTotalPopulation":
                        return new KeyValuePair<string, string>(Translations.DDLFTotalPopulation, demo.TotalPopulation.ToString());
                    case "LFDDLFPsacPopulation":
                        return new KeyValuePair<string, string>(Translations.DDLFPsacPopulation, demo.PopPsac.ToString());
                    case "LFDDLFSacPopulation":
                        return new KeyValuePair<string, string>(Translations.DDLFSacPopulation, demo.PopSac.ToString());
                    case "LFDDLFAdultPopulation":
                        return new KeyValuePair<string, string>(Translations.DDLFAdultPopulation, demo.PopAdult.ToString());
                    case "SchistoDDSchistoTotalPopulation":
                        return new KeyValuePair<string, string>(Translations.DDSchistoTotalPopulation, demo.TotalPopulation.ToString());
                    case "SchistoDDSchistoSacPopulation":
                        return new KeyValuePair<string, string>(Translations.DDSchistoSacPopulation, demo.PopSac.ToString());
                    case "SchistoDDSchistoAdultPopulation":
                        return new KeyValuePair<string, string>(Translations.DDSchistoAdultPopulation, demo.PopAdult.ToString());
                    case "STHDDSTHTotalPopulation":
                        return new KeyValuePair<string, string>(Translations.DDSTHTotalPopulation, demo.TotalPopulation.ToString());
                    case "STHDDSTHPsacPopulation":
                        return new KeyValuePair<string, string>(Translations.DDSTHPsacPopulation, demo.PopPsac.ToString());
                    case "STHDDSTHSacPopulation":
                        return new KeyValuePair<string, string>(Translations.DDSTHSacPopulation, demo.PopSac.ToString());
                    case "STHDDSTHAdultPopulation":
                        return new KeyValuePair<string, string>(Translations.DDSTHAdultPopulation, demo.PopAdult.ToString());
                    case "TrachomaDDTraTotalPopulation":
                        return new KeyValuePair<string, string>(Translations.DDTraTotalPopulation, demo.TotalPopulation.ToString());
                    case "TrachomaDDTraSacPopulation":
                        return new KeyValuePair<string, string>(Translations.DDTraSacPopulation, demo.PopSac.ToString());
                    case "TrachomaDDTraAdultPopulation":
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
