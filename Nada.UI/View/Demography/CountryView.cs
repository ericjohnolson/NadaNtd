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

namespace Nada.UI.View.Demography
{
    public partial class CountryView : UserControl
    {
        Country model = null;

        public CountryView()
        {
            InitializeComponent();
        }

        private void CountryView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                monthItemBindingSource.DataSource = GlobalizationUtil.GetAllMonths();
                lbl1.SetMaxWidth(370);
                lbl2.SetMaxWidth(370);
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
