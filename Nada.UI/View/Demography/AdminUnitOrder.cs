using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;
using BrightIdeasSoftware;
using System.Collections;

namespace Nada.UI.View.Demography
{
    public partial class AdminUnitOrder : BaseForm
    {
        public event Action<AdminLevel> OnSave = (a) => { };
        private AdminLevel model = new AdminLevel();
        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();
        private DemoRepository demo = new DemoRepository();

        public AdminUnitOrder()
            : base()
        {
            InitializeComponent();
        }


        private void AdminLevelAdd_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                LoadAdminUnits();
            }
        }

        private void LoadAdminUnits()
        {
            var levels = settings.GetAllAdminLevels();
            var units = new List<AdminLevel>();
            ImageList ilist = new ImageList();
            var t = demo.GetAdminLevelTree(levels.OrderByDescending(l => l.LevelNumber).First().Id, 0, true, false, -1, false, "", units);

            treeAvailable.SmallImageList = ilist;
            SimpleDropSink sink1 = (SimpleDropSink)treeAvailable.DropSink;
            sink1.AcceptExternal = false;
            sink1.CanDropBetween = true;
            sink1.CanDropOnBackground = false;
            sink1.CanDropOnSubItem = false;

            treeAvailable.ModelCanDrop += HandleModelCanDrop;
            treeAvailable.ModelDropped += HandleModelDropped;
            treeAvailable.CanExpandGetter = m => ((AdminLevel)m).Children.Count > 0;
            treeAvailable.ChildrenGetter = delegate(object m)
            {
                return ((AdminLevel)m).Children;
            };
            treeAvailable.SetObjects(t);

            foreach (var l in t)
            {
                treeAvailable.Expand(l);
                foreach (var l2 in l.Children)
                    treeAvailable.Expand(l2);
            }
        }

        // http://objectlistview.sourceforge.net/cs/blog4.html
        private void HandleModelCanDrop(object sender, BrightIdeasSoftware.ModelDropEventArgs e)
        {
            e.Handled = true;
            e.Effect = DragDropEffects.None;
            if (!e.SourceModels.Contains(e.TargetModel))
            {
                var sourceModels = e.SourceModels.Cast<AdminLevel>();
                AdminLevel target = e.TargetModel as AdminLevel;
                switch (e.DropTargetLocation)
                {
                    case DropTargetLocation.AboveItem:
                        CanDropSameLevel(e, sourceModels, target);
                        break;
                    case DropTargetLocation.BelowItem:
                        CanDropSameLevel(e, sourceModels, target);
                        break;
                    case DropTargetLocation.BetweenItems:
                        CanDropSameLevel(e, sourceModels, target);
                        break;
                    case DropTargetLocation.Item:
                        CanDropChild(e, sourceModels, target);
                        break;
                    case DropTargetLocation.SubItem:
                        CanDropChild(e, sourceModels, target);
                        break;
                    case DropTargetLocation.Background:
                        break;
                    case DropTargetLocation.LeftOfItem:
                        break;
                    case DropTargetLocation.None:
                        break;
                    case DropTargetLocation.RightOfItem:
                        break;
                    default:
                        break;
                }
            }
        }

        private static void CanDropSameLevel(BrightIdeasSoftware.ModelDropEventArgs e, IEnumerable<AdminLevel> sourceModels, AdminLevel target)
        {
            if (sourceModels.All(a => a.AdminLevelTypeId == target.AdminLevelTypeId && a.ParentId == target.ParentId))
                e.Effect = DragDropEffects.Move;
        }

        private static void CanDropChild(BrightIdeasSoftware.ModelDropEventArgs e, IEnumerable<AdminLevel> sourceModels, AdminLevel target)
        {
            if (sourceModels.All(a => a.LevelNumber == (target.LevelNumber + 1) && a.ParentId == target.ParentId))
                e.Effect = DragDropEffects.Move;
        }

        private void HandleModelDropped(object sender, BrightIdeasSoftware.ModelDropEventArgs e)
        {
            switch (e.DropTargetLocation)
            {
                case DropTargetLocation.AboveItem:
                    MoveObjectsToSibling(
                        e.ListView as TreeListView,
                        e.SourceListView as TreeListView,
                        (AdminLevel)e.TargetModel,
                        e.SourceModels,
                        0);
                    break;
                case DropTargetLocation.BelowItem:
                    MoveObjectsToSibling(
                        e.ListView as TreeListView,
                        e.SourceListView as TreeListView,
                        (AdminLevel)e.TargetModel,
                        e.SourceModels,
                        1);
                    break;
                case DropTargetLocation.Item:
                    MoveObjectsToChildren(
                        e.ListView as TreeListView,
                        e.SourceListView as TreeListView,
                        (AdminLevel)e.TargetModel,
                        e.SourceModels);
                    break;
                default:
                    return;
            }

            e.RefreshObjects();
        }

        private void MoveObjectsToChildren(TreeListView targetTree, TreeListView sourceTree, AdminLevel target, IList toMove)
        {
            foreach (AdminLevel x in toMove)
            {
                x.Parent.Children.Remove(x);
                x.Parent = target;
                target.Children.Add(x);
            }
        }

        private void MoveObjectsToSibling(TreeListView targetTree, TreeListView sourceTree, AdminLevel target, IList toMove, int siblingOffset)
        {
            // There are lots of things to get right here:
            // - sourceTree and targetTree may be the same
            // - target may be a root (which means that all moved objects will also become roots)
            // - one or more moved objects may be roots (which means the roots of the sourceTree will change)

            ArrayList sourceRoots = sourceTree.Roots as ArrayList;
            ArrayList targetRoots = targetTree == sourceTree ? sourceRoots : targetTree.Roots as ArrayList;

            // We want to make the moved objects to be siblings of the target. So, we have to
            // remove the moved objects from their old parent and give them the same parent as the target.
            // If the target is a root, then the moved objects have to become roots too.
            foreach (AdminLevel x in toMove)
            {
                if (x.Parent == null)
                    sourceRoots.Remove(x);
                else
                    x.Parent.Children.Remove(x);
                x.Parent = target.Parent;
            }

            // Now add to the moved objects to children of their parent (or to the roots collection
            // if the target is a root)
            if (target.Parent == null)
            {
                targetRoots.InsertRange(targetRoots.IndexOf(target) + siblingOffset, toMove);
            }
            else
            {
                target.Parent.Children.InsertRange(target.Parent.Children.IndexOf(target) + siblingOffset, toMove.Cast<AdminLevel>());
            }
            if (targetTree == sourceTree)
            {
                sourceTree.Roots = sourceRoots;
            }
            else
            {
                sourceTree.Roots = sourceRoots;
                targetTree.Roots = targetRoots;
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            // Need to iterate the loop and save the new sort orders!
            List<AdminLevel> toUpdate = new List<AdminLevel>();
            int order = 1;
            ReorderTree(treeAvailable.Objects.Cast<AdminLevel>(), order, toUpdate);
            demo.ReorderAdminUnits(toUpdate, ApplicationData.Instance.GetUserId());
            OnSave(null);
            this.Close();
        }

        private void ReorderTree(IEnumerable<AdminLevel> adminUnits, int order, List<AdminLevel> toUpdate)
        {
            foreach(var unit in adminUnits)
            {
                unit.SortOrder = order;
                toUpdate.Add(unit);
                order++;
                if (unit.Children.Count > 0)
                    ReorderTree(unit.Children, order, toUpdate);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
