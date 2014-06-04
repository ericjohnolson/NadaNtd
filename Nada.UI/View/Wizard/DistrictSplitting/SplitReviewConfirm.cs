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
using Nada.Model.Demography;

namespace Nada.UI.View.Wizard
{
    public partial class SplitReviewConfirm : BaseControl, IWizardStep
    {
        private RedistrictingOptions options;
        public Action OnFinish { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.SplitConfirmReviewTitle; } }

        public SplitReviewConfirm(RedistrictingOptions o, string message)
            : base()
        {
            options = o;
            InitializeComponent();
            lblMessage.Text = message;
        }
        
        private void ImportOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                lblMessage.SetMaxWidth(500);
            }
        }

        public void DoPrev()
        {

        }

        public void DoNext()
        {
            OnSwitchStep(new SplitDistro(options, this));
        }

        public void DoFinish()
        {
        }
    }
}
