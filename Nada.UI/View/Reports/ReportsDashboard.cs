using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;

using Nada.UI.View.Reports.CustomReport;
using Nada.Model.Reports;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Exports;
using Nada.UI.Base;
using System.IO;
using System.Configuration;
using Nada.Model.Repositories;
using Nada.UI.Controls;

namespace Nada.UI.View.Reports
{
    public partial class ReportsDashboard : BaseControl, IView
    {
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }
        public string Title { get { return ""; } }

        public void SetFocus()
        {
        }

        public ReportsDashboard()
            : base()
        {
            InitializeComponent();
        }

        private void ReportsDashboard_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                lblCmJrf.SetMaxWidth(400);
                LoadSavedReports();
            }
        }

        private void LoadSavedReports()
        {
            ReportRepository repo = new ReportRepository();
            var reports = repo.GetCustomReports();
            tblReportBuilder.Visible = false;
            this.SuspendLayout();
            int rowIndex = tblReportBuilder.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            var tblNew = new TableLayoutPanel { AutoSize = true, AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink };
            tblNew.RowStyles.Clear();
            tblNew.ColumnStyles.Clear();
            tblNew.ColumnStyles.Add(new ColumnStyle { SizeType = System.Windows.Forms.SizeType.AutoSize });
            tblNew.ColumnStyles.Add(new ColumnStyle { SizeType = System.Windows.Forms.SizeType.AutoSize });
            tblNew.RowStyles.Add(new RowStyle { SizeType = System.Windows.Forms.SizeType.AutoSize });
            var name2 = new H3bLabel { Text = Translations.CustomReport, Name = "rpt_cr", AutoSize = true, };
            name2.SetMaxWidth(400);
            var edit2 = new H3Link { Text = Translations.NewLink };
            edit2.ClickOverride += () =>
            {
                WizardForm wiz = new WizardForm(new StepCategory(), Translations.CustomReportBuilder);
                wiz.Height = 685;
                wiz.OnRunReport = RunCustomReport;
                wiz.Show();
            };
            tblNew.Controls.Add(name2, 0, 0);
            tblNew.Controls.Add(edit2, 1, 0);
            tblReportBuilder.Controls.Add(tblNew, 0, rowIndex);

            foreach (var report in reports)
            {
                rowIndex = tblReportBuilder.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                var tbl = new TableLayoutPanel { AutoSize = true, AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink };
                tbl.RowStyles.Clear();
                tbl.ColumnStyles.Clear();
                tbl.ColumnStyles.Add(new ColumnStyle { SizeType = System.Windows.Forms.SizeType.AutoSize });
                tbl.ColumnStyles.Add(new ColumnStyle { SizeType = System.Windows.Forms.SizeType.AutoSize });
                tbl.ColumnStyles.Add(new ColumnStyle { SizeType = System.Windows.Forms.SizeType.AutoSize });
                tbl.RowStyles.Add(new RowStyle { SizeType = System.Windows.Forms.SizeType.AutoSize });
                var name = new H3bLabel { Text = report.DisplayName, Name = "rpt_" + report.DisplayName, AutoSize = true, };
                name.SetMaxWidth(400);
                var edit = new H3Link { Text = Translations.NewLink };
                edit.ClickOverride += () =>
                {
                    WizardForm wiz = new WizardForm(new StepIndicators(report), Translations.CustomReportBuilder);
                    wiz.Height = 685;
                    wiz.OnRunReport = RunCustomReport;
                    wiz.Show();
                };
                var delete = new H3Link { Text = Translations.NewLink };
                delete.ClickOverride += () => { repo.DeleteCustomReport(report, ApplicationData.Instance.GetUserId()); };
                tbl.Controls.Add(name, 0, 0);
                tbl.Controls.Add(edit, 1, 0);
                tbl.Controls.Add(delete, 2, 0);
                tblReportBuilder.Controls.Add(tbl, 0, rowIndex);
            }
            this.ResumeLayout();
            tblReportBuilder.Visible = true;
        }

        private void btnDash_Click(object sender, EventArgs e)
        {
            OnClose();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "file:///" + Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["HelpFile"]);
            //HelpView help = new HelpView();
            //help.Show();
        }


        private void RunCustomReport(SavedReport r)
        {
            CustomReportView report = new CustomReportView(r);
            report.OnEditReport = EditCustomReport;
            report.OnSave = LoadSavedReports;
            report.Show();
        }

        private void EditCustomReport(SavedReport r)
        {
            WizardForm wiz = new WizardForm(new StepIndicators(r), Translations.CustomReportBuilder);
            wiz.Height = 685;
            wiz.OnRunReport = RunCustomReport;
            wiz.Show();
        }

        private void lnkPcJrfForm_ClickOverride()
        {
            PcJrfExporter exporter = new PcJrfExporter();
            WizardForm wiz = new WizardForm(new JrfExportStep(exporter, exporter.ExportName), exporter.ExportName);
            wiz.OnFinish = () => { };
            wiz.ShowDialog();
        }

        private void h3Link2_ClickOverride()
        {
            CmJrfExporter exporter = new CmJrfExporter();
            WizardForm wiz = new WizardForm(new ExportStep(exporter, exporter.ExportName), exporter.ExportName);
            wiz.OnFinish = () => { };
            wiz.ShowDialog();
        }

        private void tblCustom_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
