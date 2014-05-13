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
using Nada.UI.ViewModel;

namespace Nada.UI.View
{
    public partial class IndicatorTypeEdit : BaseControl, IView
    {
        public event Action OnSave = () => { };
        private IDataEntryVm viewModel = null;
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }
        public string Title
        {
            get
            {
                if (viewModel != null)
                    return viewModel.Title;
                return lblTitle.Text;
            }
        }
        public void SetFocus()
        {
            tbName.Focus();
        }

        public IndicatorTypeEdit()
            : base()
        {
            InitializeComponent();
        }

        public IndicatorTypeEdit(IDataEntryVm t)
            : base()
        {
            viewModel = t;
            InitializeComponent();
        }

        private void IndicatorTypeEdit_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                lvIndicators.SetObjects(viewModel.Indicators.Values.Where(i => i.IsEditable));
                tbName.Text = viewModel.Title;
                if (viewModel.CanEditTypeName)
                    pnlName.Visible = true;
                lblTitle.ForeColor = Color.FromArgb(52, 100, 160);
                lblDiseaseType.ForeColor = Color.FromArgb(52, 100, 160);
                lblCustomIndicators.ForeColor = Color.FromArgb(52, 100, 160);
                hrTop.RuleColor = Color.FromArgb(52, 100, 160);
                lblDiseaseType.Text = viewModel.TypeTitle;
                lblTitle.Text = viewModel.Title;

            }
        }

        private void lvIndicators_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            IndicatorAdd modal = new IndicatorAdd(viewModel.Indicators.Values, (Indicator)e.Model);
            modal.OnSave += edit_OnSave;
            modal.ShowDialog();
        }

        private void edit_OnSave(Indicator obj)
        {
            Indicator old = viewModel.Indicators.Values.FirstOrDefault(i => i.Id == obj.Id);
            viewModel.Indicators.Remove(old.DisplayName);
            viewModel.Indicators.Add(obj.DisplayName, obj);
            lvIndicators.SetObjects(viewModel.Indicators.Values.Where(i => i.IsEditable));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnClose();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show(Translations.Name + " " +  Translations.IsRequired, Translations.ValidationErrorTitle);
                return;
            }

            viewModel.DoSaveType(tbName.Text);
            OnSave();
            OnClose();
        }

        void add_OnSave(Indicator obj)
        {
            viewModel.Indicators.Add(obj.DisplayName, obj);
            lvIndicators.SetObjects(viewModel.Indicators.Values.Where(i => i.IsEditable));
        }

        private void fieldLink1_OnClick()
        {
            IndicatorAdd modal = new IndicatorAdd(viewModel.Indicators.Values);
            modal.OnSave += add_OnSave;
            modal.ShowDialog();
        }
    }
}
