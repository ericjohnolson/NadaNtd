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
using Nada.UI.AppLogic;

namespace Nada.UI.View
{
    public partial class SettingsView : UserControl
    {
        private DiseaseRepository diseases = null;
        private SettingsRepository settings = null;
        private List<PopGroup> popGroups = null;
        private List<AdminLevelType> adminLevels = null;

        public SettingsView()
        {
            InitializeComponent();
        }

        private void SettingsView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                diseases = new DiseaseRepository();
                settings = new SettingsRepository();
                popGroups = settings.GetAllPopGroups();
                adminLevels = settings.GetAllAdminLevels();
                lvDiseases.SetObjects(diseases.GetAllDiseases(Localizer.GetCultureName()));
                popGroupBindingSource.DataSource = popGroups;
                adminLevelBindingSource.DataSource = adminLevels;
            }
        }

        #region Diseases
        private void btnAddDisease_Click(object sender, EventArgs e)
        {
            DiseaseModal form = new DiseaseModal(new Disease());
            form.OnSave += disease_OnSave;
            form.ShowDialog();
        }

        private void lvDiseases_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            Disease selected = (Disease)e.Model;
            DiseaseModal form = new DiseaseModal(selected.Id);
            form.OnSave += disease_OnSave;
            form.ShowDialog();
        }

        private void disease_OnSave()
        {
            lvDiseases.SetObjects(diseases.GetAllDiseases(Localizer.GetCultureName()));
        }
        #endregion

        #region pop groups
        private void btnSavePops_Click(object sender, EventArgs e)
        {
            popGroupBindingSource.EndEdit();
            foreach (var popGroup in popGroups)
                settings.UpdatePopGroup(popGroup, ApplicationData.Instance.GetUserId());
        }

        private void btnPopGroup_Click(object sender, EventArgs e)
        {
            PopGroupModal form = new PopGroupModal();
            form.OnSave += pop_OnSave;
            form.ShowDialog();
        }

        private void pop_OnSave(PopGroup obj)
        {
            popGroups.Add(obj);
            popGroupBindingSource.ResetBindings(false);
        }
        #endregion

        #region admin levels
        private void btnSaveAdminLevels_Click(object sender, EventArgs e)
        {
            adminLevelBindingSource.EndEdit();
            foreach (var adminLevel in adminLevels)
                settings.UpdateAdminLevel(adminLevel, ApplicationData.Instance.GetUserId());
        }

        private void btnAdminLevel_Click(object sender, EventArgs e)
        {
            AdminLevelModal form = new AdminLevelModal(grdAdminLevels.Rows.Count + 1);
            form.OnSave += adminLevel_OnSave;
            form.ShowDialog();
        }

        private void adminLevel_OnSave(AdminLevelType obj)
        {
            adminLevels.Add(obj);
            adminLevelBindingSource.ResetBindings(false);
        }
        #endregion

    }
}
