using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Nada.Model.Survey;

namespace Nada.Model.Base
{
    [Serializable]
    public class SurveyBase : NadaClass, IHaveDynamicIndicatorValues
    {
        public SurveyBase()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }
        public Nullable<int> AdminLevelId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public SurveyType TypeOfSurvey { get; set; }
        public string Notes { get; set; }
        public List<IndicatorValue> IndicatorValues { get; set; }
    }
}
