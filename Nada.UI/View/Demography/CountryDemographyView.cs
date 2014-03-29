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
    public partial class CountryDemographyView : BaseControl
    {
        CountryDemography model = null;

        public CountryDemographyView()
            : base()
        {
            InitializeComponent();
        }

        private void CountryView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                h3Required3.SetMaxWidth(370);
                lblYear.SetMaxWidth(370);
            }
        }

        public void LoadDemo(CountryDemography country)
        {
            model = country;
            bsCountry.DataSource = model;
        }

        public CountryDemography GetDemo()
        {
            bsCountry.EndEdit();
            return model;
        }

        public void DoValidate()
        {
            errorProvider1.DataSource = bsCountry;
        }
    }
}
