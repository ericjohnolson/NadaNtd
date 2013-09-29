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

namespace Nada.UI.View.Demography
{
    public partial class DemographyView : UserControl
    {
        public Action<UserControl> LoadView = (i) => { };
        public Action<AdminLevel> LoadDashForAdminLevel = (i) => { };
        public Action<string> StatusChanged { get; set; }
        private DemographyTree treeView = null;
        private UserControl divisionView = null;
        private Dictionary<int, AdminLevelType> adminLevelTypes = null;
        private AdminLevel preloadedLevel = null;

        public DemographyView()
        {
            InitializeComponent();
        }

        public DemographyView(AdminLevel a)
        {
            InitializeComponent();
            preloadedLevel = a;
        }

        private void DemographyView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                LoadAdminLevelTypes();
                DemoRepository r = new DemoRepository();
                var tree = r.GetAdminLevelTree();

                treeView = new DemographyTree(tree);
                treeView.OnSelect += t_OnSelect;
                treeView.Dock = DockStyle.Fill;
                splitContainer.Panel1.Controls.Add(treeView);

                if (preloadedLevel != null)
                    t_OnSelect(preloadedLevel);
                else if (tree.Count > 0)
                    t_OnSelect(tree.FirstOrDefault());
            }
        }

        private void LoadAdminLevelTypes()
        {
            adminLevelTypes = new Dictionary<int, AdminLevelType>();
            SettingsRepository r = new SettingsRepository();
            List<AdminLevelType> types = r.GetAllAdminLevels();
            foreach (var t in types)
                adminLevelTypes.Add(t.LevelNumber, t);
        }

        void t_OnSelect(AdminLevel obj)
        {
            AdminLevelType adminLevelType = null;
            if (adminLevelTypes.ContainsKey(obj.LevelNumber + 1))
                adminLevelType = adminLevelTypes[obj.LevelNumber + 1];

            var view = new AdminLevelView((AdminLevel)obj, adminLevelType);
            view.StatusChanged = (s) => { StatusChanged(s); };
            view.ReloadView = (a) => { LoadDashForAdminLevel(a); };
            view.LoadView = (v) => { LoadView(v); };
            view.OnSelect += t_OnSelect;
            divisionView = view;

            divisionView.Dock = DockStyle.Fill;
            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(divisionView);
        }
    }
}
