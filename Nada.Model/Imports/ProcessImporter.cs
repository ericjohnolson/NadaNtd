using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Excel;
using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Imports;
using Nada.Model.Process;
using Nada.Model.Repositories;

namespace Nada.Model
{
    /// <summary>
    /// Handles importing data for the Process entities
    /// </summary>
    public class ProcessImporter : ImporterBase, IImporter
    {
        public override IndicatorEntityType EntityType { get { return IndicatorEntityType.Process; } }
        public override string ImportName
        {
            get
            {

                if (type != null)
                    return TranslationLookup.GetValue("ProcessIndicators") + " " + TranslationLookup.GetValue("Import") + " - " + type.TypeName.RemoveIllegalPathChars();
                else
                    return TranslationLookup.GetValue("ProcessIndicators") + " " + TranslationLookup.GetValue("Import");
            }
        }
        private ProcessRepository repo = new ProcessRepository();
        private ProcessType type = null;
        public ProcessImporter()
        {

        }
        protected override void SetSpecificType(int id)
        {
            type = repo.GetProcessType(id);
            Indicators = type.Indicators;
            DropDownValues = type.IndicatorDropdownValues;
            Calculator = new CalcProcess();
            Validator = new ProcessCustomValidator();
        }

        protected override void ReloadDropdownValues()
        {
            type = repo.GetProcessType(type.Id);
            DropDownValues = type.IndicatorDropdownValues;
        }

        public override List<TypeListItem> GetAllTypes()
        {
            return repo.GetProcessTypes().Select(t => new TypeListItem
            {
                Id = t.Id,
                Name = t.TypeName
            }).ToList();
        }

        protected override ImportResult MapAndSaveObjects(DataSet ds, int userId)
        {
            string errorMessage = "";
            List<ProcessBase> objs = new List<ProcessBase>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row["* " + TranslationLookup.GetValue("ID")] == null || row["* " + TranslationLookup.GetValue("ID")].ToString().Length == 0)
                    continue;
                string objerrors = "";
                var obj = repo.Create(type.Id);
                obj.AdminLevelId = Convert.ToInt32(row["* " + TranslationLookup.GetValue("ID")]);
                obj.Notes = row[TranslationLookup.GetValue("Notes")].ToString();
                // Validation
                obj.IndicatorValues = GetDynamicIndicatorValues(ds, row, ref objerrors);
                objerrors += !obj.IsValid() ? obj.GetAllErrors(true) : "";
                errorMessage += GetObjectErrors(objerrors, row["* " + TranslationLookup.GetValue("ID")].ToString());
                objs.Add(obj);
            }

            if (!string.IsNullOrEmpty(errorMessage))
                return new ImportResult(CreateErrorMessage(errorMessage));

            repo.Save(objs, userId);

            return new ImportResult
            {
                WasSuccess = true,
                Count = objs.Count,
                Message = string.Format(TranslationLookup.GetValue("ImportSuccess"), objs.Count)
            };
        }

        protected override ImportResult MapAndValidateObjects(DataSet ds)
        {
            // Will hold the validation result string
            string validationResultStr = "";
            // Will determine if there was any error
            bool valid = true;

            List<ProcessBase> objs = new List<ProcessBase>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                // Build an object and get its indicators
                if (row["* " + TranslationLookup.GetValue("ID")] == null || row["* " + TranslationLookup.GetValue("ID")].ToString().Length == 0)
                    continue;
                string objerrors = "";
                ProcessBase obj = repo.Create(type.Id);
                obj.AdminLevelId = Convert.ToInt32(row["* " + TranslationLookup.GetValue("ID")]);
                obj.Notes = row[TranslationLookup.GetValue("Notes")].ToString();
                obj.IndicatorValues = GetDynamicIndicatorValues(ds, row, ref objerrors);

                // Need to get the meta data
                List<KeyValuePair<string, string>> metaData = new List<KeyValuePair<string, string>>();
                // First get the DateReported indicator value
                IndicatorValue indicatorValueToCompareAgainst = obj.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "DateReported");
                // Use the DateReported value to determine the meta data as described in Mingle story 169
                if (indicatorValueToCompareAgainst != null && obj.AdminLevelId.HasValue)
                {
                    DateTime dateReported;
                    if (DateTime.TryParse(indicatorValueToCompareAgainst.DynamicValue, out dateReported))
                    {
                        // Determine the start and end date for the same year as the DateReported
                        DateTime start = new DateTime(dateReported.Year, 1, 1, 0, 0, 0, dateReported.Kind);
                        DateTime end = start.AddYears(1);
                        // Get the meta data
                        metaData = Calculator.GetMetaData(Indicators.Where(i => !i.Value.IsCalculated && i.Value.DataTypeId == (int)IndicatorDataType.Calculated).Select(i => new KeyValuePair<string, string>(type.DisplayNameKey, i.Value.DisplayName)).ToList(),
                            obj.AdminLevelId.Value, start, end);
                    }
                }

                // Validate the object
                List<ValidationResult> validationResults = Validator.ValidateIndicators(type.DisplayNameKey, translatedIndicators, obj.IndicatorValues, metaData);

                // Add the validation messages to the string
                foreach (ValidationResult validationResult in validationResults)
                {
                    // Add the validation results to the string
                    validationResultStr += string.Format("ID {0}: {1}{2}{3}{4}", row["* " + TranslationLookup.GetValue("ID")].ToString(), validationResult.Message, Environment.NewLine, "--------", Environment.NewLine);
                    // See if this set of data has already been marked as invalid
                    if (valid && !validationResult.IsSuccess)
                        valid = false;
                }

                objs.Add(obj);
            }

            return BuildValidationResult(validationResultStr, valid, objs.Count);
        }

    }
}
