﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model.Reports
{
    public class ReportIndicators
    {
        public ReportIndicators()
        {
            SurveyIndicators = new List<ReportIndicator>();
            InterventionIndicators = new List<ReportIndicator>();
        }
        public List<ReportIndicator> SurveyIndicators { get; set; }
        public List<ReportIndicator> InterventionIndicators { get; set; }
    }

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

    }
}
