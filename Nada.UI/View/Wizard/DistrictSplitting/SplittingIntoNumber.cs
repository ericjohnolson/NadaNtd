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
using Nada.UI.View.Wizard.DistrictSplitting;

namespace Nada.UI.View.Wizard
{
    public partial class SplittingIntoNumber : BaseControl, IWizardStep
    {
        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();
        private SplittingOptions options = null;
        public Action OnFinish { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.SplitHowMany; } }

        public SplittingIntoNumber(SplittingOptions o)
            : base()
        {
            options = o;
            InitializeComponent();
        }
        
        public void DoPrev()
        {
            OnSwitchStep(new SplittingSource(options));
        }

        public void DoNext()
        {
            if (!options.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }

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
                bsImportOptions.DataSource = options;
                lblHowMany.SetMaxWidth(500);
                lblHowMany.Text = string.Format(Translations.HowManyAdminUnits, options.Source.Name);
            }
        }


    }
}
