using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.Base;

namespace Nada.UI.Controls
{
    public partial class CollapsiblePanel : BaseControl
    {
        bool isCollapsed = false;

        public CollapsiblePanel()
            : base()
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
                return lblHeader.Text;
            }
            set
            {
                lblHeader.Text = value;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color TextColor
        {
            get
            {
                return lblHeader.ForeColor;
            }
            set
            {
                lblHeader.ForeColor = value;
                hr1.ForeColor = value;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool PanelAutoSize
        {
            get { return pnlContent.AutoSize; }
            set { pnlContent.AutoSize = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                this.button1.Image = global::Nada.UI.Properties.Resources.ExpanderMinusIcon16x16;
                pnlContent.Visible = true;
                isCollapsed = false;
            }
            else
            {
                this.button1.Image = global::Nada.UI.Properties.Resources.ExpanderPlusIcon16x16;
                pnlContent.Visible = false;
                isCollapsed = true;
            }
        }
    }
}
