using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;

namespace Nada.Model.Diseases
{
    public class CalcBuruliDistro : ICalcIndicators
    {
        private static readonly string Total = "TotalNumNewCases";
        private static readonly string TotalType = "TotalUlcerativeCases";
        private static readonly string TotalChild = "TotalNumChildNewCases";
        private static readonly string TotalFemale = "TotalNumFemaleNewCases";
        private static readonly string TotalCat1 = "TotalCat1Cases";
        private static readonly string TotalCat3 = "TotalCat3Cases";
        private static readonly string TotalPrc = "TotalCasesConfirmedPcr";

        public List<KeyValuePair<string, string>> PerformCalculations(List<IndicatorValue> indicatorValues, int adminLevel)
        {
            double total = 0, totalChild = 0, totalFemale = 0, totalType = 0, totalCat1 = 0, totalCat2 = 0, totalPcr = 0;

            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(indicatorValues);
            List<KeyValuePair<string, string>> calcs = new List<KeyValuePair<string, string>>();

            if (inds.ContainsKey(Total) && inds.ContainsKey(TotalType) &&
                double.TryParse(inds[Total].DynamicValue, out total) && double.TryParse(inds[TotalType].DynamicValue, out totalType))
                calcs.Add(new KeyValuePair<string, string>("PercentUlcerativeCases", string.Format("{0:0.00}", totalType / total * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentUlcerativeCases", Translations.NA)); 

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

            if (inds.ContainsKey(Total) && inds.ContainsKey(TotalCat1) &&
                double.TryParse(inds[Total].DynamicValue, out total) && double.TryParse(inds[TotalCat1].DynamicValue, out totalCat1))
                calcs.Add(new KeyValuePair<string, string>("PercentCatICases", string.Format("{0:0.00}", totalCat1 / total * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentCatICases", Translations.NA));

            if (inds.ContainsKey(Total) && inds.ContainsKey(TotalCat3) &&
                double.TryParse(inds[Total].DynamicValue, out total) && double.TryParse(inds[TotalCat3].DynamicValue, out totalCat2))
                calcs.Add(new KeyValuePair<string, string>("PercentCatIICases", string.Format("{0:0.00}", totalCat2 / total * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentCatIICases", Translations.NA));

            if (inds.ContainsKey(Total) && inds.ContainsKey(TotalPrc) &&
                double.TryParse(inds[Total].DynamicValue, out total) && double.TryParse(inds[TotalPrc].DynamicValue, out totalPcr))
                calcs.Add(new KeyValuePair<string, string>("PercentPcrCases", string.Format("{0:0.00}", totalPcr / total * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentPcrCases", Translations.NA));

            calcs.Add(new KeyValuePair<string, string>("DetectionRate100k", Translations.NA));
            return calcs;
        }
    }
}
