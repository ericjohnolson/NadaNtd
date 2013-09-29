using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Base;

namespace Nada.Model.Survey
{
    public class SurveyType : NadaClass, IHaveDynamicIndicators
    {
        public SurveyType()
        {
            Indicators = new Dictionary<string, Indicator>();
        }
        public string SurveyTypeName { get; set; }
        public Dictionary<string, Indicator> Indicators { get; set; }
    }
}
