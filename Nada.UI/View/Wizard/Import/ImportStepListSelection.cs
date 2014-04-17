using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.Globalization;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.Model;
using Nada.UI.Base;
using Nada.Model.Imports;
using Nada.UI.ViewModel;
using Nada.UI.Controls;
using Nada.Model.Survey;

namespace Nada.UI.View.Wizard
{
    public partial class ImportStepListSelection : BaseControl, IWizardStep
    {

        private SurveyRepository surveys = new SurveyRepository();
        private List<DynamicContainer> controlList = new List<DynamicContainer>();
        private SettingsRepository settings = new SettingsRepository();
        private List<KeyValuePair<Indicator, PickerControl>> controls = new List<KeyValuePair<Indicator, PickerControl>>();
        private ImportOptions options = null;
        public Action OnFinish { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return true; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return Translations.ImportListsSelect; } }

        public ImportStepListSelection()
            : base()
        {
            InitializeComponent();
        }

        public ImportStepListSelection(ImportOptions o, Action onFinish)
            : base()
        {
            options = o;
            OnFinish = onFinish;
            InitializeComponent();
        }

        private void ImportStepLists_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                saveFileDialog1.Filter = Translations.ExcelFiles + " (*.xlsx)|*.xlsx";
                saveFileDialog1.FileName = options.Importer.ImportName;
                saveFileDialog1.DefaultExt = ".xlsx";
                LoadLists(options.Importer.Indicators);
            }
        }

        public void DoPrev()
        {
            OnSwitchStep(new ImportStepOptions(options, OnFinish));
        }

        public void DoNext()
        {
        }

        public void DoFinish()
        {
            options.IndicatorValuesSublist = new Dictionary<string, List<string>>();
            foreach (var c in controls)
            {
                var vals = c.Value.GetSelectedItems();
                if (vals.Count == 0)
                    options.IndicatorValuesSublist.Add(c.Key.DisplayName, new List<string>());
                else if(c.Key.DataTypeId == (int)IndicatorDataType.Partners)
                    options.IndicatorValuesSublist.Add(c.Key.DisplayName, vals.Cast<Partner>().Select(p => p.DisplayName).ToList());
                else
                    options.IndicatorValuesSublist.Add(c.Key.DisplayName, vals.Cast<IndicatorDropdownValue>().Select(p => p.DisplayName).ToList());
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.DoWork += worker_DoWork;
                worker.RunWorkerAsync(new WorkerPayload { FileName = saveFileDialog1.FileName });

                OnSwitchStep(new WorkingStep(Translations.CreatingImportFileStatus));
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerPayload payload = (WorkerPayload)e.Argument;
            options.Importer.CreateImportFile(payload.FileName, options.AdminLevels, options.AdminLevelType, options);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnSwitchStep(new ImportStepType(options));
        }

        private class WorkerPayload
        {
            public string FileName { get; set; }
        }

        private void LoadLists(Dictionary<string, Indicator> indicators)
        {
            this.SuspendLayout();
            tblMetaData.Controls.Clear();
            int count = 0;
            int labelRowIndex = tblMetaData.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int controlRowIndex = tblMetaData.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int columnCount = 0;
            foreach (var indicator in indicators.Values)
            {
                var cntrl = CreateIndicatorControl(indicator, options.Importer.EntityType, options.Importer.DropDownValues);
                if (cntrl == null)
                    continue;

                if (count % 1 == 0)
                {
                    labelRowIndex = tblMetaData.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    controlRowIndex = tblMetaData.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    columnCount = 0;
                }

                // Add label
                tblMetaData.Controls.Add(ControlFactory.CreateLabel(indicator, true), columnCount, labelRowIndex);

                // Add field
                cntrl.TabIndex = count;
                tblMetaData.Controls.Add(cntrl, columnCount, controlRowIndex);

                count++;
                columnCount = columnCount + 2;
                controls.Add(new KeyValuePair<Indicator, PickerControl>(indicator, cntrl));
            }

            if (controls.Count == 0)
                DoFinish();

            this.ResumeLayout();
            tblMetaData.Visible = true;
        }

        private PickerControl CreateIndicatorControl(Indicator indicator, IndicatorEntityType entityType, List<IndicatorDropdownValue> dropdownKeys)
        {
            if (indicator.DataTypeId == (int)IndicatorDataType.Multiselect)
                return CreatePickerBox(indicator, entityType, dropdownKeys.Where(k => k.IndicatorId == indicator.Id).OrderBy(i => i.SortOrder).Cast<object>().ToList());
            if (indicator.DataTypeId == (int)IndicatorDataType.Partners)
            {
                IntvRepository repo = new IntvRepository();
                return CreatePickerBox(indicator, entityType, repo.GetPartners().Cast<object>().ToList());
            }
            return null;
        }

        public PickerControl CreatePickerBox(Indicator indicator, IndicatorEntityType entityType, List<object> values)
        {
            if (values.Count < 7)
                return null;

            var cntrl = new PickerControl();
            cntrl.LoadLists(values, "DisplayName");
            cntrl.Margin = new Padding(0, 5, 20, 0);
            return cntrl;
        }
    }
}
