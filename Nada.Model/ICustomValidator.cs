using Nada.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public interface ICustomValidator
    {
        List<ValidationResult> ValidateIndicators(Dictionary<string, Indicator> indicators, List<IndicatorValue> values, List<KeyValuePair<string, string>> metaData);
        string Valid(Indicator indicator, List<IndicatorValue> values);
        Dictionary<string, List<ValidationMapping>> GetMapInstance(bool instantiate);
        void ClearMap();
    }

    public class BaseValidator : ICustomValidator
    {
        protected Dictionary<string, List<ValidationMapping>> ValidationMap { get; set; }

        public virtual Dictionary<string, List<ValidationMapping>> GetMapInstance(bool instantiate)
        {
            if (ValidationMap == null && instantiate)
            {
                ValidationMap = new Dictionary<string, List<ValidationMapping>>();
            }
            return ValidationMap;
        }

        public void ClearMap()
        {
            ValidationMap = null;
        }

        public List<ValidationResult> ValidateIndicators(Dictionary<string, Indicator> indicators, List<IndicatorValue> values, List<KeyValuePair<string, string>> metaData)
        {
            // Get the validation map
            GetMapInstance(true);

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

            // Clear the validation map
            ClearMap();

            return results;
        }

        public virtual string Valid(Indicator indicator, List<IndicatorValue> values)
        {
            throw new NotImplementedException();
        }

        private List<ValidationRule> GetRules(Indicator indicator, List<IndicatorValue> values)
        {
            List<ValidationRule> rules = new List<ValidationRule>();

            if (ValidationMap.ContainsKey(indicator.DisplayName))
            {
                List<ValidationMapping> mappings = ValidationMap[indicator.DisplayName];
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

            if (metaData == null)
                return indicatorValues;

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
        public ValidationRule ValidationRule { get; set; }
        public bool IsSuccess { get; set; }
        //public bool HadMissingValues { get; set; }
        //public Indicator Indicator { get; set; }
        public string Message { get; set; }

        public static ValidationResult CreateMissingValuesInstance(ValidationRule rule, string comparisonString)
        {
            return new ValidationResult()
            {
                IsSuccess = false,
                //HadMissingValues = true,
                //Indicator = indicator,
                ValidationRule = rule,
                Message = string.Format("{0}: {1}", comparisonString, Translations.NA)
            };
        }

        public static ValidationResult CreateOkInstance(ValidationRule rule, string comparisonString)
        {
            return new ValidationResult()
            {
                IsSuccess = true,
                //HadMissingValues = false,
                //Indicator = indicator,
                ValidationRule = rule,
                Message = string.Format("{0}: {1}", comparisonString, Translations.OK.ToUpper())
            };
        }

        public static ValidationResult CreateErrorInstance(ValidationRule rule, string comparisonString)
        {
            return new ValidationResult()
            {
                IsSuccess = false,
                //HadMissingValues = false,
                //Indicator = indicator,
                ValidationRule = rule,
                Message = string.Format("{0}: {1}", comparisonString, Translations.ValidationResultError.ToUpper())
            };
        }
    }

    public enum ValidationRuleType
    {
        GreaterThanSum = 1,
        GreaterThanEqualToSum = 2,
        LessThanSum = 3,
        LessThanEqualToSum = 4,
        EqualToSum = 5,
        DateEarlierThan = 6,
        DateLaterThan = 7,
        DateHasSameYear = 8
    }

    public abstract class ValidationRule
    {
        public Indicator Indicator { get; set; }
        public List<IndicatorValue> RelatedValues { get; set; }
        public List<string> IndicatorNames { get; set; }

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

        protected string[] TranslateIndicatorsNames(List<string> names)
        {
            List<string> translatedStrings = new List<string>();
            foreach (string name in names)
            {
                translatedStrings.Add(TranslationLookup.GetValue(name, name));
            }

            return translatedStrings.ToArray();
        }
    }

    public abstract class NumberValidationRule : ValidationRule
    {
        protected abstract string ComparisonStringFormat { get; }

        public NumberValidationRule()
        {

        }

        public NumberValidationRule(Indicator indicator, List<IndicatorValue> values, List<string> indicatorNames)
        {
            Initialize(indicator, values, indicatorNames);
        }

        protected abstract bool IsNumberValid(double valueToValidate, double valueToValidateAgainst);

        public override ValidationResult IsValid()
        {
            // Get the value to validate
            double? valueToValidate = GetValidationValue();
            // Get the value to validate against
            double? valueToCompareAgainst = GetValueToCompareAgainst();

            return CreateResult(valueToValidate, valueToCompareAgainst);
        }

        protected double? GetValidationValue()
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

        protected double? GetValueToCompareAgainst()
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

        protected string BuildComparisonString(double? valueToValidate, double? valueToValidateAgainst)
        {
            // Translate the indicator name
            string validatedIndicatorName = TranslationLookup.GetValue(Indicator.DisplayName, Indicator.DisplayName);
            // If the value is an integer, round it to an int
            string valueToValidateStr;
            if (Indicator.DataTypeId == (int)IndicatorDataType.Integer)
                valueToValidateStr = valueToValidate.HasValue ? Math.Round(valueToValidate.Value).ToString() : Translations.ValidationResultMissingValue;
            else
                valueToValidateStr = valueToValidate.HasValue ? valueToValidate.Value.ToString() : Translations.ValidationResultMissingValue;

            // Translate the related indicator names
            string indicatorsToValidateAgainst = string.Join(" + ", TranslateIndicatorsNames(IndicatorNames));
            // If the value is an integer, round it to an int
            string valueToValidateAgainstStr;
            if (Indicator.DataTypeId == (int)IndicatorDataType.Integer)
                valueToValidateAgainstStr = valueToValidateAgainst.HasValue ? Math.Round(valueToValidateAgainst.Value).ToString() : Translations.ValidationResultMissingValue;
            else
                valueToValidateAgainstStr = valueToValidateAgainst.HasValue ? valueToValidateAgainst.Value.ToString() : Translations.ValidationResultMissingValue;

            return string.Format(ComparisonStringFormat,
                validatedIndicatorName, valueToValidateStr, indicatorsToValidateAgainst, valueToValidateAgainstStr);
        }

        protected ValidationResult CreateResult(double? valueToValidate, double? valueToValidateAgainst)
        {
            string comparisonString = BuildComparisonString(valueToValidate, valueToValidateAgainst);
            if (!valueToValidate.HasValue || !valueToValidateAgainst.HasValue)
            {
                return ValidationResult.CreateMissingValuesInstance(this, comparisonString);
            }
            else
            {
                if (IsNumberValid(valueToValidate.Value, valueToValidateAgainst.Value))
                {
                    return ValidationResult.CreateOkInstance(this, comparisonString);
                }
                else
                {
                    return ValidationResult.CreateErrorInstance(this, comparisonString);
                }
            }
        }
    }

    public class GreaterThanSumRule : NumberValidationRule
    {
        protected override string ComparisonStringFormat
        {
            get { return "{0} ({1}) > {2} ({3})"; }
        }

        public GreaterThanSumRule(Indicator indicator, List<IndicatorValue> values, List<string> indicatorNames)
            : base(indicator, values, indicatorNames)
        {

        }

        protected override bool IsNumberValid(double valueToValidate, double valueToValidateAgainst)
        {
            return valueToValidate > valueToValidateAgainst;
        }
    }

    public class GreaterThanEqualToSumRule : NumberValidationRule
    {
        protected override string ComparisonStringFormat
        {
            get { return "{0} ({1}) >= {2} ({3})"; }
        }

        public GreaterThanEqualToSumRule(Indicator indicator, List<IndicatorValue> values, List<string> indicatorNames)
            : base(indicator, values, indicatorNames)
        {

        }

        protected override bool IsNumberValid(double valueToValidate, double valueToValidateAgainst)
        {
            return valueToValidate >= valueToValidateAgainst;
        }
    }

    public class LessThanSumRule : NumberValidationRule
    {
        protected override string ComparisonStringFormat
        {
            get { return "{0} ({1}) < {2} ({3})"; }
        }

        public LessThanSumRule(Indicator indicator, List<IndicatorValue> values, List<string> indicatorNames)
            : base(indicator, values, indicatorNames)
        {

        }

        protected override bool IsNumberValid(double valueToValidate, double valueToValidateAgainst)
        {
            return valueToValidate < valueToValidateAgainst;
        }
    }

    public class LessThanEqualToSumRule : NumberValidationRule
    {
        protected override string ComparisonStringFormat
        {
            get { return "{0} ({1}) <= {2} ({3})"; }
        }

        public LessThanEqualToSumRule(Indicator indicator, List<IndicatorValue> values, List<string> indicatorNames)
            : base(indicator, values, indicatorNames)
        {

        }

        protected override bool IsNumberValid(double valueToValidate, double valueToValidateAgainst)
        {
            return valueToValidate <= valueToValidateAgainst;
        }
    }

    public class EqualToSumRule : NumberValidationRule
    {
        protected override string ComparisonStringFormat
        {
            get { return "{0} ({1}) = {2} ({3})"; }
        }

        public EqualToSumRule(Indicator indicator, List<IndicatorValue> values, List<string> indicatorNames)
            : base(indicator, values, indicatorNames)
        {

        }

        protected override bool IsNumberValid(double valueToValidate, double valueToValidateAgainst)
        {
            return valueToValidate == valueToValidateAgainst;
        }
    }

    public abstract class DateTimeRule : ValidationRule
    {
        protected abstract string ComparisonStringFormat { get; }

        public DateTimeRule()
        {

        }

        public DateTimeRule(Indicator indicator, List<IndicatorValue> values, List<string> indicatorNames)
        {
            Initialize(indicator, values, indicatorNames);
        }

        protected abstract bool IsDateValid(DateTime valueToValidate, DateTime valueToValidateAgainst);

        public override ValidationResult IsValid()
        {
            // Get the value to validate
            DateTime? valueToValidate = GetValidationValue();
            // Get the value to validate against
            DateTime? valueToCompareAgainst = GetValueToCompareAgainst();

            return CreateResult(valueToValidate, valueToCompareAgainst);
        }

        protected DateTime? GetValidationValue()
        {
            // Find the indicator value that is being validated
            IndicatorValue indicatorValueToValidate = RelatedValues.FirstOrDefault(v => v.Indicator.DisplayName == Indicator.DisplayName);
            if (indicatorValueToValidate == null)
                return null;
            // Make sure the value is a DateTime
            DateTime dateTime;
            if (!DateTime.TryParse(indicatorValueToValidate.DynamicValue, out dateTime))
                return null;

            return dateTime;
        }

        protected DateTime? GetValueToCompareAgainst()
        {
            // Only one indicator is expected to be compared against, so make sure the indicator name exists
            string indicatorNameToCompareAgainst = IndicatorNames.ElementAtOrDefault(0);
            if (indicatorNameToCompareAgainst == null)
                return null;

            // See if there is a matching indicator value
            IndicatorValue indicatorValueToCompareAgainst = RelatedValues.FirstOrDefault(v => v.Indicator.DisplayName == indicatorNameToCompareAgainst);
            // If there is no matching indicator value, the indicator cannot be validated
            if (indicatorValueToCompareAgainst == null)
                return null;

            // Make sure the value is a number
            DateTime valueToCompareAgainst;
            if (!DateTime.TryParse(indicatorValueToCompareAgainst.DynamicValue, out valueToCompareAgainst))
                return null;

            return valueToCompareAgainst;
        }

        protected string BuildComparisonString(DateTime? valueToValidate, DateTime? valueToValidateAgainst)
        {
            // Translate the indicator name
            string validatedIndicatorName = TranslationLookup.GetValue(Indicator.DisplayName, Indicator.DisplayName);
            // If the value is an integer, round it to an int
            string valueToValidateStr = valueToValidate.HasValue ? valueToValidate.Value.ToString() : Translations.ValidationResultMissingValue;

            // Translate the related indicator names
            string indicatorsToValidateAgainst = string.Join(" + ", TranslateIndicatorsNames(IndicatorNames));
            // If the value is an integer, round it to an int
            string valueToValidateAgainstStr = valueToValidateAgainst.HasValue ? valueToValidateAgainst.Value.ToString() : Translations.ValidationResultMissingValue;

            return string.Format(ComparisonStringFormat,
                validatedIndicatorName, valueToValidateStr, indicatorsToValidateAgainst, valueToValidateAgainstStr);
        }

        protected ValidationResult CreateResult(DateTime? valueToValidate, DateTime? valueToValidateAgainst)
        {
            string comparisonString = BuildComparisonString(valueToValidate, valueToValidateAgainst);
            if (!valueToValidate.HasValue || !valueToValidateAgainst.HasValue)
            {
                return ValidationResult.CreateMissingValuesInstance(this, comparisonString);
            }
            else
            {
                if (IsDateValid(valueToValidate.Value, valueToValidateAgainst.Value))
                {
                    return ValidationResult.CreateOkInstance(this, comparisonString);
                }
                else
                {
                    return ValidationResult.CreateErrorInstance(this, comparisonString);
                }
            }
        }
    }

    public class DateEarlierThanRule : DateTimeRule
    {
        protected override string ComparisonStringFormat
        {
            get { return "{0} ({1}) earlier than {2} ({3})"; }
        }

        public DateEarlierThanRule(Indicator indicator, List<IndicatorValue> values, List<string> indicatorNames)
            : base(indicator, values, indicatorNames)
        {

        }

        protected override bool IsDateValid(DateTime valueToValidate, DateTime valueToValidateAgainst)
        {
            return valueToValidate < valueToValidateAgainst;
        }
    }

    public class DateLaterThanRule : DateTimeRule
    {
        protected override string ComparisonStringFormat
        {
            get { return "{0} ({1}) later than {2} ({3})"; }
        }

        public DateLaterThanRule(Indicator indicator, List<IndicatorValue> values, List<string> indicatorNames)
            : base(indicator, values, indicatorNames)
        {

        }

        protected override bool IsDateValid(DateTime valueToValidate, DateTime valueToValidateAgainst)
        {
            return valueToValidate > valueToValidateAgainst;
        }
    }

    public class DateHasSameYearRule : DateTimeRule
    {
        protected override string ComparisonStringFormat
        {
            get { return "{0} ({1}) has same year {2} ({3})"; }
        }

        public DateHasSameYearRule(Indicator indicator, List<IndicatorValue> values, List<string> indicatorNames)
            : base(indicator, values, indicatorNames)
        {

        }

        protected override bool IsDateValid(DateTime valueToValidate, DateTime valueToValidateAgainst)
        {
            return valueToValidate.Year == valueToValidateAgainst.Year;
        }
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
                case ValidationRuleType.GreaterThanEqualToSum:
                    return new GreaterThanEqualToSumRule(indicator, values, mapping.IndicatorsToCompareAgainst.ToList());
                case ValidationRuleType.LessThanSum:
                    return new LessThanSumRule(indicator, values, mapping.IndicatorsToCompareAgainst.ToList());
                case ValidationRuleType.LessThanEqualToSum:
                    return new LessThanEqualToSumRule(indicator, values, mapping.IndicatorsToCompareAgainst.ToList());
                case ValidationRuleType.EqualToSum:
                    return new EqualToSumRule(indicator, values, mapping.IndicatorsToCompareAgainst.ToList());
                case ValidationRuleType.DateEarlierThan:
                    return new DateEarlierThanRule(indicator, values, mapping.IndicatorsToCompareAgainst.ToList());
                case ValidationRuleType.DateLaterThan:
                    return new DateLaterThanRule(indicator, values, mapping.IndicatorsToCompareAgainst.ToList());
                case ValidationRuleType.DateHasSameYear:
                    return new DateHasSameYearRule(indicator, values, mapping.IndicatorsToCompareAgainst.ToList());
                default:
                    return null;
            }
        }
    }
}
