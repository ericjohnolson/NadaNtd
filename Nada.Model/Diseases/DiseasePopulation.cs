using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Base;
using Nada.Model.Diseases;

namespace Nada.Model
{
    [Serializable]
    public class DiseasePopulation : NadaClass, IHaveDynamicIndicators
    {
        public DiseasePopulation()
        {
            CustomIndicatorValues = new List<IndicatorValue>();
        }

        public int Year { get; set; }
        public Nullable<int> AdminLevelId { get; set; }
        public Disease Disease { get; set; }
        public string Notes { get; set; }
        public Dictionary<string, Indicator> Indicators { get; set; }
        public List<IndicatorValue> CustomIndicatorValues { get; set; }
    }
}
