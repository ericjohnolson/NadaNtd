using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.UI.View.Help;
using Nada.UI.View.Reports.CustomReport;
using Nada.Model.Reports;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Exports;

namespace Nada.UI.View.Reports
{
    public partial class ReportsDashboard : UserControl, IView
    {
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }
        public string Title { get { return ""; } }

        public ReportsDashboard()
        {
            InitializeComponent();
        }
        
        private void ReportsDashboard_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                lblCmJrf.SetMaxWidth(400);
                lblCustom.SetMaxWidth(400);
            }
        }

        private void btnDash_Click(object sender, EventArgs e)
        {
            OnClose();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            HelpView help = new HelpView();
            help.Show();
        }

        private void lnkCustom_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WizardForm wiz = new WizardForm(new StepCategory(), Translations.CustomReportBuilder);
            wiz.OnRunReport = RunCustomReport;
            wiz.Show();
        }

        private void RunCustomReport(ReportOptions options)
        {
            CustomReportView report = new CustomReportView(options);
            report.OnEditReport = EditCustomReport;
            report.Show();
        }

        private void EditCustomReport(ReportOptions options)
        {
            WizardForm wiz = new WizardForm(new StepIndicators(options), Translations.CustomReportBuilder);
            wiz.OnRunReport = RunCustomReport;
            wiz.Show();
        }

        private void lnkCmJrf_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IExporter exporter = new CmJrfExporter();
            WizardForm wiz = new WizardForm(new ExportStep(exporter, exporter.ExportName), exporter.ExportName);
            wiz.OnFinish = () => { };
            wiz.ShowDialog();
        }
    }
}
