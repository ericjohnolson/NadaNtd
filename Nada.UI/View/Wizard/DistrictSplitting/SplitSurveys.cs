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
using Nada.UI.View.Wizard.DistrictSplitting;
using Nada.UI.Controls;

namespace Nada.UI.View.Wizard
{

    public partial class SplitSurveys : BaseControl, IWizardStep
    {
        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();
        private SplittingOptions options = null;
        private IWizardStep prev = null;
        public Action OnFinish { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.Surveys; } }

        public SplitSurveys(SplittingOptions o, IWizardStep p)
            : base()
        {
            prev = p;
            options = o;
            InitializeComponent();
        }

        public void DoPrev()
        {
            OnSwitchStep(prev);
        }

        public void DoNext()
        {
            OnSwitchStep(new SplitIntv(options, this));
        }

        public void DoFinish()
        {
        }

        private void StepCategory_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);

                SurveyRepository repo = new SurveyRepository();

                foreach (var t in repo.GetSurveyTypes())
                {
                    var index = tblNewUnits.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    var lblName = new H3bLabel { AutoSize = true, Text = t.SurveyTypeName, Margin = new Padding(0, 5, 10, 5) };
                    tblNewUnits.Controls.Add(lblName, 0, index);

                    var lnk = new H3Link { Text = Translations.DownloadImportFile, Margin = new Padding(0, 5, 10, 5) };
                    tblNewUnits.Controls.Add(lnk, 1, index);
                    var lnk2 = new H3Link { Text = Translations.UploadImportFile, Margin = new Padding(0, 5, 10, 5) };
                    tblNewUnits.Controls.Add(lnk2, 2, index);
                }
            }
        }




    }
}
