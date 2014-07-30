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
            startIndex++;
            xlsWorksheet.Cells[1, startIndex] = TranslationLookup.GetValue("IndSpotCheckName");
            startIndex++;
            xlsWorksheet.Cells[1, startIndex] = TranslationLookup.GetValue("IndSpotCheckLat");
            startIndex++;
            xlsWorksheet.Cells[1, startIndex] = TranslationLookup.GetValue("IndSpotCheckLng");
            return startIndex;
        }

        protected override void AddTypeSpecificLists(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, Microsoft.Office.Interop.Excel.Worksheet xlsValidation, 
            int adminLevelId, int r, CultureInfo currentCulture, int colCount)
        {
            if (Indicators.Values.FirstOrDefault(i => i.DataTypeId == (int)IndicatorDataType.SentinelSite) == null)
                return;
            var sites = repo.GetSitesForAdminLevel(new List<string> { adminLevelId.ToString() });
            if (sites.Count > 0)
                AddDataValidation(xlsWorksheet, xlsValidation, Util.GetExcelColumnName(colCount + 1), r, "", "", sites.Select(p => p.SiteName).ToList(), currentCulture);
        }

        protected override ImportResult MapAndSaveObjects(DataSet ds, int userId)
        {
            string errorMessage = "";
            List<SurveyBase> objs = new List<SurveyBase>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row[TranslationLookup.GetValue("ID")] == null || row[TranslationLookup.GetValue("ID")].ToString().Length == 0)
                    continue;
                string objerrors = "";
                var obj = repo.CreateSurvey(sType.Id);
                int adminLevelId = Convert.ToInt32(row[TranslationLookup.GetValue("ID")]);
                obj.AdminLevels = new List<AdminLevel> { new AdminLevel { Id = adminLevelId } };

                // CHECK FOR SENTINEL SITES/SPOTCHECK
                if (Indicators.Values.FirstOrDefault(i => i.DataTypeId == (int)IndicatorDataType.SentinelSite) != null)
                {
                    obj.HasSentinelSite = true;
                    if (string.IsNullOrEmpty(row[TranslationLookup.GetValue("IndSentinelSiteName")].ToString()))
                    {
                        obj.SiteType = TranslationLookup.GetValue("SpotCheck");
                        obj.SpotCheckName = row[TranslationLookup.GetValue("IndSpotCheckName")].ToString();

                        double d;
                        if (!string.IsNullOrEmpty(row[TranslationLookup.GetValue("IndSpotCheckLat")].ToString()) && double.TryParse(row[TranslationLookup.GetValue("IndSpotCheckLat")].ToString(), out d))
                            obj.Lat = d;
                        else
                            objerrors += TranslationLookup.GetValue("ValidLatitude") + Environment.NewLine;
                        if (!string.IsNullOrEmpty(row[TranslationLookup.GetValue("IndSpotCheckLng")].ToString()) && double.TryParse(row[TranslationLookup.GetValue("IndSpotCheckLng")].ToString(), out d))
                            obj.Lng = d;
                        else
                            objerrors += TranslationLookup.GetValue("ValidLongitude") + Environment.NewLine;
                    }
                    else
                    {
                        obj.SiteType = TranslationLookup.GetValue("Sentinel");
                        var sites = repo.GetSitesForAdminLevel(new List<string> { adminLevelId.ToString() });
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
                errorMessage += GetObjectErrors(objerrors, row[TranslationLookup.GetValue("ID")].ToString());
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
