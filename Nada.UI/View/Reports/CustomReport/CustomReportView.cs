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
using Nada.UI.Base;
using OfficeOpenXml;
using Nada.Globalization;
using C1.Win.C1Chart;
using System.Collections;
using Nada.Model.Repositories;

namespace Nada.UI.View.Reports.CustomReport
{
    public partial class CustomReportView : BaseForm
    {
        public System.Action OnSave { get; set; }
        public Action<SavedReport> OnEditReport { get; set; }
        private SavedReport report = null;
        private ReportResult currentResult = null;

        public CustomReportView(SavedReport o)
            : base()
        {
            report = o;
            InitializeComponent();
        }

        public CustomReportView()
            : base()
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
            currentResult = report.ReportOptions.ReportGenerator.Run(report.ReportOptions);
            grdReport.DataSource = currentResult.DataTableResults;
            LoadChart(currentResult);
        }

        private void LoadChart(ReportResult result)
        {
            c1Chart1.ChartGroups[0].ChartData.SeriesList.Clear();
            result.ChartData.Columns.Add("xaxis");
            foreach (DataRow dr in result.ChartData.Rows)
                dr["xaxis"] = dr[Translations.Location] + " - " + dr[Translations.Year];
            DataView dv = result.ChartData.DefaultView;
            dv.Sort = Translations.Year + ", " + Translations.Location;

            // copy data from table to chart
            int startColumn = result.ChartData.Columns.IndexOf(Translations.Year) + 1;
            int seriesIndex = 0;
            for (int i = startColumn; i < result.ChartData.Columns.Count; i++)
            {
                if (i == startColumn)
                    BindSeries(c1Chart1, seriesIndex, dv, result.ChartData.Columns[i].ColumnName, "xaxis");
                else if (result.ChartData.Columns[i].ColumnName == "xaxis")
                    continue;
                else
                    BindSeries(c1Chart1, seriesIndex, dv, result.ChartData.Columns[i].ColumnName);
                seriesIndex++;
            }
        }

        private void h3Link1_ClickOverride()
        {
            this.Close();
            OnEditReport(report);
        }

        private void lnkExport_ClickOverride()
        {
            if (currentResult.DataTableResults == null)
                return;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                    ws.Cells["A1"].LoadFromDataTable(currentResult.DataTableResults, true);
                    File.WriteAllBytes(saveFileDialog1.FileName, pck.GetAsByteArray());
                }
            }
        }

        private void h3Link2_ClickOverride()
        {
            if (currentResult.ChartData == null)
                return;

            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "JPEG (*.jpg)|*.jpg";
            sfd.DefaultExt = "png";
            sfd.FileName = Translations.Chart;
            sfd.OverwritePrompt = true;
            sfd.CheckPathExists = true;
            sfd.RestoreDirectory = false;
            sfd.ValidateNames = true;

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                c1Chart1.SaveImage(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg, new System.Drawing.Size(1024, 768));
            }
        }

        private void h3Link3_ClickOverride()
        {
            SaveReport saveReport = new SaveReport(report);
            saveReport.OnSave = OnSave;
            saveReport.ShowDialog(this);
        }

        #region chart helpers

        // copy data from a data source to the chart
        // c1c          chart
        // series       index of the series to bind (0-based, will add if necessary)
        // datasource   datasource object (cannot be DataTable, DataView is OK)
        // field        name of the field that contains the y values
        // labels       name of the field that contains the x labels
        private void BindSeries(C1Chart c1c, int series, object dataSource, string field, string labels)
        {
            // check data source object
            ITypedList il = (ITypedList)dataSource;
            IList list = (IList)dataSource;
            if (list == null || il == null)
                throw new ApplicationException("Invalid DataSource object.");

            // add series if necessary
            ChartDataSeriesCollection coll = c1c.ChartGroups[0].ChartData.SeriesList;
            while (series >= coll.Count)
                coll.AddNewSeries();

            // copy series data
            if (list.Count == 0) return;
            PointF[] data = (PointF[])Array.CreateInstance(typeof(PointF), list.Count);
            PropertyDescriptorCollection pdc = il.GetItemProperties(null);
            PropertyDescriptor pd = pdc[field];
            if (pd == null)
                throw new ApplicationException(string.Format("Invalid field name used for Y values ({0}).", field));

            int i;
            double d;
            for (i = 0; i < list.Count; i++)
            {
                data[i].X = i;

                if (Double.TryParse(pd.GetValue(list[i]).ToString(), out d))
                {
                    data[i].Y = float.Parse(pd.GetValue(list[i]).ToString());
                }
                else
                {
                    data[i].Y = float.NaN;
                }
                coll[series].PointData.CopyDataIn(data);
                coll[series].Label = field;
            }

            // copy series labels
            if (labels != null && labels.Length > 0)
            {
                pd = pdc[labels];
                if (pd == null)
                    throw new ApplicationException(string.Format("Invalid field name used for X values ({0}).", labels));
                Axis ax = c1c.ChartArea.AxisX;
                ax.ValueLabels.Clear();
                for (i = 0; i < list.Count; i++)
                {
                    string label = pd.GetValue(list[i]).ToString();
                    ax.ValueLabels.Add(i, label);
                }
                ax.AnnoMethod = AnnotationMethodEnum.ValueLabels;
            }
        }
        private void BindSeries(C1Chart c1c, int series, object dataSource, string field)
        {
            BindSeries(c1c, series, dataSource, field, null);
        }
        #endregion

    }
}
