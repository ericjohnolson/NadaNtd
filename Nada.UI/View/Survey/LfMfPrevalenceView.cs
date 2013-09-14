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
using Nada.Globalization;

namespace Nada.UI.View.Survey
{
    public partial class LfMfPrevalenceView : UserControl
    {
        public event Action<bool> OnSave = (b) => { };
        public event Action<string> StatusChanged = (s) => { };
        private LfMfPrevalence model = null;
        private SurveyRepository r = null;
        private DemoRepository demo = null;

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
                demo = new DemoRepository();
                vectorBindingSource.DataSource = r.GetVectors();
                if (model == null) model = r.CreateSurvey<LfMfPrevalence>(StaticSurveyType.LfPrevalence);
                bsSurvey.DataSource = model;
                customIndicatorControl1.LoadIndicators(model.TypeOfSurvey.Indicators.Cast<IDynamicIndicator>());
                customIndicatorControl1.OnAddRemove += customIndicatorControl1_OnAddRemove;
                fundersControl1.LoadItems(model.Partners);
                foreach (var vector in model.Vectors)
                    lbVectors.SelectedItem = vector;
                StatusChanged(Translations.LastUpdated + model.UpdatedBy);
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
                tbSiteName.Visible = false;
                model.AdminLevelId = null;
                model.SpotCheckName = null;
                model.Lat = null;
                model.Lng = null;
            }
            else if (cbSiteType.SelectedIndex == 1)
            {
                pnlSentinel.Visible = false;
                tbSiteName.Visible = true;
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
        
        private void sentinelSiteAdd_OnClick()
        {
            SentinelSiteAdd modal = new SentinelSiteAdd();
            if (model.AdminLevelId.HasValue && model.AdminLevelId > 0)
                modal = new SentinelSiteAdd(demo.GetAdminLevelById(model.AdminLevelId.Value));
            modal.OnSave += sites_OnAdd;
            modal.ShowDialog();
        }

        void sites_OnAdd(SentinelSite obj)
        {
            List<SentinelSite> sites = r.GetSitesForAdminLevel(obj.AdminLevel.Id);
            sites.Insert(0, new SentinelSite { SiteName = "Please Select", Id = -1 });
            sentinelSiteBindingSource.DataSource = sites;
            adminLevelPickerControl2.Select(obj.AdminLevel);
            model.AdminLevelId = obj.AdminLevel.Id; // TODO manage changing admin levels
            model.SentinelSiteId = obj.Id;
            bsSurvey.ResetBindings(false);
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
        private void save_Click(object sender, EventArgs e)
        {
            if (!model.IsValid())
            {
                MessageBox.Show(Translations.ValidationError);
                return;
            }

            bsSurvey.EndEdit();
            model.Partners = fundersControl1.GetSelected();
            model.Vectors = new List<Vector>();
            foreach (var vector in lbVectors.SelectedItems)
                model.Vectors.Add(vector as Vector);
            //When getting the value back out, use something like the following:
            if (model.SentinelSiteId == -1) model.SentinelSiteId = null;
            model.CustomIndicatorValues = customIndicatorControl1.GetValues<IndicatorValue>();
            int userId = ApplicationData.Instance.GetUserId();
                r.Save(model, userId);
            MessageBox.Show("Survey was saved!");
            OnSave(false);
        }

        void customIndicatorControl1_OnAddRemove()
        {
            SurveyTypeEdit editor = new SurveyTypeEdit(model.TypeOfSurvey);
            editor.OnSave += editType_OnSave;
            editor.ShowDialog();
        }

        void editType_OnSave()
        {
            customIndicatorControl1.LoadIndicators(model.TypeOfSurvey.Indicators.Cast<IDynamicIndicator>());
        }
        
        private void cancel_Click(object sender, EventArgs e)
        {
            OnSave(false);
        }

    }
}
