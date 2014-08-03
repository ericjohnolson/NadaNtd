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
using System.IO;
using Nada.Model.Demography;

namespace Nada.UI.View.Wizard
{
    public partial class SplitDate : BaseControl, IWizardStep
    {
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
        public string StepTitle { get { return Translations.RedistrictDate; } }

        public SplitDate(RedistrictingOptions o)
            : base()
        {
            options = o;
            InitializeComponent();
        }

        public void DoPrev()
        {
        }
        public void DoNext()
        {
            if (options.SplitType == SplittingType.Split)
                OnSwitchStep(new SplittingSource(options));
            else
                OnSwitchStep(new MergeYear(options));

        }
        public void DoFinish()
        {
        }

        private void SplitDate_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                bindingSource1.DataSource = options;
            }
        }

    }
}
