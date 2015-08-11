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
        public override List<KeyValuePair<string, string>> GetMetaData(List<KeyValuePair<string, string>> fields, int adminLevel, DateTime start, DateTime end)
        {
            return new List<KeyValuePair<string, string>>();
        }

        public override List<KeyValuePair<string, string>> GetCalculatedValues(List<KeyValuePair<string, string>> fields, Dictionary<string, string> relatedValues, int adminLevel, DateTime start, DateTime end)
        {
            string errors = "";
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();
            foreach (KeyValuePair<string, string> field in fields) // Key is form translation key, value is indicator name
                results.Add(GetCalculatedValue(field.Key, field.Value, relatedValues, null, start, end, ref errors));
            return results;
        }

        public override KeyValuePair<string, string> GetCalculatedValue(string formTranslationKey, string field, Dictionary<string, string> relatedValues, AdminLevelDemography demo, DateTime start, DateTime end, ref string errors)
        {
            // Combine the form translation key and field name
            string formNameFieldComposite = formTranslationKey + field;
            try
            {
                switch (formNameFieldComposite)
                {
                    case "SCMSCMRemaining":

                        return new KeyValuePair<string, string>(Translations.SCMRemaining,
                            GetTotal(
                                GetValueOrDefault("SCMSCMReceived", relatedValues), 
                                GetValueOrDefault("SCMSCMTransferredFromAnotherDisrict", relatedValues),
                                GetDifferences(GetValueOrDefault("SCMSCMUsedDistributed", relatedValues), GetValueOrDefault("SCMSCMExpiredDrugs", relatedValues),
                                GetValueOrDefault("SCMSCMWasted", relatedValues), GetValueOrDefault("SCMSCMUnusable", relatedValues),
                                GetValueOrDefault("SCMSCMLosses", relatedValues), GetValueOrDefault("SCMSCMStolen", relatedValues),
                                GetValueOrDefault("SCMSCMTransferredToAnotherDistrict", relatedValues), GetValueOrDefault("SCMSCMAdjustments", relatedValues))));
                     
                    default:
                        return new KeyValuePair<string, string>(formNameFieldComposite, Translations.NA);
                }
            }
            catch (Exception)
            {
            }
            return new KeyValuePair<string, string>(formNameFieldComposite, Translations.CalculationError);
        }

    }
}
