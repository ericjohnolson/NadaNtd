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
using Nada.UI.Base;
using Nada.UI.ViewModel;
using OfficeOpenXml;

namespace Nada.UI.View.Wizard
{
    public partial class StepDdUpdateOptions : BaseControl, IWizardStep
    {
        private DdUpdateViewModel vm { get; set; }
        public Action OnFinish { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return true; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return Translations.SelectDemoUpdateOption; } }

        public StepDdUpdateOptions(DdUpdateViewModel v)
            : base()
        {
            vm = v;
            InitializeComponent();
        }
        
        private void ImportOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                lblUpdateComplete.SetMaxWidth(500);
            }
        }

        public void DoPrev()
        {
        }

        public void DoNext()
        {
            DiseaseRepository diseases = new DiseaseRepository();
            vm.Diseases = diseases.GetSelectedDiseases();
            vm.DiseaseStepNumber = 0;
            OnSwitchStep(new StepDdReview(vm, this));
        }

        public void DoFinish()
        {
            OnFinish();
        }
    }
}
