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
    public partial class UserAdd : BaseForm
    {
        public event Action<Member> OnSave = (e) => { };
        private Member model = new Member();
        private MemberRepository members = new MemberRepository();

        public UserAdd()
            : base()
        {
            InitializeComponent();
        }

        public UserAdd(Member m)
            : base()
        {
            model = m;
            InitializeComponent();
        }

        private void UserAdd_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                bindingSource1.DataSource = model;
                lblLastUpdated.Text =  model.UpdatedBy;
                if (model.Id > 0)
                {
                    tbUsername.Enabled = false;
                    tbPassword.Visible = false;
                    lblPwd.Visible = false;
                }
                else
                    lnkChangePassword.Visible = false;

                rolePickerControl1.LoadLists(model);
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!model.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }
            if(model.Id == 0 && members.GetAllUsers().FirstOrDefault(u => u.Username == model.Username) != null)
            {
                MessageBox.Show(Translations.ValidationUserName, Translations.ValidationErrorTitle);
                return;
            }

            bindingSource1.EndEdit();
            model.SelectedRoles = rolePickerControl1.GetSelectedItems();
            int userid = ApplicationData.Instance.GetUserId();
            members.Save(model, userid);
            OnSave(model);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkChangePassword_ClickOverride()
        {
            UserPasswordChange pwd = new UserPasswordChange(model);
            pwd.OnSave += pwd_OnSave;
            pwd.ShowDialog();
        }

        void pwd_OnSave(Member obj)
        {
            OnSave(model);
            this.Close();
        }

    }
}
