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

namespace Nada.UI.View.Intervention
{
    public partial class IntvTypeEdit : Form
    {
        public event Action OnSave = () => { };
        private IntvRepository repo = null;
        private IntvType model = null;

        public IntvTypeEdit()
        {
            InitializeComponent();
        }

        public IntvTypeEdit(IntvType t)
        {
            model = t;
            InitializeComponent();
        }

        private void IntvTypeView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                repo = new IntvRepository();
                bsIntvType.DataSource = model;
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
            bsIntvType.EndEdit();
            int currentUser = ApplicationData.Instance.GetUserId();
            repo.Save(model, currentUser);
            OnSave();
            this.Close();
        }

        void add_OnSave(Indicator obj)
        {
            model.Indicators.Add(obj.DisplayName, obj);
            lvIndicators.SetObjects(model.Indicators.Values.Where(i => i.IsEditable));
        }

        private void fieldLink1_Click(object sender, EventArgs e)
        {
            IndicatorAdd modal = new IndicatorAdd();
            modal.OnSave += add_OnSave;
            modal.ShowDialog();

        }
    }
}
