using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model.Intervention
{
    /// <summary>
    /// Handles validation for Intervention entities
    /// </summary>
    public class IntvCustomValidator : BaseValidator
    {
        public override string Valid(Indicator indicator, List<IndicatorValue> values)
        {
            return "";
        }

        /// <summary>
        /// Establishes the validation rules for any Intervention indicators that need validation
        /// </summary>
        /// <param name="formTranslationKey">The translation key of the form that the indicators belong to</param>
        /// <param name="instantiate">Whether or not to instantiate the collection</param>
        /// <returns>Collection of validation rules</returns>
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
                    AddToMap("DateReported", new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    AddToMap("PcIntvNumEligibleIndividualsTargeted", new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvSthAtRisk", "PcIntvLfAtRisk", "PcIntvOnchoAtRisk", "PcIntvSchAtRisk"));
                    AddToMap("PcIntvNumSacTargeted", new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSchSacAtRisk", "PcIntvSthSacAtRisk"));
                    AddToMap("PcIntvNumSacTreated", new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvIvmAlb")
                {
                    AddToMap("DateReported", new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    AddToMap("PcIntvNumEligibleIndividualsTargeted", new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvSthAtRisk", "PcIntvLfAtRisk", "PcIntvOnchoAtRisk"));
                    AddToMap("PcIntvNumSacTargeted", new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSthSacAtRisk"));
                    AddToMap("PcIntvNumSacTreated", new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvIvmPzq")
                {
                    AddToMap("DateReported", new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    AddToMap("PcIntvNumEligibleIndividualsTargeted", new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvOnchoAtRisk", "PcIntvSchAtRisk"));
                    AddToMap("PcIntvNumSacTargeted", new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSchSacAtRisk"));
                    AddToMap("PcIntvNumSacTreated", new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvIvm")
                {
                    AddToMap("DateReported", new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    AddToMap("PcIntvNumEligibleIndividualsTargeted", new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvOnchoAtRisk"));
                }
                else if (formTranslationKey == "IntvDecAlb")
                {
                    AddToMap("DateReported", new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    AddToMap("PcIntvNumEligibleIndividualsTargeted", new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvSthAtRisk", "PcIntvLfAtRisk"));
                    AddToMap("PcIntvNumSacTargeted", new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSthSacAtRisk"));
                    AddToMap("PcIntvNumSacTreated", new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvPzqAlb")
                {
                    AddToMap("DateReported", new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    AddToMap("PcIntvNumEligibleIndividualsTargeted", new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvSthAtRisk", "PcIntvSchAtRisk"));
                    AddToMap("PcIntvNumSacTargeted", new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSchSacAtRisk", "PcIntvSthSacAtRisk"));
                    AddToMap("PcIntvNumSacTreated", new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvAlb2")
                {
                    AddToMap("DateReported", new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    AddToMap("PcIntvNumEligibleIndividualsTargeted", new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvLfAtRisk"));
                    AddToMap("PcIntvNumSacTreated", new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvAlb")
                {
                    AddToMap("DateReported", new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    AddToMap("PcIntvNumEligibleIndividualsTargeted", new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvSthAtRisk"));
                    AddToMap("PcIntvNumSacTargeted", new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSthSacAtRisk"));
                    AddToMap("PcIntvNumSacTreated", new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvMbd")
                {
                    AddToMap("DateReported", new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    AddToMap("PcIntvNumEligibleIndividualsTargeted", new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvSthAtRisk"));
                    AddToMap("PcIntvNumSacTargeted", new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSthSacAtRisk"));
                    AddToMap("PcIntvNumSacTreated", new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvPzq")
                {
                    AddToMap("DateReported", new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    AddToMap("PcIntvNumEligibleIndividualsTargeted", new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvSchAtRisk"));
                    AddToMap("PcIntvNumSacTargeted", new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "PcIntvSchSacAtRisk"));
                    AddToMap("PcIntvNumSacTreated", new ValidationMapping(ValidationRuleType.EqualToSum, "IntvFemaleSac", "IntvMaleSac"));
                }
                else if (formTranslationKey == "IntvZithroTeo")
                {
                    AddToMap("DateReported", new ValidationMapping(ValidationRuleType.DateHasSameYear, "PcIntvStartDateOfMda"));
                    AddToMap("PcIntvNumEligibleIndividualsTargeted", new ValidationMapping(ValidationRuleType.LessThanSum, "PcIntvTraAtRisk"));
                    AddToMap("PcIntvNumIndividualsTreated", new ValidationMapping(ValidationRuleType.EqualToSum, "PcIntvNumTreatedZx", "PcIntvNumTreatedTeo", "PcIntvNumTreatedZxPos"));
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
