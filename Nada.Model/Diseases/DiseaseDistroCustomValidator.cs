using Nada.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model.Diseases
{
    public class DiseaseDistroCustomValidator : BaseValidator
    {
        public override string Valid(Indicator indicator, List<IndicatorValue> values)
        {
            //IndicatorValue currentValue = values.FirstOrDefault(v => v.Indicator.DisplayName == indicator.DisplayName);
            //if (currentValue == null)
            //    return "";

            //switch (indicator.DisplayName)
            //{
            //    case "DDLFPopulationAtRisk":
            //        IndicatorValue related1 = values.FirstOrDefault(v => v.Indicator.DisplayName == "DDLFPopulationRequiringPc");
            //        if (related1 != null)
            //        {
            //            double currentPop, relatedPop;
            //            if (Double.TryParse(currentValue.DynamicValue, out currentPop) && Double.TryParse(related1.DynamicValue, out relatedPop))
            //            {
            //                if (currentPop < relatedPop)
            //                    return Translations.BrettTestErrorMessage;
            //            }
            //        }
            //        break;
            //    default:
            //        break;
            //}

            return "";
        }

        public override Dictionary<string, List<ValidationMapping>> GetMapInstance(bool instantiate)
        {
            if (ValidationMap == null && instantiate)
            {
                ValidationMap = new Dictionary<string, List<ValidationMapping>>()
                {
                    {
                        "DDLFPopulationAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDLFTotalPopulation")
                        }
                    },
                    {
                        "DDLFPopulationRequiringPc",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDLFPopulationAtRisk")
                        }
                    },
                    {
                        "DDLFHighRiskAdultsAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDLFPopulationAtRisk")
                        }
                    },
                    {
                        "DDOnchoPopulationAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDOnchoTotalPopulation")
                        }
                    },
                    {
                        "DDOnchoPopulationRequiringPc",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDOnchoPopulationAtRisk")
                        }
                    },
                    {
                        "DDSchistoPopulationAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDSchistoTotalPopulation")
                        }
                    },
                    {
                        "DDSchistoPopulationRequiringPc",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDSchistoPopulationAtRisk")
                        }
                    },
                    {
                        "DDSchistoSacAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDSchistoPopulationAtRisk")
                        }
                    },
                    {
                        "DDSchistoHighRiskAdultsAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDSchistoPopulationAtRisk")
                        }
                    },
                    {
                        "DDSTHPopulationAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDSTHTotalPopulation")
                        }
                    },
                    {
                        "DDSTHPopulationRequiringPc",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDSTHPopulationAtRisk")
                        }
                    },
                    {
                        "DDSTHPsacAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDSTHPopulationAtRisk")
                        }
                    },
                    {
                        "DDSTHSacAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDSTHPopulationAtRisk")
                        }
                    },
                    {
                        "DDSTHHighRiskAdultsAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDSTHPopulationAtRisk")
                        }
                    },
                    {
                        "DDTraPopulationAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDTraTotalPopulation")
                        }
                    }
                };
            }
            return ValidationMap;
        }
    }
}
