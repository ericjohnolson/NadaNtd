﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.Globalization;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.UI.Base;

namespace Nada.UI.View.Reports.CustomReport
{
    public partial class StepCategory : BaseControl, IWizardStep
    {
        private ReportOptions options = new ReportOptions();
        public Action<ReportOptions> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.SelectCategory; } }

        public StepCategory()
            : base()
        {
            InitializeComponent();
        }

        private void lnkDemo_ClickOverride()
        {
            ReportRepository repo = new ReportRepository();
            options.ShowDiseasesOption = false;
            options.ReportGenerator = new DemoReportGenerator();
            options.AvailableIndicators = repo.GetDemographyIndicators();
            OnSwitchStep(new StepIndicators(options));
        }

        private void lnkSaes_ClickOverride()
        {
            ReportRepository repo = new ReportRepository();
            options.ShowDiseasesOption = false;
            options.ReportGenerator = null; //new SurveyReportGenerator();
            options.AvailableIndicators = null; // repo.GetSurveyIndicators();
            OnSwitchStep(new StepIndicators(options));
        }

        private void lnkDistro_ClickOverride()
        {
            ReportRepository repo = new ReportRepository();
            options.ShowDiseasesOption = false;
            options.ReportGenerator = new DistributionReportGenerator();
            options.AvailableIndicators = repo.GetDiseaseDistroIndicators();
            OnSwitchStep(new StepIndicators(options));
        }

        private void lnkSurvey_ClickOverride()
        {
            ReportRepository repo = new ReportRepository();
            options.ShowDiseasesOption = false;
            options.ReportGenerator = new SurveyReportGenerator();
            options.AvailableIndicators = repo.GetSurveyIndicators();
            options.HideAggregation = true;
            OnSwitchStep(new StepIndicators(options));
        }

        private void lnkIntv_ClickOverride()
        {
            ReportRepository repo = new ReportRepository();
            options.ShowDiseasesOption = true;
            options.ReportGenerator = new IntvReportGenerator();
            options.AvailableIndicators = repo.GetIntvIndicators();
            OnSwitchStep(new StepIndicators(options));
        }

        private void lnkProcess_ClickOverride()
        {
            ReportRepository repo = new ReportRepository();
            options.ShowDiseasesOption = false;
            options.ReportGenerator = new ProcessReportGenerator();
            options.AvailableIndicators = repo.GetProcessIndicators();
            OnSwitchStep(new StepIndicators(options));
        }

        public void DoPrev()
        {
        }
        public void DoNext()
        {
        }
        public void DoFinish()
        {
        }

        private void StepCategory_Load(object sender, EventArgs e)
        {
            if(!DesignMode)
                Localizer.TranslateControl(this);
        }


    }
}
