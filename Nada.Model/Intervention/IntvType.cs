using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Base;

namespace Nada.Model.Intervention
{
    public class IntvType : NadaClass, IHaveDynamicIndicators
    {
        public IntvType()
        {
            Indicators = new Dictionary<string, Indicator>();
        }
        public string IntvTypeName { get; set; }
        public Dictionary<string, Indicator> Indicators { get; set; }
    }
}
