using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Repositories;
using Nada.Model.Survey;
using Nada.UI.AppLogic;

namespace Nada.UI.View
{
    public partial class SentinelSitePicker : Form
    {
        public event Action<SentinelSite> OnSelect = (e) => { };
        private SurveyRepository repo = null;

        public SentinelSitePicker()
        {
            InitializeComponent();
        }

        private void Modal_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                adminLevelPickerControl1.OnSelect += adminLevelPickerControl1_OnSelect;
                repo = new SurveyRepository();
            }
        }

        void adminLevelPickerControl1_OnSelect(AdminLevel obj)
        {
            List<SentinelSite> sites = repo.GetSitesForAdminLevel(obj.Id);
            lvChildren.SetObjects(sites);
        }

        private void DoSelect(SentinelSite obj)
        {
            OnSelect(obj);
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            SentinelSiteAdd modal = new SentinelSiteAdd();
            modal.OnSave += DoSelect;
            modal.ShowDialog();
        }

        private void lvChildren_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            DoSelect((SentinelSite)e.Model);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
