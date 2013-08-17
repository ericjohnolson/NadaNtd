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
using Nada.Model.Survey;
using Nada.UI.AppLogic;

namespace Nada.UI.View
{
    public partial class IndicatorAdd : Form
    {
        public event Action<SurveyIndicator> OnSave = (e) => { };
        private SurveyIndicator model = new SurveyIndicator();

        public IndicatorAdd()
        {
            InitializeComponent();
        }

        public IndicatorAdd(SurveyIndicator m)
        {
            model = m;
            InitializeComponent();
        }

        private void Modal_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                bsIndicator.DataSource = model;
                lblLastUpdated.Text += model.UpdatedBy;
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            SetDataType(model);
            model.IsEdited = true;
            bsIndicator.EndEdit();
            OnSave(model);
            this.Close();
        }

        private void SetDataType(SurveyIndicator model)
        {
            if (model.DataType == "Yes/No")
                model.DataTypeId = (int)IndicatorDataType.YesNo;
            else
                model.DataTypeId = (int)Enum.Parse(typeof(IndicatorDataType), model.DataType);
        }
  
    }
}
