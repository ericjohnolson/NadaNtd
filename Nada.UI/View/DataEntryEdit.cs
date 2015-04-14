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

using Nada.Model.Diseases;
using Nada.UI.ViewModel;
using Nada.UI.Base;
using Nada.UI.Controls;
using System.Web.Security;
using System.IO;
using System.Configuration;

namespace Nada.UI.View
{
    public partial class DataEntryEdit : BaseControl, IView
    {
        private IDataEntryVm viewModel = null;
        private bool setFocusBottomOnCalc = false;
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }
        public string Title { get { return viewModel.Title; } }

        public void SetFocus()
        {
            btnHelp.Focus();
        }

        public DataEntryEdit()
            : base()
        {
            InitializeComponent();
        }

        public DataEntryEdit(IDataEntryVm vm)
            : base()
        {
            viewModel = vm;
            InitializeComponent();
        }

        private void DataEntryEdit_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                tblTitle.Focus();
                lblAdminLevel.Text = viewModel.LocationName;
                tbNotes.Text = viewModel.Notes;
                LoadMetaData();
                if (viewModel.Indicators != null && viewModel.Indicators.Count() > 0)
                    indicatorControl1.LoadIndicators(viewModel.Indicators, viewModel.IndicatorValues, viewModel.IndicatorDropdownValues, viewModel.EntityType);
                indicatorControl1.OnAddRemove += customIndicatorControl1_OnAddRemove;
                indicatorControl1.OnRangeChange += indicatorControl1_OnRangeChange;
                StatusChanged(viewModel.StatusMessage);
                // design
                lblTitle.Text = TranslationLookup.GetValue(viewModel.Title, viewModel.Title);
                lblDiseaseType.Text = TranslationLookup.GetValue(viewModel.TypeTitle, viewModel.TypeTitle);
                lblLocation.ForeColor = Color.FromArgb(52, 100, 160);
                lblTitle.ForeColor = Color.FromArgb(52, 100, 160);
                lblDiseaseType.ForeColor = Color.FromArgb(52, 100, 160);
                statCalculator1.TextColor = Color.FromArgb(52, 100, 160);
                indicatorControl1.TextColor = Color.FromArgb(52, 100, 160);
                hrTop.RuleColor = Color.FromArgb(52, 100, 160);
                hr4.RuleColor = Color.FromArgb(52, 100, 160);
                hr5.RuleColor = Color.FromArgb(52, 100, 160);
                // calclulator
                if (viewModel.Calculator != null)
                {
                    statCalculator1.Calc = viewModel.Calculator;
                    statCalculator1.OnCalc += statCalculator1_OnCalc;
                    DoCalc(false);
                }
                else
                    statCalculator1.Visible = false;
                // special controls
                viewModel.AddSpecialControls(indicatorControl1);
                if (!Roles.IsUserInRole(ApplicationData.Instance.CurrentUser.UserName, "RoleDataEnterer") &&
                !Roles.IsUserInRole(ApplicationData.Instance.CurrentUser.UserName, "RoleAdmin"))
                {
                    btnTopSave.Visible = false;
                    btnBottomSave.Visible = false;
                }
            }
        }

        // NEED to add spinner while this is happening so not so much blinking like crazy... only once? (yeah right right?)
        private void LoadMetaData()
        {
            indicatorControl1.SetMetaDataLoading(true);
            BackgroundWorker metaDataFetcher = new BackgroundWorker();
            metaDataFetcher.DoWork += metaDataFetcher_DoWork;
            metaDataFetcher.RunWorkerCompleted += metaDataFetcher_RunWorkerCompleted;
            metaDataFetcher.RunWorkerAsync();
        }

        void metaDataFetcher_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<KeyValuePair<string, string>> metaData = new List<KeyValuePair<string, string>>();
                if (viewModel.Calculator != null)
                    metaData = viewModel.Calculator.GetMetaData(
                    viewModel.Indicators.Where(i => !i.Value.IsCalculated && i.Value.DataTypeId == (int)IndicatorDataType.Calculated).Select(i => viewModel.CalculatorTypeId + i.Value.DisplayName),
                    viewModel.Location.Id, indicatorControl1.start, indicatorControl1.end);
                e.Result = metaData;
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error metaDataFetcher_DoWork. ", ex);
                throw;
            }
        }

        void metaDataFetcher_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.IsDisposed)
                return;
            indicatorControl1.SetMetaDataLoading(false);
            List<KeyValuePair<string, string>> metaData = (List<KeyValuePair<string, string>>)e.Result;
            if (metaData != null && metaData.Count > 0)
            {
                //Point scrollPosition = ((Panel)this.Parent).AutoScrollPosition;
                indicatorControl1.LoadMetaData(metaData);
                //((Panel)this.Parent).AutoScrollPosition = scrollPosition;
            }
        }

        private void statCalculator1_OnCalc()
        {
            DoCalc(true);
        }

        private void DoCalc(bool setFocusBottom)
        {
            setFocusBottomOnCalc = setFocusBottom;
            statCalculator1.DoCalc(viewModel.Indicators, indicatorControl1.GetValues(), viewModel.Location.Id,
                viewModel.CalculatorTypeId, indicatorControl1.start, indicatorControl1.end, DoFocusAfterCalc);
        }

        /// <summary>
        /// SAVE Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_Click(object sender, EventArgs e)
        {
            if (!viewModel.IsValid() || !indicatorControl1.IsValid() || !viewModel.IsValid(indicatorControl1.GetValues()))
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }

            viewModel.DoSave(indicatorControl1.GetValues(), tbNotes.Text);
            OnClose();
        }

        void customIndicatorControl1_OnAddRemove()
        {
            viewModel.DoSave(indicatorControl1.GetValues(), tbNotes.Text, false);
            IndicatorTypeEdit editor = new IndicatorTypeEdit(viewModel);
            editor.OnSave += editType_OnSave;
            ViewForm form = new ViewForm(editor);
            form.ShowDialog();
        }

        void editType_OnSave()
        {
            indicatorControl1.LoadIndicators(viewModel.Indicators, viewModel.IndicatorValues, viewModel.IndicatorDropdownValues, viewModel.EntityType);
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
            Help.ShowHelp(this, "file:///" + Directory.GetCurrentDirectory() + Translations.HelpFile);
            //HelpView help = new HelpView();
            //help.Show();
        }

        void indicatorControl1_OnRangeChange()
        {
            LoadMetaData();
            statCalculator1.DoCalc(viewModel.Indicators, indicatorControl1.GetValues(), viewModel.Location.Id, viewModel.CalculatorTypeId,
                indicatorControl1.start, indicatorControl1.end, DoFocusAfterCalc);
        }

        public void DoFocusAfterCalc()
        {
            if (setFocusBottomOnCalc)
                btnBottomSave.Focus();
        }

        public static Control FindFocusedControl(Control control)
        {
            var container = control as ContainerControl;
            while (container != null)
            {
                control = container.ActiveControl;
                container = control as ContainerControl;
            }
            return control;
        }
    }
}
