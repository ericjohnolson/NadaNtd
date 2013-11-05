using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Repositories;

namespace Nada.Model.Intervention
{
    public class CalcLeprosyIntv : ICalcIndicators
    {
        public List<KeyValuePair<string, string>> PerformCalculations(List<IndicatorValue> indicatorValues, int adminLevel)
        {
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(indicatorValues);
            List<KeyValuePair<string, string>> calcs = new List<KeyValuePair<string, string>>();
            double L23 = -1, L5 = -1, L12 = -1, L40 = -1, L10 = -1, L41 = -1, L6 = -1, L9 = -1;

            L23 = Util.ParseIndicatorDouble(inds, "NumGrade2");
            L40 = Util.ParseIndicatorDouble(inds, "PatientsCuredMb");
            L41 = Util.ParseIndicatorDouble(inds, "PatientsCuredPb");


            int yearOfReporting = Util.ParseIndicatorInt(inds, "IntvYear");
            DiseaseRepository distros = new DiseaseRepository();
            DiseaseDistroCm distro = distros.GetDistroByAdminLevelYear(adminLevel, yearOfReporting, (int)DiseaseType.Yaws);
            if (distro != null)
            {
                L5 = Util.ParseIndicatorDouble(inds, "TotalNumNewCases");
                L6 = Util.ParseIndicatorDouble(inds, "TotalNumMbCases");
                L9 = Util.ParseIndicatorDouble(inds, "PrevalenceBeginningYear");
                L10 = Util.ParseIndicatorDouble(inds, "MbCasesRegisteredMdtBeginning");
                L12 = Util.ParseIndicatorDouble(inds, "MbCasesRegisteredMdtEnd");
            }

            //L48	% of New Grade II	PERCENT	calc: L23/L5
            if (L23 >= 0 && L5 > 0)
                calcs.Add(new KeyValuePair<string, string>("PercentEndemicReporting", string.Format("{0:0.00}", L23 / L5 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentNewGrade2", Translations.NA));
            //L49	Prevalence detection ratio	number	calc: L12/L5
            if (L12 >= 0 && L5 > 0)
                calcs.Add(new KeyValuePair<string, string>("PercentEndemicReporting", string.Format("{0:0.00}", L12 / L5 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PrevalenceDetectionRatio", Translations.NA));
            //L51	Cure rate of previous year MB cases	PERCENT	calc: L40/L10
            if (L40 >= 0 && L10 > 0)
                calcs.Add(new KeyValuePair<string, string>("PercentEndemicReporting", string.Format("{0:0.00}", L40 / L10 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentCureRateMb", Translations.NA));
            //L52	PB Cured rate during the current year  	PERCENT	calc: L41/(((L5-L6)/2)+(L9-l10))
            if (L41 >= 0 && L5 >= 0 && L10 >= 0 && L6 >= 0 && L9 >= 0)
                calcs.Add(new KeyValuePair<string, string>("PercentEndemicReporting", string.Format("{0:0.00}", (L41/(((L5-L6)/2)+(L9-L10))) * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentCureRatePb", Translations.NA));


            //L50	% of Health facility coverage for MDT	PERCENT	calc: L28/number of health facilities (from demography)
            calcs.Add(new KeyValuePair<string, string>("PercentCoverageMdt", Translations.NA));
            return calcs;
        }
    }
}
