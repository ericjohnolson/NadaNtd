using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Nada.Model;

namespace Nada.UI.ViewModel
{
    public interface IDataEntryVm
    {
        bool CanEditTypeName { get; }
        string StatusMessage { get; }
        string Title { get; }
        string TypeTitle { get; }
        string Notes { get; }
        ICalcIndicators Calculator { get; }
        Color FormColor { get; }
        AdminLevel Location { get; }
        Dictionary<string, Indicator> Indicators { get; }
        List<IndicatorValue> IndicatorValues { get; }
        List<IndicatorDropdownValue> IndicatorDropdownValues { get; }
        IndicatorEntityType EntityType { get;  }

        bool IsValid();
        void DoSave(List<IndicatorValue> indicatorValues, string notes);

        void DoSaveType(string p);
    }
}
