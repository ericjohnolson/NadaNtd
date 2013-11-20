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
    public partial class StepDiseases : UserControl, IWizardStep
    {
        DiseaseRepository r = new DiseaseRepository();
        public Action OnFinish { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return true; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return Translations.Diseases; } }

        public StepDiseases()
        {
            InitializeComponent();
        }
                        
        private void ImportOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                diseasePickerControl1.LoadLists(true);
            }
        }

        public void DoPrev()
        {
        }

        public void DoNext()
        {
        }

        public void DoFinish()
        {
            var selected = diseasePickerControl1.GetSelectedItems();
            var available = diseasePickerControl1.GetUnselectedItems();
            int userId = ApplicationData.Instance.GetUserId();
            r.SaveSelectedDiseases(selected, true, userId);
            r.SaveSelectedDiseases(available, false, userId);
            SettingsRepository settings = new SettingsRepository();
            settings.SetDiseasesReviewedStatus();
            OnFinish();
        }
    }
}
