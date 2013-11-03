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

namespace Nada.UI.View
{
    public partial class IndicatorControl : UserControl
    {
        public event Action OnAddRemove = () => { };
        private List<DynamicContainer> controlList = new List<DynamicContainer>();
        private List<KeyValuePair<int, string>> dropdownKeys = new List<KeyValuePair<int, string>>();

        public IndicatorControl()
        {
            InitializeComponent();
        }

        public void LoadIndicators(Dictionary<string, Indicator> indicators, List<KeyValuePair<int, string>> keys)
        {
            Localizer.TranslateControl(this);
            LoadIndicators(indicators, new List<IndicatorValue>(), keys);
        }

        public void LoadIndicators(Dictionary<string, Indicator> indicators, List<IndicatorValue> values, List<KeyValuePair<int, string>> keys)
        {
            dropdownKeys = keys;
            this.SuspendLayout();
            controlList = new List<DynamicContainer>();
            LoadCustom(indicators, values);
            LoadStatic(indicators, values);
            this.ResumeLayout();
            IsValid();
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
                if (count % 2 == 0)
                {
                    labelRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    controlRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    columnCount = 0;
                }

                // Add label
                tblStaticIndicators.Controls.Add(
                    new H3bLabel { Text = TranslationLookup.GetValue(indicator.DisplayName, indicator.DisplayName), 
                        Name = "ciLabel_" + indicator.Id, AutoSize = true, },
                    columnCount, labelRowIndex);

                // Add field
                string val = "";
                IndicatorValue iv = values.FirstOrDefault(i => i.IndicatorId == indicator.Id);
                if (iv != null)
                    val = iv.DynamicValue;

                tblStaticIndicators.Controls.Add(CreateControl(indicator, val), columnCount, controlRowIndex);

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
                tblIndicators.Controls.Add(
                    new H3bLabel { Text = indicator.DisplayName, Name = "ciLabel_" + indicator.Id, AutoSize = true, },
                    columnCount, labelRowIndex);

                // Add field
                string val = "";
                IndicatorValue iv = values.FirstOrDefault(i => i.IndicatorId == indicator.Id);
                if (iv != null)
                    val = iv.DynamicValue;

                tblIndicators.Controls.Add(CreateControl(indicator, val), columnCount, controlRowIndex);

                count++;
                columnCount = columnCount + 2;
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

        private Control CreateControl(Indicator indicator, string val)
        {
            var container = new DynamicContainer { Indicator = indicator };
            if (indicator.DataTypeId == (int)IndicatorDataType.Date)
            {
                DateTime d;
                var cntrl = new DateTimePicker { Name = "dynamicDt" + indicator.Id.ToString(), Width = 220, Margin = new Padding(0, 5, 10, 5) };
                container.IsValid = () =>
                {
                    if (indicator.IsRequired)
                    {
                        if (cntrl.Text == "" || cntrl.Text == null)
                        {
                            indicatorErrors.SetError(cntrl, Translations.Required);
                            return false;
                        }
                        else if (!DateTime.TryParse(cntrl.Text, out d))
                        {
                            indicatorErrors.SetError(cntrl, Translations.MustBeDate);
                            return false;
                        }
                        else
                            indicatorErrors.SetError(cntrl, "");
                    }
                    return true;
                };
                cntrl.Validating += (s, e) => { container.IsValid(); };
                DateTime dt = new DateTime();
                if (DateTime.TryParse(val, out dt))
                    cntrl.Value = dt;

                container.GetValue = () => { return cntrl.Value.ToString("MM/dd/yyyy"); };
                controlList.Add(container);
                return cntrl;
            }
            else if (indicator.DataTypeId == (int)IndicatorDataType.Number)
            {
                double d = 0;
                var cntrl = new TextBox { Name = "dynamicNum" + indicator.Id.ToString(), Text = val, Width = 220, Margin = new Padding(0, 5, 10, 5) };
                container.IsValid = () =>
                {
                    if (indicator.IsRequired)
                    {
                        if (cntrl.Text == "" || cntrl.Text == null)
                        {
                            indicatorErrors.SetError(cntrl, Translations.Required);
                            return false;
                        }
                        else if (!Double.TryParse(cntrl.Text, out d))
                        {
                            indicatorErrors.SetError(cntrl, Translations.MustBeNumber);
                            return false;
                        }
                        else
                            indicatorErrors.SetError(cntrl, "");
                    }
                    return true;
                };
                cntrl.Validating += (s, e) => { container.IsValid(); };

                container.GetValue = () => { return cntrl.Text; };
                controlList.Add(container);
                return cntrl;
            }
            else if (indicator.DataTypeId == (int)IndicatorDataType.YesNo)
            {
                var cntrl = new CheckBox { Name = "dynamicChk" + indicator.Id.ToString() };
                container.IsValid = () => { return true; };
                bool isChecked = false;
                if (Boolean.TryParse(val, out isChecked))
                    cntrl.Checked = isChecked;

                container.GetValue = () => { return Convert.ToInt32(cntrl.Checked).ToString(); };
                controlList.Add(container);
                return cntrl;
            }
            else if (indicator.DataTypeId == (int)IndicatorDataType.Dropdown)
            {
                var cntrl = new ComboBox { Name = "dynamicCombo" + indicator.Id.ToString(), Width = 220, Margin = new Padding(0, 5, 10, 5) };
                foreach (KeyValuePair<int, string> key in dropdownKeys.Where(k => k.Key == indicator.Id))
                    cntrl.Items.Add(TranslationLookup.GetValue(key.Value, key.Value));

                if(!string.IsNullOrEmpty(val))
                    cntrl.Text = val;
    
                container.IsValid = () =>
                {
                    if (indicator.IsRequired)
                    {
                        if (cntrl.Text == "" || cntrl.Text == null)
                        {
                            indicatorErrors.SetError(cntrl, Translations.Required);
                            return false;
                        }
                        else
                            indicatorErrors.SetError(cntrl, "");
                    }
                    return true;
                };
                cntrl.Validating += (s, e) => { container.IsValid(); };

                container.GetValue = () => { return cntrl.Text; };
                controlList.Add(container);
                return cntrl;
            }
            else
            {
                var cntrl = new TextBox { Name = "dynamicTxt" + indicator.Id.ToString(), Text = val, Width = 220, Margin = new Padding(0, 5, 10, 5) };
                container.IsValid = () =>
                {
                    if (indicator.IsRequired)
                    {
                        if (cntrl.Text == "" || cntrl.Text == null)
                        {
                            indicatorErrors.SetError(cntrl, Translations.Required);
                            return false;
                        }
                        else
                            indicatorErrors.SetError(cntrl, "");
                    }
                    return true;
                };
                cntrl.Validating += (s, e) => { container.IsValid(); };

                container.GetValue = () => { return cntrl.Text; };
                controlList.Add(container);
                return cntrl;
            }
        }

        public class DynamicContainer
        {
            public Indicator Indicator { get; set; }
            public delegate string GetValueDelegate();
            public GetValueDelegate GetValue { get; set; }
            public delegate bool IsValidDelegate();
            public IsValidDelegate IsValid { get; set; }
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

        private void fieldLink1_OnClick()
        {
            OnAddRemove();
        }


    }
}
