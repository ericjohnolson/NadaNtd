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

        public override void MapPropertiesToIndicators()
        {
            IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["CasualAgent"].Id, DynamicValue = CasualAgent });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["YearFirstRoundPc"].Id, DynamicValue = YearFirstRoundPc.HasValue ? YearFirstRoundPc.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["RoundsMda"].Id, DynamicValue = RoundsMda.HasValue ? RoundsMda.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["Examined"].Id, DynamicValue = Examined.HasValue ? Examined.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["Positive"].Id, DynamicValue = Positive.HasValue ? Positive.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["PercentPositive"].Id, DynamicValue = PercentPositive.HasValue ? PercentPositive.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["MeanDensity"].Id, DynamicValue = MeanDensity.HasValue ? MeanDensity.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["MfCount"].Id, DynamicValue = MfCount.HasValue ? MfCount.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["MfLoad"].Id, DynamicValue = MfLoad.HasValue ? MfLoad.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["SampleSize"].Id, DynamicValue = SampleSize.HasValue ? SampleSize.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["Sampled"].Id, DynamicValue = Sampled.HasValue ? Sampled.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["Nonresponsive"].Id, DynamicValue = Nonresponsive.HasValue ? Nonresponsive.Value.ToString() : null });
            IndicatorValues.Add(new IndicatorValue { IndicatorId = TypeOfSurvey.Indicators["AgeRange"].Id, DynamicValue = AgeRange }); 
        }

        public override void MapIndicatorsToProperties()
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
                string error = "";
                switch (columnName)
                {
                    case "TimingType":
                        if (String.IsNullOrEmpty(TimingType))
                            error = Translations.Required;
                        break;
                    case "TestType":
                        if (String.IsNullOrEmpty(TestType))
                            error = Translations.Required;
                        break;
                    case "SiteType":
                        if (String.IsNullOrEmpty(SiteType))
                            error = Translations.Required;
                        break;
                    case "SpotCheckName":
                        if (SiteType != "Sentinel" && String.IsNullOrEmpty(SpotCheckName))
                            error = Translations.Required;
                        break;
                    case "SentinelSiteId":
                        if (SiteType == "Sentinel" && (!SentinelSiteId.HasValue || SentinelSiteId.Value < 1))
                            error = Translations.Required;
                        break;
                    case "YearFirstRoundPc":
                        if (!YearFirstRoundPc.HasValue)
                            error = Translations.Required;
                        else if (YearFirstRoundPc.Value > 2050 || YearFirstRoundPc.Value < 1900)
                            error = Translations.ValidYear;
                        break;
                    case "AgeRange":
                        if (String.IsNullOrEmpty(AgeRange))
                            error = Translations.Required;
                        break;
                    case "Positive":
                        if (!Positive.HasValue)
                            error = Translations.Required;
                        break;
                    case "Examined":
                        if (!Examined.HasValue)
                            error = Translations.Required;
                        break;
                    case "Lat":
                        if (Lat.HasValue && (Lat > 90 || Lat < -90))
                            error = Translations.ValidLatitude;
                        break;
                    case "Lng":
                        if (Lng.HasValue && (Lng > 180 || Lng < -180))
                            error = Translations.ValidLongitude;
                        break;
                    case "RoundsMda":
                        if (RoundsMda.HasValue && (RoundsMda > 20 || RoundsMda < 1))
                            error = Translations.ValidRoundsMda;
                        break;

                    default: error = "";
                        break;

                }
                return error;
            }
        }

        #endregion

    }
}
