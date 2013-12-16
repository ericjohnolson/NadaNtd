using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Repositories;
using Nada.Model;
using Nada.UI.Base;
using Nada.Globalization;
using Nada.UI.AppLogic;
using Nada.UI.ViewModel;

namespace Nada.UI.View
{
    public partial class AdminLevelSelectForm : BaseForm
    {
        private SurveyBaseVm viewModel = null;
        private List<AdminLevel> preselected = new List<AdminLevel>();

        public AdminLevelSelectForm(List<AdminLevel> adminLevels, SurveyBaseVm vm)
            : base()
        {
            viewModel = vm;
            preselected = adminLevels;
            InitializeComponent();
        }

        public List<AdminLevel> GetSelected()
        {
            return adminLevelMultiselectAny1.GetSelected();
        }

        private void AdminLevelMultiselect_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                lblTitle.Text = Translations.ChooseLocations + " " + viewModel.Title;

                if (preselected.Count > 0)
                    adminLevelMultiselectAny1.SetSelected(preselected);
            }
        }
          
        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (adminLevelMultiselectAny1.GetSelected().Count == 0)
            {
                MessageBox.Show(Translations.LocationRequired, Translations.ValidationErrorTitle);
                return;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
