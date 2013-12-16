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
using Nada.Globalization;
using Nada.UI.View.Survey;
using Nada.Model.Intervention;
using Nada.UI.View.Help;
using Nada.Model.Diseases;
using Nada.UI.Base;

namespace Nada.UI.View.DiseaseDistribution
{
    public partial class DiseaseDistroEdit : BaseControl, IView
    {
        private AdminLevel adminLevel = null;
        private DiseaseDistroPc model = null;
        private DiseaseRepository r = null;
        private DemoRepository demo = null;
        private int diseaseId = 0;
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }
        public string Title { get { return lblTitle.Text; } }

        public DiseaseDistroEdit()
            : base()
        {
            InitializeComponent();
        }

        public DiseaseDistroEdit(AdminLevel a, int id)
            : base()
        {
            adminLevel = a;
            diseaseId = id;
            InitializeComponent();
        }

        public DiseaseDistroEdit(DiseaseDistroPc s)
            : base()
        {
            this.model = s;
            InitializeComponent();
        }

        private void DiseaseDistro_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                adminLevelPickerControl1.Focus();
                Localizer.TranslateControl(this);
                adminLevelPickerControl1.OnSelect += adminLevelPickerControl1_OnSelect;
                r = new DiseaseRepository();
                demo = new DemoRepository();
                if (model == null) 
                {
                    model = r.Create((DiseaseType)diseaseId);
                    adminLevelPickerControl1.Select(adminLevel);
                    model.AdminLevelId = adminLevel.Id;
                }
                else
                    adminLevelPickerControl1.Select(model.AdminLevelId.Value);

                ShowType(model);
                bsDiseaseDistro.DataSource = model;

                if (model.Indicators != null && model.Indicators.Count() > 0)
                    customIndicatorControl1.LoadIndicators(model.Indicators, model.IndicatorValues);

                customIndicatorControl1.OnAddRemove += customIndicatorControl1_OnAddRemove;
                StatusChanged(model.UpdatedBy);

            }
        }

        private void ShowType(DiseaseDistroPc model)
        {
            if (model.Disease.Id == (int)DiseaseType.Lf)
            {
                lblDiseaseTrichiasis.Visible = false;
                cbDiseaseTrichiasis.Visible = false;
                lblTrachomaImpactYearMonth.Visible = false;
                dtTrachomaImpactYearMonth.Visible = false;
                lblYearStoppingPcLower.Visible = false;
                tbYearStoppingPcLower.Visible = false;
                lblTrichiasisSurgeryBacklog.Visible = false;
                tbTrichiasisSurgeryBacklog.Visible = false;
                lblTrichiasisSurgeryGoal.Visible = false;
                tbTrichiasisSurgeryGoal.Visible = false;
                lblTrachomaGoal.Visible = false;
                tbTrachomaGoal.Visible = false;
            }
        }

        void adminLevelPickerControl1_OnSelect(Model.AdminLevel obj)
        {
            model.AdminLevelId = obj.Id;
        }

        /// <summary>
        /// SAVE Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_Click(object sender, EventArgs e)
        {
            if (!model.IsValid() || !customIndicatorControl1.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }
            if (!model.AdminLevelId.HasValue || model.AdminLevelId.Value < 1)
            {
                MessageBox.Show(Translations.LocationRequired);
                return;
            }

            bsDiseaseDistro.EndEdit();
            
            model.IndicatorValues = customIndicatorControl1.GetValues();
            int userId = ApplicationData.Instance.GetUserId();
            r.Save(model, userId);
            OnClose();
        }

        void customIndicatorControl1_OnAddRemove()
        {
        }

        void editType_OnSave()
        {
            customIndicatorControl1.LoadIndicators(model.Indicators);
        }
        
        private void cancel_Click(object sender, EventArgs e)
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
