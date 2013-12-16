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
using Nada.UI.Base;

namespace Nada.UI.View.Reports.CustomReport
{
    public partial class StepOptions : BaseControl, IWizardStep
    {
        private ReportOptions options = null;
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.ReportOptions; } }

        public void DoPrev()
        {
            OnSwitchStep(new StepIndicators(options));
        }

        public void DoNext()
        {
            options.IsNoAggregation = rbAggListAll.Checked;
            options.IsCountryAggregation = rbAggCountry.Checked;
            options.IsByLevelAggregation = rbAggLevel.Checked;
            options.SelectedYears = GetSelectedYears();
            OnSwitchStep(new StepLocations(options));
        }

        public void DoFinish()
        {
        }

        public StepOptions()
            : base()
        {
            InitializeComponent();
        }

        public StepOptions(ReportOptions o)
            : base()
        {
            options = o;
            InitializeComponent();
        }

        private void StepOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                var years = new List<int>();
                for (int i = DateTime.Now.Year + 2; i >= 1990; i--)
                    years.Add(i);
                lbYears.DataSource = years;
                rbAggListAll.Checked = options.IsNoAggregation;
                rbAggCountry.Checked = options.IsCountryAggregation;
                rbAggLevel.Checked = options.IsByLevelAggregation;
            }
        }

        private List<int> GetSelectedYears()
        {
            List<int> years = new List<int>();
            foreach (var year in lbYears.SelectedItems)
                years.Add(Convert.ToInt32(year));
            return years;
        }

    }
}
