using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Base;
using Nada.Model.Survey;
using Nada.Globalization;
using Nada.UI.AppLogic;
using Nada.Model.Repositories;
using Nada.UI.View;

namespace Nada.UI.Controls
{
    public partial class SentinelSitePickerControl : UserControl
    {
        private DemoRepository demo = null;
        private SurveyRepository r = null;
        private SurveyBase model = null;
        List<SentinelSite> sites = null;
        public SentinelSitePickerControl()
        {
            r = new SurveyRepository();
            demo = new DemoRepository();
            InitializeComponent();
        }

        private void SentinelSitePickerControl_Load(object sender, EventArgs e)
        {

        }

        public void LoadModel(SurveyBase m)
        {
            model = m;
            Localizer.TranslateControl(this);
            sites = r.GetSitesForAdminLevel(model.AdminLevels.Select(a => a.Id.ToString()));
            sentinelSiteBindingSource.DataSource = sites;
            bsSurvey.DataSource = model;

        }

        private void cbSiteType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SuspendLayout();
            if (cbSiteType.SelectedIndex == 0)
            {
                pnlSentinel.Visible = true;
                pnlSpotCheckName.Visible = false;
                lblLat.Visible = false;
                lblLng.Visible = false;
                tbLat.Visible = false;
                tbLng.Visible = false;
                model.SpotCheckName = null;
                model.Lat = null;
                model.Lng = null;
            }
            else if (cbSiteType.SelectedIndex == 1)
            {
                pnlSentinel.Visible = false;
                pnlSpotCheckName.Visible = true;
                lblLat.Visible = true;
                lblLng.Visible = true;
                tbLat.Visible = true;
                tbLng.Visible = true;
                model.SentinelSiteId = null;
            }
            this.ResumeLayout();
        }

        void sites_OnAdd(SentinelSite obj)
        {
            sites.Add(obj);
            sites = sites.OrderBy(s => s.SiteName).ToList();
            sentinelSiteBindingSource.DataSource = sites;
            model.SentinelSiteId = obj.Id;
            bsSurvey.ResetBindings(false);
        }

        private void fieldLink1_OnClick()
        {
            SentinelSiteAdd modal = new SentinelSiteAdd(model.AdminLevels);
            modal.OnSave += sites_OnAdd;
            modal.ShowDialog();
        }

        public void EndEdit()
        {
            bsSurvey.EndEdit();
        }
    }
}
