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
using Nada.UI.ViewModel;

namespace Nada.UI.View.DiseaseDistribution
{
    public partial class DataEntryEdit : UserControl, IView
    {
        private IDataEntryVm viewModel = null;
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }
        public string Title { get { return viewModel.Title; } }
        
        public DataEntryEdit()
        {
            InitializeComponent();
        }

        public DataEntryEdit(IDataEntryVm vm)
        {
            viewModel = vm;
            InitializeComponent();
        }

        private void DiseaseDistro_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                adminLevelPickerControl1.Focus();
                Localizer.TranslateControl(this);
                adminLevelPickerControl1.Select(viewModel.Location);

                if (viewModel.Indicators != null && viewModel.Indicators.Count() > 0)
                    indicatorControl1.LoadIndicators(viewModel.Indicators, viewModel.IndicatorValues, viewModel.IndicatorDropdownValues);
                indicatorControl1.OnAddRemove += customIndicatorControl1_OnAddRemove;
                StatusChanged(viewModel.StatusMessage);
                // design
                lblTitle.Text = TranslationLookup.GetValue(viewModel.Title, viewModel.Title);
                lblDiseaseType.Text = TranslationLookup.GetValue(viewModel.TypeTitle, viewModel.TypeTitle);
                lblTitle.ForeColor = viewModel.FormColor;
                lblDiseaseType.ForeColor = viewModel.FormColor;
                adminLevelPickerControl1.TextColor = viewModel.FormColor;
                statCalculator1.TextColor = viewModel.FormColor;
                indicatorControl1.TextColor = viewModel.FormColor;
                hrTop.RuleColor = viewModel.FormColor;
                // calclulator
                if (viewModel.Calculator != null)
                {
                    statCalculator1.Calc = viewModel.Calculator;
                    statCalculator1.OnCalc += statCalculator1_OnCalc;
                    statCalculator1_OnCalc();
                }
                else
                    statCalculator1.Visible = false;
            }
        }

        private void statCalculator1_OnCalc()
        {
            statCalculator1.DoCalc(indicatorControl1.GetValues(), viewModel.Location.Id);
        }
                
        /// <summary>
        /// SAVE Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_Click(object sender, EventArgs e)
        {
            if (!viewModel.IsValid() || !indicatorControl1.IsValid())
            {
                MessageBox.Show(Translations.ValidationError);
                return;
            }
            viewModel.DoSave(indicatorControl1.GetValues());
            OnClose();
        }

        void customIndicatorControl1_OnAddRemove()
        {
            IndicatorTypeEdit editor = new IndicatorTypeEdit(viewModel);
            editor.OnSave += editType_OnSave;
            ViewForm form = new ViewForm(editor);
            form.ShowDialog();
        }

        void editType_OnSave()
        {
            indicatorControl1.LoadIndicators(viewModel.Indicators, viewModel.IndicatorDropdownValues);
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
