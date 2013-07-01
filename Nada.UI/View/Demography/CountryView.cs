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
using Nada.UI.View.Modals;
using Nada.UI.View.Demography;

namespace Nada.UI.View
{
    public partial class CountryView : UserControl
    {
        public event Action<AdminLevel> OnSelect = (e) => { };
        private Country model = null;
        private DemoRepository r = null;
        private AdminLevel adminLevel;
        private AdminLevelType childType;

        public CountryView()
        {
            InitializeComponent();
        }

        public CountryView(AdminLevel adminLevel, AdminLevelType childType)
        {
            this.childType = childType;
            this.adminLevel = adminLevel;
            InitializeComponent();
        }

        private void CountryView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                r = new DemoRepository();
                model = r.GetCountry();
                bsCountry.DataSource = model;
                lvDemos.SetObjects(r.GetCountryDemography());
                lvChildren.SetObjects(adminLevel.Children);
            }
        }

        private void lnkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CountryModal form = new CountryModal(model);
            form.OnSave += form_OnSave;
            form.ShowDialog();
        }

        private void form_OnSave()
        {
            bsCountry.ResetBindings(false);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CountryDemoEdit form = new CountryDemoEdit(new CountryDemography());
            form.OnSave += demo_OnSave;
            form.ShowDialog();
        }

        private void lvDemos_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            CountryDemography selected = (CountryDemography)e.Model;
            CountryDemoEdit form = new CountryDemoEdit(selected);
            form.OnSave += demo_OnSave;
            form.ShowDialog();
        }

        private void demo_OnSave(CountryDemography obj)
        {
            lvDemos.SetObjects(r.GetCountryDemography());
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

        private void lvChildren_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            OnSelect((AdminLevel)e.Model);
        }

    }
}
