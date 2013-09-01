using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Base;
using Nada.Model.Survey;

namespace Nada.Model.Intervention
{
    [Serializable]
    public class IntvBase : NadaClass
    {
        public IntvBase()
        {
            CustomIndicatorValues = new List<IndicatorValue>();
        }
        public Nullable<int> AdminLevelId { get; set; }
        public DateTime IntvDate { get; set; }
        public IntvType IntvType { get; set; }
        public string Notes { get; set; }
        public List<IndicatorValue> CustomIndicatorValues { get; set; }
    }
}
