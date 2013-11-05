using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;

namespace Nada.Model.Intervention
{
    public class CalcGuineaIntv : ICalcIndicators
    {

        public List<KeyValuePair<string, string>> PerformCalculations(List<IndicatorValue> indicatorValues, int adminLevel)
        {
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(indicatorValues);
            List<KeyValuePair<string, string>> calcs = new List<KeyValuePair<string, string>>();
            double gw9 = -1, gw8 = -1, GW11 = -1, GW10 = -1, GW13 = -1, GW12 = -1, GW22 = -1, GW21 = -1, GW16 = -1, GW14 = -1, GW19 = -1;
            gw8 = Util.ParseIndicatorDouble(inds, "NumVas");
            gw9 = Util.ParseIndicatorDouble(inds, "VasReporting");
            GW10 = Util.ParseIndicatorDouble(inds, "NumIdsr");
            GW11 = Util.ParseIndicatorDouble(inds, "NumIdsrReporting");
            GW12 = Util.ParseIndicatorDouble(inds, "NumRumors");
            GW13 = Util.ParseIndicatorDouble(inds, "NumRumorsInvestigated");
            GW14 = Util.ParseIndicatorDouble(inds, "NumClinical");
            GW16 = Util.ParseIndicatorDouble(inds, "NumIndigenous");
            GW19 = Util.ParseIndicatorDouble(inds, "NumCasesContained");
            GW21 = Util.ParseIndicatorDouble(inds, "NumEndemicVillages");
            GW22 = Util.ParseIndicatorDouble(inds, "NumEndemicVillagesReporting");
            

            //GW17	Number of imported cases	number	calc: GW14 - GW16
            if (GW16 >= 0 && GW14 >= 0)
                calcs.Add(new KeyValuePair<string, string>("NumImported", string.Format("{0}", GW14 - GW16).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("NumImported", Translations.NA));
            // GW26	% of VAS reporting	PERCENT	calc: gw9/gw8
            if (gw9 >= 0 && gw8 > 0)
                calcs.Add(new KeyValuePair<string, string>("PercentVas", string.Format("{0:0.00}", gw9 / gw8 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentVas", Translations.NA));
            //GW27	% of IDSR/HF reporting on GWD	PERCENT	calc:  GW11/GW10
            if (GW11 >= 0 && GW10 > 0)
                calcs.Add(new KeyValuePair<string, string>("PercentIdsr", string.Format("{0:0.00}", GW11 / GW10 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentIdsr", Translations.NA));
            //GW28	% of rumors investigated within 24 hours	PERCENT	calc: GW13/GW12
            if (GW13 >= 0 && GW12 > 0)
                calcs.Add(new KeyValuePair<string, string>("PercentRumorsInvestigated", string.Format("{0:0.00}", GW13 / GW12 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentRumorsInvestigated", Translations.NA));
            //GW29	% of cases successfully contained	PERCENT	calc: GW19/GW14
            if (GW19 >= 0 && GW14 > 0)
                calcs.Add(new KeyValuePair<string, string>("PercentCasesContained", string.Format("{0:0.00}", GW19 / GW14 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentCasesContained", Translations.NA));
            //GW30	% of endemic villages reporting	PERCENT	calc: GW22/GW21
            if (GW22 >= 0 && GW21 > 0)
                calcs.Add(new KeyValuePair<string, string>("PercentEndemicReporting", string.Format("{0:0.00}", GW22 / GW21 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentEndemicReporting", Translations.NA));

            return calcs;
        }
    }
}
