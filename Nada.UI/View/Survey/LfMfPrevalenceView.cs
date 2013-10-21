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
using Nada.UI.View.Help;

namespace Nada.UI.View.Survey
{
    public partial class LfMfPrevalenceView : UserControl, IView
    {
        private AdminLevel adminLevel = null;
        private LfMfPrevalence model = null;
        private SurveyRepository r = null;
        private DemoRepository demo = null;
        private List<Vector> vectors = null;
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }
        public string Title { get { return lblTitle.Text; } }

        public LfMfPrevalenceView()
        {
            InitializeComponent();
        }

        public LfMfPrevalenceView(AdminLevel a)
        {
            adminLevel = a;
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
                Localizer.TranslateControl(this);

                adminLevelPickerControl1.Focus();
                adminLevelPickerControl1.OnSelect += adminLevelPickerControl1_OnSelect;
                r = new SurveyRepository();
                demo = new DemoRepository();
                if (model == null) 
                {
                    model = r.CreateSurvey<LfMfPrevalence>(StaticSurveyType.LfPrevalence);
                    adminLevelPickerControl1.Select(adminLevel);
                    model.AdminLevelId = adminLevel.Id;
                }
                else
                    adminLevelPickerControl1.Select(model.AdminLevelId.Value);

                LoadDropdownKeys(model);
                List<SentinelSite> sites = r.GetSitesForAdminLevel(model.AdminLevelId.Value);
                sites.Insert(0, new SentinelSite { SiteName = Translations.PleaseSelect, Id = -1 });
                sentinelSiteBindingSource.DataSource = sites;

                bsSurvey.DataSource = model;
                if (model.TypeOfSurvey.Indicators != null && model.TypeOfSurvey.Indicators.Count() > 0)
                    customIndicatorControl1.LoadIndicators(model.TypeOfSurvey.Indicators, model.IndicatorValues);

                customIndicatorControl1.OnAddRemove += customIndicatorControl1_OnAddRemove;
                fundersControl1.LoadItems(model.Partners);

                vectors = r.GetVectors();
                vectorBindingSource.DataSource = vectors;
                lbVectors.ClearSelected();
                foreach (var vector in vectors.Where(v => model.Vectors.Select(i => i.Id).Contains(v.Id)))
                    lbVectors.SelectedItems.Add(vector);
                StatusChanged(model.UpdatedBy);
            }
        }

        private void LoadDropdownKeys(LfMfPrevalence model)
        {
            cbTiming.Items.Clear();
            foreach(string key in model.TimingTypeValues)
                cbTiming.Items.Add(TranslationLookup.GetValue(key, key));

            cbTestType.Items.Clear();
            foreach (string key in model.TestTypeValues)
                cbTestType.Items.Add(TranslationLookup.GetValue(key, key));

            cbCasualAgent.Items.Clear();
            foreach (string key in model.CasualAgentValues)
                cbCasualAgent.Items.Add(TranslationLookup.GetValue(key, key));
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
            sites.Insert(0, new SentinelSite { SiteName = Translations.PleaseSelect, Id = -1 });
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
                model.PercentPositive = Math.Round(p / Convert.ToDouble(x) * 100.0, 2);
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
            if (!model.IsValid() || !customIndicatorControl1.IsValid())
            {
                MessageBox.Show(Translations.ValidationError);
                return;
            }
            if (!model.AdminLevelId.HasValue || model.AdminLevelId.Value < 1)
            {
                MessageBox.Show(Translations.LocationRequired);
                return;
            }

            bsSurvey.EndEdit();
            model.Partners = fundersControl1.GetSelected();
            model.Vectors = new List<Vector>();
            foreach (var vector in lbVectors.SelectedItems)
                model.Vectors.Add(vector as Vector);
            //When getting the value back out, use something like the following:
            if (model.SentinelSiteId == -1) model.SentinelSiteId = null;
            model.IndicatorValues = customIndicatorControl1.GetValues();
            model.MapPropertiesToIndicators();
            int userId = ApplicationData.Instance.GetUserId();
                r.Save(model, userId);
            OnClose();
        }

        void customIndicatorControl1_OnAddRemove()
        {
            SurveyTypeEdit editor = new SurveyTypeEdit(model.TypeOfSurvey);
            editor.OnSave += editType_OnSave;
            ViewForm form = new ViewForm(editor);
            form.ShowDialog();
        }

        void editType_OnSave()
        {
            customIndicatorControl1.LoadIndicators(model.TypeOfSurvey.Indicators);
        }
        
        private void cancel_Click(object sender, EventArgs e)
        {
            OnClose();
        }

        private void btnDash_Click(object sender, EventArgs e)
        {
            OnClose();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            HelpView help = new HelpView();
            help.Show();
        }
    }
}
