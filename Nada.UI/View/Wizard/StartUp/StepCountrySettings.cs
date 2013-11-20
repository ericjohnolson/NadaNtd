using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Csv;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using OfficeOpenXml;

namespace Nada.UI.View.Wizard
{
    public partial class StepCountrySettings : UserControl, IWizardStep
    {
        private Country model = null;
        public Action OnFinish { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.CountrySettings; } }

        public StepCountrySettings()
        {
            InitializeComponent();
        }

        public StepCountrySettings(Country c)
        {
            model = c;
            InitializeComponent();
        }
                
        private void ImportOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                countryView1.LoadCountry(model);
            }
        }

        public void DoPrev()
        {
        }

        public void DoNext()
        {
            if (!SaveCountry())
                return;

            OnSwitchStep(new StepCountryDemo());
        }

        public void DoFinish()
        {
            if (!SaveCountry())
                return;
            OnFinish();
        }

        private bool SaveCountry()
        {
            if (!model.IsValid())
            {
                MessageBox.Show(Translations.ValidationError);
                return false;
            }
            if (!adminLevelTypesControl1.HasDistrict())
            {
                MessageBox.Show(Translations.MustMakeDistrict);
                return false;
            }

            int userId = ApplicationData.Instance.GetUserId();
            var demo = new DemoRepository();
            demo.UpdateCountry(model, userId);
            return true;
        }


    }
}
