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
        }
        public bool ShowDiseasesOption { get; set; }
        public List<ReportIndicator> AvailableIndicators { get; set; }
        public List<ReportIndicator> SelectedIndicators { get; set; }
        public List<Disease> SelectedDiseases { get; set; }
        public List<AdminLevel> SelectedAdminLevels { get; set; }
        public List<int> SelectedYears { get; set; }
        public IReportGenerator ReportGenerator { get; set; }
    }
}
