using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model
{
    public class AdminLevelDemography : NadaClass
    {
        public int AdminLevelId { get; set; }
        public Nullable<int> YearDemographyData { get; set; }
        public Nullable<int> YearCensus { get; set; }
        public Nullable<int> YearProjections { get; set; }
        public double GrowthRate { get; set; }
        public Nullable<double> PercentRural { get; set; }
        public Nullable<int> TotalPopulation { get; set; }
        public Nullable<int> Pop0Month { get; set; }
        public Nullable<int> PopPsac { get; set; }
        public Nullable<int> PopSac { get; set; }
        public Nullable<int> Pop5yo { get; set; }
        public Nullable<int> PopAdult { get; set; }
        public Nullable<int> PopFemale { get; set; }
        public Nullable<int> PopMale { get; set; }
        public string Notes { get; set; }
        // display only
        public string NameDisplayOnly { get; set; }


        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                        
                    case "YearDemographyData":
                        if (!YearDemographyData.HasValue)
                            error = Translations.Required;
                        else if (YearDemographyData.Value > 2050 || YearDemographyData.Value < 1900)
                            error = Translations.ValidYear;
                        break;
                    case "YearCensus":
                        if (!YearCensus.HasValue)
                            error = Translations.Required;
                        else if (YearCensus.Value > 2050 || YearCensus.Value < 1900)
                            error = Translations.ValidYear;
                        break;
                    case "YearProjections":
                        if (YearProjections.HasValue && (YearProjections.Value > 2050 || YearProjections.Value < 1900))
                            error = Translations.ValidYear;
                        break;
                    case "GrowthRate":
                        if (GrowthRate <= 0)
                            error = Translations.Required;
                        break;
                    case "TotalPopulation":
                        if (!(this is CountryDemography) && (!TotalPopulation.HasValue || TotalPopulation.Value <= 0))
                            error = Translations.Required;
                        break;
                    case "PopSac":
                        if (!(this is CountryDemography) && (!PopSac.HasValue || PopSac.Value <= 0))
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
