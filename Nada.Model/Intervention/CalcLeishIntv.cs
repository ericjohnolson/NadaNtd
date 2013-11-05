using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;

namespace Nada.Model.Intervention
{
    public class CalcLeishIntv : ICalcIndicators
    {

        public List<KeyValuePair<string, string>> PerformCalculations(List<IndicatorValue> indicatorValues, int adminLevel)
        {
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(indicatorValues);
            List<KeyValuePair<string, string>> calcs = new List<KeyValuePair<string, string>>();
            double L18 = -1, L19 = -1, L20 = -1, L21 = -1, L22 = -1;

            L18 = Util.ParseIndicatorDouble(inds, "NumClCases");
            L19 = Util.ParseIndicatorDouble(inds, "NumLabClCases");
            L20 = Util.ParseIndicatorDouble(inds, "NumLabVlCases");
            L21 = Util.ParseIndicatorDouble(inds, "NumClVlCases");
            L22 = Util.ParseIndicatorDouble(inds, "NumCasesFoundActively");

            //Detection rate per 100,000	num		CALC: (L18+L20+L21)/TOTAL POP*100000)
            calcs.Add(new KeyValuePair<string, string>("DetectRate100kLeish", Translations.NA));
            //% of lab confirmed cases	percent		CALC: (L19+L20)/(L18+L20+L21))
            if (L18 >= 0 && L19 >= 0 && L20 >= 0 && L21 >= 0)
                calcs.Add(new KeyValuePair<string, string>("PercentLabConfirm", string.Format("{0:0.00}", ((L19 + L20) / (L18 + L20 + L21)) * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentLabConfirm", Translations.NA));
            //% only CL cases	percent		CALC: L18/(L18+L20+L21))
            if (L18 >= 0 &&  L20 >= 0 && L21 >= 0)
                calcs.Add(new KeyValuePair<string, string>("PercentCl", string.Format("{0:0.00}", (L18/(L18+L20+L21)) * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentCl", Translations.NA));
            //% only VL cases	percent		CALC: L20/(L18+L20+L21))
            if (L18 >= 0 && L20 >= 0 && L21 >= 0)
                calcs.Add(new KeyValuePair<string, string>("PercentVl", string.Format("{0:0.00}", (L20/(L18+L20+L21)) * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentVl", Translations.NA));
            //% of both CL & VL cases	percent		CALC: L21/(L18+L20+L21))
            if (L18 >= 0 && L20 >= 0 && L21 >= 0)
                calcs.Add(new KeyValuePair<string, string>("PercentClVl", string.Format("{0:0.00}", (L21/(L18+L20+L21)) * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentClVl", Translations.NA));
            //% cases actively found	percent		CALC: L22/(L18+L20+L21))
            if (L18 >= 0 && L22 >= 0 && L20 >= 0 && L21 >= 0)
                calcs.Add(new KeyValuePair<string, string>("PercentCasesActiveLeish", string.Format("{0:0.00}", (L22/(L18+L20+L21)) * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentCasesActiveLeish", Translations.NA));
            
            return calcs;
        }
    }
}
