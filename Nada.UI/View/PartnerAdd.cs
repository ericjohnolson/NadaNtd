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
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;

namespace Nada.UI.View
{
    public partial class PartnerAdd : BaseForm
    {
        public event Action<Partner> OnSave = (e) => { };
        private Partner model = new Partner();
        private List<Partner> existing = null;

        public PartnerAdd(List<Partner> p)
            : base()
        {
            existing = p;
            InitializeComponent();
        }

        public PartnerAdd(List<Partner> p, Partner m)
            : base()
        {
            existing = p;
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
            if (!model.IsValid())
            {
                errorProvider1.DataSource = bsDistributionMethod;
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }

            if (existing.FirstOrDefault(i => i.DisplayName.ToLower() == model.DisplayName.ToLower() && model != i) != null)
            {
                MessageBox.Show(string.Format(Translations.ValidationMustBeUnique, Translations.Name), Translations.ValidationErrorTitle);
                return;
            }

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
