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
using Nada.Model;

namespace Nada.UI.View.Reports.CustomReport
{
    public partial class StepLocations : BaseControl, IWizardStep
    {
        private IWizardStep prev = null;
        private SavedReport report = null;
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return true; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return Translations.ReportLocations; } }

        public void DoPrev()
        {
            OnSwitchStep(prev);
        }

        public void DoNext()
        {   
        }

        public void DoFinish()
        {
            report.ReportOptions.IsAllLocations = false;
            if (report.ReportOptions.IsNoAggregation && cbAllLocations.Checked)
                report.ReportOptions.IsAllLocations = true;
            if (report.ReportOptions.IsNoAggregation)
            {
                report.ReportOptions.SelectedAdminLevels = pickerAllLocations.GetSelected();
                if (report.ReportOptions.SelectedAdminLevels.Count == 0)
                {
                    MessageBox.Show(Translations.LocationRequired, Translations.ValidationErrorTitle);
                    return;
                }
            }
            else if (report.ReportOptions.IsByLevelAggregation)
            {
                report.ReportOptions.SelectedAdminLevels = levelPicker.GetSelectedAdminLevels();
                if (report.ReportOptions.SelectedAdminLevels.Count == 0)
                {
                    MessageBox.Show(Translations.LocationRequired, Translations.ValidationErrorTitle);
                    return;
                }
            }
            else if (report.ReportOptions.IsCountryAggregation)
                report.ReportOptions.SelectedAdminLevels = new List<AdminLevel> { new AdminLevel { Id = 1 } }; 
            OnRunReport(report);
        }

        public StepLocations()
            : base()
        {
            InitializeComponent();
        }

        public StepLocations(SavedReport o, IWizardStep p)
            : base()
        {
            prev = p;
            report = o;
            InitializeComponent();
        }

        private void StepLocations_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                lblNoLocations.Visible = false;
                levelPicker.Visible = false;
                tblListAllLocations.Visible = false;

                if (report.ReportOptions.IsNoAggregation)
                {
                    cbAllLocations.Checked = report.ReportOptions.IsAllLocations;
                    tblListAllLocations.Visible = true;
                    pickerAllLocations.ShowRedistricted(report.ReportOptions.ShowOnlyRedistrictedUnits);
                    if (!report.ReportOptions.IsAllLocations)
                        pickerAllLocations.SetSelected(report.ReportOptions.SelectedAdminLevels);
                }
                else if (report.ReportOptions.IsByLevelAggregation)
                {
                    levelPicker.Visible = true;
                    levelPicker.ShowRedistricted(report.ReportOptions.ShowOnlyRedistrictedUnits);
                    levelPicker.SetSelectedItems(report.ReportOptions.SelectedAdminLevels);
                }
                else
                    lblNoLocations.Visible = true;
            }
        }

        private void cbAllLocations_CheckedChanged(object sender, EventArgs e)
        {
            pickerAllLocations.Visible = !cbAllLocations.Checked;
        }


    }
}
