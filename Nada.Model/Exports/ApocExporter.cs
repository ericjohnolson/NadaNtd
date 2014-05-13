using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Process;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.Model.Survey;
using excel = Microsoft.Office.Interop.Excel;

namespace Nada.Model.Exports
{
    public class ApocExporter : ExporterBase, IExporter
    {
        SettingsRepository settings = new SettingsRepository();
        DemoRepository demo = new DemoRepository();
        ExportRepository repo = new ExportRepository();
        Country country = null;
        Disease disease = null;

        public string ExportName
        {
            get { return Translations.PcJrfForm; }
        }

        public ExportResult ExportData(string filePath, int userId, int year)
        {
            try
            {
                int yearReporting = year;
                country = demo.GetCountry();
                DateTime start = new DateTime(year, country.ReportingYearStartDate.Month, country.ReportingYearStartDate.Day);
                DateTime end = start.AddYears(1).AddDays(-1);
                DiseaseRepository repo = new DiseaseRepository();
                DiseaseDistroPc dd = repo.Create(DiseaseType.Oncho);
                disease = dd.Disease;
                System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Microsoft.Office.Interop.Excel.Workbook xlsWorkbook;
                Microsoft.Office.Interop.Excel.Workbooks xlsWorkbooks;
                Microsoft.Office.Interop.Excel.Worksheet xlsCountry;
                Microsoft.Office.Interop.Excel.Sheets xlsWorksheets;
                Microsoft.Office.Interop.Excel.Worksheet xlsDemo;
                Microsoft.Office.Interop.Excel.Worksheet xls3;
                Microsoft.Office.Interop.Excel.Worksheet xls4;
                Microsoft.Office.Interop.Excel.Worksheet xls5;
                Microsoft.Office.Interop.Excel.Worksheet xls6;
                Microsoft.Office.Interop.Excel.Worksheet xls7;
                Microsoft.Office.Interop.Excel.Worksheet xls8;
                Microsoft.Office.Interop.Excel.Worksheet xls9;
                Microsoft.Office.Interop.Excel.Worksheet xls10;
                Microsoft.Office.Interop.Excel.Worksheet xls11;
                excel.Range rng = null;
                object missing = System.Reflection.Missing.Value;

                // Open workbook
                xlsWorkbooks = xlsApp.Workbooks;
                xlsWorkbook = xlsWorkbooks.Add(true);
                xlsWorksheets = xlsWorkbook.Worksheets;

                var districtLevel = settings.GetAllAdminLevels().First(a => a.IsDistrict);
                CountryDemography countryDemo = demo.GetCountryDemoByYear(yearReporting);
                List<AdminLevel> demography = new List<AdminLevel>();
                demo.GetAdminLevelTreeForDemography(districtLevel.LevelNumber,
                    new DateTime(yearReporting, country.ReportingYearStartDate.Month, country.ReportingYearStartDate.Day), ref demography);
                demography = demography.Where(d => d.LevelNumber == districtLevel.LevelNumber).OrderBy(a => a.Name).ToList();

                xlsCountry = (excel.Worksheet)xlsWorkbook.Worksheets[1];
                xlsCountry.Name = TranslationLookup.GetValue("Country");
                AddCountryPage(xlsCountry, rng, country);
                xlsDemo = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorksheets.Add(missing, xlsCountry, missing, missing);
                xlsDemo.Name = TranslationLookup.GetValue("Demography");
                AddDemoPage(xlsDemo, rng, demography, districtLevel);
                xls3 = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorksheets.Add(missing, xlsDemo, missing, missing);
                xls3.Name = TranslationLookup.GetValue("DiseaseDistribution");
                AddDdPage(xls3, rng, demography, start, end, country.ReportingYearStartDate.Month, dd);
                xls4 = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorksheets.Add(missing, xls3, missing, missing);
                xls4.Name = TranslationLookup.GetValue("SurOnchoMapping").Replace(TranslationLookup.GetValue("Oncho") + " ", "");
                Add4(xls4, rng, demography, start, end, country.ReportingYearStartDate.Month);
                xls5 = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorksheets.Add(missing, xls4, missing, missing);
                xls5.Name = TranslationLookup.GetValue("SurOnchoAssesments").Replace(TranslationLookup.GetValue("Oncho") + " ", "");
                Add5(xls5, rng, demography, start, end, country.ReportingYearStartDate.Month);
                xls6 = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorksheets.Add(missing, xls5, missing, missing);
                xls6.Name = TranslationLookup.GetValue("IntvIvm");
                Add6(xls6, rng, demography, start, end, country.ReportingYearStartDate.Month);
                xls7 = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorksheets.Add(missing, xls6, missing, missing);
                xls7.Name = TranslationLookup.GetValue("IntvIvmAlb");
                Add7(xls7, rng, demography, start, end, country.ReportingYearStartDate.Month);
                xls8 = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorksheets.Add(missing, xls7, missing, missing);
                xls8.Name = TranslationLookup.GetValue("IntvIvmPzq");
                Add8(xls8, rng, demography, start, end, country.ReportingYearStartDate.Month);
                xls9 = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorksheets.Add(missing, xls8, missing, missing);
                xls9.Name = TranslationLookup.GetValue("IntvIvmPzqAlb");
                Add9(xls9, rng, demography, start, end, country.ReportingYearStartDate.Month);
                xls10 = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorksheets.Add(missing, xls9, missing, missing);
                xls10.Name = TranslationLookup.GetValue("SAEs");
                Add10(xls10, rng, demography, start, end, country.ReportingYearStartDate.Month);
                xls11 = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorksheets.Add(missing, xls10, missing, missing);
                xls11.Name = TranslationLookup.GetValue("PcTraining");
                Add11(xls11, rng, demography, start, end, country.ReportingYearStartDate.Month);

                xlsApp.DisplayAlerts = false;
                xlsWorkbook.SaveAs(filePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, missing,
                    missing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true,
                    missing, missing, missing);
                xlsApp.Visible = true;
                Marshal.ReleaseComObject(xlsWorksheets);
                Marshal.ReleaseComObject(xlsCountry);
                Marshal.ReleaseComObject(xlsDemo);
                Marshal.ReleaseComObject(xls3);
                Marshal.ReleaseComObject(xls4);
                Marshal.ReleaseComObject(xls5);
                Marshal.ReleaseComObject(xls6);
                Marshal.ReleaseComObject(xls7);
                Marshal.ReleaseComObject(xls8);
                Marshal.ReleaseComObject(xls9);
                Marshal.ReleaseComObject(xls10);
                Marshal.ReleaseComObject(xls11);
                Marshal.ReleaseComObject(xlsWorkbooks);
                Marshal.ReleaseComObject(xlsWorkbook);
                Marshal.ReleaseComObject(xlsApp);
                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
                return new ExportResult();

            }
            catch (Exception ex)
            {
                return new ExportResult(ex.Message);
            }
        }

        //Worksheet 11: Training
        //list all in which Oncho was chosed from the "Disease(s) included in intervention" indicator
        //all indicators
        private void Add11(excel.Worksheet xlsWorksheet, excel.Range rng, List<AdminLevel> districts, DateTime start, DateTime end, int month)
        {
            ReportOptions options = new ReportOptions { MonthYearStarts = month, StartDate = start, EndDate = end, IsCountryAggregation = false, IsByLevelAggregation = false, IsAllLocations = true, IsNoAggregation = true };
            ProcessRepository repo = new ProcessRepository();
            ProcessType p = repo.GetProcessType((int)StaticProcessType.PcTraining);
            ProcessReportGenerator gen = new ProcessReportGenerator();
            ReportResult result = RunReport(xlsWorksheet, p.Indicators, options, gen, (int)StaticProcessType.PcTraining);
            int headerCol = 1;
            foreach (DataColumn col in result.DataTableResults.Columns)
            {
                xlsWorksheet.Cells[1, headerCol] = col.ColumnName.Replace(" - " + p.TypeName, "");
                headerCol++;
            }

            int rowId = 2;
            foreach (DataRow dr in result.DataTableResults.Rows)
            {
                bool hasOncho = false;
                foreach (DataColumn col in result.DataTableResults.Columns)
                    if (col.ColumnName.Contains(TranslationLookup.GetValue("TrainingDiseases", "TrainingDiseases")) &&
                        dr[col.ColumnName].ToString().Contains(TranslationLookup.GetValue("SaeOncho", "SaeOncho")))
                    {
                        hasOncho = true;
                        break;
                    }

                if (hasOncho)
                {
                    int colId = 1;
                    foreach (DataColumn col in result.DataTableResults.Columns)
                    {
                        xlsWorksheet.Cells[rowId, colId] = dr[col];
                        colId++;
                    }
                    rowId++;
                }
            }
        }

        //Worksheet 10: SAEs
        //list all in which Oncho was chosen from "Which parasitic/bacterial infections did the patient have" indicator
        //all SAE indicators
        //add an additional calculated field: "Duration between dates of treatment and appearance of symptoms" = TBD
        private void Add10(excel.Worksheet xlsWorksheet, excel.Range rng, List<AdminLevel> districts, DateTime start, DateTime end, int month)
        {
            ReportOptions options = new ReportOptions { MonthYearStarts = month, StartDate = start, EndDate = end, IsCountryAggregation = false, IsByLevelAggregation = false, IsAllLocations = true, IsNoAggregation = true };
            ProcessRepository repo = new ProcessRepository();
            ProcessType p = repo.GetProcessType((int)StaticProcessType.SAEs);
            ProcessReportGenerator gen = new ProcessReportGenerator();
            ReportResult result = RunReport(xlsWorksheet, p.Indicators, options, gen, (int)StaticProcessType.SAEs);
            int headerCol = 1;
            foreach (DataColumn col in result.DataTableResults.Columns)
            {
                xlsWorksheet.Cells[1, headerCol] = col.ColumnName.Replace(" - " + p.TypeName, "");
                headerCol++;
            }
            
            int rowId = 2;
            foreach (DataRow dr in result.DataTableResults.Rows)
            {
                if (dr[TranslationLookup.GetValue("SAEWhichparasitichave", "SAEWhichparasitichave") + " - " + p.TypeName].ToString().Contains(TranslationLookup.GetValue("SaeOncho", "SaeOncho")))
                {
                    int colId = 1;
                    foreach (DataColumn col in result.DataTableResults.Columns)
                    {
                        xlsWorksheet.Cells[rowId, colId] = dr[col];
                        colId++;
                    }
                    rowId++;
                }
            }
        }

        //Worksheet 9: IVM+PZQ+ALB Intervention
        //aggregated to the reporting level 
        //all IVM+PZQ+ALB intervention indicators
        private void Add9(excel.Worksheet xlsWorksheet, excel.Range rng, List<AdminLevel> districts, DateTime start, DateTime end, int month)
        {
            ReportOptions options = new ReportOptions { MonthYearStarts = month, StartDate = start, EndDate = end, IsCountryAggregation = false, IsByLevelAggregation = true, IsAllLocations = false, IsNoAggregation = false };
            options.SelectedAdminLevels = districts;
            IntvRepository repo = new IntvRepository();
            IntvType intv = repo.GetIntvType((int)StaticIntvType.IvmPzqAlb);
            IntvReportGenerator gen = new IntvReportGenerator();
            AddReportToSheet(xlsWorksheet, intv.Indicators, options, gen, (int)StaticIntvType.IvmPzqAlb, intv.IntvTypeName);
        }

        //Worksheet 8: IVM+PZQ Intervention
        //aggregated to the reporting level 
        //all IVM+PZQ intervention indicators
        private void Add8(excel.Worksheet xlsWorksheet, excel.Range rng, List<AdminLevel> districts, DateTime start, DateTime end, int month)
        {
            ReportOptions options = new ReportOptions { MonthYearStarts = month, StartDate = start, EndDate = end, IsCountryAggregation = false, IsByLevelAggregation = true, IsAllLocations = false, IsNoAggregation = false };
            options.SelectedAdminLevels = districts;
            IntvRepository repo = new IntvRepository();
            IntvType intv = repo.GetIntvType((int)StaticIntvType.IvmPzq);
            IntvReportGenerator gen = new IntvReportGenerator();
            AddReportToSheet(xlsWorksheet, intv.Indicators, options, gen, (int)StaticIntvType.IvmPzq, intv.IntvTypeName);
        }

        //Worksheet 7: IVM+ALB Intervention
        //aggregated to the reporting level 
        //all IVM+ALB intervention indicators
        private void Add7(excel.Worksheet xlsWorksheet, excel.Range rng, List<AdminLevel> districts, DateTime start, DateTime end, int month)
        {
            ReportOptions options = new ReportOptions { MonthYearStarts = month, StartDate = start, EndDate = end, IsCountryAggregation = false, IsByLevelAggregation = true, IsAllLocations = false, IsNoAggregation = false };
            options.SelectedAdminLevels = districts;
            IntvRepository repo = new IntvRepository();
            IntvType intv = repo.GetIntvType((int)StaticIntvType.IvmAlb);
            IntvReportGenerator gen = new IntvReportGenerator();
            AddReportToSheet(xlsWorksheet, intv.Indicators, options, gen, (int)StaticIntvType.IvmAlb, intv.IntvTypeName);
        }

        //Worksheet 6: IVM Intervention
        //aggregated to the reporting level 
        //all IVM intervention indicators
        private void Add6(excel.Worksheet xlsWorksheet, excel.Range rng, List<AdminLevel> districts, DateTime start, DateTime end, int month)
        {
            ReportOptions options = new ReportOptions { MonthYearStarts = month, StartDate = start, EndDate = end, IsCountryAggregation = false, IsByLevelAggregation = true, IsAllLocations = false, IsNoAggregation = false };
            options.SelectedAdminLevels = districts;
            IntvRepository repo = new IntvRepository();
            IntvType intv = repo.GetIntvType((int)StaticIntvType.Ivm);
            IntvReportGenerator gen = new IntvReportGenerator();
            AddReportToSheet(xlsWorksheet, intv.Indicators, options, gen, (int)StaticIntvType.Ivm, intv.IntvTypeName);
        }

        //Worksheet 5: Assessment Survey
        //no aggregation - list all
        //all Oncho Assessment indicators
        private void Add5(excel.Worksheet xlsWorksheet, excel.Range rng, List<AdminLevel> districts, DateTime start, DateTime end, int month)
        {
            ReportOptions options = new ReportOptions { MonthYearStarts = month, StartDate = start, EndDate = end, IsCountryAggregation = false, IsByLevelAggregation = false, IsAllLocations = true, IsNoAggregation = true };
            SurveyRepository repo = new SurveyRepository();
            SurveyType survey = repo.GetSurveyType((int)StaticSurveyType.OnchoAssessment);
            SurveyReportGenerator gen = new SurveyReportGenerator();
            AddReportToSheet(xlsWorksheet, survey.Indicators, options, gen, (int)StaticSurveyType.OnchoAssessment, survey.SurveyTypeName);
        }

        //Worksheet 4: Mapping Survey
        //no aggregation - list all
        //all Oncho Mapping indicators
        private void Add4(excel.Worksheet xlsWorksheet, excel.Range rng, List<AdminLevel> districts, DateTime start, DateTime end, int month)
        {
            ReportOptions options = new ReportOptions { MonthYearStarts = month, StartDate = start, EndDate = end, IsCountryAggregation = false, IsByLevelAggregation = false, IsAllLocations = true, IsNoAggregation = true };
            SurveyRepository repo = new SurveyRepository();
            SurveyType survey = repo.GetSurveyType((int)StaticSurveyType.OnchoMapping);
            SurveyReportGenerator gen = new SurveyReportGenerator();
            AddReportToSheet(xlsWorksheet, survey.Indicators, options, gen, (int)StaticSurveyType.OnchoMapping, survey.SurveyTypeName);
        }

        private void AddCountryPage(excel.Worksheet xlsWorksheet, excel.Range rng, Country country)
        {
            AddValueToRange(xlsWorksheet, rng, "A1", TranslationLookup.GetValue("Country") + ": " + country.Name);
            AddValueToRange(xlsWorksheet, rng, "A2", TranslationLookup.GetValue("ApocDateReportGenerated") + ": " + DateTime.Now.ToShortDateString());
        }

        private void AddDemoPage(excel.Worksheet xlsWorksheet, excel.Range rng, List<AdminLevel> demography, AdminLevelType districtType)
        {
            AddValueToRange(xlsWorksheet, rng, "A1", districtType.DisplayName);
            AddValueToRange(xlsWorksheet, rng, "B1", TranslationLookup.GetValue("YearCensus"));
            AddValueToRange(xlsWorksheet, rng, "C1", TranslationLookup.GetValue("DateReported"));
            AddValueToRange(xlsWorksheet, rng, "D1", TranslationLookup.GetValue("GrowthRate"));
            AddValueToRange(xlsWorksheet, rng, "E1", TranslationLookup.GetValue("TotalPopulation"));
            AddValueToRange(xlsWorksheet, rng, "F1", TranslationLookup.GetValue("Pop5yo"));
            AddValueToRange(xlsWorksheet, rng, "G1", TranslationLookup.GetValue("PopAdult"));
            AddValueToRange(xlsWorksheet, rng, "H1", TranslationLookup.GetValue("PopFemale"));
            AddValueToRange(xlsWorksheet, rng, "I1", TranslationLookup.GetValue("PopMale"));
            AddValueToRange(xlsWorksheet, rng, "J1", TranslationLookup.GetValue("PercentRural"));
            AddValueToRange(xlsWorksheet, rng, "K1", TranslationLookup.GetValue("Notes"));
            int rowNum = 2;
            foreach (var demog in demography)
            {
                AddValueToRange(xlsWorksheet, rng, "A" + rowNum, demog.Name);
                if (demog.CurrentDemography.YearCensus.HasValue)
                    AddValueToRange(xlsWorksheet, rng, "B" + rowNum, demog.CurrentDemography.YearCensus.Value);
                AddValueToRange(xlsWorksheet, rng, "C" + rowNum, demog.CurrentDemography.DateDemographyData.ToShortDateString());
                if (demog.CurrentDemography.GrowthRate.HasValue)
                    AddValueToRange(xlsWorksheet, rng, "D" + rowNum, demog.CurrentDemography.GrowthRate.Value);

                if (demog.CurrentDemography.TotalPopulation.HasValue)
                    AddValueToRange(xlsWorksheet, rng, "E" + rowNum, demog.CurrentDemography.TotalPopulation.Value);
                if (demog.CurrentDemography.Pop5yo.HasValue)
                    AddValueToRange(xlsWorksheet, rng, "F" + rowNum, demog.CurrentDemography.Pop5yo.Value);
                if (demog.CurrentDemography.PopAdult.HasValue)
                    AddValueToRange(xlsWorksheet, rng, "G" + rowNum, demog.CurrentDemography.PopAdult.Value);
                if (demog.CurrentDemography.PopFemale.HasValue)
                    AddValueToRange(xlsWorksheet, rng, "H" + rowNum, demog.CurrentDemography.PopFemale.Value);
                if (demog.CurrentDemography.PopMale.HasValue)
                    AddValueToRange(xlsWorksheet, rng, "I" + rowNum, demog.CurrentDemography.PopMale.Value);
                if (demog.CurrentDemography.PercentRural.HasValue)
                    AddValueToRange(xlsWorksheet, rng, "J" + rowNum, demog.CurrentDemography.PercentRural.Value);
                AddValueToRange(xlsWorksheet, rng, "K" + rowNum, demog.CurrentDemography.Notes);
                rowNum++;
            }
        }

        //Worksheet 3: Disease Distribution
        //aggregate to the reporting level
        //all Oncho Disease Distribution indicators
        private void AddDdPage(excel.Worksheet xlsWorksheet, excel.Range rng, List<AdminLevel> districts, DateTime start, DateTime end, int month, DiseaseDistroPc dd)
        {
            ReportOptions options = new ReportOptions { MonthYearStarts = month, StartDate = start, EndDate = end, IsCountryAggregation = false, IsByLevelAggregation = true, IsAllLocations = false, IsNoAggregation = false };
            options.SelectedAdminLevels = districts;
            DistributionReportGenerator gen = new DistributionReportGenerator(); 
            AddReportToSheet(xlsWorksheet, dd.Indicators, options, gen, disease.Id, disease.DisplayName);
        }

        private void AddReportToSheet(excel.Worksheet xlsWorksheet, Dictionary<string, Indicator> indicators, ReportOptions options, IReportGenerator gen, int typeId, string typeName)
        {
            ReportResult result = RunReport(xlsWorksheet, indicators, options, gen, typeId);
            int headerCol = 1;
            foreach (DataColumn col in result.DataTableResults.Columns)
            {
                xlsWorksheet.Cells[1, headerCol] = col.ColumnName.Replace(" - " + typeName, "");
                headerCol++;
            }

            int rowId = 2;
            foreach (DataRow dr in result.DataTableResults.Rows)
            {
                int colId = 1;
                foreach (DataColumn col in result.DataTableResults.Columns)
                {
                    xlsWorksheet.Cells[rowId, colId] = dr[col];
                    colId++;
                }
                rowId++;
            }
        }

        private ReportResult RunReport(excel.Worksheet xlsWorksheet, Dictionary<string, Indicator> indicators, ReportOptions options, IReportGenerator gen, int typeId)
        {
            foreach (var indicator in indicators)
                if (!indicator.Value.IsCalculated)
                    options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(typeId, indicator));

            ReportResult result = gen.Run(new SavedReport { ReportOptions = options });
            result.DataTableResults.Columns.Remove(Translations.Location);
            result.DataTableResults.Columns.Remove(Translations.Type);
            result.DataTableResults.Columns.Remove(Translations.Year);
            return result;
        }

    }
}
