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
using Nada.Model;
using Nada.UI.Base;
using Nada.Model.Imports;
using Nada.Model.Demography;

namespace Nada.UI.View.Wizard
{
    public partial class MergeDestination : BaseControl, IWizardStep
    {
        private IWizardStep prev = null;
        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();
        private RedistrictingOptions options = null;
        public Action OnFinish { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.Destination; } }

        public MergeDestination(RedistrictingOptions o, IWizardStep p)
            : base()
        {
            options = o;
            prev = p;
            InitializeComponent();
        }
        
        public void DoPrev()
        {
            OnSwitchStep(prev);
        }

        public void DoNext()
        {
            if (!adminUnitAdd1.IsValid())
                return;
            options.MergeDestination = adminUnitAdd1.GetModel();
            if (options.SplitType == SplittingType.Merge)
                ExecuteRedistricting();
            else
                OnSwitchStep(new SplittingAdminLevel(options));

        }

        public void ExecuteRedistricting()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            OnSwitchStep(new WorkingStep(Translations.Running));
            worker.RunWorkerAsync(options);
        }

        public void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RedistrictingResult result = (RedistrictingResult)e.Result;
            if (result.HasError)
                OnSwitchStep(new MessageBoxStep(Translations.ErrorOccured, result.ErrorMessage, true, this));
            else
                OnSwitchStep(new SplitReviewConfirm(options, Translations.SplitMergeConfirmReview));
        }

        public void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                RedistrictingExpert expert = new RedistrictingExpert();
                e.Result = expert.Run((RedistrictingOptions)e.Argument);
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Exception occured when performing merge (MergeDestination:worker_DoWork).", ex);
                e.Result = new RedistrictingResult { HasError = true, ErrorMessage = Translations.UnexpectedException + " " + ex.Message };
            }
        }

        public void DoFinish()
        {
        }

        private void StepCategory_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                adminUnitAdd1.LoadLevel(options.MergeSources[0].AdminLevelTypeId);
            }
        }

    }
}
