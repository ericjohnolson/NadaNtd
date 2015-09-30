using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Excel;
using Nada.Globalization;
using Nada.Model.Base;
using Nada.Model.Repositories;
using Nada.Model.Survey;

namespace Nada.Model.Imports
{
    public class SurveyImporter : ImporterBase, IImporter
    {
        public override IndicatorEntityType EntityType { get { return IndicatorEntityType.Survey; } }
        public override string ImportName
        {
            get
            {
                if (sType != null)
                    return TranslationLookup.GetValue("Survey") + " " + TranslationLookup.GetValue("Import") + " - " + sType.SurveyTypeName.RemoveIllegalPathChars();
                else
                    return TranslationLookup.GetValue("Survey") + " " + TranslationLookup.GetValue("Import");
            }
        }
        private SurveyRepository repo = new SurveyRepository();
        private SurveyType sType = null;
        private int siteColumnIndex = 0;
        private int spotColumnIndex = 0;
        private int latColumnIndex = 0;
        private int lngColumnIndex = 0;

        public SurveyImporter() { }

        public override bool HasGroupedAdminLevels(ImportOptions opts)
        {
            var t = repo.GetSurveyType(opts.TypeId.Value);
            return t.HasMultipleLocations;
        }

        protected override string GetAdminLevelName(AdminLevel adminLevel, IHaveDynamicIndicatorValues form)
        {
            SurveyBase survey = (SurveyBase)form;
            return string.Join(", ", survey.AdminLevels.Select(a => a.Name).ToArray());
        }

        protected override void SetSpecificType(int id)
        {
            sType = repo.GetSurveyType(id);
            Indicators = sType.Indicators;
            DropDownValues = sType.IndicatorDropdownValues;
            Calculator = new CalcSurvey();
            Validator = new SurveyCustomValidator();
        }

        protected override void ReloadDropdownValues()
        {
            sType = repo.GetSurveyType(sType.Id);
            DropDownValues = sType.IndicatorDropdownValues;
        }

        public override List<TypeListItem> GetAllTypes()
        {
            return repo.GetSurveyTypes().Select(t => new TypeListItem
            {
                Id = t.Id,
                Name = t.SurveyTypeName
            }).ToList();
        }

        protected override int AddTypeSpecific(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, int startIndex)
        {
            if (Indicators.Values.FirstOrDefault(i => i.DataTypeId == (int)IndicatorDataType.SentinelSite) == null)
                return startIndex;

            if (options.SurveyNames.Count == 0)
            {
                startIndex++;
                xlsWorksheet.Cells[1, startIndex] = TranslationLookup.GetValue("IndSentinelSiteName");
                siteColumnIndex = startIndex;
            }

            startIndex++;
            xlsWorksheet.Cells[1, startIndex] = TranslationLookup.GetValue("IndSpotCheckName");
            spotColumnIndex = startIndex;
            startIndex++;
            xlsWorksheet.Cells[1, startIndex] = TranslationLookup.GetValue("IndSpotCheckLat");
            latColumnIndex = startIndex;
            startIndex++;
            xlsWorksheet.Cells[1, startIndex] = TranslationLookup.GetValue("IndSpotCheckLng");
            lngColumnIndex = startIndex;
            return startIndex;
        }

        protected override void AddTypeSpecificLists(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, Microsoft.Office.Interop.Excel.Worksheet xlsValidation,
            int adminLevelId, int r, CultureInfo currentCulture, int colCount)
        {
            if (Indicators.Values.FirstOrDefault(i => i.DataTypeId == (int)IndicatorDataType.SentinelSite) == null || adminLevelId == 0 || options.SurveyNames.Count > 0)
                return;
            var sites = repo.GetSitesForAdminLevel(new List<string> { adminLevelId.ToString() });
            if (sites.Count > 0)
                AddDataValidation(xlsWorksheet, xlsValidation, Util.GetExcelColumnName(colCount + 1), r, "", "", sites.Select(p => p.SiteName).ToList(), currentCulture);
        }

        protected override void AddTypeSpecificListValues(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, Microsoft.Office.Interop.Excel.Worksheet xlsValidation,
            int adminLevelId, int r, CultureInfo currentCulture, int colCount, IHaveDynamicIndicatorValues form)
        {
            if (Indicators.Values.FirstOrDefault(i => i.DataTypeId == (int)IndicatorDataType.SentinelSite) == null)
                return;

            SurveyBase sur = (SurveyBase)form;

            if (sur.SentinelSiteId.HasValue && sur.SentinelSiteId.Value > 0)
            {
                var site = repo.GetSiteById(sur.SentinelSiteId.Value);
                xlsWorksheet.Cells[r, siteColumnIndex] = site.SiteName;
            }
            else
            {
                xlsWorksheet.Cells[r, spotColumnIndex] = sur.SpotCheckName;
                xlsWorksheet.Cells[r, lngColumnIndex] = sur.Lng;
                xlsWorksheet.Cells[r, latColumnIndex] = sur.Lat;
            }
        }

        protected override void UpdateTypeSpecificValues(IHaveDynamicIndicatorValues form, DataRow row, ref string objerrors)
        {
            if (Indicators.Values.FirstOrDefault(i => i.DataTypeId == (int)IndicatorDataType.SentinelSite) == null)
                return;

            SurveyBase survey = (SurveyBase)form;

            survey.HasSentinelSite = true;
            if (string.IsNullOrEmpty(row[TranslationLookup.GetValue("IndSentinelSiteName")].ToString()))
            {
                survey.SiteType = TranslationLookup.GetValue("SpotCheck");
                survey.SpotCheckName = row[TranslationLookup.GetValue("IndSpotCheckName")].ToString();

                double d;
                if (!string.IsNullOrEmpty(row[TranslationLookup.GetValue("IndSpotCheckLat")].ToString()))
                    if (double.TryParse(row[TranslationLookup.GetValue("IndSpotCheckLat")].ToString(), out d))
                        survey.Lat = d;
                    else
                        objerrors += TranslationLookup.GetValue("ValidLatitude") + Environment.NewLine;

                if (!string.IsNullOrEmpty(row[TranslationLookup.GetValue("IndSpotCheckLng")].ToString()))
                    if (double.TryParse(row[TranslationLookup.GetValue("IndSpotCheckLng")].ToString(), out d))
                        survey.Lng = d;
                    else
                        objerrors += TranslationLookup.GetValue("ValidLongitude") + Environment.NewLine;
            }
            else
            {
                survey.SiteType = TranslationLookup.GetValue("Sentinel");
                var sites = repo.GetSitesForAdminLevel(survey.AdminLevels.Select(a => a.Id.ToString()));
                var site = sites.FirstOrDefault(s => s.SiteName == row[TranslationLookup.GetValue("IndSentinelSiteName")].ToString());
                if (site != null)
                    survey.SentinelSiteId = site.Id;
                else
                    objerrors += TranslationLookup.GetValue("ValidSentinelSite") + Environment.NewLine;
            }
        }
        
        protected override ImportResult MapAndSaveObjects(DataSet ds, int userId)
        {
            string rowId = "";
            string errorMessage = "";
            List<SurveyBase> objs = new List<SurveyBase>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string objerrors = "";
                var obj = repo.CreateSurvey(sType.Id);

                if (!HasMultipleAdminUnits())
                {
                    if (row["* " + TranslationLookup.GetValue("ID")] == null || row["* " + TranslationLookup.GetValue("ID")].ToString().Length == 0)
                        continue;
                    rowId = row["* " + TranslationLookup.GetValue("ID")].ToString();
                    int adminLevelId = Convert.ToInt32(row["* " + TranslationLookup.GetValue("ID")]);
                    obj.AdminLevels = new List<AdminLevel> { new AdminLevel { Id = adminLevelId } };
                }
                else
                {
                    rowId = row["* " + TranslationLookup.GetValue("SurveyName")].ToString();
                    if (!NamesToAdminUnits.ContainsKey(rowId))
                        continue;
                    obj.AdminLevels = NamesToAdminUnits[rowId].Units;
                }

                // CHECK FOR SENTINEL SITES/SPOTCHECK
                if (Indicators.Values.FirstOrDefault(i => i.DataTypeId == (int)IndicatorDataType.SentinelSite) != null)
                {
                    obj.HasSentinelSite = true;
                    if (!string.IsNullOrEmpty(row[TranslationLookup.GetValue("IndSpotCheckName")].ToString()))
                    {
                        obj.SiteType = TranslationLookup.GetValue("SpotCheck");
                        obj.SpotCheckName = row[TranslationLookup.GetValue("IndSpotCheckName")].ToString();

                        double d;
                        if (!string.IsNullOrEmpty(row[TranslationLookup.GetValue("IndSpotCheckLat")].ToString()))
                            if (double.TryParse(row[TranslationLookup.GetValue("IndSpotCheckLat")].ToString(), out d))
                                obj.Lat = d;
                            else
                                objerrors += TranslationLookup.GetValue("ValidLatitude") + Environment.NewLine;
                        if (!string.IsNullOrEmpty(row[TranslationLookup.GetValue("IndSpotCheckLng")].ToString()))
                            if (double.TryParse(row[TranslationLookup.GetValue("IndSpotCheckLng")].ToString(), out d))
                                obj.Lng = d;
                            else
                                objerrors += TranslationLookup.GetValue("ValidLongitude") + Environment.NewLine;
                    }
                    else
                    {
                        obj.SiteType = TranslationLookup.GetValue("Sentinel");
                        var sites = repo.GetSitesForAdminLevel(obj.AdminLevels.Select(a => a.Id.ToString()).ToList());
                        string siteName = !HasMultipleAdminUnits() ? row[TranslationLookup.GetValue("IndSentinelSiteName")].ToString() : NamesToAdminUnits[rowId].SentinelSiteName;
                        var site = sites.FirstOrDefault(s => s.SiteName == siteName);
                        if (site != null)
                            obj.SentinelSiteId = site.Id;
                        else
                            objerrors += TranslationLookup.GetValue("ValidSentinelSite") + Environment.NewLine;
                    }
                }

                obj.Notes = row[TranslationLookup.GetValue("Notes")].ToString();

                // Validation
                obj.IndicatorValues = GetDynamicIndicatorValues(ds, row, ref objerrors);
                objerrors += !obj.IsValid() ? obj.GetAllErrors(true) : "";
                errorMessage += GetObjectErrors(objerrors, rowId);
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

            List<SurveyBase> objs = new List<SurveyBase>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                // Uniquely identify the row
                string rowIdentifier;
                if (ds.Tables[0].Columns.Contains("* " + TranslationLookup.GetValue("ID")) && row["* " + TranslationLookup.GetValue("ID")].ToString().Length > 0)
                    rowIdentifier = row["* " + TranslationLookup.GetValue("ID")].ToString();
                else if (ds.Tables[0].Columns.Contains("* " + TranslationLookup.GetValue("SurveyName")) && row["* " + TranslationLookup.GetValue("SurveyName")].ToString().Length > 0)
                    rowIdentifier = row["* " + TranslationLookup.GetValue("SurveyName")].ToString();
                else
                    rowIdentifier = i.ToString();
                // Build an object and get its indicators
                string objerrors = "";
                SurveyBase obj = repo.CreateSurvey(sType.Id);
                obj.Notes = row[TranslationLookup.GetValue("Notes")].ToString();
                obj.IndicatorValues = GetDynamicIndicatorValues(ds, row, ref objerrors);

                // No meta data for surveys
                List<KeyValuePair<string, string>> metaData = new List<KeyValuePair<string, string>>();

                // Validate the object
                List<ValidationResult> validationResults = Validator.ValidateIndicators(sType.DisplayNameKey, translatedIndicators, obj.IndicatorValues, metaData);

                // Add the validation messages to the string
                foreach (ValidationResult validationResult in validationResults)
                {
                    // Add the validation results to the string
                    validationResultStr += string.Format("ID {0}: {1}{2}{3}{4}", rowIdentifier, validationResult.Message, Environment.NewLine, "--------", Environment.NewLine);
                    // See if this set of data has already been marked as invalid
                    if (valid && !validationResult.IsSuccess)
                        valid = false;
                }

                objs.Add(obj);
            }

            return BuildValidationResult(validationResultStr, valid, objs.Count);
        }

        private bool HasMultipleAdminUnits()
        {
            return NamesToAdminUnits != null;
        }
    
    }
}
