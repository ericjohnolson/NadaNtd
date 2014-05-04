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
using Nada.UI.View.Survey;
using Nada.Model.Intervention;

using Nada.Model.Diseases;
using Nada.UI.Base;
using System.Web.Security;
using System.IO;
using System.Configuration;

namespace Nada.UI.View.Demography
{
    public partial class DemographyView : BaseControl, IView
    {
        private AdminLevel adminLevel = null;
        private AdminLevel adminLevelBoundToGlobalTree = null;
        private AdminLevelDemography model = null;
        private DemoRepository demo = null;
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }
        public string Title { get { return adminLevel.Name + " " + Translations.Demography; } }
        private DateTime originalDate = DateTime.MinValue;
        public void SetFocus()
        {
            tbYearCensus.Focus();
        }

        public DemographyView()
            : base()
        {
            InitializeComponent();
        }

        public DemographyView(AdminLevel a)
            : base()
        {
            adminLevel = a;
            InitializeComponent();
        }

        public DemographyView(AdminLevelDemography d, AdminLevel a)
            : base()
        {
            adminLevel = a;
            this.model = d;
            InitializeComponent();
        }

        private void Demo_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                adminLevelBoundToGlobalTree = adminLevel;
                adminLevel = Util.DeepClone<AdminLevel>(adminLevel);

                Localizer.TranslateControl(this);
                demo = new DemoRepository();
                if (model == null)
                {
                    model = new AdminLevelDemography();
                    model.AdminLevelId = adminLevel.Id;
                }
                else
                    originalDate = model.DateDemographyData;

                lblType.Text = Translations.Demography;
                   
                StatusChanged(model.UpdatedBy);

                if (!Roles.IsUserInRole(ApplicationData.Instance.CurrentUser.UserName, "RoleDataEnterer") &&
                !Roles.IsUserInRole(ApplicationData.Instance.CurrentUser.UserName, "RoleAdmin"))
                {
                    lnkEditAdminLevelUnit.Visible = false;
                    tblEdit.Visible = false;
                }

                bsDemo.DataSource = model;

                BindAdminLevel();
            }
        }

        private void BindAdminLevel()
        {
            lblAdminLevel.Text = adminLevel.Name;
            if (adminLevel.LatWho.HasValue)
                lblLat.Text = adminLevel.LatWho.ToString();
            else
                lblLat.Text = Translations.NA;
            if(adminLevel.LngWho.HasValue)
                lblLng.Text = adminLevel.LngWho.ToString();
            else
                lblLng.Text = Translations.NA;
            lblLat.MakeBold();
            lblLng.MakeBold();
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
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }

            bsDemo.EndEdit();
            
            int userId = ApplicationData.Instance.GetUserId();
            demo.Save(model, userId);
            SettingsRepository settings = new SettingsRepository();
            var type = settings.GetAdminLevelTypeByLevel(adminLevel.LevelNumber);
            var country = demo.GetCountry();

            if (type.IsAggregatingLevel)
            {
                demo.AggregateUp(type, model.DateDemographyData, userId, null);
                if(Util.GetYearReported(country.ReportingYearStartDate.Month, originalDate) != Util.GetYearReported(country.ReportingYearStartDate.Month, model.DateDemographyData))
                    demo.AggregateUp(type, originalDate, userId, null);

            }

            OnClose();
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
            Help.ShowHelp(this, "file:///" + Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["HelpFile"]);
            //HelpView help = new HelpView();
            //help.Show();
        }

        private void h3Link1_ClickOverride()
        {
            AdminLevelAdd al = new AdminLevelAdd(adminLevel);
            al.OnSave += al_OnSave;
            al.ShowDialog();

        }

        void al_OnSave()
        {
            BindAdminLevel();
            adminLevelBoundToGlobalTree.Name = adminLevel.Name;
            adminLevelBoundToGlobalTree.LatWho = adminLevel.LatWho;
            adminLevelBoundToGlobalTree.LngWho = adminLevel.LngWho;
            
        }
    }
}
