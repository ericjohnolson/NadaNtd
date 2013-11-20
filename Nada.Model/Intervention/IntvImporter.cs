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

namespace Nada.Model.Intervention
{
    public class LfMdaImporter : ImporterBase, IImporter
    {
        public LfMdaImporter(IntvType t) : base(t)
        {
        }

        protected override void AddSpecificRows(DataTable dataTable)
        {
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.StartDateMda));
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.EndDateMda));
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.PcsTargeted));
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.Partners));
        }

        public ImportResult ImportData(string filePath, int userId)
        {
            try
            {
                DataSet ds = LoadDataFromFile(filePath);

                if (ds.Tables.Count == 0)
                    return new ImportResult("The file was an incorrect format or had no data");

                IntvRepository repo = new IntvRepository();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    PcMda intv = repo.CreateIntv<PcMda>((int)StaticIntvType.IvmAlbMda);
                    intv.AdminLevelId = Convert.ToInt32(row[Translations.Location + "#"]);
                    intv.Notes = row[Translations.Notes].ToString();
                    double d = 0;
                    if (double.TryParse(row[Translations.StartDateMda].ToString(), out d))
                        intv.StartDate = DateTime.FromOADate(d);
                    double d2 = 0;
                    if (double.TryParse(row[Translations.EndDateMda].ToString(), out d2))
                        intv.EndDate = DateTime.FromOADate(d2);
                    // HOW TO IMPORT Distros, funders, partners?
                    intv.IndicatorValues = GetDynamicIndicatorValues(ds, row);
                    repo.Save(intv, userId);
                }

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
            get { return Translations.IvmAlbIntervention; }
        }

        public void CreateImportFile(string filename, List<AdminLevel> adminLevels)
        {
            DiseaseRepository repo = new DiseaseRepository();
            List<Disease> diseases = repo.GetSelectedDiseases();
            IntvRepository repo2 = new IntvRepository();
            List<Partner> partners = repo2.GetPartners();
            PcMda model = new PcMda();
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook xlsWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet;
            Microsoft.Office.Interop.Excel.DropDowns xlDropDowns;
            object oMissing = System.Reflection.Missing.Value;

            //Create new workbook
            xlsWorkbook = xlsApp.Workbooks.Add(true);

            //Get the first worksheet
            xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)(xlsWorkbook.Worksheets[1]);

            // Load data into excel worksheet
            DataTable data = GetDataTable();
            AddDataToWorksheet(data, xlsWorksheet, adminLevels);

            // Set up all the different drop downs and multiselects
            xlDropDowns = ((Microsoft.Office.Interop.Excel.DropDowns)(xlsWorksheet.DropDowns(oMissing)));

            string stockOutCol = GetExcelColumnName(data.Columns[Translations.StockOut].Ordinal + 1);
            string drugsCol = GetExcelColumnName(data.Columns[Translations.StockOutDrug].Ordinal + 1);
            string lengthCol = GetExcelColumnName(data.Columns[Translations.StockOutLength].Ordinal + 1);
            string partnersCol = GetExcelColumnName(data.Columns[Translations.Partners].Ordinal + 1);
            string pcsCol = GetExcelColumnName(data.Columns[Translations.PcsTargeted].Ordinal + 1);
            for (int i = 1; i <= data.Rows.Count; i++)
            {
                AddDropdown(xlsWorksheet, xlDropDowns, model.StockOutValues, stockOutCol, i + 1);
                AddDropdown(xlsWorksheet, xlDropDowns, model.StockOutDrugValues, drugsCol, i + 1);
                AddDropdown(xlsWorksheet, xlDropDowns, model.StockOutLengthValues, lengthCol, i + 1);
                AddDropdown(xlsWorksheet, xlDropDowns, Util.ProduceEnumeration(partners.Select(p => p.DisplayName).ToList()), partnersCol, i + 1);
                AddDropdown(xlsWorksheet, xlDropDowns, Util.ProduceEnumeration(diseases.Select(p => p.DisplayName).ToList()), pcsCol, i + 1);
            }

            xlsApp.DisplayAlerts = false;
            xlsWorkbook.Close(true, filename, null);
            xlsApp.Quit();
            xlsWorksheet = null;
            xlsWorkbook = null;
            xlsApp = null;
        }
    }

    public class IntvImporter : ImporterBase, IImporter
    {
        StaticIntvType intvType;
        string importName;

        public IntvImporter(IHaveDynamicIndicators i, StaticIntvType t, string name)
            : base(i)
        {
            intvType = t;
            importName = name;
        }

        public void CreateImportFile(string filename, List<AdminLevel> adminLevels)
        {
            throw new NotImplementedException();
        }

        protected override void AddSpecificRows(DataTable dataTable)
        {
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.StartDateMda));
        }

        public ImportResult ImportData(string filePath, int userId)
        {
            try
            {
                DataSet ds = LoadDataFromFile(filePath);

                if (ds.Tables.Count == 0)
                    return new ImportResult("The file was an incorrect format or had no data");

                IntvRepository repo = new IntvRepository();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    IntvBase intv = repo.CreateIntv<IntvBase>((int)intvType);
                    intv.AdminLevelId = Convert.ToInt32(row[Translations.Location + "#"]);
                    intv.Notes = row[Translations.Notes].ToString();

                    double d = 0;
                    if (double.TryParse(row[Translations.StartDateMda].ToString(), out d))
                        intv.StartDate = DateTime.FromOADate(d);

                    intv.IndicatorValues = GetDynamicIndicatorValues(ds, row);
                    repo.SaveBase(intv, userId);
                }

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
            get { return importName; }
        }
    }
}
