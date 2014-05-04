using System;
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
    public partial class MergingSources : BaseControl, IWizardStep
    {
        private string title = "";
        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();
        private RedistrictingOptions options = null;
        public Action OnFinish { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return title; } }

        public MergingSources(RedistrictingOptions o, string t)
            : base()
        {
            options = o;
            title = t;
            InitializeComponent();
        }
        
        public void DoPrev()
        {
        }
        public void DoNext()
        {
            options.MergeSources = adminLevelMultiselect1.GetSelectedAdminLevels();
            if(options.MergeSources.Count == 0)
            {
                MessageBox.Show(Translations.LocationRequired, Translations.ValidationErrorTitle);
                return;
            }
            options.SplitIntoNumber = options.MergeSources.Count;
            OnSwitchStep(new MergeDestination(options, this));
        }
        public void DoFinish()
        {
        }

        private void StepCategory_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                
            }
        }

    }
}
