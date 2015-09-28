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

        public override Dictionary<string, List<ValidationMapping>> GetMapInstance(string formTranslationKey, bool instantiate)
        {
            if (ValidationMap == null && instantiate)
            {
                ValidationMap = new Dictionary<string, List<ValidationMapping>>();

                if (formTranslationKey == "LF")
                {
                    ValidationMap.Add("DDLFPopulationAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDLFTotalPopulation")
                        });
                    ValidationMap.Add("DDLFPopulationRequiringPc",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDLFPopulationAtRisk")
                        });
                    ValidationMap.Add("DDLFHighRiskAdultsAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDLFPopulationAtRisk")
                        });
                }
                else if (formTranslationKey == "Oncho")
                {
                    ValidationMap.Add("DDOnchoPopulationAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDOnchoTotalPopulation")
                        });
                    ValidationMap.Add("DDOnchoPopulationRequiringPc",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDOnchoPopulationAtRisk")
                        });
                }
                else if (formTranslationKey == "Schisto")
                {
                    ValidationMap.Add("DDSchistoPopulationAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDSchistoTotalPopulation")
                        });
                    ValidationMap.Add("DDSchistoPopulationRequiringPc",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDSchistoPopulationAtRisk")
                        });
                    ValidationMap.Add("DDSchistoSacAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanSum, "DDSchistoPopulationAtRisk")
                        });
                    ValidationMap.Add("DDSchistoHighRiskAdultsAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanSum, "DDSchistoPopulationAtRisk")
                        });
                }
                else if (formTranslationKey == "STH")
                {
                    ValidationMap.Add("DDSTHPopulationAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDSTHTotalPopulation")
                        });
                    ValidationMap.Add("DDSTHPopulationRequiringPc",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDSTHPopulationAtRisk")
                        });
                    ValidationMap.Add("DDSTHPsacAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanSum, "DDSTHPopulationAtRisk")
                        });
                    ValidationMap.Add("DDSTHSacAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanSum, "DDSTHPopulationAtRisk")
                        });
                    ValidationMap.Add("DDSTHHighRiskAdultsAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanSum, "DDSTHPopulationAtRisk")
                        });
                }
                else if (formTranslationKey == "Trachoma")
                {
                    ValidationMap.Add("DDTraPopulationAtRisk",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "DDTraTotalPopulation")
                        });
                }

            }
            return ValidationMap;
        }
    }
}
