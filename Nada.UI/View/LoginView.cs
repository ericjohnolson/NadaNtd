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

namespace Nada.UI.View
{
    public partial class LoginView : BaseControl
    {
        public event Action OnLogin = () => {};

        public LoginView()
            : base()
        {
            InitializeComponent();
        }

        public void DoLogin(string uid, string pwd)
        {
            if (Membership.ValidateUser(uid, pwd))
            {
                ApplicationData.Instance.CurrentUser = Membership.GetUser(uid);
                OnLogin();
            }
        }

        private void LoginView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
            }

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            DoLogin(tbUid.Text, tbPwd.Text);

        }
    }
}
