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

        public override Dictionary<string, List<ValidationMapping>> GetMapInstance(string formTranslationKey, bool instantiate)
        {
            if (ValidationMap == null && instantiate)
            {
                ValidationMap = new Dictionary<string, List<ValidationMapping>>()
                {
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
                            new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvNumEligibleIndividualsTargeted")
                        }
                    },
                    {
	                    "PcIntvNumPsacTargeted",
	                    new List<ValidationMapping>
	                    {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvNumEligibleIndividualsTargeted")
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
                            new ValidationMapping(ValidationRuleType.EqualToSum, "PcIntvNumFemalesTreated", "PcIntvNumMalesTreated")
                        }
                    },
                    {
	                    "PcIntvNumSacTreated",
	                    new List<ValidationMapping>
	                    {
		                    new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvNumIndividualsTreated"),
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvNumSacTargeted")
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

                if (formTranslationKey == "IntvIvmPzqAlb")
                {
                    ValidationMap["DateReported"].Add(new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    ValidationMap["PcIntvNumEligibleIndividualsTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvSthAtRisk", "PcIntvLfAtRisk", "PcIntvOnchoAtRisk", "PcIntvSchAtRisk"));
                    ValidationMap["PcIntvNumSacTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSchSacAtRisk", "PcIntvSthSacAtRisk"));
                    ValidationMap["PcIntvNumSacTreated"].Add(new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvIvmAlb")
                {
                    ValidationMap["DateReported"].Add(new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    ValidationMap["PcIntvNumEligibleIndividualsTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvSthAtRisk", "PcIntvLfAtRisk", "PcIntvOnchoAtRisk"));
                    ValidationMap["PcIntvNumSacTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSthSacAtRisk"));
                    ValidationMap["PcIntvNumSacTreated"].Add(new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvIvmPzq")
                {
                    ValidationMap["DateReported"].Add(new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    ValidationMap["PcIntvNumEligibleIndividualsTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvOnchoAtRisk", "PcIntvSchAtRisk"));
                    ValidationMap["PcIntvNumSacTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSchSacAtRisk"));
                    ValidationMap["PcIntvNumSacTreated"].Add(new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvIvm")
                {
                    ValidationMap["DateReported"].Add(new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    ValidationMap["PcIntvNumEligibleIndividualsTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvOnchoAtRisk"));
                    ValidationMap["PcIntvNumSacTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSchSacAtRisk", "PcIntvSthSacAtRisk"));
                }
                else if (formTranslationKey == "IntvDecAlb")
                {
                    ValidationMap["DateReported"].Add(new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    ValidationMap["PcIntvNumEligibleIndividualsTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvSthAtRisk", "PcIntvLfAtRisk"));
                    ValidationMap["PcIntvNumSacTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSthSacAtRisk"));
                    ValidationMap["PcIntvNumSacTreated"].Add(new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvPzqAlb")
                {
                    ValidationMap["DateReported"].Add(new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    ValidationMap["PcIntvNumEligibleIndividualsTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvSthAtRisk", "PcIntvSchAtRisk"));
                    ValidationMap["PcIntvNumSacTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSchSacAtRisk", "PcIntvSthSacAtRisk"));
                    ValidationMap["PcIntvNumSacTreated"].Add(new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvAlb2")
                {
                    ValidationMap["DateReported"].Add(new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    ValidationMap["PcIntvNumEligibleIndividualsTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvLfAtRisk"));
                    ValidationMap["PcIntvNumSacTreated"].Add(new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvAlb")
                {
                    ValidationMap["DateReported"].Add(new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    ValidationMap["PcIntvNumEligibleIndividualsTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvSthAtRisk"));
                    ValidationMap["PcIntvNumSacTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSthSacAtRisk"));
                    ValidationMap["PcIntvNumSacTreated"].Add(new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvMbd")
                {
                    ValidationMap["DateReported"].Add(new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    ValidationMap["PcIntvNumEligibleIndividualsTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvSthAtRisk"));
                    ValidationMap["PcIntvNumSacTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSthSacAtRisk"));
                    ValidationMap["PcIntvNumSacTreated"].Add(new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvPzq")
                {
                    ValidationMap["DateReported"].Add(new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    ValidationMap["PcIntvNumEligibleIndividualsTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvSchAtRisk"));
                    ValidationMap["PcIntvNumSacTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSchSacAtRisk"));
                    ValidationMap["PcIntvNumSacTreated"].Add(new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvZithroTeo")
                {
                    ValidationMap["DateReported"].Add(new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    ValidationMap["PcIntvNumEligibleIndividualsTargeted"].Add(new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvTraAtRisk"));
                    ValidationMap["PcIntvNumIndividualsTreated"].Add(new ValidationMapping(ValidationRuleType.EqualToSum, "PcIntvNumTreatedZx", "PcIntvNumTreatedTeo", "PcIntvNumTreatedZxPos"));
                }
                else if (formTranslationKey == "LfMorbidityManagment")
                {

                }
                else if (formTranslationKey == "TsSurgeries")
                {

                }
            }
            return ValidationMap;
        }
    }
}
