using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Csv;
using Nada.Model.Imports;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;
using Nada.UI.View.Wizard.DistrictSplitting;
using OfficeOpenXml;

namespace Nada.UI.View.Wizard
{
    public partial class SplittingDistro : BaseControl, IWizardStep
    {
        DemoRepository repo = new DemoRepository();
        private List<AdminLevel> available = new List<AdminLevel>();
        private List<AdminLevel> selected = new List<AdminLevel>();
        private SplittingOptions options;
        public Action OnFinish { get; set; }

        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return true; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return Translations.RedistributionSettings; } }

        public SplittingDistro(SplittingOptions o)
            : base()
        {
            options = o;
            InitializeComponent();
        }

        private void ImportOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                LoadLists();
            }
        }

        public void LoadLists()
        {
            options.Dashboard.adminLevel = options.Source;
            options.Dashboard.fetcher = new ActivityFetcher(options.Source);
            pnlDash.Controls.Add(options.Dashboard);
            options.Dashboard.LoadContent();
        }

        public void DoPrev()
        {
            OnSwitchStep(new SplittingAdminLevel(options));
        }

        public void DoNext()
        {
        }

        public void DoFinish()
        {
            OnSwitchStep(new WorkingStep(Translations.SplittingRedistributing));
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnFinish();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(2000);
        }



    }
}
