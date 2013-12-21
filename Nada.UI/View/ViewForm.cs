using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.UI.Base;

namespace Nada.UI.View
{
    public partial class ViewForm : BaseForm
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        IView viewControl = null;

        public ViewForm()
            : base()
        {
            InitializeComponent();
        }

        public ViewForm(IView view)
            : base()
        {
            InitializeComponent();
            viewControl = view;
            view.OnClose = () => { this.Close(); };
            pnlView.Controls.Add((UserControl)view);
            this.Text = view.Title;
            view.StatusChanged = (s) => { StatusChanged(s); };
        }

        private void StatusChanged(string status)
        {
            lblLastUpdated.Text = status;
        }

        private void ViewForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                viewControl.SetFocus();   
            }
        }

        private void ViewForm_SizeChanged(object sender, EventArgs e)
        {
        }

        private void ViewForm_Shown(object sender, EventArgs e)
        {
        }
    }
}
