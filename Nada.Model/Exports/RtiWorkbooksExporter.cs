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
                var cAdminDemo = demo.GetRecentDemography(1, StartDate, EndDate);
                var countryDemo = demo.GetCountryDemoById(cAdminDemo.Id);
                List<AdminLevel> reportingLevelUnits = demo.GetAdminUnitsWithParentsAndChildren(AdminLevelType.LevelNumber, StartDate, EndDate);
                reportingLevelUnits = reportingLevelUnits.Where(d => d.LevelNumber == AdminLevelType.LevelNumber).OrderBy(a => a.Name).ToList();
                var intvs = intvRepo.GetAll(new List<int> { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 }, StartDate, EndDate);
                Dictionary<int, DataRow> aggIntvs = GetIntvsAggregatedToReportingLevel(StartDate, EndDate, reportingLevelUnits);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["Country"];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddInfo(xlsWorksheet, rng, country, exportType, ref reportYear, reportingLevelUnits.Count, intvs);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["Demography"];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddDemo(xlsWorksheet, rng, reportingLevelUnits, countryDemo);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["LF"];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddLf(xlsWorksheet, rng, StartDate, EndDate, reportingLevelUnits, aggIntvs);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["Oncho"];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddOncho(xlsWorksheet, rng, StartDate, EndDate, reportingLevelUnits, aggIntvs);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["Schisto"];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddSchisto(xlsWorksheet, rng, StartDate, EndDate, reportingLevelUnits, aggIntvs);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["STH"];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddSth(xlsWorksheet, rng, StartDate, EndDate, reportingLevelUnits, aggIntvs);

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

        private void AddDemo(excel.Worksheet xlsWorksheet, excel.Range rng, List<AdminLevel> demography, CountryDemography countryDemo)
        {
            if (countryDemo.YearCensus.HasValue)
                AddValueToRange(xlsWorksheet, rng, "D4", countryDemo.YearCensus.Value);
            if (countryDemo.GrowthRate.HasValue)
                AddValueToRange(xlsWorksheet, rng, "D6", countryDemo.GrowthRate.Value / 100);
            if (countryDemo.PercentFemale.HasValue)
                AddValueToRange(xlsWorksheet, rng, "D7", countryDemo.PercentFemale.Value / 100);
            AddValueToRange(xlsWorksheet, rng, "D8", countryDemo.AgeRangePsac);
            if (countryDemo.PercentPsac.HasValue)
                AddValueToRange(xlsWorksheet, rng, "D9", countryDemo.PercentPsac.Value / 100);
            AddValueToRange(xlsWorksheet, rng, "D10", countryDemo.AgeRangeSac);
            if (countryDemo.PercentSac.HasValue)
                AddValueToRange(xlsWorksheet, rng, "D11", countryDemo.PercentSac.Value / 100);

            int rowNum = 17;
            foreach (var demog in demography)
            {
                AddValueToRange(xlsWorksheet, rng, "C" + rowNum, demog.TaskForceName);
                AddValueToRange(xlsWorksheet, rng, "B" + rowNum, demog.Parent.TaskForceName);
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

        private void AddLf(excel.Worksheet xlsWorksheet, excel.Range rng, DateTime start, DateTime end, List<AdminLevel> demography, Dictionary<int, DataRow> aggIntvs)
        {
            List<string> typeNames = new List<string> { "IntvAlb2", "IntvIvmAlb", "IntvDecAlb", "IntvIvmPzqAlb" };
            List<int> typeIds = new List<int> { (int)StaticIntvType.Alb2, (int)StaticIntvType.IvmAlb, (int)StaticIntvType.DecAlb, (int)StaticIntvType.IvmPzqAlb };
            // Get LF Disease Distributions
            DiseaseDistroPc lf;
            Dictionary<int, DataRow> lfDd;
            GetDdForDisease(start, end, demography, out lf, out lfDd, DiseaseType.Lf);

            // Get Lf Surveys
            var surveys = surveyRepo.GetByTypeForDateRange(
                new List<int> { (int)StaticSurveyType.LfMapping, (int)StaticSurveyType.LfSentinel, (int)StaticSurveyType.LfTas }, StartDate, EndDate);

            int rowCount = 16;
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
                // MDA count
                int mdas = 0, consecutive = 0;
                CountMdas(typeIds, unit.Id, unit.Children.Select(c => c.Id).ToList(), out mdas, out consecutive, "LF");
                AddValueToRange(xlsWorksheet, rng, "U" + rowCount, mdas);
                AddValueToRange(xlsWorksheet, rng, "V" + rowCount, consecutive);

                // INTVS
                if (aggIntvs.ContainsKey(unit.Id))
                {
                    List<string> typesToCalc = new List<string>();
                    if (aggIntvs[unit.Id].Table.Columns.Contains(TranslationLookup.GetValue("LFMMDPNumLymphoedemaPatients") + " - " + TranslationLookup.GetValue("LfMorbidityManagment")))
                        AddValueToRange(xlsWorksheet, rng, "Q" + rowCount,
                            aggIntvs[unit.Id][TranslationLookup.GetValue("LFMMDPNumLymphoedemaPatients") + " - " + TranslationLookup.GetValue("LfMorbidityManagment")]);
                    if (aggIntvs[unit.Id].Table.Columns.Contains(TranslationLookup.GetValue("LFMMDPNumHydroceleCases") + " - " + TranslationLookup.GetValue("LfMorbidityManagment")))
                        AddValueToRange(xlsWorksheet, rng, "R" + rowCount,
                            aggIntvs[unit.Id][TranslationLookup.GetValue("LFMMDPNumHydroceleCases") + " - " + TranslationLookup.GetValue("LfMorbidityManagment")]);
                    AddValueToRange(xlsWorksheet, rng, "AE" + rowCount, GetIntFromRow("PcIntvNumEligibleIndividualsTargeted", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AH" + rowCount, GetIntFromRow("PcIntvNumIndividualsTreated", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AJ" + rowCount, GetIntFromRow("PcIntvPsacTreated", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AL" + rowCount, GetIntFromRow("PcIntvNumSacTreated", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AN" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AP" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AR" + rowCount, GetDropdownFromRow("PcIntvStockOutDuringMda", typesToCalc, aggIntvs[unit.Id], 249, 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AT" + rowCount, GetDropdownFromRow("PcIntvLengthOfStockOut", typesToCalc, aggIntvs[unit.Id], 251, 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AS" + rowCount, GetCombineFromRow("PcIntvStockOutDrug", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BA" + rowCount, GetCombineFromRow("Notes", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));

                    DateTime? startMda = GetDateFromRow("PcIntvStartDateOfMda", typesToCalc, aggIntvs[unit.Id], false, 1, Util.MaxRounds, "LF", typeNames);
                    if (startMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "Y" + rowCount, startMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "Z" + rowCount, startMda.Value.Year);
                    }
                    DateTime? endMda = GetDateFromRow("PcIntvEndDateOfMda", typesToCalc, aggIntvs[unit.Id], true, 1, Util.MaxRounds, "LF", typeNames);
                    if (endMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "AA" + rowCount, endMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "AB" + rowCount, endMda.Value.Year);
                    }
                    AddValueToRange(xlsWorksheet, rng, "X" + rowCount, string.Join(", ", typesToCalc.ToArray()));

                }

                rowCount++;
            }
        }

        private void AddOncho(excel.Worksheet xlsWorksheet, excel.Range rng, DateTime start, DateTime end, List<AdminLevel> demography, Dictionary<int, DataRow> aggIntvs)
        {
            List<string> typeNames = new List<string> { "IntvIvmPzqAlb", "IntvIvmPzq", "IntvIvmAlb", "IntvIvm" };
            List<int> typeIds = new List<int> { (int)StaticIntvType.IvmPzqAlb, (int)StaticIntvType.IvmPzq, (int)StaticIntvType.IvmAlb, (int)StaticIntvType.Ivm };
            // Get ONCHO Disease Distributions
            DiseaseDistroPc oncho;
            Dictionary<int, DataRow> onchoDd;
            GetDdForDisease(start, end, demography, out oncho, out onchoDd, DiseaseType.Oncho);

            // Get ONCHO Surveys
            var surveys = surveyRepo.GetByTypeForDateRange(
                new List<int> { (int)StaticSurveyType.OnchoMapping, (int)StaticSurveyType.OnchoAssessment }, StartDate, EndDate);
            int maxLvl = 0;
            foreach (var s in surveys)
            {
                IndicatorValue posVal = new IndicatorValue { DynamicValue = null };
                if (s.TypeOfSurvey.Id == (int)StaticSurveyType.OnchoMapping)
                    posVal = s.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "OnchoMapSurNumberOfIndividualsPositive");
                else
                    posVal = s.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "OnchoSurNumberOfIndividualsPositive");

                if (!string.IsNullOrEmpty(posVal.DynamicValue) && maxLvl < s.AdminLevels.Max(a => a.LevelNumber))
                    maxLvl = s.AdminLevels.Max(a => a.LevelNumber);
            }
            var level = settings.GetAdminLevelTypeByLevel(maxLvl);

            int rowCount = 16;
            foreach (var unit in demography)
            {
                // SURVEYS
                if (level != null && !string.IsNullOrEmpty(level.DisplayName))
                    AddValueToRange(xlsWorksheet, rng, "F" + rowCount, level.DisplayName);
                //var mostRecentSurvey = surveys.Where(s => s.AdminLevels.Select(a => a.Id).Contains(unit.Id)).OrderByDescending(s => s.DateReported).FirstOrDefault();
                //if (mostRecentSurvey != null)
                //{
                //    if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.LfSentinel)
                //        AddLfPercentPositive(xlsWorksheet, rng, rowCount, mostRecentSurvey, "LFSurNumberOfIndividualsPositive", "LFSurNumberOfIndividualsExamined");
                //    if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.LfMapping)
                //        AddLfPercentPositive(xlsWorksheet, rng, rowCount, mostRecentSurvey, "LFMapSurNumberOfIndividualsPositive", "LFMapSurNumberOfIndividualsExamined");
                //    AddTypeOfLfSurveySite(xlsWorksheet, rng, rowCount, mostRecentSurvey);
                //}

                // DISEASE DISTRO
                if (onchoDd.ContainsKey(unit.Id))
                {
                    string endemicity = onchoDd[unit.Id][TranslationLookup.GetValue("DDOnchoDiseaseDistributionPcInterventio") + " - " + oncho.Disease.DisplayName].ToString();
                    endemicity = endemicity.Substring(0, endemicity.IndexOf(" - ") + 1);
                    AddValueToRange(xlsWorksheet, rng, "G" + rowCount, endemicity);
                    AddValueToRange(xlsWorksheet, rng, "K" + rowCount,
                        onchoDd[unit.Id][TranslationLookup.GetValue("DDOnchoPopulationAtRisk") + " - " + oncho.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "L" + rowCount,
                        onchoDd[unit.Id][TranslationLookup.GetValue("DDOnchoPopulationRequiringPc") + " - " + oncho.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "M" + rowCount,
                        onchoDd[unit.Id][TranslationLookup.GetValue("DDOnchoPopulationLivingInTheDistrictsTha") + " - " + oncho.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "N" + rowCount,
                        onchoDd[unit.Id][TranslationLookup.GetValue("DDOnchoYearDeterminedThatAchievedCriteri") + " - " + oncho.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "O" + rowCount,
                        onchoDd[unit.Id][TranslationLookup.GetValue("DDOnchoNumPcRoundsYearCurrentlyImplemen") + " - " + oncho.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "Q" + rowCount,
                        onchoDd[unit.Id][TranslationLookup.GetValue("DDOnchoYearPcStarted") + " - " + oncho.Disease.DisplayName]);
                }

                // MDA count
                int mdas = 0, consecutive = 0;
                CountMdas(typeIds, unit.Id, unit.Children.Select(c => c.Id).ToList(), out mdas, out consecutive, "Oncho");
                AddValueToRange(xlsWorksheet, rng, "R" + rowCount, mdas);
                AddValueToRange(xlsWorksheet, rng, "S" + rowCount, consecutive);

                // INTVS
                if (aggIntvs.ContainsKey(unit.Id))
                {
                    // ROUND 1
                    List<string> typesToCalc = new List<string>();
                    DateTime? startMda = GetDateFromRow("PcIntvStartDateOfMda", typesToCalc, aggIntvs[unit.Id], false, 1, 1, "Oncho", typeNames);
                    if (startMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "V" + rowCount, startMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "W" + rowCount, startMda.Value.Year);
                    }
                    DateTime? endMda = GetDateFromRow("PcIntvEndDateOfMda", typesToCalc, aggIntvs[unit.Id], true, 1, 1, "Oncho", typeNames);
                    if (endMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "X" + rowCount, endMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "Y" + rowCount, endMda.Value.Year);
                    }
                    AddValueToRange(xlsWorksheet, rng, "AB" + rowCount, GetIntFromRow("PcIntvNumEligibleIndividualsTargeted", typesToCalc, aggIntvs[unit.Id], 1, 1, "Oncho", typeNames, "PcIntvOfTotalTargetedForOncho"));
                    AddValueToRange(xlsWorksheet, rng, "AE" + rowCount, GetIntFromRow("PcIntvNumIndividualsTreated", typesToCalc, aggIntvs[unit.Id], 1, 1, "Oncho", typeNames, "PcIntvOfTotalTreatedForOncho"));
                    AddValueToRange(xlsWorksheet, rng, "AG" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", typesToCalc, aggIntvs[unit.Id], 1, 1, "Oncho", typeNames, "PcIntvOfTotalFemalesOncho"));
                    AddValueToRange(xlsWorksheet, rng, "AI" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", typesToCalc, aggIntvs[unit.Id], 1, 1, "Oncho", typeNames, "PcIntvOfTotalMalesOncho"));
                    AddValueToRange(xlsWorksheet, rng, "AK" + rowCount, GetDropdownFromRow("PcIntvStockOutDuringMda", typesToCalc, aggIntvs[unit.Id], 249, 1, 1, "Oncho", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AM" + rowCount, GetDropdownFromRow("PcIntvLengthOfStockOut", typesToCalc, aggIntvs[unit.Id], 251, 1, 1, "Oncho", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AL" + rowCount, GetCombineFromRow("PcIntvStockOutDrug", typesToCalc, aggIntvs[unit.Id], 1, 1, "Oncho", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "U" + rowCount, string.Join(", ", typesToCalc.ToArray()));

                    // ROUND 2
                    typesToCalc = new List<string>();
                    startMda = GetDateFromRow("PcIntvStartDateOfMda", typesToCalc, aggIntvs[unit.Id], false, 2, 2, "Oncho", typeNames);
                    if (startMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "AU" + rowCount, startMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "AV" + rowCount, startMda.Value.Year);
                    }
                    endMda = GetDateFromRow("PcIntvEndDateOfMda", typesToCalc, aggIntvs[unit.Id], true, 2, 2, "Oncho", typeNames);
                    if (endMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "AW" + rowCount, endMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "AX" + rowCount, endMda.Value.Year);
                    }
                    AddValueToRange(xlsWorksheet, rng, "BA" + rowCount, GetIntFromRow("PcIntvNumEligibleIndividualsTargeted", typesToCalc, aggIntvs[unit.Id], 2, 2, "Oncho", typeNames, "PcIntvOfTotalTargetedForOncho"));
                    AddValueToRange(xlsWorksheet, rng, "BD" + rowCount, GetIntFromRow("PcIntvNumIndividualsTreated", typesToCalc, aggIntvs[unit.Id], 2, 2, "Oncho", typeNames, "PcIntvOfTotalTreatedForOncho"));
                    AddValueToRange(xlsWorksheet, rng, "BF" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", typesToCalc, aggIntvs[unit.Id], 2, 2, "Oncho", typeNames, "PcIntvOfTotalFemalesOncho"));
                    AddValueToRange(xlsWorksheet, rng, "BH" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", typesToCalc, aggIntvs[unit.Id], 2, 2, "Oncho", typeNames, "PcIntvOfTotalMalesOncho"));
                    AddValueToRange(xlsWorksheet, rng, "BJ" + rowCount, GetDropdownFromRow("PcIntvStockOutDuringMda", typesToCalc, aggIntvs[unit.Id], 249, 2, 2, "Oncho", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BL" + rowCount, GetDropdownFromRow("PcIntvLengthOfStockOut", typesToCalc, aggIntvs[unit.Id], 252, 2, 1, "Oncho", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BK" + rowCount, GetCombineFromRow("PcIntvStockOutDrug", typesToCalc, aggIntvs[unit.Id], 2, 2, "Oncho", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AT" + rowCount, string.Join(", ", typesToCalc.ToArray()));

                    AddValueToRange(xlsWorksheet, rng, "BS" + rowCount, GetCombineFromRow("Notes", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Oncho", typeNames));

                }

                rowCount++;
            }
        }

        private void AddSchisto(excel.Worksheet xlsWorksheet, excel.Range rng, DateTime start, DateTime end, List<AdminLevel> demography, Dictionary<int, DataRow> aggIntvs)
        {
            List<string> typeNames = new List<string> { "IntvIvmPzqAlb", "IntvIvmPzq", "IntvPzq", "IntvPzqAlb", "IntvPzqMbd" };
            List<int> typeIds = new List<int> { (int)StaticIntvType.IvmPzqAlb, (int)StaticIntvType.IvmPzq, (int)StaticIntvType.Pzq, (int)StaticIntvType.PzqAlb, (int)StaticIntvType.PzqMbd };
            // Get sch Disease Distributions
            DiseaseDistroPc sch;
            Dictionary<int, DataRow> schDd;
            GetDdForDisease(start, end, demography, out sch, out schDd, DiseaseType.Schisto);

            // Get sch Surveys
            var surveys = surveyRepo.GetByTypeForDateRange(
                new List<int> { (int)StaticSurveyType.SchistoSentinel, (int)StaticSurveyType.SchMapping }, StartDate, EndDate);
            int maxLvl = 0;
            foreach (var s in surveys)
            {
                if (maxLvl < s.AdminLevels.Max(a => a.LevelNumber))
                    maxLvl = s.AdminLevels.Max(a => a.LevelNumber);
            }
            var level = settings.GetAdminLevelTypeByLevel(maxLvl);

            int rowCount = 18;
            foreach (var unit in demography)
            {
                // SURVEYS
                if (level != null && !string.IsNullOrEmpty(level.DisplayName))
                    AddValueToRange(xlsWorksheet, rng, "G" + rowCount, level.DisplayName);
                var mostRecentSurvey = surveys.Where(s => s.AdminLevels.Select(a => a.Id).Contains(unit.Id)).OrderByDescending(s => s.DateReported).FirstOrDefault();
                if (mostRecentSurvey != null)
                {
                    AddValueToRange(xlsWorksheet, rng, "K" + rowCount, mostRecentSurvey.DateReported.Year);
                    if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.SchistoSentinel)
                    {
                        AddIndicatorToRange(xlsWorksheet, rng, rowCount, mostRecentSurvey, "SCHSurPrevalenceOfIntestinalSchistosomeI", "I");
                        AddIndicatorToRange(xlsWorksheet, rng, rowCount, mostRecentSurvey, "SCHSurProportionOfHeavyIntensityIntestin", "J");
                        AddIndicatorToRange(xlsWorksheet, rng, rowCount, mostRecentSurvey, "SCHSurTestType", "L");
                    }
                    else if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.SchMapping)
                    {
                        AddIndicatorToRange(xlsWorksheet, rng, rowCount, mostRecentSurvey, "SCHMapSurPrevalenceOfIntestinalSchistoso", "I");
                        AddIndicatorToRange(xlsWorksheet, rng, rowCount, mostRecentSurvey, "SCHMapSurProportionOfHeavyIntensityIntes", "J");
                        AddIndicatorToRange(xlsWorksheet, rng, rowCount, mostRecentSurvey, "SCHMapSurTestType", "L");
                    }
                }

                // DISEASE DISTRO
                if (schDd.ContainsKey(unit.Id))
                {
                    string endemicity = schDd[unit.Id][TranslationLookup.GetValue("DDSchistoDiseaseDistributionPcIntervent") + " - " + sch.Disease.DisplayName].ToString();
                    endemicity = endemicity.Substring(0, endemicity.IndexOf(" - ") + 1);
                    AddValueToRange(xlsWorksheet, rng, "H" + rowCount, endemicity);
                    AddValueToRange(xlsWorksheet, rng, "P" + rowCount,
                        schDd[unit.Id][TranslationLookup.GetValue("DDSchistoPopulationAtRisk") + " - " + sch.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "Q" + rowCount,
                        schDd[unit.Id][TranslationLookup.GetValue("DDSchistoPopulationRequiringPc") + " - " + sch.Disease.DisplayName]);

                    AddValueToRange(xlsWorksheet, rng, "R" + rowCount,
                       schDd[unit.Id][TranslationLookup.GetValue("DDSchistoSacAtRisk") + " - " + sch.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "S" + rowCount,
                       schDd[unit.Id][TranslationLookup.GetValue("DDSchistoHighRiskAdultsAtRisk") + " - " + sch.Disease.DisplayName]);

                    AddValueToRange(xlsWorksheet, rng, "T" + rowCount,
                        schDd[unit.Id][TranslationLookup.GetValue("DDSchistoPopulationLivingInTheDistrictsT") + " - " + sch.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "U" + rowCount,
                        schDd[unit.Id][TranslationLookup.GetValue("DDSchistoYearDeterminedThatAchievedCrite") + " - " + sch.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "V" + rowCount,
                        schDd[unit.Id][TranslationLookup.GetValue("DDSchistoNumPcRoundsYearRecommendedByWh") + " - " + sch.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "W" + rowCount,
                        schDd[unit.Id][TranslationLookup.GetValue("DDSchistoNumPcRoundsYearCurrentlyImplem") + " - " + sch.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "Y" + rowCount,
                        schDd[unit.Id][TranslationLookup.GetValue("DDSchistoYearPcStarted") + " - " + sch.Disease.DisplayName]);
                }
                // MDA count
                int mdas = 0, consecutive = 0;
                CountMdas(typeIds, unit.Id, unit.Children.Select(c => c.Id).ToList(), out mdas, out consecutive, "Schisto");
                AddValueToRange(xlsWorksheet, rng, "Z" + rowCount, mdas);
                //AddValueToRange(xlsWorksheet, rng, "S" + rowCount, consecutive);
                // INTVS
                if (aggIntvs.ContainsKey(unit.Id))
                {
                    List<string> typesToCalc = new List<string>();
                    DateTime? startMda = GetDateFromRow("PcIntvStartDateOfMda", typesToCalc, aggIntvs[unit.Id], false, 1, Util.MaxRounds, "Schisto", typeNames);
                    if (startMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "AC" + rowCount, startMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "AD" + rowCount, startMda.Value.Year);
                    }
                    DateTime? endMda = GetDateFromRow("PcIntvEndDateOfMda", typesToCalc, aggIntvs[unit.Id], true, 1, Util.MaxRounds, "Schisto", typeNames);
                    if (endMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "AE" + rowCount, endMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "AF" + rowCount, endMda.Value.Year);
                    }
                    AddValueToRange(xlsWorksheet, rng, "AI" + rowCount, GetIntFromRow("PcIntvNumEligibleIndividualsTargeted", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AN" + rowCount, GetIntFromRow("PcIntvNumIndividualsTreated", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AP" + rowCount, GetIntFromRow("PcIntvNumSacTreated", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AR" + rowCount, GetIntFromRow("PcIntvNumAdultsTreated", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AT" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AV" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AX" + rowCount, GetDropdownFromRow("PcIntvStockOutDuringMda", typesToCalc, aggIntvs[unit.Id], 249, 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AZ" + rowCount, GetDropdownFromRow("PcIntvLengthOfStockOut", typesToCalc, aggIntvs[unit.Id], 251, 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AY" + rowCount, GetCombineFromRow("PcIntvStockOutDrug", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));

                    AddValueToRange(xlsWorksheet, rng, "AB" + rowCount, string.Join(", ", typesToCalc.ToArray()));
                    AddValueToRange(xlsWorksheet, rng, "BG" + rowCount, GetCombineFromRow("Notes", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));
                }

                rowCount++;
            }
        }

        private void AddSth(excel.Worksheet xlsWorksheet, excel.Range rng, DateTime start, DateTime end, List<AdminLevel> demography, Dictionary<int, DataRow> aggIntvs)
        {
            List<string> typeNames = new List<string> { "IntvIvmPzqAlb", "IntvIvmAlb", "IntvDecAlb", "IntvPzqAlb", "IntvPzqMbd", "IntvAlb", "IntvMbd" };
            List<int> typeIds = new List<int> { (int)StaticIntvType.IvmPzqAlb, (int)StaticIntvType.IvmAlb, (int)StaticIntvType.DecAlb, (int)StaticIntvType.PzqAlb, (int)StaticIntvType.PzqMbd, (int)StaticIntvType.Alb, (int)StaticIntvType.Mbd };
            // Get sch Disease Distributions
            DiseaseDistroPc sth;
            Dictionary<int, DataRow> sthDd;
            GetDdForDisease(start, end, demography, out sth, out sthDd, DiseaseType.Schisto);

            // Get sch Surveys
            var surveys = surveyRepo.GetByTypeForDateRange(
                new List<int> { (int)StaticSurveyType.SthMapping, (int)StaticSurveyType.SthSentinel }, StartDate, EndDate);
            int maxLvl = 0;
            foreach (var s in surveys)
            {
                if (maxLvl < s.AdminLevels.Max(a => a.LevelNumber))
                    maxLvl = s.AdminLevels.Max(a => a.LevelNumber);
            }
            var level = settings.GetAdminLevelTypeByLevel(maxLvl);

            int rowCount = 18;
            foreach (var unit in demography)
            {
                // SURVEYS
                if (level != null && !string.IsNullOrEmpty(level.DisplayName))
                    AddValueToRange(xlsWorksheet, rng, "G" + rowCount, level.DisplayName);
                var mostRecentSurvey = surveys.Where(s => s.AdminLevels.Select(a => a.Id).Contains(unit.Id)).OrderByDescending(s => s.DateReported).FirstOrDefault();
                if (mostRecentSurvey != null)
                {
                    AddValueToRange(xlsWorksheet, rng, "K" + rowCount, mostRecentSurvey.DateReported.Year);
                    if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.SthSentinel)
                    {
                        AddIndicatorToRange(xlsWorksheet, rng, rowCount, mostRecentSurvey, "STHSurPositiveOverall", "I");
                        AddIndicatorToRange(xlsWorksheet, rng, rowCount, mostRecentSurvey, "STHSurOverallProportionOfHeavyIntensity", "J");
                        AddIndicatorToRange(xlsWorksheet, rng, rowCount, mostRecentSurvey, "STHSurTestType", "L");
                    }
                    else if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.SthMapping)
                    {
                        AddIndicatorToRange(xlsWorksheet, rng, rowCount, mostRecentSurvey, "STHMapSurSurPerPositiveOverall", "I");
                        AddIndicatorToRange(xlsWorksheet, rng, rowCount, mostRecentSurvey, "STHMapSurSurOverallProportionOfHeavyIntensity", "J");
                        AddIndicatorToRange(xlsWorksheet, rng, rowCount, mostRecentSurvey, "STHMapSurSurTestType", "L");
                    }
                }

                // DISEASE DISTRO
                if (sthDd.ContainsKey(unit.Id))
                {
                    string endemicity = sthDd[unit.Id][TranslationLookup.GetValue("DDSTHDiseaseDistributionPcInterventions") + " - " + sth.Disease.DisplayName].ToString();
                    endemicity = endemicity.Substring(0, endemicity.IndexOf(" - ") + 1);
                    AddValueToRange(xlsWorksheet, rng, "H" + rowCount, endemicity);
                    AddValueToRange(xlsWorksheet, rng, "P" + rowCount,
                        sthDd[unit.Id][TranslationLookup.GetValue("DDSTHPopulationAtRisk") + " - " + sth.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "Q" + rowCount,
                        sthDd[unit.Id][TranslationLookup.GetValue("DDSTHPopulationRequiringPc") + " - " + sth.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "R" + rowCount,
                       sthDd[unit.Id][TranslationLookup.GetValue("DDSTHSacAtRisk") + " - " + sth.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "S" + rowCount,
                       sthDd[unit.Id][TranslationLookup.GetValue("DDSTHHighRiskAdultsAtRisk") + " - " + sth.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "T" + rowCount,
                       sthDd[unit.Id][TranslationLookup.GetValue("DDSTHPopulationLivingInTheDistrictsThatA") + " - " + sth.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "U" + rowCount,
                        sthDd[unit.Id][TranslationLookup.GetValue("DDSTHYearDeterminedThatAchievedCriteriaF") + " - " + sth.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "V" + rowCount,
                        sthDd[unit.Id][TranslationLookup.GetValue("DDSTHNumPcRoundsYearRecommendedByWhoGui") + " - " + sth.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "W" + rowCount,
                        sthDd[unit.Id][TranslationLookup.GetValue("DDSTHNumPcRoundsYearCurrentlyImplemente") + " - " + sth.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "Y" + rowCount,
                        sthDd[unit.Id][TranslationLookup.GetValue("DDSTHYearPcStarted") + " - " + sth.Disease.DisplayName]);
                }
                // MDA count
                int mdas = 0, consecutive = 0;
                CountMdas(typeIds, unit.Id, unit.Children.Select(c => c.Id).ToList(), out mdas, out consecutive, "STH");
                AddValueToRange(xlsWorksheet, rng, "AA" + rowCount, mdas);
                AddValueToRange(xlsWorksheet, rng, "AB" + rowCount, consecutive);
                // INTVS
                if (aggIntvs.ContainsKey(unit.Id))
                {
                    // ROUND 1
                    List<string> typesToCalc = new List<string>();
                    DateTime? startMda = GetDateFromRow("PcIntvStartDateOfMda", typesToCalc, aggIntvs[unit.Id], false, 1, 1, "STH", typeNames);
                    if (startMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "AE" + rowCount, startMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "AF" + rowCount, startMda.Value.Year);
                    }
                    DateTime? endMda = GetDateFromRow("PcIntvEndDateOfMda", typesToCalc, aggIntvs[unit.Id], true, 1, 1, "STH", typeNames);
                    if (endMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "AG" + rowCount, endMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "AH" + rowCount, endMda.Value.Year);
                    }
                    AddValueToRange(xlsWorksheet, rng, "AK" + rowCount, GetIntFromRow("PcIntvNumEligibleIndividualsTargeted", typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AQ" + rowCount, GetIntFromRow("PcIntvNumIndividualsTreated", typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AS" + rowCount, GetIntFromRow("PcIntvPsacTreated", typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AU" + rowCount, GetIntFromRow("PcIntvNumSacTreated", typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AW" + rowCount, GetIntFromRow("PcIntvNumAdultsTreated", typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AY" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BA" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BC" + rowCount, GetDropdownFromRow("PcIntvStockOutDuringMda", typesToCalc, aggIntvs[unit.Id], 249, 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BE" + rowCount, GetDropdownFromRow("PcIntvLengthOfStockOut", typesToCalc, aggIntvs[unit.Id], 251, 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BD" + rowCount, GetCombineFromRow("PcIntvStockOutDrug", typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AD" + rowCount, string.Join(", ", typesToCalc.ToArray()));

                    // ROUND 2
                    typesToCalc = new List<string>();
                    startMda = GetDateFromRow("PcIntvStartDateOfMda", typesToCalc, aggIntvs[unit.Id], false, 2, 2, "STH", typeNames);
                    if (startMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "BM" + rowCount, startMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "BN" + rowCount, startMda.Value.Year);
                    }
                    endMda = GetDateFromRow("PcIntvEndDateOfMda", typesToCalc, aggIntvs[unit.Id], true, 2, 2, "STH", typeNames);
                    if (endMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "BO" + rowCount, endMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "BP" + rowCount, endMda.Value.Year);
                    }
                    AddValueToRange(xlsWorksheet, rng, "BS" + rowCount, GetIntFromRow("PcIntvNumEligibleIndividualsTargeted", typesToCalc, aggIntvs[unit.Id], 2, 2, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BY" + rowCount, GetIntFromRow("PcIntvNumIndividualsTreated", typesToCalc, aggIntvs[unit.Id], 2, 2, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CA" + rowCount, GetIntFromRow("PcIntvPsacTreated", typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CC" + rowCount, GetIntFromRow("PcIntvNumSacTreated", typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CE" + rowCount, GetIntFromRow("PcIntvNumAdultsTreated", typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CG" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", typesToCalc, aggIntvs[unit.Id], 2, 2, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CI" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", typesToCalc, aggIntvs[unit.Id], 2, 2, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CK" + rowCount, GetDropdownFromRow("PcIntvStockOutDuringMda", typesToCalc, aggIntvs[unit.Id], 249, 2, 2, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CM" + rowCount, GetDropdownFromRow("PcIntvLengthOfStockOut", typesToCalc, aggIntvs[unit.Id], 252, 2, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CL" + rowCount, GetCombineFromRow("PcIntvStockOutDrug", typesToCalc, aggIntvs[unit.Id], 2, 2, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BL" + rowCount, string.Join(", ", typesToCalc.ToArray()));

                    AddValueToRange(xlsWorksheet, rng, "CT" + rowCount, GetCombineFromRow("Notes", typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "STH", typeNames));
                }

                rowCount++;
            }
        }

        #region Helpers
        private void CountMdas(List<int> types, int parentId, List<int> childrenIds, out int mdas, out int consecutive, string diseaseName)
        {
            mdas = 0;
            consecutive = 0;
            childrenIds.Add(parentId);
            var intvs = intvRepo.GetAll(types, childrenIds);
            DateTime? lastDate = null;
            foreach (var i in intvs)
            {
                if (!i.ValueDictionary.ContainsKey("PcIntvDiseases") || !i.ValueDictionary["PcIntvDiseases"].DynamicValue.Contains(diseaseName))
                    return;

                mdas++;

                if (!lastDate.HasValue || lastDate.Value.MonthDifference(i.StartDate) > 15)
                    consecutive = 0;
                lastDate = i.StartDate;
                consecutive++;
            }
        }

        private void GetDdForDisease(DateTime start, DateTime end, List<AdminLevel> demography, out DiseaseDistroPc ddType, out Dictionary<int, DataRow> dd, DiseaseType dType)
        {
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
            ddType = diseaseRepo.Create(dType);
            foreach (var indicator in ddType.Indicators.Where(i => i.Value.DataTypeId != (int)IndicatorDataType.Calculated))
                options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(ddType.Id, indicator));
            ReportResult ddResult = gen.Run(new SavedReport { ReportOptions = options });
            dd = new Dictionary<int, DataRow>();
            foreach (DataRow dr in ddResult.DataTableResults.Rows)
            {
                int id = 0;
                if (int.TryParse(dr["ID"].ToString(), out id))
                {
                    if (dd.ContainsKey(id))
                        dd[id] = dr;
                    else
                        dd.Add(id, dr);
                }
            }
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

        private void AddIndicatorToRange(excel.Worksheet xlsWorksheet, excel.Range rng, int rowCount, SurveyBase mostRecentSurvey, string indName, string colName)
        {
            var ind = mostRecentSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == indName);
            if (ind != null && !string.IsNullOrEmpty(ind.DynamicValue))
                AddValueToRange(xlsWorksheet, rng, colName + rowCount, ind.DynamicValue);
        }
        #endregion
    }
}
