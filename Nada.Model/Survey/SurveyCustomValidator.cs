﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model.Survey
{
    public class SurveyCustomValidator : ICustomValidator
    {
        public string Valid(Indicator indicator, List<IndicatorValue> values)
        {
            return "";
        }

        public List<KeyValuePair<string, string>> ValidateIndicators(Dictionary<string, Indicator> indicators, List<IndicatorValue> values)
        {
            throw new NotImplementedException();
        }
    }
}
