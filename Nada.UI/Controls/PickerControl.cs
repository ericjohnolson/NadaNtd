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
using Nada.Model.Diseases;
using Nada.UI.AppLogic;
using Nada.UI.Base;
using System.Web.Security;

namespace Nada.UI.View
{
    public partial class PickerControl : BaseControl
    {
        private List<object> available = new List<object>();
        private List<object> selected = new List<object>();

        public PickerControl()
            : base()
        {
            InitializeComponent();
        }

        private void RoleControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
            }
        }

        public List<object> GetSelectedItems()
        {
            return selected;
        }

        public List<object> GetUnselectedItems()
        {
            return available;
        }

        public void LoadLists(List<object> listObjects, string displayName)
        {
            available = listObjects;
            selected.Clear();
            lstAvailable.AllColumns[0].AspectName = displayName;
            lstSelected.AllColumns[0].AspectName = displayName;
            lstAvailable.SetObjects(available);
            lstSelected.SetObjects(selected);
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
            foreach (var item in lstAvailable.SelectedObjects)
            {
                selected.Add(item);
                available.Remove(item);
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
            foreach (var item in lstSelected.SelectedObjects)
            {

                available.Add(item);
                selected.Remove(item);
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
            lstAvailable.ClearObjects();
            lstAvailable.SetObjects(available);
            lstSelected.ClearObjects();
            lstSelected.SetObjects(selected);
        }


    }
}
