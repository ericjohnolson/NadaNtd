using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Repositories;

namespace Nada.Model.Process
{
    /// <summary>
    /// Performs calculations for process entities
    /// </summary>
    public class CalcProcess : CalcBase, ICalcIndicators
    {
        /// <summary>
        /// Calculates any indicators that belong in the meta data
        /// </summary>
        /// <param name="fields">The meta data collection of fields</param>
        /// <param name="adminLevel">The admin unit to calculate for</param>
        /// <param name="start">The starting point used to determine demography information</param>
        /// <param name="end">The ending point used to determine demography information</param>
        /// <returns>Collection of meta data calculations</returns>
        public override List<KeyValuePair<string, string>> GetMetaData(List<KeyValuePair<string, string>> fields, int adminLevel, DateTime start, DateTime end)
        {
            return new List<KeyValuePair<string, string>>();
        }

        /// <summary>
        /// Calculates each of the specified indicators
        /// </summary>
        /// <param name="fields">Indicators to calculate</param>
        /// <param name="relatedValues">Related indicator values used in the calculation</param>
        /// <param name="adminLevel">Admin unit to calculate for</param>
        /// <param name="start">The starting point used to determine demography information</param>
        /// <param name="end">The ending point used to determine demography information</param>
        /// <returns>Collection of calculation results</returns>
        public override List<KeyValuePair<string, string>> GetCalculatedValues(List<KeyValuePair<string, string>> fields, Dictionary<string, string> relatedValues, int adminLevel, DateTime start, DateTime end)
        {
            string errors = "";
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();
            foreach (KeyValuePair<string, string> field in fields) // Key is form translation key, value is indicator name
                results.Add(GetCalculatedValue(field.Key, field.Value, relatedValues, null, start, end, ref errors));
            return results;
        }

        /// <summary>
        /// Calculates a single indicator
        /// </summary>
        /// <param name="formTranslationKey">The form translation key</param>
        /// <param name="field">Indicator being calculated</param>
        /// <param name="relatedValues">Related indicator values used in the calculation</param>
        /// <param name="demo">Demography data for the admin unit</param>
        /// <param name="start">The starting point used to determine demography information</param>
        /// <param name="end">The ending point used to determine demography information</param>
        /// <param name="errors">Errors to be displayed and managed by the ReportGenerator</param>
        /// <returns>The indicator name and the calculated value</returns>
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
