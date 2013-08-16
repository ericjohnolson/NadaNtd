using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;

namespace Nada.UI.View
{
    public partial class CustomIndicatorControl : UserControl
    {
        private List<DynamicContainer> controlList = new List<DynamicContainer>();

        public CustomIndicatorControl()
        {
            InitializeComponent();
        }

        public void LoadIndicators(IEnumerable<IDynamicIndicator> indicators)
        {
            foreach (var indicator in indicators.OrderByDescending(i => i.SortOrder))
            {
                // Add field
                tblIndicators.RowStyles.Insert(0, new RowStyle { SizeType = SizeType.AutoSize });
                tblIndicators.Controls.Add(CreateControl(indicator), 0, 0);
                // Add label
                tblIndicators.RowStyles.Insert(0, new RowStyle { SizeType = SizeType.AutoSize });
                tblIndicators.Controls.Add(new Label { Text = indicator.DisplayName, Name = "ciLabel_" + indicator.Id }, 0, 0);
            }

            lblPlaceholder.Visible = false;
        }

        public List<T> GetValues<T>() where T : IDynamicIndicatorValue
        {
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(typeof(T));
            var valList = (List<T>)Activator.CreateInstance(constructedListType);

            foreach (DynamicContainer cnt in controlList)
            {
                T val = (T)Activator.CreateInstance(typeof(T));
                val.DynamicValue = cnt.GetValue();
                val.IndicatorId = cnt.IndicatorTypeId;
                valList.Add(val);
            }
            return valList;
        }

        private Control CreateControl(IDynamicIndicator indicator)
        {
            if (indicator.DataTypeId == (int)IndicatorDataType.Date)
            {
                var cntrl = new DateTimePicker { Name = indicator.Id.ToString() };
                controlList.Add(new DynamicContainer
                {
                    IndicatorTypeId = indicator.Id,
                    GetValue = () => { return cntrl.Value.ToString("MM/dd/yyyy"); }
                });
                return cntrl;
            }
            else if (indicator.DataTypeId == (int)IndicatorDataType.Number)
            {
                var cntrl = new TextBox { Name = indicator.Id.ToString() };
                controlList.Add(new DynamicContainer
                {
                    IndicatorTypeId = indicator.Id,
                    GetValue = () => { return cntrl.Text; }
                });
                return cntrl;

            }
            else if (indicator.DataTypeId == (int)IndicatorDataType.YesNo)
            {
                var cntrl = new CheckBox { Name = indicator.Id.ToString() };
                controlList.Add(new DynamicContainer
                {
                    IndicatorTypeId = indicator.Id,
                    GetValue = () => { return Convert.ToInt32(cntrl.Checked).ToString(); }
                });
                return cntrl;

            }
            else
            {
                var cntrl = new TextBox { Name = indicator.Id.ToString() };
                controlList.Add(new DynamicContainer
                {
                    IndicatorTypeId = indicator.Id,
                    GetValue = () => { return cntrl.Text; }
                });
                return cntrl;
            }
        }

        public class DynamicContainer
        {
            public int IndicatorTypeId { get; set; }
            public delegate string GetValueDelegate();
            public GetValueDelegate GetValue { get; set; }
        }

    }
}
