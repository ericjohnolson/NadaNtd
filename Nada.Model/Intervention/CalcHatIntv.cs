using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;

namespace Nada.Model.Intervention
{
    public class CalcHatIntv : ICalcIndicators
    {

        public List<KeyValuePair<string, string>> PerformCalculations(List<IndicatorValue> indicatorValues, int adminLevel)
        {
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(indicatorValues);
            List<KeyValuePair<string, string>> calcs = new List<KeyValuePair<string, string>>();
            double h19 = -1, h18 = -1, h20 = -1, h21 = -1, h22 = -1, h23 = -1, h27 = -1, h24 = -1, h29 = -1, h30 = -1;
            h19 = Util.ParseIndicatorDouble(inds, "NumLabCases");
            h18 = Util.ParseIndicatorDouble(inds, "NumClinicalCasesHat");
            h20 = Util.ParseIndicatorDouble(inds, "NumTGamb");
            h21 = Util.ParseIndicatorDouble(inds, "NumTRhod");
            h22 = Util.ParseIndicatorDouble(inds, "NumTGambTRhod");
            h23 = Util.ParseIndicatorDouble(inds, "NumProspection");
            h27 = Util.ParseIndicatorDouble(inds, "NumCasesCured");
            h24 = Util.ParseIndicatorDouble(inds, "NumCasesTreated");
            h29 = Util.ParseIndicatorDouble(inds, "NumCasesSaes");
            h30 = Util.ParseIndicatorDouble(inds, "NumDeaths");
            
            if (h18 > 0 && h19 >= 0)
                calcs.Add(new KeyValuePair<string, string>("PercentLabConfirmed", string.Format("{0:0.00}", h19 / h18 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentLabConfirmed", Translations.NA));

            if (h19 > 0 && h20 >= 0)
                calcs.Add(new KeyValuePair<string, string>("PercentTGamb", string.Format("{0:0.00}", h20 / h19 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentTGamb", Translations.NA));

            if (h19 > 0 && h21 >= 0)
                calcs.Add(new KeyValuePair<string, string>("PercentTRhod", string.Format("{0:0.00}", h19 / h19 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentTRhod", Translations.NA));

            if (h22 > 0 && h18 >= 0)
                calcs.Add(new KeyValuePair<string, string>("PercentTGambTRhod", string.Format("{0:0.00}", h22 / h18 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentTGambTRhod", Translations.NA));

            if (h23 > 0 && h18 >= 0)
                calcs.Add(new KeyValuePair<string, string>("PercentCasesActivelyFound", string.Format("{0:0.00}", h23 / h18 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentCasesActivelyFound", Translations.NA));

            if (h24 > 0 && h27 >= 0)
                calcs.Add(new KeyValuePair<string, string>("CureRate", string.Format("{0:0.00}", h27 / h24 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("CureRate", Translations.NA));

            if (h24 > 0 && h29 >= 0)
                calcs.Add(new KeyValuePair<string, string>("PercentTreatmentFailure", string.Format("{0:0.00}", h29 / h24 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentTreatmentFailure", Translations.NA));

            if (h24 > 0 && h29 >= 0)
                calcs.Add(new KeyValuePair<string, string>("PercentSae", string.Format("{0:0.00}", h29 / h24 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentSae", Translations.NA));

            if (h18 > 0 && h30 >= 0)
                calcs.Add(new KeyValuePair<string, string>("FatalityRate", string.Format("{0:0.00}", h30 / h18 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("FatalityRate", Translations.NA));

            calcs.Add(new KeyValuePair<string, string>("DetectionRatePer100k", Translations.NA));


            return calcs;
        }
    }
}
