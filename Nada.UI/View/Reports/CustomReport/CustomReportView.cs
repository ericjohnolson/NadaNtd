using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Reports;
using Nada.UI.AppLogic;
using OfficeOpenXml;

namespace Nada.UI.View.Reports.CustomReport
{
    public partial class CustomReportView : Form
    {
        public Action<ReportOptions> OnEditReport { get; set; }
        private ReportOptions options = null;
        private DataTable currentResult = null;

        public CustomReportView(ReportOptions o)
        {
            options = o;
            InitializeComponent();
        }

        public CustomReportView()
        {
            InitializeComponent();
        }

        private void CustomReport_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                CreateReport();
            }
        }

        private void CreateReport()
        {
            var result = options.ReportGenerator.Run(options);
            currentResult = result.DataTableResults;
            grdReport.DataSource = currentResult;
        }

        private void h3Link1_ClickOverride()
        {
            this.Close();
            OnEditReport(options);
        }

        private void lnkExport_ClickOverride()
        {
            if (currentResult == null)
                return;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                    ws.Cells["A1"].LoadFromDataTable(currentResult, true);
                    File.WriteAllBytes(saveFileDialog1.FileName, pck.GetAsByteArray());
                }
            }
        }
    }
}
