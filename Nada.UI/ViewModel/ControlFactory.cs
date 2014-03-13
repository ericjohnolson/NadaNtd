using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Repositories;
using Nada.UI.Base;
using Nada.UI.Controls;
using Nada.UI.View;

namespace Nada.UI.ViewModel
{
    public class DynamicContainer
    {
        public Indicator Indicator { get; set; }
        public delegate string GetValueDelegate();
        public GetValueDelegate GetValue { get; set; }
        public delegate bool IsValidDelegate();
        public IsValidDelegate IsValid { get; set; }
    }

    public class ControlFactory
    {
        public static readonly int bottomPadding = 10;

        public static Control CreateLabel(Indicator indicator, bool isStatic)
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

        public static Control CreateDate(Indicator indicator, string val, ErrorProvider indicatorErrors, List<DynamicContainer> controlList)
        {
            var container = new DynamicContainer { Indicator = indicator };
            var cntrl = new NullableDatePickerControl
            {
                Name = "dynamicDt" + indicator.Id.ToString(),
                Margin = new Padding(0, 5, 10, bottomPadding),
                ShowClear = !indicator.IsRequired
            };
            container.IsValid = () =>
            {
                if (indicator.IsRequired && cntrl.GetValue() == DateTime.MinValue)
                {
                    indicatorErrors.SetError(cntrl, Translations.Required);
                    return false;
                }

                indicatorErrors.SetError(cntrl, "");
                return true;
            };
            cntrl.Validating += (s, e) => { container.IsValid(); };
            DateTime dt = new DateTime();
            if(DateTime.TryParseExact(val, "MM/dd/yyyy", 
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

        public static Control CreateNumber(Indicator indicator, string val, ErrorProvider indicatorErrors, List<DynamicContainer> controlList, IndicatorDataType type)
        {
            var container = new DynamicContainer { Indicator = indicator };
            var cntrl = new TextBox { Name = "dynamicNum" + indicator.Id.ToString(), Text = val, Width = 220, Margin = new Padding(0, 5, 10, bottomPadding) };
            container.IsValid = () =>
            {
                double d = 0;
                int i = 0;
                if (indicator.IsRequired && (cntrl.Text == "" || cntrl.Text == null))
                {
                    indicatorErrors.SetError(cntrl, Translations.Required);
                    return false;
                }
                else if (!string.IsNullOrEmpty(cntrl.Text) && type == IndicatorDataType.Number && !Double.TryParse(cntrl.Text, out d))
                {
                    indicatorErrors.SetError(cntrl, Translations.MustBeNumber);
                    return false;
                }
                else if (!string.IsNullOrEmpty(cntrl.Text) && type == IndicatorDataType.Year && (!int.TryParse(cntrl.Text, out i) || (i > 2100 || i < 1900)))
                {
                    indicatorErrors.SetError(cntrl, Translations.ValidYear);
                    return false;
                }

                indicatorErrors.SetError(cntrl, "");
                return true;
            };
            cntrl.Validating += (s, e) => { container.IsValid(); };
            container.GetValue = () => { return cntrl.Text; };
            controlList.Add(container);
            return cntrl;
        }

        public static Control CreateYesNo(Indicator indicator, string val, ErrorProvider indicatorErrors, List<DynamicContainer> controlList)
        {
            var container = new DynamicContainer { Indicator = indicator };
            var cntrl = new CheckBox { Name = "dynamicChk" + indicator.Id.ToString() };
            container.IsValid = () => { return true; };
            bool isChecked = false;
            if (Boolean.TryParse(val, out isChecked))
                cntrl.Checked = isChecked;

            container.GetValue = () => { return Convert.ToInt32(cntrl.Checked).ToString(); };
            controlList.Add(container);
            return cntrl;
        }

        public static Control CreateDropdown(Indicator indicator, string val, ErrorProvider indicatorErrors, List<DynamicContainer> controlList,
            IndicatorEntityType entityType, List<IndicatorDropdownValue> dropdownKeys)
        {
            List<IndicatorDropdownValue> availableValues = new List<IndicatorDropdownValue>();
            var container = new DynamicContainer { Indicator = indicator };
            var cntrl = new ComboBox { Name = "dynamicCombo" + indicator.Id.ToString(), Width = 220, Margin = new Padding(0, 5, 10, bottomPadding), DropDownStyle = ComboBoxStyle.DropDownList };

            if (!indicator.IsRequired)
                cntrl.Items.Add(new IndicatorDropdownValue { DisplayName = "", Id = -1 });

            foreach (IndicatorDropdownValue v in dropdownKeys.Where(k => k.IndicatorId == indicator.Id).OrderBy(i => i.SortOrder))
            {
                cntrl.Items.Add(v);
                availableValues.Add(v);
            }
            cntrl.ValueMember = "Id";
            cntrl.DisplayMember = "DisplayName";
            if (availableValues.Count > 0)
                cntrl.DropDownWidth = BaseForm.GetDropdownWidth(availableValues.Select(a => a.DisplayName));
            if (!string.IsNullOrEmpty(val))
            {
                var valItem = availableValues.FirstOrDefault(a => a.TranslationKey == val);
                cntrl.SelectedItem = valItem;
            }

            container.IsValid = () =>
            {
                if (indicator.IsRequired && indicatorErrors != null)
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
                if (cntrl.SelectedItem == null || ((IndicatorDropdownValue)cntrl.SelectedItem).Id == -1)
                    return null;
                return ((IndicatorDropdownValue)cntrl.SelectedItem).TranslationKey;
            };
            controlList.Add(container);

            if (indicator.CanAddValues)
                return AddNewValLink(cntrl, indicator, entityType);
            else
                return cntrl;
        }

        public static Control CreateMulti(Indicator indicator, string val, ErrorProvider indicatorErrors, List<DynamicContainer> controlList,
            IndicatorEntityType entityType, List<IndicatorDropdownValue> dropdownKeys)
        {
            List<IndicatorDropdownValue> availableValues = new List<IndicatorDropdownValue>();
            var container = new DynamicContainer { Indicator = indicator };
            var cntrl = new ListBox { Name = "dynamicMulti" + indicator.Id.ToString(), Width = 220, Height = 100, Margin = new Padding(0, 5, 20, bottomPadding), SelectionMode = SelectionMode.MultiExtended };
            foreach (var v in dropdownKeys.Where(k => k.IndicatorId == indicator.Id).OrderBy(i => i.SortOrder))
            {
                cntrl.Items.Add(v);
                availableValues.Add(v);
            }
            cntrl.ValueMember = "Id";
            cntrl.DisplayMember = "DisplayName";
            if (!string.IsNullOrEmpty(val))
            {
                string[] vals = val.Split('|');
                cntrl.ClearSelected();
                foreach (var av in availableValues.Where(v => vals.Contains(v.TranslationKey)))
                    cntrl.SelectedItems.Add(av);
            }

            container.GetValue = () =>
            {
                List<string> selected = new List<string>();
                foreach (var i in cntrl.SelectedItems)
                    selected.Add((i as IndicatorDropdownValue).TranslationKey);
                return string.Join("|", selected.ToArray());
            };

            container.IsValid = () =>
            {
                if (indicator.IsRequired && indicatorErrors != null)
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
                return AddNewValLink(cntrl, indicator, entityType);
            else
                return cntrl;
        }

        public static Control CreateDynamicNameVal(Indicator indicator, string val, ErrorProvider indicatorErrors, List<DynamicContainer> controlList,
           IndicatorEntityType entityType, List<IndicatorDropdownValue> values)
        {
            var container = new DynamicContainer { Indicator = indicator };
            var cntrl = new ComboBox { Name = "dynamicCombo" + indicator.Id.ToString(), Width = 220, Margin = new Padding(0, 5, 10, bottomPadding), DropDownStyle = ComboBoxStyle.DropDownList };
            foreach (IndicatorDropdownValue v in values)
                cntrl.Items.Add(v);
            cntrl.ValueMember = "Id";
            cntrl.DisplayMember = "DisplayName";
            if (values.Count > 0)
                cntrl.DropDownWidth = BaseForm.GetDropdownWidth(values.Select(a => a.DisplayName));
            if (!string.IsNullOrEmpty(val))
            {
                var valItem = values.FirstOrDefault(a => a.Id == Convert.ToInt32(val));
                cntrl.SelectedItem = valItem;
            }

            container.IsValid = () =>
            {
                if (indicator.IsRequired && indicatorErrors != null)
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
                return ((IndicatorDropdownValue)cntrl.SelectedItem).Id.ToString();
            };
            controlList.Add(container);

            if (indicator.CanAddValues)
                return AddNewValLink(cntrl, indicator, entityType);
            else
                return cntrl;
        }

        public static Control CreatePartners(Indicator indicator, string val, ErrorProvider indicatorErrors, List<DynamicContainer> controlList)
        {
            var container = new DynamicContainer { Indicator = indicator };
            var cntrl = new ListBox { Name = "dynamicPartners" + indicator.Id.ToString(), Width = 220, Height = 100, Margin = new Padding(0, 5, 20, bottomPadding), SelectionMode = SelectionMode.MultiExtended };

            List<Partner> partners = GetAndLoadPartners(cntrl);
            cntrl.ValueMember = "Id";
            cntrl.DisplayMember = "DisplayName";
            if (!string.IsNullOrEmpty(val))
            {
                string[] vals = val.Split('|');
                cntrl.ClearSelected();
                foreach (var av in partners.Where(v => vals.Contains(v.Id.ToString())))
                    cntrl.SelectedItems.Add(av);
            }

            container.GetValue = () =>
            {
                List<string> selected = new List<string>();
                foreach (var i in cntrl.SelectedItems)
                    selected.Add((i as Partner).Id.ToString());
                return string.Join("|", selected.ToArray());
            };

            container.IsValid = () =>
            {
                if (indicator.IsRequired && indicatorErrors != null)
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

            // Add table container and link
            controlList.Add(container);
            cntrl.Margin = new Padding(0, 5, 20, 0);
            TableLayoutPanel tblContainer = new TableLayoutPanel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, AutoScroll = true };
            tblContainer.RowStyles.Clear();
            tblContainer.ColumnStyles.Clear();
            int cRow = tblContainer.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            tblContainer.Controls.Add(cntrl, 0, cRow);
            int lRow = tblContainer.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            var lnk = new H3Link { Text = Translations.AddNewItemLink, Margin = new Padding(0, 0, 3, bottomPadding) };
            lnk.ClickOverride += () =>
            {
                PartnerList list = new PartnerList();
                list.OnSave += () =>
                {
                    partners = GetAndLoadPartners(cntrl);
                };
                list.ShowDialog();
            };
            tblContainer.Controls.Add(lnk, 0, lRow);

            return tblContainer;
        }

        public static List<Partner> GetAndLoadPartners(ListBox cntrl)
        {
            IntvRepository repo = new IntvRepository();
            cntrl.Items.Clear();
            var partners = repo.GetPartners();
            foreach (var v in partners)
            {
                cntrl.Items.Add(v);
            }
            return partners;
        }

        public static Control CreateText(Indicator indicator, string val, ErrorProvider indicatorErrors, List<DynamicContainer> controlList, int height, bool isMultiLine)
        {
            var container = new DynamicContainer { Indicator = indicator };
            var cntrl = new TextBox { Name = "dynamicTxt" + indicator.Id.ToString(), Text = val, Width = 220, Height = height, Multiline = isMultiLine, Margin = new Padding(0, 5, 10, bottomPadding) };
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

        private static Control AddNewValLink(Control cntrl, Indicator indicator, IndicatorEntityType entityType)
        {
            cntrl.Margin = new Padding(0, 5, 20, 0);
            TableLayoutPanel tblContainer = new TableLayoutPanel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, AutoScroll = true };
            tblContainer.RowStyles.Clear();
            tblContainer.ColumnStyles.Clear();
            int cRow = tblContainer.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            tblContainer.Controls.Add(cntrl, 0, cRow);
            int lRow = tblContainer.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            var lnk = new H3Link { Text = Translations.AddNewItemLink, Margin = new Padding(0, 5, 3, bottomPadding) };
            lnk.ClickOverride += () =>
            {
                IndicatorValueItemAdd form = new IndicatorValueItemAdd(new IndicatorDropdownValue { IndicatorId = indicator.Id, EntityType = entityType }, indicator);
                form.OnSave += (v) =>
                {
                    v.TranslationKey = v.DisplayName;
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
