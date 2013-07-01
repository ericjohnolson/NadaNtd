using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;

namespace Nada.UI.View.Demography
{
    public partial class CountryDemoEdit : Form
    {
        private CountryDemography model = null;
        public event Action<CountryDemography> OnSave = (e) => { };

        public CountryDemoEdit()
        {
            InitializeComponent();
        }

        public CountryDemoEdit(CountryDemography m)
        {
            model = m;
            InitializeComponent();
        }

        private void CountryDemoEdit_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                bsCountryDemo.DataSource = model;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bsCountryDemo.EndEdit();
            DemoRepository r = new DemoRepository();
            if (model.Id > 0)
                r.UpdateCountryDemography(model, ApplicationData.Instance.GetUserId());
            else
                r.InsertCountryDemography(model, ApplicationData.Instance.GetUserId());
            OnSave(model);
            this.Close();
        }


    }
}
