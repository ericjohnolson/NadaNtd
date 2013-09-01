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
    public partial class IntvBaseView : UserControl
    {
        public event Action<bool> OnSave = (b) => { };
        private IntvBase model = null;
        private IntvRepository r = null;
        private StaticIntvType creationType;

        public IntvBaseView()
        {
            InitializeComponent();
        }

        public IntvBaseView(StaticIntvType type)
        {
            creationType = type;
            InitializeComponent();
        }

        public IntvBaseView(IntvBase Intv)
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
                if (model == null) model = r.CreateIntv(creationType);
                bsIntv.DataSource = model;
                bsType.DataSource = model.IntvType;
                customIndicatorControl1.LoadIndicators(model.IntvType.Indicators.Cast<IDynamicIndicator>());
            }
        }

        void adminLevelPickerControl1_OnSelect(Model.AdminLevel obj)
        {
            model.AdminLevelId = obj.Id;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IntvTypeEdit editor = new IntvTypeEdit(model.IntvType);
            editor.OnSave += editor_OnSave;
            editor.ShowDialog();
        }

        void editor_OnSave()
        {
            customIndicatorControl1.LoadIndicators(model.IntvType.Indicators.Cast<IDynamicIndicator>());
            bsType.ResetBindings(false);
        }

        /// <summary>
        /// SAVE Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            bsIntv.EndEdit();
            model.CustomIndicatorValues = customIndicatorControl1.GetValues<IndicatorValue>();
            int userId = ApplicationData.Instance.GetUserId();
            r.SaveBase(model, userId);
            MessageBox.Show("Intervention was saved!");
            OnSave(false);
        }

        private void lnkCreateImport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImportDownload form = new ImportDownload(new IntvImporter(model.IntvType, creationType, model.IntvType.IntvTypeName + " Import"));
            form.ShowDialog();
        }

        private void lnkDoImport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                int userId = ApplicationData.Instance.GetUserId();
                var importer = new IntvImporter(model.IntvType, creationType, model.IntvType.IntvTypeName + " Import");
                var result = importer.ImportData(openFileDialog1.FileName, userId);
                if (!result.WasSuccess)
                    MessageBox.Show(result.ErrorMessage);
                else
                    MessageBox.Show(string.Format("Successfully added {0} new records!", result.Count));
            }
        }
    }
}
