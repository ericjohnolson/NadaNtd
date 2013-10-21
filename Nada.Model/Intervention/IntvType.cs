using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Intervention
{
    public class IntvType : NadaClass, IHaveDynamicIndicators, IDataErrorInfo
    {
        public IntvType()
        {
            Indicators = new Dictionary<string, Indicator>();
        }
        public string IntvTypeName { get; set; }
        public Dictionary<string, Indicator> Indicators { get; set; }

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "SurveyTypeName":
                        if (String.IsNullOrEmpty(IntvTypeName))
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
