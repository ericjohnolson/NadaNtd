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
        private ReportOptions options = null;
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
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
            OnSwitchStep(new StepOptions(options));
        }

        public void DoNext()
        {   
        }

        public void DoFinish()
        {
            options.IsAllLocations = false;
            if (options.IsNoAggregation && cbAllLocations.Checked)
                options.IsAllLocations = true;
            if (options.IsNoAggregation)
                options.SelectedAdminLevels = pickerAllLocations.GetSelected();
            else if (options.IsByLevelAggregation)
                options.SelectedAdminLevels = levelPicker.GetSelectedAdminLevels();
            else if (options.IsCountryAggregation)
                options.SelectedAdminLevels = new List<AdminLevel> { new AdminLevel { Id = 1 } }; 
            OnRunReport(options);
        }

        public StepLocations()
            : base()
        {
            InitializeComponent();
        }

        public StepLocations(ReportOptions o)
            : base()
        {
            options = o;
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

                if (options.IsNoAggregation)
                {
                    cbAllLocations.Checked = options.IsAllLocations;
                    tblListAllLocations.Visible = true;
                    if(!options.IsAllLocations)
                        pickerAllLocations.SetSelected(options.SelectedAdminLevels);
                }
                else if (options.IsByLevelAggregation)
                {
                    levelPicker.Visible = true;
                    levelPicker.SetSelectedItems(options.SelectedAdminLevels);
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
