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
using Nada.Model.Imports;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;
using OfficeOpenXml;

namespace Nada.UI.View.Wizard
{
    public partial class ImportStepOptions : BaseControl, IWizardStep
    {
        private List<AdminLevel> available = new List<AdminLevel>();
        private List<AdminLevel> selected = new List<AdminLevel>();
        private ImportOptions options;
        public Action OnFinish { get; set; }

        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.ChooseAdminLevels; } }
        
        public ImportStepOptions(ImportOptions o, Action onFinish)
            : base()
        {
            options = o;
            OnFinish = onFinish;
            InitializeComponent();
        }

        private void ImportOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
            }
        }

        public void DoPrev()
        {
            OnSwitchStep(new ImportStepType(options));
        }

        public void DoNext()
        {
            var selected = adminLevelMultiselect1.GetSelectedAdminLevels();
            if (selected.Count == 0)
            {
                MessageBox.Show(Translations.LocationRequired, Translations.ValidationErrorTitle);
                return;
            }

            options.AdminLevels = selected;
            options.AdminLevelType = adminLevelMultiselect1.SelectedAdminLevelType;
            OnSwitchStep(new ImportStepLists(options, OnFinish));
        }

        public void DoFinish()
        {

        }
    }
}
