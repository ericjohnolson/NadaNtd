using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Diseases;
using Nada.Model.Imports;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.Model.Survey;
using Nada.UI.AppLogic;
using Nada.UI.Base;
using Nada.UI.View;
using Nada.UI.View.Demography;
using Nada.UI.View.DiseaseDistribution;
using Nada.UI.View.Intervention;
using Nada.UI.View.Modals;
using Nada.UI.View.Reports;
using Nada.UI.View.Survey;
using Nada.UI.View.Wizard;
using Nada.UI.ViewModel;
using Nada.Model.Demography;

namespace Nada.UI
{
    public partial class Shell : BaseForm
    {
        private IView currentView = null;
        private SettingsRepository settings = new SettingsRepository();

        #region Initialize/Login
        public Shell()
            : base()
        {
            InitializeComponent();
        }

        private void Shell_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                pnlLeft.Visible = false;
                DatabaseView dbView = new DatabaseView();
                dbView.OnFileSelected += dbView_OnFileSelected;
                LoadView(dbView);
                DoTranslate();
                if (ConfigurationManager.AppSettings["DeveloperMode"] == "QA")
                    lblDeveloperMode.Visible = true;

                // Updates
                if (UpdateApp.HasInternetConnection() && UpdateApp.HasUpdate())
                {
                    WizardForm wiz = new WizardForm(new UpdateApp(RestartApp), Translations.Updates);
                    wiz.OnFinish += () => { };
                    wiz.ShowDialog();
                }
            }
        }
        
        private void DoTranslate()
        {
            this.Text = Localizer.GetValue("ApplicationTitle");
            Localizer.TranslateControl(this);
        }

        void dbView_OnFileSelected()
        {
            DoTranslate();
            LoginView loginView = new LoginView();
            loginView.OnRestart += RestartApp;
            loginView.OnLogin += loginView1_OnLogin;
            LoadView(loginView);
        }

        public void loginView1_OnLogin()
        {
            var lang = Localizer.SupportedLanguages;
            pnlLeft.Visible = true;
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
            BackgroundWorker updateWorker = new BackgroundWorker();
            updateWorker.DoWork += updateWorker_DoWork;
            updateWorker.RunWorkerCompleted += updateWorker_RunWorkerCompleted;
            updateWorker.RunWorkerAsync();

        }

        void updateWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if ((bool)e.Result && (Roles.IsUserInRole(ApplicationData.Instance.CurrentUser.UserName, "RoleDataEnterer") ||
            //    Roles.IsUserInRole(ApplicationData.Instance.CurrentUser.UserName, "RoleAdmin")))
            //{
            //    StepDemoUpdateGrowthRate step = new StepDemoUpdateGrowthRate();
            //    WizardForm wiz = new WizardForm(step, Translations.UpdateDemographyForYear);
            //    wiz.OnFinish = () => { LoadDashboardAndCheckStartTasks(); };
            //    step.OnSkip = () => { wiz.Close(); };
            //    wiz.ShowDialog();
            //}
        }

        void updateWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = settings.ShouldDemoUpdate();
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error checking if system should update demography (updateWorker_DoWork). ", ex);
                throw;
            }
        }

        private void LoadDashboard(DashboardView dashboard)
        {
            SetPermissions();
            dashboard.StatusChanged += view_StatusChanged;
            dashboard.LoadView = (v) => { LoadView(v); };
            dashboard.LoadDashForAdminLevel = (a) =>
            {
                DashboardView d = new DashboardView();
                if (a != null)
                    d = new DashboardView(a);
                LoadDashboard(d);
            };
            LoadView(dashboard);
        }

        private void SetPermissions()
        {
            menuEditSettingsToolStripMenuItem.Visible = Roles.IsUserInRole(ApplicationData.Instance.CurrentUser.UserName, "RoleAdmin");
            if (!Roles.IsUserInRole(ApplicationData.Instance.CurrentUser.UserName, "RoleDataEnterer") &&
               !Roles.IsUserInRole(ApplicationData.Instance.CurrentUser.UserName, "RoleAdmin"))
            {
                menuNewAdminLevelToolStripMenuItem.Visible = false;
                menuSettingsToolStripMenuItem.Visible = false;
                menuImportToolStripMenuItem.Visible = false;
            }
            else
            {
                menuNewAdminLevelToolStripMenuItem.Visible = true;
                menuSettingsToolStripMenuItem.Visible = true;
                menuImportToolStripMenuItem.Visible = true;
            }
        }

        void LoadView(IView view)
        {
            pnlMain.Controls.Clear();
            view.OnClose = () => { LoadDashboard(new DashboardView()); };
            currentView = view;
            (currentView as UserControl).Dock = DockStyle.Fill;
            pnlMain.Controls.Add((UserControl)currentView);
            hrTop.Visible = (view is DashboardView);
            mainMenu.Visible = (view is DashboardView);
        }

        private void view_StatusChanged(string status)
        {
            tsLastUpdated.Text = status;
        }

        private void RestartApp()
        {
            this.Close(); //to turn off current app
        }
        #endregion

        #region Menu
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabaseView dbView = new DatabaseView();
            dbView.OnFileSelected += dbView_OnFileSelected;
            LoadView(dbView);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabaseView dbView = new DatabaseView();
            dbView.OnFileSelected += dbView_OnFileSelected;
            LoadView(dbView);
        }

        private void menuExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuViewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "file:///" + Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["HelpFile"]);
            //HelpView help = new HelpView();
            //help.Show();
        }

        private void menuViewTutorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file = "file:///" + Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["TutorialFile"];
            System.Diagnostics.Process.Start(file);
        }

        private void menuCheckForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WizardForm wiz = new WizardForm(new UpdateApp(RestartApp), Translations.Updates);
            wiz.OnFinish += () => { };
            wiz.ShowDialog();
        }

        private void menuAboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new About();
            about.OnRestart += RestartApp;
            about.ShowDialog();
        }

        private void menuNewReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadView(new ReportsDashboard());
        }

        private void menuDemographyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void menuDiseaseDistributionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadImporter(new DistroImporter());
        }

        private void menuIntvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadImporter(new IntvImporter());
        }

        private void menuProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadImporter(new ProcessImporter());
        }

        private void menuSurveysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadImporter(new SurveyImporter());
        }

        private void LoadImporter(IImporter importer)
        {
            WizardForm wiz = new WizardForm(new ImportStepType(new ImportOptions { Importer = importer }), importer.ImportName);
            wiz.OnFinish = import_OnSuccess;
            wiz.ShowDialog();
        }

        private void import_OnSuccess()
        {
            LoadDashboard(new DashboardView());
        }

        private void menuNewAdminLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var adminLevelAdd = new AdminLevelAdd();
            adminLevelAdd.OnSave += adminLevelAdd_OnSave;
            adminLevelAdd.ShowDialog();
        }

        private void adminLevelAdd_OnSave()
        {
            LoadDashboard(new DashboardView());
        }

        private void menuEditSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dashboard = new SettingsDashboard();
            LoadView(dashboard);
        }
        #endregion

        private void menuNewYearDemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StepDemoUpdateGrowthRate step = new StepDemoUpdateGrowthRate();
            WizardForm wiz = new WizardForm(step, Translations.UpdateDemographyForYear);
            wiz.OnFinish = () => { LoadDashboard(new DashboardView()); };
            step.OnSkip = () => { wiz.Close(); };
            wiz.ShowDialog();
        }

        private void menuNewYearDistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StepDdUpdateGrowthRate step = new StepDdUpdateGrowthRate();
            WizardForm wiz = new WizardForm(step, Translations.UpdateDdForYear);
            wiz.OnFinish = () => { LoadDashboard(new DashboardView()); };
            step.OnSkip = () => { wiz.Close(); };
            wiz.ShowDialog();
        }

        private void menuSplitCombineDistrictToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsRepository repo = new SettingsRepository();
            DiseaseDashboard dash = new DiseaseDashboard(true);
            dash.ReloadView = (v) => {  };
            dash.LoadView = (v) => {  };
            dash.LoadForm = (v) => { LoadForm(v); };
            dash.StatusChanged = (s) => {  };
            WizardForm wiz = new WizardForm(new BackupForRedistrict(new RedistrictingOptions { Dashboard = dash, SplitType = SplittingType.SplitCombine }),
                Translations.SplitCombineTitle);
            wiz.OnFinish += () => { LoadDashboard(new DashboardView()); };
            wiz.ShowDialog();
        }

        private void menuSplitDistrictToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsRepository repo = new SettingsRepository();
            DiseaseDashboard dash = new DiseaseDashboard(true);
            dash.ReloadView = (v) => { };
            dash.LoadView = (v) => { };
            dash.LoadForm = (v) => { LoadForm(v); };
            dash.StatusChanged = (s) => { };
            WizardForm wiz = new WizardForm(new BackupForRedistrict(new RedistrictingOptions { Dashboard = dash, SplitType = SplittingType.Split }),
                Translations.SplittingTitle);
            wiz.OnFinish += () => { LoadDashboard(new DashboardView()); };
            wiz.ShowDialog();
        }

        private void menuMergeDistrictToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsRepository repo = new SettingsRepository();
            DiseaseDashboard dash = new DiseaseDashboard(true);
            dash.ReloadView = (v) => { };
            dash.LoadView = (v) => { };
            dash.LoadForm = (v) => { LoadForm(v); };
            dash.StatusChanged = (s) => { };
            WizardForm wiz = new WizardForm(new BackupForRedistrict(new RedistrictingOptions { Dashboard = dash, SplitType = SplittingType.Merge }),
                Translations.SplitMergeTitle);
            wiz.OnFinish += () => { LoadDashboard(new DashboardView()); };
            wiz.ShowDialog();
        }

        private void LoadForm(IView v)
        {
            ViewForm form = new ViewForm(v);
            v.OnClose = () =>
            {
                form.Close();
            };
            form.Show();
        }


    }
}
