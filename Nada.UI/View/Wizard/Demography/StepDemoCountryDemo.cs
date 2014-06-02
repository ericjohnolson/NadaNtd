using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Csv;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;
using Nada.UI.ViewModel;
using OfficeOpenXml;

namespace Nada.UI.View.Wizard
{
    public partial class StepDemoCountryDemo : BaseControl, IWizardStep
    {
        private DemoUpdateViewModel vm = new DemoUpdateViewModel();
        private DemoRepository repo = new DemoRepository();
        private CountryDemography model = null;
        public Action OnFinish { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.CountrySettings; } }
        IWizardStep prev;

        public StepDemoCountryDemo(IWizardStep p, DemoUpdateViewModel v)
        {
            vm = v;
            prev = p;
            InitializeComponent();
        }

        private void StepDemoCountryDemo_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                model = repo.GetCountryDemoRecent();
                // NEW YEAR, new demo
                model.Id = 0;
                model.GrowthRate = vm.GrowthRate;
                model.DateDemographyData = vm.DateReported;
                countryDemographyView1.LoadDemo(model);
            }
        }

        public void DoPrev()
        {
            OnSwitchStep(prev);
        }

        public void DoFinish()
        {
        }


        public void DoNext()
        {
            if (!SaveDemo())
                return;

            OnSwitchStep(new WorkingStep(Translations.ApplyingGrowthRate));

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private bool SaveDemo()
        {
            countryDemographyView1.DoValidate();
            if (!model.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return false;
            }

            int userId = ApplicationData.Instance.GetUserId();
            var demo = new DemoRepository();
            demo.Save(model, userId);
            vm.CountryDemoId = model.Id;
            return true;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Result.ToString()))
                OnSwitchStep(new StepDemoUpdateOptions(vm.DateReported, vm.CountryDemoId));
            else
                MessageBox.Show(e.Result.ToString(), Translations.UnexpectedException, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SettingsRepository settings = new SettingsRepository();
                var als = settings.GetAllAdminLevels();
                var aggLevel = als.FirstOrDefault(a => a.IsAggregatingLevel);
                int userId = ApplicationData.Instance.GetUserId();
                repo.ApplyGrowthRate(vm.GrowthRate.Value, userId, aggLevel, vm.DateReported);
                repo.AggregateUp(aggLevel, vm.DateReported, userId, vm.GrowthRate.Value, vm.CountryDemoId);
                e.Result = "";
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error updating demography for new year. StepDemoUpdateGrowthRate:worker_DoWork. ", ex);
                e.Result = Translations.UnexpectedException + " " + ex.Message;
            }
        }


    }
}
