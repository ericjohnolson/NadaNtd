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

        public void MapPropertiesToIndicators()
        {
            Dictionary<string, Indicator> inds = Util.CreateIndicatorDictionary(TypeOfSurvey);
            IndicatorValues.Add(new IndicatorValue { IndicatorId = inds["CasualAgent"].Id, DynamicValue = CasualAgent });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = inds["YearFirstRoundPc"].Id, DynamicValue = YearFirstRoundPc.HasValue ? YearFirstRoundPc.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = inds["RoundsMda"].Id, DynamicValue = RoundsMda.HasValue ? RoundsMda.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = inds["Examined"].Id, DynamicValue = Examined.HasValue ? Examined.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = inds["Positive"].Id, DynamicValue = Positive.HasValue ? Positive.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = inds["PercentPositive"].Id, DynamicValue = PercentPositive.HasValue ? PercentPositive.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = inds["MeanDensity"].Id, DynamicValue = MeanDensity.HasValue ? MeanDensity.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = inds["MfCount"].Id, DynamicValue = MfCount.HasValue ? MfCount.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = inds["MfLoad"].Id, DynamicValue = MfLoad.HasValue ? MfLoad.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = inds["SampleSize"].Id, DynamicValue = SampleSize.HasValue ? SampleSize.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = inds["Sampled"].Id, DynamicValue = Sampled.HasValue ? Sampled.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = inds["Nonresponsive"].Id, DynamicValue = Nonresponsive.HasValue ? Nonresponsive.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = inds["AgeRange"].Id, DynamicValue = AgeRange }); 
        }

        public void MapIndicatorsToProperties()
        {
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(this);
            CasualAgent = inds["CasualAgent"].DynamicValue;
            AgeRange = inds["AgeRange"].DynamicValue;
            YearFirstRoundPc = inds["YearFirstRoundPc"].DynamicValue.ToNullable<int>();
            RoundsMda = inds["RoundsMda"].DynamicValue.ToNullable<int>();
            Examined = inds["Examined"].DynamicValue.ToNullable<int>();
            Positive = inds["Positive"].DynamicValue.ToNullable<int>();
            PercentPositive = inds["PercentPositive"].DynamicValue.ToNullable<double>();
            MeanDensity = inds["MeanDensity"].DynamicValue.ToNullable<double>();
            MfCount = inds["MfCount"].DynamicValue.ToNullable<int>();
            MfLoad = inds["MfLoad"].DynamicValue.ToNullable<double>();
            SampleSize = inds["SampleSize"].DynamicValue.ToNullable<int>();
            Sampled = inds["Sampled"].DynamicValue.ToNullable<int>();
            Nonresponsive = inds["Nonresponsive"].DynamicValue.ToNullable<int>();
        }

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
