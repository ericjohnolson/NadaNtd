using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model
{
    public class Partner : NadaClass
    {
        public string DisplayName { get; set; }
        public string EditText { get { return Translations.View; } }
        public string DeleteText { get { return Translations.Delete; } }

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
                        else if (DisplayName.Contains("&"))
                            error = Translations.ValidationNoIllegalChars;
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
