using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Csv;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;
using Nada.UI.ViewModel;
using OfficeOpenXml;

namespace Nada.UI.View.Wizard
{
    public partial class StepDemoLevel : BaseControl, IWizardStep
    {
        private SettingsRepository repo = new SettingsRepository();
        public Action OnFinish { get; set; }
        public Action OnSkip { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.AdminLevelType; } }

        public StepDemoLevel()
            : base()
        {
            InitializeComponent();
        }
        
        private void ImportOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                h3Label1.SetMaxWidth(300);
                var types = repo.GetAllAdminLevels();
                var aggregating = types.First(a => a.IsAggregatingLevel);
                var aggAndAbove = types.Where(a => a.LevelNumber >= aggregating.LevelNumber);
                bsLevels.DataSource = aggAndAbove;
                cbLevels.SelectedIndex = 0;
            }
        }

        public void DoPrev()
        {
        }

        public void DoNext()
        {
            if (cbLevels.SelectedItem == null)
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }
            AdminLevelType alt = (AdminLevelType)cbLevels.SelectedItem;
            OnSwitchStep(new StepAdminLevelImport(alt, this, dateTimePicker1.Value, true));
        }

        public void DoFinish()
        {
        }

    }
}
