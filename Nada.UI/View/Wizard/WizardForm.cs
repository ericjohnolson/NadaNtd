using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Reports;
using Nada.UI.AppLogic;
using Nada.UI.Base;

namespace Nada.UI.View
{
    public partial class WizardForm : BaseForm
    {
        private string title = "";
        public Action OnFinish { get; set; }
        public Action OnClose = () => { };
        public Action<SavedReport> OnRunReport { get; set; }
        private IWizardStep currentStep = null;

        public WizardForm()
            : base()
        {
            InitializeComponent();
        }

        public WizardForm(IWizardStep step, string t)
            : base()
        {
            currentStep = step;
            title = t;
            InitializeComponent();
        }

        private void ReportWizard_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                this.Text = title;
                lblTitle.Text = title;
                LoadStep(currentStep);
            }
        }

        private void LoadStep(IWizardStep step)
        {
            step.OnSwitchStep = (s) => { LoadStep(s); };
            step.OnRunReport = RunReport;
            step.OnFinish = DoFinish;
            lblStepTitle.Text = step.StepTitle;
            btnPrev.Visible = step.ShowPrev;
            btnPrev.Enabled = step.EnablePrev;
            btnNext.Visible = step.ShowNext;
            btnNext.Enabled = step.EnableNext;
            btnFinish.Visible = step.ShowFinish;
            btnFinish.Enabled = step.EnableFinish;
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add((Control)step);
            currentStep = step;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            currentStep.DoPrev();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentStep.DoNext();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            currentStep.DoFinish();
        }

        private void RunReport(SavedReport options)
        {
            this.Close();
            OnRunReport(options);
        }

        private void DoFinish()
        {
            this.Close();
            OnFinish();
        }

        private void WizardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnClose();
        }

    }
}
