using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Base;
using Nada.Model.Survey;

namespace Nada.Model.Intervention
{
    [Serializable]
    public class IntvBase : NadaClass, IHaveDynamicIndicatorValues
    {
        public IntvBase()
        {
            IndicatorValues = new List<IndicatorValue>();
        }
        public Nullable<int> AdminLevelId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IntvType IntvType { get; set; }
        public string Notes { get; set; }
        public List<IndicatorValue> IndicatorValues { get; set; }
        public virtual void MapIndicatorsToProperties() { }
        public virtual void MapPropertiesToIndicators() { }
    }
}
