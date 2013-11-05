using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;

namespace Nada.Model.Intervention
{
    public class CalcBuruliIntv : ICalcIndicators
    {
        public List<KeyValuePair<string, string>> PerformCalculations(List<IndicatorValue> indicatorValues, int adminLevel)
        {
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(indicatorValues);
            List<KeyValuePair<string, string>> calcs = new List<KeyValuePair<string, string>>();

            //B52	% of Health facility coverage for Buruli Ulcer treatment	percent		U14/total population of adults > 15 years
            calcs.Add(new KeyValuePair<string, string>("PercentCoverageBu", Translations.NA));
            return calcs;
        }
    }
}
