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
    public partial class GenericExportStep : BaseControl, IWizardStep
    {
        ExportRepository repo = new ExportRepository();
        ExportType exportType;
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

        public GenericExportStep()
            : base()
        {
            InitializeComponent();
        }

        public GenericExportStep(ExportType t)
            : base()
        {
            InitializeComponent();
            exportType = t;
        }

        private void ExportWorkingStep_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                this.saveFileDialog1.DefaultExt = "xls";
                this.saveFileDialog1.Filter = "Excel (.xls)|*.xls";
                indicatorControl1.LoadIndicators(exportType.Indicators, exportType.IndicatorValues, exportType.IndicatorDropdownValues, IndicatorEntityType.Export);
            }
        }

        public void DoPrev()
        {
        }
        public void DoNext()
        {
        }
        public void DoFinish()
        {
            if (!indicatorControl1.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }

            exportType.IndicatorValues = indicatorControl1.GetValues();
            repo.Save(exportType, ApplicationData.Instance.GetUserId());

            saveFileDialog1.FileName = exportType.Exporter.ExportName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.RunWorkerAsync(new ExportParams { FileName = saveFileDialog1.FileName });
                OnSwitchStep(new WorkingStep(Translations.ExportingData));
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ExportParams payload = (ExportParams)e.Argument;
                e.Result = exportType.Exporter.DoExport(payload.FileName, ApplicationData.Instance.GetUserId(), exportType);
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error creating " + exportType.Exporter.ExportName + " during worker_DoWork. ", ex);
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
