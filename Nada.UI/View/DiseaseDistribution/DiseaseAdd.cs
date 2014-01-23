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
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;

namespace Nada.UI.View
{
    public partial class DiseaseAdd : BaseForm
    {
        public event Action<Disease> OnSave = (e) => { };
        private Disease model = new Disease { DiseaseType = "CM" };

        public DiseaseAdd()
            : base()
        {
            InitializeComponent();
        }
        
        private void DistributionMethodAdd_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                bindingSource1.DataSource = model;
                lblLastUpdated.Text =  model.UpdatedBy;
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!model.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }
            model.UserDefinedName = model.DisplayName;
            bindingSource1.EndEdit();
            DiseaseRepository r = new DiseaseRepository();
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
