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

        public ExportResult DoExport(string fileName, int userId, ExportType exportType)
        {
            throw new NotImplementedException();
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

                DateTime startDate = new DateTime(yearReported, country.ReportingYearStartDate.Month, country.ReportingYearStartDate.Day);
                DateTime endDate = startDate.AddYears(1).AddDays(-1);
                List<AdminLevel> tree = demo.GetAdminLevelTreeForDemography(districtLevel.LevelNumber, startDate, endDate, ref demography);
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

       
    }
}
