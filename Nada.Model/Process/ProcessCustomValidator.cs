using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model.Process
{
    public class ProcessCustomValidator : ICustomValidator
    {
        public string Valid(Indicator indicator, List<IndicatorValue> values)
        {
            return "";
        }
    }
}
