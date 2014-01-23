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
using Nada.UI.Base;
using Nada.UI.View.Wizard;
using Nada.UI.View.Wizard.DistrictSplitting;
using Nada.Globalization;

namespace Nada.UI.View.Demography
{
    public partial class DashboardTabs : BaseControl
    {
        public Action<IView> LoadView = (i) => { };
        public Action<IView> LoadForm = (i) => { };
        public event Action<AdminLevel> OnSelect = (e) => { };
        public Action<string> StatusChanged = (e) => { };
        public Action<AdminLevel> ReloadView = (i) => { };
        private AdminLevel adminLevel;
        private AdminLevelType adminlevelType;
        private DemoRepository r = null;

        public DashboardTabs()
            : base()
        {
            InitializeComponent();
        }

        public DashboardTabs(AdminLevel adminLevel, AdminLevelType t)
            : base()
        {
            this.adminlevelType = t;
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
                LoadDash(adminLevel);
            }
        }

        private void LoadDash(AdminLevel adminLevel)
        {
            IFetchActivities fetcher = new ActivityFetcher(adminLevel);
            DiseaseDashboard dash = new DiseaseDashboard(fetcher, adminLevel, false);
            dash.ReloadView = (v) => { ReloadView(v); };
            dash.LoadView = (v) => { LoadView(v); };
            dash.LoadForm = (v) => { LoadForm(v); };
            dash.StatusChanged = (s) => { StatusChanged(s); };

            dash.LoadContent();
            dash.Dock = DockStyle.Fill;
            pnlLf.Controls.Add(dash);
        }

    }
}
