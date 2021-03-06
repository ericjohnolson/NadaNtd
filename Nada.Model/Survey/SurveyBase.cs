﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Repositories;
using Nada.Model.Survey;

namespace Nada.Model.Base
{
    /// <summary>
    /// Models a saved survey form
    /// </summary>
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
            SiteTypeId = SiteTypeId.Sentinel;
        }
        public Nullable<int> AdminLevelId
        {
            get
            {
                // AdminLevels property not always being populated in every case this is used
                // Need to make sure it is always populated, but to save time/budget, for now just
                // add this check to return null if admin level id can't be determined from AdminLevels
                if (AdminLevels == null || AdminLevels.Count < 1)
                    return null;

                return AdminLevels.FirstOrDefault().Id;
            }
        }

        public int SortOrder
        {
            get
            {
                // AdminLevels property not always being populated in every case this is used
                // Need to make sure it is always populated, but to save time/budget, for now just
                // add this check to return 0 if sort order can't be determined from AdminLevels
                if (AdminLevels == null || AdminLevels.Count < 1)
                    return 0;

                return AdminLevels.OrderBy(a => a.SortOrder).First().SortOrder;
            }
        }

        public List<AdminLevel> AdminLevels { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateReported { get; set; }
        public SurveyType TypeOfSurvey { get; set; }
        public string SiteType { get; set; }
        private SiteTypeId siteTypeId;
        public SiteTypeId SiteTypeId
        {
            get
            {
                return siteTypeId;
            }
            set
            {
                siteTypeId = value;
                if (SiteTypeId == SiteTypeId.Sentinel)
                {
                    SiteType = TranslationLookup.GetValue("Sentinel", "Sentinel");
                }
                else if (SiteTypeId == SiteTypeId.SpotCheck)
                {
                    SiteType = TranslationLookup.GetValue("SpotCheck", "SpotCheck");
                }
            }
        }
        public string SpotCheckName { get; set; }
        public Nullable<double> Lat { get; set; }
        public Nullable<double> Lng { get; set; }
        public Nullable<int> SentinelSiteId { get; set; }
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
            copy.SiteTypeId = SiteTypeId.Sentinel;
            copy.Notes = Notes;
            copy.HasSentinelSite = true;
            copy.IndicatorValues.Add(new IndicatorValue { DynamicValue = "0", IndicatorId = roundsId, Indicator = copy.TypeOfSurvey.Indicators[roundsName] });
            copy.IndicatorValues.Add(new IndicatorValue { DynamicValue = Translations.Baseline, IndicatorId = surveyTimingId, Indicator = copy.TypeOfSurvey.Indicators[timingName] });
            return copy;
        }
        public virtual void MapIndicatorsToProperties()
        {
            Dictionary<string, IndicatorValue> inds = Util.CreateIndicatorValueDictionary(this);
            DateReported = DateTime.ParseExact(inds["DateReported"].DynamicValue, "MM/dd/yyyy", CultureInfo.InvariantCulture);
        }
        public virtual void MapPropertiesToIndicators()
        {
            ParseNotes(this, TypeOfSurvey.Indicators["Notes"]);
        }

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
                        if (HasSentinelSite && SiteTypeId != SiteTypeId.Sentinel && String.IsNullOrEmpty(SpotCheckName))
                            error = Translations.Required;
                        break;
                    case "SentinelSiteId":
                        if (HasSentinelSite && SiteTypeId == SiteTypeId.Sentinel && (!SentinelSiteId.HasValue || SentinelSiteId.Value < 1))
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
