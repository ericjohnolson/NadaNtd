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
    public partial class MessageBoxStep : BaseControl, IWizardStep
    {
        IWizardStep prev = null;
        public Action OnFinish { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return true; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get; set; }
        private string msg = "";
        private bool wasError = false;

        public MessageBoxStep() 
            : base()
        {
            InitializeComponent();
        }

        public MessageBoxStep(string title, string message, bool error, IWizardStep p)
            : base()
        {
            wasError = error;
            StepTitle = title;
            msg = message;
            prev = p;
            InitializeComponent();
        }

        private void ImportStepResult_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                tbStatus.Text = msg;

                if (wasError)
                    MessageBox.Show(Translations.ReviewErrors, Translations.ErrorOccured, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DoPrev()
        {
            OnSwitchStep(prev);
        }

        public void DoNext()
        {
        }

        public void DoFinish()
        {
            OnFinish();
        }
    }
}
