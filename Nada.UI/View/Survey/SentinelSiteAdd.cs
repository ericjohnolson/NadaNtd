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
using Nada.Model.Repositories;
using Nada.Model.Survey;
using Nada.UI.AppLogic;
using Nada.UI.Base;

namespace Nada.UI.View
{
    public partial class SentinelSiteAdd : BaseForm
    {
        public event Action<SentinelSite> OnSave = (e) => { };
        private SentinelSite model = new SentinelSite();

        public SentinelSiteAdd()
            : base()
        {
            InitializeComponent();
        }

        public SentinelSiteAdd(SentinelSite m)
            : base()
        {
            model = m;
            InitializeComponent();
        }

        public SentinelSiteAdd(List<AdminLevel> adminLevels)
            : base()
        {
            model = new SentinelSite { AdminLevels = adminLevels };
            InitializeComponent();
        }

        private void Modal_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                bsSentinelSite.DataSource = model;
                lblLastUpdated.Text += model.UpdatedBy;
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (!model.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }
            if (model.AdminLevels == null || model.AdminLevels.Count < 1)
            {
                MessageBox.Show(Translations.LocationRequired, Translations.ValidationErrorTitle);
                return;
            }

            bsSentinelSite.EndEdit();
            SurveyRepository r = new SurveyRepository();
            int userid = ApplicationData.Instance.GetUserId();
            if (model.Id > 0)
                model = r.Update(model, userid);
            else
                model = r.Insert(model, userid);

            OnSave(model);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
  
    }
}
