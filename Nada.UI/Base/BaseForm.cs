using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nada.UI.Base
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            InitializeComponent();
        }

        public static int GetDropdownWidth(IEnumerable<string> values)
        {
            int maxWidth = 0;
            int temp = 0;
            Label label1 = new Label();
            label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            foreach (var val in values)
            {
                label1.Text = val;
                temp = label1.PreferredWidth;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }
            label1.Dispose();
            return maxWidth;
        }
    }
}
