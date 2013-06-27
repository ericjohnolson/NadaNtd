using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.UI.View;

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
                loginView.Dock = DockStyle.Fill;
                pnlMain.Controls.Add(loginView);
                loginView.OnLogin += loginView1_OnLogin;
                DoTranslate();
                // COMMENT OUT WHEN NOT DEVELOPING!
                LoadDeveloperMode(loginView);
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
            pnlMain.Controls.Clear();
            menuMain.Visible = true;
            pnlLeft.Visible = true;
            currentView = new View.WelcomeView();
            currentView.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(currentView);
            DoTranslate();
        }
        #endregion

        #region Menu
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            currentView = new SettingsView();
            currentView.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(currentView);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion


    }
}
