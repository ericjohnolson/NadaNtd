using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Survey;

namespace Nada.Model.Base
{
    [Serializable]
    public class SurveyBase : NadaClass
    {
        public Nullable<int> AdminLevelId { get; set; }
        public DateTime SurveyDate { get; set; }
        public SurveyType TypeOfSurvey { get; set; }
        public string Notes { get; set; }
        public List<IndicatorValue> CustomIndicatorValues { get; set; }
    }
}
