using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Csv;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;
using OfficeOpenXml;

namespace Nada.UI.View.Wizard
{
    public partial class UpdateDb : BaseControl, IWizardStep
    {
        ImportResult result = null;
        IWizardStep prev = null;
        List<string> filesToRun = new List<string>();
        public Action OnFinish { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.DatabaseUpdate; } }

        public UpdateDb() 
            : base()
        {
            InitializeComponent();
        }

        public UpdateDb(List<string> f)
            : base()
        {
            filesToRun = f;
            InitializeComponent();
        }

        private void ImportStepResult_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                h3bLabel1.SetMaxWidth(500);
                Localizer.TranslateControl(this);
            }
        }

        public void DoPrev()
        {
        }

        public void DoNext()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerAsync(filesToRun);

            OnSwitchStep(new WorkingStep(Translations.DatabaseScriptsExecuting));
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);
            SettingsRepository repo = new SettingsRepository();
            e.Result = repo.RunSchemaChangeScripts((List<string>)e.Argument);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool success = false;
            string result = (string)e.Result;
            if (result.Length == 0)
            {
                result = Translations.DatabaseScriptSuccess;
                success = true;
            }
            OnSwitchStep(new UpdateDbResult(success, result, this));
        }

        public void DoFinish()
        {
        }
    }
}
