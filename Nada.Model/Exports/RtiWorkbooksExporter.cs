using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using excel = Microsoft.Office.Interop.Excel;

namespace Nada.Model.Exports
{
    public class RtiWorkbooksExporter : ExporterBase, IExporter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AdminLevelType AdminLevelType { get; set; }

        SettingsRepository settings = new SettingsRepository();
        DemoRepository demo = new DemoRepository();
        ExportRepository repo = new ExportRepository();
        IntvRepository intvRepo = new IntvRepository();
        DiseaseRepository diseaseRepo = new DiseaseRepository();
        SurveyRepository surveyRepo = new SurveyRepository();
        int reportYear = DateTime.Now.Year;

        public string ExportName
        {
            get { return Translations.RtiCountryDiseaseWorkbook; }
        }

        public override string GetYear(ExportType exportType)
        {
            var ind = exportType.IndicatorValues.FirstOrDefault(i => i.Indicator.DisplayName == "RtiYearOfWorkbook");
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

                xlsWorkbook = xlsApp.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, @"Exports\Country_Disease_Workbook_v3.2.1_English_.xlsx"),
                    missing, missing, missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing, missing, missing);
                xlsWorkbook.Unprotect("NTDM&E101");

                Country country = demo.GetCountry();
                List<AdminLevel> reportingLevelUnits = new List<AdminLevel>();
                demo.GetAdminLevelTreeForDemography(AdminLevelType.LevelNumber, StartDate, EndDate, ref reportingLevelUnits);
                reportingLevelUnits = reportingLevelUnits.Where(d => d.LevelNumber == AdminLevelType.LevelNumber).OrderBy(a => a.Name).ToList();
                var intvs = intvRepo.GetAll(new List<int> { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 }, StartDate, EndDate);
                Dictionary<int, List<DataRow>> aggIntvs = GetIntvsAggregatedToReportingLevel(StartDate, EndDate, reportingLevelUnits);
                
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["Country"];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddInfo(xlsWorksheet, rng, country, exportType, ref reportYear, reportingLevelUnits.Count, intvs);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["Demography"];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddDemo(xlsWorksheet, rng, reportingLevelUnits);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["LF"];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddLf(xlsWorksheet, rng, StartDate, EndDate, reportingLevelUnits, aggIntvs);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["Oncho"];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddOncho(xlsWorksheet, StartDate, EndDate);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["Schisto"];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddSch(xlsWorksheet, StartDate, EndDate);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["STH"];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddSth(xlsWorksheet, StartDate, EndDate);

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

        private void AddInfo(excel.Worksheet xlsWorksheet, excel.Range rng, Country country, ExportType exportType, ref int year, int districtCount,
            List<IntvDetails> intvs)
        {
            AddValueToRange(xlsWorksheet, rng, "C7", country.Name);
            AddValueToRange(xlsWorksheet, rng, "C10", DateTime.Now.ToString("dd-MMM-yyyy"));
            var i = intvs.FirstOrDefault();
            if (i != null)
            {
                AddValueToRange(xlsWorksheet, rng, "C12", i.StartDate.ToString("dd-MMM-yyyy"));
                var endInd = intvRepo.GetById(i.Id).IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "PcIntvEndDateOfMda");
                if (endInd != null && !string.IsNullOrEmpty(endInd.DynamicValue))
                {
                    DateTime endDate = DateTime.ParseExact(endInd.DynamicValue, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    AddValueToRange(xlsWorksheet, rng, "C13", endDate.ToString("dd-MMM-yyyy"));
                }
            }
            AddValueToRange(xlsWorksheet, rng, "C15", districtCount);
            AddValueToRange(xlsWorksheet, rng, "C19", StartDate.ToString("dd-MMM-yyyy - ") + EndDate.ToString("dd-MMM-yyyy"));

            foreach (var val in exportType.IndicatorValues)
            {
                if (val.Indicator.DisplayName == "RtiYearOfWorkbook")
                {
                    year = Convert.ToInt32(val.DynamicValue);
                    AddValueToRange(xlsWorksheet, rng, "C11", val.DynamicValue);
                }
                if (val.Indicator.DisplayName == "RtiName")
                    AddValueToRange(xlsWorksheet, rng, "C5", val.DynamicValue);
                if (val.Indicator.DisplayName == "RtiTitle")
                    AddValueToRange(xlsWorksheet, rng, "C6", val.DynamicValue);
                if (val.Indicator.DisplayName == "RtiProjectName")
                    AddValueToRange(xlsWorksheet, rng, "C8", val.DynamicValue);
                if (val.Indicator.DisplayName == "RtiSubPartnerName")
                    AddValueToRange(xlsWorksheet, rng, "C9", val.DynamicValue);
                if (val.Indicator.DisplayName == "RtiReportingPeriod")
                    AddValueToRange(xlsWorksheet, rng, "C14", val.DynamicValue);
                if (val.Indicator.DisplayName == "RtiTotalDistrictsTreatedWithUsaid")
                    AddValueToRange(xlsWorksheet, rng, "C16", val.DynamicValue);
                if (val.Indicator.DisplayName == "RtiTotalDistrictsComplete")
                    AddValueToRange(xlsWorksheet, rng, "C17", val.DynamicValue);
                if (val.Indicator.DisplayName == "RtiDataCompleteness")
                    AddValueToRange(xlsWorksheet, rng, "C18", val.DynamicValue);
            }
        }

        private void AddDemo(excel.Worksheet xlsWorksheet, excel.Range rng, List<AdminLevel> demography)
        {
            int rowNum = 17;
            foreach (var demog in demography)
            {
                AddValueToRange(xlsWorksheet, rng, "C" + rowNum, demog.Name);
                AddValueToRange(xlsWorksheet, rng, "B" + rowNum, demog.Parent.Name);
                if (demog.CurrentDemography.TotalPopulation.HasValue)
                    AddValueToRange(xlsWorksheet, rng, "D" + rowNum, demog.CurrentDemography.TotalPopulation.Value);
                if (demog.CurrentDemography.PopPsac.HasValue)
                    AddValueToRange(xlsWorksheet, rng, "E" + rowNum, demog.CurrentDemography.PopPsac.Value);
                if (demog.CurrentDemography.PopSac.HasValue)
                    AddValueToRange(xlsWorksheet, rng, "F" + rowNum, demog.CurrentDemography.PopSac.Value);
                if (demog.CurrentDemography.PopFemale.HasValue)
                    AddValueToRange(xlsWorksheet, rng, "H" + rowNum, demog.CurrentDemography.PopFemale.Value);
                if (demog.CurrentDemography.PopMale.HasValue)
                    AddValueToRange(xlsWorksheet, rng, "G" + rowNum, demog.CurrentDemography.PopMale.Value);
                AddValueToRange(xlsWorksheet, rng, "K" + rowNum, demog.CurrentDemography.Notes);
                rowNum++;
            }
        }

        private void AddLf(excel.Worksheet xlsWorksheet, excel.Range rng, DateTime start, DateTime end, List<AdminLevel> demography, Dictionary<int, List<DataRow>> aggIntvs)
        {
            // Get LF Disease Distributions
            ReportOptions options = new ReportOptions
            {
                MonthYearStarts = start.Month,
                StartDate = start,
                EndDate = end,
                IsCountryAggregation = false,
                IsByLevelAggregation = true,
                IsAllLocations = false,
                IsNoAggregation = false
            };
            options.SelectedAdminLevels = demography;
            DistributionReportGenerator gen = new DistributionReportGenerator();
            DiseaseDistroPc lf = diseaseRepo.Create(DiseaseType.Lf);
            foreach (var indicator in lf.Indicators)
                if (!indicator.Value.IsCalculated)
                    options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(lf.Id, indicator));
            ReportResult ddResult = gen.Run(new SavedReport { ReportOptions = options });
            Dictionary<int, DataRow> lfDd = new Dictionary<int, DataRow>();
            foreach (DataRow dr in ddResult.DataTableResults.Rows)
            {
                int id = 0;
                if (int.TryParse(dr["ID"].ToString(), out id))
                {
                    if (lfDd.ContainsKey(id))
                        lfDd[id] = dr;
                    else
                        lfDd.Add(id, dr);
                }
            }
            // Get Lf Surveys
            var surveys = surveyRepo.GetByTypeForDateRange(
                new List<int> { (int)StaticSurveyType.LfMapping, (int)StaticSurveyType.LfSentinel, (int)StaticSurveyType.LfTas }, StartDate, EndDate);

            int rowCount = 15;
            foreach (var unit in demography)
            {
                // SURVEYS
                var mostRecentSurvey = surveys.Where(s => s.AdminLevels.Select(a => a.Id).Contains(unit.Id)).OrderByDescending(s => s.DateReported).FirstOrDefault();
                if (mostRecentSurvey != null)
                {
                    if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.LfSentinel)
                        AddLfPercentPositive(xlsWorksheet, rng, rowCount, mostRecentSurvey, "LFSurNumberOfIndividualsPositive", "LFSurNumberOfIndividualsExamined");
                    if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.LfMapping)
                        AddLfPercentPositive(xlsWorksheet, rng, rowCount, mostRecentSurvey, "LFMapSurNumberOfIndividualsPositive", "LFMapSurNumberOfIndividualsExamined");
                    AddTypeOfLfSurveySite(xlsWorksheet, rng, rowCount, mostRecentSurvey);
                }
                // DISEASE DISTRO
                if (lfDd.ContainsKey(unit.Id))
                {
                    string endemicity = lfDd[unit.Id][TranslationLookup.GetValue("DDLFDiseaseDistributionPcInterventions") + " - " + lf.Disease.DisplayName].ToString();
                    endemicity = endemicity.Substring(0, endemicity.IndexOf(" - ") + 1);
                    AddValueToRange(xlsWorksheet, rng, "F" + rowCount, endemicity);

                    AddValueToRange(xlsWorksheet, rng, "J" + rowCount,
                        lfDd[unit.Id][TranslationLookup.GetValue("DDLFObjectiveOfPlannedTas") + " - " + lf.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "K" + rowCount,
                        lfDd[unit.Id][TranslationLookup.GetValue("DDLFMonthOfPlannedTas") + " - " + lf.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "L" + rowCount,
                        lfDd[unit.Id][TranslationLookup.GetValue("DDLFYearOfPlannedTas") + " - " + lf.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "M" + rowCount,
                        lfDd[unit.Id][TranslationLookup.GetValue("DDLFPopulationAtRisk") + " - " + lf.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "N" + rowCount,
                        lfDd[unit.Id][TranslationLookup.GetValue("DDLFPopulationRequiringPc") + " - " + lf.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "O" + rowCount,
                        lfDd[unit.Id][TranslationLookup.GetValue("DDLFPopulationLivingInTheDistrictsThatAc") + " - " + lf.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "P" + rowCount,
                        lfDd[unit.Id][TranslationLookup.GetValue("DDLFYearDeterminedThatAchievedCriteriaFo") + " - " + lf.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "T" + rowCount,
                        lfDd[unit.Id][TranslationLookup.GetValue("DDLFYearPcStarted") + " - " + lf.Disease.DisplayName]);

                }
                // INTVS
                if (aggIntvs.ContainsKey(unit.Id))
                {

                }

                rowCount++;
            }

        }

        private void AddOncho(excel.Worksheet xlsWorksheet, DateTime start, DateTime end)
        {
            SurveyRepository repo = new SurveyRepository();
            int rowCount = 0;
            var surveys = repo.GetByTypeForDateRange(new List<int> { (int)StaticSurveyType.OnchoAssessment, (int)StaticSurveyType.OnchoMapping }, start, end);
            int rowNumber = 8;
            foreach (SurveyBase survey in surveys)
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
            var surveys = repo.GetByTypeForDateRange(new List<int> { (int)StaticSurveyType.SthSentinel, (int)StaticSurveyType.SthMapping }, start, end);
            int rowNumber = 8;

            foreach (SurveyBase survey in surveys)
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
                            xlsWorksheet.Cells[rowNumber, 4] = Math.Round(survey.Lat.Value, 2);
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
            var surveys = repo.GetByTypeForDateRange(new List<int> { (int)StaticSurveyType.SchistoSentinel, (int)StaticSurveyType.SchMapping }, start, end);
            int rowNumber = 8;

            foreach (SurveyBase survey in surveys)
            {
                if (survey.TypeOfSurvey.Id == (int)StaticSurveyType.SchistoSentinel)
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
                            xlsWorksheet.Cells[rowNumber, 4] = Math.Round(survey.Lat.Value, 2);
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

        #region Helpers


        private Dictionary<int, List<DataRow>> GetIntvsAggregatedToReportingLevel(DateTime start, DateTime end, List<AdminLevel> units)
        {
            // LF Disease Distributions
            ReportOptions options = new ReportOptions
            {
                MonthYearStarts = start.Month,
                StartDate = start,
                EndDate = end,
                IsCountryAggregation = false,
                IsByLevelAggregation = true,
                IsAllLocations = false,
                IsNoAggregation = false
            };
            options.SelectedAdminLevels = units;
            IntvReportGenerator gen = new IntvReportGenerator();
            var intvIds = new List<int> { 2, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
            foreach (int id in intvIds)
                AddIntvIndicators(options, id);
            ReportResult ddResult = gen.Run(new SavedReport { ReportOptions = options });
            Dictionary<int, List<DataRow>> intvData = new Dictionary<int, List<DataRow>>();
            foreach (DataRow dr in ddResult.DataTableResults.Rows)
            {
                int id = 0;
                if (int.TryParse(dr["ID"].ToString(), out id))
                {
                    if (intvData.ContainsKey(id))
                        intvData[id].Add(dr);
                    else
                        intvData.Add(id, new List<DataRow> { dr });
                }
            }
            return intvData;
        }

        private void AddIntvIndicators(ReportOptions options, int typeId)
        {
            IntvType iType = intvRepo.GetIntvType(typeId);
            foreach (var indicator in iType.Indicators)
                if (!indicator.Value.IsCalculated)
                    options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(iType.Id, indicator));
        }

        private void AddTypeOfLfSurveySite(excel.Worksheet xlsWorksheet, excel.Range rng, int rowCount, SurveyBase mostRecentSurvey)
        {
            string typeName = "";
            if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.LfSentinel)
            {
                var testType = mostRecentSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "LFSurTestType");
                if (testType != null)
                {
                    if (mostRecentSurvey.HasSentinelSite)
                        typeName = "SS ";
                    else
                        typeName = "SC ";
                    if (testType.DynamicValue == "MF")
                        typeName += "(mf)";
                    else
                        typeName += "(Ag)";
                }
            }
            else if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.LfMapping)
            {
                var testType = mostRecentSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "LFMapSurTestType");
                var isSentinel = mostRecentSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "LFMapSurWillTheSitesAlsoServeAsASentin");
                if (testType != null && isSentinel != null)
                {
                    if (isSentinel.DynamicValue == "YesSentinelSite")
                        typeName = "SS ";
                    else
                        typeName = "SC ";
                    if (testType.DynamicValue == "MF")
                        typeName += "(mf)";
                    else
                        typeName += "(Ag)";
                }   
            }
            else
                typeName = "TAS";

            AddValueToRange(xlsWorksheet, rng, "G" + rowCount, typeName);
        }

        private void AddLfPercentPositive(excel.Worksheet xlsWorksheet, excel.Range rng, int rowCount, SurveyBase mostRecentSurvey, string posName, string examinedName)
        {
            var pos = mostRecentSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == posName);
            var examined = mostRecentSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == examinedName);
            if (pos != null && examined != null)
            {
                string percent = CalcBase.GetPercentage(pos.DynamicValue, examined.DynamicValue);
                if (percent != Translations.NA)
                    AddValueToRange(xlsWorksheet, rng, "G" + rowCount, percent);
            }
            AddValueToRange(xlsWorksheet, rng, "H" + rowCount, mostRecentSurvey.DateReported.Year);
        }
        #endregion
    }
}
