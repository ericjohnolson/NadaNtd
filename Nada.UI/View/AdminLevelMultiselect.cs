using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Repositories;
using Nada.Model;

namespace Nada.UI.View
{
    public partial class AdminLevelMultiselect : UserControl
    {
        private DemoRepository demography = null;
        private SettingsRepository settings = null;
        private List<AdminLevel> available = new List<AdminLevel>();
        private List<AdminLevel> selected = new List<AdminLevel>();

        public AdminLevelMultiselect()
        {
            InitializeComponent();
        }

        private void AdminLevelMultiselect_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                demography = new DemoRepository();
                settings = new SettingsRepository();
                bsLevels.DataSource = settings.GetAllAdminLevels();
                cbLevels.SelectedIndex = 0;
                LoadTrees();
                treeAvailable.CanExpandGetter = model => ((AdminLevel)model).
                                                              Children.Count > 0;
                treeAvailable.ChildrenGetter = delegate(object model)
                {
                    return ((AdminLevel)model).
                            Children;
                };
                treeSelected.CanExpandGetter = model => ((AdminLevel)model).
                                                              Children.Count > 0;
                treeSelected.ChildrenGetter = delegate(object model)
                {
                    return ((AdminLevel)model).
                            Children;
                };
            }
        }

        public List<AdminLevel> GetSelectedAdminLevels()
        {
            List<AdminLevel> list = new List<AdminLevel>();
            foreach (AdminLevel a in selected)
            {
                AddNodeRecursive(a, list);
            }
                return list;
        }

        private void AddNodeRecursive(AdminLevel adminLevel, List<AdminLevel> list)
        {
            if(adminLevel.LevelNumber == ((AdminLevelType)cbLevels.SelectedItem).LevelNumber)
                list.Add(adminLevel);
            foreach (AdminLevel child in adminLevel.Children)
            {
                AddNodeRecursive(child, list);
            }
        }

        private void cbLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTrees();
        }

        private void LoadTrees()
        {
            var level = (AdminLevelType)cbLevels.SelectedItem;
            available = demography.GetAdminLevelTree(level.Id);
            selected = new List<AdminLevel>();
            ReloadLists();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (var item in available)
                selected.Add(item);
            available.Clear();
            ReloadLists();
        }

        private void treeAvailable_DoubleClick(object sender, EventArgs e)
        {
            SelectItems();
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectItems();
        }
        private void SelectItems()
        {
            foreach (var item in treeAvailable.SelectedObjects)
            {
                selected.Add((AdminLevel)item);
                available.Remove((AdminLevel)item);
            }
            ReloadLists();
        }

        private void btnDeselect_Click(object sender, EventArgs e)
        {
            DeselectItems();
        }

        private void treeSelected_DoubleClick(object sender, EventArgs e)
        {
            DeselectItems();
        }

        private void DeselectItems()
        {
            foreach (var item in treeSelected.SelectedObjects)
            {
                selected.Remove((AdminLevel)item);
                available.Add((AdminLevel)item);
            }
            ReloadLists();
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            foreach (var item in selected)
                available.Add(item);
            selected.Clear();
            ReloadLists();
        }

        private void ReloadLists()
        {
            treeAvailable.SetObjects(available.OrderBy(i => i.Name));
            treeSelected.SetObjects(selected.OrderBy(i => i.Name));
        }

    }
}
