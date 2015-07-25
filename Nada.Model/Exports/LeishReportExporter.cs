using Nada.Globalization;
using Nada.Model.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using excel = Microsoft.Office.Interop.Excel;

namespace Nada.Model.Exports
{
    public class LeishReportExporter : ExporterBase, IExporter
    {
        DemoRepository DemoRepo = new DemoRepository();

        public string ExportName
        {
            get { return Translations.LeishReport; }
        }

        public ExportResult DoExport(string fileName, int userId, ExportType exportType)
        {
            throw new NotImplementedException();
        }

        public ExportResult DoExport(string fileName, int userId, LeishReportQuestions questions)
        {
            try
            {
                System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                excel.Application xlsApp = new excel.ApplicationClass();
                xlsApp.DisplayAlerts = false;
                excel.Workbook xlsWorkbook;
                excel.Worksheet xlsWorksheet;
                excel.Range rng = null;
                object missing = System.Reflection.Missing.Value;

                xlsWorkbook = xlsApp.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, @"Exports\Leishmaniasis_Report_Card.xlsx"),
                    missing, missing, missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing, missing, missing);


                
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[1];

                Country country = DemoRepo.GetCountry();
                CountryDemography countryStats = DemoRepo.GetCountryLevelStatsRecent();
                AddCountryInfo(xlsWorksheet, rng, country, countryStats, questions);

                xlsApp.DisplayAlerts = false;
                xlsWorkbook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, missing,
                    missing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true,
                    missing, missing, missing);
                xlsApp.Visible = true;
                xlsWorksheet = null;
                xlsWorkbook = null;
                xlsApp = null;
                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
                return new ExportResult { WasSuccess = true };
            }
            catch (Exception ex)
            {
                return new ExportResult(ex.Message);
            }
        }

        public void AddCountryInfo(excel.Worksheet xlsWorksheet, excel.Range rng, Country country, CountryDemography countryStats, LeishReportQuestions questions)
        {
            AddValueToRange(xlsWorksheet, rng, "B3", country.Name);
            AddValueToRange(xlsWorksheet, rng, "M3", questions.LeishRepYearReporting);
            AddValueToRange(xlsWorksheet, rng, "D6", countryStats.TotalPopulation);

            // Gender ratio
            string femalePercent = CalculatePercent(countryStats.PopFemale, countryStats.TotalPopulation);
            string malePercent = CalculatePercent(countryStats.PopMale, countryStats.TotalPopulation);
            string genderRatio = string.Format("{0}% female - {1}% male", femalePercent, malePercent);
            AddValueToRange(xlsWorksheet, rng, "D7", genderRatio);

            // Population age
            string lessThan5Percent = CalculatePercent(countryStats.Pop5yo, countryStats.TotalPopulation);
            string ageRatio = string.Format("{0}%/{1}%/{2}%", lessThan5Percent, "--", "--"); // TODO Add other percents
            AddValueToRange(xlsWorksheet, rng, "L6", ageRatio);
        }

        private static string CalculatePercent(double? n, double? d)
        {
            if (!n.HasValue || !d.HasValue)
                return "--";
            return string.Format("{0:0.00}", n.Value / d.Value * 100);
        }

    }
}
