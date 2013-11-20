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
    }
}
