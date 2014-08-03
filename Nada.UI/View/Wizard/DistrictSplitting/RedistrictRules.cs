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
using Nada.Model;
using Nada.UI.View.Wizard;
using System.Threading;
using Nada.UI.Base;
using Nada.Model.Repositories;
using Nada.Model.Exports;
using Nada.Model.Demography;
using Nada.UI.Controls;
using Nada.UI.ViewModel;

namespace Nada.UI.View.Wizard
{
    public partial class RedistrictRules : BaseControl, IWizardStep
    {
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.SplittingSaes; } }
        private DemoRepository demo = new DemoRepository();
        private List<DynamicContainer> containers = new List<DynamicContainer>();
        RedistrictingOptions options;

        public RedistrictRules(RedistrictingOptions o)
            : base()
        {
            InitializeComponent();
            options = o;
        }

        private void RedistrictRules_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);

                if (options.SplitType == SplittingType.Split)
                {
                    var vals = new List<IndicatorDropdownValue>();
                    foreach (RedistrictingRule rule in (RedistrictingRule[])Enum.GetValues(typeof(RedistrictingRule)))
                        vals.Add(new IndicatorDropdownValue { DisplayName = rule.ToString(), WeightedValue = (int)rule });

                    lblIndicatorType.Text = Translations.RedistrictRuleSplit;
                    AddRuleControls(demo.GetCustomIndicatorsWithoutRedistrictingRules(options.SplitType), vals);
                }
                else if (options.SplitType == SplittingType.Merge)
                {
                    var vals = new List<IndicatorDropdownValue>();
                    foreach (MergingRule rule in (MergingRule[])Enum.GetValues(typeof(MergingRule)))
                        vals.Add(new IndicatorDropdownValue { DisplayName = rule.ToString(), WeightedValue = (int)rule });
                    lblIndicatorType.Text = Translations.RedistrictRuleMerge;
                    AddRuleControls(demo.GetCustomIndicatorsWithoutRedistrictingRules(options.SplitType), vals);
                }
                //else
                //{
                //    lblIndicatorType.Text = Translations.RedistrictRuleSplitCombine;
                //    AddRuleControls(demo.GetCustomIndicatorsWithoutRedistrictingRules(options.SplitType), new List<IndicatorDropdownValue>());
                //}

                if (containers.Count() == 0)
                    DoNextStep();
            }
        }

        private void AddRuleControls(List<Indicator> indicators, List<IndicatorDropdownValue> values)
        {

            for (int i = 0; i < indicators.Count; i++)
            {
                var index = tblNewUnits.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });

                // Add control to screen
                var label = new H3bLabel { AutoSize = true, Text = indicators[i].DisplayName, Margin = new Padding(0, 5, 10, 5) };
                label.MakeBold();
                tblNewUnits.Controls.Add(label, 0, index);
                var cntrl = CreateDropdown(indicators[i], values);
                tblNewUnits.Controls.Add(cntrl, 1, index);
            }
        }

        public void DoPrev()
        {
        }

        public void DoNext()
        {
            var invalid = containers.FirstOrDefault(m => !m.IsValid());
            if (invalid != null)
            {
                MessageBox.Show(Translations.SplittingMustAllocateRules, Translations.ValidationErrorTitle);
                return;
            }

            List<Indicator> indicators = new List<Indicator>();
            foreach (DynamicContainer m in containers)
            {
                m.Indicator.RedistrictRuleId = Convert.ToInt32(m.GetValue());
                indicators.Add(m.Indicator);
            }
            demo.SaveCustomIndicatorRules(indicators);

            DoNextStep();
        }

        private void DoNextStep()
        {
            OnSwitchStep(new SplitDate(options));
        }

        public void DoFinish()
        {
        }

        private Control CreateDropdown(Indicator indicator, List<IndicatorDropdownValue> dropdownKeys)
        {
            List<IndicatorDropdownValue> availableValues = new List<IndicatorDropdownValue>();
            var container = new DynamicContainer { Indicator = indicator };
            var cntrl = new ComboBox { Name = "dynamicCombo" + indicator.Id.ToString(), Width = 220, Margin = new Padding(0, 5, 10, 5), DropDownStyle = ComboBoxStyle.DropDownList };
            cntrl.MouseWheel += (s, e) => { ((HandledMouseEventArgs)e).Handled = true; };

            cntrl.Items.Add(new IndicatorDropdownValue { DisplayName = "", Id = -1 });
            foreach (IndicatorDropdownValue v in dropdownKeys)
            {
                cntrl.Items.Add(v);
                availableValues.Add(v);
            }
            cntrl.ValueMember = "WeightedValue";
            cntrl.DisplayMember = "DisplayName";
            if (availableValues.Count > 0)
                cntrl.DropDownWidth = BaseForm.GetDropdownWidth(availableValues.Select(a => a.DisplayName));

            container.IsValid = () =>
            {
                if (cntrl.Text == "" || cntrl.Text == null)
                    return false;

                return true;
            };
            cntrl.Validating += (s, e) => { container.IsValid(); };

            container.GetValue = () =>
            {
                if (cntrl.SelectedItem == null || ((IndicatorDropdownValue)cntrl.SelectedItem).Id == -1)
                    return null;
                return ((IndicatorDropdownValue)cntrl.SelectedItem).WeightedValue.ToString();
            };

            containers.Add(container);
            return cntrl;
        }
    }
}
