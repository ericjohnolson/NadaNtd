using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Repositories;

namespace Nada.Model.Exports
{
    public class CmJrfExporter : ExporterBase, IExporter
    {
        public string ExportName
        {
            get { return Translations.JrfCmNtds; }
        }

        public ExportResult ExportData(string filePath, int userId, int year)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Microsoft.Office.Interop.Excel.Workbook xlsWorkbook;
                Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet;
                object missing = System.Reflection.Missing.Value;

                // Open workbook
                xlsWorkbook = xlsApp.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, @"Exports\AFRO_CM_JRF.xls"),
                    missing, missing, missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing, missing, missing);

                // sheet 4 GW (start row 9)
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[5];
                AddIndicators(DiseaseType.GuineaWorm, StaticIntvType.GuineaWormIntervention, year, xlsWorksheet, AddGwInds, AggGwInd);
                //// leprosy
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[6];
                AddIndicators(DiseaseType.Leprosy, StaticIntvType.LeprosyIntervention, year, xlsWorksheet, AddLeprosyInds, AggLeprosyInd);
                //// hat
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[7];
                AddIndicators(DiseaseType.Hat, StaticIntvType.HatIntervention, year, xlsWorksheet, AddHatInds, AggHatInd);
                //// leish
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[8];
                AddIndicators(DiseaseType.Leish, StaticIntvType.LeishIntervention, year, xlsWorksheet, AddLeishInds, AggLeishInd);
                //// buruli
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[9];
                AddIndicators(DiseaseType.Buruli, StaticIntvType.BuruliUlcerIntv, year, xlsWorksheet, AddBuInds, AggBuInd);
                //// yaws
                xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets[10];
                AddIndicators(DiseaseType.Yaws, StaticIntvType.YawsIntervention, year, xlsWorksheet, AddYawsInds, AggYawsInd);

                xlsApp.DisplayAlerts = false;
                xlsWorkbook.SaveAs(filePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, missing,
                    missing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true,
                    missing, missing, missing);
                xlsApp.Visible = true;
                xlsWorksheet = null;
                xlsWorkbook = null;
                xlsApp = null;
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

        private void AddIndValue(AdminLevelIndicators district, string location, string key, Microsoft.Office.Interop.Excel.Range rng,
            Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, object missing, Func<AggregateIndicator, object, object> customAggRule,
            bool shouldTranslate)
        {
            // If district doesn't contain key
            object value = null;
            if (district.Indicators.ContainsKey(key))
                value = IndicatorAggregator.Aggregate(district.Indicators[key], null, customAggRule);
            else // compute key value
                value = IndicatorAggregator.AggregateChildren(district.Children, key, null, customAggRule);

            if (value != null)
            {
                rng = xlsWorksheet.get_Range(location, missing);
                if (shouldTranslate)
                    rng.Value = TranslationLookup.GetValue(value.ToString(), value.ToString());
                else
                    rng.Value = value;
            }
        }

        #region disease specific

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
                AddIndValue(district, "C" + rowId, "EndemicityStatus50", rng, xlsWorksheet, missing, AggGwInd, true);

                AddIndValue(district, "D" + rowId, "NumClCases126", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "E" + rowId, "NumLabClCases127", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "F" + rowId, "NumLabVlCases128", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "G" + rowId, "NumClVlCases129", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "H" + rowId, "NumCasesFoundActively130", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "I" + rowId, "NumCasesTreatedLeish131", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "J" + rowId, "NumCasesTreatedRef132", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "K" + rowId, "NumCasesNewMeds133", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "L" + rowId, "NumCasesCuredLeish134", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "M" + rowId, "NumTreatmentFail135", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "N" + rowId, "NumCasesSaesLeish136", rng, xlsWorksheet, missing, AggHatInd, false);
                AddIndValue(district, "O" + rowId, "NumDeathsLeish137", rng, xlsWorksheet, missing, AggHatInd, false);

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
                AddIndValue(district, "C" + rowId, "EndemicityStatus59", rng, xlsWorksheet, missing, AggGwInd, true);

                AddIndValue(district, "D" + rowId, "CaseFindingStrategy61", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "E" + rowId, "TotalNumNewCases63", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "I" + rowId, "TotalNumChildNewCases66", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "J" + rowId, "TotalNumFemaleNewCases67", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "G" + rowId, "TotalCat3Cases70", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "F" + rowId, "TotalCasesConfirmedPcr71", rng, xlsWorksheet, missing, AggGwInd, false);

                AddIndValue(district, "M" + rowId, "PrevBeginYear140", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "N" + rowId, "CasesRegisteredYear141", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "O" + rowId, "PatientsCompletedTreatment143", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "P" + rowId, "PatientsFullyScared144", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "Q" + rowId, "PatientsCuredWoDisability145", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "R" + rowId, "OtherWithdrawl146", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "S" + rowId, "AdultsAtEnd147", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "T" + rowId, "ChildrenAtEnd148", rng, xlsWorksheet, missing, AggGwInd, false);

                AddIndValue(district, "K" + rowId, "NumRelapsesCases152", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "L" + rowId, "NumCasesReadmitted153", rng, xlsWorksheet, missing, AggGwInd, false);

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

                AddIndValue(district, "C" + rowId, "EndemicityStatus60", rng, xlsWorksheet, missing, AggGwInd, true);

                AddIndValue(district, "D" + rowId, "CaseFindingStrategy62", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "E" + rowId, "TotalNumNewCases64", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "F" + rowId, "TotalCasesConfirmedLab72", rng, xlsWorksheet, missing, AggGwInd, false);
                
                AddIndValue(district, "G" + rowId, "PreSchoolCases674", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "H" + rowId, "SchoolCases1475", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "I" + rowId, "TotalNumFemaleNewCases76", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "J" + rowId, "CasesFromRural77", rng, xlsWorksheet, missing, AggGwInd, false);

                AddIndValue(district, "K" + rowId, "NumVillagesScreenedCm90", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "L" + rowId, "NumSchoolsScreenedCm91", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "M" + rowId, "TotalNumPeopleScreenedCm92", rng, xlsWorksheet, missing, AggGwInd, false);
           
                AddIndValue(district, "N" + rowId, "NumCasesTreatedYaws167", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "O" + rowId, "NumContactsTreatedYw168", rng, xlsWorksheet, missing, AggGwInd, false);

                AddIndValue(district, "P" + rowId, "NumCasesCuredYaws175", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "Q" + rowId, "NumFullyHealed176", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "R" + rowId, "NumPersonsBenza177", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "S" + rowId, "NumSaesYw178", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "T" + rowId, "NumTreatedAzithro179", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "U" + rowId, "NumFacilitiesProvidingAntibiotics180", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "V" + rowId, "NumVillagesWithVolunteers181", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "W" + rowId, "BenzaVials182", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "X" + rowId, "Benza6Vials183", rng, xlsWorksheet, missing, AggGwInd, false);
                AddIndValue(district, "Y" + rowId, "OtherAntibiotics184", rng, xlsWorksheet, missing, AggGwInd, false);

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
