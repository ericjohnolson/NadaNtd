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
    public partial class DeleteConfirm : Form
    {
        public DeleteConfirm()
        {
            InitializeComponent();
        }

        private void DeleteConfirm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
                Localizer.TranslateControl(this);

            h3Label1.SetMaxWidth(210);
        }
    }
}
