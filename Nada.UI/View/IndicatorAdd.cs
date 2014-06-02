using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Repositories;
using Nada.Model.Survey;
using Nada.UI.AppLogic;
using Nada.UI.Base;
using Nada.UI.ViewModel;

namespace Nada.UI.View
{
    public partial class IndicatorAdd : BaseForm
    {
        public event Action<Indicator> OnSave = (e) => { };
        private Indicator model = new Indicator();
        private IEnumerable<Indicator> existingIndicators = null;

        public IndicatorAdd(IEnumerable<Indicator> vm) 
            : base()
        {
            existingIndicators = vm;
            InitializeComponent();
        }

        public IndicatorAdd(IEnumerable<Indicator> vm, Indicator m)
            : base()
        {
            existingIndicators = vm;
            model = Util.DeepClone(m);
            InitializeComponent();
        }

        private void Modal_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                CreateDataTypeDropdown(comboBox1);
                bsIndicator.DataSource = model;
            }
        }

        private void CreateDataTypeDropdown(ComboBox comboBox)
        {
            List<IndicatorDropdownValue> vals = new List<IndicatorDropdownValue>();
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.Text, Id = 1 });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.Number, Id = 2 });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.Checkbox, Id = 3 });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.Date, Id = 4 });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.Year, Id = 7 });
            vals.Add(new IndicatorDropdownValue { DisplayName = Translations.Month, Id = 8 });
            comboBox.DataSource = vals;
            comboBox.SelectedItem = vals[0];
            comboBox.DropDownWidth = BaseForm.GetDropdownWidth(vals.Select(a => a.DisplayName));
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            model.DataTypeId = Convert.ToInt32(comboBox1.SelectedValue);
            model.IsEdited = true;
            model.IsEditable = true;
            if (!model.IsValid())
            {
                errorProvider1.DataSource = bsIndicator;
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }
            if (existingIndicators.FirstOrDefault(i => TranslationLookup.GetValue(i.DisplayName, i.DisplayName).ToLower() == model.DisplayName.ToLower() && (model.Id <= 0 || model.Id != i.Id)) != null)
            {
                MessageBox.Show(string.Format(Translations.ValidationMustBeUnique, Translations.Name), Translations.ValidationErrorTitle);
                return;
            }
            bsIndicator.EndEdit();
            OnSave(model);
            this.Close();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
  
    }
}
