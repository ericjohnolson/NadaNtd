using Nada.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public interface ICustomValidator
    {
        List<KeyValuePair<string, string>> ValidateIndicators(Dictionary<string, Indicator> indicators, List<IndicatorValue> values, List<KeyValuePair<string, string>> metaData);
        string Valid(Indicator indicator, List<IndicatorValue> values);
    }

    public class BaseValidator : ICustomValidator
    {
        public List<KeyValuePair<string, string>> ValidateIndicators(Dictionary<string, Indicator> indicators, List<IndicatorValue> values, List<KeyValuePair<string, string>> metaData)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            // Convert the meta data to IndicatorValues
            List<IndicatorValue> metaDataValues = ConvertMetaDataToIndicatorValues(indicators, metaData);
            values.AddRange(metaDataValues);

            foreach (KeyValuePair<string, Indicator> indicator in indicators)
            {
                List<ValidationRule> rules = GetRules(indicator.Value, values);
                foreach (ValidationRule rule in rules)
                {
                    results.Add(rule.IsValid());
                }
            }

            return new List<KeyValuePair<string, string>>();
        }

        public string Valid(Indicator indicator, List<IndicatorValue> values)
        {
            throw new NotImplementedException();
        }

        public List<ValidationResult> Validate(Indicator indicator, List<IndicatorValue> values)
        {
            return new List<ValidationResult>();
        }

        private List<ValidationRule> GetRules(Indicator indicator, List<IndicatorValue> values)
        {
            List<ValidationRule> rules = new List<ValidationRule>();

            if (ValidationMap.Map.ContainsKey(indicator.DisplayName))
            {
                List<ValidationMapping> mappings = ValidationMap.Map[indicator.DisplayName];
                foreach (ValidationMapping mapping in mappings)
                {
                    rules.Add(ValidationMapping.BuildRule(mapping, indicator, values));
                }
            }

            return rules;
        }

        private List<IndicatorValue> ConvertMetaDataToIndicatorValues(Dictionary<string, Indicator> indicators, List<KeyValuePair<string, string>> metaData)
        {
            List<IndicatorValue> indicatorValues = new List<IndicatorValue>();

            foreach (KeyValuePair<string, Indicator> indicator in indicators)
            {
                string translatedIndicatorName = TranslationLookup.GetValue(indicator.Key, indicator.Key);

                KeyValuePair<string, string> correspondingMetaData = metaData.Where(k => k.Key == translatedIndicatorName).FirstOrDefault();
                if (correspondingMetaData.Equals(new KeyValuePair<string, string>()))
                    continue;

                IndicatorValue val = new IndicatorValue();
                val.DynamicValue = correspondingMetaData.Value;
                val.Indicator = indicator.Value;
                val.IndicatorId = indicator.Value.Id;
                indicatorValues.Add(val);
            }

            return indicatorValues;
        }
    }

    public class ValidationResult
    {
        public bool IsSuccess { get; set; }
        public bool HadMissingValues { get; set; }
        //public Indicator Indicator { get; set; }
        public string Message { get; set; }

        public static ValidationResult CreateMissingValuesInstance(/*Indicator indicator*/string comparisonString)
        {
            return new ValidationResult()
            {
                IsSuccess = false,
                HadMissingValues = true,
                //Indicator = indicator,
                Message = string.Format("{0}: {1}", comparisonString, Translations.NA)
            };
        }

        public static ValidationResult CreateOkInstance(/*Indicator indicator, */string comparisonString)
        {
            return new ValidationResult()
            {
                IsSuccess = true,
                HadMissingValues = false,
                //Indicator = indicator,
                Message = string.Format("{0}: {1}", comparisonString, "OK") // TODO Add translation
            };
        }

        public static ValidationResult CreateErrorInstance(/*Indicator indicator, */string comparisonString)
        {
            return new ValidationResult()
            {
                IsSuccess = true,
                HadMissingValues = false,
                //Indicator = indicator,
                Message = string.Format("{0}: {1}", comparisonString, "VALIDATION ERROR") // TODO Add translation
            };
        }
    }

    public enum ValidationRuleType
    {
        GreaterThanSum = 1
    }

    public abstract class ValidationRule
    {
        protected Indicator Indicator { get; set; }
        protected List<IndicatorValue> RelatedValues { get; set; }
        protected List<string> IndicatorNames { get; set; }

        public ValidationRule()
        {

        }

        public ValidationRule(Indicator indicator, List<IndicatorValue> values, List<string> indicatorNames)
        {
            Initialize(indicator, values, indicatorNames);
        }

        public void Initialize(Indicator indicator, List<IndicatorValue> values, List<string> indicatorNames)
        {
            Indicator = indicator;
            RelatedValues = values;
            IndicatorNames = indicatorNames;
        }

        public abstract ValidationResult IsValid();
    }

    public class GreaterThanSumRule : ValidationRule
    {
        public GreaterThanSumRule(Indicator indicator, List<IndicatorValue> values, List<string> indicatorNames)
            : base(indicator, values, indicatorNames)
        {

        }

        public override ValidationResult IsValid()
        {
            // Get the value to validate
            double? valueToValidate = GetValidationValue();
            // Get the value to validate against
            double? valueToCompareAgainst = GetValueToCompareAgainst();

            return CreateResult(valueToValidate, valueToCompareAgainst);
        }

        private double? GetValidationValue()
        {
            // Find the indicator value that is being validated
            IndicatorValue indicatorValueToValidate = RelatedValues.FirstOrDefault(v => v.Indicator.DisplayName == Indicator.DisplayName);
            if (indicatorValueToValidate == null)
                return null;
            // Make sure the value is a number
            double d;
            if (!double.TryParse(indicatorValueToValidate.DynamicValue, out d))
                return null;

            return d;
        }

        private double? GetValueToCompareAgainst()
        {
            // Will hold the sum of the values being compared against
            double summedValueToCompareAgainst = 0;
            // Find each indicator value that is being compared against
            foreach (string indicatorName in IndicatorNames)
            {
                // See if there is a matching indicator value
                IndicatorValue indicatorValueToCompareAgainst = RelatedValues.FirstOrDefault(v => v.Indicator.DisplayName == indicatorName);
                // If there is no matching indicator value, the indicator cannot be validated
                if (indicatorValueToCompareAgainst == null)
                    return null;

                // Make sure the value is a number
                double valueToCompareAgainst;
                if (!Double.TryParse(indicatorValueToCompareAgainst.DynamicValue, out valueToCompareAgainst))
                    return null;

                // The indicator value is a number, so add it to the sum
                summedValueToCompareAgainst += valueToCompareAgainst;
            }

            return summedValueToCompareAgainst;
        }

        private ValidationResult CreateResult(double? valueToValidate, double? valueToValidateAgainst)
        {
            string comparisonString = BuildComparisonString(valueToValidate, valueToValidateAgainst);
            if (!valueToValidate.HasValue || !valueToValidateAgainst.HasValue)
            {
                return ValidationResult.CreateMissingValuesInstance(comparisonString);
            }
            else
            {
                if (valueToValidate > valueToValidateAgainst)
                {
                    return ValidationResult.CreateOkInstance(comparisonString);
                }
                else
                {
                    return ValidationResult.CreateErrorInstance(comparisonString);
                }
            }
        }

        private string BuildComparisonString(double? valueToValidate, double? valueToValidateAgainst)
        {
            string validatedIndicatorName = Indicator.DisplayName;
            string valueToValidateStr = valueToValidate.HasValue ? valueToValidate.Value.ToString() : "Missing value"; // TODO Translation
            string indicatorsToValidateAgainst = string.Join(" + ", IndicatorNames.ToArray());
            string valueToValidateAgainstStr = valueToValidateAgainst.HasValue ? valueToValidateAgainst.Value.ToString() : "Missing value"; // TODO translation
            return string.Format("{0} ({1}) > {2} ({3})",
                validatedIndicatorName, valueToValidateStr, indicatorsToValidateAgainst, valueToValidateAgainstStr);
        }
    }

    public class ValidationMap
    {
        public static Dictionary<string, List<ValidationMapping>> Map = new Dictionary<string, List<ValidationMapping>>()
        {
            { 
                "PcIntvNumEligibleIndividualsTargeted",
                new List<ValidationMapping>
                {
                    new ValidationMapping(ValidationRuleType.GreaterThanSum, "PcIntvNumEligibleFemalesTargeted", "PcIntvNumEligibleMalesTargeted"),
                    new ValidationMapping(ValidationRuleType.GreaterThanSum, "PcIntvSthAtRisk", "PcIntvLfAtRisk", "PcIntvOnchoAtRisk")
                }
            }
        };
    }

    public class ValidationMapping
    {
        public ValidationRuleType ValidationType { get; set; }
        public string[] IndicatorsToCompareAgainst { get; set; }

        public ValidationMapping()
        {

        }

        public ValidationMapping(ValidationRuleType type, params string[] indicators)
        {
            ValidationType = type;
            IndicatorsToCompareAgainst = indicators;
        }

        public static ValidationRule BuildRule(ValidationMapping mapping, Indicator indicator, List<IndicatorValue> values)
        {
            switch (mapping.ValidationType)
            {
                case ValidationRuleType.GreaterThanSum:
                    return new GreaterThanSumRule(indicator, values, mapping.IndicatorsToCompareAgainst.ToList());
                default:
                    return null;
            }
        }
    }
}
