using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Base;

namespace Nada.Model.Survey
{
    public class LfMfPrevalence : SurveyBase
    {
        public LfMfPrevalence()
        {
            TimingType = "Baseline";
            SiteType = "Sentinel";
            TestType = "MF";
        }

        public string TimingType { get; set; }
        public string TestType { get; set; }
        public string SiteType { get; set; }
        public string SpotCheckName { get; set; }
        public Nullable<double> Lat { get; set; }
        public Nullable<double> Lng { get; set; }
        public Nullable<int> SentinelSiteId { get; set; }
        public Nullable<int> RoundsMda { get; set; }
        public Nullable<int> Examined { get; set; }
        public Nullable<int> Positive { get; set; }
        public Nullable<double> MeanDensity { get; set; }
        public Nullable<int> MfCount { get; set; }
        public Nullable<double> MfLoad { get; set; }
        public Nullable<int> SampleSize { get; set; }
        public string AgeRange { get; set; }

        // unbound
        public string SpotCheckAdminLevel { get; set; }
        public string SentinelSiteName { get; set; }
    }
}
