using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Base;

namespace Nada.Model.Survey
{
    public class SentinelSite : NadaClass
    {
        public AdminLevel AdminLevel { get; set; }
        public string SiteName { get; set; }
        public Nullable<double> Lat { get; set; }
        public Nullable<double> Lng { get; set; }
        public string Notes { get; set; }
        public string SelectText { get { return "Select"; } }
    }
}
