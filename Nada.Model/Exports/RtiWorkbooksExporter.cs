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
        public CultureInfo ExportCulture { get; set; }

        SettingsRepository settings = new SettingsRepository();
        DemoRepository demo = new DemoRepository();
        ExportRepository repo = new ExportRepository();
        IntvRepository intvRepo = new IntvRepository();
        DiseaseRepository diseaseRepo = new DiseaseRepository();
        SurveyRepository surveyRepo = new SurveyRepository();
        TranslationLookupInstance transLookup = null;

        public string ExportName
        {
            get { return Translations.RtiCountryDiseaseWorkbook; }
        }

        public override string GetYear(ExportType exportType)
        {
            return StartDate.Year.ToString() + "-" + EndDate.Year.ToString();
        }

        public ExportResult DoExport(string fileName, int userId, ExportType exportType)
        {
            try
            {
                transLookup = new TranslationLookupInstance(ExportCulture);
                System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                excel.Application xlsApp = new excel.ApplicationClass();
                xlsApp.DisplayAlerts = false;
                excel.Workbook xlsWorkbook;
                excel.Worksheet xlsWorksheet;
                excel.Range rng = null;
                object missing = System.Reflection.Missing.Value;

                xlsWorkbook = xlsApp.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, @"Exports\" + transLookup.GetValue("RtiWorkbookLocation", "RtiWorkbookLocation")),
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


                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets[transLookup.GetValue("RtiTabCountry", "RtiTabCountry")];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddInfo(xlsWorksheet, rng, country, exportType, reportingLevelUnits.Count, intvs);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets[transLookup.GetValue("RtiTabDemo", "RtiTabDemo")];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddDemo(xlsWorksheet, rng, reportingLevelUnits, countryDemo);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets[transLookup.GetValue("RtiTabLf", "RtiTabLf")];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddLf(xlsWorksheet, rng, StartDate, EndDate, reportingLevelUnits, aggIntvs);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets[transLookup.GetValue("RtiTabOncho", "RtiTabOncho")];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddOncho(xlsWorksheet, rng, StartDate, EndDate, reportingLevelUnits, aggIntvs);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets[transLookup.GetValue("RtiTabSch", "RtiTabSch")];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddSchisto(xlsWorksheet, rng, StartDate, EndDate, reportingLevelUnits, aggIntvs);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets[transLookup.GetValue("RtiTabSth", "RtiTabSth")];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddSth(xlsWorksheet, rng, StartDate, EndDate, reportingLevelUnits, aggIntvs);

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets[transLookup.GetValue("RtiTabTra", "RtiTabTra")];
                xlsWorksheet.Unprotect("NTDM&E101");
                AddTrachoma(xlsWorksheet, rng, StartDate, EndDate, reportingLevelUnits, aggIntvs);

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

        private void AddInfo(excel.Worksheet xlsWorksheet, excel.Range rng, Country country, ExportType exportType, int districtCount,
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
                    AddValueToRange(xlsWorksheet, rng, "C11", transLookup.GetValue(val.DynamicValue, val.DynamicValue));
                if (val.Indicator.DisplayName == "RtiName")
                    AddValueToRange(xlsWorksheet, rng, "C5", val.DynamicValue);
                if (val.Indicator.DisplayName == "RtiTitle")
                    AddValueToRange(xlsWorksheet, rng, "C6", val.DynamicValue);
                if (val.Indicator.DisplayName == "RtiProjectName")
                    AddValueToRange(xlsWorksheet, rng, "C8", transLookup.GetValue(val.DynamicValue, val.DynamicValue));
                if (val.Indicator.DisplayName == "RtiSubPartnerName")
                    AddValueToRange(xlsWorksheet, rng, "C9", val.DynamicValue);
                if (val.Indicator.DisplayName == "RtiReportingPeriod")
                    AddValueToRange(xlsWorksheet, rng, "C14", transLookup.GetValue(val.DynamicValue, val.DynamicValue));
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
            // Get LF Surveys 
            var surveys = surveyRepo.GetByTypeInDateRange(
                new List<int> { (int)StaticSurveyType.LfMapping, (int)StaticSurveyType.LfSentinel, (int)StaticSurveyType.LfTas }, StartDate, EndDate);
            
            // Get LF Disease Distributions
            DiseaseDistroPc lf;
            Dictionary<int, DataRow> lfDd;
            GetDdForDisease(start, end, demography, out lf, out lfDd, DiseaseType.Lf);

            int rowCount = 16;
            foreach (var unit in demography)
            {
                // Surveys
                var mostRecentSurvey = surveys.Where(s => s.AdminLevels.Select(a => a.Id).Contains(unit.Id)).OrderByDescending(s => s.DateReported).FirstOrDefault();
                if (mostRecentSurvey != null)
                {
                    string percent = null;
                    int year = 0;
                    if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.LfSentinel)
                        percent = GetPercentPositive(mostRecentSurvey, "LFSurNumberOfIndividualsPositive", "LFSurNumberOfIndividualsExamined", out year);
                    else if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.LfMapping)
                        percent = GetPercentPositive(mostRecentSurvey, "LFMapSurNumberOfIndividualsPositive", "LFMapSurNumberOfIndividualsExamined", out year);
                    if (mostRecentSurvey.TypeOfSurvey.Id != (int)StaticSurveyType.LfTas)
                    {
                        AddValueToRange(xlsWorksheet, rng, "G" + rowCount, percent);
                        AddValueToRange(xlsWorksheet, rng, "H" + rowCount, year);
                    }
                    AddTypeOfLfSurveySite(xlsWorksheet, rng, rowCount, mostRecentSurvey);
                }

                // DISEASE DISTRO
                if (lfDd.ContainsKey(unit.Id))
                {
                    string endemicity = ParseLfDdEnd(lfDd[unit.Id][TranslationLookup.GetValue("DDLFDiseaseDistributionPcInterventions") + " - " + lf.Disease.DisplayName].ToString());
                    AddValueToRange(xlsWorksheet, rng, "F" + rowCount, endemicity);

                    AddValueToRange(xlsWorksheet, rng, "J" + rowCount,
                        ParseTasObjective(lfDd[unit.Id][TranslationLookup.GetValue("DDLFObjectiveOfPlannedTas") + " - " + lf.Disease.DisplayName].ToString()));
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
                    AddValueToRange(xlsWorksheet, rng, "AE" + rowCount, GetIntFromRow("PcIntvNumEligibleIndividualsTargeted", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AH" + rowCount, GetIntFromRow("PcIntvNumIndividualsTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AJ" + rowCount, GetIntFromRow("PcIntvPsacTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AL" + rowCount, GetIntFromRow("PcIntvNumSacTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AN" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AP" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AR" + rowCount, GetDropdownFromRow("PcIntvStockOutDuringMda", ref typesToCalc, aggIntvs[unit.Id], 249, 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AT" + rowCount, GetDropdownFromRow("PcIntvLengthOfStockOut", ref typesToCalc, aggIntvs[unit.Id], 251, 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AS" + rowCount, GetCombineFromRow("PcIntvStockOutDrug", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BA" + rowCount, GetCombineFromRow("Notes", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));

                    DateTime? startMda = GetDateFromRow("PcIntvStartDateOfMda", ref typesToCalc, aggIntvs[unit.Id], false, 1, Util.MaxRounds, "LF", typeNames);
                    if (startMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "Y" + rowCount, startMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "Z" + rowCount, startMda.Value.Year);
                    }
                    DateTime? endMda = GetDateFromRow("PcIntvEndDateOfMda", ref typesToCalc, aggIntvs[unit.Id], true, 1, Util.MaxRounds, "LF", typeNames);
                    if (endMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "AA" + rowCount, endMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "AB" + rowCount, endMda.Value.Year);
                    }

                    RemoveDataValidation(xlsWorksheet, rng, "X" + rowCount);
                    AddValueToRange(xlsWorksheet, rng, "X" + rowCount, TranslateMdaType(typesToCalc));

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
            var surveys = surveyRepo.GetByTypeInDateRange(
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
           
                var mostRecentSurvey = surveys.Where(s => s.AdminLevels.Select(a => a.Id).Contains(unit.Id)).OrderByDescending(s => s.DateReported).FirstOrDefault();
                if (mostRecentSurvey != null)
                {
                    string testType = null;
                    string percent = null;
                    int year = 0;
                    if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.OnchoAssessment)
                    {
                        var ind = mostRecentSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "OnchoSurTestType");
                        if (ind != null)
                            testType = ind.DynamicValue;
                        percent = GetPercentPositive(mostRecentSurvey, "OnchoSurNumberOfIndividualsPositive", "OnchoSurNumberOfIndividualsExamined", out year);
                    }
                    else if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.OnchoMapping)
                    {
                        var ind = mostRecentSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "OnchoMapSurTestType");
                        if (ind != null)
                            testType = ind.DynamicValue;
                        percent = GetPercentPositive(mostRecentSurvey, "OnchoMapSurNumberOfIndividualsPositive", "OnchoMapSurNumberOfIndividualsExamined", out year);
                    }
                    AddValueToRange(xlsWorksheet, rng, "H" + rowCount, percent);
                    AddValueToRange(xlsWorksheet, rng, "I" + rowCount, year);
                    AddValueToRange(xlsWorksheet, rng, "J" + rowCount, testType);
                }

                // DISEASE DISTRO
                if (onchoDd.ContainsKey(unit.Id))
                {
                    string endemicity = ParseOnchoDdEndo(onchoDd[unit.Id][TranslationLookup.GetValue("DDOnchoDiseaseDistributionPcInterventio") + " - " + oncho.Disease.DisplayName].ToString());
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
                    DateTime? startMda = GetDateFromRow("PcIntvStartDateOfMda", ref typesToCalc, aggIntvs[unit.Id], false, 1, 1, "Oncho", typeNames);
                    if (startMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "V" + rowCount, startMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "W" + rowCount, startMda.Value.Year);
                    }
                    DateTime? endMda = GetDateFromRow("PcIntvEndDateOfMda", ref typesToCalc, aggIntvs[unit.Id], true, 1, 1, "Oncho", typeNames);
                    if (endMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "X" + rowCount, endMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "Y" + rowCount, endMda.Value.Year);
                    }
                    AddValueToRange(xlsWorksheet, rng, "AB" + rowCount, GetIntFromRow("PcIntvNumEligibleIndividualsTargeted", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "Oncho", typeNames, "PcIntvOfTotalTargetedForOncho"));
                    AddValueToRange(xlsWorksheet, rng, "AE" + rowCount, GetIntFromRow("PcIntvNumIndividualsTreated", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "Oncho", typeNames, "PcIntvOfTotalTreatedForOncho"));
                    AddValueToRange(xlsWorksheet, rng, "AG" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "Oncho", typeNames, "PcIntvOfTotalFemalesOncho"));
                    AddValueToRange(xlsWorksheet, rng, "AI" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "Oncho", typeNames, "PcIntvOfTotalMalesOncho"));
                    AddValueToRange(xlsWorksheet, rng, "AK" + rowCount, GetDropdownFromRow("PcIntvStockOutDuringMda", ref typesToCalc, aggIntvs[unit.Id], 249, 1, 1, "Oncho", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AM" + rowCount, GetDropdownFromRow("PcIntvLengthOfStockOut", ref typesToCalc, aggIntvs[unit.Id], 251, 1, 1, "Oncho", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AL" + rowCount, GetCombineFromRow("PcIntvStockOutDrug", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "Oncho", typeNames));

                    RemoveDataValidation(xlsWorksheet, rng, "U" + rowCount);
                    AddValueToRange(xlsWorksheet, rng, "U" + rowCount, TranslateMdaType(typesToCalc));

                    // ROUND 2
                    typesToCalc = new List<string>();
                    startMda = GetDateFromRow("PcIntvStartDateOfMda", ref typesToCalc, aggIntvs[unit.Id], false, 2, 2, "Oncho", typeNames);
                    if (startMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "AU" + rowCount, startMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "AV" + rowCount, startMda.Value.Year);
                    }
                    endMda = GetDateFromRow("PcIntvEndDateOfMda", ref typesToCalc, aggIntvs[unit.Id], true, 2, 2, "Oncho", typeNames);
                    if (endMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "AW" + rowCount, endMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "AX" + rowCount, endMda.Value.Year);
                    }
                    AddValueToRange(xlsWorksheet, rng, "BA" + rowCount, GetIntFromRow("PcIntvNumEligibleIndividualsTargeted", ref typesToCalc, aggIntvs[unit.Id], 2, 2, "Oncho", typeNames, "PcIntvOfTotalTargetedForOncho"));
                    AddValueToRange(xlsWorksheet, rng, "BD" + rowCount, GetIntFromRow("PcIntvNumIndividualsTreated", ref typesToCalc, aggIntvs[unit.Id], 2, 2, "Oncho", typeNames, "PcIntvOfTotalTreatedForOncho"));
                    AddValueToRange(xlsWorksheet, rng, "BF" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", ref typesToCalc, aggIntvs[unit.Id], 2, 2, "Oncho", typeNames, "PcIntvOfTotalFemalesOncho"));
                    AddValueToRange(xlsWorksheet, rng, "BH" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", ref typesToCalc, aggIntvs[unit.Id], 2, 2, "Oncho", typeNames, "PcIntvOfTotalMalesOncho"));
                    AddValueToRange(xlsWorksheet, rng, "BJ" + rowCount, GetDropdownFromRow("PcIntvStockOutDuringMda", ref typesToCalc, aggIntvs[unit.Id], 249, 2, 2, "Oncho", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BL" + rowCount, GetDropdownFromRow("PcIntvLengthOfStockOut", ref typesToCalc, aggIntvs[unit.Id], 251, 2, 2, "Oncho", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BK" + rowCount, GetCombineFromRow("PcIntvStockOutDrug", ref typesToCalc, aggIntvs[unit.Id], 2, 2, "Oncho", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BS" + rowCount, GetCombineFromRow("Notes", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Oncho", typeNames));

                    RemoveDataValidation(xlsWorksheet, rng, "AT" + rowCount);
                    AddValueToRange(xlsWorksheet, rng, "AT" + rowCount, TranslateMdaType(typesToCalc));
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
            var surveys = surveyRepo.GetByTypeInDateRange(
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
                    // Get the most recent survey year
                    int mostRecentSurveyYear = mostRecentSurvey.DateReported.Year;
                    // Filter surveys by the type of the most recent and the year of the most recent
                    List<SurveyBase> recentYearSurveys = surveys.Where(
                        s => s.AdminLevels.Select(a => a.Id).Contains(unit.Id)
                            && mostRecentSurveyYear == s.DateReported.Year
                            && mostRecentSurvey.TypeOfSurvey.Id == s.TypeOfSurvey.Id).ToList();
                    // Get the survey with the highest values for prevalence and proportion of infections
                    SurveyBase surveyWithHighestPrevalence = null;
                    double prevalenceInfections = 0;
                    double proportionInfections = 0;
                    int year = 0;
                    foreach (SurveyBase recentYearSurvey in recentYearSurveys)
                    {
                        string currentPrevalenceStr = null;
                        string currentProportionStr = null;
                        if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.SchistoSentinel)
                        {
                            // Calculate Prevalence
                            currentPrevalenceStr = GetPercentPositive(recentYearSurvey, "SCHSurNumberOfIndividualsPositiveForInte", "SCHSurNumberOfIndividualsExaminedForInte", out year);
                            // Calculate Proportion
                            currentProportionStr = GetPercentPositive(recentYearSurvey, "SCHSurProportionOfHeavyIntensityIntestin", "SCHSurNumberOfIndividualsExaminedForInte", out year);
                        }
                        else if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.SchMapping)
                        {
                            // Calculate Prevalence
                            currentPrevalenceStr = GetPercentPositive(recentYearSurvey, "SCHMapSurNumberOfIndividualsPositiveForI", "SCHMapSurNumberOfIndividualsExaminedForI", out year);
                            // Calculate Proportion
                            currentProportionStr = GetPercentPositive(recentYearSurvey, "SCHMapSurProportionOfHeavyIntensityIntes", "SCHMapSurNumberOfIndividualsExaminedForI", out year);
                        }

                        // See if the calculated prevalence is higher than the last
                        double currentPrevalence = 0;
                        if (currentPrevalenceStr != null && double.TryParse(currentPrevalenceStr, out currentPrevalence) && currentPrevalence > prevalenceInfections)
                        {
                            prevalenceInfections = currentPrevalence;
                            // Keep track of the survey with the highest prevalence to be used to determine the test type
                            surveyWithHighestPrevalence = recentYearSurvey;
                        }
                        // See if the calculated proportion is higher than the last
                        double currentProportion = 0;
                        if (currentProportionStr != null && double.TryParse(currentProportionStr, out currentProportion) && currentProportion > proportionInfections)
                            proportionInfections = currentProportion;
                    }
                    // Test type
                    string testType = "";
                    if (surveyWithHighestPrevalence != null)
                    {
                        if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.SchistoSentinel)
                        {
                            var ind = surveyWithHighestPrevalence.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "SCHSurTestType");
                            if (ind != null && ind.DynamicValue != null)
                                testType = ind.DynamicValue;
                        }
                        else if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.SchMapping)
                        {
                            var ind = surveyWithHighestPrevalence.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "SCHMapSurTestType");
                            if (ind != null && ind.DynamicValue != null)
                                testType = ind.DynamicValue;
                        }
                    }

                    AddValueToRange(xlsWorksheet, rng, "I" + rowCount, prevalenceInfections);
                    AddValueToRange(xlsWorksheet, rng, "J" + rowCount, proportionInfections);
                    AddValueToRange(xlsWorksheet, rng, "K" + rowCount, mostRecentSurvey.DateReported.Year);
                    AddValueToRange(xlsWorksheet, rng, "L" + rowCount, testType);
                }

                // DISEASE DISTRO
                if (schDd.ContainsKey(unit.Id))
                {
                    string endemicity = ParseSchEnd(schDd[unit.Id][TranslationLookup.GetValue("DDSchistoDiseaseDistributionPcIntervent") + " - " + sch.Disease.DisplayName].ToString());
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
                        ParseSchistoFrequency(schDd[unit.Id][TranslationLookup.GetValue("DDSchistoNumPcRoundsYearRecommendedByWh") + " - " + sch.Disease.DisplayName].ToString()));
                    AddValueToRange(xlsWorksheet, rng, "W" + rowCount,
                        ParseSchistoFrequency(schDd[unit.Id][TranslationLookup.GetValue("DDSchistoNumPcRoundsYearCurrentlyImplem") + " - " + sch.Disease.DisplayName].ToString()));
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
                    DateTime? startMda = GetDateFromRow("PcIntvStartDateOfMda", ref typesToCalc, aggIntvs[unit.Id], false, 1, Util.MaxRounds, "Schisto", typeNames);
                    if (startMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "AC" + rowCount, startMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "AD" + rowCount, startMda.Value.Year);
                    }
                    DateTime? endMda = GetDateFromRow("PcIntvEndDateOfMda", ref typesToCalc, aggIntvs[unit.Id], true, 1, Util.MaxRounds, "Schisto", typeNames);
                    if (endMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "AE" + rowCount, endMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "AF" + rowCount, endMda.Value.Year);
                    }
                    AddValueToRange(xlsWorksheet, rng, "AI" + rowCount, GetIntFromRow("PcIntvNumEligibleIndividualsTargeted", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AN" + rowCount, GetIntFromRow("PcIntvNumIndividualsTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AP" + rowCount, GetIntFromRow("PcIntvNumSacTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AR" + rowCount, GetIntFromRow("PcIntvNumAdultsTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AT" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AV" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AX" + rowCount, GetDropdownFromRow("PcIntvStockOutDuringMda", ref typesToCalc, aggIntvs[unit.Id], 249, 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AZ" + rowCount, GetDropdownFromRow("PcIntvLengthOfStockOut", ref typesToCalc, aggIntvs[unit.Id], 251, 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AY" + rowCount, GetCombineFromRow("PcIntvStockOutDrug", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BG" + rowCount, GetCombineFromRow("Notes", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));

                    RemoveDataValidation(xlsWorksheet, rng, "AB" + rowCount);
                    AddValueToRange(xlsWorksheet, rng, "AB" + rowCount, TranslateMdaType(typesToCalc));
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
            GetDdForDisease(start, end, demography, out sth, out sthDd, DiseaseType.STH);

            // Get STH Surveys
            var surveys = surveyRepo.GetByTypeInDateRange(
                new List<int> { (int)StaticSurveyType.SthMapping, (int)StaticSurveyType.SthSentinel }, StartDate, EndDate);

            int rowCount = 18;
            foreach (var unit in demography)
            {
                // SURVEYS
                var mostRecentSurvey = surveys.Where(s => s.AdminLevels.Select(a => a.Id).Contains(unit.Id)).OrderByDescending(s => s.DateReported).FirstOrDefault();
                if (mostRecentSurvey != null)
                {
                    // Get the most recent survey year
                    int mostRecentSurveyYear = mostRecentSurvey.DateReported.Year;
                    // Filter surveys by the type of the most recent and the year of the most recent
                    List<SurveyBase> recentYearSurveys = surveys.Where(
                        s => s.AdminLevels.Select(a => a.Id).Contains(unit.Id)
                            && mostRecentSurveyYear == s.DateReported.Year
                            && mostRecentSurvey.TypeOfSurvey.Id == s.TypeOfSurvey.Id).ToList();
                    // Get the survey with the highest values for prevalence and proportion of infections
                    SurveyBase surveyWithHighestPrevalence = null;
                    double prevalenceInfections = 0;
                    double proportionInfections = 0;
                    int year = 0;
                    foreach (SurveyBase recentYearSurvey in recentYearSurveys)
                    {
                        string currentPrevalenceStr = null;
                        string currentProportionStr = null;
                        if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.SthSentinel)
                        {
                            // Get the prevalence
                            currentPrevalenceStr = GetPercentPositive(recentYearSurvey, "STHSurNumberOfIndividualsPositiveOverall", "STHSurNumberOfIndividualsExaminedOverall", out year);
                            // Get the proportion
                            var propInd = recentYearSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "STHSurOverallProportionOfHeavyIntensity");
                            if (propInd != null)
                                currentProportionStr = propInd.DynamicValue;
                        }
                        else if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.SthMapping)
                        {
                            // Get the prevalence
                            currentPrevalenceStr = GetPercentPositive(recentYearSurvey, "STHMapSurSurNumberOfIndividualsPositiveOverall", "STHMapSurSurNumberOfIndividualsExaminedOverall", out year);
                            // Get the proportion
                            var propInd = recentYearSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "STHMapSurSurOverallProportionOfHeavyIntensity");
                            if (propInd != null)
                                currentProportionStr = propInd.DynamicValue;
                        }

                        // See if the calculated prevalence is higher than the last
                        double currentPrevalence = 0;
                        if (currentPrevalenceStr != null && double.TryParse(currentPrevalenceStr, out currentPrevalence) && currentPrevalence > prevalenceInfections)
                        {
                            prevalenceInfections = currentPrevalence;
                            // Keep track of the survey with the highest prevalence to be used to determine the test type
                            surveyWithHighestPrevalence = recentYearSurvey;
                        }
                        // See if the calculated proportion is higher than the last
                        double currentProportion = 0;
                        if (currentProportionStr != null && double.TryParse(currentProportionStr, out currentProportion) && currentProportion > proportionInfections)
                            proportionInfections = currentProportion;
                    }
                    // Test type
                    string testType = "";
                    if (surveyWithHighestPrevalence != null)
                    {
                        if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.SthSentinel)
                        {
                            var ind = surveyWithHighestPrevalence.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "STHSurTestType");
                            if (ind != null && ind.DynamicValue != null)
                                testType = ind.DynamicValue;
                        }
                        else if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.SthMapping)
                        {
                            var ind = surveyWithHighestPrevalence.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "STHMapSurSurTestType");
                            if (ind != null && ind.DynamicValue != null)
                                testType = ind.DynamicValue;
                        }
                    }

                    AddValueToRange(xlsWorksheet, rng, "I" + rowCount, prevalenceInfections);
                    AddValueToRange(xlsWorksheet, rng, "J" + rowCount, proportionInfections);
                    AddValueToRange(xlsWorksheet, rng, "K" + rowCount, mostRecentSurvey.DateReported.Year);
                    AddValueToRange(xlsWorksheet, rng, "L" + rowCount, testType);
                }

                // DISEASE DISTRO
                if (sthDd.ContainsKey(unit.Id))
                {
                    string endemicity = ParseSthEnd(sthDd[unit.Id][TranslationLookup.GetValue("DDSTHDiseaseDistributionPcInterventions") + " - " + sth.Disease.DisplayName].ToString());
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
                        ParseSthFrequency(sthDd[unit.Id][TranslationLookup.GetValue("DDSTHNumPcRoundsYearRecommendedByWhoGui") + " - " + sth.Disease.DisplayName].ToString()));
                    AddValueToRange(xlsWorksheet, rng, "W" + rowCount,
                        ParseSthFrequency(sthDd[unit.Id][TranslationLookup.GetValue("DDSTHNumPcRoundsYearCurrentlyImplemente") + " - " + sth.Disease.DisplayName].ToString()));
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
                    DateTime? startMda = GetDateFromRow("PcIntvStartDateOfMda", ref typesToCalc, aggIntvs[unit.Id], false, 1, 1, "STH", typeNames);
                    if (startMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "AE" + rowCount, startMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "AF" + rowCount, startMda.Value.Year);
                    }
                    DateTime? endMda = GetDateFromRow("PcIntvEndDateOfMda", ref typesToCalc, aggIntvs[unit.Id], true, 1, 1, "STH", typeNames);
                    if (endMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "AG" + rowCount, endMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "AH" + rowCount, endMda.Value.Year);
                    }
                    AddValueToRange(xlsWorksheet, rng, "AK" + rowCount, GetIntFromRow("PcIntvNumEligibleIndividualsTargeted", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AQ" + rowCount, GetIntFromRow("PcIntvNumIndividualsTreated", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AS" + rowCount, GetIntFromRow("PcIntvPsacTreated", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AU" + rowCount, GetIntFromRow("PcIntvNumSacTreated", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AW" + rowCount, GetIntFromRow("PcIntvNumAdultsTreated", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "AY" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BA" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BC" + rowCount, GetDropdownFromRow("PcIntvStockOutDuringMda", ref typesToCalc, aggIntvs[unit.Id], 249, 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BE" + rowCount, GetDropdownFromRow("PcIntvLengthOfStockOut", ref typesToCalc, aggIntvs[unit.Id], 251, 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BD" + rowCount, GetCombineFromRow("PcIntvStockOutDrug", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));

                    RemoveDataValidation(xlsWorksheet, rng, "AD" + rowCount);
                    AddValueToRange(xlsWorksheet, rng, "AD" + rowCount, TranslateMdaType(typesToCalc));

                    // ROUND 2
                    typesToCalc = new List<string>();
                    startMda = GetDateFromRow("PcIntvStartDateOfMda", ref typesToCalc, aggIntvs[unit.Id], false, 2, 2, "STH", typeNames);
                    if (startMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "BM" + rowCount, startMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "BN" + rowCount, startMda.Value.Year);
                    }
                    endMda = GetDateFromRow("PcIntvEndDateOfMda", ref typesToCalc, aggIntvs[unit.Id], true, 2, 2, "STH", typeNames);
                    if (endMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "BO" + rowCount, endMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "BP" + rowCount, endMda.Value.Year);
                    }
                    AddValueToRange(xlsWorksheet, rng, "BS" + rowCount, GetIntFromRow("PcIntvNumEligibleIndividualsTargeted", ref typesToCalc, aggIntvs[unit.Id], 2, 2, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "BY" + rowCount, GetIntFromRow("PcIntvNumIndividualsTreated", ref typesToCalc, aggIntvs[unit.Id], 2, 2, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CA" + rowCount, GetIntFromRow("PcIntvPsacTreated", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CC" + rowCount, GetIntFromRow("PcIntvNumSacTreated", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CE" + rowCount, GetIntFromRow("PcIntvNumAdultsTreated", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CG" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", ref typesToCalc, aggIntvs[unit.Id], 2, 2, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CI" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", ref typesToCalc, aggIntvs[unit.Id], 2, 2, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CK" + rowCount, GetDropdownFromRow("PcIntvStockOutDuringMda", ref typesToCalc, aggIntvs[unit.Id], 249, 2, 2, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CM" + rowCount, GetDropdownFromRow("PcIntvLengthOfStockOut", ref typesToCalc, aggIntvs[unit.Id], 251, 2, 2, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CL" + rowCount, GetCombineFromRow("PcIntvStockOutDrug", ref typesToCalc, aggIntvs[unit.Id], 2, 2, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "CT" + rowCount, GetCombineFromRow("Notes", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "STH", typeNames));

                    RemoveDataValidation(xlsWorksheet, rng, "BL" + rowCount);
                    AddValueToRange(xlsWorksheet, rng, "BL" + rowCount, TranslateMdaType(typesToCalc));
                }

                rowCount++;
            }
        }

        private void AddTrachoma(excel.Worksheet xlsWorksheet, excel.Range rng, DateTime start, DateTime end, List<AdminLevel> demography, Dictionary<int, DataRow> aggIntvs)
        {
            List<string> typeNames = new List<string> { "IntvZithroTeo" };
            List<int> typeIds = new List<int> { (int)StaticIntvType.ZithroTeo };
            // Get sch Disease Distributions
            DiseaseDistroPc tra;
            Dictionary<int, DataRow> traDd;
            GetDdForDisease(start, end, demography, out tra, out traDd, DiseaseType.Trachoma);

            // get one thing from one thing
            Dictionary<int, DataRow> subDistrictEligible = GetEligibleInSubdistricts(start, end);

            var surveys = surveyRepo.GetByTypeForDistrictsInDateRange(
                new List<int> { (int)StaticSurveyType.TrachomaImpact }, StartDate, EndDate);

            int rowCount = 21;
            foreach (var unit in demography)
            {
                // Surveys
                var mostRecentSurvey = surveys.Where(s => s.AdminLevels.Select(a => a.Id).Contains(unit.Id)).OrderByDescending(s => s.DateReported).FirstOrDefault();
                if (mostRecentSurvey != null)
                {
                    AddTraLevelToRange(xlsWorksheet, rng, rowCount, mostRecentSurvey, "TraSurLevelOfAntibioticTreatmentRequired", "R");
                }
                // DISEASE DISTRO
                if (traDd.ContainsKey(unit.Id))
                {
                    string endemicity = ParseTrachomaEnd(traDd[unit.Id][TranslationLookup.GetValue("DDTraDiseaseDistributionPcInterventions") + " - " + tra.Disease.DisplayName].ToString());
                    AddValueToRange(xlsWorksheet, rng, "F" + rowCount, endemicity);
                    AddValueToRange(xlsWorksheet, rng, "J" + rowCount,
                        traDd[unit.Id][TranslationLookup.GetValue("DDTraTrichiasisBacklog") + " - " + tra.Disease.DisplayName]);

                    DateTime datePlanned;
                    if (DateTime.TryParseExact(traDd[unit.Id][TranslationLookup.GetValue("DDTraYearOfPlannedTrachomaImpactSurvey") + " - " + tra.Disease.DisplayName].ToString(),
                        "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out datePlanned))
                    {
                        AddValueToRange(xlsWorksheet, rng, "O" + rowCount, datePlanned.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "P" + rowCount, datePlanned.Year);
                    }
                    AddValueToRange(xlsWorksheet, rng, "Q" + rowCount,
                    traDd[unit.Id][TranslationLookup.GetValue("DDTraPopulationAtRisk") + " - " + tra.Disease.DisplayName]);

                    string achievedCriteria = traDd[unit.Id][TranslationLookup.GetValue("DDTraAchievedCriteria") + " - " + tra.Disease.DisplayName].ToString();
                    if (achievedCriteria == TranslationLookup.GetValue("Yes"))
                        AddValueToRange(xlsWorksheet, rng, "T" + rowCount, achievedCriteria);
                    AddValueToRange(xlsWorksheet, rng, "U" + rowCount,
                       traDd[unit.Id][TranslationLookup.GetValue("DDTraYearDeterminedThatAchievedCriteriaF") + " - " + tra.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "V" + rowCount,
                       traDd[unit.Id][TranslationLookup.GetValue("DDTraPopulationLivingInAreasDistrict") + " - " + tra.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "W" + rowCount,
                       traDd[unit.Id][TranslationLookup.GetValue("DDTraYearAchievedUiga") + " - " + tra.Disease.DisplayName]);
                    AddValueToRange(xlsWorksheet, rng, "X" + rowCount,
                       traDd[unit.Id][TranslationLookup.GetValue("DDTraPopulationLivingInAreasSubDistrict") + " - " + tra.Disease.DisplayName]);
                }
                // MDA count
                int mdas = 0, consecutive = 0;
                CountMdas(typeIds, unit.Id, unit.Children.Select(c => c.Id).ToList(), out mdas, out consecutive, "Trachoma");
                AddValueToRange(xlsWorksheet, rng, "AA" + rowCount, mdas);
                AddValueToRange(xlsWorksheet, rng, "AB" + rowCount, consecutive);
                // INTVS
                if (aggIntvs.ContainsKey(unit.Id))
                {
                    List<string> typesToCalc = new List<string>();
                    // currently implementing at district or subdistrict
                    string aggEligible = GetIntFromRow("PcIntvNumEligibleIndividualsTargeted", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "Trachoma", typeNames, false);
                    if (subDistrictEligible.ContainsKey(unit.Id))
                    {
                        string belowDistrictEligible = GetIntFromRow("PcIntvNumEligibleIndividualsTargeted", ref typesToCalc, subDistrictEligible[unit.Id], 1, 1, "Trachoma", typeNames, false);
                        if (!string.IsNullOrEmpty(belowDistrictEligible))
                            AddValueToRange(xlsWorksheet, rng, "S" + rowCount, TranslationLookup.GetValue("RtiSubDistrict", "RtiSubDistrict"));
                    }
                    else if (!string.IsNullOrEmpty(aggEligible))
                        AddValueToRange(xlsWorksheet, rng, "S" + rowCount, TranslationLookup.GetValue("RtiDistrict", "RtiDistrict"));


                    DateTime? startMda = GetDateFromRow("PcIntvStartDateOfMda", ref typesToCalc, aggIntvs[unit.Id], false, 1, 1, "Trachoma", typeNames, false);
                    if (startMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "AE" + rowCount, startMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "AF" + rowCount, startMda.Value.Year);
                    }
                    DateTime? endMda = GetDateFromRow("PcIntvEndDateOfMda", ref typesToCalc, aggIntvs[unit.Id], true, 1, 1, "Trachoma", typeNames, false);
                    if (endMda.HasValue)
                    {
                        AddValueToRange(xlsWorksheet, rng, "AG" + rowCount, endMda.Value.ToString("MMMM"));
                        AddValueToRange(xlsWorksheet, rng, "AH" + rowCount, endMda.Value.Year);
                    }

                    AddValueToRange(xlsWorksheet, rng, "AK" + rowCount, aggEligible);
                    AddValueToRange(xlsWorksheet, rng, "AN" + rowCount, GetIntFromRow("PcIntvNumIndividualsTreated", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "Trachoma", typeNames, false));

                    // Types based off these?
                    // teo
                    string teo = GetIntFromRow("PcIntvNumTreatedTeo", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "Trachoma", typeNames, false);
                    AddValueToRange(xlsWorksheet, rng, "AP" + rowCount, teo);
                    // zithro  
                    int zxTotal = 0;
                    string zx = GetIntFromRow("PcIntvNumTreatedZx", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "Trachoma", typeNames, false);
                    string zxPos = GetIntFromRow("PcIntvNumTreatedZxPos", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "Trachoma", typeNames, false);
                    if (!string.IsNullOrEmpty(zx))
                        zxTotal += int.Parse(zx);
                    if (!string.IsNullOrEmpty(zxPos))
                        zxTotal += int.Parse(zxPos);
                    AddValueToRange(xlsWorksheet, rng, "AR" + rowCount, zxTotal);

                    AddValueToRange(xlsWorksheet, rng, "AT" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "Trachoma", typeNames, false));
                    AddValueToRange(xlsWorksheet, rng, "AV" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "Trachoma", typeNames, false));
                    AddValueToRange(xlsWorksheet, rng, "AX" + rowCount, GetDropdownFromRow("PcIntvStockOutDuringMda", ref typesToCalc, aggIntvs[unit.Id], 249, 1, 1, "Trachoma", typeNames, false));
                    AddValueToRange(xlsWorksheet, rng, "AZ" + rowCount, GetDropdownFromRow("PcIntvLengthOfStockOut", ref typesToCalc, aggIntvs[unit.Id], 251, 1, 1, "Trachoma", typeNames, false));
                    AddValueToRange(xlsWorksheet, rng, "AY" + rowCount, GetCombineFromRow("PcIntvStockOutDrug", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "Trachoma", typeNames, false));


                    RemoveDataValidation(xlsWorksheet, rng, "AD" + rowCount);
                    if (!string.IsNullOrEmpty(teo) && zxTotal > 0)
                        AddValueToRange(xlsWorksheet, rng, "AD" + rowCount, "Zithro+Tetra");
                    else if (!string.IsNullOrEmpty(teo))
                        AddValueToRange(xlsWorksheet, rng, "AD" + rowCount, "Tetra");
                    else if (zxTotal > 0)
                        AddValueToRange(xlsWorksheet, rng, "AD" + rowCount, "Zithro");

                    AddValueToRange(xlsWorksheet, rng, "BG" + rowCount, GetCombineFromRow("Notes", ref typesToCalc, aggIntvs[unit.Id], 1, 1, "Trachoma", typeNames, false));
                }

                rowCount++;
            }
        }

        #region Helpers
        private void AddTypeOfLfSurveySite(excel.Worksheet xlsWorksheet, excel.Range rng, int rowCount, SurveyBase mostRecentSurvey)
        {
            string typeName = "";
            if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.LfSentinel)
            {
                var testType = mostRecentSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "LFSurTestType");
                if (testType != null)
                {
                    if (mostRecentSurvey.SentinelSiteId.HasValue && testType.DynamicValue == "MF")
                        typeName = transLookup.GetValue("RtiSsMf", "RtiSsMf");
                    else if (mostRecentSurvey.SentinelSiteId.HasValue)
                        typeName = transLookup.GetValue("RtiSsAg", "RtiSsAg");
                    else if (testType.DynamicValue == "MF")
                        typeName = transLookup.GetValue("RtiScMf", "RtiScMf");
                    else
                        typeName = transLookup.GetValue("RtiScAg", "RtiScAg");
                }
            }
            else if (mostRecentSurvey.TypeOfSurvey.Id == (int)StaticSurveyType.LfMapping)
            {
                var testType = mostRecentSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "LFMapSurTestType");
                var isSentinel = mostRecentSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == "LFMapSurWillTheSitesAlsoServeAsASentin");
                if (testType != null && isSentinel != null)
                {
                    if (isSentinel.DynamicValue == "YesSentinelSite" && testType.DynamicValue == "MF")
                        typeName = transLookup.GetValue("RtiSsMf", "RtiSsMf");
                    else if (isSentinel.DynamicValue == "YesSentinelSite")
                        typeName = transLookup.GetValue("RtiSsAg", "RtiSsAg");
                    else if (testType.DynamicValue == "MF")
                        typeName = transLookup.GetValue("RtiScMf", "RtiScMf");
                    else
                        typeName = transLookup.GetValue("RtiScAg", "RtiScAg");
                }
            }
            else
                typeName = transLookup.GetValue("RtiTas", "RtiTas");

            AddValueToRange(xlsWorksheet, rng, "I" + rowCount, typeName);
        }

        private string GetPercentPositive(SurveyBase mostRecentSurvey, string posName, string examinedName, out int year)
        {
            string percent = "";
            var pos = mostRecentSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == posName);
            var examined = mostRecentSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == examinedName);
            if (pos != null && examined != null)
            {
                percent = CalcBase.GetPercentage(pos.DynamicValue, examined.DynamicValue, 1);
                if (percent == Translations.NA)
                    percent = "";
            }
            year = mostRecentSurvey.DateReported.Year;
            return percent;
        }
     
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
        
        private void AddIndicatorToRange(excel.Worksheet xlsWorksheet, excel.Range rng, int rowCount, SurveyBase mostRecentSurvey, string indName, string colName)
        {
            var ind = mostRecentSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == indName);
            if (ind != null && !string.IsNullOrEmpty(ind.DynamicValue))
                AddValueToRange(xlsWorksheet, rng, colName + rowCount, ind.DynamicValue);
        }

        protected Dictionary<int, DataRow> GetEligibleInSubdistricts(DateTime start, DateTime end)
        {
            IntvRepository iRepo = new IntvRepository();
            ReportOptions options = new ReportOptions
            {
                MonthYearStarts = start.Month,
                StartDate = start,
                EndDate = end,
                IsCountryAggregation = false,
                IsByLevelAggregation = true,
                IsAllLocations = false,
                IsNoAggregation = false,
                IsGroupByRange = true,

            };

            var childType = settings.GetAllAdminLevels().Where(a => a.LevelNumber > AdminLevelType.LevelNumber).OrderBy(l => l.LevelNumber).FirstOrDefault();
            if (childType == null)
                return new Dictionary<int, DataRow>();

            options.SelectedAdminLevels = demo.GetAdminLevelByLevel(childType.LevelNumber);
            IntvReportGenerator gen = new IntvReportGenerator();

            IntvType iType = iRepo.GetIntvType(23);
            var eligible = iType.Indicators.FirstOrDefault(i => i.Value.DisplayName == "PcIntvNumEligibleIndividualsTargeted");
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(iType.Id, eligible.Value));
            ReportResult ddResult = gen.Run(new SavedReport { ReportOptions = options });
            Dictionary<int, DataRow> intvData = new Dictionary<int, DataRow>();
            foreach (DataRow dr in ddResult.DataTableResults.Rows)
            {
                int id = 0;
                if (int.TryParse(dr["ID"].ToString(), out id))
                {
                    if (intvData.ContainsKey(id))
                        intvData[id] = dr;
                    else
                        intvData.Add(id, dr);
                }
            }
            return intvData;
        }

        private void AddTraLevelToRange(excel.Worksheet xlsWorksheet, excel.Range rng, int rowCount, SurveyBase mostRecentSurvey, string indName, string colName)
        {
            var ind = mostRecentSurvey.IndicatorValues.FirstOrDefault(v => v.Indicator.DisplayName == indName);
            if (ind != null && !string.IsNullOrEmpty(ind.DynamicValue))
            {
                if (ind.DynamicValue == "TraSurDistLevel")
                    AddValueToRange(xlsWorksheet, rng, colName + rowCount, TranslationLookup.GetValue("RtiDistrict", "RtiDistrict"));
                else if (ind.DynamicValue == "TraSurSubDistLevel")
                    AddValueToRange(xlsWorksheet, rng, colName + rowCount, TranslationLookup.GetValue("RtiSubDistrict", "RtiSubDistrict"));
            }
        }
        #endregion

        #region Indicator Parsers
        private string ParseLfDdEnd(string end)
        {
            if (end == TranslationLookup.GetValue("LfEnd0b"))
                return transLookup.GetValue("RtiLfDd0", "RtiLfDd0");
            if (end == TranslationLookup.GetValue("LfEnd1"))
                return transLookup.GetValue("RtiLfDd1", "RtiLfDd1");
            if (end == TranslationLookup.GetValue("LfEndM"))
                return transLookup.GetValue("RtiLfDdM", "RtiLfDdM");
            if (end == TranslationLookup.GetValue("LfEndNs"))
                return transLookup.GetValue("RtiLfDdNs", "RtiLfDdNs");
            if (end == TranslationLookup.GetValue("LfEnd100"))
                return transLookup.GetValue("RtiLfDd100", "RtiLfDd100");

            return transLookup.GetValue("RtiLfDdPending", "RtiLfDdPending");
        }

        private string ParseOnchoDdEndo(string endo)
        {
            if (endo == TranslationLookup.GetValue("Oncho0"))
                return transLookup.GetValue("RtiLfDd0", "RtiLfDd0");
            if (endo == TranslationLookup.GetValue("Oncho1"))
                return transLookup.GetValue("RtiLfDd1", "RtiLfDd1");
            if (endo == TranslationLookup.GetValue("OnchoM"))
                return transLookup.GetValue("RtiLfDdM", "RtiLfDdM");
            if (endo == TranslationLookup.GetValue("OnchoNs"))
                return transLookup.GetValue("RtiLfDdNs", "RtiLfDdNs");
            if (endo == TranslationLookup.GetValue("Oncho100"))
                return transLookup.GetValue("RtiLfDd100", "RtiLfDd100");

            return transLookup.GetValue("RtiLfDdPending", "RtiLfDdPending");
        }

        private string ParseTasObjective(string tas)
        {
            if (tas == TranslationLookup.GetValue("LfPostMdaTas1"))
                return transLookup.GetValue("RtiPostMda1", "RtiPostMda1");
            if (tas == TranslationLookup.GetValue("LfPostMdaTas2"))
                return transLookup.GetValue("RtiPostMda2", "RtiPostMda2");

            return transLookup.GetValue("RtiStopMda", "RtiStopMda");
        }
   
        private string ParseSchistoFrequency(string freq)
        {
            if (freq == TranslationLookup.GetValue("xyear2"))
                return transLookup.GetValue("RtiSchMdaD", "RtiSchMdaD");
            if (freq == TranslationLookup.GetValue("xyear1every2"))
                return transLookup.GetValue("RtiSchMdaB", "RtiSchMdaB");
            if (freq == TranslationLookup.GetValue("xyear1every3"))
                return transLookup.GetValue("RtiSchMdaC", "RtiSchMdaC");
            if (freq == TranslationLookup.GetValue("xyear1"))
                return transLookup.GetValue("RtiSchMdaA", "RtiSchMdaA");

            return transLookup.GetValue("RtiSchMdaNone", "RtiSchMdaNone");

        }

        private string ParseSthFrequency(string freq)
        {
            if (freq == TranslationLookup.GetValue("xyear2"))
                return transLookup.GetValue("RtiSchMdaA", "RtiSchMdaA");
            if (freq == TranslationLookup.GetValue("xyear1every2"))
                return transLookup.GetValue("RtiSchMdaC", "RtiSchMdaC");
            if (freq == TranslationLookup.GetValue("xyear3"))
                return transLookup.GetValue("RtiSchMdaD", "RtiSchMdaD");
            if (freq == TranslationLookup.GetValue("xyear1"))
                return transLookup.GetValue("RtiSchMdaB", "RtiSchMdaB");

            return transLookup.GetValue("RtiSchMdaNone", "RtiSchMdaNone");
        }

        private string ParseSchEnd(string end)
        {
            if (end == TranslationLookup.GetValue("SchistoM"))
                return transLookup.GetValue("RtiSchM", "RtiSchM");
            if (end == TranslationLookup.GetValue("SchNs"))
                return transLookup.GetValue("RtiSchNs", "RtiSchNs");
            if (end == TranslationLookup.GetValue("Scho0"))
                return transLookup.GetValue("RtiSch0", "RtiSch0");
            if (end == TranslationLookup.GetValue("Sch1"))
                return transLookup.GetValue("RtiSch1", "RtiSch1");
            if (end == TranslationLookup.GetValue("Sch2") || end == TranslationLookup.GetValue("Sch2b"))
                return transLookup.GetValue("RtiSch2", "RtiSch2");
            if (end == TranslationLookup.GetValue("Sch2a"))
                return transLookup.GetValue("RtiSch2a", "RtiSch2a");
            if (end == TranslationLookup.GetValue("Sch3") || end == TranslationLookup.GetValue("Sch3b"))
                return transLookup.GetValue("RtiSch3", "RtiSch3");
            if (end == TranslationLookup.GetValue("Sch3a"))
                return transLookup.GetValue("RtiSch3a", "RtiSch3a");
            if (end == TranslationLookup.GetValue("Sch10"))
                return transLookup.GetValue("RtiSch10", "RtiSch10");
            if (end == TranslationLookup.GetValue("Sch20"))
                return transLookup.GetValue("RtiSch20", "RtiSch20");
            if (end == TranslationLookup.GetValue("Sch30"))
                return transLookup.GetValue("RtiSch30", "RtiSch30");
            if (end == TranslationLookup.GetValue("Sch40"))
                return transLookup.GetValue("RtiSch40", "RtiSch40");
            if (end == TranslationLookup.GetValue("Sch100"))
                return transLookup.GetValue("RtiSch100", "RtiSch100");

            return transLookup.GetValue("RtiSchPending", "RtiSchPending");
        }

        private string ParseSthEnd(string end)
        {
            if (end == TranslationLookup.GetValue("SthM"))
                return transLookup.GetValue("RtiSchM", "RtiSchM");
            if (end == TranslationLookup.GetValue("SthNs"))
                return transLookup.GetValue("RtiSchNs", "RtiSchNs");
            if (end == TranslationLookup.GetValue("Sth0"))
                return transLookup.GetValue("RtiSch0", "RtiSch0");
            if (end == TranslationLookup.GetValue("Sth1"))
                return transLookup.GetValue("RtiSch1", "RtiSch1");
            if (end == TranslationLookup.GetValue("Sth2"))
                return transLookup.GetValue("RtiSch2", "RtiSch2");
            if (end == TranslationLookup.GetValue("Sth3"))
                return transLookup.GetValue("RtiSch3", "RtiSch3");
            if (end == TranslationLookup.GetValue("Sth10"))
                return transLookup.GetValue("RtiSch10", "RtiSch10");
            if (end == TranslationLookup.GetValue("Sth20"))
                return transLookup.GetValue("RtiSch20", "RtiSch20");
            if (end == TranslationLookup.GetValue("Sth30"))
                return transLookup.GetValue("RtiSch30", "RtiSch30");
            if (end == TranslationLookup.GetValue("Sth40"))
                return transLookup.GetValue("RtiSch40", "RtiSch40");
            if (end == TranslationLookup.GetValue("Sth100"))
                return transLookup.GetValue("RtiSch100", "RtiSch100");

            return transLookup.GetValue("RtiSchPending", "RtiSchPending");
        }

        private string ParseTrachomaEnd(string end)
        {
            if (end == TranslationLookup.GetValue("TraM"))
                return transLookup.GetValue("RtiTraM", "RtiTraM");
            if (end == TranslationLookup.GetValue("TraNs"))
                return transLookup.GetValue("RtiTraNs", "RtiTraNs");
            if (end == TranslationLookup.GetValue("Tra0"))
                return transLookup.GetValue("RtiTra0", "RtiTra0");
            if (end == TranslationLookup.GetValue("Tra1"))
                return transLookup.GetValue("RtiTra1", "RtiTra1");
            if (end == TranslationLookup.GetValue("Tra2"))
                return transLookup.GetValue("RtiTra2", "RtiTra2");
            if (end == TranslationLookup.GetValue("Tra3"))
                return transLookup.GetValue("RtiTra3", "RtiTra3");
            if (end == TranslationLookup.GetValue("Tra4"))
                return transLookup.GetValue("RtiTra4", "RtiTra4");
            if (end == TranslationLookup.GetValue("Tra5"))
                return transLookup.GetValue("RtiTra5", "RtiTra5");
            if (end == TranslationLookup.GetValue("Tra100"))
                return transLookup.GetValue("RtiTra100", "RtiTra100");

            return transLookup.GetValue("RtiTraPending", "RtiTraPending");
        }

        private string TranslateMdaType(List<string> typesToCalc)
        {
            List<string> translatedTypes = new List<string>();
            foreach (var t in typesToCalc)
                translatedTypes.Add(transLookup.GetValue(t, t));
            return string.Join(", ", translatedTypes.ToArray());
        }
        #endregion
    }
}
