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
using Nada.UI.View.Help;
using Nada.Globalization;
using Nada.UI.Base;

namespace Nada.UI.View.Survey
{
    public partial class SurveyBaseView : BaseControl, IView
    {
        public event Action<bool> OnSave = (b) => { };
        private SurveyBase model = null;
        private SurveyRepository r = null;
        private int creationType;
        private AdminLevel adminLevel = null;
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }
        public string Title { get { return lblTitle.Text; } }

        public void SetFocus()
        {
            
        }

        public SurveyBaseView()
            : base()
        {
            InitializeComponent();
        }

        public SurveyBaseView(int typeId, AdminLevel a)
            : base()
        {
            adminLevel = a;
            creationType = typeId;
            InitializeComponent();
        }

        public SurveyBaseView(SurveyBase survey)
            : base()
        {
            this.model = survey;
            InitializeComponent();
        }

        private void LfPrevalence_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                r = new SurveyRepository();

                //if (model == null)
                //{
                //    if (model == null) model = r.CreateSurvey(creationType);
                //    adminLevelPickerControl1.Select(adminLevel);
                //    model.AdminLevelId = adminLevel.Id;
                //}
                //else
                //    adminLevelPickerControl1.Select(model.AdminLevelId.Value);

                bsSurvey.DataSource = model;
                lblTitle.Text = model.TypeOfSurvey.SurveyTypeName;
                customIndicatorControl1.LoadIndicators(model.TypeOfSurvey.Indicators, model.IndicatorValues);
                customIndicatorControl1.OnAddRemove += customIndicatorControl1_OnAddRemove;
            }
        }

        void customIndicatorControl1_OnAddRemove()
        {
            SurveyTypeEdit editor = new SurveyTypeEdit(model.TypeOfSurvey);
            editor.OnSave += editType_OnSave;
            ViewForm form = new ViewForm(editor);
            form.ShowDialog();
        }

        void editType_OnSave()
        {
            customIndicatorControl1.LoadIndicators(model.TypeOfSurvey.Indicators);
        }
        
        

        /// <summary>
        /// SAVE Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (!model.IsValid() || !customIndicatorControl1.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }
            //if (!model.AdminLevelId.HasValue || model.AdminLevelId.Value < 1)
            //{
            //    MessageBox.Show(Translations.LocationRequired, Translations.ValidationErrorTitle);
            //    return;
            //}

            bsSurvey.EndEdit();
            model.IndicatorValues = customIndicatorControl1.GetValues();
            int userId = ApplicationData.Instance.GetUserId();
            r.SaveSurvey(model, userId);

            OnClose();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            OnClose();
        }

        private void btnDash_Click(object sender, EventArgs e)
        {
            OnClose();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            HelpView help = new HelpView();
            help.Show();
        }


    }
}
