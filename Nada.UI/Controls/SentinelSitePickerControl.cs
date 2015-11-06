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
using Nada.UI.View.Survey;
using Nada.Model;

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
            // First hide all site type related controls
            pnlSentinel.Visible = false;
            pnlSpotCheckName.Visible = false;
            lblLat.Visible = false;
            lblLng.Visible = false;
            tbLat.Visible = false;
            tbLng.Visible = false;
        }

        public void LoadModel(SurveyBase m)
        {
            model = m;
            Localizer.TranslateControl(this);
            GetAndUpdateSites();
            bsSurvey.DataSource = model;
            SetupSiteTypeComboBox(m);
        }

        private void cbSiteType_SelectedIndexChanged(object sender, EventArgs e)
        {
            tblContainer.Visible = false;
            this.SuspendLayout();
            SwitchSiteType((SiteTypeId)cbSiteType.SelectedValue);

            this.ResumeLayout();
            tblContainer.Visible = true;
        }

        private void fieldLink1_OnClick()
        {
            if (model.AdminLevelId.HasValue)
            {
                SentinelSiteList list = new SentinelSiteList(model);
                list.OnSave += () => { GetAndUpdateSites(); };
                list.ShowDialog();
            }
        }

        public void EndEdit()
        {
            bsSurvey.EndEdit();
        }

        private void GetAndUpdateSites()
        {
            sites = r.GetSitesForAdminLevel(model.AdminLevels.Select(a => a.Id.ToString()));
            sentinelSiteBindingSource.DataSource = sites;
        }

        private void SwitchSiteType(SiteTypeId siteTypeId)
        {
            if (siteTypeId == SiteTypeId.Sentinel)
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
            else if (siteTypeId == SiteTypeId.SpotCheck)
            {
                pnlSentinel.Visible = false;
                pnlSpotCheckName.Visible = true;
                lblLat.Visible = true;
                lblLng.Visible = true;
                tbLat.Visible = true;
                tbLng.Visible = true;
                model.SentinelSiteId = null;
            }
        }

        private void SetupSiteTypeComboBox(SurveyBase m)
        {
            siteTypeBindingSource.DataSource = new Dictionary<SiteTypeId, string>()
            {
                {SiteTypeId.Sentinel, TranslationLookup.GetValue("Sentinel", "Sentinel")},
                {SiteTypeId.SpotCheck, TranslationLookup.GetValue("SpotCheck", "SpotCheck")}
            };
            cbSiteType.DisplayMember = "Value";
            cbSiteType.ValueMember = "Key";
            cbSiteType.SelectedValue = m.SiteTypeId;
            SwitchSiteType(m.SiteTypeId);
        }
    }
}
