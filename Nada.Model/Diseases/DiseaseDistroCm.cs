using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Diseases
{
    [Serializable]
    public class DiseaseDistroCm : NadaClass, IHaveDynamicIndicators, IHaveDynamicIndicatorValues, IDataErrorInfo
    {
        public DiseaseDistroCm()
        {
            IndicatorValues = new List<IndicatorValue>();
        }

        public Nullable<int> AdminLevelId { get; set; }
        public Disease Disease { get; set; }
        public string Notes { get; set; }
        public Dictionary<string, Indicator> Indicators { get; set; }
        public List<IndicatorValue> IndicatorValues { get; set; }
        public List<KeyValuePair<int, string>> IndicatorDropdownValues { get; set; }
        public Nullable<int> YearReported { get; set; }

        public void MapIndicatorsToProperties()
        {
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(this);
            YearReported = inds["DiseaseYear"].DynamicValue.ToNullable<int>();
        }

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    default: error = "";
                        break;

                }
                return error;
            }
        }

        #endregion

    }
}
