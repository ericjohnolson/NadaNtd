using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Diseases;

namespace Nada.Model.Reports
{
    [Serializable]
    public class ReportOptions
    {
        public ReportOptions()
        {
            AvailableIndicators = new List<ReportIndicator>();
            SelectedIndicators = new List<ReportIndicator>();
            SelectedAdminLevels = new List<AdminLevel>();
            Columns = new Dictionary<string, AggregateIndicator>();
            IsNoAggregation = true;
            IsAllLocations = true;
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MinValue;
        }
        public bool ShowDiseasesOption { get; set; }
        public List<ReportIndicator> AvailableIndicators { get; set; }
        public List<ReportIndicator> SelectedIndicators { get; set; }
        public Dictionary<string, AggregateIndicator> Columns { get; set; }
        public List<AdminLevel> SelectedAdminLevels { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MonthYearStarts { get; set; }
        public BaseReportGenerator ReportGenerator { get; set; }
        public IndicatorEntityType EntityType { get; set; }
        public bool IsNoAggregation { get; set; }
        public bool IsCountryAggregation { get; set; }
        public bool IsByLevelAggregation { get; set; }
        public bool IsAllLocations { get; set; }
        public bool ShowRedistrictEvents { get; set; }
        public bool ShowOnlyRedistrictedUnits { get; set; }

        // UI show/hide
        public bool HideAggregation { get; set; }

    }
}
