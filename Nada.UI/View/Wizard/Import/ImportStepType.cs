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

                // If this importer allows validation, show the importer link
                if (options.Importer is IntvImporter || options.Importer is SurveyImporter || options.Importer is DistroImporter || options.Importer is ProcessImporter)
                    lnkValidate.Visible = true;
                else
                    lnkValidate.Visible = false;

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
                OnSwitchStep(new ImportStepMultiUnits(options, OnFinish));
            else
                OnSwitchStep(new ImportStepOptions(options, OnFinish));
        }

        private void lnkUpload_ClickOverride()
        {
            try
            {
                if (!IsValid())
                    return;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    DataSet ds = ImporterBase.LoadDataFromFile(openFileDialog1.FileName);
                    if (options.Importer is SurveyImporter && ds.Tables[0].Columns.Contains("* " + Translations.SurveyName))
                    {
                        Dictionary<string, SurveyUnitsAndSentinelSite> surveyNameDict = new Dictionary<string, SurveyUnitsAndSentinelSite>();
                        foreach (DataRow row in ds.Tables[0].Rows)
                            if (row["* " + Translations.SurveyName] != null && row["* " + Translations.SurveyName].ToString().Length > 0)
                            {
                                var vm = new SurveyUnitsAndSentinelSite();
                                if (row[Translations.IndSpotCheckName] == null || row[Translations.IndSpotCheckName].ToString().Length == 0)
                                    vm.NeedsSentinelSite = true;
                                surveyNameDict.Add(row["* " + Translations.SurveyName].ToString(), vm);
                            }

                        if (surveyNameDict.Keys.Count <= 0)
                            OnSwitchStep(new ImportStepResult(new ImportResult(Translations.ImportNoDataError), this));
                        else
                            OnSwitchStep(new ImportStepSurveyNameUnits(options, this, openFileDialog1.FileName, surveyNameDict, 0));
                    }
                    else
                    {
                        int userId = ApplicationData.Instance.GetUserId();
                        BackgroundWorker worker = new BackgroundWorker();
                        worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                        worker.DoWork += worker_DoWork;
                        worker.RunWorkerAsync(new WorkerPayload { FileName = openFileDialog1.FileName, UserId = userId });
                        OnSwitchStep(new WorkingStep(Translations.ImportingFile));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error uploading file in ImportStepType: ", ex);
                throw;
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

        private void lnkValidate_ClickOverride()
        {
            try
            {
                if (!IsValid())
                    return;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.RunWorkerCompleted += workerValidation_RunWorkerCompleted;
                    worker.DoWork += workerValidation_DoWork;
                    worker.RunWorkerAsync(new WorkerPayload { FileName = openFileDialog1.FileName, UserId = 0 });
                    OnSwitchStep(new WorkingStep(Translations.ValidatingFile));
                }
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error uploading file in ImportStepType: ", ex);
                throw;
            }
        }

        private void workerValidation_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WorkerPayload payload = (WorkerPayload)e.Argument;
                ImportResult result = options.Importer.ValidateData(payload.FileName);
                e.Result = result;
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error uploading import file. ImportStepType:workerValidation_DoWork. ", ex);
                e.Result = new ImportResult(Translations.UnexpectedException + " " + ex.Message);
            }
        }

        private void workerValidation_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnSwitchStep(new ValidationStepResult((ImportResult)e.Result, this));
        }

    }
}
