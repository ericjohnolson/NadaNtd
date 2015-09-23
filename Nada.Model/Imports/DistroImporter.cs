using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Excel;
using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Imports;
using Nada.Model.Repositories;

namespace Nada.Model
{
    public class DistroImporter : ImporterBase, IImporter
    {
        public override IndicatorEntityType EntityType { get { return IndicatorEntityType.DiseaseDistribution; } }
        public override string ImportName
        {
            get
            {
                if(cmType != null)
                    return TranslationLookup.GetValue("DiseaseDistribution") + " " + TranslationLookup.GetValue("Import") + " - " + cmType.Disease.DisplayName.RemoveIllegalPathChars();
                else if (type != null)
                    return TranslationLookup.GetValue("DiseaseDistribution") + " " + TranslationLookup.GetValue("Import") + " - " + type.Disease.DisplayName.RemoveIllegalPathChars();
                else 
                    return TranslationLookup.GetValue("DiseaseDistribution") + " " + TranslationLookup.GetValue("Import");
            }
        }
        private DiseaseDistroPc type = null;
        private DiseaseDistroCm cmType = null;
        DiseaseRepository repo = new DiseaseRepository();
        protected override void SetSpecificType(int id)
        {
            cmType = null;
            type = null;
            var d = repo.GetDiseaseById(id);
            if (d.DiseaseType == Translations.CM)
            {
                cmType = repo.CreateCm((DiseaseType)d.Id);
                Indicators = cmType.Indicators;
                DropDownValues = cmType.IndicatorDropdownValues;
            }
            else
            {
                type = repo.Create((DiseaseType)d.Id);
                Indicators = type.Indicators;
                DropDownValues = type.IndicatorDropdownValues;
            }
            Calculator = new CalcDistro();
            Validator = new DiseaseDistroCustomValidator();
        }

        protected override void ReloadDropdownValues()
        {
            if (cmType != null)
            {
                cmType = repo.CreateCm((DiseaseType)cmType.Disease.Id);
                DropDownValues = cmType.IndicatorDropdownValues;
            }
            else
            {
                type = repo.Create((DiseaseType)type.Disease.Id);
                DropDownValues = type.IndicatorDropdownValues;
            }
        }

        public override List<TypeListItem> GetAllTypes()
        {
            return repo.GetSelectedDiseases().Select(t => new TypeListItem
            {
                Id = t.Id,
                Name = t.DisplayName
            }).ToList();
        }

        protected override ImportResult MapAndSaveObjects(DataSet ds, int userId)
        {
            if (type != null)
                return MapAndSaveObjectsPc(ds, userId);
            return MapAndSaveObjectsCm(ds, userId);
        }

        private ImportResult MapAndSaveObjectsPc(DataSet ds, int userId)
        {
            string errorMessage = "";
            List<DiseaseDistroPc> objs = new List<DiseaseDistroPc>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row["* " + TranslationLookup.GetValue("ID")] == null || row["* " + TranslationLookup.GetValue("ID")].ToString().Length == 0)
                    continue;
                string objerrors = "";
                var obj = repo.Create((DiseaseType)type.Disease.Id);
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
        private ImportResult MapAndSaveObjectsCm(DataSet ds, int userId)
        {
            string errorMessage = "";
            List<DiseaseDistroCm> objs = new List<DiseaseDistroCm>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row["* " + TranslationLookup.GetValue("ID")] == null || row["* " + TranslationLookup.GetValue("ID")].ToString().Length == 0)
                    continue;

                string objerrors = "";
                var obj = repo.CreateCm((DiseaseType)cmType.Disease.Id);
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
            if (type != null)
                return MapAndValidateObjectsPc(ds);
            return MapAndValidateObjectsCm(ds);
        }

        private ImportResult MapAndValidateObjectsPc(DataSet ds)
        {
            // Will hold the validation result string
            string validationResultStr = "";
            // Will determine if there was any error
            bool valid = true;

            List<DiseaseDistroPc> objs = new List<DiseaseDistroPc>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                // Build an object and get its indicators
                if (row["* " + TranslationLookup.GetValue("ID")] == null || row["* " + TranslationLookup.GetValue("ID")].ToString().Length == 0)
                    continue;
                string objerrors = "";
                DiseaseDistroPc obj = repo.Create((DiseaseType)type.Disease.Id);
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
                        metaData = Calculator.GetMetaData(Indicators.Where(i => !i.Value.IsCalculated && i.Value.DataTypeId == (int)IndicatorDataType.Calculated).Select(i => new KeyValuePair<string, string>(type.Disease.DisplayNameKey, i.Value.DisplayName)).ToList(),
                            obj.AdminLevelId.Value, start, end);
                    }
                }

                // Validate the object
                List<ValidationResult> validationResults = Validator.ValidateIndicators(type.Disease.DisplayNameKey, translatedIndicators, obj.IndicatorValues, metaData);

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

            return new ImportResult
            {
                WasSuccess = valid,
                Count = objs.Count,
                Message = validationResultStr
            };
        }

        private ImportResult MapAndValidateObjectsCm(DataSet ds)
        {
            // Will hold the validation result string
            string validationResultStr = "";
            // Will determine if there was any error
            bool valid = true;

            List<DiseaseDistroCm> objs = new List<DiseaseDistroCm>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                // Build an object and get its indicators
                if (row["* " + TranslationLookup.GetValue("ID")] == null || row["* " + TranslationLookup.GetValue("ID")].ToString().Length == 0)
                    continue;
                string objerrors = "";
                DiseaseDistroCm obj = repo.CreateCm((DiseaseType)cmType.Disease.Id);
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
                        metaData = Calculator.GetMetaData(Indicators.Where(i => !i.Value.IsCalculated && i.Value.DataTypeId == (int)IndicatorDataType.Calculated).Select(i => new KeyValuePair<string, string>(cmType.Disease.DisplayNameKey, i.Value.DisplayName)).ToList(),
                            obj.AdminLevelId.Value, start, end);
                    }
                }

                // Validate the object
                List<ValidationResult> validationResults = Validator.ValidateIndicators(cmType.Disease.DisplayNameKey, translatedIndicators, obj.IndicatorValues, metaData);

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

            return new ImportResult
            {
                WasSuccess = valid,
                Count = objs.Count,
                Message = validationResultStr
            };
        }
        
    }
}
