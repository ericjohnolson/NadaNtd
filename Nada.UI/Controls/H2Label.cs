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
    public partial class H2Label : UserControl
    {
        public H2Label()
        {
            InitializeComponent();
            
            label1.MaximumSize = new Size(370, 0);
            label1.AutoSize = true;
        }

        public void SetMaxWidth(int maxWidth)
        {
            label1.MaximumSize = new Size(maxWidth, 0);
            label1.AutoSize = true;
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color TextColor
        {
            get { return label1.ForeColor; }
            set { label1.ForeColor = value; }
        }

    }
}
