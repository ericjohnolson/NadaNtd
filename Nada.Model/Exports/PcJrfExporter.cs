using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Nada.Globalization;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using excel = Microsoft.Office.Interop.Excel;

namespace Nada.Model.Exports
{
    public class PcJrfExporter : ExporterBase, IExporter
    {
        SettingsRepository settings = new SettingsRepository();
        DemoRepository demo = new DemoRepository();
        ExportRepository repo = new ExportRepository();
        DiseaseRepository diseaseRepo = new DiseaseRepository();
        IntvRepository intvRepo = new IntvRepository();

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
                excel.Worksheet xlsSummary;
                excel.Range rng = null;
                object missing = System.Reflection.Missing.Value;

                // Open workbook
                xlsWorkbook = xlsApp.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, @"Exports\WHO_JRF_PC.xls"),
                    missing, missing, missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing, missing, missing);

                var districtLevel = questions.AdminLevelType;
                CountryDemography countryStats = demo.GetCountryLevelStatsRecent();
                Country country = demo.GetCountry();
                List<AdminLevel> demography = new List<AdminLevel>();
                DateTime startDate = new DateTime(yearReported, 1, 1);
                DateTime endDate = startDate.AddYears(1).AddDays(-1);
                List<AdminLevel> tree = demo.GetAdminLevelTreeForDemography(districtLevel.LevelNumber, startDate, endDate, ref demography);

                var reportingLevelUnits = demography.Where(d => d.LevelNumber == districtLevel.LevelNumber).ToList();
                Dictionary<int, DataRow> aggIntvs = GetIntvsAggregatedToReportingLevel(startDate, endDate, reportingLevelUnits);

                // Info page
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[1];
                xlsWorksheet.Unprotect();
                AddQuestions(xlsWorksheet, rng, questions, countryStats, demography, districtLevel.LevelNumber, country);
                // run macro to create district rows.
                xlsApp.DisplayAlerts = false;
                xlsApp.Run("Sheet1.DISTRICT");
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[2];
                xlsWorksheet.Unprotect();
                AddDemo(xlsWorksheet, rng, demography, districtLevel.LevelNumber, startDate, endDate);

                // Summary
                xlsSummary = (excel.Worksheet)xlsWorkbook.Sheets["SUMMARY"];

                // ALL THE INTVS
                AddT3(xlsWorkbook, xlsWorksheet, xlsSummary, rng, reportingLevelUnits, aggIntvs);
                AddT2(xlsWorkbook, xlsWorksheet, xlsSummary, rng, reportingLevelUnits, aggIntvs);
                AddT1(xlsWorkbook, xlsWorksheet, xlsSummary, rng, reportingLevelUnits, aggIntvs);
                AddMDA4(xlsWorkbook, xlsWorksheet, xlsSummary, rng, reportingLevelUnits, aggIntvs);
                AddMDA3(xlsWorkbook, xlsWorksheet, xlsSummary, rng, reportingLevelUnits, aggIntvs);
                AddMDA2(xlsWorkbook, xlsWorksheet, xlsSummary, rng, reportingLevelUnits, aggIntvs);
                AddMDA1(xlsWorkbook, xlsWorksheet, xlsSummary, rng, reportingLevelUnits, aggIntvs);
                AddDistricts(xlsWorkbook, xlsWorksheet, xlsSummary, rng, reportingLevelUnits, aggIntvs);

                xlsWorkbook.SaveAs(filePath, excel.XlFileFormat.xlOpenXMLWorkbook, missing,
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

        private void AddQuestions(excel.Worksheet xlsWorksheet, excel.Range rng, ExportJrfQuestions questions, CountryDemography countryDemo,
                List<AdminLevel> demography, int districtLevel, Country country)
        {
            AddValueToRange(xlsWorksheet, rng, "E34", country.Name);
            AddValueToRange(xlsWorksheet, rng, "E36", questions.JrfYearReporting.Value);
            AddValueToRange(xlsWorksheet, rng, "E38", TranslationLookup.GetValue(questions.JrfEndemicLf, questions.JrfEndemicLf));
            AddValueToRange(xlsWorksheet, rng, "E40", TranslationLookup.GetValue(questions.JrfEndemicOncho, questions.JrfEndemicOncho));
            AddValueToRange(xlsWorksheet, rng, "E42", TranslationLookup.GetValue(questions.JrfEndemicSth, questions.JrfEndemicSth));
            AddValueToRange(xlsWorksheet, rng, "E44", TranslationLookup.GetValue(questions.JrfEndemicSch, questions.JrfEndemicSch));
            AddValueToRange(xlsWorksheet, rng, "E46", demography.Where(d => d.LevelNumber == districtLevel).Count());
            if (countryDemo.PercentPsac.HasValue)
                AddValueToRange(xlsWorksheet, rng, "E48", countryDemo.PercentPsac.Value / 100);
            else
                AddValueToRange(xlsWorksheet, rng, "E48", 0);
            if (countryDemo.PercentSac.HasValue)
                AddValueToRange(xlsWorksheet, rng, "E49", countryDemo.PercentSac.Value / 100);
            else
                AddValueToRange(xlsWorksheet, rng, "E49", 0);
            if (countryDemo.PercentAdult.HasValue)
                AddValueToRange(xlsWorksheet, rng, "E50", countryDemo.PercentAdult.Value / 100);
            else
                AddValueToRange(xlsWorksheet, rng, "E50", 0);
        }

        private void AddDemo(excel.Worksheet xlsWorksheet, excel.Range rng, List<AdminLevel> demography, int districtLevel, DateTime start, DateTime end)
        {
            var districts = demography.Where(d => d.LevelNumber == districtLevel).OrderBy(d => d.SortOrder).ToList();
            var ddDict = GetDd(start, end, districts);
            int rowId = 9;
            foreach (var district in districts)
            {
                AdminLevel parent = demography.First(d => d.Id == district.ParentId);
                AddValueToRange(xlsWorksheet, rng, "B" + rowId, parent.Name);
                AddValueToRange(xlsWorksheet, rng, "C" + rowId, district.Name);
                AddValueToRange(xlsWorksheet, rng, "D" + rowId, district.CurrentDemography.TotalPopulation);

                // DISEASE DISTRO
                if (ddDict.ContainsKey(district.Id))
                {
                    if (ddDict[district.Id].Table.Columns.Contains(TranslationLookup.GetValue("DDLFDiseaseDistributionPcInterventions") + " - " + TranslationLookup.GetValue("LF")))
                        AddValueToRange(xlsWorksheet, rng, "H" + rowId, TranslateEndemicity(DiseaseType.Lf, ddDict[district.Id][TranslationLookup.GetValue("DDLFDiseaseDistributionPcInterventions") + " - " + TranslationLookup.GetValue("LF")].ToString()));
                    if (ddDict[district.Id].Table.Columns.Contains(TranslationLookup.GetValue("DDOnchoDiseaseDistributionPcInterventio") + " - " + TranslationLookup.GetValue("Oncho")))
                        AddValueToRange(xlsWorksheet, rng, "I" + rowId, TranslateEndemicity(DiseaseType.Oncho, ddDict[district.Id][TranslationLookup.GetValue("DDOnchoDiseaseDistributionPcInterventio") + " - " + TranslationLookup.GetValue("Oncho")].ToString()));
                    if (ddDict[district.Id].Table.Columns.Contains(TranslationLookup.GetValue("DDSTHDiseaseDistributionPcInterventions") + " - " + TranslationLookup.GetValue("STH")))
                        AddValueToRange(xlsWorksheet, rng, "J" + rowId, TranslateEndemicity(DiseaseType.STH, ddDict[district.Id][TranslationLookup.GetValue("DDSTHDiseaseDistributionPcInterventions") + " - " + TranslationLookup.GetValue("STH")].ToString()));
                    if (ddDict[district.Id].Table.Columns.Contains(TranslationLookup.GetValue("DDSchistoDiseaseDistributionPcIntervent") + " - " + TranslationLookup.GetValue("Schisto")))
                        AddValueToRange(xlsWorksheet, rng, "K" + rowId, TranslateEndemicity(DiseaseType.Schisto, ddDict[district.Id][TranslationLookup.GetValue("DDSchistoDiseaseDistributionPcIntervent") + " - " + TranslationLookup.GetValue("Schisto")].ToString()));

                    if (ddDict[district.Id].Table.Columns.Contains(TranslationLookup.GetValue("DDLFNumPcRoundsYearRecommendedByWhoGuid") + " - " + TranslationLookup.GetValue("LF")))
                        AddValueToRange(xlsWorksheet, rng, "P" + rowId,
                        ddDict[district.Id][TranslationLookup.GetValue("DDLFNumPcRoundsYearRecommendedByWhoGuid") + " - " + TranslationLookup.GetValue("LF")]);
                    if (ddDict[district.Id].Table.Columns.Contains(TranslationLookup.GetValue("DDOnchoNumPcRoundsYearRecommendedByWhoG") + " - " + TranslationLookup.GetValue("Oncho")))
                        AddValueToRange(xlsWorksheet, rng, "Q" + rowId,
                        ddDict[district.Id][TranslationLookup.GetValue("DDOnchoNumPcRoundsYearRecommendedByWhoG") + " - " + TranslationLookup.GetValue("Oncho")]);
                    if (ddDict[district.Id].Table.Columns.Contains(TranslationLookup.GetValue("DDSTHNumPcRoundsYearRecommendedByWhoGui") + " - " + TranslationLookup.GetValue("STH")))
                        AddValueToRange(xlsWorksheet, rng, "R" + rowId,
                       ddDict[district.Id][TranslationLookup.GetValue("DDSTHNumPcRoundsYearRecommendedByWhoGui") + " - " + TranslationLookup.GetValue("STH")]);
                    if (ddDict[district.Id].Table.Columns.Contains(TranslationLookup.GetValue("DDSchistoNumPcRoundsYearRecommendedByWh") + " - " + TranslationLookup.GetValue("Schisto")))
                        AddValueToRange(xlsWorksheet, rng, "S" + rowId,
                       ddDict[district.Id][TranslationLookup.GetValue("DDSchistoNumPcRoundsYearRecommendedByWh") + " - " + TranslationLookup.GetValue("Schisto")]);
                }
                rowId++;
            }
        }

        private void AddT3(excel.Workbook xlsWorkbook, excel.Worksheet xlsWorksheet, excel.Worksheet xlsSummary, excel.Range rng, List<AdminLevel> reportingUnits, Dictionary<int, DataRow> aggIntvs)
        {
            List<string> typeNames = new List<string> { "IntvAlb", "IntvMbd" };
            List<string> typesToCalc = new List<string>();

            // for round 1 and 2
            for (int i = Util.MaxRounds; i >= 1; i--)
            {
                if (!HasRoundNumber(reportingUnits, aggIntvs, typeNames, typesToCalc, i))
                    continue;

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["T3_R1"];
                if (i == 2)
                    xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["T3_R2"];

                if (i > 2)
                {
                    xlsWorksheet.Copy(System.Reflection.Missing.Value, xlsSummary);
                    xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets[xlsSummary.Index + 1];
                    xlsWorksheet.Name = string.Format("T1_R{0}", i);
                }
                int rowCount = 9;
                foreach (var unit in reportingUnits)
                {
                    if (aggIntvs.ContainsKey(unit.Id))
                    {
                        AddValueToRange(xlsWorksheet, rng, "G" + rowCount, GetIntFromRow("PcIntvNumPsacTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "H" + rowCount, GetIntFromRow("PcIntvNumSacTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "I" + rowCount, GetIntFromRow("PcIntvNumAdultsTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "K" + rowCount, GetIntFromRow("PcIntvPsacTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "L" + rowCount, GetIntFromRow("PcIntvNumSacTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "M" + rowCount, GetIntFromRow("PcIntvNumAdultsTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));

                        DateTime? startMda = GetDateFromRow("PcIntvStartDateOfMda", ref typesToCalc, aggIntvs[unit.Id], false, i, i, null, typeNames);
                        if (startMda.HasValue)
                            AddValueToRange(xlsWorksheet, rng, "F" + rowCount, startMda.Value.ToString("MM/dd/yyyy"));

                        if (typesToCalc.Contains("IntvAlb"))
                            AddValueToRange(xlsWorksheet, rng, "E" + rowCount, "ALB");
                        if (typesToCalc.Contains("IntvMbd"))
                            AddValueToRange(xlsWorksheet, rng, "E" + rowCount, "MBD");
                    }

                    rowCount++;
                }
            }
        }

        private void AddT1(excel.Workbook xlsWorkbook, excel.Worksheet xlsWorksheet, excel.Worksheet xlsSummary, excel.Range rng, List<AdminLevel> reportingUnits, Dictionary<int, DataRow> aggIntvs)
        {
            List<string> typeNames = new List<string> { "IntvPzqAlb", "IntvPzqMbd" };
            List<string> typesToCalc = new List<string>();

            for (int i = Util.MaxRounds; i >= 1; i--)
            {
                if (!HasRoundNumber(reportingUnits, aggIntvs, typeNames,  typesToCalc, i))
                    continue;

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["T1"];
                if (i > 1)
                {
                    xlsWorksheet.Copy(System.Reflection.Missing.Value, xlsSummary);
                    xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets[xlsSummary.Index + 1];
                    xlsWorksheet.Name = string.Format("T1_R{0}", i);
                }
                int rowCount = 9;
                foreach (var unit in reportingUnits)
                {
                    if (aggIntvs.ContainsKey(unit.Id))
                    {
                        AddValueToRange(xlsWorksheet, rng, "G" + rowCount, GetIntFromRow("PcIntvNumPsacTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "H" + rowCount, GetIntFromRow("PcIntvNumSacTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "I" + rowCount, GetIntFromRow("PcIntvNumSacTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "J" + rowCount, GetIntFromRow("PcIntvNumAdultsTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));

                        AddValueToRange(xlsWorksheet, rng, "L" + rowCount, GetIntFromRow("PcIntvPsacTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "M" + rowCount, GetIntFromRow("PcIntvNumSacTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "N" + rowCount, GetIntFromRow("PcIntvNumAdultsTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));

                        DateTime? startMda = GetDateFromRow("PcIntvStartDateOfMda", ref typesToCalc, aggIntvs[unit.Id], false, i, i, null, typeNames);
                        if (startMda.HasValue)
                            AddValueToRange(xlsWorksheet, rng, "F" + rowCount, startMda.Value.ToString("MM/dd/yyyy"));

                        if (typesToCalc.Contains("IntvPzqAlb"))
                            AddValueToRange(xlsWorksheet, rng, "E" + rowCount, "PZQ+ALB");
                        if (typesToCalc.Contains("IntvPzqMbd"))
                            AddValueToRange(xlsWorksheet, rng, "E" + rowCount, "PZQ+MBD");
                    }
                    rowCount++;
                }
            }
        }

        private void AddT2(excel.Workbook xlsWorkbook, excel.Worksheet xlsWorksheet, excel.Worksheet xlsSummary, excel.Range rng, List<AdminLevel> reportingUnits, Dictionary<int, DataRow> aggIntvs)
        {
            List<string> typeNames = new List<string> { "IntvIvmPzqAlb", "IntvIvmPzq", "IntvPzq" };
            List<string> typesToCalc = new List<string>();

            for (int i = Util.MaxRounds; i >= 1; i--)
            {
                if (!HasRoundNumber(reportingUnits, aggIntvs, typeNames,  typesToCalc, i))
                    continue;

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["T2"];
                if (i > 1)
                {
                    xlsWorksheet.Copy(System.Reflection.Missing.Value, xlsSummary);
                    xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets[xlsSummary.Index + 1];
                    xlsWorksheet.Name = string.Format("T2_R{0}", i);
                }
                int rowCount = 9;
                foreach (var unit in reportingUnits)
                {
                    if (aggIntvs.ContainsKey(unit.Id))
                    {
                        AddValueToRange(xlsWorksheet, rng, "F" + rowCount, GetIntFromRow("PcIntvNumPsacTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "G" + rowCount, GetIntFromRow("PcIntvNumSacTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "H" + rowCount, GetIntFromRow("PcIntvNumAdultsTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "J" + rowCount, GetIntFromRow("PcIntvPsacTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "K" + rowCount, GetIntFromRow("PcIntvNumSacTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "L" + rowCount, GetIntFromRow("PcIntvNumAdultsTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));

                        DateTime? startMda = GetDateFromRow("PcIntvStartDateOfMda", ref typesToCalc, aggIntvs[unit.Id], false, i, i, null, typeNames);
                        if (startMda.HasValue)
                            AddValueToRange(xlsWorksheet, rng, "E" + rowCount, startMda.Value.ToString("MM/dd/yyyy"));
                    }

                    rowCount++;
                }
            }

        }

        private void AddMDA3(excel.Workbook xlsWorkbook, excel.Worksheet xlsWorksheet, excel.Worksheet xlsSummary, excel.Range rng, List<AdminLevel> reportingUnits, Dictionary<int, DataRow> aggIntvs)
        {
            List<string> typeNames = new List<string> { "IntvIvm", "IntvIvmPzq" };
            List<string> typesToCalc = new List<string>();

            for (int i = Util.MaxRounds; i >= 1; i--)
            {
                if (i == 2 || !HasRoundNumber(reportingUnits, aggIntvs, typeNames,  typesToCalc, i))
                    continue;

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["MDA3"];
                if (i > 2)
                {
                    xlsWorksheet.Copy(System.Reflection.Missing.Value, xlsSummary);
                    xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets[xlsSummary.Index + 1];
                    xlsWorksheet.Name = string.Format("MDA3_R{0}", i);
                }
                int rowCount = 9;
                foreach (var unit in reportingUnits)
                {
                    if (aggIntvs.ContainsKey(unit.Id))
                    {


                        AddValueToRange(xlsWorksheet, rng, "I" + rowCount, GetIntFromRow("PcIntvNumSacTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "J" + rowCount, GetIntFromRow("PcIntvNumAdultsTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));

                        string sacTarg = GetIntFromRow("PcIntvNumSacTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames);
                        string adultsTarg = GetIntFromRow("PcIntvNumAdultsTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames);
                        DateTime? startMda = GetDateFromRow("PcIntvStartDateOfMda", ref typesToCalc, aggIntvs[unit.Id], false, i, i, null, typeNames);
                        if (i == 1)
                        {
                            startMda = GetDateFromRow("PcIntvStartDateOfMda", ref typesToCalc, aggIntvs[unit.Id], false, 1, 2, null, typeNames);
                            AddValueToRange(xlsWorksheet, rng, "L" + rowCount, GetIntFromRow("PcIntvNumSacTreated", ref typesToCalc, aggIntvs[unit.Id], 2, 2, null, typeNames));
                            AddValueToRange(xlsWorksheet, rng, "M" + rowCount, GetIntFromRow("PcIntvNumAdultsTreated", ref typesToCalc, aggIntvs[unit.Id], 2, 2, null, typeNames));

                            string sacTarg2 = GetIntFromRow("PcIntvNumSacTargeted", ref typesToCalc, aggIntvs[unit.Id], 2, 2, null, typeNames);
                            string adultsTarg2 = GetIntFromRow("PcIntvNumAdultsTargeted", ref typesToCalc, aggIntvs[unit.Id], 2, 2, null, typeNames);
                            sacTarg = GetMaxIntFromStrings(sacTarg, sacTarg2);
                            adultsTarg = GetMaxIntFromStrings(adultsTarg, adultsTarg2);
                        }

                        if (startMda.HasValue)
                            AddValueToRange(xlsWorksheet, rng, "E" + rowCount, startMda.Value.ToString("MM/dd/yyyy"));

                        AddValueToRange(xlsWorksheet, rng, "F" + rowCount, sacTarg);
                        AddValueToRange(xlsWorksheet, rng, "G" + rowCount, adultsTarg);

                    }

                    rowCount++;
                }
            }
        }

        private void AddMDA2(excel.Workbook xlsWorkbook, excel.Worksheet xlsWorksheet, excel.Worksheet xlsSummary, excel.Range rng, List<AdminLevel> reportingUnits, Dictionary<int, DataRow> aggIntvs)
        {
            List<string> typeNames = new List<string> { "IntvDecAlb" };
            List<string> typesToCalc = new List<string>();

            for (int i = Util.MaxRounds; i >= 1; i--)
            {
                if (!HasRoundNumber(reportingUnits, aggIntvs, typeNames,  typesToCalc, i))
                    continue;

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["MDA2"];
                if (i > 1)
                {
                    xlsWorksheet.Copy(System.Reflection.Missing.Value, xlsSummary);
                    xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets[xlsSummary.Index + 1];
                    xlsWorksheet.Name = string.Format("MDA2_R{0}", i);
                }
                int rowCount = 9;
                foreach (var unit in reportingUnits)
                {
                    if (aggIntvs.ContainsKey(unit.Id))
                    {
                        AddValueToRange(xlsWorksheet, rng, "F" + rowCount, GetIntFromRow("PcIntvNumPsacTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "G" + rowCount, GetIntFromRow("PcIntvNumSacTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "H" + rowCount, GetIntFromRow("PcIntvNumAdultsTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "J" + rowCount, GetIntFromRow("PcIntvPsacTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "K" + rowCount, GetIntFromRow("PcIntvNumSacTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "L" + rowCount, GetIntFromRow("PcIntvNumAdultsTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));

                        DateTime? startMda = GetDateFromRow("PcIntvStartDateOfMda", ref typesToCalc, aggIntvs[unit.Id], false, i, i, null, typeNames);
                        if (startMda.HasValue)
                            AddValueToRange(xlsWorksheet, rng, "E" + rowCount, startMda.Value.ToString("MM/dd/yyyy"));

                    }
                    rowCount++;
                }
            }
        }

        private void AddMDA1(excel.Workbook xlsWorkbook, excel.Worksheet xlsWorksheet, excel.Worksheet xlsSummary, excel.Range rng, List<AdminLevel> reportingUnits, Dictionary<int, DataRow> aggIntvs)
        {
            List<string> typeNames = new List<string> { "IntvIvmPzqAlb", "IntvIvmAlb" };
            List<string> typesToCalc = new List<string>();

            for (int i = Util.MaxRounds; i >= 1; i--)
            {
                if (!HasRoundNumber(reportingUnits, aggIntvs, typeNames,  typesToCalc, i))
                    continue;

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["MDA1"];
                if (i > 1)
                {
                    xlsWorksheet.Copy(System.Reflection.Missing.Value, xlsSummary);
                    xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets[xlsSummary.Index + 1];
                    xlsWorksheet.Name = string.Format("MDA1_R{0}", i);
                }

                int rowCount = 9;
                foreach (var unit in reportingUnits)
                {
                    if (aggIntvs.ContainsKey(unit.Id))
                    {
                        AddValueToRange(xlsWorksheet, rng, "G" + rowCount, GetIntFromRow("PcIntvNumSacTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "H" + rowCount, GetIntFromRow("PcIntvNumAdultsTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "K" + rowCount, GetIntFromRow("PcIntvNumSacTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "L" + rowCount, GetIntFromRow("PcIntvNumAdultsTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));

                        DateTime? startMda = GetDateFromRow("PcIntvStartDateOfMda", ref typesToCalc, aggIntvs[unit.Id], false, i, i, null, typeNames);
                        if (startMda.HasValue)
                            AddValueToRange(xlsWorksheet, rng, "E" + rowCount, startMda.Value.ToString("MM/dd/yyyy"));
                    }
                    rowCount++;
                }
            }
        }

        private void AddMDA4(excel.Workbook xlsWorkbook, excel.Worksheet xlsWorksheet, excel.Worksheet xlsSummary, excel.Range rng, List<AdminLevel> reportingUnits, Dictionary<int, DataRow> aggIntvs)
        {
            List<string> typeNames = new List<string> { "IntvAlb2" };
            List<string> typesToCalc = new List<string>();

            for (int i = Util.MaxRounds; i >= 1; i--)
            {
                if (!HasRoundNumber(reportingUnits, aggIntvs, typeNames,  typesToCalc, i))
                    continue;

                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["MDA4"];
                if (i > 1)
                {
                    xlsWorksheet.Copy(System.Reflection.Missing.Value, xlsSummary);
                    xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets[xlsSummary.Index + 1];
                    xlsWorksheet.Name = string.Format("MDA4_R{0}", i);
                }
                int rowCount = 9;
                foreach (var unit in reportingUnits)
                {
                    if (aggIntvs.ContainsKey(unit.Id))
                    {
                        AddValueToRange(xlsWorksheet, rng, "F" + rowCount, GetIntFromRow("PcIntvNumPsacTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "G" + rowCount, GetIntFromRow("PcIntvNumSacTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "H" + rowCount, GetIntFromRow("PcIntvNumAdultsTargeted", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "J" + rowCount, GetIntFromRow("PcIntvPsacTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "K" + rowCount, GetIntFromRow("PcIntvNumSacTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));
                        AddValueToRange(xlsWorksheet, rng, "L" + rowCount, GetIntFromRow("PcIntvNumAdultsTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames));

                        DateTime? startMda = GetDateFromRow("PcIntvStartDateOfMda", ref typesToCalc, aggIntvs[unit.Id], false, i, i, null, typeNames);
                        if (startMda.HasValue)
                            AddValueToRange(xlsWorksheet, rng, "E" + rowCount, startMda.Value.ToString("MM/dd/yyyy"));

                    }
                    rowCount++;
                }
            }
        }

        private void AddDistricts(excel.Workbook xlsWorkbook, excel.Worksheet xlsWorksheet, excel.Worksheet xlsSummary, excel.Range rng, List<AdminLevel> reportingUnits, Dictionary<int, DataRow> aggIntvs)
        {
            List<string> typeNames = new List<string> { "IntvAlb2", "IntvIvmPzqAlb", "IntvIvmAlb", "IntvDecAlb", "IntvIvm", "IntvIvmPzq", "IntvPzq", "IntvPzqAlb", "IntvPzqMbd", "IntvAlb", "IntvMbd" };
            List<string> typesToCalc = new List<string>();
            xlsWorksheet = (excel.Worksheet)xlsWorkbook.Sheets["DISTRICT"];

            int rowCount = 9;
            foreach (var unit in reportingUnits)
            {
                if (aggIntvs.ContainsKey(unit.Id))
                {
                    // LF
                    AddValueToRange(xlsWorksheet, rng, "E" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "F" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "LF", typeNames));
                    // Oncho
                    AddValueToRange(xlsWorksheet, rng, "K" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Oncho", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "L" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Oncho", typeNames));
                    // STH
                    AddValueToRange(xlsWorksheet, rng, "Q" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "STH", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "R" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "STH", typeNames));
                    // Schisto
                    AddValueToRange(xlsWorksheet, rng, "W" + rowCount, GetIntFromRow("PcIntvNumMalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));
                    AddValueToRange(xlsWorksheet, rng, "X" + rowCount, GetIntFromRow("PcIntvNumFemalesTreated", ref typesToCalc, aggIntvs[unit.Id], 1, Util.MaxRounds, "Schisto", typeNames));
                
                }
                rowCount++;
            }
        }

        private bool HasRoundNumber(List<AdminLevel> reportingUnits, Dictionary<int, DataRow> aggIntvs, List<string> typeNames, List<string> typesToCalc, int i)
        {
            foreach (var unit in reportingUnits)
            {
                if (aggIntvs.ContainsKey(unit.Id))
                {
                    string treated = GetIntFromRow("PcIntvNumIndividualsTreated", ref typesToCalc, aggIntvs[unit.Id], i, i, null, typeNames);
                    if (!string.IsNullOrEmpty(treated))
                        return true;
                }
            }
            return false;
        }

        private Dictionary<int, DataRow> GetDd(DateTime start, DateTime end, List<AdminLevel> demography)
        {
            Dictionary<int, DataRow> dd = new Dictionary<int, DataRow>();
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
            var lf = diseaseRepo.Create(DiseaseType.Lf);
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(lf.Id, lf.Indicators["DDLFDiseaseDistributionPcInterventions"]));
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(lf.Id, lf.Indicators["DDLFNumPcRoundsYearRecommendedByWhoGuid"]));
            var oncho = diseaseRepo.Create(DiseaseType.Oncho);
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(oncho.Id, oncho.Indicators["DDOnchoDiseaseDistributionPcInterventio"]));
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(oncho.Id, oncho.Indicators["DDOnchoNumPcRoundsYearRecommendedByWhoG"]));
            var sth = diseaseRepo.Create(DiseaseType.STH);
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(sth.Id, sth.Indicators["DDSTHDiseaseDistributionPcInterventions"]));
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(sth.Id, sth.Indicators["DDSTHNumPcRoundsYearRecommendedByWhoGui"]));
            var schisto = diseaseRepo.Create(DiseaseType.Schisto);
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(schisto.Id, schisto.Indicators["DDSchistoDiseaseDistributionPcIntervent"]));
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(schisto.Id, schisto.Indicators["DDSchistoNumPcRoundsYearRecommendedByWh"]));

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
            return dd;
        }

        private string TranslateEndemicity(DiseaseType t, string end)
        {
            var ends = end.Split(' ');
            if (ends.Count() > 0)
                end = ends[0].Trim().ToUpper();
            if (t == DiseaseType.Lf || t == DiseaseType.Oncho)
            {
                switch (end)
                {
                    case "M":
                        return "4";
                    case "NS":
                        return "4";
                    case "0":
                        return "0";
                    case "1":
                        return "1";
                    case "100":
                        return "99";
                    case "PENDING":
                        return "99";
                }
            }
            if (t == DiseaseType.Schisto || t == DiseaseType.STH)
            {
                switch (end)
                {
                    case "M":
                        return "4";
                    case "NS":
                        return "4";
                    case "0":
                        return "0";
                    case "1":
                        return "1";
                    case "2":
                        return "2";
                    case "2A":
                        return "2";
                    case "2B":
                        return "2";
                    case "3":
                        return "3";
                    case "3A":
                        return "3";
                    case "3B":
                        return "3";
                    case "10":
                        return "1";
                    case "20":
                        return "1";
                    case "30":
                        return "2";
                    case "40":
                        return "3";
                    case "100":
                        return "0";
                    case "PENDING":
                        return "0";
                }
            }

            return end;
        }



    }
}
