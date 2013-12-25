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
    public partial class RolePickerControl : BaseControl
    {
        private MemberRepository repo = null;
        private List<MemberRole> available = new List<MemberRole>();
        private List<MemberRole> selected = new List<MemberRole>();

        public RolePickerControl()
            : base()
        {
            InitializeComponent();
        }

        private void RoleControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                repo = new MemberRepository();
            }
        }

        public List<MemberRole> GetSelectedItems()
        {
            return selected;
        }

        public List<MemberRole> GetUnselectedItems()
        {
            return available;
        }

        public void LoadLists(Member user)
        {
            available.Clear();
            selected.Clear();
            var roles = repo.GetAllRoles();
            foreach (var item in roles)
            {
                if (!string.IsNullOrEmpty(user.Username) && Roles.IsUserInRole(user.Username, item.RoleName))
                    selected.Add(item);
                else
                    available.Add(item);
            }
                lstAvailable.SetObjects(available.OrderBy(i => i.RoleName).ToList());
            lstSelected.SetObjects(selected.OrderBy(i => i.RoleName).ToList());
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
            foreach (var item in lstAvailable.SelectedObjects.Cast<MemberRole>())
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
            foreach (var item in lstSelected.SelectedObjects.Cast<MemberRole>())
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
            lstAvailable.SetObjects(available.OrderBy(i => i.RoleName).ToList());
            lstSelected.ClearObjects();
            lstSelected.SetObjects(selected.OrderBy(i => i.RoleName).ToList());
        }


    }
}
