using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Reports;
using ClosedXML.Excel;
using System.Windows.Forms.DataVisualization.Charting;

namespace Nada.UI.View.Reports
{
    public partial class ReportCreatorView : UserControl
    {
        private ReportIndicators settings = new ReportIndicators();
        private ReportGenerator generator = null;
        private ReportResult currentResult = null;
        private Chart currentChart = null;

        public ReportCreatorView()
        {
            InitializeComponent();
        }

        private void ReportCreatorView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                CreateChart();
                generator = new ReportGenerator();
                bsReportSettings.DataSource = settings;
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
            L1.Title = "Admin Levels";
            L1.TitleFont = new Font("Microsoft Sans Serif", 15.0f, FontStyle.Bold);
            L1.Position = new ElementPosition(0.0f, 1.0f, 100.0f, 15.0f);
            CA1.Name = "ChartArea1";
            currentChart.Legends.Add(L1);
            currentChart.ChartAreas.Add(CA1);
            pnlChartContainer.Controls.Add(currentChart);
            
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            currentResult = generator.Run(settings);
            dataGridView1.DataSource = currentResult.DataTableResults;
            tabControl1.SelectTab(1);
            cbChartType.DataSource = currentResult.ChartIndicators;
            cbChartType.SelectedIndex = 0;
        }

        private void btnExportGrid_Click(object sender, EventArgs e)
        {
            if (currentResult == null)
                return;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                XLWorkbook workbook = new XLWorkbook();
                workbook.Worksheets.Add(currentResult.DataTableResults);
                workbook.SaveAs(saveFileDialog1.FileName);
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
            DataTable chartData = currentResult.ChartData.Copy();
            for (int i = 0; i < chartData.Rows.Count; i++)
            {
                if (cbChartType.SelectedValue.ToString() != chartData.Rows[i]["IndicatorName"].ToString())
                {
                    chartData.Rows.Remove(chartData.Rows[i]);
                }
            }
            DataView dv = new DataView(chartData);
            dv.Sort = "Year";
            currentChart.Series.Clear();
            currentChart.DataBindCrossTable(dv, "AdminLevel", "Year", "Value", "");
            foreach (Series s in currentChart.Series)
            {
                s.ChartType = SeriesChartType.Column;
            }
            currentChart.DataBind();
        }
    }
}
