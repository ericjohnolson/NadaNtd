using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Exports
{
    public class ExportJrfQuestions : NadaClass
    {
        public Nullable<int> JrfYearReporting { get; set; }
        public string JrfEndemicLf { get; set; }
        public string JrfEndemicOncho { get; set; }
        public string JrfEndemicSth { get; set; }
        public string JrfEndemicSch { get; set; }
        public AdminLevelType AdminLevelType { get; set; }

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "JrfYearReporting":
                        if (!JrfYearReporting.HasValue)
                            error = Translations.Required;
                        else if (JrfYearReporting.Value > 2100 || JrfYearReporting.Value < 1900)
                            error = Translations.ValidYear;
                        break;
                    case "JrfEndemicLf":
                        if (string.IsNullOrEmpty(JrfEndemicLf))
                            error = Translations.Required;
                        break;
                    case "JrfEndemicOncho":
                        if (string.IsNullOrEmpty(JrfEndemicOncho))
                            error = Translations.Required;
                        break;
                    case "JrfEndemicSth":
                        if (string.IsNullOrEmpty(JrfEndemicSth))
                            error = Translations.Required;
                        break;
                    case "JrfEndemicSch":
                        if (string.IsNullOrEmpty(JrfEndemicSch))
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
