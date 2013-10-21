using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;

namespace Nada.UI.View
{
    public partial class ViewForm : Form
    {

        public ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(IView view)
        {
            InitializeComponent();
            view.OnClose = () => { this.Close(); };
            pnlView.Controls.Add((UserControl)view);
            this.Text = view.Title;
        }
    }
}
