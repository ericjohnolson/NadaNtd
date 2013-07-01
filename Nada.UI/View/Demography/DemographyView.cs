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
        private DemographyTree treeView = null;
        private UserControl divisionView = null;
        private Dictionary<int, AdminLevelType> adminLevelTypes = null;

        public DemographyView()
        {
            InitializeComponent();
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
                if(tree.Count > 0)
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
            if(adminLevelTypes.ContainsKey(obj.LevelNumber + 1))
                adminLevelType = adminLevelTypes[obj.LevelNumber + 1];

            if (obj.ParentId.HasValue && obj.ParentId.Value > 0)
            {
                var view = new AdminLevelView((AdminLevel)obj, adminLevelType);
                view.OnSelect += t_OnSelect;
                divisionView = view;
            }
            else
            {
                var view = new CountryView((AdminLevel)obj, adminLevelType);
                view.OnSelect += t_OnSelect;
                divisionView = view;
            }

            divisionView.Dock = DockStyle.Fill;
            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(divisionView);
        }
    }
}
