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

namespace Nada.UI.View.Wizard
{
    public partial class ImportStepType : BaseControl, IWizardStep
    {
        private ImportOptions options = null;
        public Action OnFinish { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.ImportAction; } }

        public ImportStepType()
            : base()
        {
            InitializeComponent();
        }

        public ImportStepType(ImportOptions o)
            : base()
        {
            options = o;
            InitializeComponent();
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
            {
                Localizer.TranslateControl(this);
                openFileDialog1.Filter = "Excel files|*.xlsx;*.xls";
                var types = options.Importer.GetAllTypes();
                typeListItemBindingSource.DataSource = types.OrderBy(t => t.Name);
                bsImportOptions.DataSource = options;
                
                if (types.Count > 0)
                    cbTypes.DropDownWidth = BaseForm.GetDropdownWidth(types.Select(a => a.Name));
            }
        }

        private void lnkDownload_ClickOverride()
        {
            if (!IsValid())
                return;
            if(options.Importer.HasGroupedAdminLevels(options))
                OnSwitchStep(new ImportStepOptions(options, OnFinish));
            else
                OnSwitchStep(new ImportStepOptions(options, OnFinish));
        }


        private void lnkUpload_ClickOverride()
        {
            if (!IsValid())
                return;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                int userId = ApplicationData.Instance.GetUserId();
                BackgroundWorker worker = new BackgroundWorker();
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.DoWork += worker_DoWork;

                worker.RunWorkerAsync(new WorkerPayload { FileName = openFileDialog1.FileName, UserId = userId });

                OnSwitchStep(new WorkingStep(Translations.ImportingFile));
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WorkerPayload payload = (WorkerPayload)e.Argument;
                ImportResult result = options.Importer.ImportData(payload.FileName, payload.UserId);
                e.Result = result;
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error uploading import file. ImportStepType:worker_DoWork. ", ex);
                e.Result = new ImportResult(Translations.UnexpectedException + " " + ex.Message);
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {   
            OnSwitchStep(new ImportStepResult((ImportResult)e.Result, this));
        }

        private class WorkerPayload
        {
            public string FileName { get; set; }
            public int UserId { get; set; }
        }
           

        private bool IsValid()
        {
            if (!options.IsValid())
            {
                errorProvider1.DataSource = bsImportOptions;
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return false;
            }
            options.Importer.SetType(options.TypeId.Value);
            return true;
        }

    }
}
