using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public interface ICalcIndicators
    {
        List<KeyValuePair<string, string>> PerformCalculations(List<IndicatorValue> indicatorValues, int adminLevel);
    }


}
