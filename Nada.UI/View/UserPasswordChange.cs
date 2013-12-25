using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;

namespace Nada.UI.View
{
    public partial class UserPasswordChange : BaseForm
    {
        public event Action<Member> OnSave = (e) => { };
        private Member model = new Member();
        private MemberRepository members = new MemberRepository();

        public UserPasswordChange(Member m)
            : base()
        {
            model = m;
            InitializeComponent();
        }

        private void UserAdd_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                model.Password = "";
                bindingSource1.DataSource = model;
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!model.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }
            bindingSource1.EndEdit();
            int userid = ApplicationData.Instance.GetUserId();
            members.ChangePassword(model);
            OnSave(model);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
