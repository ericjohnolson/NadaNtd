using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model
{
    public class CountryDemography : AdminLevelDemography
    {
        public string AgeRangePsac { get; set; }
        public string AgeRangeSac { get; set; }
        public Nullable<double> Percent6mos { get; set; }
        public Nullable<double> PercentPsac { get; set; }
        public Nullable<double> PercentSac { get; set; }
        public Nullable<double> Percent5yo { get; set; }
        public Nullable<double> PercentFemale { get; set; }
        public Nullable<double> PercentMale { get; set; }
        public Nullable<double> PercentAdult { get; set; }
        public Nullable<double> GrossDomesticProduct { get; set; }
        public string CountryIncomeStatus { get; set; }
        public Nullable<double> LifeExpectBirthFemale { get; set; }
        public Nullable<double> LifeExpectBirthMale { get; set; }

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "YearCensus":
                        if (!YearCensus.HasValue)
                            error = Translations.Required;
                        else if (YearCensus.Value > 2100 || YearCensus.Value < 1900)
                                error = Translations.ValidYear;
                        break;
                    case "GrowthRate":
                        if (!GrowthRate.HasValue)
                            error = Translations.Required;
                        break;
                    case "TotalPopulation":
                        if (!(this is CountryDemography) && !TotalPopulation.HasValue)
                            error = Translations.Required;
                        break;
                    case "PopSac":
                        if (!(this is CountryDemography) && !PopSac.HasValue)
                            error = Translations.Required;
                        break;
                    case "PercentSac":
                        if (!PercentSac.HasValue)
                            error = Translations.Required;
                        break;

                    default: error = "";
                        break;

                }
                return error;
            }
        }
        #endregion

    }
}
