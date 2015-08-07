using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using excel = Microsoft.Office.Interop.Excel;

namespace Nada.Model.Exports
{
    public class LeishReportExporter : ExporterBase, IExporter
    {
        excel.Application XlsApp;
        excel.Workbook XlsWorkbook;
        excel.Worksheet XlsWorksheet;
        excel.Range XlsRange;
        System.Globalization.CultureInfo OldCultureInfo;

        DateTime StartDate;
        DateTime EndDate;

        Country CountryData;
        CountryDemography CountryDemo;

        IntvType LeishMonthlyIntvType;
        IntvType LeishAnnualIntvType;
        ReportResult LeishMonthlyIntvReport;
        ReportResult LeishAnnualIntvReport;

        DiseaseDistroPc LeishDd;
        ReportResult LeishCountryAggDdReport;

        DemoRepository DemoRepo = new DemoRepository();
        SettingsRepository SettingsRepo = new SettingsRepository();
        IntvRepository IntvRepo = new IntvRepository();
        DiseaseRepository DiseaseRepo = new DiseaseRepository();

        IntvReportGenerator IntvReportGen = new IntvReportGenerator();
        DistributionReportGenerator DdReportGen = new DistributionReportGenerator();

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
                // Setup the Excel worksheet
                SetupWorksheet();

                // Determine the start and end of the year
                DetermineStartDate(questions);

                // Get country data
                GetCountryData();

                // Add country data
                AddCountryInfo(questions);

                // Run Leish monthly intervention report data
                RunLeishMonthlyIntvReport();

                // Run Leish annual intervention report data
                RunLeishAnnualIntvReport();

                // Run the Leish disease distribution report for the country
                RunLeishCountryAggDdReport();

                // Add epi
                AddEpi(questions);

                // Control
                AddControl(questions);

                // Diagnosis
                AddDiagnosis();

                // Treatment
                AddTreatment(questions);

                // Teardown worksheet
                TeardownWorksheet(fileName);

                return new ExportResult { WasSuccess = true };
            }
            catch (Exception ex)
            {
                return new ExportResult(ex.Message);
            }
        }

        private void SetupWorksheet()
        {
            OldCultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            XlsApp = new excel.ApplicationClass();
            XlsApp.DisplayAlerts = false;

            object missing = System.Reflection.Missing.Value;

            XlsWorkbook = XlsApp.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, @"Exports\Leishmaniasis_Report_Card.xlsx"),
                missing, missing, missing, missing, missing, missing, missing,
                missing, missing, missing, missing, missing, missing, missing);

            XlsWorksheet = (excel.Worksheet)XlsWorkbook.Worksheets[1];
            XlsRange = null;
        }

        private void TeardownWorksheet(string fileName)
        {
            object missing = System.Reflection.Missing.Value;

            XlsApp.DisplayAlerts = false;
            XlsWorkbook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, missing,
                missing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true,
                missing, missing, missing);
            XlsApp.Visible = true;
            XlsWorksheet = null;
            XlsWorkbook = null;
            XlsRange = null;
            XlsApp = null;
            System.Threading.Thread.CurrentThread.CurrentCulture = OldCultureInfo;
        }

        private void DetermineStartDate(LeishReportQuestions questions)
        {
            int year;
            if (questions != null && questions.LeishRepYearReporting.HasValue)
            {
                // Determine the year that was entered
                year = questions.LeishRepYearReporting.Value;
            }
            else
            {
                // Use the current year
                year = DateTime.Now.Year;
            }
            StartDate = new DateTime(year, 1, 1);
            EndDate = new DateTime(year, 12, 31);
        }

        private void GetCountryData()
        {
            CountryData = DemoRepo.GetCountry();
            CountryDemo = DemoRepo.GetCountryLevelStatsRecent();
        }

        private void RunLeishMonthlyIntvReport()
        {
            // Get the intervention type
            LeishMonthlyIntvType = IntvRepo.GetIntvType((int)StaticIntvType.LeishMonthly);
            // Report options
            ReportOptions reportOptions = new ReportOptions {
                MonthYearStarts = 1, StartDate = StartDate, EndDate = EndDate, IsCountryAggregation = true, 
                IsByLevelAggregation = false, IsAllLocations = false, IsNoAggregation = false
            };
            // Add the indicators from the intervention type
            foreach (var indicator in LeishMonthlyIntvType.Indicators)
                reportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(LeishMonthlyIntvType.Id, indicator));
            // Run the report
            LeishMonthlyIntvReport = IntvReportGen.Run(new SavedReport { ReportOptions = reportOptions });
        }

        private void RunLeishAnnualIntvReport()
        {
            // Get the intervention type
            LeishAnnualIntvType = IntvRepo.GetIntvType((int)StaticIntvType.LeishAnnual);
            // Report options
            ReportOptions reportOptions = new ReportOptions {
                MonthYearStarts = 1, StartDate = StartDate, EndDate = EndDate, IsCountryAggregation = true,
                IsByLevelAggregation = false, IsAllLocations = false, IsNoAggregation = false
            };
            // Add the indicators from the intervention type
            foreach (var indicator in LeishAnnualIntvType.Indicators)
                reportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(LeishAnnualIntvType.Id, indicator));
            // Run the report
            LeishAnnualIntvReport = IntvReportGen.Run(new SavedReport { ReportOptions = reportOptions });
        }

        private void RunLeishCountryAggDdReport()
        {
            // Get the distro
            LeishDd = DiseaseRepo.Create(DiseaseType.Leish);
            // Report options
            ReportOptions reportOptions = new ReportOptions {
                MonthYearStarts = 1, StartDate = StartDate, EndDate = EndDate, IsCountryAggregation = true,
                IsByLevelAggregation = false, IsAllLocations = false, IsNoAggregation = false
            };
            // Add the indicators from the distro
            foreach (var indicator in LeishDd.Indicators)
                reportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(LeishDd.Id, indicator));
            // Run the report
            LeishCountryAggDdReport = DdReportGen.Run(new SavedReport { ReportOptions = reportOptions });
        }

        public void AddCountryInfo(LeishReportQuestions questions)
        {
            AddValueToRange(XlsWorksheet, XlsRange, "B3", CountryData.Name);
            AddValueToRange(XlsWorksheet, XlsRange, "M3", questions.LeishRepYearReporting);
            AddValueToRange(XlsWorksheet, XlsRange, "D6", CountryDemo.TotalPopulation);
            // Income status
            AddValueToRange(XlsWorksheet, XlsRange, "D9", CountryDemo.CountryIncomeStatus);
            // GDP
            AddValueToRange(XlsWorksheet, XlsRange, "D8", CountryDemo.GrossDomesticProduct);
            // Gender ratio
            string femalePercent = CalculatePercent(CountryDemo.PopFemale, CountryDemo.TotalPopulation);
            string malePercent = CalculatePercent(CountryDemo.PopMale, CountryDemo.TotalPopulation);
            string genderRatio = string.Format("{0}% female - {1}% male", femalePercent, malePercent);
            AddValueToRange(XlsWorksheet, XlsRange, "D7", genderRatio);
            // Population age
            string ageRatio = string.Format("{0}%/{1}%/{2}%", CountryDemo.PercentPsac, CountryDemo.PercentSac, CountryDemo.PercentAdult);
            AddValueToRange(XlsWorksheet, XlsRange, "L6", ageRatio);
            // Life expectancy
            string lifeExpectancy = string.Format("female: {0}, male: {1}", CountryDemo.LifeExpectBirthFemale, CountryDemo.LifeExpectBirthMale);
            AddValueToRange(XlsWorksheet, XlsRange, "J7", lifeExpectancy);
            // Name of admin 2 level
            AdminLevelType adminLevel2 = SettingsRepo.GetAdminLevelTypeByLevel(2);
            if (adminLevel2 != null)
                AddValueToRange(XlsWorksheet, XlsRange, "M8", adminLevel2.DisplayName);
            // Number of admin level 2
            List<AdminLevel> admin2Levels = DemoRepo.GetAdminLevelByLevel(2);
            if (admin2Levels != null)
                AddValueToRange(XlsWorksheet, XlsRange, "K8", admin2Levels.Count);
        }

        public void AddEpi(LeishReportQuestions questions)
        {
            // Endemicity
            AddReportValueToExport(XlsWorksheet, XlsRange, "G13", LeishDd.Disease.DisplayName,
                Translations.LeishDiseaseDistEndemicityStatusVL, LeishCountryAggDdReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I13", LeishDd.Disease.DisplayName,
                Translations.LeishDiseaseDistEndemicityStatusCL, LeishCountryAggDdReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "K13", LeishDd.Disease.DisplayName,
                Translations.LeishDiseaseDistEndemicityStatusPKDL, LeishCountryAggDdReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "M13", LeishDd.Disease.DisplayName,
                Translations.LeishDiseaseDistEndemicityStatusMCL, LeishCountryAggDdReport);
            // Number of new cases (incidence)
            AddReportValueToExport(XlsWorksheet, XlsRange, "G14", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I14", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "K14", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "M14", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvTotalNumberOfNewMCLCasesDiagnosed, LeishMonthlyIntvReport);
            // Number of imported new cases
            AddReportValueToExport(XlsWorksheet, XlsRange, "G15", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfImportedVLCases, LeishAnnualIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I15", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfImportedCLCases, LeishAnnualIntvReport);
            // Incidence rate (calc)
            AddReportValueToExport(XlsWorksheet, XlsRange, "G16", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvVLIncidenceRate10000PeopleYear, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I16", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvCLIncidenceRate10000PeopleYear, LeishMonthlyIntvReport);
            // Gender distribution (%)
            AddReportValueToExport(XlsWorksheet, XlsRange, "G17", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntFemaleVL, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I17", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntFemaleCL, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "K17", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntFemalePKDL, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "M17", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntFemaleMCL, LeishMonthlyIntvReport);
            // Age group Distribution
            AddAgeGroupDistToExport(XlsWorksheet, XlsRange, "G18", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfNewVLCasesInChildrenLssThn5Years, Translations.LeishMontIntvPrcntOfNewVLCasesInChildren5To14Years,
                Translations.LeishMontIntvPrcntOfNewVLCasesInAdultsGrtrThn14Years, LeishMonthlyIntvReport);
            AddAgeGroupDistToExport(XlsWorksheet, XlsRange, "I18", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfNewCLCasesInChildrenLssThn5Years, Translations.LeishMontIntvPrcntOfNewCLCasesInChildren5To14Years,
                Translations.LeishMontIntvPrcntOfNewCLCasesInAdultsGrtrThn14Years, LeishMonthlyIntvReport);
            AddAgeGroupDistToExport(XlsWorksheet, XlsRange, "K18", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfNewPKDLCasesInChildrenLssThn5Years, Translations.LeishMontIntvPrcntOfNewPKDLCasesInChildren5To14Years,
                Translations.LeishMontIntvPrcntOfNewPKDLCasesInAdultsGrtrThn14Years, LeishMonthlyIntvReport);
            AddAgeGroupDistToExport(XlsWorksheet, XlsRange, "M18", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfNewMCLCasesInChildrenLssThn5Years, Translations.LeishMontIntvPrcntOfNewMCLCasesInChildren5To14Years,
                Translations.LeishMontIntvPrcntOfNewMCLCasesInAdultsGrtrThn14Years, LeishMonthlyIntvReport);
            // 2nd level admin endemic
            AddValueToRange(XlsWorksheet, XlsRange, "G19", questions.LeishRepEndemicAdmin2Vl.HasValue ? questions.LeishRepEndemicAdmin2Vl.Value.ToString() : "");
            AddValueToRange(XlsWorksheet, XlsRange, "I19", questions.LeishRepEndemicAdmin2Cl.HasValue ? questions.LeishRepEndemicAdmin2Cl.Value.ToString() : "");
            // TODO Pop at risk
            // Outbreak
            AddReportValueToExport(XlsWorksheet, XlsRange, "G21", LeishDd.Disease.DisplayName,
                Translations.LeishDiseaseDistWasThereAnyVLOutbreakThisYear, LeishCountryAggDdReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I21", LeishDd.Disease.DisplayName,
                Translations.LeishDiseaseDistWasThereAnyCLOutbreakThisYear, LeishCountryAggDdReport);
            // Foci
            AddReportValueToExport(XlsWorksheet, XlsRange, "G22", LeishDd.Disease.DisplayName,
                Translations.LeishDiseaseDistNumberOfNewVLFociThisYearAreasReportingCasesForTheFirstTime, LeishCountryAggDdReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I22", LeishDd.Disease.DisplayName,
                Translations.LeishDiseaseDistNumberOfNewCLFociThisYearAreasReportingCasesForTheFirstTime, LeishCountryAggDdReport);
        }

        public void AddControl(LeishReportQuestions questions)
        {
            // Year established
            AddValueToRange(XlsWorksheet, XlsRange, "F60", questions.LeishRepYearLncpEstablished.HasValue ? questions.LeishRepYearLncpEstablished.Value.ToString() : "");
            // URL LNCP
            AddValueToRange(XlsWorksheet, XlsRange, "J60", !string.IsNullOrEmpty(questions.LeishRepUrlLncp) ? questions.LeishRepUrlLncp : "");
            // Year latest guidelines
            AddValueToRange(XlsWorksheet, XlsRange, "F61", questions.LeishRepYearLatestGuide.HasValue ? questions.LeishRepYearLatestGuide.Value.ToString() : "");
            // Is notifiable
            AddValueToRange(XlsWorksheet, XlsRange, "M61", questions.LeishRepIsNotifiable ? "Yes" : "No");
            // Vector
            AddValueToRange(XlsWorksheet, XlsRange, "F62", questions.LeishRepIsVectProg ? "Yes" : "No");
            // Host
            AddValueToRange(XlsWorksheet, XlsRange, "M62", questions.LeishRepIsHostProg ? "Yes" : "No");
            // Type of insesticide IRS
            AddReportValueToExport(XlsWorksheet, XlsRange, "F63", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvTypeOfInsecticideUsedForIndoorResidualSpryaing, LeishAnnualIntvReport);
            // Number of facilities
            AddValueToRange(XlsWorksheet, XlsRange, "M63", questions.LeishRepNumHealthFac.HasValue ? questions.LeishRepNumHealthFac.Value.ToString() : "");
        }

        public void AddDiagnosis()
        {
            // Number screened actively
            AddReportValueToExport(XlsWorksheet, XlsRange, "G67", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedActivelyForVL, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I67", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedActivelyForCL, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "K67", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedActivelyForPKDL, LeishMonthlyIntvReport);
            // Number screened passively
            AddReportValueToExport(XlsWorksheet, XlsRange, "G68", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedPassivelyForVL, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I68", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedPassivelyForCL, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "K68", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedPassivelyForPKDL, LeishMonthlyIntvReport);
            // VL Cases diagnosed by RDT
            AddReportValueToExport(XlsWorksheet, XlsRange, "G69", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfVLCasesDiagnosedByRDT, LeishMonthlyIntvReport);
            // Proportion of positive RDT
            AddReportValueToExport(XlsWorksheet, XlsRange, "G70", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfPostitiveRDT, LeishMonthlyIntvReport);
            // Cases diagnosed by direct exam
            AddReportValueToExport(XlsWorksheet, XlsRange, "G71", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfVLParasitologicallyConfirmedCases, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I71", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfCLParasitologicallyConfirmedCases, LeishMonthlyIntvReport);
            // Proportion of positive slides
            AddReportValueToExport(XlsWorksheet, XlsRange, "G72", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfParasitologicallyConfirmedVLSamples, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I72", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfParasitologicallyConfirmedCLSamples, LeishMonthlyIntvReport);
            // Cases diangosed clinically
            string clinicalVl = CalcBase.GetPercentageWithRatio(
                GetValueFromCountryAggReport(LeishMonthlyIntvType.IntvTypeName, Translations.LeishMontIntvNumberOfCasesDiagnosedClinicallyForVL, LeishMonthlyIntvReport),
                GetValueFromCountryAggReport(LeishMonthlyIntvType.IntvTypeName, Translations.LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical, LeishMonthlyIntvReport)
                );
            AddValueToRange(XlsWorksheet, XlsRange, "G73", clinicalVl);
            string clinicalCl = CalcBase.GetPercentageWithRatio(
                GetValueFromCountryAggReport(LeishMonthlyIntvType.IntvTypeName, Translations.LeishMontIntvNumberOfClinicalCutaneousLeishmaniasisCLCases, LeishMonthlyIntvReport),
                GetValueFromCountryAggReport(LeishMonthlyIntvType.IntvTypeName, Translations.LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical, LeishMonthlyIntvReport)
                );
            AddValueToRange(XlsWorksheet, XlsRange, "I73", clinicalCl);
            // Months elased
            AddReportValueToExport(XlsWorksheet, XlsRange, "G74", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvMonthsElapsedBetweenOnsetOfSymptomsAndDiagnosisMedianForVL, LeishAnnualIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I74", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvMonthsElapsedBetweenOnsetOfSymptomsAndDiagnosisMedianForCL, LeishAnnualIntvReport);
            // Percentage of cases with HIV co-infection
            AddReportValueToExport(XlsWorksheet, XlsRange, "G75", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfVLHIVCoInfectedCasesOfTheTotalNewVLCases, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "K75", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfPKDLHIVCoInfectedCasesOfTheTotalNewPKDLCases, LeishMonthlyIntvReport);
        }

        public void AddTreatment(LeishReportQuestions questions)
        {
            // Treatment free in public sector
            AddValueToRange(XlsWorksheet, XlsRange, "I78", questions.LeishRepIsTreatFree ? "Yes" : "No");
            // Antileish medicines
            AddValueToRange(XlsWorksheet, XlsRange, "I79", !string.IsNullOrEmpty(questions.LeishRepAntiMedInNml) ? questions.LeishRepAntiMedInNml : "");
            // Number of relapses
            AddReportValueToExport(XlsWorksheet, XlsRange, "G82", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfVLRelapseCases, LeishAnnualIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I82", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfCLRelapseCases, LeishAnnualIntvReport);
            // Number of cases treated
            AddReportValueToExport(XlsWorksheet, XlsRange, "G83", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvNumberOfNewVLCasesTreated, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I83", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvNumberOfNewCLCasesTreated, LeishMonthlyIntvReport);
            // Initial cure rate
            AddReportValueToExport(XlsWorksheet, XlsRange, "G84", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForVL, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I84", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForCL, LeishMonthlyIntvReport);
            // Failure rate
            AddReportValueToExport(XlsWorksheet, XlsRange, "G85", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForVL, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I85", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForCL, LeishMonthlyIntvReport);
            // Case fatality rate
            AddReportValueToExport(XlsWorksheet, XlsRange, "G86", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntCaseFatalityRateForVL, LeishMonthlyIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I86", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntCaseFatalityRateForCL, LeishMonthlyIntvReport);
            // Number of cases followed up at least 6 months
            AddReportValueToExport(XlsWorksheet, XlsRange, "G87", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfNewVLCasesFollowedUpAtLeast6Months, LeishAnnualIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I87", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfNewCLCasesFollowedUpAtLeast6Months, LeishAnnualIntvReport);
            // Cure rate 6 months
            AddReportValueToExport(XlsWorksheet, XlsRange, "G88", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvOfNewVLCasesCuredOutOfNewCasesFollowedUp, LeishAnnualIntvReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I88", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvOfNewCLCasesCuredOutOfNewCasesFollowedUp, LeishAnnualIntvReport);
            // Relapse definition
            AddValueToRange(XlsWorksheet, XlsRange, "F89", !string.IsNullOrEmpty(questions.LeishRepRelapseDefVl) ? questions.LeishRepRelapseDefVl : "");
            AddValueToRange(XlsWorksheet, XlsRange, "F90", !string.IsNullOrEmpty(questions.LeishRepRelapseDefCl) ? questions.LeishRepRelapseDefCl : "");
            // Failure definition
            AddValueToRange(XlsWorksheet, XlsRange, "F91", !string.IsNullOrEmpty(questions.LeishRepFailureDefVl) ? questions.LeishRepFailureDefVl : "");
            AddValueToRange(XlsWorksheet, XlsRange, "F92", !string.IsNullOrEmpty(questions.LeishRepFailureDefCl) ? questions.LeishRepFailureDefCl : "");
        }

        private string GetValueFromCountryAggReport(string formName, string fieldName, ReportResult report)
        {
            string colName;
            if (string.IsNullOrEmpty(formName))
                colName = fieldName;
            else
                colName = string.Format("{0} - {1}", fieldName, formName);

            if (report.DataTableResults.Columns.Contains(colName))
            {
                // Aggregated to country, so check first row
                DataRow row = report.DataTableResults.Rows[0];
                // Get the value
                return row[colName].ToString();
            }
            return null;
        }

        private void AddReportValueToExport(excel.Worksheet xlsWorksheet, excel.Range rng, string cell, string formName, string fieldName, ReportResult report)
        {
            string val = GetValueFromCountryAggReport(formName, fieldName, report);
            if (!string.IsNullOrEmpty(val))
                AddValueToRange(xlsWorksheet, rng, cell, val);
        }

        private void AddAgeGroupDistToExport(excel.Worksheet xlsWorksheet, excel.Range rng, string cell, string formName,
            string fieldNameGroup1, string fieldNameGroup2, string fieldNameGroup3, ReportResult report)
        {
            // Get the values
            string val1 = GetValueFromCountryAggReport(formName, fieldNameGroup1, report);
            if (string.IsNullOrEmpty(val1))
                val1 = "--";
            string val2 = GetValueFromCountryAggReport(formName, fieldNameGroup2, report);
            if (string.IsNullOrEmpty(val2))
                val2 = "--";
            string val3 = GetValueFromCountryAggReport(formName, fieldNameGroup3, report);
            if (string.IsNullOrEmpty(val3))
                val3 = "--";
            // Concat all the values
            string final = string.Format("{0}% / {1}% / {2}%", val1, val2, val3);
            // Add to the export
            AddValueToRange(xlsWorksheet, rng, cell, final);
        }

        private static string CalculatePercent(double? n, double? d)
        {
            if (!n.HasValue || !d.HasValue)
                return "--";
            return string.Format("{0:0.00}", n.Value / d.Value * 100);
        }

    }
}
