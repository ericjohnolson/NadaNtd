using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public interface IHaveDynamicIndicators
    {
        Dictionary<string, Indicator> Indicators { get; set; }
        List<IndicatorDropdownValue> IndicatorDropdownValues { get; set; }
    }

    public interface IHaveDynamicIndicatorValues
    {
        int Id { get; set; }
        List<IndicatorValue> IndicatorValues { get; set; }
        string Notes { get; set; }
        Boolean IsValid();
        string GetAllErrors(bool showNames);
        Nullable<int> AdminLevelId { get;  }
    }
}
