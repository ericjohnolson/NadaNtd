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

namespace Nada.UI.View
{
    public partial class LoginView : UserControl
    {
        public event Action OnLogin = () => {};

        public LoginView()
        {
            InitializeComponent();
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
            lblHeader.Text = Localizer.GetValue("LoginTitle");
            lblLang.Text = Localizer.GetValue("Language");
            lblPwd.Text = Localizer.GetValue("Password");
            lblUid.Text = Localizer.GetValue("Username");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (true)
            {
                CultureInfo ci = new CultureInfo(cbLanguages.SelectedValue.ToString());
                Localizer.SetCulture(ci);
                OnLogin();
            }
        }

    }
}
