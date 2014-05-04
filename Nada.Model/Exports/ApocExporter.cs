using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Repositories;
using excel = Microsoft.Office.Interop.Excel;

namespace Nada.Model.Exports
{
    public class ApocExporter : ExporterBase, IExporter
    {
        SettingsRepository settings = new SettingsRepository();
        DemoRepository demo = new DemoRepository();
        ExportRepository repo = new ExportRepository();

        public string ExportName
        {
            get { return Translations.PcJrfForm; }
        }

        public ExportResult ExportData(string filePath, int userId, int year)
        {
            try
            {

                int yearReporting = year;
                System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Microsoft.Office.Interop.Excel.Workbook xlsWorkbook;
                Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet;
                excel.Range rng = null;
                object missing = System.Reflection.Missing.Value;

                // Open workbook
                xlsWorkbook = xlsApp.Workbooks.Add(true);

                var districtLevel = settings.GetAllAdminLevels().First(a => a.IsDistrict);
                CountryDemography countryDemo = demo.GetCountryDemoByYear(yearReporting);
                Country country = demo.GetCountry();
                List<AdminLevel> demography = new List<AdminLevel>();
                List<AdminLevel> tree = demo.GetAdminLevelTreeForDemography(districtLevel.LevelNumber, 
                    new DateTime(yearReporting, country.ReportingYearStartDate.Month, country.ReportingYearStartDate.Day), ref demography);
                
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[1];
                AddCountryPage(xlsWorksheet, rng, country);
                //xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[3];
                //AddCountryPage(xlsWorksheet, rng, demography, districtLevel.LevelNumber, questions, country, countryDemo);
                //xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[11];
                //AddPlanningPage(xlsWorksheet, rng, questions);


                // sheet 4 GW (start row 9)
                //xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[5];
                //AddIndicators(DiseaseType.GuineaWorm, StaticIntvType.GuineaWormIntervention, yearReporting, xlsWorksheet, AddGwInds, AggGwInd);
                ////// leprosy
                //xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[6];
                //AddIndicators(DiseaseType.Leprosy, StaticIntvType.LeprosyIntervention, yearReporting, xlsWorksheet, AddLeprosyInds, AggLeprosyInd);
                ////// hat
                //xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[7];
                //AddIndicators(DiseaseType.Hat, StaticIntvType.HatIntervention, yearReporting, xlsWorksheet, AddHatInds, AggHatInd);
                ////// leish
                //xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[8];
                //AddIndicators(DiseaseType.Leish, StaticIntvType.LeishIntervention, yearReporting, xlsWorksheet, AddLeishInds, AggLeishInd);
                ////// buruli
                //xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[9];
                //AddIndicators(DiseaseType.Buruli, StaticIntvType.BuruliUlcerIntv, yearReporting, xlsWorksheet, AddBuInds, AggBuInd);
                ////// yaws
                //xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[10];
                //AddIndicators(DiseaseType.Yaws, StaticIntvType.YawsIntervention, yearReporting, xlsWorksheet, AddYawsInds, AggYawsInd);

                xlsApp.DisplayAlerts = false;
                xlsWorkbook.SaveAs(filePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, missing,
                    missing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true,
                    missing, missing, missing);
                xlsApp.Visible = true;
                xlsWorksheet = null;
                xlsWorkbook = null;
                xlsApp = null;
                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
                return new ExportResult();

            }
            catch (Exception ex)
            {
                return new ExportResult(ex.Message);
            }
        }

        private void AddCountryPage(excel.Worksheet xlsWorksheet, excel.Range rng, Country country)
        {
            AddValueToRange(xlsWorksheet, rng, "A1", TranslationLookup.GetValue("Country") + ": " + country.Name);
            AddValueToRange(xlsWorksheet, rng, "A2", TranslationLookup.GetValue("ApocDateReportGenerated") + ": " + DateTime.Now.ToShortDateString());
        }

        private void AddIndicators(DiseaseType diseaseType, StaticIntvType intvType, int year, excel.Worksheet xlsWorksheet,
            Action<excel.Worksheet, List<AdminLevelIndicators>> AddToWorksheet,
            Func<AggregateIndicator, object, object> customAggRule)
        {
            var indicators = repo.GetDistrictIndicatorTrees((int)intvType, year, (int)diseaseType, customAggRule);
            AddToWorksheet(xlsWorksheet, indicators);
        }


        #region Sheet Specific
        private void AddQuestions(excel.Worksheet xlsWorksheet, excel.Range rng,  ExportJrfQuestions questions, CountryDemography countryDemo,
                List<AdminLevel> demography, int districtLevel, Country country)
        {
            AddValueToRange(xlsWorksheet, rng, "E34", country.Name);
            AddValueToRange(xlsWorksheet, rng, "E36", questions.JrfYearReporting.Value);
            AddValueToRange(xlsWorksheet, rng, "E38", TranslationLookup.GetValue(questions.JrfEndemicLf, questions.JrfEndemicLf));
            AddValueToRange(xlsWorksheet, rng, "E40", TranslationLookup.GetValue(questions.JrfEndemicOncho, questions.JrfEndemicOncho));
            AddValueToRange(xlsWorksheet, rng, "E42", TranslationLookup.GetValue(questions.JrfEndemicSth, questions.JrfEndemicSth));
            AddValueToRange(xlsWorksheet, rng, "E44", TranslationLookup.GetValue(questions.JrfEndemicSch, questions.JrfEndemicSch));
            AddValueToRange(xlsWorksheet, rng, "E46", demography.Where(d => d.LevelNumber == districtLevel).Count());
            if(countryDemo.PercentPsac.HasValue)
                AddValueToRange(xlsWorksheet, rng, "E48", countryDemo.PercentPsac.Value / 100);
            else
                AddValueToRange(xlsWorksheet, rng, "E48", 0);
            if(countryDemo.PercentSac.HasValue)
                AddValueToRange(xlsWorksheet, rng, "E49", countryDemo.PercentSac.Value / 100);
            else
                AddValueToRange(xlsWorksheet, rng, "E49", 0);
            if(countryDemo.PercentAdult.HasValue)
                AddValueToRange(xlsWorksheet, rng, "E50", countryDemo.PercentAdult.Value / 100);
            else
                AddValueToRange(xlsWorksheet, rng, "E50", 0);
        }

        private void AddDemo(excel.Worksheet xlsWorksheet, excel.Range rng, List<AdminLevel> demography, int districtLevel)
        {
            int rowId = 9;
            foreach (var district in demography.Where(d => d.LevelNumber == districtLevel))
            {
                AdminLevel parent = demography.First(d => d.Id == district.ParentId);
                AddValueToRange(xlsWorksheet, rng, "B" + rowId, parent.Name);
                AddValueToRange(xlsWorksheet, rng, "C" + rowId, district.Name);
                AddValueToRange(xlsWorksheet, rng, "D" + rowId, district.CurrentDemography.TotalPopulation);
                rowId++;
            }
        }

        private void AddGwInds(excel.Worksheet xlsWorksheet, List<AdminLevelIndicators> districtIndicators)
        {
            excel.Range rng;
            object missing = System.Reflection.Missing.Value;
            int rowId = 9;
            foreach (var district in districtIndicators)
            {
                rng = xlsWorksheet.get_Range("A" + rowId, missing);
                rng.Value = district.Parent.Name;
                rng = xlsWorksheet.get_Range("B" + rowId, missing);
                rng.Value = district.Name;
                //AddIndValue(district, "C" + rowId, "EndemicityStatus28", rng, xlsWorksheet, missing, true);
                //AddIndValue(district, "D" + rowId, "NumVas67", rng, xlsWorksheet, missing, false);
                //AddIndValue(district, "E" + rowId, "VasReporting68", rng, xlsWorksheet, missing,  false);
                //AddIndValue(district, "F" + rowId, "NumIdsr69", rng, xlsWorksheet, missing,  false);
                //AddIndValue(district, "G" + rowId, "NumIdsrReporting70", rng, xlsWorksheet, missing,  false);
                //AddIndValue(district, "H" + rowId, "NumRumors71", rng, xlsWorksheet, missing,  false);
                //AddIndValue(district, "I" + rowId, "NumRumorsInvestigated72", rng, xlsWorksheet, missing,  false);
                //AddIndValue(district, "J" + rowId, "NumClinical73", rng, xlsWorksheet, missing,  false);
                //AddIndValue(district, "K" + rowId, "NumLab74", rng, xlsWorksheet, missing,  false);
                //AddIndValue(district, "L" + rowId, "NumIndigenous75", rng, xlsWorksheet, missing,  false);
                //AddIndValue(district, "N" + rowId, "NumVillageWithImported77", rng, xlsWorksheet, missing,  false);
                //AddIndValue(district, "O" + rowId, "NumCasesContained78", rng, xlsWorksheet, missing,  false);
                //AddIndValue(district, "P" + rowId, "NumCasesLost79", rng, xlsWorksheet, missing,  false);
                //AddIndValue(district, "Q" + rowId, "NumEndemicVillages80", rng, xlsWorksheet, missing,  false);
                //AddIndValue(district, "R" + rowId, "NumEndemicVillagesReporting81", rng, xlsWorksheet, missing,  false);
                //AddIndValue(district, "S" + rowId, "NumEndemicVillageWater82", rng, xlsWorksheet, missing,  false);
                //AddIndValue(district, "T" + rowId, "NumEndemicAbate83", rng, xlsWorksheet, missing,  false);
                //AddIndValue(district, "U" + rowId, "NumVillagesSafeWater84", rng, xlsWorksheet, missing,  false);

                rowId++;
            }

            rng = null;
        }

        private object AggGwInd(AggregateIndicator ind1, object existingValue)
        {
            if (ind1.IndicatorId == 28)
            {
                if (ind1.Value == "Endemic" || existingValue.ToString() == "Endemic")
                    return "Endemic";
                if (ind1.Value == "EndemicityTbv" || existingValue.ToString() == "EndemicityTbv")
                    return "EndemicityTbv";
                return "NotEndemic";
            }

            return ind1.Value;
        }

        

        #endregion
    }
}
