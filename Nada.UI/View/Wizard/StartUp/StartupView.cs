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

namespace Nada.UI.View
{
    public partial class StartupView : UserControl
    {
        public Action OnFinished = () => { };
        DemoRepository demo = new DemoRepository();
        SettingsRepository settings = new SettingsRepository();

        public StartupView()
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
            wiz.OnFinish = () =>
            {
                CheckStatus();
            };
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
            var ctrl = new H3Link { Text = status.HasEnteredCountrySettings ? Translations.EditLink : Translations.StartLink, Anchor = AnchorStyles.Bottom | AnchorStyles.Left, Margin = new Padding(3, 3, 3, 5) };
            ctrl.ClickOverride += () => { LoadWiz(new StepCountrySettings(demo.GetCountry())); };
            tblTasks.Controls.Add(ctrl, 1, rowIndex);

            rowIndex = tblTasks.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            tblTasks.Controls.Add(CreateLabel("2. " + Translations.StartUpStepDiseases, status.HasEnteredDiseaseDetails), 0, rowIndex);
            if (status.HasEnteredCountrySettings)
            {
                var ctrl2 = new H3Link { Text = status.HasEnteredDiseaseDetails ? Translations.EditLink : Translations.StartLink, Anchor = AnchorStyles.Bottom | AnchorStyles.Left, Margin = new Padding(3, 3, 3, 5) };
                ctrl2.ClickOverride += () => { LoadWiz(new StepDiseases()); };
                tblTasks.Controls.Add(ctrl2, 1, rowIndex);
            }
            bool hasFinishedPrevStep = status.HasEnteredDiseaseDetails;
            int stepCount = 3;
            foreach (var al in status.AdminLevelTypes)
            {
                rowIndex = tblTasks.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                tblTasks.Controls.Add(CreateLabel(stepCount + ". " + Translations.StartUpStepAdminLevels + al.LevelName, al.HasEntered), 0, rowIndex);
                if(hasFinishedPrevStep)
                {
                    var ctrl3 = new H3Link { Text = al.HasEntered ? Translations.EditLink : Translations.StartLink, Anchor = AnchorStyles.Bottom | AnchorStyles.Left, Margin = new Padding(3, 3, 3, 5) };
                    ctrl3.ClickOverride += () => { LoadWiz(new StepAdminLevelImport(settings.GetNextLevel(al.Level - 1), null)); };
                    tblTasks.Controls.Add(ctrl3, 1, rowIndex);
                    hasFinishedPrevStep = al.HasEntered;
                }
                stepCount++;
            }

            bool skipStep = true; // !ApplicationDeployment.IsNetworkDeployed
            if (skipStep)
            {
                rowIndex = tblTasks.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize }); 
                var skipLink = new H3Link { Text = "Skip Start Up", Anchor = AnchorStyles.Bottom | AnchorStyles.Left, Margin = new Padding(3, 3, 3, 5) };
                skipLink.ClickOverride += skipLink_ClickOverride;
                tblTasks.Controls.Add(skipLink, 0, rowIndex);
            }

            this.ResumeLayout();
            tblTasks.Visible = true;
        }

        private Control CreateLabel(string name, bool isComplete)
        {
            var lbl = new H2Label
            {
                Text = name,
                Name = "ciLabel_" + name,
                TextColor = isComplete ? Color.FromArgb(190, 190, 190) : Color.FromArgb(23, 55, 93)
            };
            lbl.SetMaxWidth(500);
            return lbl;
        }

        private void SetStatus(bool isComplete, bool CanStart, Label lbl, Controls.H3Link startLink, Controls.H3Link editLink)
        {
            editLink.Visible = isComplete;
            startLink.Visible = !isComplete && CanStart;
            lbl.ForeColor = isComplete ? Color.FromArgb(190, 190, 190) : Color.FromArgb(23, 55, 93);
        }

        void skipLink_ClickOverride()
        {
            int userId = ApplicationData.Instance.GetUserId();
            var c = demo.GetCountry();
            c.Name = "Test Country Name";
            demo.UpdateCountry(c, userId);
            settings.SetDiseasesReviewedStatus();
            //Import stuff
            settings.Save(new AdminLevelType { DisplayName = "Village", LevelNumber = 3 }, userId);
            var adminLevels = settings.GetAllAdminLevels();
            AdminLevelDemoImporter regImporter = new AdminLevelDemoImporter(adminLevels.FirstOrDefault(a => a.DisplayName == "Region"));
            regImporter.ImportData("TestRegions.xlsx", userId, false, false, 2, null, 2012);
            AdminLevelDemoImporter disImporter = new AdminLevelDemoImporter(adminLevels.FirstOrDefault(a => a.DisplayName == "District"));
            disImporter.ImportData("TestDistricts.xlsx", userId, true, true, 4, null, 2012);
            AdminLevelDemoUpdater update = new AdminLevelDemoUpdater(adminLevels.FirstOrDefault(a => a.DisplayName == "District"));
            update.ImportData("TestDistrictUpdate.xlsx", userId, 2011, true);
            AdminLevelDemoImporter vilImporter = new AdminLevelDemoImporter(adminLevels.FirstOrDefault(a => a.DisplayName == "Village"));
            vilImporter.ImportData("TestVillages.xlsx", userId, true, false, 2, demo.GetAdminLevelById(2), 2012);
            CheckStatus();
        }
    }
}
