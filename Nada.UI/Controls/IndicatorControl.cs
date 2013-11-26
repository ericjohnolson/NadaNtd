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

namespace Nada.UI.View
{
    public partial class IndicatorControl : BaseControl
    {
        IndicatorEntityType entityType = IndicatorEntityType.DiseaseDistribution;
        public event Action OnAddRemove = () => { };
        private List<DynamicContainer> controlList = new List<DynamicContainer>();
        private List<IndicatorDropdownValue> dropdownKeys = new List<IndicatorDropdownValue>();

        public class DynamicContainer
        {
            public Indicator Indicator { get; set; }
            public delegate string GetValueDelegate();
            public GetValueDelegate GetValue { get; set; }
            public delegate bool IsValidDelegate();
            public IsValidDelegate IsValid { get; set; }
        }

        public IndicatorControl()
            : base()
        {
            InitializeComponent();
        }

        public void LoadIndicators(Dictionary<string, Indicator> indicators, List<IndicatorDropdownValue> keys, IndicatorEntityType eType)
        {
            Localizer.TranslateControl(this);
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
            IsValid();
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
                if (count % 2 == 0)
                {
                    labelRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    controlRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    columnCount = 0;
                }

                // Add label
                tblStaticIndicators.Controls.Add(CreateLabel(indicator, true), columnCount, labelRowIndex);

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
                tblIndicators.Controls.Add(CreateLabel(indicator, false), columnCount, labelRowIndex);

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

        private Control CreateLabel(Indicator indicator, bool isStatic)
        {
            var text = indicator.DisplayName;
            if (isStatic)
                text = TranslationLookup.GetValue(indicator.DisplayName, indicator.DisplayName);
            if (indicator.IsRequired)
            {
                var required = new H3Required
                {
                    Text = text,
                    Name = "ciLabel_" + indicator.Id,
                    AutoSize = true,
                    Anchor = (AnchorStyles.Bottom | AnchorStyles.Left),
                    TabStop = false
                };
                required.SetMaxWidth(370);
                return required;
            }
            else
            {
                var lbl = new H3bLabel
                    {
                        Text = text,
                        Name = "ciLabel_" + indicator.Id,
                        AutoSize = true,
                        Anchor = (AnchorStyles.Bottom | AnchorStyles.Left),
                        TabStop = false
                    };
                lbl.SetMaxWidth(370);
                return lbl;
            }
        }

        private Control CreateControl(Indicator indicator, string val)
        {
            List<IndicatorDropdownValue> availableValues = new List<IndicatorDropdownValue>();
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
                foreach (IndicatorDropdownValue v in dropdownKeys.Where(k => k.IndicatorId == indicator.Id))
                {
                    cntrl.Items.Add(v);
                    availableValues.Add(v);
                }
                cntrl.ValueMember = "IndicatorId";
                cntrl.DisplayMember = "DisplayName";
                if (!string.IsNullOrEmpty(val))
                {
                    var valItem = availableValues.FirstOrDefault(a => a.IndicatorId.ToString() == val);
                    cntrl.SelectedItem = valItem;
                }

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

                container.GetValue = () =>
                {
                    if (cntrl.SelectedItem == null)
                        return null;
                    return ((IndicatorDropdownValue)cntrl.SelectedItem).IndicatorId.ToString();
                };
                controlList.Add(container);

                if (indicator.CanAddValues)
                    return AddNewValLink(cntrl, indicator);
                else
                    return cntrl;
            }
            else if (indicator.DataTypeId == (int)IndicatorDataType.Multiselect)
            {
                var cntrl = new ListBox { Name = "dynamicMulti" + indicator.Id.ToString(), Width = 220, Height = 100, Margin = new Padding(0, 5, 20, 5), SelectionMode = SelectionMode.MultiExtended };
                foreach (var v in dropdownKeys.Where(k => k.IndicatorId == indicator.Id))
                {
                    cntrl.Items.Add(v);
                    availableValues.Add(v);
                }
                cntrl.ValueMember = "IndicatorId";
                cntrl.DisplayMember = "DisplayName";
                if (!string.IsNullOrEmpty(val))
                {
                    string[] vals = val.Split('|');
                    cntrl.ClearSelected();
                    foreach (var av in availableValues.Where(v => vals.Contains(v.IndicatorId.ToString())))
                        cntrl.SelectedItems.Add(av);
                }

                container.GetValue = () =>
                {
                    List<string> selected = new List<string>();
                    foreach (var i in cntrl.SelectedItems)
                        selected.Add((i as IndicatorDropdownValue).IndicatorId.ToString());
                    return string.Join("|", selected.ToArray());
                };

                container.IsValid = () =>
                {
                    if (indicator.IsRequired)
                    {
                        if (string.IsNullOrEmpty(container.GetValue()))
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

                controlList.Add(container);

                if (indicator.CanAddValues)
                    return AddNewValLink(cntrl, indicator);
                else
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

        private Control AddNewValLink(Control cntrl, Indicator indicator)
        {
            cntrl.Margin = new Padding(0, 5, 20, 0);
            TableLayoutPanel tblContainer = new TableLayoutPanel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, AutoScroll = true };
            tblContainer.RowStyles.Clear();
            tblContainer.ColumnStyles.Clear();
            int cRow = tblContainer.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            tblContainer.Controls.Add(cntrl, 0, cRow);
            int lRow = tblContainer.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            var lnk = new H3Link { Text = Translations.AddNewItemLink, Margin = new Padding(0, 0, 3, 5) };
            lnk.ClickOverride += () => 
            {
                IndicatorValueItemAdd form = new IndicatorValueItemAdd(new IndicatorDropdownValue { IndicatorId = indicator.Id, EntityType=entityType });
                form.OnSave += (v) =>
                    {
                        if (cntrl is ListBox)
                            (cntrl as ListBox).Items.Add(v);
                        else if (cntrl is ComboBox)
                            (cntrl as ComboBox).Items.Add(v);
                    };
                form.ShowDialog();
            };
            tblContainer.Controls.Add(lnk, 0, lRow);

            return tblContainer;
        }



        

    }
}
