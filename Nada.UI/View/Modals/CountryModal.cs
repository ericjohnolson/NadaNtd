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

namespace Nada.UI.View.Modals
{
    public partial class CountryModal : Form
    {
        private Country model = null;
        public event Action OnSave = () => { };

        public CountryModal()
        {
            InitializeComponent();
        }

        public CountryModal(Country c)
        {
            model = c;
            InitializeComponent();
        }

        private void CountryModal_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
                bsCountry.DataSource = model;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DemoRepository repo = new DemoRepository();
            repo.UpdateCountry(model, ApplicationData.Instance.GetUserId());
            OnSave();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
