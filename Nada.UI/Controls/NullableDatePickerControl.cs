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
    public partial class NullableDatePickerControl : UserControl
    {
        public bool ShowClear { get; set; }

        public NullableDatePickerControl()
        {
            InitializeComponent();
        }

        private void h3Link1_ClickOverride()
        {
            nullableDateTimePicker1.Clear();
        }

        private void h3Link1_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                h3Link1.Visible = ShowClear;
            }
        }

        public DateTime GetValue()
        {
            return nullableDateTimePicker1.Value;
        }

        public DateTime Value
        {
            get
            {
                return nullableDateTimePicker1.Value;
            }
            set
            {
                nullableDateTimePicker1.Value = value;
            }
        }
    }
}
