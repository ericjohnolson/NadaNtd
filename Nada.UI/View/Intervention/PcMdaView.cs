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

namespace Nada.UI.View.Intervention
{
    public partial class PcMdaView : UserControl, IView
    {
        private AdminLevel adminLevel = null;
        private PcMda model = null;
        private IntvRepository r = null;
        private DemoRepository demo = null;
        private IntvType intvType = null;
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }
        public string Title { get { return lblTitle.Text; } }
        
        public PcMdaView()
        {
            InitializeComponent();
        }

        public PcMdaView(AdminLevel a, IntvType t)
        {
            adminLevel = a;
            intvType = t;
            InitializeComponent();
        }

        public PcMdaView(PcMda s)
        {
            this.model = s;
            InitializeComponent();
        }

        private void PcMda_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                adminLevelPickerControl1.Focus();
                Localizer.TranslateControl(this);
                adminLevelPickerControl1.OnSelect += adminLevelPickerControl1_OnSelect;
                r = new IntvRepository();
                demo = new DemoRepository();
                if (model == null) 
                {
                    model = r.CreateIntv<PcMda>(intvType.Id);
                    adminLevelPickerControl1.Select(adminLevel);
                    model.AdminLevelId = adminLevel.Id;
                }
                else
                    adminLevelPickerControl1.Select(model.AdminLevelId.Value);
                LoadListValues(model);
                ShowType(model);
                bsIntv.DataSource = model;

                if (model.IntvType.Indicators != null && model.IntvType.Indicators.Count() > 0)
                    customIndicatorControl1.LoadIndicators(model.IntvType.Indicators, model.IndicatorValues);

                customIndicatorControl1.OnAddRemove += customIndicatorControl1_OnAddRemove;
                fundersControl1.LoadItems(model.Partners);
                diseasesControl1.LoadItems(model.DiseasesTargeted);
                StatusChanged(model.UpdatedBy);
            }
        }

        private void LoadListValues(PcMda model)
        {
            cbStockOut.Items.Clear();
            foreach (string key in model.StockOutValues)
                cbStockOut.Items.Add(TranslationLookup.GetValue(key, key));

            cbStockOutDrug.Items.Clear();
            foreach (string key in model.StockOutDrugValues)
                cbStockOutDrug.Items.Add(TranslationLookup.GetValue(key, key));

            cbStockOutLength.Items.Clear();
            foreach (string key in model.StockOutLengthValues)
                cbStockOutLength.Items.Add(TranslationLookup.GetValue(key, key));

        }

        private void ShowType(PcMda model)
        {
            if (model.IntvType.Id == (int)StaticIntvType.IvmAlbMda)
            {
                lblNumPsacTargeted.Enabled = false;
                tbNumPsacTargeted.Enabled = false;
                lblNumTreatedZx.Enabled = false;
                tbNumTreatedZx.Enabled = false;
                lblNumTreatedZxPos.Enabled = false;
                tbNumTreatedZxPos.Enabled = false;
                lblNumTreatedTeo.Enabled = false;
                tbNumTreatedTeo.Enabled = false;
                lblPsacCoverage.Enabled = false;
                tbPsacCoverage.Enabled = false;
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
                MessageBox.Show(Translations.ValidationError);
                return;
            }
            if (!model.AdminLevelId.HasValue || model.AdminLevelId.Value < 1)
            {
                MessageBox.Show(Translations.LocationRequired);
                return;
            }

            model.Partners = fundersControl1.GetSelected();
            model.DiseasesTargeted = diseasesControl1.GetSelected();
            if (model.DiseasesTargeted == null || model.DiseasesTargeted.Count == 0)
            {
                MessageBox.Show(Translations.DiseasesRequired);
                return;
            }

            bsIntv.EndEdit();
            
            model.IndicatorValues = customIndicatorControl1.GetValues();
            model.MapPropertiesToIndicators();
            int userId = ApplicationData.Instance.GetUserId();
            r.Save(model, userId);
            OnClose();
        }

        void customIndicatorControl1_OnAddRemove()
        {
            IntvTypeEdit editor = new IntvTypeEdit(model.IntvType);
            editor.OnSave += editType_OnSave;
            ViewForm form = new ViewForm(editor);
            form.ShowDialog();
        }

        void editType_OnSave()
        {
            customIndicatorControl1.LoadIndicators(model.IntvType.Indicators);
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

        #region calculated fields

        private void tbNumEligibleTargeted_TextChanged(object sender, EventArgs e)
        {
            CalcProgramCoverage();
        }

        private void tbNumEligibleTreated_TextChanged(object sender, EventArgs e)
        {
            CalcProgramCoverage();
            CalcEpiCoverage();
            CalcFemales();
            CalcMales();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            CalcEpiCoverage();
        }


        private void tbNumEligibleFemalesTreated_TextChanged(object sender, EventArgs e)
        {
            CalcFemales();
        }

        private void tbNumEligibleMalesTreated_TextChanged(object sender, EventArgs e)
        {
            CalcMales();
        }

        private void tbNumSacTargeted_TextChanged(object sender, EventArgs e)
        {
            CalcSac();
        }

        private void tbNumPsacTargeted_TextChanged(object sender, EventArgs e)
        {
            CalcPsac();
        }

        private void tbNumSacTreated_TextChanged(object sender, EventArgs e)
        {
            CalcSac();
        }

        private void tbNumPsacTreated_TextChanged(object sender, EventArgs e)
        {
            CalcPsac();
        }

        private void CalcEpiCoverage()
        {
            int x, y;
            if (int.TryParse(tbNumEligibleTreated.Text, out x) && int.TryParse(textBox4.Text, out y))
                model.EpiCoverage = Math.Round(x / Convert.ToDouble(y) * 100.0, 2);
        }

        private void CalcProgramCoverage()
        {
            int treat, target;
            if (int.TryParse(tbNumEligibleTargeted.Text, out target) && int.TryParse(tbNumEligibleTreated.Text, out treat))
                model.ProgramCoverage = Math.Round(treat / Convert.ToDouble(target) * 100.0, 2);
        }

        private void CalcFemales()
        {
            int x, y;
            if (int.TryParse(tbNumEligibleFemalesTreated.Text, out x) && int.TryParse(tbNumEligibleTreated.Text, out y))
                model.FemalesCoverage = Math.Round(x / Convert.ToDouble(y) * 100.0, 2);
        }

        private void CalcMales()
        {
            int x, y;
            if (int.TryParse(tbNumEligibleMalesTreated.Text, out x) && int.TryParse(tbNumEligibleTreated.Text, out y))
                model.MalesCoverage = Math.Round(x / Convert.ToDouble(y) * 100.0, 2);
        }

        private void CalcPsac()
        {
            int x, y;
            if (int.TryParse(tbNumPsacTreated.Text, out x) && int.TryParse(tbNumPsacTargeted.Text, out y))
                model.PsacCoverage = Math.Round(x / Convert.ToDouble(y) * 100.0, 2);
        }

        private void CalcSac()
        {
            int x, y;
            if (int.TryParse(tbNumSacTreated.Text, out x) && int.TryParse(tbNumSacTargeted.Text, out y))
                model.SacCoverage = Math.Round(x / Convert.ToDouble(y) * 100.0, 2);
        }
        #endregion

    }
}
