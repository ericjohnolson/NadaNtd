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
    public partial class ImportStepOptions : BaseControl, IWizardStep
    {
        private List<AdminLevel> available = new List<AdminLevel>();
        private List<AdminLevel> selected = new List<AdminLevel>();
        private ImportOptions options;
        public Action OnFinish { get; set; }

        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return true; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return Translations.ChooseAdminLevels; } }

        public ImportStepOptions()
            : base()
        {
            InitializeComponent();
        }

        public ImportStepOptions(ImportOptions o, Action onFinish)
        {
            options = o;
            OnFinish = onFinish;
            InitializeComponent();
        }

        private void ImportOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                saveFileDialog1.Filter = Translations.ExcelFiles + " (*.xlsx)|*.xlsx";
                saveFileDialog1.FileName = options.Importer.ImportName;
                saveFileDialog1.DefaultExt = ".xlsx";
            }
        }

        public void DoPrev()
        {
            OnSwitchStep(new ImportStepType(options));
        }

        public void DoNext()
        {
        }

        public void DoFinish()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.DoWork += worker_DoWork;

                List<AdminLevel> adminLevels = new List<AdminLevel>();
                foreach (AdminLevel l in adminLevelMultiselect1.GetSelectedAdminLevels())
                    adminLevels.Add(l);
                worker.RunWorkerAsync(new WorkerPayload { FileName = saveFileDialog1.FileName, AdminLevels = adminLevels });

                OnSwitchStep(new WorkingStep(Translations.CreatingImportFileStatus));
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerPayload payload = (WorkerPayload)e.Argument;
            options.Importer.CreateImportFile(payload.FileName, payload.AdminLevels);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnFinish();
        }

        private class WorkerPayload
        {
            public string FileName { get; set; }
            public List<AdminLevel> AdminLevels { get; set; }
        }

    }
}
