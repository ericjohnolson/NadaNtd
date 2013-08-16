using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Survey;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.Model;
using Nada.Model.Base;

namespace Nada.UI.View.Survey
{
    public partial class SurveyBaseView : UserControl
    {
        public event Action<bool> OnSave = (b) => { };
        private SurveyBase model = null;
        private SurveyRepository r = null;
        private StaticSurveyType creationType;

        public SurveyBaseView()
        {
            InitializeComponent();
        }

        public SurveyBaseView(StaticSurveyType type)
        {
            creationType = type;
            InitializeComponent();
        }

        public SurveyBaseView(SurveyBase survey)
        {
            this.model = survey;
            InitializeComponent();
        }

        private void LfPrevalence_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                adminLevelPickerControl1.OnSelect += adminLevelPickerControl1_OnSelect;
                r = new SurveyRepository();
                if (model == null) model = r.CreateSurvey(creationType);
                bsSurvey.DataSource = model;
                bsType.DataSource = model.TypeOfSurvey;
                customIndicatorControl1.LoadIndicators(model.TypeOfSurvey.Indicators.Cast<IDynamicIndicator>());
            }
        }

        void adminLevelPickerControl1_OnSelect(Model.AdminLevel obj)
        {
            model.AdminLevelId = obj.Id;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SurveyTypeEdit editor = new SurveyTypeEdit(model.TypeOfSurvey);
            editor.OnSave += editor_OnSave;
            editor.ShowDialog();
        }

        void editor_OnSave()
        {
            customIndicatorControl1.LoadIndicators(model.TypeOfSurvey.Indicators.Cast<IDynamicIndicator>());
            bsType.ResetBindings(false);
        }

        /// <summary>
        /// SAVE Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            bsSurvey.EndEdit();
            model.CustomIndicatorValues = customIndicatorControl1.GetValues<SurveyIndicatorValue>();
            int userId = ApplicationData.Instance.GetUserId();
            r.SaveSurvey(model, userId);
            MessageBox.Show("Survey was saved!");
            OnSave(false);
        }


    }
}
