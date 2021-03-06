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
using OfficeOpenXml;

namespace Nada.UI.View.Wizard
{
    public partial class StepAdminLevelImport : BaseControl, IWizardStep
    {
        string stepTitle = "";
        bool isDemoOnly = false;
        bool isSingleImport = false;
        int? countryDemoId = null;
        AdminLevelDemoImporter importer = null;
        AdminLevelDemoUpdater updater = null;
        DemoRepository repo = new DemoRepository();
        AdminLevelType locationType = null;
        AdminLevelType nextType = null;
        IWizardStep prev = null;
        private DateTime demoDate;
        public Action OnFinish { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public bool ShowNext { get { return nextType != null; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return prev != null; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return nextType == null; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return stepTitle; } }

        public StepAdminLevelImport() 
            : base()
        {
            InitializeComponent();
        }

        public StepAdminLevelImport(AdminLevelType type, IWizardStep p, int? cid)
            : base()
        {
            Init(type, p, false, cid);
        }

        public StepAdminLevelImport(AdminLevelType type, IWizardStep p, bool demoOnly, DateTime d, int? cid)
            : base()
        {
            demoDate = d;
            Init(type, p, demoOnly, cid);
        }

        public StepAdminLevelImport(AdminLevelType type, IWizardStep p, DateTime d, bool isImport)
            : base()
        {
            isSingleImport = isImport;
            demoDate = d;
            Init(type, p, true, null);
        }

        private void Init(AdminLevelType type, IWizardStep step, bool demoOnly, int? cid)
        {
            countryDemoId = cid;
            isDemoOnly = demoOnly;
            locationType = type;
            prev = step;
            SettingsRepository settings = new SettingsRepository();
            DemoRepository demo = new DemoRepository();
            if (!isDemoOnly)
                demoDate = demo.GetCountryDemoRecent().DateDemographyData;
            if (!isSingleImport)
            {
                nextType = settings.GetNextLevel(locationType.LevelNumber);
                stepTitle = isDemoOnly ? Translations.UpdateDemography + " - " + locationType.DisplayName : Translations.ImportAdminLevels + locationType.DisplayName;
                importer = new AdminLevelDemoImporter(locationType, countryDemoId);
            }
            else
                stepTitle = Translations.Demography + " - " + locationType.DisplayName;
            updater = new AdminLevelDemoUpdater(locationType, countryDemoId);
            InitializeComponent();
        }

        private void ImportOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                lblRows.SetMaxWidth(370);
                tbImportFor.SetMaxWidth(370);
                lblYear.SetMaxWidth(370);
                saveFileDialog1.FileName = stepTitle;
            
                tbRows.Visible = !isDemoOnly;
                lblRows.Visible = !isDemoOnly;
                
                if (!isDemoOnly)
                {
                    if (locationType.LevelNumber > 2)
                    {
                        tbImportFor.Visible = true;
                        cbImportFor.Visible = true;
                        var levels = repo.GetAdminLevelByLevel(locationType.LevelNumber - 2);
                        cbImportFor.DataSource = levels;
                        if(levels.Count > 0)
                            cbImportFor.DropDownWidth = BaseForm.GetDropdownWidth(levels.Select(a => a.Name));
                    }
                }

                if (locationType.IsAggregatingLevel)
                    lblIsAggLevel.Visible = true;

            }
        }

        public void DoPrev()
        {
            OnSwitchStep(prev);
        }

        public void DoNext()
        {
            if (ConfirmNext())
                OnSwitchStep(new StepAdminLevelImport(nextType, this, isDemoOnly, demoDate, countryDemoId));
            
        }

        public void DoFinish()
        {
            if (ConfirmNext())
                OnFinish();
        }

        private bool ConfirmNext()
        {
            if (locationType.LevelNumber > 2)
            {
                var result = MessageBox.Show(this, Translations.StartUpFinishedAllUnits, Translations.Confirm, MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return false;
                }
            }

            return true;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            AdminLevel filteredBy = null;
            int rows = 0;
            DateTime dateReported = demoDate;
            if (!IsValid(ref filteredBy, out rows))
                return;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var worker = new BackgroundWorker();
                worker.DoWork += dl_DoWork;
                worker.RunWorkerCompleted += dl_RunWorkerCompleted;
                ShowLoading(true);
                worker.RunWorkerAsync(new Payload
                {
                    DoDemography = locationType.IsDemographyAllowed,
                    Filename = saveFileDialog1.FileName,
                    FilteredBy = filteredBy,
                    RowCount = rows,
                    DateReported = dateReported,
                    IsOnlyDemo = isDemoOnly,
                    IncludeData = !isSingleImport
                });
            }
        }

        void dl_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ShowLoading(false);
            ImportResult result = (ImportResult)e.Result;
            tbStatus.Text = result.Message;

            if (!result.WasSuccess)
                MessageBox.Show(Translations.ImportFailed, Translations.ErrorOccured, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void dl_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Payload payload = (Payload)e.Argument;
                if (payload.IsOnlyDemo)
                    updater.CreateUpdateFile(payload.Filename, payload.IncludeData);
                else
                    importer.CreateImportFile(payload.Filename, payload.DoDemography, payload.RowCount, payload.FilteredBy);
                e.Result = new ImportResult { WasSuccess = true };
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error downloading admin level import. StepAdminLevelImport:worker_DoWork. ", ex);
                e.Result = new ImportResult(Translations.UnexpectedException + " " + ex.Message);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            AdminLevel filteredBy = null;
            int rows = 0;
            DateTime dateReported = demoDate;
            if (!IsValid(ref filteredBy, out rows))
                return;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var worker = new BackgroundWorker();
                worker.DoWork += up_DoWork;
                worker.RunWorkerCompleted += up_RunWorkerCompleted;
                ShowLoading(true);
                worker.RunWorkerAsync(new Payload
                {
                    DoAggregate = locationType.IsAggregatingLevel,
                    DoDemography = locationType.IsDemographyAllowed,
                    Filename = openFileDialog1.FileName,
                    FilteredBy = filteredBy,
                    RowCount = rows,
                    DateReported = dateReported,
                    IsOnlyDemo = isDemoOnly
                });
            }
        }

        private void up_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ShowLoading(false);
            ImportResult result = (ImportResult)e.Result;
            tbStatus.Text = result.Message;

            if (!result.WasSuccess) 
                MessageBox.Show(Translations.ImportFailed, Translations.ErrorOccured, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void up_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Payload payload = (Payload)e.Argument;
                ImportResult result = null;

                if (!payload.IsOnlyDemo)
                    result = importer.ImportData(payload.Filename, ApplicationData.Instance.GetUserId(),
                        payload.DoDemography, payload.DoAggregate, payload.RowCount, payload.FilteredBy, payload.DateReported);
                else
                    result = updater.ImportData(payload.Filename, ApplicationData.Instance.GetUserId(), payload.DateReported, payload.DoAggregate);

                e.Result = result;
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error uploading admin level import. StepAdminLevelImport:worker_DoWork. ", ex);
                e.Result = new ImportResult(Translations.UnexpectedException + " " + ex.Message);
            }
        }

        private void ShowLoading(bool p)
        {
            btnDownload.Visible = !p;
            btnImport.Visible = !p;
            loadingImport.Visible = p;
        }

        private bool IsValid(ref AdminLevel filteredBy, out int count)
        {
            count = 0;
            if (isDemoOnly)
            {
                return true;
            }

            if ((cbImportFor.Visible && cbImportFor.SelectedItem == null) ||
                (string.IsNullOrEmpty(tbRows.Text) || !int.TryParse(tbRows.Text, out count)))
            {
                MessageBox.Show(Translations.PleaseEnterRequiredFields);
                count = 0;
                return false;
            }
            else if (cbImportFor.Visible && cbImportFor.SelectedItem != null)
                filteredBy = (AdminLevel)cbImportFor.SelectedItem;

            return true;
        }

        public class Payload
        {
            public string Filename { get; set; }
            public bool DoDemography { get; set; }
            public bool DoAggregate { get; set; }
            public bool IsOnlyDemo { get; set; }
            public bool IncludeData { get; set; }
            public int RowCount { get; set; }
            public DateTime DateReported { get; set; }
            public AdminLevel FilteredBy { get; set; }
        }
    }
}
