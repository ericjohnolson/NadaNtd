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
using Nada.UI.Base;
using Nada.UI.AppLogic;

namespace Nada.UI.View
{
    public partial class AdminLevelMultiselectAny : BaseControl
    {
        private DemoRepository demography = null;
        private SettingsRepository settings = null;
        private List<AdminLevel> available = new List<AdminLevel>();
        public List<AdminLevel> Selected = new List<AdminLevel>();


        public AdminLevelMultiselectAny()
            : base()
        {
            InitializeComponent();
        }

        public List<AdminLevel> GetSelected()
        {
            return Selected;
        }

        public void SetSelected(List<AdminLevel> list)
        {
            foreach (var item in list)
                SelectItem(item);
            ReloadLists();
        }

        private void AdminLevelMultiselect_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                
                demography = new DemoRepository();
                settings = new SettingsRepository();
                var levels = settings.GetAllAdminLevels();
                bsLevels.DataSource = levels;
                cbLevels.SelectedIndex = 0;
                LoadAvailable();
                treeAvailable.CanExpandGetter = model => ((AdminLevel)model).
                                                              Children.Count > 0;
                treeAvailable.ChildrenGetter = delegate(object model)
                {
                    return ((AdminLevel)model).
                            Children;
                };
                cbLevels.DropDownWidth = BaseForm.GetDropdownWidth(levels.Select(a => a.DisplayName));

            }
        }
                
        private void cbLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAvailable();
        }

        private void LoadAvailable()
        {
            if (cbLevels.SelectedItem == null)
                return;
            var level = (AdminLevelType)cbLevels.SelectedItem;
            available = demography.GetAdminLevelTree(level.Id);
            treeAvailable.SetObjects(available.OrderBy(i => i.Name).ToList());
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            List<AdminLevel> children = new List<AdminLevel>();
            foreach (var item in available)
                GetChildren(children, item);
            foreach (var item in children)
                SelectItem(item);
            ReloadLists();
        }

        private void SelectItem(AdminLevel item)
        {
            if(Selected.FirstOrDefault(a => a.Id == item.Id) == null)
                Selected.Add(item);
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
            List<AdminLevel> children = new List<AdminLevel>();
            foreach (var item in treeAvailable.SelectedObjects.Cast<AdminLevel>())
                GetChildren(children, item);
            foreach (var item in children)
                SelectItem(item);
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
            foreach (var item in lvSelected.SelectedObjects.Cast<AdminLevel>())
            {
                DeleteNode(item, Selected);
            }
            ReloadLists();
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            Selected.Clear();
            ReloadLists();
        }

        private void ReloadLists()
        {
            treeAvailable.ClearObjects();
            treeAvailable.SetObjects(available.OrderBy(i => i.Name).ToList());
            lvSelected.ClearObjects();
            lvSelected.SetObjects(Selected.OrderBy(i => i.Name).ToList());
        }

        private void DeleteNode(AdminLevel item, List<AdminLevel> oldList)
        {
            oldList.Remove(item);
        }

        private void GetChildren(List<AdminLevel> children, AdminLevel parent)
        {
            if (parent.Children.Count == 0)
                children.Add(parent);
            else
                foreach (var i in parent.Children)
                    GetChildren(children, i);
        }

    }
}
