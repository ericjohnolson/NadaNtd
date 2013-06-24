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

        public Shell()
        {
            InitializeComponent();

            StartUp();
        }

        private void StartUp()
        {
            toolStripContainer1.LeftToolStripPanelVisible = false;
            menuStrip1.Visible = false;
            loginView2.OnLogin += loginView1_OnLogin;
            DoTranslate();
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
            menuStrip1.Visible = true;
            toolStripContainer1.LeftToolStripPanelVisible = true;
            currentView = new View.WelcomeView();
            currentView.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(currentView);
            DoTranslate();
        }
    }
}
