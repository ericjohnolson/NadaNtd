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

            startIndex++;
            xlsWorksheet.Cells[1, startIndex] = TranslationLookup.GetValue("IndSentinelSiteName");
            siteColumnIndex = startIndex;
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
            if (Indicators.Values.FirstOrDefault(i => i.DataTypeId == (int)IndicatorDataType.SentinelSite) == null || adminLevelId == 0)
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

                if (NamesToAdminUnits == null)
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
                    obj.AdminLevels = NamesToAdminUnits[rowId];
                }

                // CHECK FOR SENTINEL SITES/SPOTCHECK
                if (Indicators.Values.FirstOrDefault(i => i.DataTypeId == (int)IndicatorDataType.SentinelSite) != null)
                {
                    obj.HasSentinelSite = true;
                    if (string.IsNullOrEmpty(row[TranslationLookup.GetValue("IndSentinelSiteName")].ToString()))
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
                        var sites = repo.GetSitesForAdminLevel(new List<string> { obj.AdminLevelId.Value.ToString() });
                        var site = sites.FirstOrDefault(s => s.SiteName == row[TranslationLookup.GetValue("IndSentinelSiteName")].ToString());
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
    
    }
}
