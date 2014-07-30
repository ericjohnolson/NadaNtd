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
using Nada.UI.Controls;
using Nada.Model.Demography;
using Nada.Model.Intervention;

namespace Nada.UI.View.Wizard
{

    public partial class SplitIntv : BaseControl, IWizardStep
    {
        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();
        private RedistrictingOptions options = null;
        private IWizardStep prev = null;
        public Action OnFinish { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.Interventions; } }

        public SplitIntv(RedistrictingOptions o, IWizardStep p)
            : base()
        {
            prev = p;
            options = o;
            InitializeComponent();
        }

        public void DoPrev()
        {
            OnSwitchStep(prev);
        }

        public void DoNext()
        {
            OnSwitchStep(new SplitProcesses(options, this));
        }

        public void DoFinish()
        {
        }

        private void StepCategory_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);

                Dictionary<int, IntvType> types = new Dictionary<int, IntvType>();
                foreach (var form in options.Intvs)
                    if (!types.ContainsKey(form.IntvType.Id))
                        types.Add(form.IntvType.Id, form.IntvType);

                if (types.Count == 0)
                    DoNext();

                foreach (var t in types.Values)
                {
                    var index = tblNewUnits.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    var lblName = new H3bLabel { AutoSize = true, Text = t.IntvTypeName, Margin = new Padding(0, 5, 10, 5) };
                    tblNewUnits.Controls.Add(lblName, 0, index);

                    var lnk = new H3Link { Text = Translations.DownloadImportFile, Margin = new Padding(0, 5, 10, 5) };
                    tblNewUnits.Controls.Add(lnk, 1, index); 
                    lnk.ClickOverride += () =>
                    {
                        List<IHaveDynamicIndicatorValues> forms = options.Intvs.Where(s => s.IntvType.Id == t.Id).Cast<IHaveDynamicIndicatorValues>().ToList();
                        IntvImporter importer = new IntvImporter();
                        importer.SetType(t.Id);
                        var payload = new SplitDistro.WorkerPayload
                        {
                            FileName = t.IntvTypeName + "_" + options.SplitType.ToString() + DateTime.Now.ToString("yyyyMMdd") + ".xlsx",
                            Importer = importer,
                            Forms = forms
                        };
                        SplitDistro.CreateDownload(payload);
                    };
                    var lnk2 = new H3Link { Text = Translations.UploadImportFile, Margin = new Padding(0, 5, 10, 5) };
                    tblNewUnits.Controls.Add(lnk2, 2, index);
                    lnk2.ClickOverride += () =>
                    {
                        Upload(t);
                    };
                }
            }
        }
        private void Upload(IntvType type)
        {
            List<IHaveDynamicIndicatorValues> forms = new List<IHaveDynamicIndicatorValues>();
            forms = options.Intvs.Where(d => d.IntvType.Id == type.Id).Cast<IHaveDynamicIndicatorValues>().ToList();
            IntvImporter importer = new IntvImporter();
            importer.SetType(type.Id);
            var payload = new Nada.UI.View.Wizard.SplitDistro.WorkerPayload
            {
                Importer = importer,
                Forms = forms,
            };
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = Translations.ExcelFiles + " (*.xlsx)|*.xlsx";
            ofd.DefaultExt = ".xlsx";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                OnSwitchStep(new WorkingStep(Translations.ImportingFile));
                payload.FileName = ofd.FileName;
                BackgroundWorker importerWorker = new BackgroundWorker();
                importerWorker.DoWork += importerWorker_DoWork;
                importerWorker.RunWorkerCompleted += importerWorker_RunWorkerCompleted;
                importerWorker.RunWorkerAsync(payload);
            }
        }

        void importerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int userId = ApplicationData.Instance.GetUserId();
                Nada.UI.View.Wizard.SplitDistro.WorkerPayload payload = (Nada.UI.View.Wizard.SplitDistro.WorkerPayload)e.Argument;
                ImportResult result = payload.Importer.UpdateData(payload.FileName, userId, payload.Forms);
                if (result.WasSuccess)
                {
                    IntvRepository repo = new IntvRepository();
                    repo.Save(result.Forms.Cast<IntvBase>().ToList(), userId);
                }
                e.Result = result;
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error updating forms during split. SplitIntvs:importerWorker_DoWork. ", ex);
                throw;
            }
        }

        void importerWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ImportResult result = (ImportResult)e.Result;
            if (result.WasSuccess)
            {
                OnSwitchStep(this);
                MessageBox.Show(result.Message);
            }
            else
                OnSwitchStep(new ImportStepResult(result, this, false));
        }




    }
}
