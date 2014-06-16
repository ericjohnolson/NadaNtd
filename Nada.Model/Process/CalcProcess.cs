using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Repositories;

namespace Nada.Model.Process
{
    public class CalcProcess : CalcBase, ICalcIndicators
    {
        public override List<KeyValuePair<string, string>> GetMetaData(IEnumerable<string> fields, int adminLevel, DateTime start, DateTime end)
        {
            return new List<KeyValuePair<string, string>>();
        }

        public override List<KeyValuePair<string, string>> GetCalculatedValues(List<string> fields, Dictionary<string, string> relatedValues, int adminLevel, DateTime start, DateTime end)
        {
            string errors = "";
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();
            foreach (string field in fields)
                results.Add(GetCalculatedValue(field, relatedValues, null, start, end, ref errors));
            return results;
        }

        public override KeyValuePair<string, string> GetCalculatedValue(string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime start, DateTime end, ref string errors)
        {
            try
            {
                switch (field)
                {
                    case "8SCMRemaining":

                        return new KeyValuePair<string, string>(Translations.SCMRemaining,
                            GetDifferences(GetTotal(GetValueOrDefault("8SCMReceived", relatedValues), GetValueOrDefault("8SCMTransferredFromAnotherDisrict", relatedValues)),
                                GetValueOrDefault("8SCMUsedDistributed", relatedValues), GetValueOrDefault("8SCMExpiredDrugs", relatedValues),
                                GetValueOrDefault("8SCMWasted", relatedValues), GetValueOrDefault("8SCMUnusable", relatedValues),
                                GetValueOrDefault("8SCMLosses", relatedValues), GetValueOrDefault("8SCMStolen", relatedValues),
                                GetValueOrDefault("8SCMTransferredToAnotherDistrict", relatedValues), GetValueOrDefault("8SCMAdjustments", relatedValues)));
                     
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
