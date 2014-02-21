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
using System.IO;
using System.Deployment.Application;
using System.Configuration;
using Nada.Model;
using System.Runtime.Serialization.Formatters.Binary;
using Nada.Globalization;


namespace Nada.UI.View
{
    public partial class DatabaseView : BaseControl, IView
    {
        public Action<string> StatusChanged { get; set; }
        public Action OnClose { get; set; }
        public string Title { get { return ""; } }
        public void SetFocus() { }
        private string NewFileLocation = "";
        private string RecentFilesPath = "";
        private string UserFilesPath = "";
        private string RecentFileName = "";
        private List<RecentFile> RecentFiles = new List<RecentFile>();
        public event Action OnFileSelected = () => { };

        public DatabaseView()
            : base()
        {
            InitializeComponent();
        }

        private void DatabaseView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                SetUpFiles();
                List<Language> langz = Localizer.SupportedLanguages;
                bsLanguages.DataSource = langz;
                if (langz.FirstOrDefault(l => l.IsoCode == Thread.CurrentThread.CurrentCulture.Name) != null)
                    cbLanguages.SelectedValue = Thread.CurrentThread.CurrentCulture.Name;
                else
                    cbLanguages.SelectedValue = "en-US";

                Localizer.TranslateControl(this);
                cbLanguages.DropDownWidth = BaseForm.GetDropdownWidth(Localizer.SupportedLanguages.Select(l => l.Name));
                if (!RecentFileExists())
                {
                    c1Button1.Visible = false;
                    lblRecentFile.Visible = false;
                }
                lblRecentFile.Text = Translations.RecentFile + RecentFileName;
                openFileDialog1.Filter = Translations.AccessFiles + "|*.accdb";
                saveFileDialog1.Filter = Translations.AccessFiles + " (*.accdb)|*.accdb";
                saveFileDialog1.DefaultExt = ".accdb";
                label1.MaximumSize = new Size(900, 0);
                label1.AutoSize = true;
            }
        }

        private bool RecentFileExists()
        {
            foreach (RecentFile f in RecentFiles)
            {
                if (File.Exists(f.Path))
                {
                    RecentFileName = f.Name.Trim();
                    return true;
                }
            }
            return false;
        }

        private void cbLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLanguages.SelectedValue == null)
                return;

            CultureInfo ci = new CultureInfo(cbLanguages.SelectedValue.ToString());
            Localizer.SetCulture(ci);
            Localizer.TranslateControl(this);
            lblRecentFile.Text = Translations.RecentFile + RecentFileName;
            this.ParentForm.Text = Localizer.GetValue("ApplicationTitle");
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFile(RecentFiles.First().Path);
        }

        private void lnkNew_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.Copy(NewFileLocation, saveFileDialog1.FileName, true);
                OpenFile(saveFileDialog1.FileName);
            }
        }

        private void lnkBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OpenFile(openFileDialog1.FileName);
            }
        }

        private void OpenFile(string file)
        {
            CultureInfo ci = new CultureInfo(cbLanguages.SelectedValue.ToString());
            Localizer.SetCulture(ci);
            SetDatabaseConnection(file);
            AddRecentFile(file);
            OnFileSelected();
        }

        private void SetUpFiles()
        {
            string localAppData =
                Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData);
            UserFilesPath = Path.Combine(localAppData, "IotaInk");
            RecentFilesPath = Path.Combine(UserFilesPath, "NadaRecentFiles.txt");

            if (!Directory.Exists(UserFilesPath))
                Directory.CreateDirectory(UserFilesPath);

            // ALWAYS COPY BIN TO LOCAL STORAGE
            string sourceFilePath = Path.Combine(
              System.Windows.Forms.Application.StartupPath, "NewNationalDatabaseTemplate.accdb");
            NewFileLocation = Path.Combine(UserFilesPath, "NewNationalDatabaseTemplate.accdb");
            File.Copy(sourceFilePath, NewFileLocation, true);

            // Get Recent File
            if (!File.Exists(RecentFilesPath))
                CreateRecentFiles();
            try
            {
                BinaryFormatter serializer = new BinaryFormatter();
                using (Stream stream = new FileStream(RecentFilesPath, FileMode.Open))
                {
                    RecentFiles = (List<RecentFile>)serializer.Deserialize(stream);
                    stream.Close();
                }
            }
            catch (Exception)
            {
                CreateRecentFiles();
            }
        }

        private void CreateRecentFiles()
        {
            BinaryFormatter serializer = new BinaryFormatter();
            using (Stream stream = new FileStream(RecentFilesPath, FileMode.Create))
            {
                RecentFiles = new List<RecentFile>();
                serializer.Serialize(stream, RecentFiles);
                stream.Close();
            }
        }

        public void AddRecentFile(string fileName)
        {
            BinaryFormatter serializer = new BinaryFormatter();
            FileInfo file = new FileInfo(fileName);
            RecentFiles.RemoveAll(r => r.Path == fileName);
            RecentFiles.Insert(0, new RecentFile { Name = file.Name, Path = fileName });
            RecentFiles = RecentFiles.Take(10).ToList();

            using (FileStream fileStream = new FileStream(RecentFilesPath, FileMode.Create))
            {
                serializer.Serialize(fileStream, RecentFiles);
                fileStream.Close();
            }
        }

        private void SetDatabaseConnection(string filePath)
        {
            string connection = ConfigurationManager.ConnectionStrings["AccessFileName"].ConnectionString;
            DatabaseData.Instance.AccessConnectionString = connection.Replace("Source=NewNationalDatabaseTemplate.accdb", "Source=" + filePath);
            DatabaseData.Instance.FilePath = filePath;
            string backupFile = Path.Combine(UserFilesPath, "DatabaseBackup.accdb");
            File.Copy(filePath, backupFile, true);
        }

        [Serializable]
        public class RecentFile
        {
            public string Name { get; set; }
            public string Path { get; set; }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "file:///" + Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["HelpFile"]);
            //HelpView help = new HelpView();
            //help.Show();
        }
    }
}
