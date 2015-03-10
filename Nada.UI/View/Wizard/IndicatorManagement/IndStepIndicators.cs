using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.UI.AppLogic;
using Nada.Model.Reports;
using Nada.UI.Base;
using Nada.Model;

namespace Nada.UI.View.Wizard.IndicatorManagement
{
    public partial class IndStepIndicators : BaseControl, IWizardStep
    {
        private IndicatorManagerOptions opts = null;
        public IndStepIndicators()
            : base()
        {
            InitializeComponent();
        }

        public IndStepIndicators(IndicatorManagerOptions o)
            : base()
        {
            opts = o;
            InitializeComponent();
        }

        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return true; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return Translations.SelectIndicators; } }

        public void DoPrev()
        {
            OnSwitchStep(new IndStepCategory());
        }

        public void DoNext()
        {
        }

        public void DoFinish()
        {
            opts.SelectedIndicators = new List<ReportIndicator>();
            AddSelectedIndicators(opts.SelectedIndicators, opts.AvailableIndicators);
            if (opts.SelectedIndicators.Count == 0)
            {
                MessageBox.Show(Translations.ValidationErrorAddIndicator, Translations.ValidationErrorTitle);
                return;
            }


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.DoWork += worker_DoWork;
                worker.RunWorkerAsync(new WorkerPayload { FileName = saveFileDialog1.FileName, Options = opts });

                OnSwitchStep(new WorkingStep(Translations.CreatingImportFileStatus));
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WorkerPayload payload = (WorkerPayload)e.Argument;
                IndicatorManager mngr = new IndicatorManager();
                mngr.CreateImportFile(payload.Options, payload.FileName);
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error creating import file. ImportStepListSelection:worker_DoWork. ", ex);
                throw;
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnSwitchStep(new IndStepType());
        }

        private class WorkerPayload
        {
            public string FileName { get; set; }
            public IndicatorManagerOptions Options { get; set; }
        }

        private void StepIndicators_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                LoadTree(opts.AvailableIndicators);
            }
        }

        private void AddSelectedIndicators(List<ReportIndicator> indicators, List<ReportIndicator> nodes)
        {
            foreach (var node in nodes)
            {
                if (node.IsChecked && !node.IsCategory)
                    indicators.Add(node);

                if (node.Children.Count > 0)
                    AddSelectedIndicators(indicators, node.Children);
            }
        }

        public void LoadTree(List<ReportIndicator> list)
        {
            treeListView1.ClearObjects();
            treeListView1.CanExpandGetter = model => ((ReportIndicator)model).Children.Count > 0;
            treeListView1.ChildrenGetter = delegate(object model)
            {
                return ((ReportIndicator)model).Children;
            };
            treeListView1.SetObjects(list);
            
            for(int i = list.Count - 1; i >= 0; i--)
                treeListView1.Expand(list[i]);
            
        }
        
    }
}
