using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Globalization;
using Nada.UI.Base;

namespace Nada.UI.Controls
{
    public partial class StatCalculator : BaseControl
    {
        public ICalcIndicators Calc { get; set; }
        public event Action OnCalc = () => { };

        public StatCalculator()
            : base()
        {
            InitializeComponent();
        }
        
        private void fieldLink1_OnClick()
        {
            OnCalc();
        }

        public void DoCalc(List<IndicatorValue> values, int adminLevel)
        {
            this.SuspendLayout();
            tblIndicators.Controls.Clear();
            int count = 0;
            int labelRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int controlRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int columnCount = 0;
            foreach (var item in Calc.PerformCalculations(values, adminLevel))
            {
                if (count % 2 == 0)
                {
                    labelRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    controlRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    columnCount = 0;
                }

                // Add label
                tblIndicators.Controls.Add(
                    new H3bLabel { Text = TranslationLookup.GetValue(item.Key, item.Key), Name = "ciLabel_" + item.Key, AutoSize = true, },
                    columnCount, labelRowIndex);

                // Add val
                var label = new H3bLabel { Text = item.Value, Name = "ciLabel_Val" + item.Key, AutoSize = true, };
                label.MakeBold();
                tblIndicators.Controls.Add(label, columnCount, controlRowIndex);

                count++;
                columnCount = columnCount + 2;
            }

            this.ResumeLayout();
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color TextColor
        {
            get { return lblCustomIndicators.ForeColor; }
            set
            {
                lblCustomIndicators.ForeColor = value;
                hr4.BackColor = value;
            }
        }

    }
}
