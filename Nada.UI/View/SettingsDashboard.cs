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
        private Country country = null;
        private MemberRepository members = new MemberRepository();
        private SettingsRepository settings = new SettingsRepository();
        private DiseaseRepository diseases = new DiseaseRepository();
        private DemoRepository demo = new DemoRepository();
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
                country = demo.GetCountry();
                countryView1.LoadCountry(country);
                diseasePickerControl1.LoadLists(true);
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

        private void c1Button2_Click(object sender, EventArgs e)
        {
            DeleteConfirm confirm = new DeleteConfirm(Translations.Restore, Translations.RestoreConfirm);
            if (confirm.ShowDialog() == DialogResult.OK)
            {
                string localAppData =
                    Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData);
                string uPath = Path.Combine(localAppData, "IotaInk");
                string backupFile = Path.Combine(uPath, "DatabaseBackup.accdb");
                File.Copy(backupFile, DatabaseData.Instance.FilePath, true);
                OnClose();
            }
        }

        private void saveCountry_Click(object sender, EventArgs e)
        {
            countryView1.DoValidate();
            if (!country.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }
            if (!adminLevelTypesControl1.HasDistrict())
            {
                MessageBox.Show(Translations.MustMakeDistrict, Translations.ValidationErrorTitle);
                return;
            }
            if (!adminLevelTypesControl1.HasAggregatingLevel())
            {
                MessageBox.Show(Translations.MustMakeAggregatingLevel, Translations.ValidationErrorTitle);
                return;
            }

            int userId = ApplicationData.Instance.GetUserId();
            demo.UpdateCountry(country, userId);
            OnClose();
        }

        private void btnSaveDiseases_Click(object sender, EventArgs e)
        {
            var selected = diseasePickerControl1.GetSelectedItems();
            var available = diseasePickerControl1.GetUnselectedItems();
            int userId = ApplicationData.Instance.GetUserId();
            diseases.SaveSelectedDiseases(selected, true, userId);
            diseases.SaveSelectedDiseases(available, false, userId);
            OnClose();
        }

        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sourceFilePath = Path.Combine(
                  System.Windows.Forms.Application.StartupPath, "NationalDatabaseLog.log");
                File.Copy(sourceFilePath, saveFileDialog1.FileName, true);
                System.Diagnostics.Process.Start("notepad.exe", saveFileDialog1.FileName);
            }
        }
    }
}
