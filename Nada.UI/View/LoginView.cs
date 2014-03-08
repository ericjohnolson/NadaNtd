using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using System.Threading;
using System.Globalization;
using System.Web.Security;
using Nada.UI.Base;
using Nada.Model.Repositories;
using System.IO;
using System.Configuration;
using Nada.UI.View.Wizard;
using Nada.Globalization;


namespace Nada.UI.View
{
    public partial class LoginView : BaseControl, IView
    {
        MemberRepository repo = new MemberRepository();
        private bool autoLogin = false;
        public Action<string> StatusChanged { get; set; }
        public Action OnClose { get; set; }
        public Action OnRestart { get; set; }
        public string Title { get { return ""; } }
        public void SetFocus() { }
        public event Action OnLogin = () => {};

        public LoginView()
            : base()
        {
            InitializeComponent();
        }

        private void LoginView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                RunUpdates();
                autoLogin = repo.Authenticate("admin", "@ntd1one!");
            }
        }

        private void RunUpdates()
        {
            if (UpdateApp.HasInternetConnection() && UpdateApp.HasUpdate())
            {
                WizardForm wiz = new WizardForm(new UpdateApp(OnRestart), Translations.Updates);
                wiz.OnFinish += () => { };
                wiz.ShowDialog();
            }
            SettingsRepository repo = new SettingsRepository();
            string scriptsPath = Path.Combine(
              System.Windows.Forms.Application.StartupPath, @"DatabaseScripts\Differentials\");
            List<string> scriptsToRun = repo.GetSchemaChangeScripts(scriptsPath);
            if (scriptsToRun.Count > 0)
            {
                WizardForm wiz = new WizardForm(new UpdateDb(scriptsToRun), Translations.Updates);
                wiz.OnFinish += () => { };
                wiz.ShowDialog();
            }
        }

        public void DoLogin(string uid, string pwd)
        {
            if (repo.Authenticate(uid, pwd))
                OnLogin();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if(tbUid.Text == "" && tbPwd.Text == "" && autoLogin)
                OnLogin();

            DoLogin(tbUid.Text, tbPwd.Text);
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "file:///" + Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["HelpFile"]);
            //HelpView help = new HelpView();
            //help.Show();
        }
    }
}
