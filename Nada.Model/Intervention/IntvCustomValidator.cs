using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model.Intervention
{
    public class IntvCustomValidator : BaseValidator
    {
        public override string Valid(Indicator indicator, List<IndicatorValue> values)
        {
            return "";
        }

        public override Dictionary<string, List<ValidationMapping>> GetMapInstance(bool instantiate)
        {
            if (ValidationMap == null && instantiate)
            {
                ValidationMap = new Dictionary<string, List<ValidationMapping>>()
                {
                    {
                        "PcIntvNumEligibleIndividualsTargeted",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvSthAtRisk", "PcIntvLfAtRisk", "PcIntvOnchoAtRisk"),
                            new ValidationMapping(ValidationRuleType.EqualToSum, "PcIntvNumEligibleFemalesTargeted", "PcIntvNumEligibleMalesTargeted")
                        }
                    },
                    {
                        "PcIntvNumEligibleFemalesTargeted",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvNumEligibleIndividualsTargeted")
                        }
                    },
                    {
                        "PcIntvNumEligibleMalesTargeted",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvNumEligibleIndividualsTargeted")
                        }
                    },
                    {
                        "DateReported",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda")
                        }
                    },
                    {
                        "PcIntvEndDateOfMda",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.DateLaterThan, "PcIntvStartDateOfMda")
                        }
                    }
                };
            }
            return ValidationMap;
        }
    }
}
