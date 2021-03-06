﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.Globalization;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.Model;
using Nada.UI.Base;
using Nada.Model.Imports;
using Nada.Model.Demography;

namespace Nada.UI.View.Wizard
{
    public partial class MergeDestination : BaseControl, IWizardStep
    {
        private IWizardStep prev = null;
        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();
        private RedistrictingOptions options = null;
        public Action OnFinish { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.Destination; } }

        public MergeDestination(RedistrictingOptions o, IWizardStep p)
            : base()
        {
            options = o;
            prev = p;
            InitializeComponent();
        }
        
        public void DoPrev()
        {
            OnSwitchStep(prev);
        }

        public void DoNext()
        {
            var model = adminUnitAdd1.GetModel();
            if (model.Id == 0)
                return;
            options.MergeDestination = model;
            if (options.SplitType == SplittingType.Merge)
                OnSwitchStep(new SplittingDemography(options, true));
            else
                OnSwitchStep(new SplittingAdminLevel(options));

        }

        public void DoFinish()
        {
        }

        private void StepCategory_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                adminUnitAdd1.LoadUnit(options.MergeSources[0].LevelNumber);
            }
        }

    }
}
