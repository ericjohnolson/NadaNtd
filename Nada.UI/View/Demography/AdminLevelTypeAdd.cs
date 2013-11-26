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
    public partial class AdminLevelTypeAdd : BaseForm
    {
        public event Action OnSave = () => { };
        private AdminLevelType model = new AdminLevelType();

        public AdminLevelTypeAdd()
            : base()
        {
            InitializeComponent();
        }

        public AdminLevelTypeAdd(AdminLevelType m)
            : base()
        {
            model = m;
            InitializeComponent();
        }

        private void DistributionMethodAdd_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                bsAdminLevel.DataSource = model;
                lblLastUpdated.Text += model.UpdatedBy;
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!model.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }
            SettingsRepository r = new SettingsRepository();
            var adminLevels = r.GetAllAdminLevels();
            if (adminLevels.FirstOrDefault(a => a.DisplayName == model.DisplayName.Trim()) != null)
            {
                MessageBox.Show(Translations.AdminLevelNameUnique, Translations.ValidationErrorTitle);
                return;
            }

            bsAdminLevel.EndEdit();
            int userid = ApplicationData.Instance.GetUserId();
            r.Save(model, userid);
            OnSave();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
