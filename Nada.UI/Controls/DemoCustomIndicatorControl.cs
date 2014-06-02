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
using System.Globalization;

namespace Nada.UI.View
{
    public partial class CustomIndicatorControl : BaseControl
    {
        public event Action OnAddRemove = () => { };
        private List<DynamicContainer> controlList = new List<DynamicContainer>();

        public CustomIndicatorControl()
            : base()
        {
            InitializeComponent();
        }
        
        public void LoadIndicators(Dictionary<string, Indicator> indicators)
        {
            Localizer.TranslateControl(this);
            LoadIndicators(indicators, new List<IndicatorValue>());
        }

        public void LoadIndicators(Dictionary<string, Indicator> indicators, List<IndicatorValue> values)
        {
            this.SuspendLayout();
            controlList = new List<DynamicContainer>();
            tblIndicators.Controls.Clear();
            int count = 0;
            int labelRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize }); 
            int controlRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize }); 
            int columnCount = 0;
            foreach (var indicator in indicators.Values.Where(i => i.IsEditable && !i.IsDisplayed).ToList())
            {
                if (count % 4 == 0)
                {
                    labelRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    controlRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    columnCount = 0;
                }

                // Add label
                tblIndicators.Controls.Add(
                    new H3Label { Text = indicator.DisplayName, Name = "ciLabel_" + indicator.Id },
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
            this.ResumeLayout();
            IsValid();
        }

        public bool IsValid()
        {
            foreach (DynamicContainer cnt in controlList)
            {
                if (!cnt.IsValid())
                    return false;
            }
            return true;
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
                var cntrl = new NullableDatePickerControl
                {
                    Name = "dynamicDt" + indicator.Id.ToString(),
                    ShowClear = true
                };
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
                if (DateTime.TryParseExact(val, "MM/dd/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out dt))
                    cntrl.Value = dt;
                else
                    cntrl.Value = DateTime.MinValue;

                container.GetValue = () =>
                {
                    if (cntrl.GetValue() == DateTime.MinValue)
                        return "";
                    else
                        return cntrl.GetValue().ToString("MM/dd/yyyy");
                };
                controlList.Add(container);
                return cntrl;
            }
            else if (indicator.DataTypeId == (int)IndicatorDataType.Number)
            {
                double d = 0;
                var cntrl = new TextBox { Name = "dynamicNum" + indicator.Id.ToString(), Text = val };
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
            else
            {
                var cntrl = new TextBox { Name = "dynamicTxt" + indicator.Id.ToString(), Text = val };
                container.IsValid = () => {
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
            set { lblCustomIndicators.ForeColor = value; }
        }

        private void fieldLink1_OnClick()
        {
            OnAddRemove();
        }


    }
}
