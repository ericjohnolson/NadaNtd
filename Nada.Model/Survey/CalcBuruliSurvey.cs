using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;

namespace Nada.Model.Survey
{
    public class CalcBuruliSurvey : ICalcIndicators
    {
        private static readonly string NumCasesDiagnosed = "NumCasesDiagnosedCm";
        private static readonly string NumCasesPcrCm = "NumCasesPcrCm";

        public List<KeyValuePair<string, string>> PerformCalculations(List<IndicatorValue> indicatorValues, int adminLevel)
        {
            double diagnosed, pcr;
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(indicatorValues);
            List<KeyValuePair<string, string>> calcs = new List<KeyValuePair<string, string>>();

            if (inds.ContainsKey(NumCasesDiagnosed) && inds.ContainsKey(NumCasesPcrCm) &&
                double.TryParse(inds[NumCasesDiagnosed].DynamicValue, out diagnosed) && double.TryParse(inds[NumCasesPcrCm].DynamicValue, out pcr))
                calcs.Add(new KeyValuePair<string, string>("PercentNewCasesPcrCm", string.Format("{0:0.00}", pcr / diagnosed * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentNewCasesPcrCm", Translations.NA));

            return calcs;
        }
    }
}
