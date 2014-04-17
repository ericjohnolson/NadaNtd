using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Nada.Model.Base;

namespace Nada.Model.Exports
{
    public class ExportType : NadaClass, IHaveDynamicIndicators, IDataErrorInfo
    {
        public ExportType()
        {
            Indicators = new Dictionary<string, Indicator>();
            IndicatorValues = new List<IndicatorValue>();
        }

        public Dictionary<string, Indicator> Indicators { get; set; }
        public List<IndicatorValue> IndicatorValues { get; set; }

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


        public List<IndicatorDropdownValue> IndicatorDropdownValues
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
