using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Repositories;
using Nada.Model.Diseases;
using Nada.UI.AppLogic;

namespace Nada.UI.View.Demography
{
    public partial class AdminLevelView : UserControl
    {
        public Action<UserControl> LoadView = (i) => { };
        public event Action<AdminLevel> OnSelect = (e) => { };
        public Action<string> StatusChanged = (e) => { };
        public Action<AdminLevel> ReloadView = (i) => { };
        private AdminLevel adminLevel;
        private AdminLevelType childType;
        private DemoRepository r = null;

        public AdminLevelView()
        {
            InitializeComponent();
        }

        public AdminLevelView(AdminLevel adminLevel, AdminLevelType childType)
        {
            this.childType = childType;
            this.adminLevel = adminLevel;
            InitializeComponent();
        }

        private void AdminLevelView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                r = new DemoRepository();
                bsAdminLevel.DataSource = adminLevel;
                // Foreach type of disease load the dashboard, add tabs, 
                LoadDisease(DiseaseType.Lf, adminLevel);
            }
        }

        private void LoadDisease(DiseaseType diseaseType, AdminLevel adminLevel)
        {
            IFetchDiseaseActivities fetcher = DiseaseActivitiesFactory.GetForDisease(diseaseType, adminLevel);
            DiseaseDashboard dash = new DiseaseDashboard(fetcher, adminLevel);
            dash.ReloadView = (v) => { ReloadView(v); };
            dash.LoadView = (v) => { LoadView(v); };
            dash.StatusChanged = (s) => { StatusChanged(s); };

            dash.LoadContent();
            dash.Dock = DockStyle.Fill;
            pnlLf.Controls.Add(dash);
        }

    }
}
