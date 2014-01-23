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
        private Indicator indicator = null;

        public IndicatorValueItemAdd(IndicatorDropdownValue m, Indicator i)
            : base()
        {
            indicator = i;
            model = m;
            InitializeComponent();
        }

        private void DistributionMethodAdd_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                lblTitle.Text = TranslationLookup.GetValue(indicator.DisplayName, indicator.DisplayName) + " " + Translations.Option;
                this.Text = TranslationLookup.GetValue(indicator.DisplayName, indicator.DisplayName) + " " + Translations.Option;
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

            SettingsRepository s = new SettingsRepository();
            RepositoryBase r = new RepositoryBase();
            int userid = ApplicationData.Instance.GetUserId();

            if (model.EntityType == IndicatorEntityType.EcologicalZone)
                s.SaveEz(model, userid);
            if (model.EntityType == IndicatorEntityType.EvalSubDistrict)
                s.SaveEvalSubDistrict(model, userid);
            else if (model.EntityType == IndicatorEntityType.EvaluationUnit)
                s.SaveEu(model, userid);
            else
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
