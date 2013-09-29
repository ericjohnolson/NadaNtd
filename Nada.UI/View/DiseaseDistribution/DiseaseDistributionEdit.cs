using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Repositories;
using Nada.Model.Intervention;
using Nada.UI.AppLogic;
using Nada.Model.Diseases;

namespace Nada.UI.View
{
    public partial class DiseaseDistributionEdit : Form
    {
        public event Action OnSave = () => { };
        private DiseaseRepository repo = null;
        private DiseaseDistribution model = null;

        public DiseaseDistributionEdit()
        {
            InitializeComponent();
        }

        public DiseaseDistributionEdit(DiseaseDistribution m)
        {
            model = m;
            InitializeComponent();
        }

        private void DiseaseDistributionEdit_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                repo = new DiseaseRepository();
                openFileDialog1.Filter = "Excel files|*.xlsx;*.xls";
                bsDiseaseDistribution.DataSource = model;
                customIndicatorControl1.LoadIndicators(model.Indicators);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            bsDiseaseDistribution.EndEdit();
            model.CustomIndicatorValues = customIndicatorControl1.GetValues();
            int currentUser = ApplicationData.Instance.GetUserId();
            repo.Save(model, currentUser);
            OnSave();
            this.Close();
        }

        private void lnkIndicators_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DiseaseDistributionIndicators modal = new DiseaseDistributionIndicators(model);
            modal.OnSave += editor_OnSave;
            modal.ShowDialog();
        }

        void editor_OnSave()
        {
            customIndicatorControl1.LoadIndicators(model.Indicators);
        }

        private void lnkCreateImport_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImportDownload form = new ImportDownload(new DiseaseDistributionImporter(model));
            form.ShowDialog();
        }

        private void lnkDoImport_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                int userId = ApplicationData.Instance.GetUserId();
                var importer = new DiseaseDistributionImporter(model);
                var result = importer.ImportData(openFileDialog1.FileName, userId);
                if (!result.WasSuccess)
                    MessageBox.Show(result.ErrorMessage);
                else
                    MessageBox.Show(string.Format("Successfully added {0} new records!", result.Count));
            }
        }
    }
}
