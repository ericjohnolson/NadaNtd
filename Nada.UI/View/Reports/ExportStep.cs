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

namespace Nada.UI.View.Reports
{
    public partial class ExportStep : BaseControl, IWizardStep
    {
        IExporter exporter = null;
        string title = "";
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return true; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return Translations.StartExport; } }

        public ExportStep()
            : base()
        {
            InitializeComponent();
        }

        public ExportStep(IExporter e, string t)
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
                Localizer.TranslateControl(this);
                this.saveFileDialog1.DefaultExt = "xlsx";
                this.saveFileDialog1.Filter = "Excel (.xlsx)|*.xlsx";
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Payload payload = (Payload)e.Argument;
            exporter.ExportData(payload.FileName, ApplicationData.Instance.GetUserId(), payload.Year);
            Thread.Sleep(1000);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnFinish();
        }

        public void DoPrev()
        {
        }
        public void DoNext()
        {
        }
        public void DoFinish()
        {
            saveFileDialog1.FileName = title + " " + tbYear.Text;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.RunWorkerAsync(new Payload { FileName = saveFileDialog1.FileName, Year = Convert.ToInt32(tbYear.Text)});

                OnSwitchStep(new WorkingStep(Translations.ExportingData));
            }

        }
        public class Payload
        {
            public int Year { get; set; }
            public string FileName { get; set; }
        }
    }
}
