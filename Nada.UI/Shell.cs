using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
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
using Nada.UI.View.Help;
using Nada.UI.View.Intervention;
using Nada.UI.View.Modals;
using Nada.UI.View.Reports;
using Nada.UI.View.Survey;
using Nada.UI.View.Wizard;
using Nada.UI.ViewModel;

namespace Nada.UI
{
    public partial class Shell : BaseForm
    {
        private UserControl currentView = null;
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
            }
        }

        private void DoTranslate()
        {
            this.Text = Localizer.GetValue("ApplicationTitle");
            Localizer.TranslateControl(this);
            // Translate controls
            foreach (ToolStripMenuItem item in mainMenu.Items)
            {
                item.Text = TranslationLookup.GetValue(item.Text, item.Text);
                foreach (var child in item.DropDownItems)
                    if (child is ToolStripMenuItem)
                        (child as ToolStripMenuItem).Text = TranslationLookup.GetValue((child as ToolStripMenuItem).Text, (child as ToolStripMenuItem).Text);
            }
        }

        void dbView_OnFileSelected()
        {
            DoTranslate();
            LoginView loginView = new LoginView();
            loginView.OnLogin += loginView1_OnLogin;
            LoadView(loginView);
        }

        public void loginView1_OnLogin()
        {
            var lang = Localizer.SupportedLanguages;
            //menuMain.Visible = true;
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
            if ((bool)e.Result)
            {
                StepDemoUpdateGrowthRate step = new StepDemoUpdateGrowthRate();
                WizardForm wiz = new WizardForm(step, Translations.UpdateDemographyForYear);
                wiz.OnFinish = () => { LoadDashboardAndCheckStartTasks(); };
                step.OnSkip = () => { wiz.Close(); };
                wiz.ShowDialog();
            }
        }

        void updateWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = settings.ShouldDemoUpdate();
        }

        private void LoadDashboard(DashboardView dashboard)
        {
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

        void LoadView(UserControl view)
        {
            pnlMain.Controls.Clear();
            currentView = view;
            currentView.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(currentView);
            hrTop.Visible = (view is DashboardView);
            mainMenu.Visible = (view is DashboardView);
        }

        private void view_StatusChanged(string status)
        {
            tsLastUpdated.Text = status;
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
            HelpView help = new HelpView();
            help.Show();
        }

        private void menuCheckForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var updates = new Updates();
            updates.ShowDialog();
        }

        private void menuAboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new About();
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
            LoadView(new DashboardView());
        }

        private void menuNewAdminLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var adminLevelAdd = new AdminLevelAdd();
            adminLevelAdd.OnSave += adminLevelAdd_OnSave;
            adminLevelAdd.ShowDialog();
        }

        private void adminLevelAdd_OnSave()
        {
            var dashboard = new DashboardView();
            LoadDashboard(dashboard);
        }
        #endregion


        

    }
}
