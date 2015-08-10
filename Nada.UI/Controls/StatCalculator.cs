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
using Nada.UI.View;

namespace Nada.UI.Controls
{


    public partial class StatCalculator : BaseControl
    {
        public ICalcIndicators Calc { get; set; }
        public event Action OnCalc = () => { };
        public Action OnFocus { get; set; }

        public StatCalculator()
            : base()
        {
            InitializeComponent();
        }

        private void fieldLink1_OnClick()
        {
            OnCalc();
        }

        public void DoFocus()
        {
            fieldLink1.Focus();
        }

        public void DoCalc(Dictionary<string, Indicator> indicators, List<IndicatorValue> values, int adminLevel, string typeId, string formTranslationKey,
            DateTime start, DateTime end, Action doFocus)
        {
            OnFocus = doFocus;
            lblCalculating.Visible = true;
            BackgroundWorker calcWorker = new BackgroundWorker();
            calcWorker.RunWorkerCompleted += calcWorker_RunWorkerCompleted;
            calcWorker.DoWork += calcWorker_DoWork;
            calcWorker.RunWorkerAsync(new CalcWorkerPayload
            {
                AdminLevel = adminLevel,
                End = end,
                Indicators = indicators,
                Start = start,
                TypeId = typeId,
                FormTranslationKey = formTranslationKey,
                Values = values
            });
        }

        void calcWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                CalcWorkerPayload payload = (CalcWorkerPayload)e.Argument;
                e.Result = Calc.PerformCalculations(payload.Indicators, payload.Values, payload.AdminLevel, payload.TypeId,
                    payload.FormTranslationKey, payload.Start, payload.End);
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error calcWorker_DoWork. ", ex);
                throw;
            }
        }

        void calcWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.IsDisposed)
                return;

            List<KeyValuePair<string, string>> calculationResults = (List<KeyValuePair<string, string>>)e.Result;

            tblIndicators.Visible = false;
            this.SuspendLayout();
            tblIndicators.Controls.Clear();
            int count = 0;
            int labelRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int controlRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            int columnCount = 0;

            if (calculationResults.Count == 0)
                this.Visible = false;

            foreach (var item in calculationResults)
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
            tblIndicators.Visible = true;
            lblCalculating.Visible = false;
            OnFocus();
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

    public class CalcWorkerPayload
    {
        public Dictionary<string, Indicator> Indicators { get; set; }
        public List<IndicatorValue> Values { get; set; }
        public int AdminLevel { get; set; }
        public string TypeId { get; set; }
        public string FormTranslationKey { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
