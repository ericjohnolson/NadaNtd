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

namespace Nada.UI.View.Reports.CustomReport
{
    public partial class StepIndicators : BaseControl, IWizardStep
    {
        private Dictionary<string, ReportIndicator> SelectedDict = new Dictionary<string, ReportIndicator>();
        private Dictionary<string, ReportIndicator> AvailableDict = new Dictionary<string, ReportIndicator>();
        private SavedReport report = null;
        public StepIndicators()
            : base()
        {
            InitializeComponent();
        }

        public StepIndicators(SavedReport o)
            : base()
        {
            report = o;
            InitializeComponent();
        }

        private void StepIndicators_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                foreach (var selected in report.ReportOptions.SelectedIndicators)
                    if (!SelectedDict.ContainsKey(selected.UniqueId))
                        SelectedDict.Add(selected.UniqueId, selected);
                LoadTree(triStateTreeView1.Nodes, report.ReportOptions.AvailableIndicators);
                LoadSelectedIndicators(triStateTreeView1.Nodes);

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

        private void LoadSelectedIndicators(TreeNodeCollection parentNodes)
        {
            foreach (TreeNode node in parentNodes)
            {
                if (node.Nodes.Count > 0)
                    LoadSelectedIndicators(node.Nodes);
                
                if(node.Tag != null && SelectedDict.ContainsKey((node.Tag as ReportIndicator).UniqueId))
                {
                    node.StateImageIndex = (int)RikTheVeggie.TriStateTreeView.CheckedState.Checked;
                    triStateTreeView1.UpdateParentState(node.Parent);
                }
            }
        }

        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.SelectIndicators; } }
        
        public void DoPrev()
        {
            OnSwitchStep(new StepCategory());
        }

        public void DoNext()
        {
            report.ReportOptions.SelectedIndicators = new List<ReportIndicator>();
            AddSelectedIndicators(report.ReportOptions.SelectedIndicators, triStateTreeView1.Nodes);
            if (report.ReportOptions.SelectedIndicators.Count == 0)
            {
                MessageBox.Show(Translations.ValidationErrorAddIndicator, Translations.ValidationErrorTitle);
                return;
            }

            OnSwitchStep(new StepOptions(report));
        }

        public void DoFinish()
        {
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

       
        
    }
}
