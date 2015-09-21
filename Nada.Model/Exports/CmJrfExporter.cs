using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using excel = Microsoft.Office.Interop.Excel;

namespace Nada.Model.Exports
{
    public class CmJrfExporter : ExporterBase, IExporter
    {
        SettingsRepository settings = new SettingsRepository();
        DemoRepository demo = new DemoRepository();
        ExportRepository repo = new ExportRepository();
        TranslationLookupInstance exportLangLookup = null;

        public string ExportName
        {
            get { return Translations.JrfCmNtds; }
        }

        public ExportResult DoExport(string fileName, int userId, ExportType exportType)
        {
            throw new NotImplementedException();
        }

        public ExportResult ExportData(string filePath, int userId, ExportCmJrfQuestions questions)
        {
            try
            {
                int yearReporting = questions.YearReporting.Value; 
                exportLangLookup = new TranslationLookupInstance(questions.ExportCulture); 
                System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Microsoft.Office.Interop.Excel.Workbook xlsWorkbook;
                Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet;
                excel.Range rng = null;
                object missing = System.Reflection.Missing.Value;

                // Open workbook
                xlsWorkbook = xlsApp.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, exportLangLookup.GetValue("CmJrfExportFileName")),
                    missing, missing, missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing, missing, missing);

                var districtLevel = questions.AdminLevelType;
                CountryDemography countryDemo = demo.GetCountryDemoByYear(yearReporting);
                Country country = demo.GetCountry();
                List<AdminLevel> demography = new List<AdminLevel>();
                DateTime startDate = new DateTime(yearReporting, 1, 1);
                DateTime endDate = startDate.AddYears(1).AddDays(-1);
                List<AdminLevel> tree = demo.GetAdminLevelTreeForDemography(districtLevel.LevelNumber, startDate, endDate, ref demography);
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[2];
                AddContactsPage(xlsWorksheet, rng, questions, country);
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[3];
                AddCountryPage(xlsWorksheet, rng, demography, districtLevel.LevelNumber, questions, country, countryDemo, startDate, endDate);
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[11];
                AddPlanningPage(xlsWorksheet, rng, questions);


                // sheet 4 GW (start row 9)
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[5];
                AddIndicators(DiseaseType.GuineaWorm, StaticIntvType.GuineaWormIntervention, yearReporting, xlsWorksheet, AddGwInds, AggGwInd, districtLevel.LevelNumber);
                //// leprosy
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[6];
                AddIndicators(DiseaseType.Leprosy, StaticIntvType.LeprosyIntervention, yearReporting, xlsWorksheet, AddLeprosyInds, AggLeprosyInd, districtLevel.LevelNumber);
                //// hat
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[7];
                AddIndicators(DiseaseType.Hat, StaticIntvType.HatIntervention, yearReporting, xlsWorksheet, AddHatInds, AggHatInd, districtLevel.LevelNumber);
                //// leish monthly
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[8];
                AddIndicators(DiseaseType.Leish, StaticIntvType.LeishMonthly, yearReporting, xlsWorksheet, AddLeishMonthlyInds, AggLeishInd, districtLevel.LevelNumber);
                //// leish annual
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[8];
                AddIndicators(DiseaseType.Leish, StaticIntvType.LeishAnnual, yearReporting, xlsWorksheet, AddLeishAnnualInds, AggLeishInd, districtLevel.LevelNumber);
                //// buruli
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[9];
                AddIndicators(DiseaseType.Buruli, StaticIntvType.BuruliUlcerIntv, yearReporting, xlsWorksheet, AddBuInds, AggBuInd, districtLevel.LevelNumber);
                //// yaws
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[10];
                AddIndicators(DiseaseType.Yaws, StaticIntvType.YawsIntervention, yearReporting, xlsWorksheet, AddYawsInds, AggYawsInd, districtLevel.LevelNumber);

                xlsApp.DisplayAlerts = false;
                xlsWorkbook.SaveAs(filePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, missing,
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

        private void AddIndicators(DiseaseType diseaseType, StaticIntvType intvType, int year, Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet,
            Action<Microsoft.Office.Interop.Excel.Worksheet, List<AdminLevelIndicators>> AddToWorksheet,
            Func<AggregateIndicator, object, object> customAggRule, int reportingLevelNumber)
        {
            ExportRepository repo = new ExportRepository();
            var indicators = repo.GetDistrictIndicatorTrees((int)intvType, year, (int)diseaseType, customAggRule, reportingLevelNumber);
            AddToWorksheet(xlsWorksheet, indicators);
        }

        protected void AddIndValue(AdminLevelIndicators district, string location, string key, Microsoft.Office.Interop.Excel.Range rng,
            Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, object missing, Func<AggregateIndicator, object, object> customAggRule,
            bool shouldTranslate)
        {
            // If district doesn't contain key
            string value = null;
            if(district.Indicators.ContainsKey(key))
                value = district.Indicators[key].Value;
            else
                value = IndicatorAggregator.AggregateChildren(district.Children, key, null, new List<IndicatorDropdownValue>()).Value;

            if (value != null)
            {
                rng = xlsWorksheet.get_Range(location, missing);
                if (shouldTranslate)
                    rng.Value = TranslationLookup.GetValue(value.ToString(), value.ToString());
                else
                    rng.Value = value;
            }
        }

        #region SHEET specific
        private void AddContactsPage(excel.Worksheet xlsWorksheet, excel.Range rng, ExportCmJrfQuestions questions, Country country)
        {
            AddValueToRange(xlsWorksheet, rng, "C2", country.Name);
            AddValueToRange(xlsWorksheet, rng, "E2", questions.YearReporting.Value);

            int rowId = 5;
            foreach (var contact in questions.Contacts.Take(16))
            {
                AddValueToRange(xlsWorksheet, rng, "B" + rowId, contact.CmContactName);
                AddValueToRange(xlsWorksheet, rng, "C" + rowId, contact.CmContactPost);
                AddValueToRange(xlsWorksheet, rng, "D" + rowId, contact.CmContactTele);
                AddValueToRange(xlsWorksheet, rng, "E" + rowId, contact.CmContactEmail);

                rowId++;
            }
        }

        private void AddCountryPage(excel.Worksheet xlsWorksheet, excel.Range rng, List<AdminLevel> demography, int districtLevel, 
            ExportCmJrfQuestions questions, Country country, CountryDemography cDemo, DateTime start, DateTime end)
        {
            var districts = demography.Where(d => d.LevelNumber == districtLevel).ToList();
            AddValueToRange(xlsWorksheet, rng, "B2", country.Name);
            AddValueToRange(xlsWorksheet, rng, "B3", questions.YearReporting.Value);
            if(cDemo.TotalPopulation.HasValue)
                AddValueToRange(xlsWorksheet, rng, "F2", cDemo.TotalPopulation.Value);
            AddValueToRange(xlsWorksheet, rng, "G3", districts.Count);
            if (questions.CmBudgetProportion.HasValue)
                AddValueToRange(xlsWorksheet, rng, "F4", questions.CmBudgetProportion.Value);

            // DISEASE DISTRO STATS
            var ddDict = GetDd(start, end, districts);
            double gEnd = 0, gNoEnd = 0, gEndPop = 0, lpEnd = 0, lpNoEnd = 0, lpEndPop = 0, lhEnd = 0, lhNoEnd = 0, lhEndPop = 0, bEnd = 0, bNoEnd = 0, bEndPop = 0, yEnd = 0, yNoEnd = 0, yEndPop = 0,
                hEnd = 0, hNoEnd = 0, hEndPop = 0;
            int rowId = 9;
            foreach (var district in districts)
            {
                if (ddDict.ContainsKey(district.Id))
                {
                    ParseEndemicity(ddDict, ref gEnd, ref gNoEnd, ref gEndPop, district, "Dracun", "Endemic", "NotEndemic", null);
                    ParseEndemicity(ddDict, ref lpEnd, ref lpNoEnd, ref lpEndPop, district, "Leprosy", "High", "Low", null);
                    ParseEndemicity(ddDict, ref hEnd, ref hNoEnd, ref hEndPop, district, "HAT", "Endemic", "NotEndemic", "FormerlyEndemic");
                    ParseEndemicity(ddDict, ref lhEnd, ref lhNoEnd, ref lhEndPop, district, "Leishmaniasis", "Endemic", "NotEndemic", null);
                    ParseEndemicity(ddDict, ref bEnd, ref bNoEnd, ref bEndPop, district, "BuruliUlcer", "Endemic", "NotEndemic", null);
                    ParseEndemicity(ddDict, ref yEnd, ref yNoEnd, ref yEndPop, district, "YAWS", "Endemic", "NotEndemic", null);
                }
                rowId++;
            }

            AddValueToRange(xlsWorksheet, rng, "B8", gEnd);
            AddValueToRange(xlsWorksheet, rng, "C8", gNoEnd);
            AddValueToRange(xlsWorksheet, rng, "D8", gEndPop);
            AddValueToRange(xlsWorksheet, rng, "E8", gEnd);
            AddValueToRange(xlsWorksheet, rng, "B9", lpEnd);
            AddValueToRange(xlsWorksheet, rng, "C9", lpNoEnd);
            AddValueToRange(xlsWorksheet, rng, "D9", lpEndPop);
            AddValueToRange(xlsWorksheet, rng, "E9", lpEnd);
            AddValueToRange(xlsWorksheet, rng, "B10", hEnd);
            AddValueToRange(xlsWorksheet, rng, "C10", hNoEnd);
            AddValueToRange(xlsWorksheet, rng, "D10", hEndPop);
            AddValueToRange(xlsWorksheet, rng, "E10", hEnd);
            AddValueToRange(xlsWorksheet, rng, "B11", lhEnd);
            AddValueToRange(xlsWorksheet, rng, "C11", lhNoEnd);
            AddValueToRange(xlsWorksheet, rng, "D11", lhEndPop);
            AddValueToRange(xlsWorksheet, rng, "E11", lhEnd);
            AddValueToRange(xlsWorksheet, rng, "B12", bEnd);
            AddValueToRange(xlsWorksheet, rng, "C12", bNoEnd);
            AddValueToRange(xlsWorksheet, rng, "D12", bEndPop);
            AddValueToRange(xlsWorksheet, rng, "E12", bEnd);
            AddValueToRange(xlsWorksheet, rng, "B13", yEnd);
            AddValueToRange(xlsWorksheet, rng, "C13", yNoEnd);
            AddValueToRange(xlsWorksheet, rng, "D13", yEndPop);
            AddValueToRange(xlsWorksheet, rng, "E13", yEnd);
        }

        private static void ParseEndemicity(Dictionary<int, DataRow> ddDict, ref double end, ref double noEnd, ref double endPop, AdminLevel district, string diseaseKey, 
            string keyEndemic, string keyNotEndemic, string keyNotEndemic2)
        {
            if (ddDict[district.Id].Table.Columns.Contains(TranslationLookup.GetValue("EndemicityStatus") + " - " + TranslationLookup.GetValue(diseaseKey)))
            {
                string endemicity = ddDict[district.Id][TranslationLookup.GetValue("EndemicityStatus") + " - " + TranslationLookup.GetValue(diseaseKey)].ToString();
                if (endemicity == TranslationLookup.GetValue(keyEndemic))
                {
                    end++;
                    endPop += district.CurrentDemography.TotalPopulation.Value;
                }
                else if (endemicity == TranslationLookup.GetValue(keyNotEndemic) || 
                    (!string.IsNullOrEmpty(keyNotEndemic2) && endemicity == TranslationLookup.GetValue(keyNotEndemic2)))
                    noEnd++;
            }
        }

        private Dictionary<int, DataRow> GetDd(DateTime start, DateTime end, List<AdminLevel> demography)
        {
            DiseaseRepository diseaseRepo = new DiseaseRepository();
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
            var gw = diseaseRepo.CreateCm(DiseaseType.GuineaWorm);
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(gw.Id, gw.Indicators["EndemicityStatus"]));
            var lep = diseaseRepo.CreateCm(DiseaseType.Leprosy);
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(lep.Id, lep.Indicators["EndemicityStatus"]));
            var lei = diseaseRepo.CreateCm(DiseaseType.Leish);
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(lei.Id, lei.Indicators["LeishDiseaseDistEndemicityStatusVL"]));
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(lei.Id, lei.Indicators["LeishDiseaseDistEndemicityStatusCL"]));
            var hat = diseaseRepo.CreateCm(DiseaseType.Hat);
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(hat.Id, hat.Indicators["EndemicityStatus"]));
            var b = diseaseRepo.CreateCm(DiseaseType.Buruli);
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(b.Id, b.Indicators["EndemicityStatus"]));
            var y = diseaseRepo.CreateCm(DiseaseType.Yaws);
            options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(y.Id, y.Indicators["EndemicityStatus"]));

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

        private void AddPlanningPage(excel.Worksheet xlsWorksheet, excel.Range rng, ExportCmJrfQuestions questions)
        {
            AddValueToRange(xlsWorksheet, rng, "C5", questions.CmHaveMasterPlan ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C6", questions.CmYearsMasterPlan);
            AddValueToRange(xlsWorksheet, rng, "C7", questions.CmBuget);
            AddValueToRange(xlsWorksheet, rng, "C8", questions.CmPercentFunded);

            AddValueToRange(xlsWorksheet, rng, "C9", questions.CmHaveAnnualOpPlan ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C10", questions.CmDiseaseSpecOrNtdIntegrated);
            AddValueToRange(xlsWorksheet, rng, "C12", questions.CmBuHasPlan ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C13", questions.CmGwHasPlan ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C14", questions.CmHatHasPlan ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C15", questions.CmLeishHasPlan ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C16", questions.CmLeprosyHasPlan ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C17", questions.CmYawsHasPlan ? Translations.Yes : Translations.No);

            AddValueToRange(xlsWorksheet, rng, "C20", questions.CmAnySupplyFunds ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C21", questions.CmHasStorage ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C22", questions.CmStorageNtdOrCombined);
            AddValueToRange(xlsWorksheet, rng, "C24", questions.CmStorageSponsor1);
            AddValueToRange(xlsWorksheet, rng, "C25", questions.CmStorageSponsor2);
            AddValueToRange(xlsWorksheet, rng, "C26", questions.CmStorageSponsor3);
            AddValueToRange(xlsWorksheet, rng, "C27", questions.CmStorageSponsor4);

            AddValueToRange(xlsWorksheet, rng, "C30", questions.CmHasTaskForce ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C32", questions.CmHasMoh ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C33", questions.CmHasMosw ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C34", questions.CmHasMot ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C35", questions.CmHasMoe ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C36", questions.CmHasMoc ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C37", questions.CmHasUni ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C38", questions.CmHasNgo ? Translations.Yes : Translations.No);

            AddValueToRange(xlsWorksheet, rng, "C40", questions.CmHasAnnualForum ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C41", questions.CmForumHasRegions ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C42", questions.CmForumHasTaskForce ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C44", questions.CmHasNtdReviewMeetings ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C45", questions.CmHasDiseaseSpecMeetings ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C47", questions.CmHasGwMeeting ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C48", questions.CmHasLeprosyMeeting ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C49", questions.CmHasHatMeeting ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C50", questions.CmHasLeishMeeting ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C51", questions.CmHasBuMeeting ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C52", questions.CmHasYawsMeeting ? Translations.Yes : Translations.No);

            AddValueToRange(xlsWorksheet, rng, "C55", questions.CmHasWeeklyMech ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C56", questions.CmHasMonthlyMech ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C57", questions.CmHasQuarterlyMech ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C58", questions.CmHasSemesterMech ? Translations.Yes : Translations.No);
            AddValueToRange(xlsWorksheet, rng, "C59", questions.CmOtherMechs);
        }

        private void AddGwInds(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, List<AdminLevelIndicators> districtIndicators)
        {
            Microsoft.Office.Interop.Excel.Range rng;
            object missing = System.Reflection.Missing.Value;
            int rowId = 9;
            foreach (var district in districtIndicators)
            {
                rng = xlsWorksheet.get_Range("A" + rowId, missing);
                rng.Value = district.Parent.Name;
                rng = xlsWorksheet.get_Range("B" + rowId, missing);
                rng.Value = district.Name;
                AddIndValue(district, "C" + rowId, "EndemicityStatus28", rng, xlsWorksheet, missing, AggGwInd, true);
                AddIndValue(district, "D" + rowId, "NumVas67", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "E" + rowId, "VasReporting68", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "F" + rowId, "NumIdsr69", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "G" + rowId, "NumIdsrReporting70", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "H" + rowId, "NumRumors71", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "I" + rowId, "NumRumorsInvestigated72", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "J" + rowId, "NumClinical73", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "K" + rowId, "NumLab74", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "L" + rowId, "NumIndigenous75", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "N" + rowId, "NumVillageWithImported77", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "O" + rowId, "NumCasesContained78", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "P" + rowId, "NumCasesLost79", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "Q" + rowId, "NumEndemicVillages80", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "R" + rowId, "NumEndemicVillagesReporting81", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "S" + rowId, "NumEndemicVillageWater82", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "T" + rowId, "NumEndemicAbate83", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "U" + rowId, "NumVillagesSafeWater84", rng, xlsWorksheet, missing, AggGwInd, false);

                rowId++;
            }

            rng = null;
        }

        private object AggGwInd(AggregateIndicator ind1, object existingValue)
        {
            if (ind1.IndicatorId == 28)
            {
                if (ind1.Value == "Endemic" || existingValue.ToString() == "Endemic")
                    return "Endemic";
                if (ind1.Value == "EndemicityTbv" || existingValue.ToString() == "EndemicityTbv")
                    return "EndemicityTbv";
                return "NotEndemic";
            }

            return ind1.Value;
        }

        private void AddLeprosyInds(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, List<AdminLevelIndicators> districtIndicators)
        {
            Microsoft.Office.Interop.Excel.Range rng;
            object missing = System.Reflection.Missing.Value;
            int rowId = 9;
            foreach (var district in districtIndicators)
            {
                rng = xlsWorksheet.get_Range("A" + rowId, missing);
                rng.Value = district.Parent.Name;
                rng = xlsWorksheet.get_Range("B" + rowId, missing);
                rng.Value = district.Name;
                AddIndValue(district, "C" + rowId, "EndemicityStatus32", rng, xlsWorksheet, missing, AggLeprosyInd, true);
                AddIndValue(district, "D" + rowId, "CaseFindingStrategy33", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "E" + rowId, "TotalNumNewCases34", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "F" + rowId, "TotalNumMbCases35", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "H" + rowId, "TotalNumChildNewCases36", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "I" + rowId, "TotalNumFemaleNewCases37", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "L" + rowId, "PrevalenceBeginningYear38", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "M" + rowId, "MbCasesRegisteredMdtBeginning39", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "P" + rowId, "MbCasesRegisteredMdtEnd40", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "G" + rowId, "NumGrade285", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "J" + rowId, "NumRelapses86", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "K" + rowId, "CasesReadmitted87", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "O" + rowId, "TotalWithdrawal88", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "Q" + rowId, "NumFirstLine89", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "R" + rowId, "NumDistributionPoints90", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "S" + rowId, "NumMbAdult91", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "T" + rowId, "NumMbChild92", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "U" + rowId, "NumPbAdult93", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "V" + rowId, "NumPbChild94", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "W" + rowId, "ClofazAvail95", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "X" + rowId, "OtherMedsLeprosy96", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "Y" + rowId, "MbAdultsEnd97", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "Z" + rowId, "MbChildEnd98", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "AA" + rowId, "PbAdultEnd99", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "AB" + rowId, "PbChildEnd100", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "AC" + rowId, "PatientsCuredMb101", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "AD" + rowId, "PatientsCuredPb102", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "AE" + rowId, "NumPatientsType1103", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "AF" + rowId, "NumPatientsType2104", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "AG" + rowId, "NumPatientsFootwear105", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "AH" + rowId, "NumLeprosySelfCare106", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "AI" + rowId, "NumLeprosySurgery107", rng, xlsWorksheet, missing, AggLeprosyInd, false);
                AddIndValue(district, "AJ" + rowId, "NumLeprosyRehab108", rng, xlsWorksheet, missing, AggLeprosyInd, false);

                rowId++;
            }

            rng = null;
        }

        private object AggLeprosyInd(AggregateIndicator ind1, object existingValue)
        {
            if (ind1.IndicatorId == 32)
            {
                if (ind1.Value == "High" || existingValue.ToString() == "High")
                    return "High";
                return "Low";
            }

            return ind1.Value;
        }

        private void AddHatInds(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, List<AdminLevelIndicators> districtIndicators)
        {
            Microsoft.Office.Interop.Excel.Range rng;
            object missing = System.Reflection.Missing.Value;
            int rowId = 9;
            foreach (var district in districtIndicators)
            {
                rng = xlsWorksheet.get_Range("A" + rowId, missing);
                rng.Value = district.Parent.Name;
                rng = xlsWorksheet.get_Range("B" + rowId, missing);
                rng.Value = district.Name;
                AddIndValue(district, "C" + rowId, "EndemicityStatus42", rng, xlsWorksheet, missing, AggHatInd, true);
                AddIndValue(district, "D" + rowId, "NumClinicalCasesHat109", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "E" + rowId, "NumLabCases110", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "F" + rowId, "NumTGamb111", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "G" + rowId, "NumTRhod112", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "H" + rowId, "NumTGambTRhod113", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "I" + rowId, "NumProspection114", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "J" + rowId, "NumCasesTreated115", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "K" + rowId, "NumTreatedRef116", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "L" + rowId, "NumTreatedNect117", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "M" + rowId, "NumCasesCured118", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "N" + rowId, "NumTreatmentFailures119", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "O" + rowId, "NumCasesSaes120", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "P" + rowId, "NumDeaths121", rng, xlsWorksheet, missing, AggHatInd, false);

                rowId++;
            }

            rng = null;
        }

        private object AggHatInd(AggregateIndicator ind1, object existingValue)
        {
            if (ind1.IndicatorId == 42)
            {
                if (ind1.Value == "Endemic" || existingValue.ToString() == "Endemic")
                    return "Endemic";
                if (ind1.Value == "FormerlyEndemic" || existingValue.ToString() == "FormerlyEndemic")
                    return "FormerlyEndemic";
                return "NotEndemic";
            }

            return ind1.Value;
        }

        private void AddLeishMonthlyInds(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, List<AdminLevelIndicators> districtIndicators)
        {
            Microsoft.Office.Interop.Excel.Range rng;
            object missing = System.Reflection.Missing.Value;
            int rowId = 9;
            foreach (var district in districtIndicators)
            {
                rng = xlsWorksheet.get_Range("A" + rowId, missing);
                rng.Value = district.Parent.Name;
                rng = xlsWorksheet.get_Range("B" + rowId, missing);
                rng.Value = district.Name;
                AddIndValue(district, "C" + rowId, "LeishDiseaseDistEndemicityStatusVL234", rng, xlsWorksheet, missing, AggLeishInd, true);
                AddIndValue(district, "D" + rowId, "LeishDiseaseDistEndemicityStatusCL235", rng, xlsWorksheet, missing, AggLeishInd, true);

                AddIndValue(district, "E" + rowId, "LeishMontIntvNumberOfClinicalCutaneousLeishmaniasisCLCases437", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "F" + rowId, "LeishMontIntvNumberOfLabConfirmedCLCases439", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "G" + rowId, "LeishMontIntvNumberOfLabConfirmedVisceralLeishmaniasisVLCases438", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "H" + rowId, "LeishMontIntvNumberOfVisceralLeishmaniasisHIVCoInfection450", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "I" + rowId, "LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical455", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "J" + rowId, "LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical442", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "K" + rowId, "LeishMontIntvNumberOfCasesFoundActively435", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "L" + rowId, "LeishMontIntvNumberOfNewVLCasesTreated481", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "M" + rowId, "LeishMontIntvNumberOfNewCLCasesTreated486", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "R" + rowId, "LeishMontIntvNumberOfFailureCasesVL483", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "S" + rowId, "LeishMontIntvNumberOfFailureCasesCL488", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "T" + rowId, "LeishMontIntvNumberOfCLCasesWithSeriousAdverseEvents490", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "U" + rowId, "LeishMontIntvNumberOfVLCasesWithSeriousAdverseEvents485", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "V" + rowId, "LeishMontIntvNumberOfDeathsForNewVLCases484", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "W" + rowId, "LeishMontIntvNumberOfDeathsForNewCLCases489", rng, xlsWorksheet, missing, AggLeishInd, false);

                rowId++;
            }

            rng = null;
        }

        private void AddLeishAnnualInds(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, List<AdminLevelIndicators> districtIndicators)
        {
            Microsoft.Office.Interop.Excel.Range rng;
            object missing = System.Reflection.Missing.Value;
            int rowId = 9;
            foreach (var district in districtIndicators)
            {
                rng = xlsWorksheet.get_Range("A" + rowId, missing);
                rng.Value = district.Parent.Name;
                rng = xlsWorksheet.get_Range("B" + rowId, missing);
                rng.Value = district.Name;

                AddIndValue(district, "N" + rowId, "LeishAnnIntvNumberOfNewVLCasesCuredAfterFollowUpOfAtLeast6Months423", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "O" + rowId, "LeishAnnIntvNumberOfNewCLCasesCuredAfterFollowUpOfAtLeast6Months425", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "P" + rowId, "LeishAnnIntvNumberOfVLRelapseCases416", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "Q" + rowId, "LeishAnnIntvNumberOfCLRelapseCases418", rng, xlsWorksheet, missing, AggLeishInd, false);

                rowId++;
            }

            rng = null;
        }

        private object AggLeishInd(AggregateIndicator ind1, object existingValue)
        {
            if (ind1.IndicatorId == 234 || ind1.IndicatorId == 235)
            {
                if (ind1.Value == "Endemic" || existingValue.ToString() == "Endemic")
                    return "Endemic";
                if (ind1.Value == "UnknownEndemicity" || existingValue.ToString() == "UnknownEndemicity")
                    return "UnknownEndemicity";
                return "NotEndemic";
            }

            return ind1.Value;
        }

        private void AddBuInds(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, List<AdminLevelIndicators> districtIndicators)
        {
            Microsoft.Office.Interop.Excel.Range rng;
            object missing = System.Reflection.Missing.Value;
            int rowId = 9;
            foreach (var district in districtIndicators)
            {
                rng = xlsWorksheet.get_Range("A" + rowId, missing);
                rng.Value = district.Parent.Name;
                rng = xlsWorksheet.get_Range("B" + rowId, missing);
                rng.Value = district.Name;
                AddIndValue(district, "C" + rowId, "EndemicityStatus59", rng, xlsWorksheet, missing, AggBuInd, true);

                AddIndValue(district, "D" + rowId, "CaseFindingStrategy61", rng, xlsWorksheet, missing, AggBuInd, false);
                AddIndValue(district, "E" + rowId, "TotalNumNewCases63", rng, xlsWorksheet, missing, AggBuInd, false);
                AddIndValue(district, "I" + rowId, "TotalNumChildNewCases66", rng, xlsWorksheet, missing, AggBuInd, false);
                AddIndValue(district, "J" + rowId, "TotalNumFemaleNewCases67", rng, xlsWorksheet, missing, AggBuInd, false);
                AddIndValue(district, "G" + rowId, "TotalCat3Cases70", rng, xlsWorksheet, missing, AggBuInd, false);
                AddIndValue(district, "F" + rowId, "TotalCasesConfirmedPcr71", rng, xlsWorksheet, missing, AggBuInd, false);

                AddIndValue(district, "M" + rowId, "PrevBeginYear140", rng, xlsWorksheet, missing, AggBuInd, false);
                AddIndValue(district, "N" + rowId, "CasesRegisteredYear141", rng, xlsWorksheet, missing, AggBuInd, false);
                AddIndValue(district, "O" + rowId, "PatientsCompletedTreatment143", rng, xlsWorksheet, missing, AggBuInd, false);
                AddIndValue(district, "P" + rowId, "PatientsFullyScared144", rng, xlsWorksheet, missing, AggBuInd, false);
                AddIndValue(district, "Q" + rowId, "PatientsCuredWoDisability145", rng, xlsWorksheet, missing, AggBuInd, false);
                AddIndValue(district, "R" + rowId, "OtherWithdrawl146", rng, xlsWorksheet, missing, AggBuInd, false);
                AddIndValue(district, "S" + rowId, "AdultsAtEnd147", rng, xlsWorksheet, missing, AggBuInd, false);
                AddIndValue(district, "T" + rowId, "ChildrenAtEnd148", rng, xlsWorksheet, missing, AggBuInd, false);

                AddIndValue(district, "K" + rowId, "NumRelapsesCases152", rng, xlsWorksheet, missing, AggBuInd, false);
                AddIndValue(district, "L" + rowId, "NumCasesReadmitted153", rng, xlsWorksheet, missing, AggBuInd, false);

                rowId++;
            }

            rng = null;
        }

        private object AggBuInd(AggregateIndicator ind1, object existingValue)
        {
            if (ind1.IndicatorId == 59)
            {
                if (ind1.Value == "Endemic" || existingValue.ToString() == "Endemic")
                    return "Endemic";
                if (ind1.Value == "UnknownEndemicity" || existingValue.ToString() == "UnknownEndemicity")
                    return "UnknownEndemicity";
                return "NotEndemic";
            }

            return ind1.Value;
        }

        private void AddYawsInds(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, List<AdminLevelIndicators> districtIndicators)
        {
            Microsoft.Office.Interop.Excel.Range rng;
            object missing = System.Reflection.Missing.Value;
            int rowId = 9;
            foreach (var district in districtIndicators)
            {
                rng = xlsWorksheet.get_Range("A" + rowId, missing);
                rng.Value = district.Parent.Name;
                rng = xlsWorksheet.get_Range("B" + rowId, missing);
                rng.Value = district.Name;

                AddIndValue(district, "C" + rowId, "EndemicityStatus60", rng, xlsWorksheet, missing, AggYawsInd, true);

                AddIndValue(district, "D" + rowId, "CaseFindingStrategy62", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "E" + rowId, "TotalNumNewCases64", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "F" + rowId, "TotalCasesConfirmedLab72", rng, xlsWorksheet, missing, AggYawsInd, false);
                
                AddIndValue(district, "G" + rowId, "PreSchoolCases674", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "H" + rowId, "SchoolCases1475", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "I" + rowId, "TotalNumFemaleNewCases76", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "J" + rowId, "CasesFromRural77", rng, xlsWorksheet, missing, AggYawsInd, false);

                AddIndValue(district, "K" + rowId, "NumVillagesScreenedCm90", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "L" + rowId, "NumSchoolsScreenedCm91", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "M" + rowId, "TotalNumPeopleScreenedCm92", rng, xlsWorksheet, missing, AggYawsInd, false);
           
                AddIndValue(district, "N" + rowId, "NumCasesTreatedYaws167", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "O" + rowId, "NumContactsTreatedYw168", rng, xlsWorksheet, missing, AggYawsInd, false);

                AddIndValue(district, "P" + rowId, "NumCasesCuredYaws175", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "Q" + rowId, "NumFullyHealed176", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "R" + rowId, "NumPersonsBenza177", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "S" + rowId, "NumSaesYw178", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "T" + rowId, "NumTreatedAzithro179", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "U" + rowId, "NumFacilitiesProvidingAntibiotics180", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "V" + rowId, "NumVillagesWithVolunteers181", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "W" + rowId, "BenzaVials182", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "X" + rowId, "Benza6Vials183", rng, xlsWorksheet, missing, AggYawsInd, false);
                AddIndValue(district, "Y" + rowId, "OtherAntibiotics184", rng, xlsWorksheet, missing, AggYawsInd, false);

                rowId++;
            }

            rng = null;
        }

        private object AggYawsInd(AggregateIndicator ind1, object existingValue)
        {
            if (ind1.IndicatorId == 60)
            {
                if (ind1.Value == "Endemic" || existingValue.ToString() == "Endemic")
                    return "Endemic";
                if (ind1.Value == "UnknownEndemicity" || existingValue.ToString() == "UnknownEndemicity")
                    return "UnknownEndemicity";
                return "NotEndemic";
            }

            return ind1.Value;
        }

        #endregion
    }
}
