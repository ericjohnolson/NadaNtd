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
using Nada.Model.Diseases;

namespace Nada.UI.View.Wizard
{

    public partial class SplitDistro : BaseControl, IWizardStep
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
        public string StepTitle { get { return Translations.DiseaseDistribution; } }

        public SplitDistro(RedistrictingOptions o, IWizardStep p)
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
            OnSwitchStep(new SplitSurveys(options, this));
        }

        public void DoFinish()
        {
        }

        private void StepCategory_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);


                Dictionary<int, Disease> diseases = new Dictionary<int, Disease>();
                foreach (var distro in options.DistrosCm)
                    if (!diseases.ContainsKey(distro.Disease.Id))
                        diseases.Add(distro.Disease.Id, distro.Disease);
                foreach (var distro in options.DistrosPc)
                    if (!diseases.ContainsKey(distro.Disease.Id))
                        diseases.Add(distro.Disease.Id, distro.Disease);

                if (diseases.Count == 0)
                    DoNext();

                foreach (var disease in diseases.Values.OrderBy(d => d.DisplayName))
                {
                    var index = tblNewUnits.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    var lblName = new H3bLabel { AutoSize = true, Text = disease.DisplayName, Margin = new Padding(0, 5, 10, 5) };
                    tblNewUnits.Controls.Add(lblName, 0, index);

                    var lnk = new H3Link { Text = Translations.DownloadImportFile, Margin = new Padding(0, 5, 10, 5) };
                    tblNewUnits.Controls.Add(lnk, 1, index);
                    lnk.ClickOverride += () =>
                    {
                        List<IHaveDynamicIndicatorValues> forms = new List<IHaveDynamicIndicatorValues>();
                        if (disease.DiseaseType == "CM")
                            forms = options.DistrosCm.Where(d => d.Disease.Id == disease.Id).Cast<IHaveDynamicIndicatorValues>().ToList();
                        else
                            forms = options.DistrosPc.Where(d => d.Disease.Id == disease.Id).Cast<IHaveDynamicIndicatorValues>().ToList();
                        DistroImporter importer = new DistroImporter();
                        importer.SetType(disease.Id);
                        var payload = new WorkerPayload
                        {
                            FileName = Util.CleanFilename(disease.DisplayName) + "_" + options.SplitType.ToString() + DateTime.Now.ToString("yyyyMMdd") + ".xlsx",
                            Importer = importer,
                            Forms = forms
                        };
                        CreateDownload(payload);
                    };
                    var lnk2 = new H3Link { Text = Translations.UploadImportFile, Margin = new Padding(0, 5, 10, 5) };
                    tblNewUnits.Controls.Add(lnk2, 2, index);
                    lnk2.ClickOverride += () =>
                    {
                        Upload(disease);
                    };
                }
            }
        }

        private void Upload(Disease disease)
        {
            List<IHaveDynamicIndicatorValues> forms = new List<IHaveDynamicIndicatorValues>();
            if (disease.DiseaseType == "CM")
                forms = options.DistrosCm.Where(d => d.Disease.Id == disease.Id).Cast<IHaveDynamicIndicatorValues>().ToList();
            else
                forms = options.DistrosPc.Where(d => d.Disease.Id == disease.Id).Cast<IHaveDynamicIndicatorValues>().ToList();
            DistroImporter importer = new DistroImporter();
            importer.SetType(disease.Id);
            var payload = new WorkerPayload
            {
                Importer = importer,
                Forms = forms, 
                DiseaseType = disease.DiseaseType
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
                WorkerPayload payload = (WorkerPayload)e.Argument;
                ImportResult result = payload.Importer.UpdateData(payload.FileName, userId, payload.Forms);
                if (result.WasSuccess)
                {
                    DiseaseRepository repo = new DiseaseRepository();
                    if (payload.DiseaseType == "CM")
                        repo.Save(result.Forms.Cast<DiseaseDistroCm>().ToList(), userId);
                    else
                        repo.Save(result.Forms.Cast<DiseaseDistroPc>().ToList(), userId);
                }
                e.Result = result;
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error updating distribution forms during split. SplitDistro:importerWorker_DoWork. ", ex);
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

        public static void CreateDownload(WorkerPayload payload)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = Translations.ExcelFiles + " (*.xlsx)|*.xlsx";
            sfd.DefaultExt = ".xlsx";
            sfd.FileName = payload.FileName;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                payload.FileName = sfd.FileName;
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerAsync(payload);
            }
        }

        static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WorkerPayload payload = (WorkerPayload)e.Argument;
                payload.Importer.CreateUpdateFile(payload.FileName, payload.Forms);
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error creating splitting review file. SplitDistro:worker_DoWork. ", ex);
                throw;
            }
        }

        public class WorkerPayload
        {
            public string FileName { get; set; }
            public ImporterBase Importer { get; set; }
            public List<IHaveDynamicIndicatorValues> Forms { get; set; }
            public string DiseaseType { get; set; }
        }


    }
}
