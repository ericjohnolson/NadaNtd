﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Reports;
using Nada.UI.AppLogic;

namespace Nada.UI.View.Reports.CustomReport
{
    public partial class ReportWizard : Form
    {
        public Action<ReportOptions> OnRunReport { get; set; }
        private IWizardStep currentStep = null;

        public ReportWizard()
        {
            InitializeComponent();
        }

        public ReportWizard(IWizardStep step)
        {
            currentStep = step;
            InitializeComponent();
        }

        private void ReportWizard_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                LoadStep(currentStep);
            }
        }

        private void LoadStep(IWizardStep step)
        {
            step.OnSwitchStep = (s) => { LoadStep(s); };
            step.OnRunReport = RunReport;
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

        private void RunReport(ReportOptions options)
        {
            this.Close();
            OnRunReport(options);
        }

    }
}
