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
        List<IndicatorValue> IndicatorValues { get; set; }
    }
}
