﻿using System;
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
        private ReportOptions options = null;
        public StepIndicators()
            : base()
        {
            InitializeComponent();
        }

        public StepIndicators(ReportOptions o)
            : base()
        {
            options = o;
            InitializeComponent();
        }

        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
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
            options.SelectedIndicators = new List<ReportIndicator>();
            AddSelectedIndicators(options.SelectedIndicators, options.AvailableIndicators);
            OnSwitchStep(new StepOptions(options));
        }

        public void DoFinish()
        {
        }

        private void StepIndicators_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                LoadTree(options.AvailableIndicators);
                //LoadTree(options.AvailableIndicators, this.treeView.Nodes);
            }
        }

        //private void LoadTree(List<ReportIndicator> list, TreeNodeCollection tree)
        //{
        //    foreach (ReportIndicator ind in list)
        //    {
        //        TreeNode node = new TreeNode { Text = ind.Name, Tag = ind };
        //        tree.Add(node);
        //        if (ind.Children.Count > 0)
        //            LoadTree(ind.Children, node.Nodes);
        //    }
        //}

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

            foreach(var ind in list)
                treeListView1.Expand(ind);
        }
        
    }
}
