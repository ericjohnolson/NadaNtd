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

namespace Nada.UI.View.Wizard
{
    public partial class ImportStepType : UserControl, IWizardStep
    {
        private IImporter importer = null;
        public Action OnFinish { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.ImportAction; } }

        public ImportStepType()
        {
            InitializeComponent();
        }

        public ImportStepType(IImporter i)
        {
            importer = i;
            InitializeComponent();
        }
        
        public void DoPrev()
        {
        }
        public void DoNext()
        {
        }
        public void DoFinish()
        {
        }

        private void StepCategory_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                openFileDialog1.Filter = "Excel files|*.xlsx;*.xls";
            }
        }


        private void lnkDownload_ClickOverride()
        {

            OnSwitchStep(new ImportStepOptions(importer, OnFinish));
        }

        private void lnkUpload_ClickOverride()
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                int userId = ApplicationData.Instance.GetUserId();
                var result = importer.ImportData(openFileDialog1.FileName, userId);
                if (!result.WasSuccess)
                    MessageBox.Show(result.ErrorMessage);
                else
                    MessageBox.Show(string.Format(Translations.ImportSuccess, result.Count));
            }
        }

    }
}
