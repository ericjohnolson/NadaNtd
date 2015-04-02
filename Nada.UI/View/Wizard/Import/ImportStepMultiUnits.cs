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

namespace Nada.UI.View.Wizard
{
    public partial class ImportStepMultiUnits : BaseControl, IWizardStep
    {
        private ImportOptions options = null;
        public Action OnFinish { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.ImportMultiSurveysQuestion; } }

        public ImportStepMultiUnits()
            : base()
        {
            InitializeComponent();
        }

        public ImportStepMultiUnits(ImportOptions o, Action finish)
            : base()
        {
            options = o;
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

        private void StepCategory_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
            }
        }

        private void lnkImportOne_ClickOverride()
        {
            OnSwitchStep(new ImportStepOptions(options, OnFinish));
        }

        private void lnkImportMulti_ClickOverride()
        {
            OnSwitchStep(new ImportStepMultiAuLabels(options, OnFinish));
        }
    }
}
