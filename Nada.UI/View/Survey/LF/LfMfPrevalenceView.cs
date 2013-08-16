using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Survey;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.Model;

namespace Nada.UI.View.Survey
{
    public partial class LfMfPrevalenceView : UserControl
    {
        public event Action<bool> OnSave = (b) => { };
        private LfMfPrevalence model = null;
        private SurveyRepository r = null;

        public LfMfPrevalenceView()
        {
            InitializeComponent();
        }

        public LfMfPrevalenceView(LfMfPrevalence s)
        {
            this.model = s;
            InitializeComponent();
        }

        private void LfPrevalence_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                adminLevelPickerControl1.OnSelect += adminLevelPickerControl1_OnSelect;
                adminLevelPickerControl2.OnSelect += adminLevelPickerControl2_OnSelect;
                r = new SurveyRepository();
                if (model == null) model = r.CreateSurvey<LfMfPrevalence>(StaticSurveyType.LfPrevalence);
                bsSurvey.DataSource = model;
                bsType.DataSource = model.TypeOfSurvey;
                customIndicatorControl1.LoadIndicators(model.TypeOfSurvey.Indicators.Cast<IDynamicIndicator>());
            }
        }

        void adminLevelPickerControl1_OnSelect(Model.AdminLevel obj)
        {
            model.AdminLevelId = obj.Id;
        }

        private void cbSiteType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSiteType.SelectedIndex == 0)
            {
                pnlSentinel.Visible = true;
                pnlSpotCheck.Visible = false;
                model.AdminLevelId = null;
                model.SpotCheckName = null;
                model.Lat = null;
                model.Lng = null;
            }
            else if (cbSiteType.SelectedIndex == 1)
            {
                pnlSentinel.Visible = false;
                pnlSpotCheck.Visible = true;
                model.SentinelSiteId = null;
            }
        }

        void adminLevelPickerControl2_OnSelect(AdminLevel obj)
        {
            model.AdminLevelId = obj.Id;
            List<SentinelSite> sites = r.GetSitesForAdminLevel(obj.Id);
            sites.Insert(0, new SentinelSite { SiteName = "Please Select", Id = -1 });
            sentinelSiteBindingSource.DataSource = sites;
        }

        private void lnkAddSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SentinelSiteAdd modal = new SentinelSiteAdd();
            modal.OnSave += sites_OnAdd;
            modal.ShowDialog();
        }

        void sites_OnAdd(SentinelSite obj)
        {
            List<SentinelSite> sites = r.GetSitesForAdminLevel(obj.Id);
            sites.Insert(0, new SentinelSite { SiteName = "Please Select", Id = -1 });
            sentinelSiteBindingSource.DataSource = sites;
            adminLevelPickerControl2.Select(obj.AdminLevel);
            model.AdminLevelId = obj.AdminLevel.Id; // TODO manage changing admin levels
            model.SentinelSiteId = obj.Id;
        }

        private void UpdatePercentage()
        {
            int p, x;
            if (int.TryParse(tbExamined.Text, out x) && int.TryParse(tbPositive.Text, out p))
                tbPercentPositive.Text = Math.Round(p / Convert.ToDouble(x) * 100.0, 2).ToString();
        }

        private void tbExamined_Validated(object sender, EventArgs e)
        {
            UpdatePercentage();
        }

        private void tbPositive_Validated(object sender, EventArgs e)
        {
            UpdatePercentage();
        }

        /// <summary>
        /// SAVE Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            bsSurvey.EndEdit();
            if (model.SentinelSiteId == -1) model.SentinelSiteId = null;
            model.CustomIndicatorValues = customIndicatorControl1.GetValues<SurveyIndicatorValue>();
            int userId = ApplicationData.Instance.GetUserId();
            if (model.Id > 0)
                r.Update(model, userId);
            else
                r.Insert(model, userId);
            MessageBox.Show("Survey was saved!");
            OnSave(false);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SurveyTypeEdit editor = new SurveyTypeEdit(model.TypeOfSurvey);
            editor.OnSave += editor_OnSave;
            editor.ShowDialog();
        }

        void editor_OnSave()
        {
            customIndicatorControl1.LoadIndicators(model.TypeOfSurvey.Indicators.Cast<IDynamicIndicator>());
            bsType.ResetBindings(false);
        }
    }
}
