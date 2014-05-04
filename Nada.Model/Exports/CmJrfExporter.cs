using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Repositories;
using excel = Microsoft.Office.Interop.Excel;

namespace Nada.Model.Exports
{
    public class CmJrfExporter : ExporterBase, IExporter
    {
        SettingsRepository settings = new SettingsRepository();
        DemoRepository demo = new DemoRepository();
        ExportRepository repo = new ExportRepository();

        public string ExportName
        {
            get { return Translations.JrfCmNtds; }
        }

        public ExportResult ExportData(string filePath, int userId, ExportCmJrfQuestions questions)
        {
            try
            {
                int yearReporting = questions.YearReporting.Value;
                System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Microsoft.Office.Interop.Excel.Workbook xlsWorkbook;
                Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet;
                excel.Range rng = null;
                object missing = System.Reflection.Missing.Value;

                // Open workbook
                xlsWorkbook = xlsApp.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, @"Exports\AFRO_CM_JRF.xls"),
                    missing, missing, missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing, missing, missing);

                var districtLevel = settings.GetAllAdminLevels().First(a => a.IsDistrict);
                CountryDemography countryDemo = demo.GetCountryDemoByYear(yearReporting);
                Country country = demo.GetCountry();
                DateTime reportingYearStart = new DateTime(yearReporting, country.ReportingYearStartDate.Month, country.ReportingYearStartDate.Day);
                List<AdminLevel> demography = new List<AdminLevel>();
                List<AdminLevel> tree = demo.GetAdminLevelTreeForDemography(districtLevel.LevelNumber, reportingYearStart, ref demography);
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[2];
                AddContactsPage(xlsWorksheet, rng, questions, country);
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[3];
                AddCountryPage(xlsWorksheet, rng, demography, districtLevel.LevelNumber, questions, country, countryDemo);
                xlsWorksheet = (excel.Worksheet)xlsWorkbook.Worksheets[11];
                AddPlanningPage(xlsWorksheet, rng, questions);

                
                // sheet 4 GW (start row 9)
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[5];
                AddIndicators(DiseaseType.GuineaWorm, StaticIntvType.GuineaWormIntervention, yearReporting, xlsWorksheet, AddGwInds, AggGwInd);
                //// leprosy
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[6];
                AddIndicators(DiseaseType.Leprosy, StaticIntvType.LeprosyIntervention, yearReporting, xlsWorksheet, AddLeprosyInds, AggLeprosyInd);
                //// hat
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[7];
                AddIndicators(DiseaseType.Hat, StaticIntvType.HatIntervention, yearReporting, xlsWorksheet, AddHatInds, AggHatInd);
                //// leish
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[8];
                AddIndicators(DiseaseType.Leish, StaticIntvType.LeishIntervention, yearReporting, xlsWorksheet, AddLeishInds, AggLeishInd);
                //// buruli
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[9];
                AddIndicators(DiseaseType.Buruli, StaticIntvType.BuruliUlcerIntv, yearReporting, xlsWorksheet, AddBuInds, AggBuInd);
                //// yaws
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[10];
                AddIndicators(DiseaseType.Yaws, StaticIntvType.YawsIntervention, yearReporting, xlsWorksheet, AddYawsInds, AggYawsInd);

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
                return new ExportResult();

            }
            catch (Exception ex)
            {
                return new ExportResult(ex.Message);
            }
        }

        private void AddIndicators(DiseaseType diseaseType, StaticIntvType intvType, int year, Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet,
            Action<Microsoft.Office.Interop.Excel.Worksheet, List<AdminLevelIndicators>> AddToWorksheet,
            Func<AggregateIndicator, object, object> customAggRule)
        {
            ExportRepository repo = new ExportRepository();
            var indicators = repo.GetDistrictIndicatorTrees((int)intvType, year, (int)diseaseType, customAggRule);
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
                value = IndicatorAggregator.AggregateChildren(district.Children, key, null).Value;

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
            ExportCmJrfQuestions questions, Country country, CountryDemography cDemo)
        {
            AddValueToRange(xlsWorksheet, rng, "B2", country.Name);
            AddValueToRange(xlsWorksheet, rng, "B3", questions.YearReporting.Value);
            if(cDemo.TotalPopulation.HasValue)
                AddValueToRange(xlsWorksheet, rng, "F2", cDemo.TotalPopulation.Value);
            AddValueToRange(xlsWorksheet, rng, "G3", demography.Where(d => d.LevelNumber == districtLevel).Count());
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

        private void AddLeishInds(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, List<AdminLevelIndicators> districtIndicators)
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
                AddIndValue(district, "C" + rowId, "EndemicityStatus50", rng, xlsWorksheet, missing, AggLeishInd, true);

                AddIndValue(district, "D" + rowId, "NumClCases126", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "E" + rowId, "NumLabClCases127", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "F" + rowId, "NumLabVlCases128", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "G" + rowId, "NumClVlCases129", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "H" + rowId, "NumCasesFoundActively130", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "I" + rowId, "NumCasesTreatedLeish131", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "J" + rowId, "NumCasesTreatedRef132", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "K" + rowId, "NumCasesNewMeds133", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "L" + rowId, "NumCasesCuredLeish134", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "M" + rowId, "NumTreatmentFail135", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "N" + rowId, "NumCasesSaesLeish136", rng, xlsWorksheet, missing, AggLeishInd, false);
                AddIndValue(district, "O" + rowId, "NumDeathsLeish137", rng, xlsWorksheet, missing, AggLeishInd, false);

                rowId++;
            }

            rng = null;
        }

        private object AggLeishInd(AggregateIndicator ind1, object existingValue)
        {
            if (ind1.IndicatorId == 50)
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
