using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Survey
{
    [Serializable]
    public class SurveyType : NadaClass, IHaveDynamicIndicators, IDataErrorInfo
    {
        public SurveyType()
        {
            Indicators = new Dictionary<string, Indicator>();
            IndicatorDropdownValues = new List<IndicatorDropdownValue>();
        }
        public string DisplayNameKey { get; set; }
        public string SurveyTypeName { get; set; }
        public Dictionary<string, Indicator> Indicators { get; set; }
        public List<IndicatorDropdownValue> IndicatorDropdownValues { get; set; }
        public int DiseaseId { get; set; }
        public string DiseaseType { get; set; }
        public bool HasMultipleLocations { get; set; }

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "SurveyTypeName":
                        if (String.IsNullOrEmpty(SurveyTypeName))
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
