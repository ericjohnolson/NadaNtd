using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Globalization;
using Nada.UI.Base;

namespace Nada.UI.View.Demography
{
    public partial class CountryView : BaseControl
    {
        Country model = null;

        public CountryView()
            : base()
        {
            InitializeComponent();
        }

        private void CountryView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                var months = GlobalizationUtil.GetAllMonths(); 
                monthItemBindingSource.DataSource = months;
                lbl1.SetMaxWidth(370);
                lbl2.SetMaxWidth(370);

                cbMonths.DropDownWidth = BaseForm.GetDropdownWidth(months.Select(m => m.Name));
            }
        }

        public void LoadCountry(Country country)
        {
            model = country;
            bsCountry.DataSource = model;
        }

        public Country GetCountry()
        {
            bsCountry.EndEdit();
            return model;
        }
    }
}
