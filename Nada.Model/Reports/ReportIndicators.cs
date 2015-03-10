using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model.Reports
{
    //public class ReportIndicators
    //{
    //    public ReportIndicators()
    //    {
    //        SurveyIndicators = new List<ReportIndicator>();
    //        InterventionIndicators = new List<ReportIndicator>();
    //    }
    //    public List<ReportIndicator> SurveyIndicators { get; set; }
    //    public List<ReportIndicator> InterventionIndicators { get; set; }
    //}


    [Serializable]
    public class ReportIndicator
    {
        public ReportIndicator()
        {
            Children = new List<ReportIndicator>();
            ParentId = 0;
        }
        public string Name { get; set; }
        public string Key { get; set; }
        public int ID { get; set; }
        public bool Selected { get; set; }
        public bool IsStatic { get; set; }
        public int DataTypeId { get; set; }
        public int ParentId { get; set; }
        public List<ReportIndicator> Children { get; set; }
        public bool IsCategory { get; set; }
        public bool IsChecked { get; set; }
        public bool IsCalculated { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsRequired { get; set; }
        public int TypeId { get; set; }
        public int AggregationRuleId { get; set; }
        public int MergeRule { get; set; }
        public int SplitRule { get; set; }
        public string FormName { get; set; }
        public int SortOrder { get; set; }
    }
}
