using Nada.Model.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model.Imports
{
    public class SurveyUnitsAndSentinelSite
    {
        public SurveyUnitsAndSentinelSite()
        {
            Units = new List<AdminLevel>();
        }

        public List<AdminLevel> Units { get; set; }
        public string SentinelSiteName { get; set; }
        public bool NeedsSentinelSite { get; set; }
    }
}
