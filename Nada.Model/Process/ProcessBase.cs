using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using Nada.Model.Base;
using Nada.Model.Survey;

namespace Nada.Model.Process
{
    [Serializable]
    public class ProcessBase : NadaClass, IHaveDynamicIndicatorValues
    {
        public ProcessBase()
        {
            DateReported = DateTime.Now;
            IndicatorValues = new List<IndicatorValue>();
        }
        
        public Nullable<int> AdminLevelId { get; set; }
        public DateTime DateReported { get; set; }
        public ProcessType ProcessType { get; set; }
        public string PCTrainTrainingCategory { get; set; }
        public string SCMDrug { get; set; }
        public List<IndicatorValue> IndicatorValues { get; set; }
        public virtual void MapIndicatorsToProperties()
        {
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(this);

            DateReported = DateTime.ParseExact(inds["DateReported"].DynamicValue, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            if (inds.ContainsKey("SCMDrug"))
                SCMDrug = inds["SCMDrug"].DynamicValue;
            if (inds.ContainsKey("PCTrainTrainingCategory"))
                PCTrainTrainingCategory = inds["PCTrainTrainingCategory"].DynamicValue;
        }

        public virtual void MapPropertiesToIndicators()
        {
            ParseNotes(this, ProcessType.Indicators["Notes"]);
        }
    }
}
