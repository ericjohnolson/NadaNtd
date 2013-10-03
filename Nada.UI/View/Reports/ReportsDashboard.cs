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

namespace Nada.UI.View.Reports
{
    public partial class ReportsDashboard : UserControl, IView
    {
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }

        public ReportsDashboard()
        {
            InitializeComponent();
        }
        
        private void ReportsDashboard_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
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
            ReportWizard wiz = new ReportWizard(new StepCategory());
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
            ReportWizard wiz = new ReportWizard(new StepIndicators(options));
            wiz.OnRunReport = RunCustomReport;
            wiz.Show();
        }
    }
}
