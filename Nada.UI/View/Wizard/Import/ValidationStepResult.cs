﻿using System;
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
    public partial class ValidationStepResult : BaseControl, IWizardStep
    {
        ImportResult result = null;
        IWizardStep prev = null;
        bool showFinish = true;
        public Action OnFinish { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return showFinish; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get; set; }

        public ValidationStepResult()
            : base()
        {
            InitializeComponent();
        }

        public ValidationStepResult(ImportResult r, IWizardStep p)
            : base()
        {
            result = r;
            prev = p;
            InitializeComponent();
            SetStepTitle(r);
        }

        public ValidationStepResult(ImportResult r, IWizardStep p, bool f)
            : base()
        {
            showFinish = f;
            result = r;
            prev = p;
            InitializeComponent();
            SetStepTitle(r);
        }

        private void ValidationStepResult_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                tbStatus.Text = result.Message;
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

        private void SetStepTitle(ImportResult result)
        {
            if (!result.WasSuccess)
                StepTitle = Translations.ValidationImportReviewErrorsBelow;
            else
                StepTitle = Translations.ValidationImportNoErrors;
        }
    }
}
