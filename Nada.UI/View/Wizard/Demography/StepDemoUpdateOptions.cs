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
using OfficeOpenXml;

namespace Nada.UI.View.Wizard
{
    public partial class StepDemoUpdateOptions : BaseControl, IWizardStep
    {
        int countryDemoId;
        private DateTime demoDate { get; set; }
        public Action OnFinish { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return true; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return Translations.SelectDemoUpdateOption; } }

        public StepDemoUpdateOptions(DateTime d, int cid)
            : base()
        {
            countryDemoId = cid;
            demoDate = d;
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
            SettingsRepository settings = new SettingsRepository();
            var als = settings.GetAllAdminLevels();
            var aggLevel = als.FirstOrDefault(a => a.IsAggregatingLevel);
            OnSwitchStep(new StepAdminLevelImport(aggLevel, this, true, demoDate, countryDemoId));
        }

        public void DoFinish()
        {
            OnFinish();
        }
    }
}
