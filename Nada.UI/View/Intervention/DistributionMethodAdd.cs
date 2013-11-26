using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;

namespace Nada.UI.View.Intervention
{
    public partial class DistributionMethodAdd : BaseForm
    { 
        public event Action<DistributionMethod> OnSave = (e) => { };
        private DistributionMethod model = new DistributionMethod();

        public DistributionMethodAdd()
            : base()
        {
            InitializeComponent();
        }

        public DistributionMethodAdd(DistributionMethod m)
            : base()
        {
            model = m;
            InitializeComponent();
        }

        private void DistributionMethodAdd_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
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
