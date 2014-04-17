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
using Nada.Model.Repositories;
using Nada.Model.Exports;

namespace Nada.UI.View.Reports
{
    public partial class ApocExport : BaseControl, IWizardStep
    {
        ApocExporter exporter = new ApocExporter();
        string title = Translations.ExportApocReport;
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

        public ApocExport()
            : base()
        {
            InitializeComponent();
        }
        
        private void ExportWorkingStep_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                h3bLabel1.SetMaxWidth(500);
                this.saveFileDialog1.DefaultExt = "xlsx";
                this.saveFileDialog1.Filter = "Excel (.xlsx)|*.xlsx";
            }
        }

        private void ClearErrors()
        {
            errorProvider1.SetError(textBox5, "");
        }
        
        public void DoPrev()
        {
        }
        public void DoNext()
        {
        }
        public void DoFinish()
        {
            int year = 0;
            if (string.IsNullOrEmpty(textBox5.Text) || !int.TryParse(textBox5.Text, out year))
            {
                errorProvider1.SetError(textBox5, Translations.Required);
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }
            if (year > 2100 || year < 1900)
            {
                errorProvider1.SetError(textBox5, Translations.ValidYear);
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }

            bindingSource1.EndEdit();
            saveFileDialog1.FileName = title + "-" + textBox5.Text;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.RunWorkerAsync(new ExportParams { FileName = saveFileDialog1.FileName, Year = year });
                OnSwitchStep(new WorkingStep(Translations.ExportingData));
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ExportParams payload = (ExportParams)e.Argument;
            exporter.ExportData(payload.FileName, ApplicationData.Instance.GetUserId(), payload.Year);
            Thread.Sleep(1000);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnFinish();
        }
    }
}
