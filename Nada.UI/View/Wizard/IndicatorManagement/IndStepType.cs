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

namespace Nada.UI.View.Wizard.IndicatorManagement
{
    public partial class IndStepType : BaseControl, IWizardStep
    {
        public Action<SavedReport> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.PleaseSelect; } }

        public IndStepType()
            : base()
        {
            InitializeComponent();
        }

        private void lnkDownload_ClickOverride()
        {
            OnSwitchStep(new IndStepCategory());
        }
        
        private void h3Link1_ClickOverride()
        {
            MessageBox.Show("Create new form!");
        }

        private void lnkUpload_ClickOverride()
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                    worker.DoWork += worker_DoWork;

                    worker.RunWorkerAsync(new WorkerPayload { FileName = openFileDialog1.FileName });

                    OnSwitchStep(new WorkingStep(Translations.ImportingFile));
                }
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error uploading file in IndStepType: ", ex);
                throw;
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                IndicatorManager mngr = new IndicatorManager();
                WorkerPayload payload = (WorkerPayload)e.Argument;
                IndicatorUpdateResult result = mngr.ImportData(payload.FileName); 
                e.Result = result;
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error uploading indicator file. IndStepType:worker_DoWork. ", ex);
                e.Result = new IndicatorUpdateResult(Translations.UnexpectedException + " " + ex.Message);
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnSwitchStep(new IndStepResult((IndicatorUpdateResult)e.Result, this));
        }

        private class WorkerPayload
        {
            public string FileName { get; set; }
            public int UserId { get; set; }
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
