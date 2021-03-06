﻿using System;
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
    public partial class StepDdUpdateGrowthRate : BaseControl, IWizardStep
    {
        private DdUpdateViewModel vm = new DdUpdateViewModel();
        private DemoRepository repo = new DemoRepository();
        public Action OnFinish { get; set; }
        public Action OnSkip { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.ApplyCountryGrowthRate; } }

        public StepDdUpdateGrowthRate()
            : base()
        {
            InitializeComponent();
        }
        
        private void ImportOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                h3bLabel1.SetMaxWidth(300);
                h3bLabel2.SetMaxWidth(300);
                Localizer.TranslateControl(this);
                CountryDemography d = repo.GetCountryDemoRecent();
                vm.GrowthRate = d.GrowthRate;
                bsVm.DataSource = vm;
            }
        }

        public void DoPrev()
        {
        }

        public void DoNext()
        {
            if (!vm.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }
            OnSwitchStep(new WorkingStep(Translations.ApplyingGrowthRate));

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Result.ToString()))
                OnSwitchStep(new StepDdUpdateOptions(vm));
            else
                MessageBox.Show(e.Result.ToString(), Translations.UnexpectedException, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Disease distro not demo
                //SettingsRepository settings = new SettingsRepository();
                //var als = settings.GetAllAdminLevels();
                //var aggLevel = als.FirstOrDefault(a => a.IsAggregatingLevel);
                //int userId = ApplicationData.Instance.GetUserId();
                //repo.ApplyGrowthRate(vm.GrowthRate.Value, userId, aggLevel, vm.DateReported);
                //repo.AggregateUp(aggLevel, vm.DateReported, userId, vm.GrowthRate.Value);
                e.Result = "";
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error updating disease distribution for new year. StepDdUpdateGrowthRate:worker_DoWork. ", ex);
                e.Result = Translations.UnexpectedException + " " + ex.Message;
            }
        }

        public void DoFinish()
        {
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OnSkip();
        }
    }
}
