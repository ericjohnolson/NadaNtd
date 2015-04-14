using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public interface ICustomValidator
    {
        string Valid(Indicator indicator, List<IndicatorValue> values);
    }
}
