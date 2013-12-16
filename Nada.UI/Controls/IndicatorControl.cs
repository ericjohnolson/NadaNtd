﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.UI.Controls;
using Nada.UI.AppLogic;
using Nada.Globalization;
using Nada.UI.Base;
using Nada.UI.ViewModel;

namespace Nada.UI.View
{
    public partial class IndicatorControl : BaseControl
    {
        IndicatorEntityType entityType = IndicatorEntityType.DiseaseDistribution;
        public event Action OnAddRemove = () => { };
        private List<DynamicContainer> controlList = new List<DynamicContainer>();
        private List<IndicatorDropdownValue> dropdownKeys = new List<IndicatorDropdownValue>();

        public IndicatorControl()
            : base()
        {
            InitializeComponent();
        }

        private void IndicatorControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                tblMetaData.Visible = false;
                tblTopControls.Visible = false;
            }
        }

        public void LoadIndicators(Dictionary<string, Indicator> indicators, List<IndicatorDropdownValue> keys, IndicatorEntityType eType)
        {
            LoadIndicators(indicators, new List<IndicatorValue>(), keys, eType);
        }

        public void LoadIndicators(Dictionary<string, Indicator> indicators, List<IndicatorValue> values, List<IndicatorDropdownValue> keys, IndicatorEntityType eType)
        {
            entityType = eType;
            dropdownKeys = keys;
            tblContainer.Visible = false;
            this.SuspendLayout();
            controlList = new List<DynamicContainer>();
            LoadCustom(indicators, values);
            LoadStatic(indicators, values);
            this.ResumeLayout();
            tblContainer.Visible = true;
        }

        public SentinelSitePickerControl LoadSentinelSitePicker(Color color)
        {
            tblTopControls.Visible = true;
            tblTopControls.Controls.Clear();
            var picker = new SentinelSitePickerControl();
            tblTopControls.Controls.Add(picker, 0, 0);
            int hrRow = tblTopControls.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            tblTopControls.Controls.Add(new HR { RuleColor = color, Margin = new Padding(0, 5, 0, 10) }, 0, hrRow);
            return picker;
        }

        public void LoadMetaData(List<KeyValuePair<string, string>> values)
        {
            this.SuspendLayout();
            tblMetaData.Controls.Clear();
            int count = 0;
            int labelRowIndex = tblMetaData.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int controlRowIndex = tblMetaData.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int columnCount = 0;
            foreach (var item in values)
            {
                if (count % 3 == 0)
                {
                    labelRowIndex = tblMetaData.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    controlRowIndex = tblMetaData.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    columnCount = 0;
                }

                // Add label
                tblMetaData.Controls.Add(
                    new H3bLabel { Text = TranslationLookup.GetValue(item.Key, item.Key), Name = "metaData_" + item.Key, AutoSize = true, },
                    columnCount, labelRowIndex);

                // Add val
                var label = new H3bLabel { Text = item.Value, Name = "metaData_Val" + item.Key, AutoSize = true, };
                label.MakeBold();
                tblMetaData.Controls.Add(label, columnCount, controlRowIndex);

                count++;
                columnCount = columnCount + 2;
            }
            this.ResumeLayout();
            tblMetaData.Visible = true;
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color TextColor
        {
            get { return lblCustomIndicators.ForeColor; }
            set
            {
                lblCustomIndicators.ForeColor = value;
                hr4.BackColor = value;
            }
        }

        public bool IsValid()
        {
            bool isValid = true;
            foreach (DynamicContainer cnt in controlList)
            {
                if (!cnt.IsValid())
                    isValid = false;
            }
            return isValid;
        }

        public List<IndicatorValue> GetValues()
        {
            var valList = new List<IndicatorValue>();

            foreach (DynamicContainer cnt in controlList)
            {
                IndicatorValue val = new IndicatorValue();
                val.DynamicValue = cnt.GetValue();
                val.Indicator = cnt.Indicator;
                val.IndicatorId = cnt.Indicator.Id;
                valList.Add(val);
            }
            return valList;
        }

        private void AddRemoveIndicator_OnClick()
        {
            OnAddRemove();
        }

        private void LoadStatic(Dictionary<string, Indicator> indicators, List<IndicatorValue> values)
        {
            tblStaticIndicators.Controls.Clear();
            int count = 0;
            int labelRowIndex = tblStaticIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int controlRowIndex = tblStaticIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int columnCount = 0;
            foreach (var indicator in indicators.Values.Where(i => !i.IsDisplayed).ToList())
            {
                if (indicator.DataTypeId == (int)IndicatorDataType.SentinelSite)
                    continue;

                if (count % 2 == 0)
                {
                    labelRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    controlRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    columnCount = 0;
                }

                // Add label
                tblStaticIndicators.Controls.Add(ControlFactory.CreateLabel(indicator, true), columnCount, labelRowIndex);

                // Add field
                string val = "";
                IndicatorValue iv = values.FirstOrDefault(i => i.IndicatorId == indicator.Id);
                if (iv != null)
                    val = iv.DynamicValue;
                var cntrl = CreateControl(indicator, val);
                cntrl.TabIndex = count;
                tblStaticIndicators.Controls.Add(cntrl, columnCount, controlRowIndex);

                count++;
                columnCount = columnCount + 2;
            }
        }

        private void LoadCustom(Dictionary<string, Indicator> indicators, List<IndicatorValue> values)
        {
            tblIndicators.Controls.Clear();
            int count = 0;
            int labelRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int controlRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int columnCount = 0;
            foreach (var indicator in indicators.Values.Where(i => i.IsDisplayed).ToList())
            {
                if (count % 2 == 0)
                {
                    labelRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    controlRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    columnCount = 0;
                }

                // Add label
                tblIndicators.Controls.Add(ControlFactory.CreateLabel(indicator, false), columnCount, labelRowIndex);

                // Add field
                string val = "";
                IndicatorValue iv = values.FirstOrDefault(i => i.IndicatorId == indicator.Id);
                if (iv != null)
                    val = iv.DynamicValue;

                var cntrl = CreateControl(indicator, val);
                cntrl.TabIndex = count;
                tblIndicators.Controls.Add(cntrl, columnCount, controlRowIndex);

                count++;
                columnCount = columnCount + 2;
            }
        }

        private Control CreateControl(Indicator indicator, string val)
        {
            if (indicator.DataTypeId == (int)IndicatorDataType.Date)
                return ControlFactory.CreateDate(indicator, val, indicatorErrors, controlList);
            if (indicator.DataTypeId == (int)IndicatorDataType.Number)
                return ControlFactory.CreateNumber(indicator, val, indicatorErrors, controlList, IndicatorDataType.Number);
            if (indicator.DataTypeId == (int)IndicatorDataType.Year)
                return ControlFactory.CreateNumber(indicator, val, indicatorErrors, controlList, IndicatorDataType.Year);
            if (indicator.DataTypeId == (int)IndicatorDataType.YesNo)
                return ControlFactory.CreateYesNo(indicator, val, indicatorErrors, controlList);
            if (indicator.DataTypeId == (int)IndicatorDataType.Dropdown)
                return ControlFactory.CreateDropdown(indicator, val, indicatorErrors, controlList, entityType, dropdownKeys);
            if (indicator.DataTypeId == (int)IndicatorDataType.Multiselect)
                return ControlFactory.CreateMulti(indicator, val, indicatorErrors, controlList, entityType, dropdownKeys);
            if (indicator.DataTypeId == (int)IndicatorDataType.Month)
            {
                var months = GlobalizationUtil.GetAllMonths();
                return ControlFactory.CreateDropdown(indicator, val, indicatorErrors, controlList, entityType,
                    months.Select(m => new IndicatorDropdownValue
                    {
                        IndicatorId = indicator.Id,
                        Id = m.Id,
                        DisplayName = m.Name
                    }).ToList());
            }
            if (indicator.DataTypeId == (int)IndicatorDataType.Partners)
                return ControlFactory.CreatePartners(indicator, val, indicatorErrors, controlList);

            return ControlFactory.CreateText(indicator, val, indicatorErrors, controlList);
        }


    }
}
