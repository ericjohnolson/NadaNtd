﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Diseases;
using Nada.Model.Repositories;
using Nada.Model.Survey;
using Nada.UI.AppLogic;
using Nada.UI.View;
using Nada.UI.View.Demography;
using Nada.UI.View.Intervention;
using Nada.UI.View.Modals;
using Nada.UI.View.Reports;
using Nada.UI.View.Survey;

namespace Nada.UI
{
    public partial class Shell : Form
    {
        private UserControl currentView = null;

        #region Initialize/Login
        public Shell()
        {
            InitializeComponent();
        }

        private void Shell_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                pnlLeft.Visible = false;
                menuMain.Visible = false;
                LoginView loginView = new LoginView();
                loginView.OnLogin += loginView1_OnLogin;
                LoadView(loginView);
                DoTranslate();
                // COMMENT OUT WHEN NOT DEVELOPING!
                //LoadDeveloperMode(loginView);
            }
        }

        private void LoadDeveloperMode(LoginView view)
        {
            view.DoLogin("admin", "@ntd1one!");
            lblDeveloperMode.Visible = true;
        }

        private void DoTranslate()
        {
            this.Text = Localizer.GetValue("ApplicationTitle");
            Localizer.TranslateControl(this);
        }

        public void loginView1_OnLogin()
        {
            Shell sh = new Shell();
            var lang = Localizer.SupportedLanguages;
            //menuMain.Visible = true;
            pnlLeft.Visible = true;
            DoTranslate();
            var dashboard = new DemographyView();
            LoadDashboard(dashboard);
        }

        private void LoadDashboard(DemographyView dashboard)
        {
            dashboard.StatusChanged += view_StatusChanged;
            dashboard.LoadView = (v) => { LoadView(v); };
            dashboard.LoadDashForAdminLevel = (a) => 
            { 
                DemographyView d = new DemographyView();
                if(a != null)
                    d = new DemographyView(a);
                LoadDashboard(d);
            };
            LoadView(dashboard);
        }
 
        void LoadView(UserControl view)
        {
            pnlMain.Controls.Clear();
            currentView = view;
            currentView.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(currentView);
        }

        private void view_StatusChanged(string status)
        {
            tsLastUpdated.Text = status;
        }
        #endregion

        #region Menu

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadView(new SettingsView());
        }

        private void countryInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DemoRepository r = new DemoRepository();
            CountryModal form = new CountryModal(r.GetCountry());
            form.ShowDialog();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            LoadView(new DemographyView());
        }

        private void demographyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadView(new DemographyView());
        }

        private void createCustomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadView(new ReportCreatorView());
        }
        
        private void mappingToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var view = new SurveyBaseView(StaticSurveyType.LfMapping);
            view.OnSave += Survey_OnSave;
            LoadView(view);
        }

        private void prevalenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var view = new LfMfPrevalenceView();
            view.StatusChanged += view_StatusChanged;
            view.OnClose = Dash_Load;
            LoadView(view);
        }

        private void tASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var view = new SurveyBaseView(StaticSurveyType.LfTas);
            view.OnSave += Survey_OnSave;
            LoadView(view);
        }

        void Survey_OnSave(bool doRefresh)
        {
            LoadView(new DemographyView());
        }

        void Dash_Load()
        {
            LoadView(new DemographyView());
        }

        private void lFMDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var view = new LfMdaView();
            view.OnSave += Survey_OnSave;
            LoadView(view);
        }

        private void lFLymphedemaMorbidityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var view = new IntvBaseView(StaticIntvType.LfLymphedemaMorbidity);
            view.OnSave += Survey_OnSave;
            LoadView(view);
        }

        private void lFHydroceleMorbidityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var view = new IntvBaseView(StaticIntvType.LfHydroceleMorbidity);
            view.OnSave += Survey_OnSave;
            LoadView(view);
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var updates = new Updates();
            updates.ShowDialog();
        }

        private void aboutNationalDatabaseTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new About();
            about.ShowDialog();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void lFDiseaseDistributionToolStripMenuItem_Click(object sender, EventArgs e)
        {        
            AdminLevelPicker picker = new AdminLevelPicker();
            picker.OnSelect += diseaseDistroAdminLevel_OnSelect;
            picker.ShowDialog();
        }

        void diseaseDistroAdminLevel_OnSelect(Model.AdminLevel obj)
        {
            DiseaseRepository r = new DiseaseRepository();
            DiseaseDistribution dd = r.GetDiseaseDistribution(obj.Id, DiseaseType.Lf);
            DiseaseDistributionEdit modal = new DiseaseDistributionEdit(dd);
            modal.ShowDialog();
        }

        private void lFPopulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var view = new DiseasePopulationEdit(DiseaseType.Lf);
            view.OnSave += Survey_OnSave;
            LoadView(view);
        }
        #endregion

        private void lFSentinelSpotCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SurveyRepository r = new SurveyRepository();
            ImportDownload form = new ImportDownload(new LfSentinelImporter(r.GetSurveyType((int)StaticSurveyType.LfPrevalence)));
            form.ShowDialog();
        }



    }
}
