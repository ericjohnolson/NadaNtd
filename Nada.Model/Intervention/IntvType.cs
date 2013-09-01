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
            Indicators = new List<Indicator>();
        }
        public string IntvTypeName { get; set; }
        public List<Indicator> Indicators { get; set; }
    }
}
