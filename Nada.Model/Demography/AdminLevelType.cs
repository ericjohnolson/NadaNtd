using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model
{
    public class AdminLevelType : NadaClass
    {
        public AdminLevelType()
        {
            IsDistrict = false;
        }

        public bool IsDistrict { get; set; }
        public bool IsAggregatingLevel { get; set; }
        public bool IsDemographyAllowed { get; set; }
        public int LevelNumber { get; set; }
        public string DisplayName { get; set; }
        public string EditText { get { return Translations.View; } }
        public string DeleteText { get { return Translations.Delete; } }
        
        public string IsDistrictText
        {
            get
            {
                if (IsDistrict)
                    return Translations.DistrictAdminLevel;
                else
                    return "";
            }
        }
        public string IsAggText
        {
            get
            {
                if (IsAggregatingLevel)
                    return Translations.AggAdminLevel;
                else
                    return "";
            }
        }
    }
}
