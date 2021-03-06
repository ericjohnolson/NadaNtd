﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Excel;
using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Imports;
using Nada.Model.Repositories;

namespace Nada.Model
{

    /// <summary>
    /// Handles creating an import file for AdminLevelDemography and then importing the data
    /// </summary>
    public class AdminLevelDemoUpdater : ImporterBase
    {
        AdminLevelType locationType = null;
        DemoRepository demo = new DemoRepository();
        int? countryDemoId = null;

        public AdminLevelDemoUpdater(AdminLevelType l, int? cid)
            : base()
        {
            countryDemoId = cid;
            locationType = l;
        }

        public void CreateUpdateFile(string filename, bool addData)
        {
            // Get data
            var cDemo = demo.GetCountryDemoRecent();
            var levels = demo.GetRecentDemography(locationType.LevelNumber, cDemo.DateDemographyData.Year);
            DataTable data = new DataTable();
            if (addData)
                data = CreateUpdateDataTable(levels);
            else
                data = CreateInsertDataTable(locationType);

            // Create excel
            System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook xlsWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet;
            object oMissing = System.Reflection.Missing.Value;

            //Create new workbook
            xlsWorkbook = xlsApp.Workbooks.Add(true);

            //Get the first worksheet
            xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)(xlsWorkbook.Worksheets[1]);

            // Load data into excel worksheet
            AddTableToWorksheet(data, xlsWorksheet);

            xlsApp.DisplayAlerts = false;
            xlsWorkbook.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, oMissing,
                oMissing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true,
                oMissing, oMissing, oMissing);
            xlsApp.Visible = true;
            xlsWorksheet = null;
            xlsWorkbook = null;
            xlsApp = null;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }

        private DataTable CreateInsertDataTable(AdminLevelType type)
        {
            DataTable data = new System.Data.DataTable();
            data.Columns.Add(new System.Data.DataColumn("* " + TranslationLookup.GetValue("Location") + "#"));
            data.Columns.Add(new System.Data.DataColumn("* " + TranslationLookup.GetValue("YearCensus")));
            data.Columns.Add(new System.Data.DataColumn("* " + TranslationLookup.GetValue("GrowthRate")));
            data.Columns.Add(new System.Data.DataColumn("* " + TranslationLookup.GetValue("TotalPopulation")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("Pop0Month")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("PopPsac")));
            data.Columns.Add(new System.Data.DataColumn("* " + TranslationLookup.GetValue("PopSac")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("Pop5yo")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("PopAdult")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("PopFemale")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("PopMale")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("PercentRural")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("Notes")));

            DemoRepository drepo = new DemoRepository();
            var allUnits = drepo.GetAdminLevelByLevel(type.LevelNumber);
            foreach (AdminLevel l in allUnits)
            {
                DataRow row = data.NewRow();
                row["* " + TranslationLookup.GetValue("Location") + "#"] = l.Id; 

                List<AdminLevel> parents = demo.GetAdminLevelParentNames(l.Id);
                for (int i = 0; i < parents.Count; i++)
                {
                    if (!data.Columns.Contains("* " + parents[i].LevelName))
                    {
                        DataColumn dc = new DataColumn("* " + parents[i].LevelName);
                        data.Columns.Add(dc);
                        dc.SetOrdinal(i + 1);
                    }
                    row["* " + parents[i].LevelName] = parents[i].Name;
                }
                
                data.Rows.Add(row);
            }
            return data;
        }

        protected DataTable CreateUpdateDataTable(List<AdminLevelDemography> levels)
        {
            DataTable data = new System.Data.DataTable();
            data.Columns.Add(new System.Data.DataColumn("* " + TranslationLookup.GetValue("ID") + "#"));
            data.Columns.Add(new System.Data.DataColumn("* " + TranslationLookup.GetValue("Location") + "#"));
            data.Columns.Add(new System.Data.DataColumn("* " + TranslationLookup.GetValue("YearCensus")));
            data.Columns.Add(new System.Data.DataColumn("* " + TranslationLookup.GetValue("GrowthRate")));
            data.Columns.Add(new System.Data.DataColumn("* " + TranslationLookup.GetValue("TotalPopulation")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("Pop0Month")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("PopPsac")));
            data.Columns.Add(new System.Data.DataColumn("* " + TranslationLookup.GetValue("PopSac")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("Pop5yo")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("PopAdult")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("PopFemale")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("PopMale")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("PercentRural")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("Notes")));

            // Add rows to data table
            foreach (AdminLevelDemography l in levels)
            {
                DataRow row = data.NewRow();
                row["* " + TranslationLookup.GetValue("ID") + "#"] = l.Id;
                row["* " + TranslationLookup.GetValue("Location") + "#"] = l.AdminLevelId;

                List<AdminLevel> parents = demo.GetAdminLevelParentNames(l.Id);
                for (int i = 0; i < parents.Count; i++)
                {
                    if (!data.Columns.Contains("* " + parents[i].LevelName))
                    {
                        DataColumn dc = new DataColumn("* " + parents[i].LevelName);
                        data.Columns.Add(dc);
                        dc.SetOrdinal(i + 2);
                    }
                    row["* " + parents[i].LevelName] = parents[i].Name;
                }

                row["* " + TranslationLookup.GetValue("YearCensus")] = l.YearCensus;
                row["* " + TranslationLookup.GetValue("GrowthRate")] = l.GrowthRate;
                row["* " + TranslationLookup.GetValue("TotalPopulation")] = l.TotalPopulation;
                row[TranslationLookup.GetValue("Pop0Month")] = l.Pop0Month;
                row[TranslationLookup.GetValue("PopPsac")] = l.PopPsac;
                row["* " + TranslationLookup.GetValue("PopSac")] = l.PopSac;
                row[TranslationLookup.GetValue("Pop5yo")] = l.Pop5yo;
                row[TranslationLookup.GetValue("PopAdult")] = l.PopAdult;
                row[TranslationLookup.GetValue("PopFemale")] = l.PopFemale;
                row[TranslationLookup.GetValue("PopMale")] = l.PopMale;
                row[TranslationLookup.GetValue("PercentRural")] = l.PercentRural;
                row[TranslationLookup.GetValue("Notes")] = l.Notes;
                data.Rows.Add(row);
            }
            return data;
        }

        public ImportResult ImportData(string filePath, int userId, DateTime dateReported, bool doAggregate)
        {
            try
            {
                System.Globalization.CultureInfo cultureEn = new System.Globalization.CultureInfo("en-US");
                DataSet ds = LoadDataFromFile(filePath);

                if (ds.Tables.Count == 0)
                    return new ImportResult(TranslationLookup.GetValue("NoDataFound"));

                string errorMessage = "";
                List<AdminLevelDemography> demos = new List<AdminLevelDemography>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var demography = new AdminLevelDemography();
                    if (ds.Tables[0].Columns.Contains("* " + TranslationLookup.GetValue("ID") + "#"))
                        demography.Id = Convert.ToInt32(row["* " + TranslationLookup.GetValue("ID") + "#"]);
                    demography.AdminLevelId = Convert.ToInt32(row["* " + TranslationLookup.GetValue("Location") + "#"]);
                    demography.Notes = row[TranslationLookup.GetValue("Notes")].ToString();
                    demography.DateDemographyData = dateReported;
                    // need to do the required validation, do all and then show errors
                    int i = 0;
                    if (int.TryParse(row["* " + TranslationLookup.GetValue("YearCensus")].ToString(), out i))
                        demography.YearCensus = i;
                    
                    double d = 0;
                    int integer = 0;
                    if (double.TryParse(row["* " + TranslationLookup.GetValue("GrowthRate")].ToString(), NumberStyles.Any, cultureEn, out d))
                        demography.GrowthRate = d;
                    if (double.TryParse(row[TranslationLookup.GetValue("PercentRural")].ToString(), NumberStyles.Any, cultureEn, out d))
                        demography.PercentRural = d;
                    if (TryParseAsIntThenDouble(row["* " + TranslationLookup.GetValue("TotalPopulation")].ToString(), NumberStyles.Any, cultureEn, out integer))
                        demography.TotalPopulation = integer;
                    if (TryParseAsIntThenDouble(row[TranslationLookup.GetValue("Pop0Month")].ToString(), NumberStyles.Any, cultureEn, out integer))
                        demography.Pop0Month = integer;
                    if (TryParseAsIntThenDouble(row[TranslationLookup.GetValue("PopPsac")].ToString(), NumberStyles.Any, cultureEn, out integer))
                        demography.PopPsac = integer;
                    if (TryParseAsIntThenDouble(row["* " + TranslationLookup.GetValue("PopSac")].ToString(), NumberStyles.Any, cultureEn, out integer))
                        demography.PopSac = integer;
                    if (TryParseAsIntThenDouble(row[TranslationLookup.GetValue("Pop5yo")].ToString(), NumberStyles.Any, cultureEn, out integer))
                        demography.Pop5yo = integer;
                    if (TryParseAsIntThenDouble(row[TranslationLookup.GetValue("PopAdult")].ToString(), NumberStyles.Any, cultureEn, out integer))
                        demography.PopAdult = integer;
                    if (TryParseAsIntThenDouble(row[TranslationLookup.GetValue("PopFemale")].ToString(), NumberStyles.Any, cultureEn, out integer))
                        demography.PopFemale = integer;
                    if (TryParseAsIntThenDouble(row[TranslationLookup.GetValue("PopMale")].ToString(), NumberStyles.Any, cultureEn, out integer))
                        demography.PopMale = integer;


                    var demographyErrors = !demography.IsValid() ? demography.GetAllErrors(true) : "";
                    if (!string.IsNullOrEmpty(demographyErrors))
                        errorMessage += string.Format(TranslationLookup.GetValue("ImportErrors"), row["* " + locationType.DisplayName]) + demographyErrors + Environment.NewLine;

                    demos.Add(demography);
                }

                if (!string.IsNullOrEmpty(errorMessage))
                    return new ImportResult(TranslationLookup.GetValue("ImportErrorHeader") + Environment.NewLine + errorMessage);

                demo.Save(demos, userId);

                if (doAggregate)
                    demo.AggregateUp(locationType, dateReported, userId, null, countryDemoId);
                
                int rec = demos.Count;
                return new ImportResult
                {
                    WasSuccess = true,
                    Count = rec,
                    Message = string.Format(TranslationLookup.GetValue("ImportSuccess"), rec)
                };
            }
            catch (Exception ex)
            {
                return new ImportResult(TranslationLookup.GetValue("UnexpectedException") + ex.Message);
            }
        }

    }

    /// <summary>
    /// Handles creating an import file for AdminLevels and then importing the data
    /// </summary>
    public class AdminLevelDemoImporter : ImporterBase
    {
        DemoRepository demo = new DemoRepository();
        SettingsRepository settings = new SettingsRepository();
        AdminLevelType locationType = null;
        AdminLevelType filterByType = null;
        AdminLevelType dropdownBy = null;
        AdminLevel filterBy = null;
        List<string> dropdownValues = null;
        int? countryDemoId = null;

        public AdminLevelDemoImporter(AdminLevelType l, int? cid)
            : base()
        {
            countryDemoId = cid;
            locationType = l;
        }

        public override string ImportName
        {
            get { return TranslationLookup.GetValue("ImportAdminLevels") + locationType.DisplayName; }
        }

        public void CreateImportFile(string filename, bool importDemography, int rows, AdminLevel filterLevel)
        {
            // Get data
            filterBy = filterLevel;
            int dropdownCol = GetParams();
            DataTable data = CreateNewImportDataTable(importDemography);
            DemoRepository demo = new DemoRepository();
            CountryDemography recentCountryDemo = demo.GetCountryDemoRecent();

            // Create excel
            System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook xlsWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet;
            Microsoft.Office.Interop.Excel.Worksheet xlsValidation;
            object oMissing = System.Reflection.Missing.Value;
            validationRanges = new Dictionary<string, string>();

            //Create new workbook
            xlsWorkbook = xlsApp.Workbooks.Add(true);

            //Get the first worksheet
            xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)(xlsWorkbook.Worksheets[1]);

            // add hidden validation worksheet
            xlsValidation = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorkbook.Worksheets.Add(oMissing, xlsWorksheet, oMissing, oMissing);
            xlsValidation.Name = validationSheetName;
            xlsValidation.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden;

            // Add columns
            int iCol = 0;
            foreach (DataColumn c in data.Columns)
            {
                iCol++;
                xlsWorksheet.Cells[1, iCol] = c.ColumnName;
            }
            string totalPopColumn = "F";
            // Add rows
            for (int r = 1; r <= rows + 1; r++)
            {
                for (int i = 1; i < data.Columns.Count + 1; i++)
                {
                    if (r == 1)
                    {
                        // Add the header the first time through 
                        xlsWorksheet.Cells[1, i] = data.Columns[i - 1].ColumnName;
                    }
                    else
                    {
                        if (i == 1 && filterByType != null)
                            xlsWorksheet.Cells[r, i] = filterLevel.Name;
                        if (dropdownCol == i && dropdownValues.Count > 0)
                        {
                            AddDataValidation(xlsWorksheet, xlsValidation, Util.GetExcelColumnName(i), r, dropdownBy.DisplayName, TranslationLookup.GetValue("PleaseSelect"), dropdownValues, oldCI);
                        }

                        if (data.Columns[i - 1].ColumnName == TranslationLookup.GetValue("UrbanOrRural"))
                            AddDataValidation(xlsWorksheet, xlsValidation, Util.GetExcelColumnName(i), r, TranslationLookup.GetValue("UrbanOrRural"), TranslationLookup.GetValue("PleaseSelect"), 
                                new List<string> { TranslationLookup.GetValue("AdminUnitUrban"),  TranslationLookup.GetValue("AdminUnitRural"),  TranslationLookup.GetValue("AdminUnitPeriRural")}, oldCI);
                        
                        if (importDemography)
                        {
                            if (data.Columns[i - 1].ColumnName == "* " + TranslationLookup.GetValue("YearCensus"))
                                xlsWorksheet.Cells[r, i] = recentCountryDemo.YearCensus;
                            if (data.Columns[i - 1].ColumnName == "* " + TranslationLookup.GetValue("GrowthRate"))
                                xlsWorksheet.Cells[r, i] = recentCountryDemo.GrowthRate;
                            if (data.Columns[i - 1].ColumnName == "* " + TranslationLookup.GetValue("TotalPopulation"))
                                totalPopColumn = Util.GetExcelColumnName(i);
                            if (data.Columns[i - 1].ColumnName == TranslationLookup.GetValue("Pop0Month") && recentCountryDemo.Percent6mos.HasValue)
                                xlsWorksheet.Cells[r, i] = string.Format("={2}{1}*{0}/100", recentCountryDemo.Percent6mos, r, totalPopColumn);
                            if (data.Columns[i - 1].ColumnName == TranslationLookup.GetValue("PopPsac") && recentCountryDemo.PercentPsac.HasValue)
                                xlsWorksheet.Cells[r, i] = string.Format("={2}{1}*{0}/100", recentCountryDemo.PercentPsac, r, totalPopColumn);
                            if (data.Columns[i - 1].ColumnName == "* " + TranslationLookup.GetValue("PopSac") && recentCountryDemo.PercentSac.HasValue)
                                xlsWorksheet.Cells[r, i] = string.Format("={2}{1}*{0}/100", recentCountryDemo.PercentSac, r, totalPopColumn);
                            if (data.Columns[i - 1].ColumnName == TranslationLookup.GetValue("Pop5yo") && recentCountryDemo.Percent5yo.HasValue)
                                xlsWorksheet.Cells[r, i] = string.Format("={2}{1}*{0}/100", recentCountryDemo.Percent5yo, r, totalPopColumn);
                            if (data.Columns[i - 1].ColumnName == TranslationLookup.GetValue("PopAdult") && recentCountryDemo.PercentAdult.HasValue)
                                xlsWorksheet.Cells[r, i] = string.Format("={2}{1}*{0}/100", recentCountryDemo.PercentAdult, r, totalPopColumn);
                            if (data.Columns[i - 1].ColumnName == TranslationLookup.GetValue("PopFemale") && recentCountryDemo.PercentFemale.HasValue)
                                xlsWorksheet.Cells[r, i] = string.Format("={2}{1}*{0}/100", recentCountryDemo.PercentFemale, r, totalPopColumn);
                            if (data.Columns[i - 1].ColumnName == TranslationLookup.GetValue("PopMale") && recentCountryDemo.PercentMale.HasValue)
                                xlsWorksheet.Cells[r, i] = string.Format("={2}{1}*{0}/100", recentCountryDemo.PercentMale, r, totalPopColumn);
                            if (data.Columns[i - 1].ColumnName == TranslationLookup.GetValue("PercentRural") && recentCountryDemo.PercentRural.HasValue)
                                xlsWorksheet.Cells[r, i] = string.Format("={2}{1}*{0}/100", recentCountryDemo.PercentRural, r, totalPopColumn);
                        }
                    }
                }
            }

            var last = xlsWorksheet.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            var range = xlsWorksheet.get_Range("A1", last);
            range.Columns.AutoFit();

            xlsApp.DisplayAlerts = false;
            xlsWorkbook.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, oMissing,
                oMissing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true,
                oMissing, oMissing, oMissing);
            xlsApp.Visible = true;
            xlsWorksheet = null;
            xlsValidation = null;
            xlsWorkbook = null;
            xlsApp = null;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }

        protected DataTable CreateNewImportDataTable(bool isDemo)
        {
            DataTable data = new System.Data.DataTable();

            if (filterByType != null)
                data.Columns.Add(new System.Data.DataColumn("* " + filterByType.DisplayName));

            if (dropdownBy != null)
                data.Columns.Add(new System.Data.DataColumn("* " + dropdownBy.DisplayName));

            data.Columns.Add(new System.Data.DataColumn("* " + locationType.DisplayName));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("AltSpellingNames")));
            if (isDemo)
            {
                data.Columns.Add(new System.Data.DataColumn("* " + TranslationLookup.GetValue("YearCensus")));
                data.Columns.Add(new System.Data.DataColumn("* " + TranslationLookup.GetValue("GrowthRate")));
                data.Columns.Add(new System.Data.DataColumn("* " + TranslationLookup.GetValue("TotalPopulation")));
                data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("Pop0Month")));
                data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("PopPsac")));
                data.Columns.Add(new System.Data.DataColumn("* " + TranslationLookup.GetValue("PopSac")));
                data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("Pop5yo")));
                data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("PopAdult")));
                data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("PopFemale")));
                data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("PopMale")));
                data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("PercentRural")));
            }
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("LatWho")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("LngWho")));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("UrbanOrRural")));
            if (isDemo)
                data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("Notes")));

            return data;
        }

        public ImportResult ImportData(string filePath, int userId, bool importDemography, bool doAggregate, int rows,
            AdminLevel filterLevel, DateTime dateReported)
        {
            try
            {
                System.Globalization.CultureInfo cultureEn = new System.Globalization.CultureInfo("en-US");
                filterBy = filterLevel;
                GetParams();

                DataSet ds = LoadDataFromFile(filePath);

                if (ds.Tables.Count == 0)
                    return new ImportResult(TranslationLookup.GetValue("NoDataFound"));

                string errorMessage = "";
                Dictionary<string, int> parentIds = demo.GetParentIds(filterBy, dropdownBy);
                List<AdminLevel> levels = new List<AdminLevel>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int parentId = 1;
                    if (parentIds.Count > 0)
                        if (parentIds.ContainsKey(row["* " + dropdownBy.DisplayName].ToString().Trim().ToLower()))
                            parentId = parentIds[row["* " + dropdownBy.DisplayName].ToString().Trim().ToLower()];

                    var adminLevel = new AdminLevel
                    {
                        AdminLevelTypeId = locationType.Id,
                        LevelNumber = locationType.LevelNumber,
                        Name = row["* " + locationType.DisplayName].ToString(),
                        UrbanOrRural = row[TranslationLookup.GetValue("UrbanOrRural")].ToString(),
                        ParentId = parentId,
                        CurrentDemography = null
                    };

                    double d1 = 0;
                    if (double.TryParse(row[TranslationLookup.GetValue("LatWho")].ToString(), NumberStyles.Any, cultureEn, out d1))
                        adminLevel.LatWho = d1;
                    if (double.TryParse(row[TranslationLookup.GetValue("LngWho")].ToString(), NumberStyles.Any, cultureEn, out d1))
                        adminLevel.LngWho = d1;

                    if (importDemography)
                    {
                        var demography = new AdminLevelDemography();

                        demography.Notes = row[TranslationLookup.GetValue("Notes")].ToString();
                        demography.DateDemographyData = dateReported;
                        // need to do the required validation, do all and then show errors
                        int i = 0;
                        if (int.TryParse(row["* " + TranslationLookup.GetValue("YearCensus")].ToString(), out i))
                            demography.YearCensus = i;

                        double d = 0;
                        int integer = 0;
                        if (double.TryParse(row["* " + TranslationLookup.GetValue("GrowthRate")].ToString(), NumberStyles.Any, cultureEn, out d))
                            demography.GrowthRate = d;
                        if (double.TryParse(row[TranslationLookup.GetValue("PercentRural")].ToString(), NumberStyles.Any, cultureEn, out d))
                            demography.PercentRural = d;
                        if (TryParseAsIntThenDouble(row["* " + TranslationLookup.GetValue("TotalPopulation")].ToString(), NumberStyles.Any, cultureEn, out integer))
                            demography.TotalPopulation = integer;
                        if (TryParseAsIntThenDouble(row[TranslationLookup.GetValue("Pop0Month")].ToString(), NumberStyles.Any, cultureEn, out integer))
                            demography.Pop0Month = integer;
                        if (TryParseAsIntThenDouble(row[TranslationLookup.GetValue("PopPsac")].ToString(), NumberStyles.Any, cultureEn, out integer))
                            demography.PopPsac = integer;
                        if (TryParseAsIntThenDouble(row["* " + TranslationLookup.GetValue("PopSac")].ToString(), NumberStyles.Any, cultureEn, out integer))
                            demography.PopSac = integer;
                        if (TryParseAsIntThenDouble(row[TranslationLookup.GetValue("Pop5yo")].ToString(), NumberStyles.Any, cultureEn, out integer))
                            demography.Pop5yo = integer;
                        if (TryParseAsIntThenDouble(row[TranslationLookup.GetValue("PopAdult")].ToString(), NumberStyles.Any, cultureEn, out integer))
                            demography.PopAdult = integer;
                        if (TryParseAsIntThenDouble(row[TranslationLookup.GetValue("PopFemale")].ToString(), NumberStyles.Any, cultureEn, out integer))
                            demography.PopFemale = integer;
                        if (TryParseAsIntThenDouble(row[TranslationLookup.GetValue("PopMale")].ToString(), NumberStyles.Any, cultureEn, out integer))
                            demography.PopMale = integer;


                        adminLevel.CurrentDemography = demography;
                    }

                    var demographyErrors = (adminLevel.CurrentDemography != null && !adminLevel.CurrentDemography.IsValid()) ? adminLevel.CurrentDemography.GetAllErrors(true) : "";
                    var adminErrors = !adminLevel.IsValid() ? adminLevel.GetAllErrors(true) : "";
                    if (!string.IsNullOrEmpty(demographyErrors) && !string.IsNullOrEmpty(adminErrors))
                        errorMessage += string.Format(TranslationLookup.GetValue("ImportErrors"), adminLevel.Name) + adminErrors + "," + demographyErrors + Environment.NewLine;
                    else if (!string.IsNullOrEmpty(demographyErrors) || !string.IsNullOrEmpty(adminErrors))
                        errorMessage += string.Format(TranslationLookup.GetValue("ImportErrors"), adminLevel.Name) + adminErrors + demographyErrors + Environment.NewLine;

                    levels.Add(adminLevel);
                }

                // Validation rules and errors
                if (levels.Count != rows)
                    errorMessage = string.Format(TranslationLookup.GetValue("ImportRecordsDontMatch"), rows, levels.Count) + Environment.NewLine + errorMessage;

                if (!string.IsNullOrEmpty(errorMessage))
                    return new ImportResult(TranslationLookup.GetValue("ImportErrorHeader") + Environment.NewLine + errorMessage);

                demo.BulkImportAdminLevelsForLevel(levels, locationType.Id, userId);

                if (doAggregate)
                    demo.AggregateUp(locationType, dateReported, userId, null, countryDemoId); 

                int rec = levels.Count;
                return new ImportResult
                {
                    WasSuccess = true,
                    Count = rec,
                    Message = string.Format(TranslationLookup.GetValue("ImportSuccess"), rec)
                };
            }
            catch (Exception ex)
            {
                return new ImportResult(TranslationLookup.GetValue("UnexpectedException") + ex.Message);
            }
        }

        private int GetParams()
        {
            if (locationType.LevelNumber > 2)
                filterByType = settings.GetAdminLevelTypeByLevel(locationType.LevelNumber - 2);

            if (locationType.LevelNumber > 1)
            {
                dropdownBy = settings.GetAdminLevelTypeByLevel(locationType.LevelNumber - 1);
                var levels = demo.GetAdminLevelByLevel(locationType.LevelNumber - 1);
                if (filterBy != null)
                {
                    dropdownValues = levels.Where(a => a.ParentId == filterBy.Id).Select(a => a.Name).ToList();
                    return 2;
                }
                else
                {
                    dropdownValues = levels.Select(a => a.Name).ToList();
                    return 1;
                }
            }
            return -1;
        }


    }
}
