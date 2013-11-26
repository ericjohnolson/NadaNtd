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
using Nada.UI.Base;

namespace Nada.UI.View
{
    public partial class DiseaseDistributionIndicators : BaseForm
    {
        public event Action OnSave = () => { };
        private DiseaseRepository repo = null;
        private IHaveDynamicIndicators model = null;

        public DiseaseDistributionIndicators()
            : base()
        {
            InitializeComponent();
        }

        public DiseaseDistributionIndicators(IHaveDynamicIndicators m)
            : base()
        {
            model = m;
            InitializeComponent();
        }

        private void IntvTypeView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                repo = new DiseaseRepository();
                lvIndicators.SetObjects(model.Indicators.Values.Where(i => i.IsEditable));
            }
        }

        private void lvIndicators_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            IndicatorAdd modal = new IndicatorAdd((Indicator)e.Model);
            modal.OnSave += edit_OnSave;
            modal.ShowDialog();
        }

        private void edit_OnSave(Indicator obj)
        {
            lvIndicators.SetObjects(model.Indicators.Values.Where(i => i.IsEditable));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            int currentUser = ApplicationData.Instance.GetUserId();
            if(model is DiseaseDistroPc)
                repo.SaveIndicators((DiseaseDistroPc)model, ((DiseaseDistroPc)model).Disease.Id, currentUser);
            OnSave();
            this.Close();
        }

        private void lnkNewIndicator_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IndicatorAdd modal = new IndicatorAdd();
            modal.OnSave += add_OnSave;
            modal.ShowDialog();
        }

        void add_OnSave(Indicator obj)
        {
            model.Indicators.Add(obj.DisplayName, obj);
            lvIndicators.SetObjects(model.Indicators.Values.Where(i => i.IsEditable));
        }
    }
}
