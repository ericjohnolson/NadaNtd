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
        private List<int> years = new List<int>();
        private SavedReport report = null;
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
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
            OnSwitchStep(new StepIndicators(report));
        }

        public void DoNext()
        {
            if (!report.ReportOptions.IsNoAggregation && rbAggListAll.Checked)
                report.ReportOptions.IsAllLocations = true;
            report.ReportOptions.IsNoAggregation = rbAggListAll.Checked;
            report.ReportOptions.IsCountryAggregation = rbAggCountry.Checked;
            report.ReportOptions.IsByLevelAggregation = rbAggLevel.Checked;
            report.ReportOptions.StartDate = dtStart.Value;
            report.ReportOptions.EndDate = dtEnd.Value;
            report.ReportOptions.MonthYearStarts = Convert.ToInt32(cbMonths.SelectedValue);
            OnSwitchStep(new StepLocations(report));
        }

        public void DoFinish()
        {
        }

        public StepOptions()
            : base()
        {
            InitializeComponent();
        }

        public StepOptions(SavedReport o)
            : base()
        {
            report = o;
            InitializeComponent();
        }

        private void StepOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);

                if (report.ReportOptions.StartDate != DateTime.MinValue)
                    dtStart.Value = report.ReportOptions.StartDate;
                if (report.ReportOptions.EndDate != DateTime.MinValue)
                    dtEnd.Value = report.ReportOptions.EndDate;

                var months = GlobalizationUtil.GetAllMonths();
                monthItemBindingSource.DataSource = months;
                cbMonths.DropDownWidth = BaseForm.GetDropdownWidth(months.Select(m => m.Name));
                if (report.ReportOptions.MonthYearStarts > 0)
                    cbMonths.SelectedValue = report.ReportOptions.MonthYearStarts;
                else
                    cbMonths.SelectedValue = 1;

                rbAggListAll.Checked = report.ReportOptions.IsNoAggregation;
                rbAggCountry.Checked = report.ReportOptions.IsCountryAggregation;
                rbAggLevel.Checked = report.ReportOptions.IsByLevelAggregation;

                if (report.ReportOptions.HideAggregation)
                {
                    rbAggCountry.Visible = false;
                    rbAggLevel.Visible = false;
                    rbAggListAll.Checked = true;
                }
            }
        }


    }
}
