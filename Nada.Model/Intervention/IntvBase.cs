using System;
using System.Collections.Generic;
using System.Globalization;
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
        public int GetFirstAdminLevelId()
        {
            return AdminLevelId.Value;
        }
        public Nullable<int> AdminLevelId { get; set; }
        public DateTime StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public IntvType IntvType { get; set; }
        public DateTime DateReported { get; set; }
        public Nullable<int> PcIntvRoundNumber { get; set; }
        public string Notes { get; set; }
        public List<IndicatorValue> IndicatorValues { get; set; }
        public Dictionary<string, IndicatorValue> ValueDictionary { get; set; }
        public virtual void MapIndicatorsToProperties()
        {
            ValueDictionary = Util.CreateIndicatorValueDictionary(this);
            DateReported = DateTime.ParseExact(ValueDictionary["DateReported"].DynamicValue, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            if (ValueDictionary.ContainsKey("PcIntvStartDateOfMda"))
                StartDate = DateTime.ParseExact(ValueDictionary["PcIntvStartDateOfMda"].DynamicValue, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            if (ValueDictionary.ContainsKey("PcIntvEndDateOfMda") && !string.IsNullOrEmpty(ValueDictionary["PcIntvEndDateOfMda"].DynamicValue))
                EndDate = DateTime.ParseExact(ValueDictionary["PcIntvEndDateOfMda"].DynamicValue, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            if (ValueDictionary.ContainsKey("PcIntvRoundNumber"))
            {
                int round = 0;
                int.TryParse(ValueDictionary["PcIntvRoundNumber"].DynamicValue, out round);
                PcIntvRoundNumber = round;
            }
        }
        public virtual void MapPropertiesToIndicators() { }
    }
}
