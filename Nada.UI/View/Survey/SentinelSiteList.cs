using Nada.Globalization;
using Nada.Model;
using Nada.Model.Base;
using Nada.Model.Repositories;
using Nada.Model.Survey;
using Nada.UI.AppLogic;
using Nada.UI.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nada.UI.View.Survey
{
    public partial class SentinelSiteList : BaseForm
    {

        private int AdminLevelId;
        private SurveyBase Survey;
        private SurveyRepository SurveyRepo;
        private List<SentinelSite> SentinelSites = new List<SentinelSite>();
        public event Action OnSave = () => { };

        public SentinelSiteList() : base()
        {
            InitializeComponent();
        }

        public SentinelSiteList(SurveyBase surveyBase) : this()
        {
            Survey = surveyBase;
            AdminLevelId = Survey.AdminLevelId.HasValue ? Survey.AdminLevelId.Value : 0;
        }

        private void SentinelSiteList_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                // Translate the UI
                Localizer.TranslateControl(this);
                // Make sure there is an AdminlevelId
                if (AdminLevelId == 0)
                    return;
                // Update the title
                AdminLevel adminLevel = Survey.AdminLevels.FirstOrDefault();
                if (adminLevel != null && adminLevel.Name != null)
                    headerLabel.Text = String.Format("{0}: {1}", headerLabel.Text, adminLevel.Name);
                // Get the Survey repo
                SurveyRepo = new SurveyRepository();
                // Get all the SentinelSites for this Admin unit
                SentinelSites = SurveyRepo.GetSitesForAdminLevel(new List<string>() { AdminLevelId.ToString() });
                // Order the sites
                SentinelSites = SentinelSites.OrderBy(s => s.SiteName).ToList();
                // Load the SentinelSites into the ListView
                sentinelSiteListView.SetObjects(SentinelSites);
            }
        }

        private void AddSiteLink_OnClick()
        {
            // Make sure the Survey has admin levels
            if (Survey == null || Survey.AdminLevels == null)
                return;
            // Show the modal for adding sites
            SentinelSiteAdd modal = new SentinelSiteAdd(Survey.AdminLevels);
            modal.OnSave += sites_OnAdd;
            modal.ShowDialog();
        }

        private void sites_OnAdd(SentinelSite newSite)
        {
            // Add the new site to the collection
            SentinelSites.Add(newSite);
            // Update the list
            UpdateSiteList(newSite);
        }

        private void sites_OnEdit(SentinelSite editedSite)
        {
            // Find the corresponding site
            SentinelSite matchingSite = SentinelSites.Where(s => s.Id == editedSite.Id).FirstOrDefault();
            if (matchingSite != null)
            {
                int currentIndex = SentinelSites.IndexOf(matchingSite);
                // Replace the matching site
                if (currentIndex >= 0)
                    SentinelSites[currentIndex] = editedSite;
            }
            else
            {
                // Add the site to the collection
                SentinelSites.Add(editedSite);
            }
            // Update the list
            UpdateSiteList(editedSite);
        }

        private void UpdateSiteList(SentinelSite savedSite)
        {
            // Order the sites
            SentinelSites = SentinelSites.OrderBy(s => s.SiteName).ToList();
            // Update the ListView
            sentinelSiteListView.SetObjects(SentinelSites);
            // Update the Survey
            if (savedSite != null)
                Survey.SentinelSiteId = savedSite.Id;
            // Callback action to update the invoking UI
            OnSave();
        }

        private void sentinelSiteListView_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            if (e.Column.AspectName == "EditText")
            {
                SentinelSiteAdd modal = new SentinelSiteAdd((SentinelSite) e.Model);
                modal.OnSave += sites_OnEdit;
                modal.ShowDialog();
            }
            else if (e.Column.AspectName == "DeleteText")
            {
                // The site to delete
                SentinelSite siteToDelete = (SentinelSite) e.Model;

                // Check if the site has associated data
                if (SurveyRepo.GetSiteSurveyCount(siteToDelete) > 0)
                {
                    // The site has associated data, so display a message
                    MessageBox.Show(Translations.CannotDeleteSentinelSiteHasData, Translations.CannotDeleteSite);
                    return;
                }

                // Confirm the delete with the user
                DeleteConfirm confirm = new DeleteConfirm();
                if (confirm.ShowDialog() == DialogResult.OK)
                {
                    // The site has no associated data, so it is safe to delete
                    SurveyRepo.DeleteSite(siteToDelete, ApplicationData.Instance.GetUserId());
                    // Get the index of the site in the collection
                    int currentIndex = SentinelSites.IndexOf(siteToDelete);
                    // Remove the deleted site from the collection
                    if (currentIndex >= 0)
                        SentinelSites.RemoveAt(currentIndex);
                    // Update the list
                    UpdateSiteList(null);
                }
            }
        }
    }
}
