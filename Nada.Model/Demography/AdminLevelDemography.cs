﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model
{

    /// <summary>
    /// Models an admin unit's demography
    /// </summary>
    [Serializable]
    public class AdminLevelDemography : NadaClass
    {
        public AdminLevelDemography()
        {
            DateDemographyData = DateTime.Now;
        }
        public int AdminLevelId { get; set; }
        public DateTime DateDemographyData { get; set; }
        public Nullable<int> YearCensus { get; set; }
        public Nullable<double> GrowthRate { get; set; }
        public Nullable<double> PercentRural { get; set; }
        public Nullable<int> TotalPopulation { get; set; }
        public Nullable<int> Pop0Month { get; set; }
        public Nullable<int> PopPsac { get; set; }
        public Nullable<int> PopSac { get; set; }
        public Nullable<int> Pop5yo { get; set; }
        public Nullable<int> PopAdult { get; set; }
        public Nullable<int> PopFemale { get; set; }
        public Nullable<int> PopMale { get; set; }
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

                    default: error = "";
                        break;

                }
                return error;
            }
        }

        #endregion
    }
}
