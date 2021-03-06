﻿using System;
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
        public string StepTitle { get { return Translations.RedistrictingRulesSplit; } }
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
                    lblIndicatorType.Text = Translations.RedistrictRuleSplit;
                    AddRuleControls(demo.GetCustomIndicatorsWithoutRedistrictingRules(options.SplitType), options.SplitType);
                }
                else if (options.SplitType == SplittingType.Merge)
                {
                    lblIndicatorType.Text = Translations.RedistrictRuleMerge;
                    AddRuleControls(demo.GetCustomIndicatorsWithoutRedistrictingRules(options.SplitType), options.SplitType);
                }

                if (containers.Count() == 0)
                    DoNextStep();
            }
        }

        private void AddRuleControls(List<Indicator> indicators, SplittingType splitType)
        {
            for (int i = 0; i < indicators.Count; i++)
            {
                var index = tblNewUnits.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });

                // Add control to screen
                var label = new H3bLabel { AutoSize = true, Text = indicators[i].DisplayName, Margin = new Padding(0, 5, 10, 5) };
                label.MakeBold();
                tblNewUnits.Controls.Add(label, 0, index);
                var cntrl = CreateDropdown(indicators[i], GetRuleValues(splitType, indicators[i].DataTypeId));
                tblNewUnits.Controls.Add(cntrl, 1, index);
            }
        }

        private List<IndicatorDropdownValue> GetRuleValues(SplittingType splitType, int dataTypeId)
        {
            var vals = new List<IndicatorDropdownValue>();

            if (options.SplitType == SplittingType.Split)
            {
                if (dataTypeId == (int)IndicatorDataType.Number)
                    vals.Add(new IndicatorDropdownValue { DisplayName = Translations.RedistrictRuleSplitByPercent, WeightedValue = (int)RedistrictingRule.SplitByPercent });
                vals.Add(new IndicatorDropdownValue { DisplayName = Translations.RedistrictRuleDefaultBlank, WeightedValue = (int)RedistrictingRule.DefaultBlank });
                vals.Add(new IndicatorDropdownValue { DisplayName = Translations.RedistrictRuleDuplicate, WeightedValue = (int)RedistrictingRule.Duplicate });
                vals.OrderBy(v => v.DisplayName).ToList();
            }
            else if (options.SplitType == SplittingType.Merge)
            {
                if (dataTypeId == (int)IndicatorDataType.Number || dataTypeId == (int)IndicatorDataType.Year || dataTypeId == (int)IndicatorDataType.Integer)
                {
                    vals.Add(new IndicatorDropdownValue { DisplayName = Translations.RedistrictRuleMin, WeightedValue = (int)MergingRule.Min });
                    vals.Add(new IndicatorDropdownValue { DisplayName = Translations.RedistrictRuleMax, WeightedValue = (int)MergingRule.Max });
                    if (dataTypeId == (int)IndicatorDataType.Number || dataTypeId == (int)IndicatorDataType.Integer)
                    {
                        vals.Add(new IndicatorDropdownValue { DisplayName = Translations.RedistrictRuleAverage, WeightedValue = (int)MergingRule.Average });
                        vals.Add(new IndicatorDropdownValue { DisplayName = Translations.RedistrictRuleSum, WeightedValue = (int)MergingRule.Sum });
                    }
                }
                else if (dataTypeId == (int)IndicatorDataType.Text || dataTypeId == (int)IndicatorDataType.LargeText || dataTypeId == (int)IndicatorDataType.Multiselect)
                {
                    vals.Add(new IndicatorDropdownValue { DisplayName = Translations.RedistrictRuleListAll, WeightedValue = (int)MergingRule.ListAll });
                }

                vals.Add(new IndicatorDropdownValue { DisplayName = Translations.RedistrictRuleDefaultBlank, WeightedValue = (int)MergingRule.DefaultBlank });
                vals.OrderBy(v => v.DisplayName).ToList();
            }

            return vals;
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
                if (options.SplitType == SplittingType.Split)
                    m.Indicator.RedistrictRuleId = Convert.ToInt32(m.GetValue());
                else if (options.SplitType == SplittingType.Merge)
                    m.Indicator.MergeRuleId = Convert.ToInt32(m.GetValue());
                
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
