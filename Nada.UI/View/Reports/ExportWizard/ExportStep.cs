using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.Model.Reports;
using Nada.Globalization;
using Nada.Model;
using Nada.UI.View.Wizard;
using System.Threading;
using Nada.UI.Base;
using Nada.Model.Exports;
using Nada.Model.Repositories;

namespace Nada.UI.View.Reports
{
    public partial class ExportStep : BaseControl, IWizardStep
    {
        ExportRepository repo = new ExportRepository();
        ExportCmJrfQuestions questions = null;
        CmJrfExporter exporter = null;
        string title = "";
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return true; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return Translations.StartExport; } }

        public ExportStep()
            : base()
        {
            InitializeComponent();
        }

        public ExportStep(CmJrfExporter e, string t)
            : base()
        {
            exporter = e;
            title = t;
            InitializeComponent();
        }

        private void ExportWorkingStep_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                questions = repo.GetCmExportQuestions();
                h3bLabel1.SetMaxWidth(500);
                this.saveFileDialog1.DefaultExt = "xlsx";
                this.saveFileDialog1.Filter = "Excel (.xlsx)|*.xlsx";
                bindingSource1.DataSource = questions;
                exportContactBindingSource.DataSource = questions.Contacts;
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ExportParams payload = (ExportParams)e.Argument;
                exporter.ExportData(payload.FileName, ApplicationData.Instance.GetUserId(), payload.CmQuestions);
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error creating CM JRF:worker_DoWork. ", ex);
                throw;
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnFinish();
        }

        public void DoPrev()
        {
        }
        public void DoNext()
        {
        }
        public void DoFinish()
        {
            if (!questions.IsValid())
            {
                errorProvider1.DataSource = bindingSource1;
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }
            bindingSource1.EndEdit();
            repo.UpdateCmExportQuestions(questions);

            saveFileDialog1.FileName = title + " " + questions.YearReporting.Value;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.RunWorkerAsync(new ExportParams { FileName = saveFileDialog1.FileName, CmQuestions = questions });

                OnSwitchStep(new WorkingStep(Translations.ExportingData));
            }

        }
    }
}
