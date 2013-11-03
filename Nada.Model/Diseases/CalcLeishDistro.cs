using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;

namespace Nada.Model.Diseases
{
    public class CalcLeishDistro : ICalcIndicators
    {
        private static readonly string Total = "TotalNumNewCases";
        private static readonly string TotalChild = "TotalNumChildNewCases";
        private static readonly string TotalFemale = "TotalNumFemaleNewCases";

        public List<KeyValuePair<string, string>> PerformCalculations(List<IndicatorValue> indicatorValues)
        {
            double total = 0, totalChild = 0, totalFemale = 0;

            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(indicatorValues);
            List<KeyValuePair<string, string>> calcs = new List<KeyValuePair<string, string>>();


            if (inds.ContainsKey(Total) && inds.ContainsKey(TotalChild) &&
                double.TryParse(inds[Total].DynamicValue, out total) && double.TryParse(inds[TotalChild].DynamicValue, out totalChild))
                calcs.Add(new KeyValuePair<string, string>("PercentNewChildren", string.Format("{0:0.00}", totalChild / total * 100).ToString())); 
            else
                calcs.Add(new KeyValuePair<string, string>("PercentNewChildren", Translations.NA)); 

            if (inds.ContainsKey(Total) && inds.ContainsKey(TotalFemale) &&
                double.TryParse(inds[Total].DynamicValue, out total) && double.TryParse(inds[TotalFemale].DynamicValue, out totalFemale))
                calcs.Add(new KeyValuePair<string, string>("PercentNewFemales", string.Format("{0:0.00}", totalFemale / total * 100).ToString())); 
            else
                calcs.Add(new KeyValuePair<string, string>("PercentNewFemales", Translations.NA));

            return calcs;
        }
    }
}
