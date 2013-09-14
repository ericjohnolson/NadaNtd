using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Base;

namespace Nada.Model
{
    [Serializable]
    public class DiseaseDistribution : NadaClass, IHaveDynamicIndicators
    {
        public DiseaseDistribution()
        {
            CustomIndicatorValues = new List<IndicatorValue>();

        }

        public Nullable<int> AdminLevelId { get; set; }
        public Disease Disease { get; set; }
        public string Notes { get; set; }
        public List<Indicator> Indicators { get; set; }
        public List<IndicatorValue> CustomIndicatorValues { get; set; }
    }
}
