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
        public DateTime DateReported { get; set; }
        public Nullable<int> PcIntvRoundNumber { get; set; }
        public string Notes { get; set; }
        public List<IndicatorValue> IndicatorValues { get; set; }
        public virtual void MapIndicatorsToProperties()
        {
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(this);
            DateReported = Convert.ToDateTime(inds["DateReported"].DynamicValue);
            if(inds.ContainsKey("PcIntvStartDateOfMda"))
                StartDate = Convert.ToDateTime(inds["PcIntvStartDateOfMda"].DynamicValue);
            if (inds.ContainsKey("PcIntvEndDateOfMda"))
                EndDate = Convert.ToDateTime(inds["PcIntvEndDateOfMda"].DynamicValue);
            if (inds.ContainsKey("PcIntvRoundNumber"))
            {
                int round = 0;
                int.TryParse(inds["PcIntvRoundNumber"].DynamicValue, out round);
                PcIntvRoundNumber = round;
            }
        }
        public virtual void MapPropertiesToIndicators() { }
    }
}
