using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.UI.Base;

namespace Nada.UI.Controls
{
    public partial class Loading : BaseControl
    {
        public Loading()
            : base()
        {
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public string StatusMessage
        {
            get
            {
                return h3Label1.Text;
            }
            set
            {
                h3Label1.Text = value;
            }
        }

    }
}
