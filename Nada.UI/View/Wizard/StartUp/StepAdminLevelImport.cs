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

namespace Nada.UI.View.Wizard
{
    public partial class StepAdminLevelImport : BaseControl, IWizardStep
    {
        string stepTitle = "";
        bool isDemoOnly = false;
        AdminLevelDemoImporter importer = null;
        AdminLevelDemoUpdater updater = null;
        DemoRepository repo = new DemoRepository();
        AdminLevelType locationType = null;
        AdminLevelType nextType = null;
        IWizardStep prev = null;
        private DateTime demoDate;
        public Action OnFinish { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
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

        public StepAdminLevelImport(AdminLevelType type, IWizardStep p)
            : base()
        {
            Init(type, p, false);
        }

        public StepAdminLevelImport(AdminLevelType type, IWizardStep p, bool demoOnly, DateTime d)
            : base()
        {
            demoDate = d;
            Init(type, p, demoOnly);
        }

        private void Init(AdminLevelType type, IWizardStep step, bool demoOnly)
        {
            isDemoOnly = demoOnly;
            locationType = type;
            prev = step;
            SettingsRepository settings = new SettingsRepository();
            nextType = settings.GetNextLevel(locationType.LevelNumber);
            importer = new AdminLevelDemoImporter(locationType);
            updater = new AdminLevelDemoUpdater(locationType);
            stepTitle = isDemoOnly ? Translations.UpdateDemography + " - " + locationType.DisplayName : Translations.ImportAdminLevels + locationType.DisplayName;
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

                    if (locationType.IsDemographyAllowed)
                    {
                        lblYear.Visible = true;
                        dateTimePicker1.Visible = true;
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
            OnSwitchStep(new StepAdminLevelImport(nextType, this, isDemoOnly, demoDate));
        }

        public void DoFinish()
        {
            OnFinish();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            AdminLevel filteredBy = null;
            int rows = 0;
            DateTime dateReported = DateTime.Now;
            if (!IsValid(ref filteredBy, out rows, out dateReported))
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
                    IsOnlyDemo = isDemoOnly
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
                if (!payload.IsOnlyDemo)
                    importer.CreateImportFile(payload.Filename, payload.DoDemography, payload.RowCount, payload.FilteredBy);
                else
                    updater.CreateUpdateFile(payload.Filename);
                e.Result = new ImportResult { WasSuccess = true };
            }
            catch (Exception ex)
            {
                e.Result = new ImportResult(Translations.UnexpectedException + " " + ex.Message);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            AdminLevel filteredBy = null;
            int rows = 0;
            DateTime dateReported = DateTime.Now;
            if (!IsValid(ref filteredBy, out rows, out dateReported))
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

            if (!result.WasSuccess) //MessageBox.Show(Translations.ImportComplete, Translations.ImportComplete);
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
                e.Result = new ImportResult(Translations.UnexpectedException + " " + ex.Message);
            }
        }

        private void ShowLoading(bool p)
        {
            btnDownload.Visible = !p;
            btnImport.Visible = !p;
            loadingImport.Visible = p;
        }

        private bool IsValid(ref AdminLevel filteredBy, out int count, out DateTime dateReported)
        {
            dateReported = DateTime.Now;
            count = 0;
            if (isDemoOnly)
            {
                dateReported = demoDate;
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
            public int RowCount { get; set; }
            public DateTime DateReported { get; set; }
            public AdminLevel FilteredBy { get; set; }
        }
    }
}
