using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Diseases;

namespace Nada.Model.Reports
{
    public class ReportOptions
    {
        public ReportOptions()
        {
            AvailableIndicators = new List<ReportIndicator>();
            SelectedIndicators = new List<ReportIndicator>();
            SelectedAdminLevels = new List<AdminLevel>();
            SelectedYears = new List<int> { DateTime.Now.Year };
            Columns = new Dictionary<string, AggregateIndicator>();
            IsNoAggregation = true;
            IsAllLocations = true;
        }
        public bool ShowDiseasesOption { get; set; }
        public List<ReportIndicator> AvailableIndicators { get; set; }
        public List<ReportIndicator> SelectedIndicators { get; set; }
        public Dictionary<string, AggregateIndicator> Columns { get; set; }
        public List<AdminLevel> SelectedAdminLevels { get; set; }
        public List<int> SelectedYears { get; set; }
        public int MonthYearStarts { get; set; }
        public IReportGenerator ReportGenerator { get; set; }
        public bool IsNoAggregation { get; set; }
        public bool IsCountryAggregation { get; set; }
        public bool IsByLevelAggregation { get; set; }
        public bool IsAllLocations { get; set; }

        // UI show/hide
        public bool HideAggregation { get; set; }

    }
}
