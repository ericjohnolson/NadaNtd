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
    public class PcJrfExporter : ExporterBase, IExporter
    {
        SettingsRepository settings = new SettingsRepository();
        DemoRepository demo = new DemoRepository();
        ExportRepository repo = new ExportRepository();

        public string ExportName
        {
            get { return Translations.PcJrfForm; }
        }

        public ExportResult ExportData(string filePath, int userId, ExportJrfQuestions questions)
        {
            try
            {
                int yearReported = questions.JrfYearReporting.Value;
                System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                excel.Application xlsApp = new excel.ApplicationClass();
                excel.Workbook xlsWorkbook;
                excel.Worksheet xlsWorksheet;
                excel.Range rng = null;
                object missing = System.Reflection.Missing.Value;

                // Open workbook
                xlsWorkbook = xlsApp.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, @"Exports\WHO_JRF_PC.xls"),
                    missing, missing, missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing, missing, missing);

                var districtLevel = settings.GetAllAdminLevels().First(a => a.IsDistrict);
                CountryDemography countryDemo = demo.GetCountryDemoByYear(yearReported);
                Country country = demo.GetCountry();
                List<AdminLevel> demography = new List<AdminLevel>();
                List<AdminLevel> tree = demo.GetAdminLevelTreeForDemography(districtLevel.LevelNumber, yearReported, ref demography);
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[1];
                AddQuestions(xlsWorksheet, rng, questions, countryDemo, demography, districtLevel.LevelNumber, country);
                // run macro to create district rows.
                xlsApp.DisplayAlerts = false;
                xlsApp.Run("Sheet1.DISTRICT");
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[2];
                AddDemo(xlsWorksheet, rng, demography, districtLevel.LevelNumber);

                //// Todo map diseases to workbook
                //xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[5];
                //AddIndicators(DiseaseType.GuineaWorm, StaticIntvType.GuineaWormIntervention, questions.JrfYearReporting.Value, xlsWorksheet, AddGwInds, AggGwInd);

                xlsWorkbook.SaveAs(filePath, excel.XlFileFormat.xlOpenXMLWorkbook, missing,
                    missing, false, false, excel.XlSaveAsAccessMode.xlNoChange,
                    excel.XlSaveConflictResolution.xlUserResolution, true,
                    missing, missing, missing);
                xlsApp.ScreenUpdating = true;
                xlsApp.Visible = true;
                rng = null;
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

        private void AddIndicators(DiseaseType diseaseType, StaticIntvType intvType, int year, excel.Worksheet xlsWorksheet,
            Action<excel.Worksheet, List<AdminLevelIndicators>> AddToWorksheet,
            Func<AggregateIndicator, object, object> customAggRule)
        {
            var indicators = repo.GetDistrictIndicatorTrees((int)intvType, year, (int)diseaseType, customAggRule);
            AddToWorksheet(xlsWorksheet, indicators);
        }

        private void AddIndValue(AdminLevelIndicators district, string location, string key, excel.Range rng,
            excel.Worksheet xlsWorksheet, object missing, bool shouldTranslate)
        {
            // If district doesn't contain key
            string value = district.Indicators[key].Value;
            if (!district.Indicators.ContainsKey(key))
                value = IndicatorAggregator.AggregateChildren(district.Children, key, null).Value;

            if (value != null)
            {
                rng = xlsWorksheet.get_Range(location, missing);
                if (shouldTranslate)
                    rng.Value = TranslationLookup.GetValue(value.ToString(), value.ToString());
                else
                    rng.Value = value;
            }
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
                AddIndValue(district, "C" + rowId, "EndemicityStatus28", rng, xlsWorksheet, missing, true);
                AddIndValue(district, "D" + rowId, "NumVas67", rng, xlsWorksheet, missing, false);
                AddIndValue(district, "E" + rowId, "VasReporting68", rng, xlsWorksheet, missing,  false);
                AddIndValue(district, "F" + rowId, "NumIdsr69", rng, xlsWorksheet, missing,  false);
                AddIndValue(district, "G" + rowId, "NumIdsrReporting70", rng, xlsWorksheet, missing,  false);
                AddIndValue(district, "H" + rowId, "NumRumors71", rng, xlsWorksheet, missing,  false);
                AddIndValue(district, "I" + rowId, "NumRumorsInvestigated72", rng, xlsWorksheet, missing,  false);
                AddIndValue(district, "J" + rowId, "NumClinical73", rng, xlsWorksheet, missing,  false);
                AddIndValue(district, "K" + rowId, "NumLab74", rng, xlsWorksheet, missing,  false);
                AddIndValue(district, "L" + rowId, "NumIndigenous75", rng, xlsWorksheet, missing,  false);
                AddIndValue(district, "N" + rowId, "NumVillageWithImported77", rng, xlsWorksheet, missing,  false);
                AddIndValue(district, "O" + rowId, "NumCasesContained78", rng, xlsWorksheet, missing,  false);
                AddIndValue(district, "P" + rowId, "NumCasesLost79", rng, xlsWorksheet, missing,  false);
                AddIndValue(district, "Q" + rowId, "NumEndemicVillages80", rng, xlsWorksheet, missing,  false);
                AddIndValue(district, "R" + rowId, "NumEndemicVillagesReporting81", rng, xlsWorksheet, missing,  false);
                AddIndValue(district, "S" + rowId, "NumEndemicVillageWater82", rng, xlsWorksheet, missing,  false);
                AddIndValue(district, "T" + rowId, "NumEndemicAbate83", rng, xlsWorksheet, missing,  false);
                AddIndValue(district, "U" + rowId, "NumVillagesSafeWater84", rng, xlsWorksheet, missing,  false);

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
