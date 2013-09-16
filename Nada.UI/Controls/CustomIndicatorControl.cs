using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.UI.Controls;

namespace Nada.UI.View
{
    public partial class CustomIndicatorControl : UserControl
    {
        public event Action OnAddRemove = () => { };
        private List<DynamicContainer> controlList = new List<DynamicContainer>();

        public CustomIndicatorControl()
        {
            InitializeComponent();
        }

        public void LoadIndicators(IEnumerable<Indicator> indicators)
        {
            this.SuspendLayout();
            controlList = new List<DynamicContainer>();
            tblIndicators.Controls.Clear();
            int count = 0;
            int labelRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize }); 
            int controlRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize }); 
            int columnCount = 0;
            foreach (var indicator in indicators.OrderBy(i => i.SortOrder).ToList())
            {
                if (count % 4 == 0)
                {
                    labelRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    controlRowIndex = tblIndicators.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    columnCount = 0;
                }

                // Add label
                tblIndicators.Controls.Add(
                    new H3Label { Text = indicator.DisplayName, Name = "ciLabel_" + indicator.Id, AutoSize = true, },
                    columnCount, labelRowIndex);

                // Add field
                tblIndicators.Controls.Add(CreateControl(indicator), columnCount, controlRowIndex);

                count++;
                columnCount = columnCount + 2;
            }
            this.ResumeLayout();
        }

        public List<T> GetValues<T>() where T : IndicatorValue
        {
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(typeof(T));
            var valList = (List<T>)Activator.CreateInstance(constructedListType);

            foreach (DynamicContainer cnt in controlList)
            {
                T val = (T)Activator.CreateInstance(typeof(T));
                val.DynamicValue = cnt.GetValue();
                val.Indicator = cnt.Indicator;
                valList.Add(val);
            }
            return valList;
        }

        public List<IndicatorValue> GetValues() 
        {
            var valList = new List<IndicatorValue>();

            foreach (DynamicContainer cnt in controlList)
            {
                IndicatorValue val = new IndicatorValue();
                val.DynamicValue = cnt.GetValue();
                val.Indicator = cnt.Indicator;
                val.IndicatorId = cnt.Indicator.Id;
                valList.Add(val);
            }
            return valList;
        }

        private Control CreateControl(Indicator indicator)
        {
            if (indicator.DataTypeId == (int)IndicatorDataType.Date)
            {
                var cntrl = new DateTimePicker { Name = "dynamicDt" + indicator.Id.ToString() };
                controlList.Add(new DynamicContainer
                {
                    Indicator = indicator,
                    GetValue = () => { return cntrl.Value.ToString("MM/dd/yyyy"); }
                });
                return cntrl;
            }
            else if (indicator.DataTypeId == (int)IndicatorDataType.Number)
            {
                var cntrl = new TextBox { Name = "dynamicNum" + indicator.Id.ToString() };
                controlList.Add(new DynamicContainer
                {
                    Indicator = indicator,
                    GetValue = () => { return cntrl.Text; }
                });
                return cntrl;

            }
            else if (indicator.DataTypeId == (int)IndicatorDataType.YesNo)
            {
                var cntrl = new CheckBox { Name = "dynamicChk" + indicator.Id.ToString() };
                controlList.Add(new DynamicContainer
                {
                    Indicator = indicator,
                    GetValue = () => { return Convert.ToInt32(cntrl.Checked).ToString(); }
                });
                return cntrl;

            }
            else
            {
                var cntrl = new TextBox { Name = "dynamicTxt" + indicator.Id.ToString() };
                controlList.Add(new DynamicContainer
                {
                    Indicator = indicator,
                    GetValue = () => { return cntrl.Text; }
                });
                return cntrl;
            }
        }

        public class DynamicContainer
        {
            public Indicator Indicator { get; set; }
            public delegate string GetValueDelegate();
            public GetValueDelegate GetValue { get; set; }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color TextColor
        {
            get { return lblCustomIndicators.ForeColor; }
            set { lblCustomIndicators.ForeColor = value; }
        }

        private void fieldLink1_OnClick()
        {
            OnAddRemove();
        }


    }
}
