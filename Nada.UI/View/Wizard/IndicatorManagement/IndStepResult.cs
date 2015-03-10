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
using OfficeOpenXml;

namespace Nada.UI.View.Wizard.IndicatorManagement
{
    public partial class IndStepResult : BaseControl, IWizardStep
    {
        IndicatorUpdateResult result = null;
        IWizardStep prev = null;
        bool showFinish = true;
        public Action OnFinish { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return showFinish; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return Translations.IndicatorUpdatesValidated; } }

        public IndStepResult() 
            : base()
        {
            InitializeComponent();
        }

        public IndStepResult(IndicatorUpdateResult r, IWizardStep p)
            : base()
        {
            result = r;
            prev = p;
            InitializeComponent();
        }

        public IndStepResult(IndicatorUpdateResult r, IWizardStep p, bool f)
            : base()
        {
            showFinish = f;
            result = r;
            prev = p;
            InitializeComponent();
        }

        private void ImportStepResult_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                tbStatus.Text = result.Message;

                if (!result.WasSuccess)
                    MessageBox.Show(Translations.ImportFailed, Translations.ErrorOccured, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public void DoPrev()
        {
            OnSwitchStep(prev);
        }

        public void DoNext()
        {
        }

        public void DoFinish()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.DoWork += worker_DoWork;
                worker.RunWorkerAsync(new WorkerPayload { FileName = saveFileDialog1.FileName, Details = result });

                OnSwitchStep(new WorkingStep(Translations.CreatingUpdateFiles));
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WorkerPayload payload = (WorkerPayload)e.Argument;
                IndicatorManager mngr = new IndicatorManager();
                mngr.CreateUpdateZip(payload.Details, payload.FileName);
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error compressed zip of update files. IndStepResult:worker_DoWork. ", ex);
                throw;
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnFinish();
        }

        private class WorkerPayload
        {
            public string FileName { get; set; }
            public IndicatorUpdateResult Details { get; set; }
        }
    }
}
