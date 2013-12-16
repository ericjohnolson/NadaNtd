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
using Nada.Model.Repositories;
using Nada.Model.Survey;
using Nada.UI.AppLogic;
using Nada.UI.Base;

namespace Nada.UI.View.Survey
{
    public partial class SurveyTypeEdit : BaseControl, IView
    {
        public event Action OnSave = () => { };
        private SurveyRepository repo = null;
        private SurveyType model = null;
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }
        public string Title { get { return lblTitle.Text; } }

        public SurveyTypeEdit()
            : base()
        {
            InitializeComponent();
        }

        public SurveyTypeEdit(SurveyType t)
            : base()
        {
            model = t;
            InitializeComponent();
        }

        private void SurveyTypeView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                repo = new SurveyRepository();
                bsSurveyType.DataSource = model;
                lvIndicators.SetObjects(model.Indicators.Values.Where(i => i.IsEditable));
                if (model.Id == 0)
                    pnlName.Visible = true;
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
            OnClose();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!model.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }

            if (model.Indicators.Values.FirstOrDefault() == null)
            {
                MessageBox.Show(Translations.ValidationErrorAddIndicator, Translations.ValidationErrorTitle);
                return;
            }

            bsSurveyType.EndEdit();
            int currentUser = ApplicationData.Instance.GetUserId();
            repo.Save(model, currentUser);
            OnSave();
            OnClose();
        }

        void add_OnSave(Indicator obj)
        {
            model.Indicators.Add(obj.DisplayName, obj);
            lvIndicators.SetObjects(model.Indicators.Values.Where(i => i.IsEditable));
        }

        private void fieldLink1_OnClick()
        {
            IndicatorAdd modal = new IndicatorAdd();
            modal.OnSave += add_OnSave;
            modal.ShowDialog();
        }
    }
}
