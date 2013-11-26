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
using Nada.UI.Base;

namespace Nada.UI.View
{
    public partial class MedicineAdd : BaseForm
    {
        public event Action<Medicine> OnSave = (e) => { };
        private Medicine model = new Medicine();

        public MedicineAdd()
            : base()
        {
            InitializeComponent();
        }

        public MedicineAdd(Medicine m)
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
