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
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.Model.Survey;
using Nada.UI.AppLogic;
using Nada.UI.View;
using Nada.UI.View.Demography;
using Nada.UI.View.DiseaseDistribution;
using Nada.UI.View.Intervention;
using Nada.UI.View.Modals;
using Nada.UI.View.Reports;
using Nada.UI.View.Survey;
using Nada.UI.View.Wizard;
using Nada.UI.ViewModel;

namespace Nada.UI
{
    public partial class Shell : Form
    {
        private UserControl currentView = null;
        private SettingsRepository settings = new SettingsRepository();

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

            var status = settings.GetStartUpStatus();
            if (status.ShouldShowStartup())
            {
                var startup = new StartupView();
                startup.OnFinished = () => { LoadDashboardAndCheckStartTasks(); };
                LoadView(startup);
            }
            else
                LoadDashboardAndCheckStartTasks();
        }

        private void LoadDashboardAndCheckStartTasks()
        {
            var dashboard = new DashboardView();
            LoadDashboard(dashboard);

            if (settings.ShouldDemoUpdate())
            {
                StepDemoUpdateGrowthRate step = new StepDemoUpdateGrowthRate();
                WizardForm wiz = new WizardForm(step, Translations.UpdateDemographyForYear);
                wiz.OnFinish = () => { LoadDashboardAndCheckStartTasks(); };
                step.OnSkip = () => { wiz.Close(); };
                wiz.ShowDialog();
            }
        }

        private void LoadDashboard(DashboardView dashboard)
        {
            dashboard.StatusChanged += view_StatusChanged;
            dashboard.LoadView = (v) => { LoadView(v); };
            dashboard.LoadDashForAdminLevel = (a) => 
            { 
                DashboardView d = new DashboardView();
                if(a != null)
                    d = new DashboardView(a);
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
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            LoadView(new DashboardView());
        }

        private void demographyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadView(new DashboardView());
        }

        private void createCustomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadView(new OldReportCreatorView());
        }
        

        private void prevalenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var view = new LfMfPrevalenceView();
            view.StatusChanged += view_StatusChanged;
            view.OnClose = Dash_Load;
            LoadView(view);
        }

        void Survey_OnSave(bool doRefresh)
        {
            LoadView(new DashboardView());
        }

        void Dash_Load()
        {
            LoadView(new DashboardView());
        }

        private void lFMDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void lFLymphedemaMorbidityToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void lFHydroceleMorbidityToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

        void diseaseDistroAdminLevel_OnSelect(Model.AdminLevel obj)
        {
            MessageBox.Show("Not Implemented");
        }

        private void lFPopulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void lFSentinelSpotCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SurveyRepository r = new SurveyRepository();
            ImportDownload form = new ImportDownload(new LfSentinelImporter(r.GetSurveyType((int)StaticSurveyType.LfPrevalence)));
            form.ShowDialog();
        }
        #endregion

    }
}
