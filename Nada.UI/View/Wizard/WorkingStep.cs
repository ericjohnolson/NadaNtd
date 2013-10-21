using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.Model.Reports;
using Nada.Globalization;

namespace Nada.UI.View.Wizard
{
    public partial class WorkingStep : UserControl, IWizardStep
    {
        string title = "Working Status";
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return title; } }

        public WorkingStep()
        {
            InitializeComponent();
        }

        public WorkingStep(string t)
        {
            title = t;
            InitializeComponent();
        }

        public void DoPrev()
        {
        }
        public void DoNext()
        {
        }
        public void DoFinish()
        {
        }
    }
}
