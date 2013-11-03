using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;

namespace Nada.Model.Survey
{
    public class CalcBuruliSurvey : ICalcIndicators
    {
        private static readonly string Total = "TotalNumNewCases";
        private static readonly string TotalType = "TotalUlcerativeCases";
        private static readonly string TotalChild = "TotalNumChildNewCases";
        private static readonly string TotalFemale = "TotalNumFemaleNewCases";
        private static readonly string TotalCat1 = "TotalCat1Cases";
        private static readonly string TotalCat3 = "TotalCat3Cases";
        private static readonly string TotalPrc = "TotalCasesConfirmedPcr";

        public List<KeyValuePair<string, string>> PerformCalculations(List<IndicatorValue> indicatorValues)
        {
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(indicatorValues);
            List<KeyValuePair<string, string>> calcs = new List<KeyValuePair<string, string>>();

            calcs.Add(new KeyValuePair<string, string>("PercentNewCasesPcrCm", Translations.NA));
            return calcs;
        }
    }
}
