using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Repositories;
using Nada.Model.Survey;
using Nada.UI.AppLogic;

namespace Nada.UI.View.Survey
{
    public partial class SurveyTypeEdit : Form
    {
        public event Action OnSave = () => { };
        private SurveyRepository repo = null;
        private SurveyType model = null;

        public SurveyTypeEdit()
        {
            InitializeComponent();
        }

        public SurveyTypeEdit(SurveyType t)
        {
            model = t;
            InitializeComponent();
        }

        private void SurveyTypeView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                repo = new SurveyRepository();
                bsSurveyType.DataSource = model;
                lvIndicators.SetObjects(model.Indicators.Where(i => i.IsEditable));
            }
        }

        private void lvIndicators_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            IndicatorAdd modal = new IndicatorAdd((SurveyIndicator)e.Model);
            modal.OnSave += edit_OnSave;
            modal.ShowDialog();
        }

        private void edit_OnSave(SurveyIndicator obj)
        {
            lvIndicators.SetObjects(model.Indicators);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            bsSurveyType.EndEdit();
            int currentUser = ApplicationData.Instance.GetUserId();
            repo.Save(model, currentUser);
            OnSave();
            this.Close();
        }

        private void lnkNewIndicator_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IndicatorAdd modal = new IndicatorAdd();
            modal.OnSave += add_OnSave;
            modal.ShowDialog();
        }

        void add_OnSave(SurveyIndicator obj)
        {
            model.Indicators.Add(obj);
            lvIndicators.SetObjects(model.Indicators);
        }
    }
}
