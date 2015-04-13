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
using Nada.UI.ViewModel;
using Nada.UI.Controls;
using Nada.Model.Survey;

namespace Nada.UI.View.Wizard
{
    public partial class ImportStepSurveyNameToSentinel : BaseControl, IWizardStep
    {
        private SurveyRepository surveys = new SurveyRepository();
        private List<DynamicContainer> controlList = new List<DynamicContainer>();
        private SettingsRepository settings = new SettingsRepository();
        private List<string> selectedDiseases = new List<string>();
        private ImportOptions options = null;
        private IWizardStep previousStep;
        private string filename;
        private Dictionary<string, SurveyUnitsAndSentinelSite> surveyNamesDictionary;
        private List<ComboBox> sitePickers = new List<ComboBox>();
        private readonly int bottomPadding = 10;
        public Action OnFinish { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return true; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return Translations.ImportAssignSentinelSites; } }
        

        public ImportStepSurveyNameToSentinel()
            : base()
        {
            InitializeComponent();
        }

        public ImportStepSurveyNameToSentinel(ImportOptions o, IWizardStep prev, string file, Dictionary<string, SurveyUnitsAndSentinelSite> surveyNamesDict)
            : base()
        {
            options = o;
            previousStep = prev;
            filename = file;
            surveyNamesDictionary = surveyNamesDict;
            InitializeComponent();
        }

        private void ImportStepSurveyNameToSentinel_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                var sitesNeeded = surveyNamesDictionary.Where(x => x.Value.NeedsSentinelSite).ToList();
                if (sitesNeeded.Count() > 0)
                    LoadSentinelSitePickers(sitesNeeded);
                else
                    DoFinish();
            }
        }
        
        public void DoPrev()
        {
            OnSwitchStep(previousStep);
        }

        public void DoNext()
        {
        }

        public void DoFinish()
        {
            foreach(var cntrl in sitePickers)
            {
                KeyValuePair<string, SurveyUnitsAndSentinelSite> survey = (KeyValuePair<string, SurveyUnitsAndSentinelSite>)cntrl.Tag;
                survey.Value.SentinelSiteName = (cntrl.SelectedItem as SentinelSite).SiteName;
            }
            DoImport();
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
            public Dictionary<string, SurveyUnitsAndSentinelSite> SurveyNamesDict { get; set; }
        }

        private void LoadSentinelSitePickers(List<KeyValuePair<string , SurveyUnitsAndSentinelSite>> sitesNeeded)
        {
            this.SuspendLayout();
            tblMetaData.Controls.Clear();
            int count = 0;
            int labelRowIndex = tblMetaData.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int controlRowIndex = tblMetaData.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int columnCount = 0;
            foreach (var survey in sitesNeeded)
            {
                var cntrl = CreateSentinelSitePicker(survey);
                
                if (count % 1 == 0)
                {
                    labelRowIndex = tblMetaData.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    controlRowIndex = tblMetaData.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    columnCount = 0;
                }

                // Add label
                tblMetaData.Controls.Add(CreateLabel(survey), columnCount, labelRowIndex);

                // Add field
                cntrl.TabIndex = count;
                tblMetaData.Controls.Add(cntrl, columnCount, controlRowIndex);

                count++;
                columnCount = columnCount + 2;
            }
            this.ResumeLayout();
            tblMetaData.Visible = true;
        }

        private Control CreateLabel(KeyValuePair<string, SurveyUnitsAndSentinelSite> survey)
        {
            var required = new H3Required
            {
                Text = survey.Key + " - " + Translations.SentinelSite,
                Name = "ciLabel_" + survey.Key,
                AutoSize = true,
                Anchor = (AnchorStyles.Bottom | AnchorStyles.Left),
                TabStop = false
            };
            required.SetMaxWidth(370);
            return required;
        }

        public Control CreateSentinelSitePicker(KeyValuePair<string, SurveyUnitsAndSentinelSite> survey)
        {
            var cntrl = new ComboBox { Name = "dynamicMulti" + survey.Key, Width = 220, Height = 100, Margin = new Padding(0, 5, 20, bottomPadding) };
            List<SentinelSite> sites = surveys.GetSitesForAdminLevel(survey.Value.Units.Select(a => a.Id.ToString()));
            foreach (var v in sites)
                cntrl.Items.Add(v);
            cntrl.ValueMember = "Id";
            cntrl.DisplayMember = "SiteName";
            cntrl.SelectedIndex = 0;
            cntrl.Tag = survey;
            cntrl.Margin = new Padding(0, 5, 20, 0);
            TableLayoutPanel tblContainer = new TableLayoutPanel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, AutoScroll = true };
            tblContainer.RowStyles.Clear();
            tblContainer.ColumnStyles.Clear();
            int cRow = tblContainer.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            tblContainer.Controls.Add(cntrl, 0, cRow);
            int lRow = tblContainer.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            var lnk = new H3Link { Text = Translations.AddNewItemLink, Margin = new Padding(0, 5, 3, bottomPadding) };
            lnk.ClickOverride += () =>
            {
                AddSentinelSite(cntrl, survey);
            };
            tblContainer.Controls.Add(lnk, 0, lRow);
            
            sitePickers.Add(cntrl);
            return tblContainer;
        }

        private void AddSentinelSite(ComboBox cntrl, KeyValuePair<string, SurveyUnitsAndSentinelSite> survey)
        {
            SentinelSiteAdd form = new SentinelSiteAdd(survey.Value.Units);
            form.OnSave += (v) =>
            {
                cntrl.Items.Add(v);
            };
            form.ShowDialog();
        }
    }
}
