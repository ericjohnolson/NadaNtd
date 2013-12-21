using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Excel;
using Nada.Globalization;
using Nada.Model.Imports;
using Nada.Model.Repositories;

namespace Nada.Model
{
    public class ImporterBase : IImporter
    {
        public ImporterBase()
        {
            LoadRelatedLists();
        }

        protected Dictionary<string, Indicator> Indicators = null;
        protected Dictionary<int, Indicator> ColumnIdToIndicator = null;
        protected List<Partner> partners = new List<Partner>();
        private List<IndicatorDropdownValue> ezs = new List<IndicatorDropdownValue>();
        private List<IndicatorDropdownValue> eus = new List<IndicatorDropdownValue>();
        protected List<MonthItem> months = new List<MonthItem>();
        protected List<IndicatorDropdownValue> DropDownValues = null;
        public virtual string ImportName { get { return ""; } }
        public virtual List<TypeListItem> GetAllTypes() { return new List<TypeListItem>(); }
        public virtual void SetType(int id) { }

        public virtual void CreateImportFile(string filename, List<AdminLevel> adminLevels)
        {
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook xlsWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet;
            object oMissing = System.Reflection.Missing.Value;

            //Create new workbook
            xlsWorkbook = xlsApp.Workbooks.Add(true);

            //Get the first worksheet
            xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)(xlsWorkbook.Worksheets[1]);

            // row 1 column headers
            xlsWorksheet.Cells[1, 1] = Translations.Location + "#";
            xlsWorksheet.Cells[1, 2] = Translations.Location;
            int xlsColCount = 2;
            xlsColCount += AddTypeSpecific(xlsWorksheet);
            int colCountAfterStatic = xlsColCount;

            foreach (var key in Indicators.Keys)
            {
                if (Indicators[key].DataTypeId == (int)IndicatorDataType.SentinelSite)
                    continue;
                //TODO TEST DATE FIELD? if (Indicators[key].DataTypeId == (int)IndicatorDataType.Date)
                //    col = new DataColumn(TranslationLookup.GetValue(key, key), typeof(DateTime));
                xlsColCount++;
                xlsWorksheet.Cells[1, xlsColCount] = TranslationLookup.GetValue(key, key);
                ColumnIdToIndicator.Add(xlsColCount, Indicators[key]);
            }
            xlsWorksheet.Cells[1, xlsColCount + 1] = Translations.Notes;

            // row 2+ admin levels
            int xlsRowCount = 2;
            foreach (AdminLevel l in adminLevels)
            {
                xlsWorksheet.Cells[xlsRowCount, 1] = l.Id;
                xlsWorksheet.Cells[xlsRowCount, 2] = l.Name;
                AddTypeSpecificLists(xlsWorksheet, l.Id, xlsRowCount);
                int colCount = colCountAfterStatic;
                foreach (var key in Indicators.Keys)
                {
                    if (Indicators[key].DataTypeId == (int)IndicatorDataType.SentinelSite)
                        continue;
                    colCount++;
                    AddValueToCell(xlsWorksheet, colCount, xlsRowCount, "", Indicators[key]);
                }
                xlsRowCount++;
            }

            // Auto fit
            var last = xlsWorksheet.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            var range = xlsWorksheet.get_Range("A1", last);
            range.Columns.AutoFit();

            // Save and display
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

        protected virtual void AddTypeSpecificLists(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, int adminLevelId, int r)
        {

        }

        protected virtual int AddTypeSpecific(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet)
        {
            return 0;
        }

        private void AddValueToCell(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, int c, int r, string value, Indicator indicator)
        {
            if (indicator.DataTypeId == (int)IndicatorDataType.Partners)
                AddDataValidation(xlsWorksheet, Util.GetExcelColumnName(c), r, "", "", Util.ProduceEnumeration(partners.Select(p => p.DisplayName).ToList()));
            else if (indicator.DataTypeId == (int)IndicatorDataType.EvaluationUnit)
                AddDataValidation(xlsWorksheet, Util.GetExcelColumnName(c), r, "", "", eus.Select(p => p.DisplayName).ToList());
            else if (indicator.DataTypeId == (int)IndicatorDataType.EcologicalZone)
                AddDataValidation(xlsWorksheet, Util.GetExcelColumnName(c), r, "", "", ezs.Select(p => p.DisplayName).ToList());
            else if (indicator.DataTypeId == (int)IndicatorDataType.Multiselect)
                AddDataValidation(xlsWorksheet, Util.GetExcelColumnName(c), r, "", "",
                    Util.ProduceEnumeration(DropDownValues.Where(i => i.IndicatorId == indicator.Id).Select(p => p.DisplayName).ToList()));
            else if (indicator.DataTypeId == (int)IndicatorDataType.Dropdown)
                AddDataValidation(xlsWorksheet, Util.GetExcelColumnName(c), r, "", "",
                    DropDownValues.Where(i => i.IndicatorId == indicator.Id).Select(p => p.DisplayName).ToList());
            else if (indicator.DataTypeId == (int)IndicatorDataType.Month)
                AddDataValidation(xlsWorksheet, Util.GetExcelColumnName(c), r, "", "", months.Select(p => p.Name).ToList());
            else
                xlsWorksheet.Cells[r, c] = value;

        }

        public virtual ImportResult ImportData(string filePath, int userId)
        {
            try
            {
                DataSet ds = LoadDataFromFile(filePath);

                if (ds.Tables.Count == 0)
                    return new ImportResult(Translations.NoDataFound);
                return MapAndSaveObjects(ds, userId);
            }
            catch (Exception ex)
            {
                return new ImportResult(Translations.UnexpectedException + ex.Message);
            }
        }

        protected virtual ImportResult MapAndSaveObjects(DataSet ds, int userId)
        {
            throw new NotImplementedException();
        }


        protected DataSet LoadDataFromFile(string filePath)
        {
            DataSet ds = null;
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                if (filePath.EndsWith(".xlsx"))
                    using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                    {
                        excelReader.IsFirstRowAsColumnNames = true;
                        ds = excelReader.AsDataSet();
                        excelReader.Close();
                    }
                else
                    using (IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream))
                    {
                        excelReader.IsFirstRowAsColumnNames = true;
                        ds = excelReader.AsDataSet();
                        excelReader.Close();
                    }
            }
            return ds;
        }

        protected List<IndicatorValue> GetDynamicIndicatorValues(DataSet ds, DataRow row)
        {
            Dictionary<string, Indicator> translatedIndicators = new Dictionary<string, Indicator>();
            foreach (var keyValue in Indicators)
                translatedIndicators.Add(TranslationLookup.GetValue(keyValue.Key, keyValue.Key), keyValue.Value);
            List<IndicatorValue> inds = new List<IndicatorValue>();
            foreach (DataColumn col in ds.Tables[0].Columns)
            {
                if (translatedIndicators.ContainsKey(col.ColumnName))
                {
                    string val = row[col].ToString();
                    double d = 0;
                    if (translatedIndicators[col.ColumnName].DataTypeId == (int)IndicatorDataType.Date && double.TryParse(val, out d))
                        val = DateTime.FromOADate(d).ToString();
                    else if (translatedIndicators[col.ColumnName].DataTypeId == (int)IndicatorDataType.Partners && !string.IsNullOrEmpty(val))
                    {
                        List<string> partnerIds = new List<string>();
                        string p = val.Replace(" - ", "-");
                        string[] vals = p.Split('-');
                        foreach (var partner in partners.Where(v => vals.Contains(v.DisplayName)))
                            partnerIds.Add(partner.Id.ToString());
                        val = string.Join("|", partnerIds.ToArray());
                    }
                    else if (translatedIndicators[col.ColumnName].DataTypeId == (int)IndicatorDataType.Multiselect && !string.IsNullOrEmpty(val))
                    {
                        string p = val.Replace(" - ", "-");
                        string[] vals = p.Split('-');
                        val = string.Join("|", vals);
                    }
                    else if (translatedIndicators[col.ColumnName].DataTypeId == (int)IndicatorDataType.EcologicalZone && !string.IsNullOrEmpty(val))
                    {
                        var selected = ezs.FirstOrDefault(v => v.DisplayName == val);
                        val = selected == null ? null : selected.Id.ToString();
                    }
                    else if (translatedIndicators[col.ColumnName].DataTypeId == (int)IndicatorDataType.EvaluationUnit && !string.IsNullOrEmpty(val))
                    {
                        var selected = eus.FirstOrDefault(v => v.DisplayName == val);
                        val = selected == null ? null : selected.Id.ToString();
                    }

                    inds.Add(new IndicatorValue
                    {
                        IndicatorId = translatedIndicators[col.ColumnName].Id,
                        DynamicValue = val,
                        Indicator = translatedIndicators[col.ColumnName]
                    });
                }
            }
            return inds;
        }

        private void LoadRelatedLists()
        {
            ColumnIdToIndicator = new Dictionary<int, Indicator>();
            IntvRepository repo = new IntvRepository();
            partners = repo.GetPartners();
            months = GlobalizationUtil.GetAllMonths();
            SettingsRepository settings = new SettingsRepository();
            ezs = settings.GetEcologicalZones();
            eus = settings.GetEvaluationUnits();
        }

        /// <summary>
        /// Adds a small Infobox and a Validation with restriction (only these values will be selectable) to the specified cell.
        /// </summary>
        /// <param name="worksheet">The excel-sheet</param>
        /// <param name="rowNr">1-based row index of the cell that will contain the validation</param>
        /// <param name="columnNr">1-based column index of the cell that will contain the validation</param>
        /// <param name="title">Title of the Infobox</param>
        /// <param name="message">Message in the Infobox</param>
        /// <param name="validationValues">List of available values for selection of the cell. No other value, than this list is allowed to be used.</param>
        /// <exception cref="Exception">Thrown, if an error occurs, or the worksheet was null.</exception>
        public static void AddDataValidation(Microsoft.Office.Interop.Excel.Worksheet worksheet, string col, int row,
            string title, string message, List<string> validationValues)
        {
            //If the message-string is too long (more than 255 characters, prune it)
            if (message.Length > 255)
                message = message.Substring(0, 254);

            try
            {
                //The validation requires a ';'-separated list of values, that goes as the restrictions-parameter.
                //Fold the list, so you can add it as restriction. (Result is "Value1;Value2;Value3")
                //If you use another separation-character (e.g in US) change the ; appropriately (e.g. to the ,)
                string values = string.Join(",", validationValues.ToArray());
                //Select the specified cell
                Microsoft.Office.Interop.Excel.Range cell = (Microsoft.Office.Interop.Excel.Range)worksheet.get_Range(col + row, col + row);
                //Delete any previous validation
                cell.Validation.Delete();
                //Add the validation, that only allowes selection of provided values.
                cell.Validation.Add(Microsoft.Office.Interop.Excel.XlDVType.xlValidateList, Microsoft.Office.Interop.Excel.XlDVAlertStyle.xlValidAlertStop,
                    Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlBetween, values, Type.Missing);
                cell.Validation.IgnoreBlank = true;
                //Optional put a message there
                cell.Validation.InputTitle = title;
                cell.Validation.InputMessage = message;

            }
            catch (Exception exception)
            {
                //This part should not be reached, but is used for stability-reasons
                throw new Exception(String.Format("Error when adding a Validation with restriction to the specified cell Row:{0}, Column:{1}, Message: {2}", row, col, message), exception);

            }
        }

        #region Data Table Specific

        protected virtual DataTable GetDataTable()
        {
            DataTable data = new System.Data.DataTable();
            data.Columns.Add(new System.Data.DataColumn(Translations.Location + "#"));
            data.Columns.Add(new System.Data.DataColumn(Translations.Location));
            AddDynamicIndicators(data);
            data.Columns.Add(new System.Data.DataColumn(Translations.Notes));
            return data;
        }

        private void AddDynamicIndicators(DataTable dataTable)
        {
            AddSpecificRows(dataTable);
            foreach (var key in Indicators.Keys)
            {
                DataColumn col = null;
                if (Indicators[key].DataTypeId == (int)IndicatorDataType.Date)
                    col = new DataColumn(TranslationLookup.GetValue(key, key), typeof(DateTime));
                else
                    col = new System.Data.DataColumn(TranslationLookup.GetValue(key, key));
                dataTable.Columns.Add(col);
            }
        }

        protected virtual void AddSpecificRows(DataTable dataTable) { }

        public void AddDataToWorksheet(DataTable data, Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, List<AdminLevel> rows)
        {
            // Add rows to data table
            foreach (AdminLevel l in rows)
            {
                DataRow row = data.NewRow();
                row[Translations.Location + "#"] = l.Id;
                row[Translations.Location] = l.Name;
                data.Rows.Add(row);
            }

            AddTableToWorksheet(data, xlsWorksheet);
        }

        public void AddTableToWorksheet(DataTable data, Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet)
        {
            // Add columns
            int iCol = 0;
            foreach (DataColumn c in data.Columns)
            {
                iCol++;
                xlsWorksheet.Cells[1, iCol] = c.ColumnName;
            }

            // Add rows
            int iRow = 0;
            foreach (DataRow r in data.Rows)
            {
                iRow++;

                for (int i = 1; i < data.Columns.Count + 1; i++)
                {
                    if (iRow == 1)
                    {
                        // Add the header the first time through 
                        xlsWorksheet.Cells[iRow, i] = data.Columns[i- 1].ColumnName;
                    }

                    if (r[1].ToString() != "")
                    {
                        xlsWorksheet.Cells[iRow + 1, i] = r[i -1].ToString();
                    }
                }
            }

            var last = xlsWorksheet.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            var range = xlsWorksheet.get_Range("A1", last);
            range.Columns.AutoFit();
        }
        #endregion
    }
}
