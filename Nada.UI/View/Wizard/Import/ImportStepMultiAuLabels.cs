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
    public partial class ImportStepMultiAuLabels : BaseControl, IWizardStep
    {
        private ImportOptions options = null;
        public Action OnFinish { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.ImportMultiUnits; } }
        private List<IndicatorDropdownValue> surveyNames = new List<IndicatorDropdownValue>();

        public ImportStepMultiAuLabels()
            : base()
        {
            InitializeComponent();
        }

        public ImportStepMultiAuLabels(ImportOptions o, Action finish)
            : base()
        {
            OnFinish = finish;
            options = o;
            InitializeComponent();
        }
        
        public void DoPrev()
        {
            OnSwitchStep(new ImportStepMultiUnits(options, OnFinish));
        }

        public void DoNext()
        {
            if (lbSurveyNames.Items.Count == 0)
            {
                MessageBox.Show(Translations.SurveyName + " - " + Translations.IsRequired, Translations.ValidationErrorTitle);
                return;
            }
            options.SurveyNames = lbSurveyNames.Items.Cast<IndicatorDropdownValue>().ToList();
            OnSwitchStep(new ImportStepLists(options, OnFinish));
        }
        public void DoFinish()
        {
        }

        private void ImportStepMultiAuLabels_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
            }
        }

        private void fieldLink1_ClickOverride()
        {
            IndicatorValueItemAdd form = new IndicatorValueItemAdd(new IndicatorDropdownValue { IndicatorId = 1, EntityType = IndicatorEntityType.Export }, new Indicator { DisplayName = "SurveyName" });
            form.OnSave += (v) =>
            {
                if (surveyNames.FirstOrDefault(s => s.DisplayName == v.DisplayName) != null)
                {
                    MessageBox.Show(string.Format(Translations.ValidationMustBeUnique, Translations.SurveyName), Translations.ValidationErrorTitle);
                    return;
                }
                surveyNames.Add(v);
                lbSurveyNames.Items.Add(v);
            };
            form.ShowDialog();
        }
    }
}
