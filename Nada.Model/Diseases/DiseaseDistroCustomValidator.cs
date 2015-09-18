using Nada.Globalization;
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
            IndicatorValue currentValue = values.FirstOrDefault(v => v.Indicator.DisplayName == indicator.DisplayName);
            if (currentValue == null)
                return "";

            switch (indicator.DisplayName)
            {
                case "DDLFPopulationAtRisk":
                    IndicatorValue related1 = values.FirstOrDefault(v => v.Indicator.DisplayName == "DDLFPopulationRequiringPc");
                    if (related1 != null)
                    {
                        double currentPop, relatedPop;
                        if (Double.TryParse(currentValue.DynamicValue, out currentPop) && Double.TryParse(related1.DynamicValue, out relatedPop))
                        {
                            if (currentPop < relatedPop)
                                return Translations.BrettTestErrorMessage;
                        }
                    }
                    break;
                default:
                    break;
            }

            return "";
        }

        public List<ValidationResult> ValidateIndicators(Dictionary<string, Indicator> indicators, List<IndicatorValue> values, List<KeyValuePair<string, string>> metaData)
        {
            throw new NotImplementedException();
        }
    }
}
