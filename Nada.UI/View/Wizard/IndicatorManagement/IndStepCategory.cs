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
using Nada.Model;
using Nada.Model.Process;

namespace Nada.UI.View.Wizard.IndicatorManagement
{
    public partial class IndStepCategory : BaseControl, IWizardStep
    {
        private IndicatorManagerOptions opts = new IndicatorManagerOptions();
        public Action<SavedReport> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.IndFormType; } }

        public IndStepCategory()
            : base()
        {
            InitializeComponent();
        }

        private void lnkDistro_ClickOverride()
        {
            ReportRepository repo = new ReportRepository();
            opts.EntityType = Model.IndicatorEntityType.DiseaseDistribution;
            opts.AvailableIndicators = repo.GetDiseaseDistroIndicators();
            DiseaseRepository typeRepo = new DiseaseRepository();
            var types = typeRepo.GetSelectedDiseases();
            opts.FormTypes = types.Select(t => t.DisplayName).OrderBy(t => t).ToList();
            OnSwitchStep(new IndStepIndicators(opts));
        }

        private void lnkSurvey_ClickOverride()
        {
            ReportRepository repo = new ReportRepository();
            opts.EntityType = Model.IndicatorEntityType.Survey;
            opts.AvailableIndicators = repo.GetSurveyIndicators(false);
            SurveyRepository typeRepo = new SurveyRepository();
            var types = typeRepo.GetSurveyTypes();
            opts.FormTypes = types.Select(t => t.SurveyTypeName).OrderBy(t => t).ToList();
            OnSwitchStep(new IndStepIndicators(opts));
        }

        private void lnkIntv_ClickOverride()
        {
            ReportRepository repo = new ReportRepository();
            opts.EntityType = Model.IndicatorEntityType.Intervention;
            opts.AvailableIndicators = repo.GetIntvIndicators();
            IntvRepository intv = new IntvRepository();
            var types = intv.GetAllTypes();
            opts.FormTypes = types.Select(t => t.IntvTypeName).OrderBy(t => t).ToList();
            OnSwitchStep(new IndStepIndicators(opts));
        }

        private void lnkProcess_ClickOverride()
        {
            ReportRepository repo = new ReportRepository();
            opts.EntityType = Model.IndicatorEntityType.Process;
            opts.AvailableIndicators = repo.GetProcessIndicators();
            ProcessRepository typeRepo = new ProcessRepository();
            var types = typeRepo.GetProcessTypes().Select(t => t.TypeName).ToList();
            ProcessBase saes = typeRepo.Create(9);
            types.Add(saes.ProcessType.TypeName);
            opts.FormTypes = types.OrderBy(t => t).ToList();
            OnSwitchStep(new IndStepIndicators(opts));
        }

        private void h3Link1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Create new form!");
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
