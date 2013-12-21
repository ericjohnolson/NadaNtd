using System;
using System.Collections.Generic;
using System.Data;
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
        public override string ImportName { get { return Translations.Survey + " " + Translations.Import; } }
        private SurveyRepository repo = new SurveyRepository();
        private SurveyType sType = null;
        public SurveyImporter() { }

        public override void SetType(int id)
        {
            sType = repo.GetSurveyType(id);
            Indicators = sType.Indicators;
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

        protected override int AddTypeSpecific(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet)
        {
            if (Indicators.Values.FirstOrDefault(i => i.DataTypeId == (int)IndicatorDataType.SentinelSite) == null)
                return 0;

            xlsWorksheet.Cells[1, 3] = Translations.IndSentinelSiteName;
            xlsWorksheet.Cells[1, 4] = Translations.IndSpotCheckName;
            xlsWorksheet.Cells[1, 5] = Translations.IndSpotCheckLat;
            xlsWorksheet.Cells[1, 6] = Translations.IndSpotCheckLng;
            return 4;
        }

        protected override void AddTypeSpecificLists(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, int adminLevelId, int r)
        {
            if (Indicators.Values.FirstOrDefault(i => i.DataTypeId == (int)IndicatorDataType.SentinelSite) == null)
                return;
            var sites = repo.GetSitesForAdminLevel(new List<string> { adminLevelId.ToString() });
            if(sites.Count > 0)
                AddDataValidation(xlsWorksheet, Util.GetExcelColumnName(3), r, "", "", sites.Select(p => p.SiteName).ToList());
        }

        protected override ImportResult MapAndSaveObjects(DataSet ds, int userId)
        {
            string errorMessage = "";
            List<SurveyBase> objs = new List<SurveyBase>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string objerrors = "";
                var obj = repo.CreateSurvey(sType.Id);
                int adminLevelId = Convert.ToInt32(row[Translations.Location + "#"]);
                obj.AdminLevels = new List<AdminLevel> { new AdminLevel { Id =  adminLevelId } };

                // CHECK FOR SENTINEL SITES/SPOTCHECK
                if (Indicators.Values.FirstOrDefault(i => i.DataTypeId == (int)IndicatorDataType.SentinelSite) != null)
                {
                    obj.HasSentinelSite = true;
                    if(string.IsNullOrEmpty(row[Translations.IndSentinelSiteName].ToString()))
                    {
                        obj.SiteType = Translations.SpotCheck;
                        obj.SpotCheckName = row[Translations.IndSpotCheckName].ToString();

                        double d;
                        if (!string.IsNullOrEmpty(row[Translations.IndSpotCheckLat].ToString()) && double.TryParse(row[Translations.IndSpotCheckLat].ToString(), out d))
                            obj.Lat = d;
                        else
                            objerrors += Translations.ValidLatitude + Environment.NewLine;
                        if (!string.IsNullOrEmpty(row[Translations.IndSpotCheckLng].ToString()) && double.TryParse(row[Translations.IndSpotCheckLng].ToString(), out d))
                            obj.Lng = d;
                        else
                            objerrors += Translations.ValidLongitude + Environment.NewLine;
                    }
                    else
                    {
                        obj.SiteType = Translations.Sentinel;
                        var sites = repo.GetSitesForAdminLevel(new List<string> { adminLevelId.ToString() });
                        var site = sites.FirstOrDefault(s => s.SiteName == row[Translations.IndSentinelSiteName].ToString());
                        if (site != null)
                            obj.SentinelSiteId = site.Id;
                        else
                            objerrors += Translations.ValidSentinelSite + Environment.NewLine;

                    }
                }

                obj.Notes = row[Translations.Notes].ToString();
                obj.IndicatorValues = GetDynamicIndicatorValues(ds, row);

                objerrors += !obj.IsValid() ? obj.GetAllErrors(true) : "";
                if (!string.IsNullOrEmpty(objerrors))
                    errorMessage += string.Format(Translations.ImportErrors, row[Translations.Location], "", objerrors) + Environment.NewLine;

                objs.Add(obj);
            }

            if (!string.IsNullOrEmpty(errorMessage))
                return new ImportResult(Translations.ImportErrorHeader + Environment.NewLine + errorMessage);

            foreach (var obj in objs)
                repo.SaveSurvey(obj, userId);

            return new ImportResult
            {
                WasSuccess = true,
                Count = objs.Count,
                Message = string.Format(Translations.ImportSuccess, objs.Count)
            };
        }

    }

}
