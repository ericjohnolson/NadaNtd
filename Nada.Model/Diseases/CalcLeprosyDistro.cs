using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Repositories;

namespace Nada.Model.Diseases
{
    public class CalcLeprosyDistro : ICalcIndicators
    {
        private static readonly string Total = "TotalNumNewCases";
        private static readonly string TotalMb = "TotalNumMbCases";
        private static readonly string TotalChild = "TotalNumChildNewCases";
        private static readonly string TotalFemale = "TotalNumFemaleNewCases";

        public List<KeyValuePair<string, string>> PerformCalculations(List<IndicatorValue> indicatorValues, int adminLevel)
        {
            double total = 0, totalMb = 0, totalChild = 0, totalFemale = 0;

            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(indicatorValues);
            List<KeyValuePair<string, string>> calcs = new List<KeyValuePair<string, string>>();

            double L5 = -1, L24 = -1, L25 = -1, l9 = -1;
            L5 = Util.ParseIndicatorDouble(inds, "TotalNumNewCases");
            l9 = Util.ParseIndicatorDouble(inds, "PrevalenceBeginningYear");

            int yearOfReporting = Util.ParseIndicatorInt(inds, "IntvYear");
            DiseaseRepository distros = new DiseaseRepository();
            DiseaseDistroCm distro = distros.GetDistroByAdminLevelYear(adminLevel, yearOfReporting, (int)DiseaseType.Yaws);
            if (distro != null)
            {
                L24 = Util.ParseIndicatorDouble(inds, "NumRelapses");
                L25 = Util.ParseIndicatorDouble(inds, "CasesReadmitted");
            }

            //L11	Total number of cases registered 	PERCENT	CALC: L5+L24+L25+l9
            if (L5 >= 0 && L24 >= 0 && L25 >= 0 && l9 >= 0)
                calcs.Add(new KeyValuePair<string, string>("TotalNumCasesRegistered", string.Format("{0}", L5 + L24 + L25 + l9).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("TotalNumCasesRegistered", Translations.NA));

            // total mb / total new
            if(inds.ContainsKey(Total) && inds.ContainsKey(TotalMb) &&
                double.TryParse(inds[Total].DynamicValue, out total) && double.TryParse(inds[TotalMb].DynamicValue, out totalMb))
                calcs.Add(new KeyValuePair<string, string>("PercentNewMb", string.Format("{0:0.00}", totalMb / total * 100).ToString())); 
            else
                calcs.Add(new KeyValuePair<string, string>("PercentNewMb", Translations.NA)); 

            // total child / total new
            if (inds.ContainsKey(Total) && inds.ContainsKey(TotalChild) &&
                double.TryParse(inds[Total].DynamicValue, out total) && double.TryParse(inds[TotalChild].DynamicValue, out totalChild))
                calcs.Add(new KeyValuePair<string, string>("PercentNewChildren", string.Format("{0:0.00}", totalChild / total * 100).ToString())); 
            else
                calcs.Add(new KeyValuePair<string, string>("PercentNewChildren", Translations.NA)); 

            // total female / total new
            if (inds.ContainsKey(Total) && inds.ContainsKey(TotalFemale) &&
                double.TryParse(inds[Total].DynamicValue, out total) && double.TryParse(inds[TotalFemale].DynamicValue, out totalFemale))
                calcs.Add(new KeyValuePair<string, string>("PercentNewFemales", string.Format("{0:0.00}", totalFemale / total * 100).ToString())); 
            else
                calcs.Add(new KeyValuePair<string, string>("PercentNewFemales", Translations.NA)); 

            //L18	Prevalence rate at the end of the year per 10,000	number	CALC: L12/total population(from demography)
            calcs.Add(new KeyValuePair<string, string>("PrevalenceRateEndOfYear", Translations.NA));
            //L15	Rate of New Grade II per 1 million population	number	CALC: L23/total population
            calcs.Add(new KeyValuePair<string, string>("RateNewGrade2", Translations.NA));
            //L13	Detection rate per 100,000	number	CALC: L5/total population (from demography)
            calcs.Add(new KeyValuePair<string, string>("DetectionRate100k", Translations.NA));
            return calcs;
        }
    }
}
