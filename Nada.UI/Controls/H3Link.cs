using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nada.UI.Controls
{
    public partial class H3Link : UserControl
    {
        public event Action ClickOverride = () => { };

        public H3Link()
        {
            InitializeComponent();
        }


        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get
            {
                return linkLabel1.Text;
            }
            set
            {
                linkLabel1.Text = value;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClickOverride();
        }
    }
}
