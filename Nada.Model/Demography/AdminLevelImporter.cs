using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Excel;
using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Repositories;

namespace Nada.Model
{
    public class AdminLevelImporter : ImporterBase, IImporter
    {
        public AdminLevelImporter()
            : base()
        {
        }

        protected override void AddSpecificRows(DataTable dataTable)
        {
        }

        public void CreateImportFile(string filename, List<AdminLevel> adminLevels)
        {
            throw new NotImplementedException();
        }

        public ImportResult ImportData(string filePath, int userId)
        {
            try
            {
                DataSet ds = LoadDataFromFile(filePath);

                if (ds.Tables.Count == 0)
                    return new ImportResult("The file was an incorrect format or had no data");

                DemoRepository repo = new DemoRepository();
                repo.BulkImportAdminLevels(ds, userId);

                int rec = ds.Tables[0].Rows.Count;
                return new ImportResult
                {
                    WasSuccess = true,
                    Count = rec,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new ImportResult("An unexpected exception occurred: " + ex.Message);
            }
        }

        public string ImportName
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class AdminLevelDemoUpdater : ImporterBase
    {
        AdminLevelType locationType = null;
        DemoRepository demo = new DemoRepository();

        public AdminLevelDemoUpdater(AdminLevelType l)
            : base()
        {
            locationType = l;
        }

        public void CreateUpdateFile(string filename)
        {
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook xlsWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet;
            object oMissing = System.Reflection.Missing.Value;

            //Create new workbook
            xlsWorkbook = xlsApp.Workbooks.Add(true);

            //Get the first worksheet
            xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)(xlsWorkbook.Worksheets[1]);

            // Load data into excel worksheet
            var cDemo = demo.GetCountryDemoRecent();
            var levels = demo.GetRecentDemography(locationType.LevelNumber, cDemo.YearDemographyData.Value);
            DataTable data = CreateUpdateDataTable(levels);

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
        }

        protected DataTable CreateUpdateDataTable(List<AdminLevelDemography> levels)
        {
            DataTable data = new System.Data.DataTable();
            data.Columns.Add(new System.Data.DataColumn(Translations.ID + "#"));
            data.Columns.Add(new System.Data.DataColumn(Translations.Location + "#"));
            data.Columns.Add(new System.Data.DataColumn(locationType.DisplayName));
            data.Columns.Add(new System.Data.DataColumn(Translations.YearCensus));
            data.Columns.Add(new System.Data.DataColumn(Translations.YearProjections));
            data.Columns.Add(new System.Data.DataColumn(Translations.GrowthRate));
            data.Columns.Add(new System.Data.DataColumn(Translations.TotalPopulation));
            data.Columns.Add(new System.Data.DataColumn(Translations.Pop0Month));
            data.Columns.Add(new System.Data.DataColumn(Translations.PopPsac));
            data.Columns.Add(new System.Data.DataColumn(Translations.PopSac));
            data.Columns.Add(new System.Data.DataColumn(Translations.Pop5yo));
            data.Columns.Add(new System.Data.DataColumn(Translations.PopAdult));
            data.Columns.Add(new System.Data.DataColumn(Translations.PopFemale));
            data.Columns.Add(new System.Data.DataColumn(Translations.PopMale));
            data.Columns.Add(new System.Data.DataColumn(Translations.PercentRural));
            data.Columns.Add(new System.Data.DataColumn(Translations.Notes));

            // Add rows to data table
            foreach (AdminLevelDemography l in levels)
            {
                DataRow row = data.NewRow();
                row[Translations.ID + "#"] = l.Id;
                row[Translations.Location + "#"] = l.AdminLevelId;
                row[locationType.DisplayName] = l.NameDisplayOnly;
                row[Translations.YearCensus] = l.YearCensus;
                row[Translations.YearProjections] = l.YearProjections;
                row[Translations.GrowthRate] = l.GrowthRate;
                row[Translations.TotalPopulation] = l.TotalPopulation;
                row[Translations.Pop0Month] = l.Pop0Month;
                row[Translations.PopPsac] = l.PopPsac;
                row[Translations.PopSac] = l.PopSac;
                row[Translations.Pop5yo] = l.Pop5yo;
                row[Translations.PopAdult] = l.PopAdult;
                row[Translations.PopFemale] = l.PopFemale;
                row[Translations.PopMale] = l.PopMale;
                row[Translations.PercentRural] = l.PercentRural;
                row[Translations.Notes] = l.Notes;
                data.Rows.Add(row);
            }
            return data;
        }

        public ImportResult ImportData(string filePath, int userId, int yearDemo, bool doAggregate)
        {
            try
            {
                DataSet ds = LoadDataFromFile(filePath);

                if (ds.Tables.Count == 0)
                    return new ImportResult(Translations.NoDataFound);

                string errorMessage = "";
                List<AdminLevelDemography> demos = new List<AdminLevelDemography>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var demography = new AdminLevelDemography();
                    if(ds.Tables[0].Columns.Contains(Translations.ID + "#"))
                        demography.Id = Convert.ToInt32(row[Translations.ID + "#"]);
                    demography.AdminLevelId = Convert.ToInt32(row[Translations.Location + "#"]);
                    demography.Notes = row[Translations.Notes].ToString();
                    demography.YearDemographyData = yearDemo;
                    // need to do the required validation, do all and then show errors
                    int i = 0;
                    if (int.TryParse(row[Translations.YearCensus].ToString(), out i))
                        demography.YearCensus = i;
                    if (int.TryParse(row[Translations.YearProjections].ToString(), out i))
                        demography.YearProjections = i;
                    if (int.TryParse(row[Translations.TotalPopulation].ToString(), out i))
                        demography.TotalPopulation = i;
                    if (int.TryParse(row[Translations.Pop0Month].ToString(), out i))
                        demography.Pop0Month = i;
                    if (int.TryParse(row[Translations.PopPsac].ToString(), out i))
                        demography.PopPsac = i;
                    if (int.TryParse(row[Translations.PopSac].ToString(), out i))
                        demography.PopSac = i;
                    if (int.TryParse(row[Translations.Pop5yo].ToString(), out i))
                        demography.Pop5yo = i;
                    if (int.TryParse(row[Translations.PopAdult].ToString(), out i))
                        demography.PopAdult = i;
                    if (int.TryParse(row[Translations.PopFemale].ToString(), out i))
                        demography.PopFemale = i;
                    if (int.TryParse(row[Translations.PopMale].ToString(), out i))
                        demography.PopMale = i;

                    double d = 0;
                    if (double.TryParse(row[Translations.GrowthRate].ToString(), out d))
                        demography.GrowthRate = d;
                    if (double.TryParse(row[Translations.PopMale].ToString(), out d))
                        demography.PercentRural = d;


                    var demographyErrors = !demography.IsValid() ? demography.GetAllErrors(true) : "";
                    if (!string.IsNullOrEmpty(demographyErrors))
                        errorMessage += string.Format(Translations.ImportErrors, row[locationType.DisplayName], "", demographyErrors) + Environment.NewLine;

                    demos.Add(demography);
                }

                if (!string.IsNullOrEmpty(errorMessage))
                    return new ImportResult(Translations.ImportErrorHeader + Environment.NewLine + errorMessage);

                demo.Save(demos, userId);

                if (doAggregate)
                    demo.AggregateUp(locationType, yearDemo, userId);

                int rec = demos.Count;
                return new ImportResult
                {
                    WasSuccess = true,
                    Count = rec,
                    Message = string.Format(Translations.ImportSuccess, rec)
                };
            }
            catch (Exception ex)
            {
                return new ImportResult(Translations.UnexpectedException + ex.Message);
            }
        }

    }

    public class AdminLevelDemoImporter : ImporterBase
    {
        DemoRepository demo = new DemoRepository();
        SettingsRepository settings = new SettingsRepository();
        AdminLevelType locationType = null;
        AdminLevelType filterByType = null;
        AdminLevelType dropdownBy = null;
        AdminLevel filterBy = null;
        List<string> dropdownValues = null;

        public AdminLevelDemoImporter(AdminLevelType l)
            : base()
        {
            locationType = l;
        }

        public string ImportName
        {
            get { return Translations.ImportAdminLevels + locationType.DisplayName; }
        }

        public void CreateImportFile(string filename, bool importDemography, int rows, AdminLevel filterLevel)
        {
            filterBy = filterLevel;
            int dropdownCol = GetParams();
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook xlsWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet;
            object oMissing = System.Reflection.Missing.Value;

            //Create new workbook
            xlsWorkbook = xlsApp.Workbooks.Add(true);

            //Get the first worksheet
            xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)(xlsWorkbook.Worksheets[1]);

            // Load data into excel worksheet
            DataTable data = CreateNewImportDataTable(importDemography);
            
            // Add columns
            int iCol = 0;
            foreach (DataColumn c in data.Columns)
            {
                iCol++;
                xlsWorksheet.Cells[1, iCol] = c.ColumnName;
            }

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
                            xlsWorksheet.Cells[r, 1] = filterLevel.Name;
                        if (dropdownCol == i && dropdownValues.Count > 0)
                        {
                            AddDataValidation(xlsWorksheet, GetExcelColumnName(i), r, dropdownBy.DisplayName, Translations.PleaseSelect, dropdownValues);
                            //AddDropdown(xlsWorksheet, xlDropDowns, dropdownValues, GetExcelColumnName(i), r);
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
            xlsWorkbook = null;
            xlsApp = null;
        }

        protected DataTable CreateNewImportDataTable(bool isDemo)
        {
            DataTable data = new System.Data.DataTable();

            if (filterByType != null)
                data.Columns.Add(new System.Data.DataColumn(filterByType.DisplayName));

            if (dropdownBy != null)
                data.Columns.Add(new System.Data.DataColumn(dropdownBy.DisplayName));

            data.Columns.Add(new System.Data.DataColumn(locationType.DisplayName));
            data.Columns.Add(new System.Data.DataColumn(Translations.AltSpellingNames));
            if (isDemo)
            {
                data.Columns.Add(new System.Data.DataColumn(Translations.YearCensus));
                data.Columns.Add(new System.Data.DataColumn(Translations.YearProjections));
                data.Columns.Add(new System.Data.DataColumn(Translations.GrowthRate));
                data.Columns.Add(new System.Data.DataColumn(Translations.TotalPopulation));
                data.Columns.Add(new System.Data.DataColumn(Translations.Pop0Month));
                data.Columns.Add(new System.Data.DataColumn(Translations.PopPsac));
                data.Columns.Add(new System.Data.DataColumn(Translations.PopSac));
                data.Columns.Add(new System.Data.DataColumn(Translations.Pop5yo));
                data.Columns.Add(new System.Data.DataColumn(Translations.PopAdult));
                data.Columns.Add(new System.Data.DataColumn(Translations.PopFemale));
                data.Columns.Add(new System.Data.DataColumn(Translations.PopMale));
                data.Columns.Add(new System.Data.DataColumn(Translations.PercentRural));
            }
            data.Columns.Add(new System.Data.DataColumn(Translations.LatWho));
            data.Columns.Add(new System.Data.DataColumn(Translations.LngWho));
            data.Columns.Add(new System.Data.DataColumn(Translations.LatOther));
            data.Columns.Add(new System.Data.DataColumn(Translations.LngOther));
            data.Columns.Add(new System.Data.DataColumn(Translations.UrbanOrRural));
            if (isDemo)
                data.Columns.Add(new System.Data.DataColumn(Translations.Notes));

            return data;
        }

        public ImportResult ImportData(string filePath, int userId, bool importDemography, bool doAggregate, int rows,
            AdminLevel filterLevel, int yearDemo)
        {
            try
            {
                filterBy = filterLevel;
                GetParams();

                DataSet ds = LoadDataFromFile(filePath);

                if (ds.Tables.Count == 0)
                    return new ImportResult(Translations.NoDataFound);

                string errorMessage = "";
                Dictionary<string, int> parentIds = demo.GetParentIds(filterBy, dropdownBy);
                List<AdminLevel> levels = new List<AdminLevel>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int parentId = 1;
                    if (parentIds.Count > 0)
                        if (parentIds.ContainsKey(row[dropdownBy.DisplayName].ToString()))
                            parentId = parentIds[row[dropdownBy.DisplayName].ToString()];

                    var adminLevel = new AdminLevel
                    {
                        AdminLevelTypeId = locationType.Id,
                        LevelNumber = locationType.LevelNumber,
                        Name = row[locationType.DisplayName].ToString(),
                        UrbanOrRural = row[Translations.UrbanOrRural].ToString(),
                        ParentId = parentId,
                        CurrentDemography = null
                    };

                    double d1 = 0;
                    if (double.TryParse(row[Translations.LatWho].ToString(), out d1))
                        adminLevel.LatWho = d1;
                    if (double.TryParse(row[Translations.LngWho].ToString(), out d1))
                        adminLevel.LngWho = d1;
                    if (double.TryParse(row[Translations.LatOther].ToString(), out d1))
                        adminLevel.LatOther = d1;
                    if (double.TryParse(row[Translations.LngOther].ToString(), out d1))
                        adminLevel.LngOther = d1;

                    if (importDemography)
                    {
                        var demography = new AdminLevelDemography();

                        demography.Notes = row[Translations.Notes].ToString();
                        demography.YearDemographyData = yearDemo;
                        // need to do the required validation, do all and then show errors
                        int i = 0;
                        if (int.TryParse(row[Translations.YearCensus].ToString(), out i))
                            demography.YearCensus = i;
                        if (int.TryParse(row[Translations.YearProjections].ToString(), out i))
                            demography.YearProjections = i;
                        if (int.TryParse(row[Translations.TotalPopulation].ToString(), out i))
                            demography.TotalPopulation = i;
                        if (int.TryParse(row[Translations.Pop0Month].ToString(), out i))
                            demography.Pop0Month = i;
                        if (int.TryParse(row[Translations.PopPsac].ToString(), out i))
                            demography.PopPsac = i;
                        if (int.TryParse(row[Translations.PopSac].ToString(), out i))
                            demography.PopSac = i;
                        if (int.TryParse(row[Translations.Pop5yo].ToString(), out i))
                            demography.Pop5yo = i;
                        if (int.TryParse(row[Translations.PopAdult].ToString(), out i))
                            demography.PopAdult = i;
                        if (int.TryParse(row[Translations.PopFemale].ToString(), out i))
                            demography.PopFemale = i;
                        if (int.TryParse(row[Translations.PopMale].ToString(), out i))
                            demography.PopMale = i;

                        double d = 0;
                        if (double.TryParse(row[Translations.GrowthRate].ToString(), out d))
                            demography.GrowthRate = d;
                        if (double.TryParse(row[Translations.PopMale].ToString(), out d))
                            demography.PercentRural = d;

                        adminLevel.CurrentDemography = demography;
                    }

                    var demographyErrors = (adminLevel.CurrentDemography != null && !adminLevel.CurrentDemography.IsValid()) ? adminLevel.CurrentDemography.GetAllErrors(true) : "";
                    var adminErrors = !adminLevel.IsValid() ? adminLevel.GetAllErrors(true) : "";
                    if (!string.IsNullOrEmpty(demographyErrors) && !string.IsNullOrEmpty(adminErrors))
                        errorMessage += string.Format(Translations.ImportErrors, adminLevel.Name, adminErrors + ",", demographyErrors) + Environment.NewLine;
                    else if (!string.IsNullOrEmpty(demographyErrors) || !string.IsNullOrEmpty(adminErrors))
                        errorMessage += string.Format(Translations.ImportErrors, adminLevel.Name, adminErrors, demographyErrors) + Environment.NewLine;

                    levels.Add(adminLevel);
                }

                // Validation rules and errors
                if (levels.Count != rows)
                    errorMessage = string.Format(Translations.ImportRecordsDontMatch, rows, levels.Count) + Environment.NewLine + errorMessage;

                if (!string.IsNullOrEmpty(errorMessage))
                    return new ImportResult(Translations.ImportErrorHeader + Environment.NewLine + errorMessage);

                demo.BulkImportAdminLevelsForLevel(levels, locationType.Id, userId);

                if (doAggregate)
                    demo.AggregateUp(locationType, yearDemo, userId);

                int rec = levels.Count;
                return new ImportResult
                {
                    WasSuccess = true,
                    Count = rec,
                    Message = string.Format(Translations.ImportSuccess, rec)
                };
            }
            catch (Exception ex)
            {
                return new ImportResult(Translations.UnexpectedException + ex.Message);
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
