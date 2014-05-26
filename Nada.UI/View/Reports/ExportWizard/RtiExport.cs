using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.Model.Reports;
using Nada.Globalization;
using Nada.Model;
using Nada.UI.View.Wizard;
using System.Threading;
using Nada.UI.Base;
using Nada.Model.Repositories;
using Nada.Model.Exports;

namespace Nada.UI.View.Reports
{
    public partial class RtiExport : BaseControl, IWizardStep
    {
        private RtiWorkbooksExporter exporter = null;
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.RtiWorkbookQuestions; } }

        public RtiExport()
            : base()
        {
            InitializeComponent();
        }

        private void ExportWorkingStep_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                SettingsRepository repo = new SettingsRepository();
                exporter = new RtiWorkbooksExporter();
                exporter.StartDate = DateTime.Now.AddYears(-1);
                exporter.EndDate = DateTime.Now;
                var allLevelTypes = repo.GetAllAdminLevels();
                var reportingType = allLevelTypes.First(t => t.IsDistrict);
                exporter.AdminLevelType = reportingType;
                bindingSource1.DataSource = allLevelTypes;
                cbTypes.SelectedIndex = allLevelTypes.IndexOf(reportingType);
                bindingSource2.DataSource = exporter;
            }
        }

        public void DoPrev()
        {
        }
        public void DoNext()
        {
            bindingSource1.EndEdit();
            ExportRepository r = new ExportRepository();
            ExportType exportType = r.GetExportType(ExportTypeId.RtiWorkbooks);
            exportType.Exporter = exporter;
            OnSwitchStep(new GenericExportStep(exportType, Translations.RtiWorkbookQuestions));
        }
        public void DoFinish()
        {
        }

    }
}
