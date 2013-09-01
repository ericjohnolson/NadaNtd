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
using Nada.Model.Survey;
using Nada.UI.AppLogic;

namespace Nada.UI.View
{
    public partial class SentinelSiteAdd : Form
    {
        public event Action<SentinelSite> OnSave = (e) => { };
        private SentinelSite model = new SentinelSite();

        public SentinelSiteAdd()
        {
            InitializeComponent();
        }

        public SentinelSiteAdd(SentinelSite m)
        {
            model = m;
            InitializeComponent();
        }

        public SentinelSiteAdd(AdminLevel adminLevel)
        {
            model = new SentinelSite { AdminLevel = adminLevel };
            InitializeComponent();
        }

        private void Modal_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {

                adminLevelPickerControl1.OnSelect += adminLevelPickerControl1_OnSelect;
                adminLevelPickerControl1.Select(model.AdminLevel);
                bsSentinelSite.DataSource = model;
                lblLastUpdated.Text += model.UpdatedBy;
            }
        }

        void adminLevelPickerControl1_OnSelect(AdminLevel obj)
        {
            model.AdminLevel = obj;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
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
