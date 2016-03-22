using Nada.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    /// <summary>
    /// Interface that outlines the behavior that an entity validator
    /// </summary>
    public interface ICustomValidator
    {
        /// <summary>
        /// Should validate the specified collection of indicators
        /// </summary>
        /// <param name="formTranslationKey">The translation of the form to uniquely identify the form and its indicators</param>
        /// <param name="indicators">The indicators to validate</param>
        /// <param name="values">Related indicator values to validate against</param>
        /// <param name="metaData">Meta data indicators that will be added to the collection of related indicator values so they can be validated against</param>
        /// <returns>Collection of validation results</returns>
        List<ValidationResult> ValidateIndicators(string formTranslationKey, Dictionary<string, Indicator> indicators, List<IndicatorValue> values, List<KeyValuePair<string, string>> metaData);

        /// <summary>
        /// Determines if there are any validation criteria that needs to be met
        /// </summary>
        /// <param name="formTranslationKey">The translation of the form to uniquely identify the form and its indicators</param>
        /// <param name="indicators">The indicators to validate</param>
        /// <param name="values">Related indicator values to validate against</param>
        /// <returns>True if there are indicators to validate</returns>
        bool HasIndicatorsToValidate(string formTranslationKey, Dictionary<string, Indicator> indicators, List<IndicatorValue> values);

        /// <summary>
        /// NOTE: Not used
        /// </summary>
        /// <param name="indicator">Indicator to validate</param>
        /// <param name="values">Related indicator values to validate against</param>
        /// <returns></returns>
        string Valid(Indicator indicator, List<IndicatorValue> values);

        /// <summary>
        /// Gets or creates the ValidationMap instance if it does not exist
        /// </summary>
        /// <param name="formTranslationKey">The translation of the form to uniquely identify the form and its indicators</param>
        /// <param name="instantiate">Whether or not to instantiate the collection</param>
        /// <returns>The ValidationMap</returns>
        Dictionary<string, List<ValidationMapping>> GetMapInstance(string formTranslationKey, bool instantiate);

        /// <summary>
        /// Clears the instance's ValidationMap
        /// </summary>
        void ClearMap();
    }

    /// <summary>
    /// Base implementation of an entity validator that all other validators will extend
    /// </summary>
    public class BaseValidator : ICustomValidator
    {
        /// <summary>
        /// The collection that holds all of the validation rules.  The key is the translation of the key indicator being validated
        /// and the value is a collection of validation rule mappings
        /// </summary>
        protected Dictionary<string, List<ValidationMapping>> ValidationMap { get; set; }

        /// <summary>
        /// Establishes the validation rules for any indicators that need validation
        /// </summary>
        /// <param name="formTranslationKey">The translation key of the form that the indicators belong to</param>
        /// <param name="instantiate">Whether or not to instantiate the collection</param>
        /// <returns>Collection of validation rules</returns>
        public virtual Dictionary<string, List<ValidationMapping>> GetMapInstance(string formTranslationKey, bool instantiate)
        {
            if (ValidationMap == null && instantiate)
            {
                ValidationMap = new Dictionary<string, List<ValidationMapping>>();
            }
            return ValidationMap;
        }

        /// <summary>
        /// Clears the validation map
        /// </summary>
        public void ClearMap()
        {
            ValidationMap = null;
        }

        /// <summary>
        /// Adds a ValidationMapping to the ValidationMap that establishes a validation criteria that needs to be met for the
        /// specified indicator
        /// </summary>
        /// <param name="indicatorName">The indicator to add a validation rule for</param>
        /// <param name="mapping">ValidationMapping that builds the ValidationRule</param>
        public void AddToMap(string indicatorName, ValidationMapping mapping)
        {
            if (ValidationMap != null)
            {
                if (!ValidationMap.ContainsKey(indicatorName))
                {
                    ValidationMap.Add(indicatorName, new List<ValidationMapping>());
                }
                // Add the new mapping
                ValidationMap[indicatorName].Add(mapping);
            }
        }

        /// <summary>
        /// Validates the specified collection of indicators
        /// </summary>
        /// <param name="formTranslationKey">The translation of the form to uniquely identify the form and its indicators</param>
        /// <param name="indicators">The indicators to validate</param>
        /// <param name="values">Related indicator values to validate against</param>
        /// <param name="metaData">Meta data indicators that will be added to the collection of related indicator values so they can be validated against</param>
        /// <returns></returns>
        public List<ValidationResult> ValidateIndicators(string formTranslationKey, Dictionary<string, Indicator> indicators, List<IndicatorValue> values, List<KeyValuePair<string, string>> metaData)
        {
            // Get the validation map
            GetMapInstance(formTranslationKey, true);

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

        /// <summary>
        /// Determines if there are any validation criteria that needs to be met
        /// </summary>
        /// <param name="formTranslationKey">The translation of the form to uniquely identify the form and its indicators</param>
        /// <param name="indicators">The indicators to validate</param>
        /// <param name="values">Related indicator values to validate against</param>
        /// <returns>True if there are indicators to validate</returns>
        public virtual bool HasIndicatorsToValidate(string formTranslationKey, Dictionary<string, Indicator> indicators, List<IndicatorValue> values)
        {
            // Get the validation map
            GetMapInstance(formTranslationKey, true);

            List<ValidationRule> rules = new List<ValidationRule>();

            foreach (KeyValuePair<string, Indicator> indicator in indicators)
            {
                rules.AddRange(GetRules(indicator.Value, values));
            }

            // Clear the validation map
            ClearMap();

            return rules.Count > 0;
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="indicators">The indicator to validate</param>
        /// <param name="values">Related indicator values to validate against</param>
        /// <returns></returns>
        public virtual string Valid(Indicator indicator, List<IndicatorValue> values)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the collection of validation rules for the specified indicator
        /// </summary>
        /// <param name="indicator">The indicator to get the validation rules for</param>
        /// <param name="values">The values to validate against</param>
        /// <returns>The collection of validation rules</returns>
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

        /// <summary>
        /// Converts a collection of meta data to values that can be validated against
        /// </summary>
        /// <param name="indicators">Collection of indicators that contain values for the meta data</param>
        /// <param name="metaData">The meta data collection</param>
        /// <returns></returns>
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

    /// <summary>
    /// Result of a validation that indicates if the validation was successful
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// The validation rule
        /// </summary>
        public ValidationRule ValidationRule { get; set; }

        /// <summary>
        /// Whether or not the validation criteria was met
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Whether or not there were missing indicator values
        /// </summary>
        public bool HadMissingValues { get; set; }

        /// <summary>
        /// The validation error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Creates an instance indcating that all the values were not present to perform the validation
        /// </summary>
        /// <param name="rule">The validation rule</param>
        /// <param name="comparisonString">String representing what validation took place</param>
        /// <returns>The new ValidationResult</returns>
        public static ValidationResult CreateMissingValuesInstance(ValidationRule rule, string comparisonString)
        {
            return new ValidationResult()
            {
                IsSuccess = false,
                HadMissingValues = true,
                ValidationRule = rule,
                Message = string.Format("{0}: {1}", comparisonString, Translations.NA)
            };
        }

        /// <summary>
        /// Creates an instance indcating that all the result was successful
        /// </summary>
        /// <param name="rule">The validation rule</param>
        /// <param name="comparisonString">String representing what validation took place</param>
        /// <returns>The new ValidationResult</returns>
        public static ValidationResult CreateOkInstance(ValidationRule rule, string comparisonString)
        {
            return new ValidationResult()
            {
                IsSuccess = true,
                HadMissingValues = false,
                ValidationRule = rule,
                Message = string.Format("{0}: {1}", comparisonString, Translations.OK.ToUpper())
            };
        }

        /// <summary>
        /// Creates an instance indcating that all the validation was not successful
        /// </summary>
        /// <param name="rule">The validation rule</param>
        /// <param name="comparisonString">String representing what validation took place</param>
        /// <returns>The new ValidationResult</returns>
        public static ValidationResult CreateErrorInstance(ValidationRule rule, string comparisonString)
        {
            return new ValidationResult()
            {
                IsSuccess = false,
                HadMissingValues = false,
                ValidationRule = rule,
                Message = string.Format("{0}: {1}", comparisonString, Translations.ValidationResultError.ToUpper())
            };
        }
    }

    /// <summary>
    /// Enums for the different types of ValidationRules
    /// </summary>
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

    /// <summary>
    /// Abstraction of a validation rule
    /// </summary>
    public abstract class ValidationRule
    {
        /// <summary>
        /// The indicator being validated
        /// </summary>
        public Indicator Indicator { get; set; }

        /// <summary>
        /// The values to validate against
        /// </summary>
        public List<IndicatorValue> RelatedValues { get; set; }

        /// <summary>
        /// The names of the indicators to be used when displaying messages to the users
        /// </summary>
        public List<string> IndicatorNames { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ValidationRule()
        {

        }

        /// <summary>
        /// Instantiates a ValidationRule
        /// </summary>
        /// <param name="indicator">The indicator to validate</param>
        /// <param name="values">Values to validate against</param>
        /// <param name="indicatorNames">The names of the indicators to be used when displaying messages to the users</param>
        public ValidationRule(Indicator indicator, List<IndicatorValue> values, List<string> indicatorNames)
        {
            Initialize(indicator, values, indicatorNames);
        }

        /// <summary>
        /// Initializes the ValidationRule
        /// </summary>
        /// <param name="indicator">The indicator to validate</param>
        /// <param name="values">Values to validate against</param>
        /// <param name="indicatorNames">The names of the indicators to be used when displaying messages to the users</param>
        public void Initialize(Indicator indicator, List<IndicatorValue> values, List<string> indicatorNames)
        {
            Indicator = indicator;
            RelatedValues = values;
            IndicatorNames = indicatorNames;
        }

        /// <summary>
        /// Should be implemented so it determines whether or not the ValidationRule is valid given the indicator and related values
        /// </summary>
        /// <returns>A ValidationResult that says whether or not the validation was successful</returns>
        public abstract ValidationResult IsValid();

        /// <summary>
        /// Translates the indicator names for the background thread
        /// </summary>
        /// <param name="names">The indicator names to translate</param>
        /// <returns>Collection of translated indicator names</returns>
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

    /// <summary>
    /// Abstraction of a validation rule that compares two numbers
    /// </summary>
    public abstract class NumberValidationRule : ValidationRule
    {
        /// <summary>
        /// String representing the number comparison that took place, such as: x > y
        /// </summary>
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

        /// <summary>
        /// Parses the indicator value to make sure it is a valid number
        /// </summary>
        /// <returns>The number value of the indicator</returns>
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

        /// <summary>
        /// Parses the value to compare against to ensure it is anumber
        /// </summary>
        /// <returns>The number value of the indicator to validate against</returns>
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

        /// <summary>
        /// Builds the comparison string, eg: x > y
        /// </summary>
        /// <param name="valueToValidate">Value to validate</param>
        /// <param name="valueToValidateAgainst">Value that is being validated against</param>
        /// <returns>Thestring representation of the result of the validation</returns>
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

        /// <summary>
        /// Creates the ValidationnResult based on the validation comparison
        /// </summary>
        /// <param name="valueToValidate">Value to validate</param>
        /// <param name="valueToValidateAgainst">Value that is being validated against</param>
        /// <returns>ValidationResult for this comparison</returns>
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

    /// <summary>
    /// Checks if a number is greater than another
    /// </summary>
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

    /// <summary>
    /// Checks if a number is greater to or equal to another
    /// </summary>
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

    /// <summary>
    /// Checks is a number is less than another
    /// </summary>
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

    /// <summary>
    /// Checks that a number is less than or equal to another
    /// </summary>
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

    /// <summary>
    /// Checks that a number is equal to another
    /// </summary>
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

    /// <summary>
    /// Abstraction of a validation rule that validates a Date
    /// </summary>
    public abstract class DateTimeRule : ValidationRule
    {
        /// <summary>
        /// String representing the number comparison that took place, such as: dateX was after dateY
        /// </summary>
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

        /// <summary>
        /// Parses the DateTime value from the indicator being validated
        /// </summary>
        /// <returns>The DateTime value or null if there was not a valid value</returns>
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

        /// <summary>
        /// Parses the DateTime value for the value to compare against
        /// </summary>
        /// <returns>The DateTime value or null if there was not a valid value</returns>
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

        /// <summary>
        /// Builds the comaparison string between the two values
        /// </summary>
        /// <param name="valueToValidate">Value to validate</param>
        /// <param name="valueToValidateAgainst">Value to validate against</param>
        /// <returns>String representation of the validation result</returns>
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

        /// <summary>
        /// Creates the ValidationResult
        /// </summary>
        /// <param name="valueToValidate">The value to validate</param>
        /// <param name="valueToValidateAgainst">Value to validate against</param>
        /// <returns>ValidationResult indicating the succeess</returns>
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

    /// <summary>
    /// Checks that the date is earlier than another
    /// </summary>
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

    /// <summary>
    /// Checks that a date is later than another
    /// </summary>
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

    /// <summary>
    /// Checks that a date has the same year as another
    /// </summary>
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

    /// <summary>
    /// Models a mapping of a Validation Rule and the indicators that will be used in the validation
    /// </summary>
    public class ValidationMapping
    {
        /// <summary>
        /// The type of validation
        /// </summary>
        public ValidationRuleType ValidationType { get; set; }

        /// <summary>
        /// The indicators to validate against
        /// </summary>
        public string[] IndicatorsToCompareAgainst { get; set; }

        public ValidationMapping()
        {

        }

        public ValidationMapping(ValidationRuleType type, params string[] indicators)
        {
            ValidationType = type;
            IndicatorsToCompareAgainst = indicators;
        }

        /// <summary>
        /// Builds an instance of a ValidationRule
        /// </summary>
        /// <param name="mapping">The ValidationMapping</param>
        /// <param name="indicator">The indicator to validate gainst</param>
        /// <param name="values">The values to validate against</param>
        /// <returns></returns>
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
