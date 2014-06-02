using System;
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
using Nada.Model.Repositories;

namespace Nada.UI.View
{
    public partial class IndicatorControl : BaseControl
    {
        List<string> selectedDiseases = new List<string>();
        IndicatorEntityType entityType = IndicatorEntityType.DiseaseDistribution;
        SettingsRepository settings = new SettingsRepository();
        bool allowCustom = true;
        public event Action OnAddRemove = () => { };
        public event Action OnRangeChange = () => { };
        private List<DynamicContainer> controlList = new List<DynamicContainer>();
        private List<IndicatorDropdownValue> dropdownKeys = new List<IndicatorDropdownValue>();
        public DateTime start = DateTime.Now.AddYears(-1);
        public DateTime end = DateTime.Now;

        public IndicatorControl()
            : base()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AllowCustom
        {
            get { return allowCustom; }
            set
            {
                allowCustom = value;
                hr1.Visible = allowCustom;
                hr4.Visible = allowCustom;
                pnlCustom.Visible = allowCustom;
            }
        }

        private void IndicatorControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                dtStart.Value = start;
                dtEnd.Value = end;
                tblMetaData.Visible = false;
                tblTopControls.Visible = false;
                DiseaseRepository diseases = new DiseaseRepository();
                selectedDiseases = diseases.GetSelectedDiseases().Select(d => d.DisplayName).ToList();
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
                if (count % 4 == 0)
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
                string labelVal = string.IsNullOrEmpty(item.Value) ? Translations.NA : item.Value;
                double d = 0;
                if(double.TryParse(item.Value, out d))
                    labelVal = Convert.ToDouble(item.Value).ToString("N");
                var tb = new TextBox { Text = labelVal, Name = "metaData_Val" + item.Key, Width = 100, ReadOnly = true };
                tblMetaData.Controls.Add(tb, columnCount, controlRowIndex);

                count++;
                columnCount = columnCount + 2;
            }
            this.ResumeLayout();
            tblMetaData.Visible = true;
            lblDateRange.Visible = true;
            tblDateRange.Visible = true;
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
                hr1.BackColor = value;
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
            foreach (var indicator in indicators.Values.Where(i => !i.IsEditable && !i.IsDisplayed && !i.IsCalculated && !i.IsMetaData).ToList())
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
                var cntrl = CreateIndicatorControl(indicator, val);
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
            foreach (var indicator in indicators.Values.Where(i => i.IsEditable && !i.IsDisplayed && !i.IsDisabled).ToList())
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

                var cntrl = CreateIndicatorControl(indicator, val);
                cntrl.TabIndex = count;
                tblIndicators.Controls.Add(cntrl, columnCount, controlRowIndex);

                count++;
                columnCount = columnCount + 2;
            }
        }

        private Control CreateIndicatorControl(Indicator indicator, string val)
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
            if (indicator.DataTypeId == (int)IndicatorDataType.DiseaseMultiselect)
                return ControlFactory.CreateMulti(indicator, val, indicatorErrors, controlList, entityType, dropdownKeys.Where(k => selectedDiseases.Contains(k.DisplayName)).ToList());
            if (indicator.DataTypeId == (int)IndicatorDataType.Month)
            {
                var months = GlobalizationUtil.GetAllMonths();
                return ControlFactory.CreateDropdown(indicator, val, indicatorErrors, controlList, entityType,
                    months.Select(m => new IndicatorDropdownValue
                    {
                        IndicatorId = indicator.Id,
                        Id = m.Id,
                        DisplayName = m.Name, 
                        TranslationKey = m.Id.ToString()
                    }).ToList());
            }
            if (indicator.DataTypeId == (int)IndicatorDataType.Partners)
                return ControlFactory.CreatePartners(indicator, val, indicatorErrors, controlList);
            if (indicator.DataTypeId == (int)IndicatorDataType.EvaluationUnit)
                return ControlFactory.CreateDynamicNameVal(indicator, val, indicatorErrors, controlList, IndicatorEntityType.EvaluationUnit, settings.GetEvaluationUnits());
            if (indicator.DataTypeId == (int)IndicatorDataType.EvaluationSite)
                return ControlFactory.CreateDynamicNameVal(indicator, val, indicatorErrors, controlList, IndicatorEntityType.EvalSite, settings.GetEvalSites());
            if (indicator.DataTypeId == (int)IndicatorDataType.EcologicalZone)
                return ControlFactory.CreateDynamicNameVal(indicator, val, indicatorErrors, controlList, IndicatorEntityType.EcologicalZone, settings.GetEcologicalZones());
            if (indicator.DataTypeId == (int)IndicatorDataType.EvalSubDistrict)
                return ControlFactory.CreateDynamicNameVal(indicator, val, indicatorErrors, controlList, IndicatorEntityType.EvalSubDistrict, settings.GetEvalSubDistricts());
            if (indicator.DataTypeId == (int)IndicatorDataType.LargeText)
                return ControlFactory.CreateText(indicator, val, indicatorErrors, controlList, 100, true);
            
            return ControlFactory.CreateText(indicator, val, indicatorErrors, controlList, 21, false);
        }

        private void h3Link3_ClickOverride()
        {
            start = dtStart.Value;
            end = dtEnd.Value;
            OnRangeChange();
        }

        private void dtStart_ValueChanged(object sender, EventArgs e)
        {
            dtEnd.Value = dtStart.Value.AddYears(1);
        }
    }
}
