﻿using System;
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
    public partial class AdminLevelMultiselect : BaseControl
    {
        private DemoRepository demography = null;
        private SettingsRepository settings = null;
        private List<AdminLevel> available = new List<AdminLevel>();
        private List<AdminLevel> selected = new List<AdminLevel>();

        public AdminLevelMultiselect()
            : base()
        {
            InitializeComponent();
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

                cbLevels.DropDownWidth = BaseForm.GetDropdownWidth(levels.Select(a => a.DisplayName));

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
            if (adminLevel.LevelNumber == ((AdminLevelType)cbLevels.SelectedItem).LevelNumber)
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
            if (cbLevels.SelectedItem == null)
                return;
            var level = (AdminLevelType)cbLevels.SelectedItem;
            available = demography.GetAdminLevelTree(level.Id);
            selected = new List<AdminLevel>();
            treeAvailable.SetObjects(available.OrderBy(i => i.Name).ToList());
            treeSelected.SetObjects(selected.OrderBy(i => i.Name).ToList());
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
            foreach (var item in treeAvailable.SelectedObjects.Cast<AdminLevel>())
            {
                var parent = GetParent(item, available, selected);
                if (parent != null)
                    MergeNodes(selected, parent);
                DeleteNode(item, available);
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
            foreach (var item in treeSelected.SelectedObjects.Cast<AdminLevel>())
            {
                var parent = GetParent(item, selected, available);
                if (parent != null)
                    MergeNodes(available, parent);

                DeleteNode(item, selected);
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
            treeAvailable.ClearObjects();
            treeAvailable.SetObjects(available.OrderBy(i => i.Name).ToList());
            treeSelected.ClearObjects();
            treeSelected.SetObjects(selected.OrderBy(i => i.Name).ToList());
        }

        private AdminLevel GetParent(AdminLevel item, List<AdminLevel> oldList, List<AdminLevel> newList)
        {
            // if it is parent add it
            if (item.LevelNumber == 1)
                return item;

            AdminLevel oldParent = oldList.FirstOrDefault(p => p.Id == item.ParentId);
            AdminLevel newParent = newList.FirstOrDefault(p => p.Id == item.ParentId);
            if (newParent == null)
            {
                newParent = oldParent.CopyTreeNode();
                newParent.Children.Add(item);
                return GetParent(newParent, oldList, newList);
            }
            else
            {
                MergeNodes(newParent.Children, item);
                return null;
            }
        }

        private void MergeNodes(List<AdminLevel> list, AdminLevel item)
        {
            var existing = list.FirstOrDefault(i => i.Id == item.Id);
            if (existing == null)
                list.Add(item);
            else
                foreach (var i in item.Children)
                    MergeNodes(existing.Children, i);
        }

        private void DeleteNode(AdminLevel item, List<AdminLevel> oldList)
        {
            if (item.LevelNumber == 1)
                oldList.Remove(item);
            else
            {
                AdminLevel parent = oldList.FirstOrDefault(p => p.Id == item.ParentId);
                parent.Children.Remove(item);
            }
        }

        private void treeSelected_CellClick(object sender, BrightIdeasSoftware.CellClickEventArgs e)
        {
            // e.Handled = true;
            treeSelected.Expand(e.Model);
        }

        private void treeAvailable_CellClick(object sender, BrightIdeasSoftware.CellClickEventArgs e)
        {
            // e.Handled = true;
            treeAvailable.Expand(e.Model);
        }


    }
}
