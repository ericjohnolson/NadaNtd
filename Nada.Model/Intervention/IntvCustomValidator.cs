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
                    },
                    {
                        "PcIntvNumEligibleIndividualsTargeted",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvSthAtRisk", "PcIntvLfAtRisk", "PcIntvOnchoAtRisk", "PcIntvSchAtRisk", "PcIntvTraAtRisk"),
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
                        "PcIntvNumSacTargeted",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvNumEligibleIndividualsTargeted"),
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSchSacAtRisk", "PcIntvSthSacAtRisk")
                        }
                    },
                    {
	                    "PcIntvNumPsacTargeted",
	                    new List<ValidationMapping>
	                    {
		                    new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSthPsacAtRisk"),
                            new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvNumEligibleIndividualsTargeted")
	                    }
                    },
                    {
	                    "PcIntvNumAdultsTargeted",
	                    new List<ValidationMapping>
	                    {
		                    new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvNumEligibleIndividualsTargeted")
	                    }
                    },
                    {
                        "PcIntvOfTotalTargetedForOncho",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvNumEligibleIndividualsTargeted"),
                            new ValidationMapping(ValidationRuleType.EqualToSum, "PcIntvOfTotalFemalesTargetedOncho", "PcIntvOfTotalMalesTargetedOncho")
                        }
                    },
                    {
                        "PcIntvNumIndividualsTreated",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvNumEligibleIndividualsTargeted"),
                            new ValidationMapping(ValidationRuleType.EqualToSum, "PcIntvNumFemalesTreated", "PcIntvNumMalesTreated"),
                            new ValidationMapping(ValidationRuleType.EqualToSum, "PcIntvNumTreatedZx", "PcIntvNumTreatedTeo", "PcIntvNumTreatedZxPos")
                        }
                    },
                    {
	                    "PcIntvNumSacTreated",
	                    new List<ValidationMapping>
	                    {
		                    new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvNumIndividualsTreated"),
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvNumSacTargeted"),
                            new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac")
	                    }
                    },
                    {
	                    "PcIntvPsacTreated",
	                    new List<ValidationMapping>
	                    {
		                    new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvNumIndividualsTreated"),
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvNumPsacTargeted")
	                    }
                    },
                    {
	                    "PcIntvNumAdultsTreated",
	                    new List<ValidationMapping>
	                    {
		                    new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvNumIndividualsTreated")
	                    }
                    },
                    {
	                    "PcIntvOfTotalFemalesOncho",
	                    new List<ValidationMapping>
	                    {
		                    new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvOfTotalTreatedForOncho")
	                    }
                    },
                    {
	                    "PcIntvOfTotalMalesOncho",
	                    new List<ValidationMapping>
	                    {
		                    new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvOfTotalTreatedForOncho")
	                    }
                    },
                    {
	                    "PcIntvOfTotalTreatedForOncho",
	                    new List<ValidationMapping>
	                    {
		                    new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvNumIndividualsTreated"),
                            new ValidationMapping(ValidationRuleType.EqualToSum, "PcIntvOfTotalFemalesOncho", "PcIntvOfTotalMalesOncho")
	                    }
                    },
                    {
	                    "PcIntvNumSeriousAdverseEventsReported",
	                    new List<ValidationMapping>
	                    {
		                    new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvNumIndividualsTreated")
	                    }
                    },
                    {
	                    "PcIntvNumTreatedZx",
	                    new List<ValidationMapping>
	                    {
		                    new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvNumIndividualsTreated")
	                    }
                    },
                    {
	                    "PcIntvNumTreatedTeo",
	                    new List<ValidationMapping>
	                    {
		                    new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvNumIndividualsTreated")
	                    }
                    },
                    {
	                    "PcIntvNumTreatedZxPos",
	                    new List<ValidationMapping>
	                    {
		                    new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvNumIndividualsTreated")
	                    }
                    },
                    {
	                    "LFMMDPNumLymphoedemaPatientsTreated",
	                    new List<ValidationMapping>
	                    {
		                    new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMMDPNumLymphoedemaPatients")
	                    }
                    },
                    {
	                    "LFMMDPNumHydroceleCasesTreated",
	                    new List<ValidationMapping>
	                    {
		                    new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMMDPNumHydroceleCases")
	                    }
                    },
                    {
	                    "PcIntvTsNumOperated",
	                    new List<ValidationMapping>
	                    {
		                    new ValidationMapping(ValidationRuleType.EqualToSum, "PcIntvTsNumFemales", "PcIntvTsNumMales")
	                    }
                    }
                };
            }
            return ValidationMap;
        }
    }
}
