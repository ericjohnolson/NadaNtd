using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Repositories;
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
            IndicatorValues = new List<IndicatorValue>();
            AdminLevels = new List<AdminLevel>();
            SiteType = "Sentinel";
        }
        public List<AdminLevel> AdminLevels { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Year { get; set; }
        public SurveyType TypeOfSurvey { get; set; }
        public string SiteType { get; set; }
        public string SpotCheckName { get; set; }
        public Nullable<double> Lat { get; set; }
        public Nullable<double> Lng { get; set; }
        public Nullable<int> SentinelSiteId { get; set; }
        public string Notes { get; set; }
        public List<IndicatorValue> IndicatorValues { get; set; }
        public bool HasSentinelSite { get; set; }
        
        public SurveyBase CreateCopy(int newTypeId, int roundsId, int surveyTimingId, string roundsName, string timingName)
        {
            SurveyRepository repo = new SurveyRepository();
            SurveyBase copy = new SurveyBase();
            copy.TypeOfSurvey = repo.GetSurveyType(newTypeId);
            copy.AdminLevels = AdminLevels;
            copy.StartDate = StartDate;
            copy.EndDate = EndDate;
            copy.SiteType = Translations.Sentinel;
            copy.Notes = Notes;
            copy.HasSentinelSite = true;
            copy.IndicatorValues.Add(new IndicatorValue { DynamicValue = "0", IndicatorId = roundsId, Indicator = copy.TypeOfSurvey.Indicators[roundsName] });
            copy.IndicatorValues.Add(new IndicatorValue { DynamicValue = Translations.Baseline, IndicatorId = surveyTimingId, Indicator = copy.TypeOfSurvey.Indicators[timingName] });
            return copy;
        }
        public virtual void MapIndicatorsToProperties()
        {
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(this);
            Year = Convert.ToInt32(inds["SurveyYear"].DynamicValue);
        }
        public virtual void MapPropertiesToIndicators() { }

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "SiteType":
                        if (HasSentinelSite && String.IsNullOrEmpty(SiteType))
                            error = Translations.Required;
                        break;
                    case "SpotCheckName":
                        if (HasSentinelSite && SiteType != "Sentinel" && String.IsNullOrEmpty(SpotCheckName))
                            error = Translations.Required;
                        break;
                    case "SentinelSiteId":
                        if (HasSentinelSite && SiteType == "Sentinel" && (!SentinelSiteId.HasValue || SentinelSiteId.Value < 1))
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

                    default: error = "";
                        break;

                }
                return error;
            }
        }

        #endregion
    }
}
