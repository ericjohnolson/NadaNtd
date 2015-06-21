using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;
using Nada.Model.Intervention;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using excel = Microsoft.Office.Interop.Excel;

namespace Nada.Model.Exports
{
    public class PcEpiExporter : ExporterBase, IExporter
    {
        SettingsRepository settings = new SettingsRepository();
        DemoRepository demo = new DemoRepository();
        ExportRepository repo = new ExportRepository();

        public string ExportName
        {
            get { return Translations.ExportsPcEpiDataForm; }
        }

        public override string Extension
        {
            get { return "xls"; }
        }

        public override string GetYear(ExportType exportType)
        {
            var ind = exportType.IndicatorValues.FirstOrDefault(i => i.Indicator.DisplayName == "Year");
            if (ind != null)
                return ind.DynamicValue;
            return "";
        }

        public ExportResult DoExport(string fileName, int userId, ExportType exportType)
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

                xlsWorkbook = xlsApp.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, @"Exports\WHO_EPIRF_PC_NATDAT.xls"),
                    missing, missing, missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing, missing, missing);


                Country country = demo.GetCountry();
                int reportYear = DateTime.Now.Year;
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[1];
                AddInfo(xlsWorksheet, rng, country, exportType, ref reportYear);

                //xlsApp.Visible = true;       //set to 'true' when debbugging, Exec is visible
                //xlsApp.DisplayAlerts = true; //enable all the prompt alerts for debug. 
                DateTime start = new DateTime(reportYear, 1, 1);
                DateTime end = new DateTime(reportYear, 12, 31);
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[2];
                AddLfMm(xlsWorksheet, start, end);
                xlsApp.Run("Sheet13.UNIT");
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[3];
                AddOncho(xlsWorksheet, start, end);
                xlsApp.Run("Sheet17.UNIT_ONCHO");
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[4];
                AddSth(xlsWorksheet, start, end);
                xlsApp.Run("Sheet15.UNIT_STH");
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[5];
                AddSch(xlsWorksheet, start, end);
                xlsApp.Run("Sheet16.UNIT_SCH");

                xlsWorkbook.SaveAs(fileName, excel.XlFileFormat.xlOpenXMLWorkbook, missing,
                    missing, false, false, excel.XlSaveAsAccessMode.xlNoChange,
                    excel.XlSaveConflictResolution.xlUserResolution, true,
                    missing, missing, missing);
                xlsApp.ScreenUpdating = true;
                xlsApp.Visible = true;
                rng = null;

                Marshal.ReleaseComObject(xlsWorksheet);
                Marshal.ReleaseComObject(xlsWorkbook);
                Marshal.ReleaseComObject(xlsApp);
                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
                return new ExportResult { WasSuccess = true };

            }
            catch (Exception ex)
            {
                return new ExportResult(ex.Message);
            }
        }

        private void AddLfMm(excel.Worksheet xlsWorksheet, DateTime start, DateTime end)
        {
            ReportOptions options = new ReportOptions { MonthYearStarts = 1, StartDate = start, EndDate = end, IsCountryAggregation = true, IsByLevelAggregation = false, IsAllLocations = false, IsNoAggregation = false };
            IntvReportGenerator gen = new IntvReportGenerator();
            IntvRepository repo = new IntvRepository();
            SurveyRepository surveys = new SurveyRepository();
            int rowCount = 0;
            IntvType type = repo.GetIntvType((int)StaticIntvType.LfMorbidityManagement);
            foreach (var indicator in type.Indicators)
                if (!indicator.Value.IsCalculated)
                    options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(type.Id, indicator));
            ReportResult result = gen.Run(new SavedReport { ReportOptions = options });

            // Top horizontal columns
            foreach (DataRow dr in result.DataTableResults.Rows)
            {
                foreach (DataColumn col in result.DataTableResults.Columns)
                {
                    if (col.ColumnName.Contains(TranslationLookup.GetValue("LFMMDPNumLymphoedemaPatients", "LFMMDPNumLymphoedemaPatients") + " -"))
                        xlsWorksheet.Cells[5, 10] = dr[col];
                    if (col.ColumnName.Contains(TranslationLookup.GetValue("LFMMDPNumLymphoedemaPatientsTreated", "LFMMDPNumLymphoedemaPatientsTreated") + " -"))
                        xlsWorksheet.Cells[7, 10] = dr[col];
                    if (col.ColumnName.Contains(TranslationLookup.GetValue("LFMMDPNumHydroceleCases", "LFMMDPNumHydroceleCases") + " -"))
                        xlsWorksheet.Cells[8, 10] = dr[col];
                    if (col.ColumnName.Contains(TranslationLookup.GetValue("LFMMDPNumHydroceleCasesTreated", "LFMMDPNumHydroceleCasesTreated") + " -"))
                        xlsWorksheet.Cells[9, 10] = dr[col];
                }
            }

            // Last half of year (J6)
            options.StartDate = start.AddMonths(6);
            result = gen.Run(new SavedReport { ReportOptions = options });
            foreach (DataRow dr in result.DataTableResults.Rows)
            {
                foreach (DataColumn col in result.DataTableResults.Columns)
                {
                    if (col.ColumnName.Contains(TranslationLookup.GetValue("LFMMDPNumLymphoedemaPatients", "LFMMDPNumLymphoedemaPatients") + " -"))
                        xlsWorksheet.Cells[6, 10] = dr[col];
                }
            }

            // Get surveys for rows
            var lfSurveys = surveys.GetByTypeForDistrictsInDateRange(new List<int> { (int)StaticSurveyType.LfMapping, (int)StaticSurveyType.LfSentinel, (int)StaticSurveyType.LfTas }, start, end);
            int rowNumber = 15;
            foreach (SurveyBase survey in lfSurveys.OrderBy(s => s.SortOrder))
            {
                foreach (var adminLevel in survey.AdminLevels)
                {
                    bool isMfTestType = false;
                    
                    // Static indicators
                    xlsWorksheet.Cells[rowNumber, (int)ExcelCol.B] = adminLevel.Name;
                    xlsWorksheet.Cells[rowNumber, (int)ExcelCol.E] = survey.DateReported.ToString("MMMM");
                    if (survey.TypeOfSurvey.Id == (int)StaticSurveyType.LfMapping)
                    {
                        var testTypeVal = survey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "LFMapTestType");
                        if (testTypeVal != null && testTypeVal.DynamicValue == "Mf")
                            isMfTestType = true; 

                        xlsWorksheet.Cells[rowNumber, (int)ExcelCol.D] = TranslationLookup.GetValue("Mapping", "Mapping");
                    }
                    else if (survey.TypeOfSurvey.Id == (int)StaticSurveyType.LfSentinel)
                    {
                        var testTypeVal = survey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "LFSurTestType");
                        if (testTypeVal != null && testTypeVal.DynamicValue == "Mf")
                            isMfTestType = true;

                        xlsWorksheet.Cells[rowNumber, (int)ExcelCol.D] = survey.SiteType;
                        if (survey.SentinelSiteId.HasValue)
                        {
                            var site = surveys.GetSiteById(survey.SentinelSiteId.Value);
                            if (site.Lat.HasValue)
                                xlsWorksheet.Cells[rowNumber, (int)ExcelCol.F] = Math.Round(site.Lat.Value, 2);
                            if (site.Lng.HasValue)
                                xlsWorksheet.Cells[rowNumber, (int)ExcelCol.G] = Math.Round(site.Lng.Value, 2);
                            xlsWorksheet.Cells[rowNumber, (int)ExcelCol.C] = site.SiteName;
                        }
                        else
                        {
                            if (survey.Lat.HasValue)
                                xlsWorksheet.Cells[rowNumber, (int)ExcelCol.F] = Math.Round(survey.Lat.Value, 2);
                            if (survey.Lng.HasValue)
                                xlsWorksheet.Cells[rowNumber, (int)ExcelCol.G] = Math.Round(survey.Lng.Value, 2);
                            xlsWorksheet.Cells[rowNumber, (int)ExcelCol.C] = survey.SpotCheckName;
                        }
                    }
                    else if (survey.TypeOfSurvey.Id == (int)StaticSurveyType.LfTas)
                    {
                        //xlsWorksheet.Cells[rowNumber, 1] = survey.IndicatorValues
                    }
 

                    // Dynamic indicators
                    foreach (IndicatorValue val in survey.IndicatorValues)
                    {
                        // Eval name
                        if (val.Indicator.DisplayName == "EuName")
                            xlsWorksheet.Cells[rowNumber, (int)ExcelCol.A] = val.DynamicValue;
                        // Name of survey site
                        else if (val.Indicator.DisplayName == "LFMapSurSiteNames")
                            xlsWorksheet.Cells[rowNumber, (int)ExcelCol.C] = val.DynamicValue;
                        // TAS objective
                        else if (val.Indicator.DisplayName == "TASTasObjective")
                            xlsWorksheet.Cells[rowNumber, (int)ExcelCol.D] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "LFMapSurLatitude" && !string.IsNullOrEmpty(val.DynamicValue))
                            xlsWorksheet.Cells[rowNumber, (int)ExcelCol.F] = Math.Round(Convert.ToDouble(val.DynamicValue), 2);
                        else if (val.Indicator.DisplayName == "LFMapSurLongitude" && !string.IsNullOrEmpty(val.DynamicValue))
                            xlsWorksheet.Cells[rowNumber, (int)ExcelCol.G] = Math.Round(Convert.ToDouble(val.DynamicValue), 2);
                        else if (val.Indicator.DisplayName == "LFSurDateOfTheFirstRoundOfPc")
                            xlsWorksheet.Cells[rowNumber, (int)ExcelCol.H] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "LFSurNumberOfRoundsOfPcCompletedPriorToS")
                            xlsWorksheet.Cells[rowNumber, (int)ExcelCol.I] = val.DynamicValue;
                        //else if (val.Indicator.DisplayName == "LFMapSurTestType" || val.Indicator.DisplayName == "LFSurTestType")
                        //    xlsWorksheet.Cells[rowNumber, 9] = TranslationLookup.GetValue(val.DynamicValue, val.DynamicValue);
                        // MF: Number of people examined
                        else if (val.Indicator.DisplayName == "LFMapSurNumberOfIndividualsExamined" || val.Indicator.DisplayName == "LFSurNumberOfIndividualsExamined")
                        {
                            if(isMfTestType)
                                xlsWorksheet.Cells[rowNumber, (int)ExcelCol.J] = val.DynamicValue;
                            else
                                xlsWorksheet.Cells[rowNumber, (int)ExcelCol.S] = val.DynamicValue;
                        }
                        // MF: Number of people positive
                        else if (val.Indicator.DisplayName == "LFSurNumberOfIndividualsPositive" || val.Indicator.DisplayName == "LFMapSurNumberOfIndividualsPositive")
                        {
                            if (isMfTestType)
                                xlsWorksheet.Cells[rowNumber, (int)ExcelCol.K] = val.DynamicValue;
                            else
                                xlsWorksheet.Cells[rowNumber, (int)ExcelCol.T] = val.DynamicValue;
                        }
                        // mean dens
                        //else if (val.Indicator.DisplayName == "LFMapSurMeanDensity" || val.Indicator.DisplayName == "LFSurMeanDensity")
                        //    xlsWorksheet.Cells[rowNumber, 13] = val.DynamicValue;
                        // count
                        //else if (val.Indicator.DisplayName == "LFSurCount" || val.Indicator.DisplayName == "LFMapSurCount")
                        //    xlsWorksheet.Cells[rowNumber, 14] = val.DynamicValue;
                        // community load
                        //else if (val.Indicator.DisplayName == "LFMapSurCommunityMfLoad" || val.Indicator.DisplayName == "LFSurCommunityMfLoad")
                        //    xlsWorksheet.Cells[rowNumber, 15] = val.DynamicValue;
                        // ly peeps examined
                        // MF: % Positive

                        else if (val.Indicator.DisplayName == "LFSurExaminedLympho" || val.Indicator.DisplayName == "LFMapSurExaminedLympho")
                            xlsWorksheet.Cells[rowNumber, (int)ExcelCol.Y] = val.DynamicValue;
                        // ly peeps pos
                        else if (val.Indicator.DisplayName == "LFMapSurNumberOfCasesOfLymphoedema" || val.Indicator.DisplayName == "LFSurPosLympho")
                            xlsWorksheet.Cells[rowNumber, (int)ExcelCol.Z] = val.DynamicValue;
                        // hy peeps examined
                        else if (val.Indicator.DisplayName == "LFSurExaminedHydro" || val.Indicator.DisplayName == "LFMapSurExaminedHydro1")
                            xlsWorksheet.Cells[rowNumber, (int)ExcelCol.AB] = val.DynamicValue;
                        // hy peeps pos
                        else if (val.Indicator.DisplayName == "LFMapSurNumberOfCasesOfHydrocele" || val.Indicator.DisplayName == "LFSurPosHydro")
                            xlsWorksheet.Cells[rowNumber, (int)ExcelCol.AC] = val.DynamicValue;

                    }
                    rowNumber++;
                    rowCount++;
                }
            }

            // run formula
            xlsWorksheet.Cells[3, 5] = rowCount;

        }

        private void AddInfo(excel.Worksheet xlsWorksheet, excel.Range rng, Country country, ExportType exportType, ref int year)
        {
            AddValueToRange(xlsWorksheet, rng, "E32", country.Name);
            foreach (var val in exportType.IndicatorValues)
            {
                if (val.Indicator.DisplayName == "Year")
                {
                    year = Convert.ToInt32(val.DynamicValue);
                    AddValueToRange(xlsWorksheet, rng, "E34", val.DynamicValue);
                }
                if (val.Indicator.DisplayName == "JrfEndemicLf")
                    AddValueToRange(xlsWorksheet, rng, "E36", TranslationLookup.GetValue(val.DynamicValue, val.DynamicValue));
                if (val.Indicator.DisplayName == "JrfEndemicOncho")
                    AddValueToRange(xlsWorksheet, rng, "E38", TranslationLookup.GetValue(val.DynamicValue, val.DynamicValue));
                if (val.Indicator.DisplayName == "JrfEndemicSth")
                    AddValueToRange(xlsWorksheet, rng, "E40", TranslationLookup.GetValue(val.DynamicValue, val.DynamicValue));
                if (val.Indicator.DisplayName == "JrfEndemicSch")
                    AddValueToRange(xlsWorksheet, rng, "E42", TranslationLookup.GetValue(val.DynamicValue, val.DynamicValue));
            }
        }

        private void AddOncho(excel.Worksheet xlsWorksheet, DateTime start, DateTime end)
        {
            SurveyRepository repo = new SurveyRepository();
            int rowCount = 0;
            var surveys = repo.GetByTypeForDistrictsInDateRange(new List<int> { (int)StaticSurveyType.OnchoAssessment, (int)StaticSurveyType.OnchoMapping }, start, end);
            int rowNumber = 8;
            foreach (SurveyBase survey in surveys.OrderBy(s => s.SortOrder))
            {
                foreach (AdminLevel unit in survey.AdminLevels)
                {
                    xlsWorksheet.Cells[rowNumber, 1] = unit.Name;
                    xlsWorksheet.Cells[rowNumber, 3] = survey.DateReported.ToString("MMMM");
                    foreach (IndicatorValue val in survey.IndicatorValues)
                    {
                        if (val.Indicator.DisplayName == "OnchoSurSiteName" || val.Indicator.DisplayName == "OnchoMapSiteName")
                            xlsWorksheet.Cells[rowNumber, 2] = val.DynamicValue;
                        else if ((val.Indicator.DisplayName == "OnchoSurLatitude" || val.Indicator.DisplayName == "OnchoMapSurLatitude") && !string.IsNullOrEmpty(val.DynamicValue))
                            xlsWorksheet.Cells[rowNumber, 4] = Math.Round(Convert.ToDouble(val.DynamicValue), 2);
                        else if ((val.Indicator.DisplayName == "OnchoSurLongitude" || val.Indicator.DisplayName == "OnchoMapSurLongitude") && !string.IsNullOrEmpty(val.DynamicValue))
                            xlsWorksheet.Cells[rowNumber, 5] = Math.Round(Convert.ToDouble(val.DynamicValue), 2);
                        else if (val.Indicator.DisplayName == "OnchoSurSurveyType" || val.Indicator.DisplayName == "OnchoMapSurSurveyType")
                            xlsWorksheet.Cells[rowNumber, 6] = TranslationLookup.GetValue(val.DynamicValue, val.DynamicValue);
                        else if (val.Indicator.DisplayName == "OnchoSurAgeRange" || val.Indicator.DisplayName == "OnchoMapSurAgeRange")
                            xlsWorksheet.Cells[rowNumber, 7] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "OnchoSurTestType" || val.Indicator.DisplayName == "OnchoMapSurTestType")
                            xlsWorksheet.Cells[rowNumber, 8] = TranslationLookup.GetValue(val.DynamicValue, val.DynamicValue);
                        else if (val.Indicator.DisplayName == "OnchoSurNumberOfIndividualsExamined" || val.Indicator.DisplayName == "OnchoMapSurNumberOfIndividualsExamined")
                            xlsWorksheet.Cells[rowNumber, 9] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "OnchoSurNumberOfIndividualsPositive" || val.Indicator.DisplayName == "OnchoMapSurNumberOfIndividualsPositive")
                            xlsWorksheet.Cells[rowNumber, 10] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "OnchoSurIfTestTypeIsNpNumDep" || val.Indicator.DisplayName == "OnchoMapSurIfTestTypeIsNpNumDep")
                            xlsWorksheet.Cells[rowNumber, 12] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "OnchoSurIfTestTypeIsNpNumNod" || val.Indicator.DisplayName == "OnchoMapSurIfTestTypeIsNpNumNod")
                            xlsWorksheet.Cells[rowNumber, 13] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "OnchoSurIfTestTypeIsNpNumWri" || val.Indicator.DisplayName == "OnchoMapSurIfTestTypeIsNpNumWri")
                            xlsWorksheet.Cells[rowNumber, 14] = val.DynamicValue;
                    }
                    rowNumber++;
                    rowCount++;
                }
            }
            xlsWorksheet.Cells[3, 5] = rowCount;
        }

        private void AddSth(excel.Worksheet xlsWorksheet, DateTime start, DateTime end)
        {
            SurveyRepository repo = new SurveyRepository();
            int rowCount = 0;
            var surveys = repo.GetByTypeForDistrictsInDateRange(new List<int> { (int)StaticSurveyType.SthSentinel, (int)StaticSurveyType.SthMapping }, start, end);
            int rowNumber = 8;

            foreach (SurveyBase survey in surveys.OrderBy(s => s.SortOrder))
            {
                if (survey.TypeOfSurvey.Id == (int)StaticSurveyType.SthSentinel)
                {
                    if (survey.HasSentinelSite && survey.SentinelSiteId.HasValue)
                    {
                        var site = repo.GetSiteById(survey.SentinelSiteId.Value);
                        if (site.Lat.HasValue)
                            xlsWorksheet.Cells[rowNumber, 4] = Math.Round(site.Lat.Value, 2);
                        if (site.Lng.HasValue)
                            xlsWorksheet.Cells[rowNumber, 5] = Math.Round(site.Lng.Value, 2);
                        xlsWorksheet.Cells[rowNumber, 2] = site.SiteName;

                    }
                    else
                    {
                        if (survey.Lat.HasValue)
                            xlsWorksheet.Cells[rowNumber,4] = Math.Round(survey.Lat.Value, 2);
                        if (survey.Lng.HasValue)
                            xlsWorksheet.Cells[rowNumber, 5] = Math.Round(survey.Lng.Value, 2);
                        xlsWorksheet.Cells[rowNumber, 2] = survey.SpotCheckName;
                    }
                }

                foreach (AdminLevel unit in survey.AdminLevels)
                {
                    xlsWorksheet.Cells[rowNumber, 1] = unit.Name;
                    xlsWorksheet.Cells[rowNumber, 3] = survey.DateReported.ToString("MMMM");
                    foreach (IndicatorValue val in survey.IndicatorValues)
                    {
                        if (val.Indicator.DisplayName == "STHMapSurSurSiteNames")
                            xlsWorksheet.Cells[rowNumber, 2] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "STHMapSurSurLatitude" && !string.IsNullOrEmpty(val.DynamicValue))
                            xlsWorksheet.Cells[rowNumber, 4] = Math.Round(Convert.ToDouble(val.DynamicValue), 2);
                        else if (val.Indicator.DisplayName == "STHMapSurSurLongitude" && !string.IsNullOrEmpty(val.DynamicValue))
                            xlsWorksheet.Cells[rowNumber, 5] = Math.Round(Convert.ToDouble(val.DynamicValue), 2);
                        else if (val.Indicator.DisplayName == "STHMapSurSurTestType" || val.Indicator.DisplayName == "STHSurTestType")
                            xlsWorksheet.Cells[rowNumber, 6] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "STHMapSurSurAgeGroupSurveyed" || val.Indicator.DisplayName == "STHSurAgeGroupSurveyed")
                            xlsWorksheet.Cells[rowNumber, 7] = val.DynamicValue;

                        else if (val.Indicator.DisplayName == "STHMapSurSurNumberOfIndividualsExaminedAscaris" || val.Indicator.DisplayName == "STHSurNumberOfIndividualsExaminedAscaris")
                            xlsWorksheet.Cells[rowNumber, 8] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "STHSurNumberOfIndividualsPositiveAscaris" || val.Indicator.DisplayName == "STHMapSurSurNumberOfIndividualsPositiveAscaris")
                            xlsWorksheet.Cells[rowNumber, 9] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "STHMapSurSurProportionOfHeavyIntensityOfInAS" || val.Indicator.DisplayName == "STHSurProportionOfHeavyIntensityOfAsc")
                            xlsWorksheet.Cells[rowNumber, 11] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "STHMapSurSurProportionOfModerateIntensityOfInAS" || val.Indicator.DisplayName == "STHSurProportionOfModerateIntensityAsc")
                            xlsWorksheet.Cells[rowNumber, 12] = val.DynamicValue;

                        else if (val.Indicator.DisplayName == "STHSurNumberOfIndividualsExaminedHookwor" || val.Indicator.DisplayName == "STHMapSurSurNumberOfIndividualsExaminedHookwor")
                            xlsWorksheet.Cells[rowNumber, 13] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "STHSurNumberOfIndividualsPositiveHookwor" || val.Indicator.DisplayName == "STHMapSurSurNumberOfIndividualsPositiveHookwor")
                            xlsWorksheet.Cells[rowNumber, 14] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "STHSurProportionOfHeavyIntensityHook" || val.Indicator.DisplayName == "STHMapSurSurProportionOfHeavyIntensityOfInHook")
                            xlsWorksheet.Cells[rowNumber, 16] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "STHSurProportionOfModerateIntensityHook" || val.Indicator.DisplayName == "STHMapSurSurProportionOfModerateIntensityOfInHook")
                            xlsWorksheet.Cells[rowNumber, 17] = val.DynamicValue;

                        else if (val.Indicator.DisplayName == "STHSurNumberOfIndividualsExaminedTrichur" || val.Indicator.DisplayName == "STHMapSurSurNumberOfIndividualsExaminedTrichur")
                            xlsWorksheet.Cells[rowNumber, 18] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "STHSurNumberOfIndividualsPositiveTrichur" || val.Indicator.DisplayName == "STHMapSurSurNumberOfIndividualsPositiveTrichur")
                            xlsWorksheet.Cells[rowNumber, 19] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "STHSurProportionOfHeavyIntensityOfTri" || val.Indicator.DisplayName == "STHMapSurSurProportionOfHeavyIntensityOfInfecti")
                            xlsWorksheet.Cells[rowNumber, 21] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "STHSurProportionOfModerateIntensityTri" || val.Indicator.DisplayName == "STHMapSurSurProportionOfModerateIntensityOfInfe")
                            xlsWorksheet.Cells[rowNumber, 22] = val.DynamicValue;

                        else if (val.Indicator.DisplayName == "STHSurNumberOfIndividualsExaminedOverall" || val.Indicator.DisplayName == "STHMapSurSurNumberOfIndividualsExaminedOverall")
                            xlsWorksheet.Cells[rowNumber, 23] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "STHSurNumberOfIndividualsPositiveOverall" || val.Indicator.DisplayName == "STHMapSurSurNumberOfIndividualsPositiveOverall")
                            xlsWorksheet.Cells[rowNumber, 24] = val.DynamicValue;
                    }
                    rowNumber++;
                    rowCount++;
                }
            }
            xlsWorksheet.Cells[3, 5] = rowCount;
        }

        private void AddSch(excel.Worksheet xlsWorksheet, DateTime start, DateTime end)
        {
            SurveyRepository repo = new SurveyRepository();
            int rowCount = 0;
            var surveys = repo.GetByTypeForDistrictsInDateRange(new List<int> { (int)StaticSurveyType.SchistoSentinel, (int)StaticSurveyType.SchMapping }, start, end);
            int rowNumber = 8;

            foreach (SurveyBase survey in surveys.OrderBy(s => s.SortOrder))
            {
                if (survey.TypeOfSurvey.Id == (int)StaticSurveyType.SchistoSentinel)
                {
                    if (survey.HasSentinelSite && survey.SentinelSiteId.HasValue)
                    {
                        var site = repo.GetSiteById(survey.SentinelSiteId.Value);
                        if (site.Lat.HasValue)
                            xlsWorksheet.Cells[rowNumber,4] = Math.Round(site.Lat.Value, 2);
                        if (site.Lng.HasValue)
                            xlsWorksheet.Cells[rowNumber, 5] = Math.Round(site.Lng.Value, 2);
                        xlsWorksheet.Cells[rowNumber, 2] = site.SiteName;

                    }
                    else
                    {
                        if (survey.Lat.HasValue)
                            xlsWorksheet.Cells[rowNumber,4] = Math.Round(survey.Lat.Value, 2);
                        if (survey.Lng.HasValue)
                            xlsWorksheet.Cells[rowNumber, 5] = Math.Round(survey.Lng.Value, 2);
                        xlsWorksheet.Cells[rowNumber, 2] = survey.SpotCheckName;
                    }
                }

                foreach (AdminLevel unit in survey.AdminLevels)
                {
                    xlsWorksheet.Cells[rowNumber, 1] = unit.Name;
                    xlsWorksheet.Cells[rowNumber, 3] = survey.DateReported.ToString("MMMM");
                    foreach (IndicatorValue val in survey.IndicatorValues)
                    {
                        if (val.Indicator.DisplayName == "SCHMapSurSiteNames")
                            xlsWorksheet.Cells[rowNumber, 2] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "SCHMapSurLatitude" && !string.IsNullOrEmpty(val.DynamicValue))
                            xlsWorksheet.Cells[rowNumber, 4] = Math.Round(Convert.ToDouble(val.DynamicValue), 2);
                        else if (val.Indicator.DisplayName == "SCHMapSurLongitude" && !string.IsNullOrEmpty(val.DynamicValue))
                            xlsWorksheet.Cells[rowNumber, 5] = Math.Round(Convert.ToDouble(val.DynamicValue), 2);

                        else if (val.Indicator.DisplayName == "SCHMapSurTestType" || val.Indicator.DisplayName == "SCHSurTestType")
                            xlsWorksheet.Cells[rowNumber, 6] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "SCHMapSurAgeGroupSurveyed" || val.Indicator.DisplayName == "SCHSurAgeGroupSurveyed")
                            xlsWorksheet.Cells[rowNumber, 7] = val.DynamicValue;

                        else if (val.Indicator.DisplayName == "SCHSurNumberOfIndividualsExaminedForUrin" || val.Indicator.DisplayName == "SCHMapSurNumberOfIndividualsExaminedForU")
                            xlsWorksheet.Cells[rowNumber, 8] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "SCHSurNumberOfIndividualsPositiveForHaem" || val.Indicator.DisplayName == "SCHMapSurNumberOfIndividualsPositiveForH")
                            xlsWorksheet.Cells[rowNumber, 9] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "SCHSurProportionOfHeavyIntensityUrinaryS" || val.Indicator.DisplayName == "SCHMapSurProportionOfHeavyIntensityUrina")
                            xlsWorksheet.Cells[rowNumber, 11] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "SCHSurProportionOfModerateIntensityUrina" || val.Indicator.DisplayName == "SCHMapSurProportionOfModerateIntensityUr")
                            xlsWorksheet.Cells[rowNumber, 12] = val.DynamicValue;

                        else if (val.Indicator.DisplayName == "SCHSurNumberOfIndividualsExaminedForInte" || val.Indicator.DisplayName == "SCHMapSurNumberOfIndividualsExaminedForI")
                            xlsWorksheet.Cells[rowNumber, 13] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "SCHSurNumberOfIndividualsPositiveForInte" || val.Indicator.DisplayName == "SCHMapSurNumberOfIndividualsPositiveForI")
                            xlsWorksheet.Cells[rowNumber, 14] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "SCHSurProportionOfHeavyIntensityIntestin" || val.Indicator.DisplayName == "SCHMapSurProportionOfHeavyIntensityIntes")
                            xlsWorksheet.Cells[rowNumber, 16] = val.DynamicValue;
                        else if (val.Indicator.DisplayName == "SCHSurProportionOfModerateIntensityIntes" || val.Indicator.DisplayName == "SCHMapSurProportionOfModerateIntensityIn")
                            xlsWorksheet.Cells[rowNumber, 17] = val.DynamicValue;
                    }
                    rowNumber++;
                    rowCount++;
                }
            }
            xlsWorksheet.Cells[3, 5] = rowCount;
        }

    }
}
