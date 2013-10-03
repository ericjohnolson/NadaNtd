using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Repositories;

namespace Nada.Model.Reports
{
    public class ReportGenerator
    {
        private ReportRepository repo = new ReportRepository();

        public ReportResult Run(ReportIndicators settings)
        {
            ReportResult result = new ReportResult();
            result.DataTableResults = CreateTable(settings);
            result.ChartData = CreateChart();
            repo.GetReportData(settings, result.DataTableResults, result.ChartData);
            result.ChartIndicators = GetChartIndicators(settings);
            return result;
        }

        private List<ReportIndicator> GetChartIndicators(ReportIndicators settings)
        {
            List<ReportIndicator> chartIndicators = new List<ReportIndicator>();
            chartIndicators.AddRange(settings.SurveyIndicators.Where(s => s.Selected && s.DataTypeId == 2));
            chartIndicators.AddRange(settings.InterventionIndicators.Where(s => s.Selected && s.DataTypeId == 2));
            return chartIndicators;
        }

        private DataTable CreateChart()
        {
            DataTable chartData = new DataTable();
            chartData.Columns.Add(new DataColumn("Location"));
            chartData.Columns.Add(new DataColumn("IndicatorName"));
            chartData.Columns.Add(new DataColumn("IndicatorId"));
            chartData.Columns.Add(new DataColumn("Year"));
            chartData.Columns.Add(new DataColumn("Value"));
            return chartData;
        }

        private DataTable CreateTable(ReportIndicators settings)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Location"));
            dt.Columns.Add(new DataColumn("Type"));
            dt.Columns.Add(new DataColumn("Year"));
            foreach (var i in settings.SurveyIndicators.Where(s => s.Selected))
                dt.Columns.Add(new DataColumn(i.Name));
            foreach (var i in settings.InterventionIndicators.Where(s => s.Selected))
                dt.Columns.Add(new DataColumn(i.Name));
            return dt;
        }
    }

    public interface IReportGenerator
    {
        ReportResult Run(ReportOptions options);
    }

    public class SurveyReportGenerator : IReportGenerator
    {
        private ReportRepository repo = new ReportRepository();
        public ReportResult Run(ReportOptions options)
        {
            ReportResult result = new ReportResult();
            result.DataTableResults = repo.CreateSurveyReport(options);
            return result;
        }
    }

    public class IntvReportGenerator : IReportGenerator
    {
        private ReportRepository repo = new ReportRepository();
        public ReportResult Run(ReportOptions options)
        {
            ReportResult result = new ReportResult();
            result.DataTableResults = repo.CreateIntvReport(options);
            return result;
        }
    }
}
