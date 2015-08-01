using Nada.Globalization;
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
        DemoRepository DemoRepo = new DemoRepository();
        SettingsRepository SettingsRepo = new SettingsRepository();
        IntvRepository IntvRepo = new IntvRepository();

        IntvReportGenerator IntvReportGen = new IntvReportGenerator();

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

                // Determine the start and end of the year
                DateTime start = new DateTime(questions.LeishRepYearReporting.Value, 1, 1);
                DateTime end = new DateTime(questions.LeishRepYearReporting.Value, 12, 31);

                // Get country data
                Country country = DemoRepo.GetCountry();
                CountryDemography countryStats = DemoRepo.GetCountryLevelStatsRecent();
                // Add country data
                AddCountryInfo(xlsWorksheet, rng, country, countryStats, questions);

                // Run Leish monthly intervention report data
                IntvType leishMonthlyType = IntvRepo.GetIntvType((int)StaticIntvType.LeishMonthly);
                // Report options
                ReportOptions leishMonthlyReportOptions = new ReportOptions { MonthYearStarts = 1, StartDate = start, EndDate = end, IsCountryAggregation = true, IsByLevelAggregation = false, IsAllLocations = false, IsNoAggregation = false };
                foreach (var indicator in leishMonthlyType.Indicators)
                    //if (!indicator.Value.IsCalculated)
                        leishMonthlyReportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(leishMonthlyType.Id, indicator));
                ReportResult leishMonthlyResult = IntvReportGen.Run(new SavedReport { ReportOptions = leishMonthlyReportOptions });

                // Run Leish annual intervention report data
                IntvType leishAnnualType = IntvRepo.GetIntvType((int)StaticIntvType.LeishAnnual);
                ReportOptions leishAnnualReportOptions = new ReportOptions { MonthYearStarts = 1, StartDate = start, EndDate = end, IsCountryAggregation = true, IsByLevelAggregation = false, IsAllLocations = false, IsNoAggregation = false };
                foreach (var indicator in leishAnnualType.Indicators)
                    //if (!indicator.Value.IsCalculated)
                        leishAnnualReportOptions.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(leishAnnualType.Id, indicator));
                ReportResult leishAnnualResult = IntvReportGen.Run(new SavedReport { ReportOptions = leishAnnualReportOptions });

                // Add epi
                AddEpi(xlsWorksheet, rng, leishMonthlyType, leishMonthlyResult, leishAnnualType, leishAnnualResult);

                // Control
                AddControl(xlsWorksheet, rng, questions, leishAnnualType, leishAnnualResult);

                // Diagnosis
                AddDiagnosis(xlsWorksheet, rng, leishMonthlyType, leishMonthlyResult, leishAnnualType, leishAnnualResult);

                // Treatment
                AddTreatment(xlsWorksheet, rng, questions, leishMonthlyType, leishMonthlyResult, leishAnnualType, leishAnnualResult);

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
            // Income status
            AddValueToRange(xlsWorksheet, rng, "D9", countryStats.CountryIncomeStatus);
            // GDP
            AddValueToRange(xlsWorksheet, rng, "D8", countryStats.GrossDomesticProduct);
            // Gender ratio
            string femalePercent = CalculatePercent(countryStats.PopFemale, countryStats.TotalPopulation);
            string malePercent = CalculatePercent(countryStats.PopMale, countryStats.TotalPopulation);
            string genderRatio = string.Format("{0}% female - {1}% male", femalePercent, malePercent);
            AddValueToRange(xlsWorksheet, rng, "D7", genderRatio);
            // Population age
            string ageRatio = string.Format("{0}%/{1}%/{2}%", countryStats.PercentPsac, countryStats.PercentSac, countryStats.PercentAdult);
            AddValueToRange(xlsWorksheet, rng, "L6", ageRatio);
            // Life expectancy
            string lifeExpectancy = string.Format("female: {0}, male: {1}", countryStats.LifeExpectBirthFemale, countryStats.LifeExpectBirthMale);
            AddValueToRange(xlsWorksheet, rng, "J7", lifeExpectancy);
            // Name of admin 2 level
            AdminLevelType adminLevel2 = SettingsRepo.GetAdminLevelTypeByLevel(2);
            if (adminLevel2 != null)
                AddValueToRange(xlsWorksheet, rng, "M8", adminLevel2.DisplayName);
            // Number of admin level 2
            List<AdminLevel> admin2Levels = DemoRepo.GetAdminLevelByLevel(2);
            if (admin2Levels != null)
                AddValueToRange(xlsWorksheet, rng, "K8", admin2Levels.Count);
        }

        public void AddEpi(excel.Worksheet xlsWorksheet, excel.Range rng, IntvType leishMonthlyType, ReportResult leishMonthlyResult, IntvType leishAnnualType, ReportResult leishAnnualResult)
        {
            // Endemicity
            // Number of new cases (incidence)
            AddReportValueToExport(xlsWorksheet, rng, "G14", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "I14", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "K14", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "M14", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvTotalNumberOfNewMCLCasesDiagnosed, leishMonthlyResult);
            // Number of imported new cases
            AddReportValueToExport(xlsWorksheet, rng, "G15", leishAnnualType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfImportedVLCases, leishAnnualResult);
            AddReportValueToExport(xlsWorksheet, rng, "I15", leishAnnualType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfImportedCLCases, leishAnnualResult);
            // Incidence rate (calc)
            AddReportValueToExport(xlsWorksheet, rng, "G16", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvVLIncidenceRate10000PeopleYear, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "I16", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvCLIncidenceRate10000PeopleYear, leishMonthlyResult);
            // Gender distribution (%)
            AddReportValueToExport(xlsWorksheet, rng, "G17", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntFemaleVL, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "I17", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntFemaleCL, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "K17", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntFemalePKDL, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "M17", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntFemaleMCL, leishMonthlyResult);
            // Age group Distribution
            AddAgeGroupDistToExport(xlsWorksheet, rng, "G18", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfNewVLCasesInChildrenLssThn5Years, Translations.LeishMontIntvPrcntOfNewVLCasesInChildren5To14Years,
                Translations.LeishMontIntvPrcntOfNewVLCasesInAdultsGrtrThn14Years, leishMonthlyResult);
            AddAgeGroupDistToExport(xlsWorksheet, rng, "I18", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfNewCLCasesInChildrenLssThn5Years, Translations.LeishMontIntvPrcntOfNewCLCasesInChildren5To14Years,
                Translations.LeishMontIntvPrcntOfNewCLCasesInAdultsGrtrThn14Years, leishMonthlyResult);
            AddAgeGroupDistToExport(xlsWorksheet, rng, "K18", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfNewPKDLCasesInChildrenLssThn5Years, Translations.LeishMontIntvPrcntOfNewPKDLCasesInChildren5To14Years,
                Translations.LeishMontIntvPrcntOfNewPKDLCasesInAdultsGrtrThn14Years, leishMonthlyResult);
            AddAgeGroupDistToExport(xlsWorksheet, rng, "M18", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfNewMCLCasesInChildrenLssThn5Years, Translations.LeishMontIntvPrcntOfNewMCLCasesInChildren5To14Years,
                Translations.LeishMontIntvPrcntOfNewMCLCasesInAdultsGrtrThn14Years, leishMonthlyResult);
            // TODO Pop at risk, outbreak, foci
        }

        public void AddControl(excel.Worksheet xlsWorksheet, excel.Range rng, LeishReportQuestions questions, IntvType leishAnnualType, ReportResult leishAnnualResult)
        {
            // Year established
            AddValueToRange(xlsWorksheet, rng, "F60", questions.LeishRepYearLncpEstablished.HasValue ? questions.LeishRepYearLncpEstablished.Value.ToString() : "");
            // URL LNCP
            AddValueToRange(xlsWorksheet, rng, "J60", !string.IsNullOrEmpty(questions.LeishRepUrlLncp) ? questions.LeishRepUrlLncp : "");
            // Year latest guidelines
            AddValueToRange(xlsWorksheet, rng, "F61", questions.LeishRepYearLatestGuide.HasValue ? questions.LeishRepYearLatestGuide.Value.ToString() : "");
            // Is notifiable
            AddValueToRange(xlsWorksheet, rng, "M61", questions.LeishRepIsNotifiable ? "Yes" : "No");
            // Vector
            AddValueToRange(xlsWorksheet, rng, "F62", questions.LeishRepIsVectProg ? "Yes" : "No");
            // Host
            AddValueToRange(xlsWorksheet, rng, "M62", questions.LeishRepIsHostProg ? "Yes" : "No");
            // Type of insesticide IRS
            AddReportValueToExport(xlsWorksheet, rng, "F63", leishAnnualType.IntvTypeName,
                Translations.LeishAnnIntvTypeOfInsecticideUsedForIndoorResidualSpryaing, leishAnnualResult);
            // Number of facilities
            AddValueToRange(xlsWorksheet, rng, "M63", questions.LeishRepNumHealthFac.HasValue ? questions.LeishRepNumHealthFac.Value.ToString() : "");
        }

        public void AddDiagnosis(excel.Worksheet xlsWorksheet, excel.Range rng, IntvType leishMonthlyType, ReportResult leishMonthlyResult, IntvType leishAnnualType, ReportResult leishAnnualResult)
        {
            // Number screened actively
            AddReportValueToExport(xlsWorksheet, rng, "G67", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedActivelyForVL, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "I67", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedActivelyForCL, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "K67", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedActivelyForPKDL, leishMonthlyResult);
            // Number screened passively
            AddReportValueToExport(xlsWorksheet, rng, "G68", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedPassivelyForVL, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "I68", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedPassivelyForCL, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "K68", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvNumberOfPeopleScreenedPassivelyForPKDL, leishMonthlyResult);
            // VL Cases diagnosed by RDT
            AddReportValueToExport(xlsWorksheet, rng, "G69", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfVLCasesDiagnosedByRDT, leishMonthlyResult);
            // Proportion of positive RDT
            AddReportValueToExport(xlsWorksheet, rng, "G70", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfPostitiveRDT, leishMonthlyResult);
            // Cases diagnosed by direct exam
            AddReportValueToExport(xlsWorksheet, rng, "G71", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfVLParasitologicallyConfirmedCases, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "I71", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfCLParasitologicallyConfirmedCases, leishMonthlyResult);
            // Proportion of positive slides
            AddReportValueToExport(xlsWorksheet, rng, "G72", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfParasitologicallyConfirmedVLSamples, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "I72", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfParasitologicallyConfirmedCLSamples, leishMonthlyResult);
            // Cases diangosed clinically
            string clinicalVl = CalcBase.GetPercentageWithRatio(
                GetValueFromCountryAggReport(leishMonthlyType.IntvTypeName, Translations.LeishMontIntvNumberOfCasesDiagnosedClinicallyForVL, leishMonthlyResult),
                GetValueFromCountryAggReport(leishMonthlyType.IntvTypeName, Translations.LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical, leishMonthlyResult)
                );
            AddValueToRange(xlsWorksheet, rng, "G73", clinicalVl);
            string clinicalCl = CalcBase.GetPercentageWithRatio(
                GetValueFromCountryAggReport(leishMonthlyType.IntvTypeName, Translations.LeishMontIntvNumberOfClinicalCutaneousLeishmaniasisCLCases, leishMonthlyResult),
                GetValueFromCountryAggReport(leishMonthlyType.IntvTypeName, Translations.LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical, leishMonthlyResult)
                );
            AddValueToRange(xlsWorksheet, rng, "I73", clinicalCl);
            // Months elased
            AddReportValueToExport(xlsWorksheet, rng, "G74", leishAnnualType.IntvTypeName,
                Translations.LeishAnnIntvMonthsElapsedBetweenOnsetOfSymptomsAndDiagnosisMedianForVL, leishAnnualResult);
            AddReportValueToExport(xlsWorksheet, rng, "I74", leishAnnualType.IntvTypeName,
                Translations.LeishAnnIntvMonthsElapsedBetweenOnsetOfSymptomsAndDiagnosisMedianForCL, leishAnnualResult);
            // Percentage of cases with HIV co-infection
            AddReportValueToExport(xlsWorksheet, rng, "G75", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfVLHIVCoInfectedCasesOfTheTotalNewVLCases, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "K75", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfPKDLHIVCoInfectedCasesOfTheTotalNewPKDLCases, leishMonthlyResult);
        }

        public void AddTreatment(excel.Worksheet xlsWorksheet, excel.Range rng, LeishReportQuestions questions,
            IntvType leishMonthlyType, ReportResult leishMonthlyResult, IntvType leishAnnualType, ReportResult leishAnnualResult)
        {
            // Treatment free in public sector
            AddValueToRange(xlsWorksheet, rng, "I78", questions.LeishRepIsTreatFree ? "Yes" : "No");
            // Antileish medicines
            AddValueToRange(xlsWorksheet, rng, "I79", !string.IsNullOrEmpty(questions.LeishRepAntiMedInNml) ? questions.LeishRepAntiMedInNml : "");
            // Number of relapses
            AddReportValueToExport(xlsWorksheet, rng, "G82", leishAnnualType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfVLRelapseCases, leishAnnualResult);
            AddReportValueToExport(xlsWorksheet, rng, "I82", leishAnnualType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfCLRelapseCases, leishAnnualResult);
            // Number of cases treated
            AddReportValueToExport(xlsWorksheet, rng, "G83", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvNumberOfNewVLCasesTreated, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "I83", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvNumberOfNewCLCasesTreated, leishMonthlyResult);
            // Initial cure rate
            AddReportValueToExport(xlsWorksheet, rng, "G84", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForVL, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "I84", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForCL, leishMonthlyResult);
            // Failure rate
            AddReportValueToExport(xlsWorksheet, rng, "G85", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForVL, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "I85", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForCL, leishMonthlyResult);
            // Case fatality rate
            AddReportValueToExport(xlsWorksheet, rng, "G86", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntCaseFatalityRateForVL, leishMonthlyResult);
            AddReportValueToExport(xlsWorksheet, rng, "I86", leishMonthlyType.IntvTypeName,
                Translations.LeishMontIntvPrcntCaseFatalityRateForCL, leishMonthlyResult);
            // Number of cases followed up at least 6 months
            AddReportValueToExport(xlsWorksheet, rng, "G87", leishAnnualType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfNewVLCasesFollowedUpAtLeast6Months, leishAnnualResult);
            AddReportValueToExport(xlsWorksheet, rng, "I87", leishAnnualType.IntvTypeName,
                Translations.LeishAnnIntvNumberOfNewCLCasesFollowedUpAtLeast6Months, leishAnnualResult);
            // Cure rate 6 months
            AddReportValueToExport(xlsWorksheet, rng, "G88", leishAnnualType.IntvTypeName,
                Translations.LeishAnnIntvOfNewVLCasesCuredOutOfNewCasesFollowedUp, leishAnnualResult);
            AddReportValueToExport(xlsWorksheet, rng, "I88", leishAnnualType.IntvTypeName,
                Translations.LeishAnnIntvOfNewCLCasesCuredOutOfNewCasesFollowedUp, leishAnnualResult);
            // Relapse definition
            AddValueToRange(xlsWorksheet, rng, "F89", !string.IsNullOrEmpty(questions.LeishRepRelapseDefVl) ? questions.LeishRepRelapseDefVl : "");
            AddValueToRange(xlsWorksheet, rng, "F90", !string.IsNullOrEmpty(questions.LeishRepRelapseDefCl) ? questions.LeishRepRelapseDefCl : "");
            // Failure definition
            AddValueToRange(xlsWorksheet, rng, "F91", !string.IsNullOrEmpty(questions.LeishRepFailureDefVl) ? questions.LeishRepFailureDefVl : "");
            AddValueToRange(xlsWorksheet, rng, "F92", !string.IsNullOrEmpty(questions.LeishRepFailureDefCl) ? questions.LeishRepFailureDefCl : "");
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
