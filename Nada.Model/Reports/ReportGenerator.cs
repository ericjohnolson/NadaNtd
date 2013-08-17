using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Nada.Model.Repositories;

namespace Nada.Model.Reports
{
    public class ReportGenerator
    {
        private ReportRepository repo = new ReportRepository();

        public ReportResult Run(ReportIndicators settings)
        {
            ReportResult result = new ReportResult();
            DataTable data = repo.GetReportData(settings);
            result.DataTableResults = FormatResultData(data);
            result.ChartData = GenerateChartData(data);
            result.ChartIndicators = GetChartIndicators(settings);
            return result;
        }

        private List<string> GetChartIndicators(ReportIndicators settings)
        {
            List<string> inds = new List<string>();
            if (settings.ShowRoundsMda)
                inds.Add("RoundsMda");
            if (settings.ShowExamined)
                inds.Add("Examined");
            if (settings.ShowPositive)
                inds.Add("Positive");
            if (settings.ShowMeanDensity)
                inds.Add("MeanDensity");
            if (settings.ShowMfCount)
                inds.Add("MfCount");
            if (settings.ShowMfLoad)
                inds.Add("MfLoad");
            if (settings.ShowSampleSize)
                inds.Add("SampleSize");
            return inds;
        }

        private DataTable FormatResultData(DataTable data)
        {
            DataTable resultsTable = data.Copy();
            foreach (DataRow dr in resultsTable.Rows)
            {
                List<string> surveyType = new List<string>();
                surveyType.Add(dr["TestType"].ToString());
                surveyType.Add(dr["TimingType"].ToString());
                surveyType.Add(dr["SiteType"].ToString());
                surveyType.Add(Convert.ToDateTime(dr["SurveyDate"]).ToString("MM/yyyy"));
                dr["TimingType"] = string.Join(", ", surveyType.Where(t => !string.IsNullOrEmpty(t)).ToArray());
            }
            resultsTable.Columns.Remove(resultsTable.Columns["TestType"]);
            resultsTable.Columns.Remove(resultsTable.Columns["SurveyDate"]);
            resultsTable.Columns.Remove(resultsTable.Columns["SiteType"]);
            resultsTable.Columns[1].ColumnName = "Survey Type";
            return resultsTable;
        }

        private DataTable GenerateChartData(DataTable data)
        {
            DataTable chartData = new DataTable();
            chartData.Columns.Add(new DataColumn("AdminLevel"));
            chartData.Columns.Add(new DataColumn("IndicatorName"));
            chartData.Columns.Add(new DataColumn("Year", typeof(int)));
            chartData.Columns.Add(new DataColumn("Value", typeof(double)));
            foreach (DataRow dr in data.Rows)
            {
                CreateRowForIndicator("RoundsMda", chartData, dr);
                CreateRowForIndicator("Examined", chartData, dr);
                CreateRowForIndicator("Positive", chartData, dr);
                CreateRowForIndicator("MeanDensity", chartData, dr);
                CreateRowForIndicator("MfCount", chartData, dr);
                CreateRowForIndicator("MfLoad", chartData, dr);
                CreateRowForIndicator("SampleSize", chartData, dr);
            }
            return chartData;
        }

        private void CreateRowForIndicator(string name, DataTable chartData, DataRow dr)
        {
            if (dr.Table.Columns.Contains(name) && !string.IsNullOrEmpty(dr[name].ToString()))
            {
                var newRow = chartData.NewRow();
                newRow["AdminLevel"] = dr["AdminLevel"];
                newRow["Year"] = Convert.ToDateTime(dr["SurveyDate"]).Year;
                newRow["IndicatorName"] = name;
                newRow["Value"] = Convert.ToDouble(dr[name]);
                chartData.Rows.Add(newRow);

            }
        }
    }
}
