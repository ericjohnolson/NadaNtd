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
using Nada.Globalization;
using Nada.UI.Base;

namespace Nada.UI.View.Intervention
{
    public partial class IntvTypeEdit : BaseControl, IView
    {
        public event Action OnSave = () => { };
        private IntvRepository repo = null;
        private IntvType model = null;
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }
        public string Title { get { return lblTitle.Text; } }

        public void SetFocus()
        {
        }

        public IntvTypeEdit()
            : base()
        {
            InitializeComponent();
        }

        public IntvTypeEdit(IntvType t)
            : base()
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
                if (model.Id == 0)
                    pnlName.Visible = true;
            }
        }

        private void lvIndicators_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            IndicatorAdd modal = new IndicatorAdd(model.Indicators.Values, (Indicator)e.Model);
            modal.OnSave += edit_OnSave;
            modal.ShowDialog();
        }

        private void edit_OnSave(Indicator obj)
        {
            Indicator old = model.Indicators.Values.FirstOrDefault(i => i.Id == obj.Id);
            model.Indicators.Remove(old.DisplayName);
            model.Indicators.Add(obj.DisplayName, obj);
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

            bsIntvType.EndEdit();
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
            IndicatorAdd modal = new IndicatorAdd(model.Indicators.Values);
            modal.OnSave += add_OnSave;
            modal.ShowDialog();
        }
    }
}
