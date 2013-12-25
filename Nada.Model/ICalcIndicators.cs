using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;

namespace Nada.Model
{
    public interface ICalcIndicators
    {
        List<KeyValuePair<string, string>> PerformCalculations(Dictionary<string, Indicator> indicators, List<IndicatorValue> indicatorValues,
            int adminLevel, string typeId);
        List<KeyValuePair<string, string>> GetCalculatedValues(List<string> fields, Dictionary<string, string> relatedValues, int adminLevel);
    }

    public class CalcBase : ICalcIndicators
    {
        public virtual List<KeyValuePair<string, string>> PerformCalculations(Dictionary<string, Indicator> indicators, List<IndicatorValue> indicatorValues, 
            int adminLevel, string typeId)
        {
            var calculations = indicators.Values.Where(i => i.IsCalculated);
            if (calculations.Count() == 0)
                return new List<KeyValuePair<string, string>>();

            Dictionary<string, string> values = new Dictionary<string, string>();
            foreach (var val in indicatorValues)
                values.Add(typeId + val.Indicator.DisplayName, val.DynamicValue);

            return GetCalculatedValues(
                calculations.Select(i => typeId + i.DisplayName).ToList(),
                values, 
                adminLevel);
        }

        public virtual List<KeyValuePair<string, string>> GetCalculatedValues(List<string> fields, Dictionary<string, string> relatedValues, int adminLevel)
        {
            return new List<KeyValuePair<string, string>>();
        }

        public string GetPercentage(Dictionary<string, IndicatorValue> indicators, string numerator, string denominator)
        {
            double n, d;
            if (indicators.ContainsKey(numerator) && indicators.ContainsKey(denominator) &&
                double.TryParse(indicators[numerator].DynamicValue, out n) &&
                double.TryParse(indicators[denominator].DynamicValue, out d) && d != 0)
                return string.Format("{0:0.00}", n / d * 100);

            return Translations.NA;
        }

        public string GetPercentage(string numerator, string denominator)
        {
            return GetPercentage(numerator, denominator, 100);
        }

        public string GetPercentage(string numerator, string denominator, int multipler)
        {
            double n, d;
            if (!string.IsNullOrEmpty(numerator) && !string.IsNullOrEmpty(denominator) &&
                double.TryParse(numerator, out n) &&
                double.TryParse(denominator, out d) && d != 0)
                return string.Format("{0:0.00}", n / d * multipler);

            return Translations.NA;
        }

        public string GetTotal(params string[] itemsToSum)
        {
            double total = 0;
            double parsed = 0;
            foreach (string v in itemsToSum)
                if (double.TryParse(v, out parsed))
                    total += parsed;

            return string.Format("{0:0.00}", total);
        }

        public string GetDifference(string val1, string val2)
        {
            double v1, v2;
            if (double.TryParse(val1, out v1) && double.TryParse(val2, out v2))
                return string.Format("{0:0.00}", v1 - v2);
            return Translations.NA;
        }
    }


}
