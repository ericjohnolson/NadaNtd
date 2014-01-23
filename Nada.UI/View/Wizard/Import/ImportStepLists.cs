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
    public partial class ImportStepLists : BaseControl, IWizardStep
    {
        
        private SurveyRepository surveys = new SurveyRepository();
        private List<DynamicContainer> controlList = new List<DynamicContainer>();
        private SettingsRepository settings = new SettingsRepository();
        private ImportOptions options = null;
        public Action OnFinish { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return true; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return Translations.ImportLists; } }

        public ImportStepLists()
            : base()
        {
            InitializeComponent();
        }

        public ImportStepLists(ImportOptions o, Action onFinish)
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
                List<Indicator> indicators = options.Importer.Indicators.Values.Where(i => i.CanAddValues).ToList();
                if (indicators.Count() > 0)
                    LoadLists(indicators);
                else
                    DoFinish();
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
            options.Importer.CreateImportFile(payload.FileName, options.AdminLevels);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnSwitchStep(new ImportStepType(options));
        }

        private class WorkerPayload
        {
            public string FileName { get; set; }
        }

        private void LoadLists(List<Indicator> indicators)
        {
            this.SuspendLayout();
            tblMetaData.Controls.Clear();
            int count = 0;
            int labelRowIndex = tblMetaData.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int controlRowIndex = tblMetaData.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int columnCount = 0;
            foreach (var indicator in indicators)
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
            }
            this.ResumeLayout();
            tblMetaData.Visible = true;
        }

        private Control CreateIndicatorControl(Indicator indicator, IndicatorEntityType entityType, List<IndicatorDropdownValue> dropdownKeys)
        {
            if (indicator.DataTypeId == (int)IndicatorDataType.Dropdown || indicator.DataTypeId == (int)IndicatorDataType.Multiselect)
                return CreateListBox(indicator, AddValues, AddNewVal, entityType, dropdownKeys.Where(k => k.IndicatorId == indicator.Id).OrderBy(i => i.SortOrder).ToList());
            if (indicator.DataTypeId == (int)IndicatorDataType.EvaluationUnit)
                return CreateListBox(indicator, AddValues, AddNewVal, IndicatorEntityType.EvaluationUnit, settings.GetEvaluationUnits());
            if (indicator.DataTypeId == (int)IndicatorDataType.EcologicalZone)
                return CreateListBox(indicator, AddValues, AddNewVal, IndicatorEntityType.EcologicalZone, settings.GetEcologicalZones());
            if (indicator.DataTypeId == (int)IndicatorDataType.EvalSubDistrict)
                return CreateListBox(indicator, AddValues, AddNewVal, IndicatorEntityType.EvalSubDistrict, settings.GetEvalSubDistricts());
            if (indicator.DataTypeId == (int)IndicatorDataType.SentinelSite)
                return CreateListBox(indicator, AddSites, AddSentinelSite, entityType, dropdownKeys, "SiteName");
            if (indicator.DataTypeId == (int)IndicatorDataType.Partners)
                return CreateListBox(indicator, AddPartners, AddPartner, entityType, dropdownKeys);
            return null;
        }

        public readonly int bottomPadding = 10;

        public Control CreateListBox(Indicator indicator, Action<ListBox, List<IndicatorDropdownValue>> addValues,
            Action<ListBox, Indicator, IndicatorEntityType> onAddValue, IndicatorEntityType entityType, List<IndicatorDropdownValue> values)
        {
            return CreateListBox(indicator, addValues, onAddValue, entityType, values, "DisplayName");
        }
        public Control CreateListBox(Indicator indicator, Action<ListBox, List<IndicatorDropdownValue>> addValues, 
            Action<ListBox, Indicator, IndicatorEntityType> onAddValue, IndicatorEntityType entityType, List<IndicatorDropdownValue> values,
            string displayMember)
        {
            var cntrl = new ListBox { Name = "dynamicMulti" + indicator.Id.ToString(), Width = 220, Height = 100, Margin = new Padding(0, 5, 20, bottomPadding), 
                SelectionMode = SelectionMode.None };
            addValues(cntrl, values);
            cntrl.ValueMember = "Id";
            cntrl.DisplayMember = displayMember;

            cntrl.Margin = new Padding(0, 5, 20, 0);
            TableLayoutPanel tblContainer = new TableLayoutPanel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, AutoScroll = true };
            tblContainer.RowStyles.Clear();
            tblContainer.ColumnStyles.Clear();
            int cRow = tblContainer.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            tblContainer.Controls.Add(cntrl, 0, cRow);
            int lRow = tblContainer.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            var lnk = new H3Link { Text = Translations.AddNewItemLink, Margin = new Padding(0, 5, 3, bottomPadding) };
            lnk.ClickOverride += () =>
            {
                onAddValue(cntrl, indicator, entityType);
            };
            tblContainer.Controls.Add(lnk, 0, lRow);

            return tblContainer;
        }

        private void AddValues(ListBox cntrl, List<IndicatorDropdownValue> values)
        {
            foreach (IndicatorDropdownValue v in values)
                cntrl.Items.Add(v);
        }
        private void AddNewVal(ListBox cntrl, Indicator indicator, IndicatorEntityType entityType)
        {
            IndicatorValueItemAdd form = new IndicatorValueItemAdd(new IndicatorDropdownValue { IndicatorId = indicator.Id, EntityType = entityType }, indicator);
            form.OnSave += (v) =>
            {
                cntrl.Items.Add(v);
            };
            form.ShowDialog();
        }

        private void AddPartners(ListBox cntrl, List<IndicatorDropdownValue> values)
        {
            IntvRepository repo = new IntvRepository();
            cntrl.Items.Clear();
            var partners = repo.GetPartners();
            foreach (var v in partners)
            {
                cntrl.Items.Add(v);
            }
        }
        private void AddPartner(ListBox cntrl, Indicator indicator, IndicatorEntityType entityType)
        {
            PartnerList list = new PartnerList();
            list.OnSave += () =>
            {
                AddPartners(cntrl, null);
            };
            list.ShowDialog();
        }
        private void AddSites(ListBox cntrl, List<IndicatorDropdownValue> values)
        {
            List<SentinelSite> sites = surveys.GetSitesForAdminLevel(options.AdminLevels.Select(a => a.Id.ToString()));
            foreach (var v in sites)
                cntrl.Items.Add(v);
        }
        private void AddSentinelSite(ListBox cntrl, Indicator indicator, IndicatorEntityType entityType)
        {
            SentinelSiteAdd form = new SentinelSiteAdd(options.AdminLevels);
            form.OnSave += (v) =>
            {
                cntrl.Items.Add(v);
            };
            form.ShowDialog();
        }
    }
}
