using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Nada.Globalization;


namespace Nada.Model.Survey
{
    public class LfMfPrevalence : SurveyBase, IDataErrorInfo
    {
        public LfMfPrevalence() : base()
        {
            TimingType = "Baseline";
            SiteType = "Sentinel";
            TestType = "MF";
            Partners = new List<Partner>();
            Vectors = new List<Vector>();
        }

        // database fields
        public string TimingType { get; set; }
        public string TestType { get; set; }
        public string SiteType { get; set; }
        public string SpotCheckName { get; set; }
        public Nullable<double> Lat { get; set; }
        public Nullable<double> Lng { get; set; }
        public Nullable<int> SentinelSiteId { get; set; }
        // List fields
        public List<Partner> Partners { get; set; }
        public List<Vector> Vectors { get; set; }

        // dynamic indicators
        public string CasualAgent { get; set; }
        public Nullable<int> YearFirstRoundPc { get; set; }
        public Nullable<int> RoundsMda { get; set; }
        public Nullable<int> Examined { get; set; }
        public Nullable<int> Positive { get; set; }
        public Nullable<double> PercentPositive { get; set; }
        public Nullable<double> MeanDensity { get; set; }
        public Nullable<int> MfCount { get; set; }
        public Nullable<double> MfLoad { get; set; }
        public Nullable<int> SampleSize { get; set; }
        public Nullable<int> Sampled { get; set; }
        public Nullable<int> Nonresponsive { get; set; }
        public string AgeRange { get; set; }

        // Display only
        public string SpotCheckAdminLevel { get; set; }
        public string SentinelSiteName { get; set; }


        #region IDataErrorInfo Members

        public override string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "TimingType":
                        if (String.IsNullOrEmpty(TimingType))
                            _lastError = Translations.Required;
                        break;
                    case "TestType":
                        if (String.IsNullOrEmpty(TestType))
                            _lastError = Translations.Required;
                        break;
                    case "SiteType":
                        if (String.IsNullOrEmpty(SiteType))
                            _lastError = Translations.Required;
                        break;
                    case "YearFirstRoundPc":
                        if (!YearFirstRoundPc.HasValue)
                            _lastError = Translations.Required;
                        else if (YearFirstRoundPc.Value > 2050 || YearFirstRoundPc.Value < 1900)
                            _lastError = Translations.ValidYear;
                        break;
                    case "AgeRange":
                        if (String.IsNullOrEmpty(AgeRange))
                            _lastError = Translations.Required;
                        break;
                    case "Positive":
                        if (!Positive.HasValue)
                            _lastError = Translations.Required;
                        break;
                    case "Examined":
                        if (!Examined.HasValue)
                            _lastError = Translations.Required;
                        break;
                    case "Lat":
                        if (Lat.HasValue && (Lat > 90 || Lat < -90))
                            _lastError = Translations.ValidLatitude;
                        break;
                    case "Lng":
                        if (Lng.HasValue && (Lng > 180 || Lng < -180))
                            _lastError = Translations.ValidLatitude;
                        break;
                    case "RoundsMda":
                        if (RoundsMda.HasValue && (RoundsMda > 20 || RoundsMda < 1))
                            _lastError = Translations.ValidRoundsMda;
                        break;

                    default: _lastError = "";
                        break;

                }
                return _lastError;
            }
        }

        #endregion
    }
}
