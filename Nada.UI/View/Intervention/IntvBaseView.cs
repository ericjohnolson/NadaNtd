using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.Model;
using Nada.Model.Base;
using Nada.Model.Intervention;
using Nada.UI.View.Help;
using Nada.Globalization;

namespace Nada.UI.View.Intervention
{
    public partial class IntvBaseView : UserControl, IView
    {
        public event Action<bool> OnSave = (b) => { };
        private IntvBase model = null;
        private AdminLevel adminLevel = null;
        private IntvRepository r = null;
        private int creationType;
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }
        public string Title { get { return lblTitle.Text; } }

        public IntvBaseView()
        {
            InitializeComponent();
        }

        public IntvBaseView(int type, AdminLevel a)
        {
            creationType = type;
            adminLevel = a;
            InitializeComponent();
        }

        public IntvBaseView(IntvBase Intv)
        {
            this.model = Intv;
            InitializeComponent();
        }

        private void base_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                adminLevelPickerControl1.OnSelect += adminLevelPickerControl1_OnSelect;
                r = new IntvRepository();
                if (model == null)
                {
                    model = r.CreateIntv(creationType);
                    adminLevelPickerControl1.Select(adminLevel);
                    model.AdminLevelId = adminLevel.Id;
                }
                else
                    adminLevelPickerControl1.Select(model.AdminLevelId.Value);

                bsIntv.DataSource = model;
                lblTitle.Text = model.IntvType.IntvTypeName;
                customIndicatorControl1.OnAddRemove += customIndicatorControl1_OnAddRemove;
                customIndicatorControl1.LoadIndicators(model.IntvType.Indicators, model.IndicatorValues);
            }
        }


        void adminLevelPickerControl1_OnSelect(Model.AdminLevel obj)
        {
            model.AdminLevelId = obj.Id;
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

        /// <summary>
        /// SAVE Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kryptonButton1_Click(object sender, EventArgs e)
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

            bsIntv.EndEdit();
            model.IndicatorValues = customIndicatorControl1.GetValues();
            int userId = ApplicationData.Instance.GetUserId();
            r.SaveBase(model, userId);

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
