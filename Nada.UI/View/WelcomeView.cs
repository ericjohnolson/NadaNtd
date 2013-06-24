using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;

namespace Nada.UI.View
{
    public partial class WelcomeView : UserControl
    {
        public WelcomeView()
        {
            InitializeComponent();
            DoTranslate();
        }

        private void DoTranslate()
        {
            lblHeader.Text = Localizer.GetValue("WelcomeTitle") + " " + "User First Name!";
        }

    }
}
