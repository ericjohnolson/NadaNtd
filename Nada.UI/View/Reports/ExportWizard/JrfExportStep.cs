﻿using System;
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
using Nada.Model.Repositories;
using Nada.Model.Exports;

namespace Nada.UI.View.Reports
{
    public partial class JrfExportStep : BaseControl, IWizardStep
    {
        SettingsRepository settings = new SettingsRepository();
        ExportRepository repo = new ExportRepository();
        ExportJrfQuestions questions = null;
        PcJrfExporter exporter = null;
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

        public JrfExportStep()
            : base()
        {
            InitializeComponent();
        }

        public JrfExportStep(PcJrfExporter e, string t)
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
                questions = repo.GetExportQuestions();
                if (!questions.JrfYearReporting.HasValue || questions.JrfYearReporting.Value == 0)
                    questions.JrfYearReporting = DateTime.Now.Year;
                Localizer.TranslateControl(this);
                h3bLabel1.SetMaxWidth(500);
                h3bLabel2.SetMaxWidth(500);
                h3bLabel3.SetMaxWidth(500);
                h3bLabel4.SetMaxWidth(500);
                h3bLabel5.SetMaxWidth(500);
                CreateEndemicityDropdown(comboBox1);
                CreateEndemicityDropdown(comboBox2);
                CreateEndemicityDropdown(comboBox3);
                CreateEndemicityDropdown(comboBox4);
                bindingSource1.DataSource = questions;
                //ClearErrors();
                this.saveFileDialog1.DefaultExt = "xlsx";
                this.saveFileDialog1.Filter = "Excel (.xlsx)|*.xlsx";

                var allLevelTypes = settings.GetAllAdminLevels();
                var reportingType = allLevelTypes.First();
                questions.AdminLevelType = reportingType;
                bindingSource2.DataSource = allLevelTypes;
            }
        }

        private void ClearErrors()
        {
            errorProvider1.SetError(textBox5, "");
            errorProvider1.SetError(comboBox1, "");
            errorProvider1.SetError(comboBox2, "");
            errorProvider1.SetError(comboBox3, "");
            errorProvider1.SetError(comboBox4, "");
        }

        private void CreateEndemicityDropdown(ComboBox comboBox)
        {
            List<IndicatorDropdownValue> vals = new List<IndicatorDropdownValue>();
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.JrfEndemic, TranslationKey = "JrfEndemic" });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.JrfEndemicNoPc, TranslationKey = "JrfEndemicNoPc" });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.JrfEndemicNot, TranslationKey = "JrfEndemicNot" });
            comboBox.DataSource=  vals;
            comboBox.DropDownWidth = BaseForm.GetDropdownWidth(vals.Select(a => a.DisplayName));
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
            repo.UpdateExportQuestions(questions);

            saveFileDialog1.FileName = title + "-" + textBox5.Text;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.RunWorkerAsync(new ExportParams { FileName = saveFileDialog1.FileName, Questions = questions });
                OnSwitchStep(new WorkingStep(Translations.ExportingData));
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ExportParams payload = (ExportParams)e.Argument;
                e.Result = exporter.ExportData(payload.FileName, ApplicationData.Instance.GetUserId(), payload.Questions);
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error creating PC JRF:worker_DoWork. ", ex);
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
    }
}
