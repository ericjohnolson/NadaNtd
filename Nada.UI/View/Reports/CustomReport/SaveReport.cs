using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;
using Nada.Model.Reports;
using System.Drawing.Drawing2D;

namespace Nada.UI.View.Reports.CustomReport
{
    public partial class SaveReport : BaseForm
    {
        public Action OnSave { get; set; }
        private Nada.Model.Reports.SavedReport model;

        public SaveReport(Nada.Model.Reports.SavedReport m)
            : base()
        {
            model = m;
            InitializeComponent();
        }

        private void UserAdd_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                bindingSource1.DataSource = model;
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(model.DisplayName))
            {
                MessageBox.Show(Translations.PleaseEnterRequiredFields, Translations.ValidationErrorTitle);
                return;
            }
            bindingSource1.EndEdit();
            int userid = ApplicationData.Instance.GetUserId();
            ReportRepository repo = new ReportRepository();
            repo.Save(model, userid);
            OnSave();
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
