using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;

using Nada.UI.View.Reports.CustomReport;
using Nada.Model.Reports;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Exports;
using Nada.UI.Base;
using Nada.Model.Repositories;
using System.IO;
using System.Configuration;

namespace Nada.UI.View
{
    public partial class SettingsDashboard : BaseControl, IView
    {
        private MemberRepository members = new MemberRepository();
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }
        public string Title { get { return ""; } }

        public void SetFocus()
        {
        }

        public SettingsDashboard()
            : base()
        {
            InitializeComponent();
        }
        
        private void Settings_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                lvUsers.SetObjects(members.GetAllUsers());
            }
        }

        private void btnDash_Click(object sender, EventArgs e)
        {
            OnClose();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "file:///" + Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["HelpFile"]);
            //HelpView help = new HelpView();
            //help.Show();
        }


        private void lnkAddUser_ClickOverride()
        {
            UserAdd form = new UserAdd();
            form.OnSave += form_OnSave;
            form.ShowDialog();
        }

        private void lvUsers_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {

            e.Handled = true;
            if (e.Column.AspectName == "View")
            {
                UserAdd form = new UserAdd((Member)e.Model);
                form.OnSave += form_OnSave;
                form.ShowDialog();
            }
            else if (e.Column.AspectName == "Delete")
            {
                DeleteConfirm confirm = new DeleteConfirm();
                if (confirm.ShowDialog() == DialogResult.OK)
                {
                    members.Delete((Member)e.Model);
                }
            }
        }

        void form_OnSave(Member obj)
        {
            lvUsers.SetObjects(members.GetAllUsers());
        }
    }
}
