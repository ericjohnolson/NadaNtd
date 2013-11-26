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

namespace Nada.UI.View
{
    public partial class IndicatorValueItemAdd : BaseForm
    {
        public event Action<IndicatorDropdownValue> OnSave = (e) => { };
        private IndicatorDropdownValue model = new IndicatorDropdownValue();

        public IndicatorValueItemAdd()
            : base()
        {
            InitializeComponent();
        }

        public IndicatorValueItemAdd(IndicatorDropdownValue m)
            : base()
        {
            model = m;
            InitializeComponent();
        }

        private void DistributionMethodAdd_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                bindingSource1.DataSource = model;
                lblLastUpdated.Text =  model.UpdatedBy;
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!model.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }
            bindingSource1.EndEdit();
            RepositoryBase r = new RepositoryBase();
            int userid = ApplicationData.Instance.GetUserId();
            r.Save(model, userid);
            OnSave(model);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
