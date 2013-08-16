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

namespace Nada.UI.View
{
    public partial class LoginView : UserControl
    {
        public event Action OnLogin = () => {};

        public LoginView()
        {
            InitializeComponent();
        }

        public void DoLogin(string uid, string pwd)
        {
            if (Membership.ValidateUser(uid, pwd))
            {
                ApplicationData.Instance.CurrentUser = Membership.GetUser(uid);
                CultureInfo ci = new CultureInfo(cbLanguages.SelectedValue.ToString());
                Localizer.SetCulture(ci);
                OnLogin();
            }
        }

        private void LoginView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                bsLanguages.DataSource = Localizer.SupportedLanguages;
                cbLanguages.SelectedValue = Thread.CurrentThread.CurrentCulture.Name;
                TranslatePage();
            }

        }

        private void TranslatePage()
        {
            lblLang.Text = Localizer.GetValue("Language");
            lblPwd.Text = Localizer.GetValue("Password");
            lblUid.Text = Localizer.GetValue("Username");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoLogin(tbUid.Text, tbPwd.Text);
        }

    }
}
