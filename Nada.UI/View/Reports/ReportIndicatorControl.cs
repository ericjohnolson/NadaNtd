using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Reports;

namespace Nada.UI.View
{
    public partial class ReportIndicatorControl : UserControl
    {
        private List<IndicatorContainer> controlList = new List<IndicatorContainer>();

        public ReportIndicatorControl()
        {
            InitializeComponent();
        }

        public void LoadIndicators(IEnumerable<ReportIndicator> indicators)
        {
            controlList = new List<IndicatorContainer>();
            tblIndicators.Controls.Clear();

            foreach (var i in indicators.ToList())
            {
                // Add field
                int index = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                var chk = new CheckBox { Text = i.Name, AutoSize = true };
                controlList.Add(new IndicatorContainer
                {
                    Ind = i,
                    Chk = chk
                });
                tblIndicators.Controls.Add(chk, 0, index);
           }

            lblPlaceholder.Visible = false;
        }

        public List<ReportIndicator> GetValues()
        {
            List<ReportIndicator> indicators = new List<ReportIndicator>();
            foreach (IndicatorContainer cnt in controlList)
            {
                cnt.Ind.Selected = cnt.Chk.Checked;
                indicators.Add(cnt.Ind);
            }
            return indicators;
        }

        private class IndicatorContainer
        {
            public ReportIndicator Ind { get; set; }
            public CheckBox Chk { get; set; }
        }

    }
}
