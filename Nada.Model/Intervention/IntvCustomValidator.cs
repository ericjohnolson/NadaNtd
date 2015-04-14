using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model.Intervention
{
    public class IntvCustomValidator : ICustomValidator
    {
        public string Valid(Indicator indicator, List<IndicatorValue> values)
        {
            return "";
        }
    }
}
