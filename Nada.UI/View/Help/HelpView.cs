using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.Base;

namespace Nada.UI.View
{
    public partial class HelpView : BaseForm
    {
        public HelpView()
            : base()
        {
            InitializeComponent();
        }

        private void HelpView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                string curDir = Directory.GetCurrentDirectory();
                string url = String.Format("file:///{0}/View/Help/NaDa_help_screen_text.htm", curDir);
                this.webBrowser1.Url = new Uri(url);
            }
        }
    }
}
