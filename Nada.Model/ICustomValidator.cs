using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public interface ICustomValidator
    {
        List<KeyValuePair<string, string>> ValidateIndicators(Dictionary<string, Indicator> indicators, List<IndicatorValue> values);
        string Valid(Indicator indicator, List<IndicatorValue> values);
    }
}
