﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.UI.Base;

namespace Nada.UI.View.Modals
{
    public partial class ErrorModal : BaseForm
    {
        public ErrorModal()
            : base()
        {
            InitializeComponent();
        }

        public ErrorModal(string message)
            : base()
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
