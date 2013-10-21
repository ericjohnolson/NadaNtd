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

namespace Nada.UI.View.Reports.CustomReport
{
    public partial class StepLocations : UserControl, IWizardStep
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
            options.SelectedAdminLevels = adminLevelMultiselect1.GetSelectedAdminLevels();
            OnRunReport(options);
        }

        public StepLocations()
        {
            InitializeComponent();
        }

        public StepLocations(ReportOptions o)
        {
            options = o;
            InitializeComponent();
        }

        private void StepLocations_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
            }
        }
    }
}
