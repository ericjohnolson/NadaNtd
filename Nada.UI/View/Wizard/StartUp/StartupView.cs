using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Repositories;
using Nada.UI.View.Wizard;
using Nada.Globalization;
using Nada.UI.AppLogic;
using Nada.UI.Controls;
using System.Deployment.Application;
using Nada.Model;
using System.Configuration;
using Nada.UI.Base;
using System.IO;


namespace Nada.UI.View
{
    public partial class StartupView : BaseControl, IView
    {
        public Action OnClose { get; set; }
        public string Title { get { return ""; } }
        public void SetFocus() { }
        public Action<string> StatusChanged { get; set; }
        public Action OnFinished = () => { };
        DemoRepository demo = new DemoRepository();
        SettingsRepository settings = new SettingsRepository();

        public StartupView() : base()
        {
            InitializeComponent();
        }

        private void StartupView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                CheckStatus();
            }
        }
        
        private void LoadWiz(IWizardStep step)
        {
            WizardForm wiz = new WizardForm(step, Translations.StartUpTasks);
            wiz.OnFinish = () => { };
            wiz.OnClose = () =>
            {
                CheckStatus();
            };
            wiz.ShowDialog();
        }

        private void CheckStatus()
        {
            var settings = new SettingsRepository();
            var status = settings.GetStartUpStatus();
            if (!status.ShouldShowStartup())
                OnFinished();

            tblTasks.Visible = false;
            this.SuspendLayout();

            tblTasks.Controls.Clear();

            int rowIndex = tblTasks.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            tblTasks.Controls.Add(CreateLabel("1. " + Translations.StartUpStepCountry, status.HasEnteredCountrySettings), 0, rowIndex);
            var ctrl = new H3Link { Text = status.HasEnteredCountrySettings ? Translations.EditLink : Translations.StartLink, Anchor = AnchorStyles.Bottom | AnchorStyles.Left, Margin = new Padding(3, 3, 3, 5), TextColor = Color.FromArgb(255, 255, 255) };
            ctrl.ClickOverride += () => { LoadWiz(new StepCountrySettings(demo.GetCountry())); };
            tblTasks.Controls.Add(ctrl, 1, rowIndex);

            rowIndex = tblTasks.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            tblTasks.Controls.Add(CreateLabel("2. " + Translations.StartUpStepDiseases, status.HasEnteredDiseaseDetails), 0, rowIndex);
            if (status.HasEnteredCountrySettings)
            {
                var ctrl2 = new H3Link { Text = status.HasEnteredDiseaseDetails ? Translations.EditLink : Translations.StartLink, Anchor = AnchorStyles.Bottom | AnchorStyles.Left, Margin = new Padding(3, 3, 3, 5), TextColor = Color.FromArgb(255, 255, 255) };
                ctrl2.ClickOverride += () => { LoadWiz(new StepDiseases()); };
                tblTasks.Controls.Add(ctrl2, 1, rowIndex);
            }
            bool hasFinishedPrevStep = status.HasEnteredDiseaseDetails;
            var countryDemo = demo.GetCountryDemoRecent();
            int stepCount = 3;
            foreach (var al in status.AdminLevelTypes)
            {
                rowIndex = tblTasks.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                tblTasks.Controls.Add(CreateLabel(stepCount + ". " + Translations.StartUpStepAdminLevels + al.LevelName, al.HasEntered), 0, rowIndex);
                if(hasFinishedPrevStep)
                {
                    var ctrl3 = new H3Link { Text = al.HasEntered ? Translations.EditLink : Translations.StartLink, Anchor = AnchorStyles.Bottom | AnchorStyles.Left, Margin = new Padding(3, 3, 3, 5), TextColor = Color.FromArgb(255, 255, 255) };
                    int levelNumber = al.Level - 1;
                    ctrl3.ClickOverride += () => { LoadWiz(new StepAdminLevelImport(settings.GetNextLevel(levelNumber), null, countryDemo.Id)); };
                    tblTasks.Controls.Add(ctrl3, 1, rowIndex);
                    hasFinishedPrevStep = al.HasEntered;
                }
                stepCount++;
            }


            if (ConfigurationManager.AppSettings["DeveloperMode"] == "QA")
            {
                rowIndex = tblTasks.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                var skipLink = new H3Link { Text = "Skip Start Up", Anchor = AnchorStyles.Bottom | AnchorStyles.Left, Margin = new Padding(3, 3, 3, 5), TextColor = Color.FromArgb(255, 255, 255) };
                skipLink.ClickOverride += skipLink_ClickOverride;
                tblTasks.Controls.Add(skipLink, 0, rowIndex);
            }

            this.ResumeLayout();
            tblTasks.Visible = true;
        }

        private Control CreateLabel(string name, bool isComplete)
        {
            var lbl = new H2LabelLight
            {
                Text = name,
                Name = "ciLabel_" + name,
                TextColor = isComplete ? Color.FromArgb(238, 238, 238) : Color.FromArgb(255, 255, 255)
            };
            lbl.SetMaxWidth(500);
            return lbl;
        }

        private void SetStatus(bool isComplete, bool CanStart, Label lbl, Controls.H3Link startLink, Controls.H3Link editLink)
        {
            editLink.Visible = isComplete;
            startLink.Visible = !isComplete && CanStart;
            lbl.ForeColor = isComplete ? Color.FromArgb(238, 238, 238) : Color.FromArgb(255, 255, 255);
        }

        void skipLink_ClickOverride()
        {
            int year = Convert.ToInt32(ConfigurationManager.AppSettings["SkipStartDemoYear"]);
            int userId = ApplicationData.Instance.GetUserId();
            var c = demo.GetCountry();
            c.Name = "Murkonia";
            demo.UpdateCountry(c, userId);
            demo.Save(new CountryDemography { AdminLevelId = 1, GrowthRate = 9.5, DateDemographyData = new DateTime(year, 1, 1), TotalPopulation = 1, PopSac = 1, PercentAdult = 30, PercentPsac = 20, PercentSac = 50 },
                userId); 
            DiseaseRepository diseases = new DiseaseRepository();
            var availableDiseases = diseases.GetAvailableDiseases();
            diseases.SaveSelectedDiseases(availableDiseases, true, userId);
            settings.SetDiseasesReviewedStatus();
            //Import stuff
            settings.Save(new AdminLevelType { DisplayName = "Village", LevelNumber = 3 }, userId);
            var adminLevels = settings.GetAllAdminLevels();
            var region = adminLevels.FirstOrDefault(a => a.DisplayName == "Region");
            region.DisplayName = "Province";
            settings.Save(region, userId);
            var countryDemo = demo.GetCountryDemoRecent();
            AdminLevelDemoImporter regImporter = new AdminLevelDemoImporter(region, countryDemo.Id);
            regImporter.ImportData("TestProvinces.xlsx", userId, false, false, 4, null, new DateTime(year, 1, 1));
            AdminLevelDemoImporter disImporter = new AdminLevelDemoImporter(adminLevels.FirstOrDefault(a => a.DisplayName == "District"), countryDemo.Id);
            disImporter.ImportData("TestDistricts.xlsx", userId, true, true, 25, null, new DateTime(year, 1, 1));
            AdminLevelDemoImporter vilImporter = new AdminLevelDemoImporter(adminLevels.FirstOrDefault(a => a.DisplayName == "Village"), countryDemo.Id);
            vilImporter.ImportData("TestVillages.xlsx", userId, true, false, 13, demo.GetAdminLevelById(3), new DateTime(year, 1, 1));
            CheckStatus();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "file:///" + Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["HelpFile"]);
            //HelpView help = new HelpView();
            //help.Show();
        }
    }
}
