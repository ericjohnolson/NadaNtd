using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.Model;
using Nada.Model.Base;
using Nada.Model.Intervention;

namespace Nada.UI.View
{
    public partial class DiseasePopulationEdit : UserControl
    {
        public event Action<bool> OnSave = (b) => { };
        private DiseasePopulation model = null;
        private DiseaseRepository r = null;
        private DiseaseType creationType;

        public DiseasePopulationEdit()
        {
            InitializeComponent();
        }

        public DiseasePopulationEdit(DiseaseType type)
        {
            creationType = type;
            InitializeComponent();
        }

        private void base_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                openFileDialog1.Filter = "Excel files|*.xlsx;*.xls";
                adminLevelPickerControl1.OnSelect += adminLevelPickerControl1_OnSelect;
                r = new DiseaseRepository();
                if (model == null) model = r.CreatePop(creationType);
                bsPop.DataSource = model;
                customIndicatorControl1.LoadIndicators(model.Indicators.Cast<IDynamicIndicator>());
            }
        }

        void adminLevelPickerControl1_OnSelect(Model.AdminLevel obj)
        {
            model.AdminLevelId = obj.Id;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DiseaseDistributionIndicators inds = new DiseaseDistributionIndicators(model);
            inds.OnSave += editor_OnSave;
            inds.ShowDialog();
        }

        void editor_OnSave()
        {
            customIndicatorControl1.LoadIndicators(model.Indicators.Cast<IDynamicIndicator>());
        }

        /// <summary>
        /// SAVE Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            bsPop.EndEdit();
            model.CustomIndicatorValues = customIndicatorControl1.GetValues<IndicatorValue>();
            int userId = ApplicationData.Instance.GetUserId();
            r.Save(model, userId);
            OnSave(false);
        }

        private void lnkCreateImport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImportDownload form = new ImportDownload(new DiseasePopImporter(model));
            form.ShowDialog();
        }

        private void lnkDoImport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                int userId = ApplicationData.Instance.GetUserId();
                var importer = new DiseasePopImporter(model);
                var result = importer.ImportData(openFileDialog1.FileName, userId);
                if (!result.WasSuccess)
                    MessageBox.Show(result.ErrorMessage);
                else
                    MessageBox.Show(string.Format("Successfully added {0} new records!", result.Count));
            }
        }
    }
}
