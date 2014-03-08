using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        }

        public virtual IndicatorEntityType EntityType { get { return IndicatorEntityType.Intervention; } }
        public Dictionary<string, Indicator> Indicators { get; set; }
        public List<IndicatorDropdownValue> DropDownValues { get; set; }
        protected Dictionary<string, Indicator> translatedIndicators = new Dictionary<string, Indicator>();
        protected Dictionary<int, Indicator> ColumnIdToIndicator = null;
        protected List<Partner> partners = new List<Partner>();
        private List<IndicatorDropdownValue> ezs = new List<IndicatorDropdownValue>();
        private List<IndicatorDropdownValue> eus = new List<IndicatorDropdownValue>();
        private List<IndicatorDropdownValue> subdistricts = new List<IndicatorDropdownValue>();
        protected List<MonthItem> months = new List<MonthItem>();
        public virtual string ImportName { get { return ""; } }
        public virtual List<TypeListItem> GetAllTypes() { return new List<TypeListItem>(); }
        protected virtual void SetSpecificType(int id) { }
        public void SetType(int id)
        {
            translatedIndicators = new Dictionary<string, Indicator>();
            SetSpecificType(id);
            if (Indicators == null)
                throw new ArgumentException("Need to override SetSpecificType and set Indicators for import type");

            foreach (var keyValue in Indicators)
                translatedIndicators.Add(TranslationLookup.GetValue(keyValue.Key, keyValue.Key), keyValue.Value);
        }

        public virtual void CreateImportFile(string filename, List<AdminLevel> adminLevels)
        {
            LoadRelatedLists();
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

            // row 1 column headers
            xlsWorksheet.Cells[1, 1] = TranslationLookup.GetValue("Location") + "#";
            xlsWorksheet.Cells[1, 2] = TranslationLookup.GetValue("Location");
            int xlsColCount = 2;
            xlsColCount += AddTypeSpecific(xlsWorksheet);
            int colCountAfterStatic = xlsColCount;

            foreach (var item in Indicators)
            {
                if (item.Value.DataTypeId == (int)IndicatorDataType.SentinelSite || item.Value.IsCalculated || item.Value.IsMetaData)
                    continue;
                //TODO TEST DATE FIELD? if (Indicators[key].DataTypeId == (int)IndicatorDataType.Date)
                //    col = new DataColumn(TranslationLookup.GetValue(key, key), typeof(DateTime));
                xlsColCount++;
                xlsWorksheet.Cells[1, xlsColCount] = TranslationLookup.GetValue(item.Key, item.Key);
                ColumnIdToIndicator.Add(xlsColCount, item.Value);
            }
            xlsWorksheet.Cells[1, xlsColCount + 1] = TranslationLookup.GetValue("Notes");

            // row 2+ admin levels
            int xlsRowCount = 2;
            foreach (AdminLevel l in adminLevels)
            {
                xlsWorksheet.Cells[xlsRowCount, 1] = l.Id;
                xlsWorksheet.Cells[xlsRowCount, 2] = l.Name;
                AddTypeSpecificLists(xlsWorksheet, l.Id, xlsRowCount, oldCI);
                int colCount = colCountAfterStatic;
                foreach (var key in Indicators.Keys)
                {
                    if (Indicators[key].DataTypeId == (int)IndicatorDataType.SentinelSite || Indicators[key].IsCalculated || Indicators[key].IsMetaData)
                        continue;
                    colCount++;
                    AddValueToCell(xlsWorksheet, colCount, xlsRowCount, "", Indicators[key], oldCI);
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
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }

        protected virtual void AddTypeSpecificLists(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, int adminLevelId, int r, CultureInfo currentCulture)
        {

        }

        protected virtual int AddTypeSpecific(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet)
        {
            return 0;
        }

        private void AddValueToCell(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, int c, int r, string value, Indicator indicator, CultureInfo currentCulture)
        {
            if (indicator.DataTypeId == (int)IndicatorDataType.Partners)
                AddDataValidation(xlsWorksheet, Util.GetExcelColumnName(c), r, "", "", Util.ProduceEnumeration(partners.Select(p => p.DisplayName).ToList()), currentCulture);
            else if (indicator.DataTypeId == (int)IndicatorDataType.EvaluationUnit)
                AddDataValidation(xlsWorksheet, Util.GetExcelColumnName(c), r, "", "", eus.Select(p => p.DisplayName).ToList(), currentCulture);
            else if (indicator.DataTypeId == (int)IndicatorDataType.EcologicalZone)
                AddDataValidation(xlsWorksheet, Util.GetExcelColumnName(c), r, "", "", ezs.Select(p => p.DisplayName).ToList(), currentCulture);
            else if (indicator.DataTypeId == (int)IndicatorDataType.Multiselect)
                AddDataValidation(xlsWorksheet, Util.GetExcelColumnName(c), r, "", "",
                    Util.ProduceEnumeration(DropDownValues.Where(i => i.IndicatorId == indicator.Id).Select(p => p.DisplayName).ToList()), currentCulture);
            else if (indicator.DataTypeId == (int)IndicatorDataType.Dropdown)
                AddDataValidation(xlsWorksheet, Util.GetExcelColumnName(c), r, "", "",
                    DropDownValues.Where(i => i.IndicatorId == indicator.Id).Select(p => p.DisplayName).ToList(), currentCulture);
            else if (indicator.DataTypeId == (int)IndicatorDataType.Month)
                AddDataValidation(xlsWorksheet, Util.GetExcelColumnName(c), r, "", "", months.Select(p => p.Name).ToList(), currentCulture);
            else
                xlsWorksheet.Cells[r, c] = value;

        }

        public virtual ImportResult ImportData(string filePath, int userId)
        {
            LoadRelatedLists();
            try
            {
                DataSet ds = LoadDataFromFile(filePath);

                if (ds.Tables.Count == 0)
                    return new ImportResult(TranslationLookup.GetValue("NoDataFound"));
                return MapAndSaveObjects(ds, userId);
            }
            catch (Exception ex)
            {
                return new ImportResult(TranslationLookup.GetValue("UnexpectedException") + ex.Message);
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

        protected List<IndicatorValue> GetDynamicIndicatorValues(DataSet ds, DataRow row, ref string errors)
        {
            List<IndicatorValue> inds = new List<IndicatorValue>();
            foreach (DataColumn col in ds.Tables[0].Columns)
            {
                if (translatedIndicators.ContainsKey(col.ColumnName))
                {
                    string val = row[col].ToString().Trim();
                    Indicator curInd = translatedIndicators[col.ColumnName];

                    errors += GetValueAndValidate(curInd, ref val, col.ColumnName);

                    inds.Add(new IndicatorValue
                    {
                        IndicatorId = curInd.Id,
                        DynamicValue = val,
                        Indicator = curInd
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
            eus = settings.GetEvalSubDistricts();
        }

        private string GetValueAndValidate(Indicator indicator, ref string val, string name)
        {
            double d = 0;
            int i = 0;
            DateTime dt = new DateTime();

            if (indicator.IsRequired && string.IsNullOrEmpty(val))
                return name + ": " + TranslationLookup.GetValue("IsRequired") + Environment.NewLine;

            switch (indicator.DataTypeId)
            {
                case (int)IndicatorDataType.Date:
                    if (val.Length > 0 && !DateTime.TryParse(val, out dt))
                        return name + ": " + TranslationLookup.GetValue("MustBeDate") + Environment.NewLine;
                    else
                        val = dt.ToShortDateString();
                    break;
                case (int)IndicatorDataType.Number:
                    if (val.Length > 0 && !Double.TryParse(val, out d))
                        return name + ": " + TranslationLookup.GetValue("MustBeNumber") + Environment.NewLine;
                    break;
                case (int)IndicatorDataType.Year:
                    if (val.Length > 0 && (!int.TryParse(val, out i) || (i > 2100 || i < 1900)))
                        return name + ": " + TranslationLookup.GetValue("ValidYear") + Environment.NewLine;
                    break;
                case (int)IndicatorDataType.YesNo:
                    bool isChecked = false;
                    if (val.ToLower() == "no")
                        val = "false";
                    else if (val.ToLower() == "yes")
                        val = "true";
                    if (val.Length > 0 && !Boolean.TryParse(val, out isChecked))
                        return name + ": " + TranslationLookup.GetValue("MustBeYesNo") + Environment.NewLine;
                    val = isChecked.ToString();
                    break;
                case (int)IndicatorDataType.Multiselect:
                    val = val.Replace(Util.EnumerationDelinator, "|");
                    break;
                case (int)IndicatorDataType.Partners:
                    List<string> partnerIds = new List<string>();
                    string p = val.Replace(Util.EnumerationDelinator, "|");
                    string[] ps = p.Split('|');
                    foreach (var partner in partners.Where(v => ps.Contains(v.DisplayName)))
                        partnerIds.Add(partner.Id.ToString());
                    val = string.Join("|", partnerIds.ToArray());
                    break;
                case (int)IndicatorDataType.EvaluationUnit:
                    string euVal = val;
                    var eu = eus.FirstOrDefault(v => v.DisplayName == euVal);
                    val = eu == null ? null : eu.Id.ToString();
                    break;
                case (int)IndicatorDataType.EcologicalZone:
                    var evVal = val;
                    var ez = ezs.FirstOrDefault(v => v.DisplayName == evVal);
                    val = ez == null ? null : ez.Id.ToString();
                    break;
                case (int)IndicatorDataType.EvalSubDistrict:
                    var sdVal = val;
                    var sd = subdistricts.FirstOrDefault(v => v.DisplayName == sdVal);
                    val = sd == null ? null : sd.Id.ToString();
                    break;
            }

            // Values are manipulated in the above loop, double check if now they are required because invalid.
            if (indicator.IsRequired && string.IsNullOrEmpty(val))
                return name + ": " + TranslationLookup.GetValue("IsRequired") + Environment.NewLine;

            return "";
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
            string title, string message, List<string> validationValues, CultureInfo currentCulture)
        {
            if (validationValues == null || validationValues.Count == 0)
                return;
            //If the message-string is too long (more than 255 characters, prune it)
            if (message.Length > 255)
                message = message.Substring(0, 254);

            try
            {
                //The validation requires a ';'-separated list of values, that goes as the restrictions-parameter.
                //Fold the list, so you can add it as restriction. (Result is "Value1;Value2;Value3")
                //If you use another separation-character (e.g in US) change the ; appropriately (e.g. to the ,)
                string values = "";
                if (currentCulture.TwoLetterISOLanguageName == "en")
                    values = string.Join(",", validationValues.ToArray());
                else
                    values = string.Join(";", validationValues.ToArray());

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
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("Location") + "#"));
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("Location")));
            AddDynamicIndicators(data);
            data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("Notes")));
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
                row[TranslationLookup.GetValue("Location") + "#"] = l.Id;
                row[TranslationLookup.GetValue("Location")] = l.Name;
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
                        xlsWorksheet.Cells[iRow, i] = data.Columns[i - 1].ColumnName;
                    }

                    if (r[1].ToString() != "")
                    {
                        xlsWorksheet.Cells[iRow + 1, i] = r[i - 1].ToString();
                    }
                }
            }

            var last = xlsWorksheet.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            var range = xlsWorksheet.get_Range("A1", last);
            range.Columns.AutoFit();
        }

        protected string CreateErrorMessage(string errorMessage)
        {
            return TranslationLookup.GetValue("ImportErrorHeader") + Environment.NewLine + "--------" + Environment.NewLine + errorMessage;
        }

        protected string GetObjectErrors(string objerrors, string location)
        {
            if (!string.IsNullOrEmpty(objerrors))
                return Environment.NewLine + string.Format(TranslationLookup.GetValue("ImportErrors"), location) +
                    Environment.NewLine + "--------" + Environment.NewLine + objerrors;
            return "";
        }
        #endregion
    }
}
