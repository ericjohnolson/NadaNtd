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
            AddSelectedIndicators(opts.SelectedIndicators, triStateTreeView1.Nodes);
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
                LoadTree(triStateTreeView1.Nodes, opts.AvailableIndicators);
            }
        }

        private void AddSelectedIndicators(List<ReportIndicator> indicators, TreeNodeCollection parentNodes)
        {
            foreach (TreeNode node in parentNodes)
            {
                if (node.StateImageIndex == (int)RikTheVeggie.TriStateTreeView.CheckedState.Checked && node.Tag != null)
                    indicators.Add((ReportIndicator)node.Tag);

                if (node.Nodes.Count > 0)
                    AddSelectedIndicators(indicators, node.Nodes);
            }
        }

        private void LoadTree(TreeNodeCollection parentNodes, List<ReportIndicator> list)
        {
            foreach (var ind in list)
            {
                TreeNode tn = new TreeNode(ind.Name);
                if (!ind.IsCategory)
                    tn.Tag = ind;
                if (ind.Children.Count > 0)
                    LoadTree(tn.Nodes, ind.Children);

                tn.StateImageIndex = (int)RikTheVeggie.TriStateTreeView.CheckedState.UnChecked;
                parentNodes.Add(tn);
            }
        }
        
    }
}
