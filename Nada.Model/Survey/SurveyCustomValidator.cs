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

        public override Dictionary<string, List<ValidationMapping>> GetMapInstance(string formTranslationKey, bool instantiate)
        {
            if (ValidationMap == null && instantiate)
            {
                ValidationMap = new Dictionary<string, List<ValidationMapping>>();
                if (formTranslationKey == "SurLfMapping")
                {
                    ValidationMap.Add("DateReported",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "LFMapSurStartDateOfSurvey")
                        });
                    ValidationMap.Add("LFMapSurTargetSampleSize",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurPopulationLivingInMappingSites")
                        });
                    ValidationMap.Add("LFMapSurNumberOfIndividualsExamined",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurPopulationLivingInMappingSites")
                        });
                    ValidationMap.Add("LFMapSurNumberOfFemalesExamined",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurNumberOfIndividualsExamined")
                        });
                    ValidationMap.Add("LFMapSurNumberOfMalesExamined",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurNumberOfIndividualsExamined")
                        });
                    ValidationMap.Add("LFMapSurExaminedLympho",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurNumberOfIndividualsExamined")
                        });
                    ValidationMap.Add("LFMapSurExaminedHydro1",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurNumberOfIndividualsExamined")
                        });
                    ValidationMap.Add("LFMapSurNumberOfIndividualsPositive",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurNumberOfIndividualsExamined")
                        });
                    ValidationMap.Add("LFMapSurNumberOfFemalesPositive",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurNumberOfIndividualsPositive")
                        });
                    ValidationMap.Add("LFMapSurNumberOfMalesPositive",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurNumberOfIndividualsPositive")
                        });
                    ValidationMap.Add("LFMapSurNumberOfCasesOfLymphoedema",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurExaminedLympho")
                        });
                    ValidationMap.Add("LFMapSurNumberOfCasesOfHydrocele",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFMapSurExaminedHydro1")
                        });
                }
                else if (formTranslationKey == "SurLfSentinelSpotCheckSurvey")
                {
                    ValidationMap.Add("DateReported",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "LFSurStartDateOfSurvey")
                        });
                    ValidationMap.Add("LFSurNumberOfIndividualsPositive",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFSurNumberOfIndividualsExamined")
                        });
                    ValidationMap.Add("LFSurPosLympho",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFSurExaminedLympho")
                        });
                    ValidationMap.Add("LFSurPosHydro",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "LFSurPosHydro")
                        });
                }
                else if (formTranslationKey == "SurTransAssessSurvey")
                {
                    ValidationMap.Add("DateReported",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "TASStartDateOfSurvey")
                        });
                    ValidationMap.Add("TASActualSampleSizePositive",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "d807913f-b3a1-4948-a2b3-54eb0800a3bc")
                        });
                    ValidationMap.Add("TASActualSampleSizeNegative",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "d807913f-b3a1-4948-a2b3-54eb0800a3bc")
                        });
                    ValidationMap.Add("TASActualNonResponseRateAbsent",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "d807913f-b3a1-4948-a2b3-54eb0800a3bc")
                        });
                    ValidationMap.Add("TASActualNonResponseRateRefusalOrNoConse",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "d807913f-b3a1-4948-a2b3-54eb0800a3bc")
                        });
                    ValidationMap.Add("TASActualNonResponseRateInabilityToPerfo",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "d807913f-b3a1-4948-a2b3-54eb0800a3bc")
                        });
                    ValidationMap.Add("TASActualNonResponseRateTotal",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.EqualToSum, "TASActualSampleSizePositive", "TASActualSampleSizeNegative", "TASActualNonResponseRateAbsent", "TASActualNonResponseRateRefusalOrNoConse", "TASActualNonResponseRateInabilityToPerfo")
                        });
                }
                else if (formTranslationKey == "SurOnchoMapping")
                {
                    ValidationMap.Add("DateReported",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "OnchoMapSurStartDateOfSurvey")
                        });
                    ValidationMap.Add("OnchoMapSurNumberOfIndividualsPositive",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "OnchoMapSurNumberOfIndividualsExamined")
                        });
                }
                else if (formTranslationKey == "SurOnchoAssesments")
                {
                    ValidationMap.Add("DateReported",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "OnchoSurStartDateOfSurvey")
                        });
                    ValidationMap.Add("OnchoSurNumberOfIndividualsExamined",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "OnchoSurRegistrationPopulation")
                        });
                    ValidationMap.Add("OnchoSurNumRefused",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "OnchoSurRegistrationPopulation")
                        });
                    ValidationMap.Add("OnchoSurNumberOfIndividualsPositive",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "OnchoSurNumberOfIndividualsExamined")
                        });
                }
                else if (formTranslationKey == "OnchoSurEntomological")
                {
                    ValidationMap.Add("OnchoSurFliesDissected",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "OnchoSurNoFlies")
                        });
                    ValidationMap.Add("OnchoSurParousFliesDisected",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "OnchoSurParousFlies")
                        });
                }
                else if (formTranslationKey == "SurSchMapping")
                {
                    ValidationMap.Add("DateReported",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "SCHMapSurStartDateOfSurvey")
                        });
                    ValidationMap.Add("SCHMapSurNumberOfIndividualsPositiveForH",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHMapSurNumberOfIndividualsExaminedForU")
                        });
                    ValidationMap.Add("SCHMapSurNumPosSchParasite",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHMapSurNumberOfIndividualsExaminedForU")
                        });
                    ValidationMap.Add("SCHMapSurProportionOfHeavyIntensityUrina",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHMapSurNumberOfIndividualsExaminedForU")
                        });
                    ValidationMap.Add("SCHMapSurProportionOfModerateIntensityUr",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHMapSurNumberOfIndividualsExaminedForU")
                        });
                    ValidationMap.Add("SCHMapSurNumberOfIndividualsPositiveForI",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHMapSurNumberOfIndividualsExaminedForI")
                        });
                    ValidationMap.Add("SCHMapSurProportionOfHeavyIntensityIntes",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHMapSurNumberOfIndividualsExaminedForI")
                        });
                    ValidationMap.Add("SCHMapSurProportionOfModerateIntensityIn",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHMapSurNumberOfIndividualsExaminedForI")
                        });
                }
                else if (formTranslationKey == "SurSchSentinelSpotCheckSurvey")
                {
                    ValidationMap.Add("DateReported",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "SCHSurStartDateOfSurvey")
                        });
                    ValidationMap.Add("SCHSurNumberOfIndividualsPositiveForHaem",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHSurNumberOfIndividualsExaminedForUrin")
                        });
                    ValidationMap.Add("SCHSurNumPosSchParasite",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHSurNumberOfIndividualsExaminedForUrin")
                        });
                    ValidationMap.Add("SCHSurProportionOfHeavyIntensityUrinaryS",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHSurNumberOfIndividualsExaminedForUrin")
                        });
                    ValidationMap.Add("SCHSurProportionOfModerateIntensityUrina",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHSurNumberOfIndividualsExaminedForUrin")
                        });
                    ValidationMap.Add("SCHSurNumberOfIndividualsPositiveForInte",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHSurNumberOfIndividualsExaminedForInte")
                        });
                    ValidationMap.Add("SCHSurProportionOfHeavyIntensityIntestin",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHSurNumberOfIndividualsExaminedForInte")
                        });
                    ValidationMap.Add("SCHSurProportionOfModerateIntensityIntes",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "SCHSurNumberOfIndividualsExaminedForInte")
                        });
                }
                else if (formTranslationKey == "SurSthMapping")
                {
                    ValidationMap.Add("DateReported",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "STHMapSurSurStartDateOfSurvey")
                        });
                    ValidationMap.Add("STHMapSurSurNumberOfIndividualsPositiveAscaris",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedAscaris")
                        });
                    ValidationMap.Add("STHMapSurSurProportionOfHeavyIntensityOfInAS",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedAscaris")
                        });
                    ValidationMap.Add("STHMapSurSurProportionOfModerateIntensityOfInAS",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedAscaris")
                        });
                    ValidationMap.Add("STHMapSurSurNumberOfIndividualsPositiveHookwor",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedHookwor")
                        });
                    ValidationMap.Add("STHMapSurSurProportionOfHeavyIntensityOfInHook",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedHookwor")
                        });
                    ValidationMap.Add("STHMapSurSurProportionOfModerateIntensityOfInHook",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedHookwor")
                        });
                    ValidationMap.Add("STHMapSurSurNumberOfIndividualsPositiveTrichur",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedTrichur")
                        });
                    ValidationMap.Add("STHMapSurSurProportionOfHeavyIntensityOfInfecti",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedTrichur")
                        });
                    ValidationMap.Add("STHMapSurSurProportionOfModerateIntensityOfInfe",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedTrichur")
                        });
                    ValidationMap.Add("STHMapSurSurNumberOfIndividualsPositiveOverall",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHMapSurSurNumberOfIndividualsExaminedOverall")
                        });
                }
                else if (formTranslationKey == "SurSthSentinelSpotCheckSurvey")
                {
                    ValidationMap.Add("DateReported",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "STHSurStartDateOfSurvey")
                        });
                    ValidationMap.Add("STHSurNumberOfIndividualsPositiveAscaris",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedAscaris")
                        });
                    ValidationMap.Add("STHSurProportionOfHeavyIntensityOfAsc",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedAscaris")
                        });
                    ValidationMap.Add("STHSurProportionOfModerateIntensityAsc",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedAscaris")
                        });
                    ValidationMap.Add("STHSurNumberOfIndividualsPositiveHookwor",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedHookwor")
                        });
                    ValidationMap.Add("STHSurProportionOfHeavyIntensityHook",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedHookwor")
                        });
                    ValidationMap.Add("STHSurProportionOfModerateIntensityHook",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedHookwor")
                        });
                    ValidationMap.Add("STHSurNumberOfIndividualsPositiveTrichur",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedTrichur")
                        });
                    ValidationMap.Add("STHSurProportionOfHeavyIntensityOfTri",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedTrichur")
                        });
                    ValidationMap.Add("STHSurProportionOfModerateIntensityTri",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedTrichur")
                        });
                    ValidationMap.Add("STHSurNumberOfIndividualsPositiveOverall",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.LessThanEqualToSum, "STHSurNumberOfIndividualsExaminedOverall")
                        });
                }
                else if (formTranslationKey == "SurTrachomaMapping")
                {
                    ValidationMap.Add("DateReported",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "TraMapSurStartDateOfSurvey")
                        });
                }
                else if (formTranslationKey == "SurImpactSurvey")
                {
                    ValidationMap.Add("DateReported",
                        new List<ValidationMapping>
                        {
                            new ValidationMapping(ValidationRuleType.DateHasSameYear, "TraSurStartDateOfSurvey")
                        });
                }

            }
            return ValidationMap;
        }
    }
}
