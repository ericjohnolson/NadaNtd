﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nada.Model.Reports
{
    public class ReportResult
    {
        public DataTable DataTableResults { get; set; }
        public DataTable ChartData { get; set; }
        public List<ReportIndicator> ChartIndicators { get; set; }
        public string MetaDataWarning { get; set; }
    }
}
