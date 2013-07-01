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
    public partial class AdminLevelView : UserControl
    {
        public event Action<AdminLevel> OnSelect = (e) => { };
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
                r = new DemoRepository();
                bsAdminLevel.DataSource = adminLevel;

                lvDemos.SetObjects(r.GetAdminLevelDemography(adminLevel.Id));

                if (childType == null)
                    grpChildren.Visible = false;
                else
                {
                    grpChildren.Visible = true;
                    grpChildren.Values.Heading = childType.DisplayName;
                    btnImportChildren.Text = childType.DisplayName + " Import";
                    btnAddChild.Text = "Add " + childType.DisplayName;
                    lvChildren.SetObjects(adminLevel.Children);
                }
            }
        }

        private void btnImportChildDemos_Click(object sender, EventArgs e)
        {
            ImportDemographyModal dialog = new ImportDemographyModal(adminLevel);
            dialog.ShowDialog();
        }

        private void btnImportChildren_Click(object sender, EventArgs e)
        {
            ImportAdminLevelsModal dialog = new ImportAdminLevelsModal(adminLevel, childType);
            dialog.OnSuccess += importChildren_OnSuccess;
            dialog.ShowDialog();
        }

        private void importChildren_OnSuccess()
        {
            lvChildren.SetObjects(r.GetAdminLevelChildren(adminLevel.Id));
        }

        private void btnAddChild_Click(object sender, EventArgs e)
        {

        }

        private void btnAddDemo_Click(object sender, EventArgs e)
        {

        }

        private void lvChildren_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            OnSelect((AdminLevel)e.Model);
        }


    }
}
