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
        private bool restartNeeded = false;
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
            SettingsRepository repo = new SettingsRepository();
            string scriptsPath = Path.Combine(
              System.Windows.Forms.Application.StartupPath, @"DatabaseScripts\Differentials\");
            List<string> scriptsToRun = repo.GetSchemaChangeScripts(scriptsPath);
            if (scriptsToRun.Count > 0)
            {
                WizardForm wiz = new WizardForm(new UpdateDb(scriptsToRun, DoRestart), Translations.Updates);
                wiz.OnFinish += () => 
                {
                    if (restartNeeded)
                        OnRestart();
                };
                wiz.ShowDialog();
            }
        }

        public void DoRestart()
        {
            restartNeeded = true;
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

    }
}
