using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Reports;
using System.Windows.Forms.DataVisualization.Charting;
using OfficeOpenXml;
using System.IO;
using Nada.Model.Repositories;
using Nada.UI.Base;

namespace Nada.UI.View.Reports
{
    public partial class OldReportCreatorView : BaseControl
    {
        private ReportIndicators indicators = null;
        private ReportGenerator generator = null;
        private ReportResult currentResult = null;
        private Chart currentChart = null;

        public OldReportCreatorView()
            : base()
        {
            InitializeComponent();
        }

        private void ReportCreatorView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                CreateChart();
                generator = new ReportGenerator();
                ReportRepository repo = new ReportRepository();
                indicators = null;
                surveyIndicators.LoadIndicators(indicators.SurveyIndicators);
                intvIndicators.LoadIndicators(indicators.InterventionIndicators);
            }
        }

        private void CreateChart()
        {
            currentChart = new Chart();
            currentChart.Palette = ChartColorPalette.Pastel;
            currentChart.Dock = DockStyle.Fill;
            ChartArea CA1 = new ChartArea();
            Legend L1 = new Legend();
            L1.Name = "Legend2";
            L1.Title = "Location";
            L1.TitleFont = new Font("Microsoft Sans Serif", 12.0f, FontStyle.Bold);
            L1.Position = new ElementPosition(0.0f, 1.0f, 100.0f, 15.0f);
            CA1.Name = "ChartArea1";
            currentChart.Legends.Add(L1);
            currentChart.ChartAreas.Add(CA1);
            pnlChartContainer.Controls.Add(currentChart);
        }


        private void btnExportGrid_Click(object sender, EventArgs e)
        {
            if (currentResult == null)
                return;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    //Create the worksheet
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                    ws.Cells["A1"].LoadFromDataTable(currentResult.DataTableResults, true);
                    File.WriteAllBytes(saveFileDialog1.FileName, pck.GetAsByteArray());
                }
            }
        }

        private void btnExportChart_Click(object sender, EventArgs e)
        {
            if (currentChart == null)
                return;
            if (saveChart.ShowDialog() == DialogResult.OK)
                currentChart.SaveImage(saveChart.FileName, ChartImageFormat.Png);
        }

        private void cbChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChart();  
        }

        private void LoadChart()
        {
            if (cbChartType.SelectedItem == null)
                return; 

            DataTable chartData = currentResult.ChartData.Copy();
            List<DataRow> rowsToRemove = new List<DataRow>();
            for (int i = 0; i < chartData.Rows.Count; i++)
            {
                if (((ReportIndicator)cbChartType.SelectedItem).ID.ToString() != chartData.Rows[i]["IndicatorId"].ToString())
                {
                    rowsToRemove.Add(chartData.Rows[i]);
                }
            }

            foreach (var dr in rowsToRemove)
                chartData.Rows.Remove(dr);

            DataView dv = new DataView(chartData);
            dv.Sort = "Year";
            currentChart.Series.Clear();
            currentChart.DataBindCrossTable(dv, "Location", "Year", "Value", "");
            foreach (Series s in currentChart.Series)
            {
                s.ChartType = SeriesChartType.Column;
            }
            currentChart.DataBind();
        }

        private void btnGen2_Click(object sender, EventArgs e)
        {
            GenerateReports();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            GenerateReports();
        }

        private void GenerateReports()
        {
            indicators.SurveyIndicators = surveyIndicators.GetValues();
            indicators.InterventionIndicators = intvIndicators.GetValues();
            currentResult = generator.Run(indicators);
            dataGridView1.DataSource = currentResult.DataTableResults;
            tabControl1.SelectTab(1);
            reportIndicatorBindingSource.DataSource = currentResult.ChartIndicators;
            if (currentResult.ChartIndicators.Count > 0)
            {
                cbChartType.SelectedIndex = 0;
                LoadChart();
            }
        }

    }
}
