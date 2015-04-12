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
using Nada.UI.View.Reports.CustomReport;
using Nada.Model.Repositories;
using Nada.Model;
using Nada.Model.Diseases;

namespace Nada.UI.View.Reports.Standard
{
    public partial class EliminationOptions : BaseControl, IWizardStep
    {
        private EliminationReportOptions options = null;
        private SavedReport report = null;
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.ReportOptions; } }

        public void DoPrev()
        {
        }

        public void DoNext()
        {
            report.ReportOptions.IsCountryAggregation = rbAggCountry.Checked;
            report.ReportOptions.IsByLevelAggregation = rbAggLevel.Checked;
            report.ReportOptions.SelectedIndicators = new List<ReportIndicator>();

            report.ReportOptions.ReportGenerator = new EliminationPersonsReportGenerator();
            if (cbEliminationType.SelectedItem.ToString() == Translations.Persons)
            {
                options.IsPersons = true;
                report.TypeName = Translations.ReportProgressTowardsEliminiation + " (" + Translations.Persons + ")";
                report.ReportOptions.ReportGenerator = new EliminationPersonsReportGenerator();
            }
            else
            {
                options.IsPersons = false;
                report.TypeName = Translations.ReportProgressTowardsEliminiation + " (" + options.DistrictType.DisplayName + ")";
                report.ReportOptions.ReportGenerator = new EliminationDistrictsReportGenerator();
            }

            options.Diseases = new List<Disease>();

            options.Diseases.Add((Disease)cbDiseases.SelectedItem);
            report.ReportOptions.StartDate = dtStart.Value;
            report.ReportOptions.EndDate = dtEnd.Value;
            report.ReportOptions.MonthYearStarts = Convert.ToInt32(cbMonths.SelectedValue);
            OnSwitchStep(new StepLocations(report, this));
        }

        public void DoFinish()
        {
        }

        public EliminationOptions()
            : base()
        {
            InitializeComponent();
        }

        public EliminationOptions(SavedReport o)
            : base()
        {
            report = o;
            options = (EliminationReportOptions)o.StandardReportOptions;
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
                
                DiseaseRepository r = new DiseaseRepository();
                var diseases = r.GetSelectedDiseases().Where(d => d.DiseaseType == Translations.PC).ToList();
                bindingSource1.DataSource = diseases;
                cbDiseases.DropDownWidth = BaseForm.GetDropdownWidth(diseases.Select(m => m.DisplayName));
                if (options.Diseases.Count > 0)
                    cbDiseases.SelectedItem = diseases.FirstOrDefault(d => d.Id == options.Diseases.First().Id);
                else
                    cbMonths.SelectedIndex = 0;

                SettingsRepository settings = new SettingsRepository();
                cbEliminationType.Items.Add(Translations.Persons);
                cbEliminationType.Items.Add(Translations.RtiReportingLevel);
                if (options.IsPersons)
                    cbEliminationType.SelectedItem = Translations.Persons;
                else
                    cbEliminationType.SelectedItem = options.DistrictType.DisplayName;

                rbAggCountry.Checked = report.ReportOptions.IsCountryAggregation;
                rbAggLevel.Checked = report.ReportOptions.IsByLevelAggregation;

                var allLevelTypes = settings.GetAllAdminLevels();
                var reportingType = allLevelTypes.First();
                options.DistrictType = reportingType;
                bindingSource2.DataSource = allLevelTypes;
                bindingSource3.DataSource = options;
            }
        }

        private void cbEliminationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEliminationType.SelectedItem.ToString() == Translations.Persons)
            {
                rbAggLevel.Visible = true;
            }
            else
            {
                rbAggLevel.Visible = false;
                rbAggCountry.Checked = true;
            }
        }
    }
}
