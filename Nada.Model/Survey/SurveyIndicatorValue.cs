using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Base;

namespace Nada.Model.Survey
{
    public class SurveyIndicatorValue : NadaClass, IDynamicIndicatorValue
    {
        public int IndicatorId { get; set; }
        public string DynamicValue { get; set; }
    }
}
