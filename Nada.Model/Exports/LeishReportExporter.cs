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
        ReportResult LeishMonthlyIntvCountryAggReport;
        ReportResult LeishAnnualIntvCountryAggReport;
        ReportResult LeishMonthlyIntvNoAggReport;

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

                // Run Leish monthly intervention with country agg report data
                RunLeishMonthlyIntvCountryAggReport();

                // Run Leish annual intervention with country agg report data
                RunLeishAnnualIntvCountryAggReport();

                // Run Leish monthly intervention report with no agg
                RunLeishMonthlyIntvNoAggReport();

                // Run the Leish disease distribution report for the country
                RunLeishCountryAggDdReport();

                // Add epi
                AddEpi(questions);

                // Add monthly dist
                AddMonthlyDist();

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

        private void RunLeishMonthlyIntvCountryAggReport()
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
            LeishMonthlyIntvCountryAggReport = IntvReportGen.Run(new SavedReport { ReportOptions = reportOptions });
        }

        private void RunLeishAnnualIntvCountryAggReport()
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
            LeishAnnualIntvCountryAggReport = IntvReportGen.Run(new SavedReport { ReportOptions = reportOptions });
        }

        private void RunLeishMonthlyIntvNoAggReport()
        {
            // Report options
            ReportOptions reportOptions = new ReportOptions
            {
                MonthYearStarts = 1,
                StartDate = StartDate,
                EndDate = EndDate,
                IsCountryAggregation = false,
                IsByLevelAggregation = false,
                IsAllLocations = true,
                IsNoAggregation = true
            };
            // Add the indicators from the intervention type
            foreach (var indicator in LeishMonthlyIntvType.Indicators)
                reportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(LeishMonthlyIntvType.Id, indicator));
            // Run the report
            LeishMonthlyIntvNoAggReport = IntvReportGen.Run(new SavedReport { ReportOptions = reportOptions });
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
                Translations.LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I14", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "K14", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "M14", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvTotalNumberOfNewMCLCasesDiagnosed, LeishMonthlyIntvCountryAggReport);
            // Number of imported new cases
            AddReportValueToExport(XlsWorksheet, XlsRange, "G15", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfImportedVLCases, LeishAnnualIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I15", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfImportedCLCases, LeishAnnualIntvCountryAggReport);
            // Incidence rate (calc)
            AddReportValueToExport(XlsWorksheet, XlsRange, "G16", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvVLIncidenceRate10000PeopleYear, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I16", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvCLIncidenceRate10000PeopleYear, LeishMonthlyIntvCountryAggReport);
            // Gender distribution (%)
            AddReportValueToExport(XlsWorksheet, XlsRange, "G17", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntFemaleVL, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I17", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntFemaleCL, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "K17", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntFemalePKDL, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "M17", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntFemaleMCL, LeishMonthlyIntvCountryAggReport);
            // Age group Distribution
            AddAgeGroupDistToExport(XlsWorksheet, XlsRange, "G18", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfNewVLCasesInChildrenLssThn5Years, Translations.LeishMontIntvPrcntOfNewVLCasesInChildren5To14Years,
                Translations.LeishMontIntvPrcntOfNewVLCasesInAdultsGrtrThn14Years, LeishMonthlyIntvCountryAggReport);
            AddAgeGroupDistToExport(XlsWorksheet, XlsRange, "I18", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfNewCLCasesInChildrenLssThn5Years, Translations.LeishMontIntvPrcntOfNewCLCasesInChildren5To14Years,
                Translations.LeishMontIntvPrcntOfNewCLCasesInAdultsGrtrThn14Years, LeishMonthlyIntvCountryAggReport);
            AddAgeGroupDistToExport(XlsWorksheet, XlsRange, "K18", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfNewPKDLCasesInChildrenLssThn5Years, Translations.LeishMontIntvPrcntOfNewPKDLCasesInChildren5To14Years,
                Translations.LeishMontIntvPrcntOfNewPKDLCasesInAdultsGrtrThn14Years, LeishMonthlyIntvCountryAggReport);
            AddAgeGroupDistToExport(XlsWorksheet, XlsRange, "M18", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfNewMCLCasesInChildrenLssThn5Years, Translations.LeishMontIntvPrcntOfNewMCLCasesInChildren5To14Years,
                Translations.LeishMontIntvPrcntOfNewMCLCasesInAdultsGrtrThn14Years, LeishMonthlyIntvCountryAggReport);
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

        public void AddMonthlyDist()
        {
            // Will hold the Total number of new VL/CL cases diagnosed (lab and clinical) where the key is the month and the value is the case count
            Dictionary<int, double> vlCases = new Dictionary<int, double>();
            Dictionary<int, double> clCases = new Dictionary<int, double>();
            // Set up the initial values
            for (int i = 1; i < 13; i++)
            {
                vlCases.Add(i, 0);
                clCases.Add(i, 0);
            }
            // Iterate through the intervention report data
            foreach (DataRow row in LeishMonthlyIntvNoAggReport.DataTableResults.Rows)
            {
                // Get the date reported
                string dateString = row[string.Format("{0} - {1}", Translations.DateReported, LeishMonthlyIntvType.IntvTypeName)].ToString();
                DateTime date;
                if (string.IsNullOrEmpty(dateString) || !DateTime.TryParse(dateString, out date))
                    continue;

                // Determine the month
                int month = date.Month;

                // Get the number of VL/CL cases
                double vlCaseCount = GetColumnDouble(string.Format("{0} - {1}", Translations.LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical,
                    LeishMonthlyIntvType.IntvTypeName), LeishMonthlyIntvNoAggReport.DataTableResults, row);
                double clCaseCount = GetColumnDouble(string.Format("{0} - {1}", Translations.LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical,
                    LeishMonthlyIntvType.IntvTypeName), LeishMonthlyIntvNoAggReport.DataTableResults, row);

                // Add to the collection
                vlCases[month] = vlCases[month] + vlCaseCount;
                clCases[month] = clCases[month] + clCaseCount;
            }

            // Map out the monthly case count to their cells
            Dictionary<int, string> vlCaseMap = new Dictionary<int, string>()
            {
                {1, "C26"}, {2, "D26"}, {3, "E26"}, {4, "F26"}, {5, "G26"}, {6, "H26"},
                {7, "I26"}, {8, "J26"}, {9, "K26"}, {10, "L26"}, {11, "M26"}, {12, "N26"}
            };
            Dictionary<int, string> clCaseMap = new Dictionary<int, string>()
            {
                {1, "C27"}, {2, "D27"}, {3, "E27"}, {4, "F27"}, {5, "G27"}, {6, "H27"},
                {7, "I27"}, {8, "J27"}, {9, "K27"}, {10, "L27"}, {11, "M27"}, {12, "N27"}
            };
            // Add the values to the report
            for (int i = 1; i < 13; i++)
            {
                AddValueToRange(XlsWorksheet, XlsRange, vlCaseMap[i], vlCases[i]);
                AddValueToRange(XlsWorksheet, XlsRange, clCaseMap[i], clCases[i]);
            }
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
                Translations.LeishAnnIntvTypeOfInsecticideUsedForIndoorResidualSpryaing, LeishAnnualIntvCountryAggReport);
            // Number of facilities
            AddValueToRange(XlsWorksheet, XlsRange, "M63", questions.LeishRepNumHealthFac.HasValue ? questions.LeishRepNumHealthFac.Value.ToString() : "");
        }

        public void AddDiagnosis()
        {
            // Number screened actively
            AddReportValueToExport(XlsWorksheet, XlsRange, "G67", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedActivelyForVL, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I67", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedActivelyForCL, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "K67", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedActivelyForPKDL, LeishMonthlyIntvCountryAggReport);
            // Number screened passively
            AddReportValueToExport(XlsWorksheet, XlsRange, "G68", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedPassivelyForVL, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I68", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedPassivelyForCL, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "K68", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedPassivelyForPKDL, LeishMonthlyIntvCountryAggReport);
            // VL Cases diagnosed by RDT
            AddReportValueToExport(XlsWorksheet, XlsRange, "G69", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfVLCasesDiagnosedByRDT, LeishMonthlyIntvCountryAggReport);
            // Proportion of positive RDT
            AddReportValueToExport(XlsWorksheet, XlsRange, "G70", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfPostitiveRDT, LeishMonthlyIntvCountryAggReport);
            // Cases diagnosed by direct exam
            AddReportValueToExport(XlsWorksheet, XlsRange, "G71", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfVLParasitologicallyConfirmedCases, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I71", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfCLParasitologicallyConfirmedCases, LeishMonthlyIntvCountryAggReport);
            // Proportion of positive slides
            AddReportValueToExport(XlsWorksheet, XlsRange, "G72", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfParasitologicallyConfirmedVLSamples, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I72", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfParasitologicallyConfirmedCLSamples, LeishMonthlyIntvCountryAggReport);
            // Cases diangosed clinically
            string clinicalVl = CalcBase.GetPercentageWithRatio(
                GetValueFromCountryAggReport(LeishMonthlyIntvType.IntvTypeName, Translations.LeishMontIntvNumberOfCasesDiagnosedClinicallyForVL, LeishMonthlyIntvCountryAggReport),
                GetValueFromCountryAggReport(LeishMonthlyIntvType.IntvTypeName, Translations.LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical, LeishMonthlyIntvCountryAggReport)
                );
            AddValueToRange(XlsWorksheet, XlsRange, "G73", clinicalVl);
            string clinicalCl = CalcBase.GetPercentageWithRatio(
                GetValueFromCountryAggReport(LeishMonthlyIntvType.IntvTypeName, Translations.LeishMontIntvNumberOfClinicalCutaneousLeishmaniasisCLCases, LeishMonthlyIntvCountryAggReport),
                GetValueFromCountryAggReport(LeishMonthlyIntvType.IntvTypeName, Translations.LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical, LeishMonthlyIntvCountryAggReport)
                );
            AddValueToRange(XlsWorksheet, XlsRange, "I73", clinicalCl);
            // Months elased
            AddReportValueToExport(XlsWorksheet, XlsRange, "G74", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvMonthsElapsedBetweenOnsetOfSymptomsAndDiagnosisMedianForVL, LeishAnnualIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I74", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvMonthsElapsedBetweenOnsetOfSymptomsAndDiagnosisMedianForCL, LeishAnnualIntvCountryAggReport);
            // Percentage of cases with HIV co-infection
            AddReportValueToExport(XlsWorksheet, XlsRange, "G75", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfVLHIVCoInfectedCasesOfTheTotalNewVLCases, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "K75", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfPKDLHIVCoInfectedCasesOfTheTotalNewPKDLCases, LeishMonthlyIntvCountryAggReport);
        }

        public void AddTreatment(LeishReportQuestions questions)
        {
            // Treatment free in public sector
            AddValueToRange(XlsWorksheet, XlsRange, "I78", questions.LeishRepIsTreatFree ? "Yes" : "No");
            // Antileish medicines
            AddValueToRange(XlsWorksheet, XlsRange, "I79", !string.IsNullOrEmpty(questions.LeishRepAntiMedInNml) ? questions.LeishRepAntiMedInNml : "");
            // Number of relapses
            AddReportValueToExport(XlsWorksheet, XlsRange, "G82", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfVLRelapseCases, LeishAnnualIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I82", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfCLRelapseCases, LeishAnnualIntvCountryAggReport);
            // Number of cases treated
            AddReportValueToExport(XlsWorksheet, XlsRange, "G83", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvNumberOfNewVLCasesTreated, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I83", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvNumberOfNewCLCasesTreated, LeishMonthlyIntvCountryAggReport);
            // Initial cure rate
            AddReportValueToExport(XlsWorksheet, XlsRange, "G84", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForVL, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I84", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForCL, LeishMonthlyIntvCountryAggReport);
            // Failure rate
            AddReportValueToExport(XlsWorksheet, XlsRange, "G85", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForVL, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I85", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForCL, LeishMonthlyIntvCountryAggReport);
            // Case fatality rate
            AddReportValueToExport(XlsWorksheet, XlsRange, "G86", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntCaseFatalityRateForVL, LeishMonthlyIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I86", LeishMonthlyIntvType.IntvTypeName,
                Translations.LeishMontIntvPrcntCaseFatalityRateForCL, LeishMonthlyIntvCountryAggReport);
            // Number of cases followed up at least 6 months
            AddReportValueToExport(XlsWorksheet, XlsRange, "G87", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfNewVLCasesFollowedUpAtLeast6Months, LeishAnnualIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I87", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfNewCLCasesFollowedUpAtLeast6Months, LeishAnnualIntvCountryAggReport);
            // Cure rate 6 months
            AddReportValueToExport(XlsWorksheet, XlsRange, "G88", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvOfNewVLCasesCuredOutOfNewCasesFollowedUp, LeishAnnualIntvCountryAggReport);
            AddReportValueToExport(XlsWorksheet, XlsRange, "I88", LeishAnnualIntvType.IntvTypeName,
                Translations.LeishAnnIntvOfNewCLCasesCuredOutOfNewCasesFollowedUp, LeishAnnualIntvCountryAggReport);
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

        protected double GetColumnDouble(string col, DataTable dataTable, DataRow row)
        {
            double d = 0;
            if (dataTable.Columns.Contains(col))
                if (double.TryParse(row[col].ToString(), out d))
                    return d;
            return 0;
        }

        private static string CalculatePercent(double? n, double? d)
        {
            if (!n.HasValue || !d.HasValue)
                return "--";
            return string.Format("{0:0.00}", n.Value / d.Value * 100);
        }

    }
}
