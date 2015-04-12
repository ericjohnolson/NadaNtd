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
        }

        public bool IsAggregatingLevel { get; set; }
        public bool IsDemographyAllowed { get; set; }
        public int LevelNumber { get; set; }
        public string DisplayName { get; set; }
        public string EditText { get { return Translations.View; } }
        public string ImportText { get { return Translations.Import; } }
        public string DeleteText { get { return Translations.Delete; } }
        
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

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "DisplayName":
                        if (string.IsNullOrEmpty(DisplayName))
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
