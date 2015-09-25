using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Exports;
using Nada.UI.Base;
using Nada.UI.AppLogic;
using Nada.Model.Reports;
using Nada.Globalization;
using Nada.UI.View.Wizard;
using Nada.Model;

namespace Nada.UI.View.Reports.ExportWizard
{
    public partial class LeishReportStep : BaseControl, IWizardStep
    {
        //SettingsRepository settings = new SettingsRepository();
        //ExportRepository repo = new ExportRepository();
        LeishReportQuestions Questions = null;
        LeishReportExporter Exporter = null;
        string Title = "";
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

        public LeishReportStep()
            : base()
        {
            InitializeComponent();
        }

        public LeishReportStep(LeishReportExporter e, string t)
            : base()
        {
            Exporter = e;
            Title = t;
            InitializeComponent();
        }

        public void DoPrev()
        {
            throw new NotImplementedException();
        }
        public void DoNext()
        {
            throw new NotImplementedException();
        }
        public void DoFinish()
        {
            // Validate
            if (!Questions.IsValid())
            {
                errorProvider1.DataSource = questionDataSource;
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }
            questionDataSource.EndEdit();

            saveFileDialog1.FileName = Title + " " + Questions.LeishRepYearReporting.Value;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.RunWorkerAsync(new ExportParams { FileName = saveFileDialog1.FileName, LeishRepQuestions = Questions });

                OnSwitchStep(new WorkingStep(Translations.ExportingData));
            }
        }

        private void LeishReportStep_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                // Translate
                Localizer.TranslateControl(this);
                // Setup the options
                SetupOptions();
                // Set the datasource
                questionDataSource.DataSource = Questions;
                // Set the dropdown options for the anti leish med combobox
                CreateAntiLeishMedDropdown(antiLeishMedCb);
                // Set the save dialog extension
                this.saveFileDialog1.DefaultExt = "xlsx";
                this.saveFileDialog1.Filter = "Excel (.xlsx)|*.xlsx";
            }
        }

        private void SetupOptions()
        {
            Questions = new LeishReportQuestions();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ExportParams payload = (ExportParams)e.Argument;
                e.Result = Exporter.DoExport(payload.FileName, ApplicationData.Instance.GetUserId(), payload.LeishRepQuestions);
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error creating Leish Report (worker_DoWork). ", ex);
                e.Result = new ExportResult(Translations.UnexpectedException + " " + ex.Message);
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ExportResult result = (ExportResult)e.Result;
            if (!result.WasSuccess)
                MessageBox.Show(result.ErrorMessage, Translations.ErrorOccured, MessageBoxButtons.OK, MessageBoxIcon.Error);
            OnFinish();
        }

        private void CreateAntiLeishMedDropdown(ComboBox comboBox)
        {
            List<IndicatorDropdownValue> vals = new List<IndicatorDropdownValue>();
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.AmpBDeox, TranslationKey = "AmpBDeox" });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.LipoAmpB, TranslationKey = "LipoAmpB" });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.MegluAnti, TranslationKey = "MegluAnti" });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.Miltefosine, TranslationKey = "Miltefosine" });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.Paromomycin, TranslationKey = "Paromomycin" });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.Pentamidine, TranslationKey = "Pentamidine" });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.SodiumStib, TranslationKey = "SodiumStib" });
            comboBox.DataSource = vals;
            comboBox.DropDownWidth = BaseForm.GetDropdownWidth(vals.Select(a => a.DisplayName));
        }

    }
}
