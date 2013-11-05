using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;
using Nada.Model.Diseases;
using Nada.Model.Repositories;

namespace Nada.Model.Intervention
{
    public class CalcYawsIntv : ICalcIndicators
    {

        public List<KeyValuePair<string, string>> PerformCalculations(List<IndicatorValue> indicatorValues, int adminLevel)
        {
            SurveyRepository surveys = new SurveyRepository();
            DiseaseRepository distros = new DiseaseRepository();
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(indicatorValues);
            List<KeyValuePair<string, string>> calcs = new List<KeyValuePair<string, string>>();
            int yearOfReporting = Util.ParseIndicatorInt(inds, "IntvYear");
            SurveyBase survey = surveys.GetSurveyByAdminLevelYear(adminLevel, yearOfReporting);
            DiseaseDistroCm distro = distros.GetDistroByAdminLevelYear(adminLevel, yearOfReporting, (int)DiseaseType.Yaws);
            double Y5 = -1, Y55 = -1, Y8 = -1, Y18 = -1;
            double Y21 = Util.ParseIndicatorDouble(inds, "NumCasesTreatedYaws");
            double Y22 = Util.ParseIndicatorDouble(inds, "NumContactsTreatedYw");

            if (survey != null)
                Y18 = Util.ParseIndicatorDouble(Util.CreateIndicatorValueDictionary(survey.IndicatorValues), "TotalNumPeopleScreenedCm");

            if (distro != null)
            {
                var distroInds = Util.CreateIndicatorValueDictionary(distro.IndicatorValues);
                Y5 = Util.ParseIndicatorDouble(distroInds, "TotalNumNewCases");//Total number of new cases (dd)
                Y55 = Util.ParseIndicatorDouble(distroInds, "TotalCasesConfirmedLab");// New cases confirmed by lab test (dd)
                Y8 = Util.ParseIndicatorDouble(distroInds, "SchoolCases14");//School age cases (6 to 14 years old) (dd)
            }

            //Y41	% of pre-school age cases	percent		Y5.5/Y5
            if (Y55 >= 0 && Y5 > 0)
                calcs.Add(new KeyValuePair<string, string>("PercentPsacYw", string.Format("{0:0.00}", Y55 / Y5 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentPsacYw", Translations.NA));
            //Y42	% of school-age cases	percent		Y8/Y5
            if (Y8 >= 0 && Y5 > 0)
                calcs.Add(new KeyValuePair<string, string>("PercentSacYw", string.Format("{0:0.00}", Y8 / Y5 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentSacYw", Translations.NA));
            //Y45	% cases among screened populations (per 10,000)	percent		Y5*100)/Y18
            if (Y5 >= 0 && Y18 > 0)
                calcs.Add(new KeyValuePair<string, string>("PercentScreenedYw", string.Format("{0:0.00}", (Y5 * 100) / Y18).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentScreenedYw", Translations.NA));
            //Y46	% treated among detected cases	percent		Y21/Y5
            if (Y21 >= 0 && Y5 > 0)
                calcs.Add(new KeyValuePair<string, string>("PercentTreatedAmongDetected", string.Format("{0:0.00}", Y21 / Y5 * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentTreatedAmongDetected", Translations.NA));

            //Y39	Detection rate per 100 000	number		Y5/total population (demography)*100000)
            calcs.Add(new KeyValuePair<string, string>("DetectRate100kYw", Translations.NA));

            //Y40	% of new cases confirmed by lab test	percent		Y5/total population (demography)*100000)
            calcs.Add(new KeyValuePair<string, string>("PercentNewCasesLabYw", Translations.NA));
            
            //Y47	% contacts treated among all treated	percent		Y22/(Y21+Y22)
            if (Y22 >= 0 && Y21 > 0)
                calcs.Add(new KeyValuePair<string, string>("PercentTreatedYw", string.Format("{0:0.00}", (Y22 / (Y22 + Y21)) * 100).ToString()));
            else
                calcs.Add(new KeyValuePair<string, string>("PercentTreatedYw", Translations.NA));

            return calcs;
        }
    }
}
