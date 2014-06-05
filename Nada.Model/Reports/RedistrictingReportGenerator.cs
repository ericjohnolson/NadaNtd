using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Repositories;

namespace Nada.Model.Reports
{
    public class RedistrictingReportGenerator : BaseReportGenerator
    {
        public override ReportResult Run(SavedReport report)
        {
            repo = new ReportRepository();
            ReportResult result = new ReportResult();
            result.DataTableResults = repo.RunRedistrictingReport();
            result.ChartData = null;
            return result;
        }

    }

}
