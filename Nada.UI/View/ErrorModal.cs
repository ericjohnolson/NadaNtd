using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;

namespace Nada.UI.View.Modals
{
    public partial class ErrorModal : Form
    {
        public ErrorModal()
        {
            InitializeComponent();
        }

        public ErrorModal(string message)
        {
            InitializeComponent();
            tbErrorMessage.Text = message;
        }

        private void ErrorModal_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
                Localizer.TranslateControl(this);
        }
    }
}
