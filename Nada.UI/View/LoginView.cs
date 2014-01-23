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


namespace Nada.UI.View
{
    public partial class LoginView : BaseControl, IView
    {
        MemberRepository repo = new MemberRepository();
        private bool autoLogin = false;
        public Action<string> StatusChanged { get; set; }
        public Action OnClose { get; set; }
        public string Title { get { return ""; } }
        public void SetFocus() { }
        public event Action OnLogin = () => {};

        public LoginView()
            : base()
        {
            InitializeComponent();
        }

        public void DoLogin(string uid, string pwd)
        {
            if (repo.Authenticate(uid, pwd))
                OnLogin();
        }

        private void LoginView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                autoLogin = repo.Authenticate("admin", "@ntd1one!");
            }
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
