using System;
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
        private SavedReport report = new SavedReport();
        public Action<SavedReport> OnRunReport { get; set; }
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
            report.ReportOptions.ShowDiseasesOption = false;
            report.ReportOptions.EntityType = Model.IndicatorEntityType.Demo;
            report.ReportOptions.CategoryName = Translations.Demography;
            report.ReportOptions.ReportGenerator = new DemoReportGenerator();
            report.ReportOptions.AvailableIndicators = repo.GetDemographyIndicators();
            OnSwitchStep(new StepIndicators(report));
        }

        private void lnkSaes_ClickOverride()
        {
            ReportRepository repo = new ReportRepository();
            report.ReportOptions.ShowDiseasesOption = false;
            report.ReportOptions.EntityType = Model.IndicatorEntityType.Sae;
            report.ReportOptions.CategoryName = Translations.SAEs;
            report.ReportOptions.ReportGenerator = null; //new SurveyReportGenerator();
            report.ReportOptions.AvailableIndicators = null; // repo.GetSurveyIndicators();
            OnSwitchStep(new StepIndicators(report));
        }

        private void lnkDistro_ClickOverride()
        {
            ReportRepository repo = new ReportRepository();
            report.ReportOptions.ShowDiseasesOption = false;
            report.ReportOptions.EntityType = Model.IndicatorEntityType.DiseaseDistribution;
            report.ReportOptions.CategoryName = Translations.DiseaseDistribution;
            report.ReportOptions.ReportGenerator = new DistributionReportGenerator();
            report.ReportOptions.AvailableIndicators = repo.GetDiseaseDistroIndicators();
            OnSwitchStep(new StepIndicators(report));
        }

        private void lnkSurvey_ClickOverride()
        {
            ReportRepository repo = new ReportRepository();
            report.ReportOptions.ShowDiseasesOption = false;
            report.ReportOptions.EntityType = Model.IndicatorEntityType.Survey;
            report.ReportOptions.CategoryName = Translations.Surveys;
            report.ReportOptions.ReportGenerator = new SurveyReportGenerator();
            report.ReportOptions.AvailableIndicators = repo.GetSurveyIndicators();
            report.ReportOptions.HideAggregation = true;
            OnSwitchStep(new StepIndicators(report));
        }

        private void lnkIntv_ClickOverride()
        {
            ReportRepository repo = new ReportRepository();
            report.ReportOptions.ShowDiseasesOption = true;
            report.ReportOptions.EntityType = Model.IndicatorEntityType.Intervention;
            report.ReportOptions.CategoryName = Translations.Interventions;
            report.ReportOptions.ReportGenerator = new IntvReportGenerator();
            report.ReportOptions.AvailableIndicators = repo.GetIntvIndicators();
            OnSwitchStep(new StepIndicators(report));
        }

        private void lnkProcess_ClickOverride()
        {
            ReportRepository repo = new ReportRepository();
            report.ReportOptions.ShowDiseasesOption = false;
            report.ReportOptions.EntityType = Model.IndicatorEntityType.Process;
            report.ReportOptions.CategoryName = Translations.ProcessIndicators;
            report.ReportOptions.ReportGenerator = new ProcessReportGenerator();
            report.ReportOptions.AvailableIndicators = repo.GetProcessIndicators();
            OnSwitchStep(new StepIndicators(report));
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
            if (!DesignMode)
                Localizer.TranslateControl(this);
        }


    }
}
