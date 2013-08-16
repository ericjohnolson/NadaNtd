using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.View;
using Nada.UI.View.Demography;
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
            this.Text = Localizer.GetValue("Title");
        }

        public void loginView1_OnLogin()
        {
            Shell sh = new Shell();
            var lang = Localizer.SupportedLanguages;
            menuMain.Visible = true;
            pnlLeft.Visible = true;
            LoadView(new View.WelcomeView());
            DoTranslate();
        }

        private void LoadView(UserControl view)
        {
            pnlMain.Controls.Clear();
            currentView = view;
            currentView.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(currentView);
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

        private void lFPrevalenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadLfMfSurvey();
        }

        private void LoadLfMfSurvey()
        {
            var view = new LfMfPrevalenceView();
            view.OnSave += LfMfPrevalenceView_OnSave;
            LoadView(view);
        }

        void LfMfPrevalenceView_OnSave(bool doRefresh)
        {
                LoadView(new View.WelcomeView());
        }
        
        private void createCustomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadView(new ReportCreatorView());
        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadView(new SurveyView());
        }
        #endregion

    }
}
