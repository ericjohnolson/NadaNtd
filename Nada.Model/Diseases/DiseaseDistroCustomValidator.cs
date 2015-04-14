using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model.Diseases
{
    public class DiseaseDistroCustomValidator : ICustomValidator
    {
        public string Valid(Indicator indicator, List<IndicatorValue> values)
        {
            return "";
        }
    }
}
