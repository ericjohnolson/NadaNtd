using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Process
{
    public class ProcessType : NadaClass, IHaveDynamicIndicators, IDataErrorInfo
    {
        public ProcessType()
        {
            Indicators = new Dictionary<string, Indicator>();
            IndicatorDropdownValues = new List<IndicatorDropdownValue>();
        }
        public string TypeName { get; set; }
        public string DiseaseType { get; set; }
        public Dictionary<string, Indicator> Indicators { get; set; }
        public List<IndicatorDropdownValue> IndicatorDropdownValues { get; set; }


        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "TypeName":
                        if (String.IsNullOrEmpty(TypeName))
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
