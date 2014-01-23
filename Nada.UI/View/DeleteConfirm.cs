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
using Nada.UI.Base;

namespace Nada.UI.View
{
    public partial class DeleteConfirm : BaseForm
    {
        private string title = "";
        private string message = "";
        public DeleteConfirm()
            : base()
        {
            InitializeComponent();
        }

        public DeleteConfirm(string t, string m)
            : base()
        {
            title = t;
            message = m;
            InitializeComponent();
        }

        private void DeleteConfirm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
                Localizer.TranslateControl(this);

            h3Label1.SetMaxWidth(210);

            if (title.Length > 0)
            {
                h3Label1.Text = message;
                label3.Text = title;
            }
        }
    }
}
