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
            IndicatorDropdownValues = new List<IndicatorDropdownValue>();
        }

        public Nullable<int> AdminLevelId { get; set; }
        public Disease Disease { get; set; }
        public string Notes { get; set; }
        public Dictionary<string, Indicator> Indicators { get; set; }
        public List<IndicatorValue> IndicatorValues { get; set; }
        public List<IndicatorDropdownValue> IndicatorDropdownValues { get; set; }
        public Nullable<DateTime> DateReported { get; set; }

        public void MapIndicatorsToProperties()
        {
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(this);
            if (inds.ContainsKey("DateReported"))
                DateReported = Convert.ToDateTime(inds["DateReported"].DynamicValue);
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
