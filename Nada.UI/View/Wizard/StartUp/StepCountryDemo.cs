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
    public partial class StepCountryDemo : UserControl, IWizardStep
    {
        DemoRepository r = new DemoRepository();
        private CountryDemography model = null;
        public Action OnFinish { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return true; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return Translations.CountrySettings; } }

        public StepCountryDemo()
        {
            InitializeComponent();
        }
                        
        private void ImportOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                model = r.GetCountryDemoRecent();
                countryDemographyView1.LoadDemo(model);
            }
        }

        public void DoPrev()
        {
            OnSwitchStep(new StepCountrySettings(r.GetCountry()));
        }

        public void DoNext()
        {
        }

        public void DoFinish()
        {
            if (!SaveDemo())
                return;
            OnFinish();
        }
        
        private bool SaveDemo()
        {
            if (!model.IsValid())
            {
                MessageBox.Show(Translations.ValidationError);
                return false;
            }

            int userId = ApplicationData.Instance.GetUserId();
            var demo = new DemoRepository();
            demo.Save(model, userId);
            return true;
        }
    }
}
