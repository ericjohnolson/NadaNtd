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
using Nada.UI.Controls;
using System.Threading;

namespace Nada.UI.View
{
    public partial class ReportIndicatorControl : UserControl
    {
        private List<ReportIndicator> indicatorList = null;
        private List<IndicatorContainer> controlList = new List<IndicatorContainer>();

        public ReportIndicatorControl()
        {
            InitializeComponent();
        }

        public void LoadIndicators(List<ReportIndicator> indicators)
        {
            indicatorList = indicators;

            BackgroundWorker sleeper = new BackgroundWorker();
            sleeper.DoWork += sleeper_DoWork;
            sleeper.RunWorkerCompleted += sleeper_RunWorkerCompleted;
            sleeper.RunWorkerAsync();
        }

        void sleeper_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(300);
        }

        void sleeper_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.SuspendLayout();

            controlList = new List<IndicatorContainer>();
            tblIndicators.Controls.Clear();
            CreateIndicatorSet(indicatorList, tblIndicators);

            this.ResumeLayout();
            loading1.Visible = false;
        }

        public void CreateIndicatorSet(List<ReportIndicator> indicators, TableLayoutPanel pnlIndicatorSet)
        {
            int count = 0;
            int controlRowIndex = pnlIndicatorSet.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int columnCount = 0;
            foreach (var i in indicators)
            {
                if (count % 2 == 0)
                {
                    controlRowIndex = pnlIndicatorSet.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    columnCount = 0;
                }

                // Add field
                var chk = new CheckBox { Text = i.Name, AutoSize = true };
                controlList.Add(new IndicatorContainer
                {
                    Ind = i,
                    Chk = chk
                });
                tblIndicators.Controls.Add(chk, columnCount, controlRowIndex);

                count++;
                columnCount = columnCount + 2;
            }
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
