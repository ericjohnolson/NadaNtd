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

namespace Nada.UI.View.Intervention
{
    public partial class LfMdaView : UserControl
    {
        public event Action<bool> OnSave = (b) => { };
        private LfMda model = null;
        private IntvRepository r = null;

        public LfMdaView()
        {
            InitializeComponent();
        }

        public LfMdaView(LfMda Intv)
        {
            this.model = Intv;
            InitializeComponent();
        }

        private void base_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                openFileDialog1.Filter = "Excel files|*.xlsx;*.xls";
                adminLevelPickerControl1.OnSelect += adminLevelPickerControl1_OnSelect;
                r = new IntvRepository();
                if (model == null) model = r.CreateIntv<LfMda>(StaticIntvType.LfMda);
                LoadDistributionList();
                partnerBindingSource.DataSource = r.GetPartners();
                foreach (var p in model.Partners)
                    lbPartners.SelectedItem = p;
                medicineBindingSource.DataSource = r.GetMedicines();
                foreach (var m in model.Medicines)
                    lbMeds.SelectedItem = m;
                bsIntv.DataSource = model;
                bsType.DataSource = model.IntvType;
                customIndicatorControl1.LoadIndicators(model.IntvType.Indicators.Cast<IDynamicIndicator>());
            }
        }

        private void LoadDistributionList()
        {
            var distros = r.GetDistributionMethods();
            distros.Insert(0, new DistributionMethod { DisplayName = "Please Select", Id = -1 });
            distributionMethodBindingSource.DataSource = distros;
        }

        void adminLevelPickerControl1_OnSelect(Model.AdminLevel obj)
        {
            model.AdminLevelId = obj.Id;
        }

        /// <summary>
        /// SAVE Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            bsIntv.EndEdit();
            model.Partners.Clear();
            foreach (var p in lbPartners.SelectedItems)
                model.Partners.Add(p as Partner);
            model.Medicines.Clear();
            foreach (var m in lbMeds.SelectedItems)
                model.Medicines.Add(m as Medicine);
            model.CustomIndicatorValues = customIndicatorControl1.GetValues<IndicatorValue>();
            int userId = ApplicationData.Instance.GetUserId();
            r.Save(model, userId);
            MessageBox.Show("Intervention was saved!");
            OnSave(false);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DistributionMethodList list = new DistributionMethodList();
            list.OnSave += () => { LoadDistributionList(); };
            list.ShowDialog();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PartnerList list = new PartnerList();
            list.OnSave += () => { partnerBindingSource.DataSource = r.GetPartners(); };
            list.ShowDialog();
        }

        private void linkMeds_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MedicineList list = new MedicineList();
            list.OnSave += () => { medicineBindingSource.DataSource = r.GetMedicines(); };
            list.ShowDialog();
        }

        void editor_OnSave()
        {
            customIndicatorControl1.LoadIndicators(model.IntvType.Indicators.Cast<IDynamicIndicator>());
            bsType.ResetBindings(false);
        }

        private void lnkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            IntvTypeEdit editor = new IntvTypeEdit(model.IntvType);
            editor.OnSave += editor_OnSave;
            editor.ShowDialog();
        }

        private void lnkCreateImport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImportDownload form = new ImportDownload(new LfMdaImporter(model.IntvType));
            form.ShowDialog();
        }

        private void lnkDoImport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                int userId = ApplicationData.Instance.GetUserId();
                var importer = new LfMdaImporter(model.IntvType);
                var result = importer.ImportData(openFileDialog1.FileName, userId);
                if (!result.WasSuccess)
                    MessageBox.Show(result.ErrorMessage);
                else
                    MessageBox.Show(string.Format("Successfully added {0} new records!", result.Count));
            }
        }
    }
}
