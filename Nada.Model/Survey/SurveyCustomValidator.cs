using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model.Survey
{
    public class SurveyCustomValidator : BaseValidator
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
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "LFMapSurStartDateOfSurvey"),
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "LFSurStartDateOfSurvey"),
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "TASStartDateOfSurvey"),
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "OnchoMapSurStartDateOfSurvey"),
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "OnchoSurStartDateOfSurvey"),
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "SCHMapSurStartDateOfSurvey"),
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "SCHSurStartDateOfSurvey"),
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "STHMapSurSurStartDateOfSurvey"),
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "STHSurStartDateOfSurvey"),
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "TraMapSurStartDateOfSurvey"),
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "TraSurStartDateOfSurvey")
                        }
                    },
                    {
                        "LFMapSurTargetSampleSize",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurPopulationLivingInMappingSites")
                        }
                    },
                    {
                        "LFMapSurNumberOfIndividualsExamined",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurPopulationLivingInMappingSites")
                        }
                    },
                    {
                        "LFMapSurNumberOfFemalesExamined",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurNumberOfIndividualsExamined")
                        }
                    },
                    {
                        "LFMapSurNumberOfMalesExamined",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurNumberOfIndividualsExamined")
                        }
                    },
                    {
                        "LFMapSurExaminedLympho",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurNumberOfIndividualsExamined")
                        }
                    },
                    {
                        "LFMapSurExaminedHydro1",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurNumberOfIndividualsExamined")
                        }
                    },
                    {
                        "LFMapSurNumberOfIndividualsPositive",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurNumberOfIndividualsExamined")
                        }
                    },
                    {
                        "LFMapSurNumberOfFemalesPositive",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurNumberOfIndividualsPositive")
                        }
                    },
                    {
                        "LFMapSurNumberOfMalesPositive",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurNumberOfIndividualsPositive")
                        }
                    },
                    {
                        "LFMapSurNumberOfCasesOfLymphoedema",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurExaminedLympho")
                        }
                    },
                    {
                        "LFMapSurNumberOfCasesOfHydrocele",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurExaminedHydro1")
                        }
                    },
                    {
                        "LFSurNumberOfIndividualsPositive",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFSurNumberOfIndividualsExamined")
                        }
                    },
                    {
                        "LFSurPosLympho",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFSurExaminedLympho")
                        }
                    },
                    {
                        "LFSurPosHydro",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFSurPosHydro")
                        }
                    },
                    {
                        "TASActualSampleSizePositive",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "d807913f-b3a1-4948-a2b3-54eb0800a3bc")
                        }
                    },
                    {
                        "TASActualSampleSizeNegative",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "d807913f-b3a1-4948-a2b3-54eb0800a3bc")
                        }
                    },
                    {
                        "TASActualNonResponseRateAbsent",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "d807913f-b3a1-4948-a2b3-54eb0800a3bc")
                        }
                    },
                    {
                        "TASActualNonResponseRateRefusalOrNoConse",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "d807913f-b3a1-4948-a2b3-54eb0800a3bc")
                        }
                    },
                    {
                        "TASActualNonResponseRateInabilityToPerfo",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "d807913f-b3a1-4948-a2b3-54eb0800a3bc")
                        }
                    },
                    {
                        "TASActualNonResponseRateTotal",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.EqualToSum, "TASActualSampleSizePositive", "TASActualSampleSizeNegative", "TASActualNonResponseRateAbsent", "TASActualNonResponseRateRefusalOrNoConse", "TASActualNonResponseRateInabilityToPerfo")
                        }
                    },
                    {
                        "OnchoMapSurNumberOfIndividualsPositive",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "OnchoMapSurNumberOfIndividualsExamined")
                        }
                    },
                    {
                        "OnchoSurNumberOfIndividualsExamined",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "OnchoSurRegistrationPopulation")
                        }
                    },
                    {
                        "OnchoSurNumRefused",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "OnchoSurRegistrationPopulation")
                        }
                    },
                    {
                        "OnchoSurNumberOfIndividualsPositive",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "OnchoSurNumberOfIndividualsExamined")
                        }
                    },
                    {
                        "OnchoSurFliesDissected",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "OnchoSurNoFlies")
                        }
                    },
                    {
                        "OnchoSurParousFliesDisected",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "OnchoSurParousFlies")
                        }
                    },
                    {
                        "SCHMapSurNumberOfIndividualsPositiveForH",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHMapSurNumberOfIndividualsExaminedForU")
                        }
                    },
                    {
                        "SCHMapSurNumPosSchParasite",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHMapSurNumberOfIndividualsExaminedForU")
                        }
                    },
                    {
                        "SCHMapSurProportionOfHeavyIntensityUrina",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHMapSurNumberOfIndividualsExaminedForU")
                        }
                    },
                    {
                        "SCHMapSurProportionOfModerateIntensityUr",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHMapSurNumberOfIndividualsExaminedForU")
                        }
                    },
                    {
                        "SCHMapSurNumberOfIndividualsPositiveForI",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHMapSurNumberOfIndividualsExaminedForI")
                        }
                    },
                    {
                        "SCHMapSurProportionOfHeavyIntensityIntes",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHMapSurNumberOfIndividualsExaminedForI")
                        }
                    },
                    {
                        "SCHMapSurProportionOfModerateIntensityIn",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHMapSurNumberOfIndividualsExaminedForI")
                        }
                    },
                    {
                        "SCHSurNumberOfIndividualsPositiveForHaem",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHSurNumberOfIndividualsExaminedForUrin")
                        }
                    },
                    {
                        "SCHSurNumPosSchParasite",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHSurNumberOfIndividualsExaminedForUrin")
                        }
                    },
                    {
                        "SCHSurProportionOfHeavyIntensityUrinaryS",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHSurNumberOfIndividualsExaminedForUrin")
                        }
                    },
                    {
                        "SCHSurProportionOfModerateIntensityUrina",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHSurNumberOfIndividualsExaminedForUrin")
                        }
                    },
                    {
                        "SCHSurNumberOfIndividualsPositiveForInte",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHSurNumberOfIndividualsExaminedForInte")
                        }
                    },
                    {
                        "SCHSurProportionOfHeavyIntensityIntestin",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHSurNumberOfIndividualsExaminedForInte")
                        }
                    },
                    {
                        "SCHSurProportionOfModerateIntensityIntes",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHSurNumberOfIndividualsExaminedForInte")
                        }
                    },
                    {
                        "STHMapSurSurNumberOfIndividualsPositiveAscaris",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedAscaris")
                        }
                    },
                    {
                        "STHMapSurSurProportionOfHeavyIntensityOfInAS",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedAscaris")
                        }
                    },
                    {
                        "STHMapSurSurProportionOfModerateIntensityOfInAS",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedAscaris")
                        }
                    },
                    {
                        "STHMapSurSurNumberOfIndividualsPositiveHookwor",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedHookwor")
                        }
                    },
                    {
                        "STHMapSurSurProportionOfHeavyIntensityOfInHook",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedHookwor")
                        }
                    },
                    {
                        "STHMapSurSurProportionOfModerateIntensityOfInHook",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedHookwor")
                        }
                    },
                    {
                        "STHMapSurSurNumberOfIndividualsPositiveTrichur",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedTrichur")
                        }
                    },
                    {
                        "STHMapSurSurProportionOfHeavyIntensityOfInfecti",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedTrichur")
                        }
                    },
                    {
                        "STHMapSurSurProportionOfModerateIntensityOfInfe",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedTrichur")
                        }
                    },
                    {
                        "STHMapSurSurNumberOfIndividualsPositiveOverall",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedOverall")
                        }
                    },
                    {
                        "STHSurNumberOfIndividualsPositiveAscaris",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedAscaris")
                        }
                    },
                    {
                        "STHSurProportionOfHeavyIntensityOfAsc",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedAscaris")
                        }
                    },
                    {
                        "STHSurProportionOfModerateIntensityAsc",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedAscaris")
                        }
                    },
                    {
                        "STHSurNumberOfIndividualsPositiveHookwor",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedHookwor")
                        }
                    },
                    {
                        "STHSurProportionOfHeavyIntensityHook",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedHookwor")
                        }
                    },
                    {
                        "STHSurProportionOfModerateIntensityHook",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedHookwor")
                        }
                    },
                    {
                        "STHSurNumberOfIndividualsPositiveTrichur",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedTrichur")
                        }
                    },
                    {
                        "STHSurProportionOfHeavyIntensityOfTri",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedTrichur")
                        }
                    },
                    {
                        "STHSurProportionOfModerateIntensityTri",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedTrichur")
                        }
                    },
                    {
                        "STHSurNumberOfIndividualsPositiveOverall",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedOverall")
                        }
                    }
                };
            }
            return ValidationMap;
        }
    }
}
