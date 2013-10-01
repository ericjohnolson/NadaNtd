using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nada.UI.View.Help
{
    public partial class HelpView : Form
    {
        public HelpView()
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
