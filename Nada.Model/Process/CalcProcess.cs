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
                    case "SCMRemaining":

                        return new KeyValuePair<string, string>(Translations.SCMRemaining,
                            GetTotal(
                                GetValueOrDefault("SCMReceived", relatedValues), 
                                GetValueOrDefault("SCMTransferredFromAnotherDisrict", relatedValues),
                                GetDifferences(GetValueOrDefault("SCMUsedDistributed", relatedValues), GetValueOrDefault("SCMExpiredDrugs", relatedValues),
                                GetValueOrDefault("SCMWasted", relatedValues), GetValueOrDefault("SCMUnusable", relatedValues),
                                GetValueOrDefault("SCMLosses", relatedValues), GetValueOrDefault("SCMStolen", relatedValues),
                                GetValueOrDefault("SCMTransferredToAnotherDistrict", relatedValues), GetValueOrDefault("SCMAdjustments", relatedValues))));
                     
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
