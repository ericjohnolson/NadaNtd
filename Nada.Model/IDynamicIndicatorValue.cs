using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public interface IDynamicIndicatorValue
    {
        int IndicatorId { get; set; }
        string DynamicValue { get; set; }
    }
}
