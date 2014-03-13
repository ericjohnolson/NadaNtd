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
    public partial class StepDdReview : BaseControl, IWizardStep
    {
        string stepTitle = "";
        private DdUpdateViewModel vm = new DdUpdateViewModel();
        DemoRepository repo = new DemoRepository();
        IWizardStep prev = null;
        public Action OnFinish { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
        public bool ShowNext { get { return vm.DiseaseStepNumber < vm.Diseases.Count - 1; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return prev != null; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return vm.DiseaseStepNumber == vm.Diseases.Count - 1; ; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return stepTitle; } }

        public StepDdReview() 
            : base()
        {
            InitializeComponent();
        }

        public StepDdReview(DdUpdateViewModel v, IWizardStep p)
            : base()
        {
            vm = v;
            prev = p;
            SettingsRepository settings = new SettingsRepository();
            stepTitle = Translations.UpdateDiseaseDistro + " - " + vm.Diseases[vm.DiseaseStepNumber].DisplayName;
            InitializeComponent();
        }

        private void ImportOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                saveFileDialog1.FileName = stepTitle;
            }
        }

        public void DoPrev()
        {
            vm.DiseaseStepNumber--;
            OnSwitchStep(prev);
        }

        public void DoNext()
        {
            vm.DiseaseStepNumber++;
            OnSwitchStep(new StepDdReview(vm, this));
        }

        public void DoFinish()
        {
            OnFinish();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var worker = new BackgroundWorker();
                worker.DoWork += dl_DoWork;
                worker.RunWorkerCompleted += dl_RunWorkerCompleted;
                ShowLoading(true);
                worker.RunWorkerAsync(new Payload
                {
                    Filename = saveFileDialog1.FileName
                });
            }
        }

        void dl_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ShowLoading(false);
            ImportResult result = (ImportResult)e.Result;
            tbStatus.Text = result.Message;

            if (!result.WasSuccess)
                MessageBox.Show(Translations.ImportFailed, Translations.ErrorOccured, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void dl_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Payload payload = (Payload)e.Argument;
                //updater.CreateUpdateFile(payload.Filename);
                e.Result = new ImportResult { WasSuccess = true };
            }
            catch (Exception ex)
            {
                e.Result = new ImportResult(Translations.UnexpectedException + " " + ex.Message);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var worker = new BackgroundWorker();
                worker.DoWork += up_DoWork;
                worker.RunWorkerCompleted += up_RunWorkerCompleted;
                ShowLoading(true);
                worker.RunWorkerAsync(new Payload
                {
                    Filename = openFileDialog1.FileName
                });
            }
        }

        private void up_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ShowLoading(false);
            ImportResult result = (ImportResult)e.Result;
            tbStatus.Text = result.Message;

            if (!result.WasSuccess) //MessageBox.Show(Translations.ImportComplete, Translations.ImportComplete);
                MessageBox.Show(Translations.ImportFailed, Translations.ErrorOccured, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void up_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Payload payload = (Payload)e.Argument;
                ImportResult result = null;

                    //result = importer.ImportData(payload.Filename, ApplicationData.Instance.GetUserId(),
                    //    payload.DoDemography, payload.DoAggregate, payload.RowCount, payload.FilteredBy, payload.Year);
                
                e.Result = result;
            }
            catch (Exception ex)
            {
                e.Result = new ImportResult(Translations.UnexpectedException + " " + ex.Message);
            }
        }

        private void ShowLoading(bool p)
        {
            btnDownload.Visible = !p;
            btnImport.Visible = !p;
            loadingImport.Visible = p;
        }

        public class Payload
        {
            public string Filename { get; set; }
            public DdUpdateViewModel ViewModel { get; set; }
        }
    }
}
