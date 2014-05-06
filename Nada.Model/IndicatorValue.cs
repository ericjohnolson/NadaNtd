using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Base;

namespace Nada.Model
{
    [Serializable]
    public class IndicatorValue : NadaClass
    {
        public int IndicatorId { get; set; }
        public string DynamicValue { get; set; }
        public Indicator Indicator { get; set; }
    }
}
