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
using Nada.Model.Imports;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;
using OfficeOpenXml;

namespace Nada.UI.View.Wizard
{
    public partial class ImportStepSurveyNameUnits : BaseControl, IWizardStep
    {
        private List<AdminLevel> available = new List<AdminLevel>();
        private List<AdminLevel> selected = new List<AdminLevel>();
        private ImportOptions options;
        private IWizardStep previousStep;
        private string filename;
        private string stepTitle;
        private int index;
        private Dictionary<string, List<AdminLevel>> surveyNamesDictionary;
        public Action OnFinish { get; set; }

        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return stepTitle; } }

        public ImportStepSurveyNameUnits(ImportOptions o, IWizardStep prev, string file, Dictionary<string, List<AdminLevel>> surveyNamesDict, int i)
            : base()
        {
            stepTitle = Translations.ChooseAdminLevels + " - " + surveyNamesDict.Keys.ElementAt(i);
            index = i;
            surveyNamesDictionary = surveyNamesDict;
            filename = file;
            previousStep = prev;
            options = o;
            InitializeComponent();
        }

        private void ImportStepSurveyNameUnits_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
            }
        }

        public void DoPrev()
        {
            OnSwitchStep(previousStep);
        }

        public void DoNext()
        {
            var selected = adminLevelMultiselect1.GetSelectedAdminLevels();
            if (selected.Count == 0)
            {
                MessageBox.Show(Translations.LocationRequired, Translations.ValidationErrorTitle);
                return;
            }
            string key = surveyNamesDictionary.Keys.ElementAt(index);
            surveyNamesDictionary[key] = selected;
            if (surveyNamesDictionary.Keys.Count == (index + 1))
                DoImport();
            else
                OnSwitchStep(new ImportStepSurveyNameUnits(options, this, filename, surveyNamesDictionary, index + 1));

        }

        private void DoImport()
        {
            int userId = ApplicationData.Instance.GetUserId();
            BackgroundWorker worker = new BackgroundWorker();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerAsync(new WorkerPayload { FileName = filename, UserId = userId, SurveyNamesDict = surveyNamesDictionary });
            OnSwitchStep(new WorkingStep(Translations.ImportingFile));
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WorkerPayload payload = (WorkerPayload)e.Argument;
                ImportResult result = options.Importer.ImportWithMulitpleAdminUnits(payload.FileName, payload.UserId, payload.SurveyNamesDict);
                e.Result = result;
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error uploading import file. ImportStepSurveyNameUnits:worker_DoWork. ", ex);
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
            public Dictionary<string, List<AdminLevel>> SurveyNamesDict { get; set; }
        }

        public void DoFinish()
        {

        }
    }
}
