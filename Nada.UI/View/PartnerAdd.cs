using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;

namespace Nada.UI.View
{
    public partial class PartnerAdd : Form
    {
        public event Action<Partner> OnSave = (e) => { };
        private Partner model = new Partner();

        public PartnerAdd()
        {
            InitializeComponent();
        }

        public PartnerAdd(Partner m)
        {
            model = m;
            InitializeComponent();
        }

        private void DistributionMethodAdd_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                bsDistributionMethod.DataSource = model;
                lblLastUpdated.Text += model.UpdatedBy;
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            bsDistributionMethod.EndEdit();
            IntvRepository r = new IntvRepository();
            int userid = ApplicationData.Instance.GetUserId();
            r.Save(model, userid);
            OnSave(model);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
