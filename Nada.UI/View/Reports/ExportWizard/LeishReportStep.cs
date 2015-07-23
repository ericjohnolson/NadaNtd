using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Exports;
using Nada.UI.Base;
using Nada.UI.AppLogic;
using Nada.Model.Reports;
using Nada.Globalization;

namespace Nada.UI.View.Reports.ExportWizard
{
    public partial class LeishReportStep : BaseControl, IWizardStep
    {
        //SettingsRepository settings = new SettingsRepository();
        //ExportRepository repo = new ExportRepository();
        LeishReportQuestions questions = null;
        LeishReportExporter exporter = null;
        string title = "";
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return true; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return Translations.StartExport; } }

        public LeishReportStep()
            : base()
        {
            InitializeComponent();
        }

        public LeishReportStep(LeishReportExporter e, string t)
            : base()
        {
            exporter = e;
            title = t;
            InitializeComponent();
        }

        public void DoPrev()
        {
            throw new NotImplementedException();
        }
        public void DoNext()
        {
            throw new NotImplementedException();
        }
        public void DoFinish()
        {
            throw new NotImplementedException();
        }

    }
}
