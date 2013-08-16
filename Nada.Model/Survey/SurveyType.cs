using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Base;

namespace Nada.Model.Survey
{
    public class SurveyType : NadaClass
    {
        public SurveyType()
        {
            Indicators = new List<SurveyIndicator>();
        }
        public string SurveyTypeName { get; set; }
        public List<SurveyIndicator> Indicators { get; set; }
    }
}
