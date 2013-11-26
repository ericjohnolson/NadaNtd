using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Excel;
using Nada.Globalization;
using Nada.Model.Repositories;

namespace Nada.Model
{
    public class ImporterBase
    {
        Dictionary<string, Indicator> dynamicIndicators = null;

        public ImporterBase()
        {
            dynamicIndicators = new Dictionary<string, Indicator>();
        }

        public ImporterBase(IHaveDynamicIndicators i)
        {
            dynamicIndicators = i.Indicators;
        }

        public virtual DataTable GetDataTable()
        {
            DataTable data = new System.Data.DataTable();
            data.Columns.Add(new System.Data.DataColumn(Translations.Location + "#"));
            data.Columns.Add(new System.Data.DataColumn(Translations.Location));
            AddDynamicIndicators(data);
            data.Columns.Add(new System.Data.DataColumn(Translations.Notes));
            return data;
        }

        protected void AddDynamicIndicators(DataTable dataTable)
        {
            AddSpecificRows(dataTable);
            foreach (var key in dynamicIndicators.Keys)
                if (dynamicIndicators[key].DataTypeId == (int)IndicatorDataType.Date)
                    dataTable.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue(key, key), typeof(DateTime)));
                else
                    dataTable.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue(key, key)));
        }

        protected virtual void AddSpecificRows(DataTable dataTable)
        {
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
            List<IndicatorValue> inds = new List<IndicatorValue>();
            foreach (DataColumn col in ds.Tables[0].Columns)
            {
                if (dynamicIndicators.ContainsKey(col.ColumnName))
                {
                    string val = row[col].ToString();
                    double d = 0;
                    if (dynamicIndicators[col.ColumnName].DataTypeId == (int)IndicatorDataType.Date && double.TryParse(row[col].ToString(), out d))
                        val = DateTime.FromOADate(d).ToString();
                    inds.Add(new IndicatorValue
                    {
                        IndicatorId = dynamicIndicators[col.ColumnName].Id,
                        DynamicValue = val,
                        Indicator = dynamicIndicators[col.ColumnName]
                    });
                }
            }
            return inds;
        }

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

        protected string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        protected void AddDropdown(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, Microsoft.Office.Interop.Excel.DropDowns xlDropDowns,
            List<string> values, string col, int row)
        {
            var tRange = xlsWorksheet.get_Range(col + row, col + row);
            var tDropDown = xlDropDowns.Add((double)tRange.Left, (double)tRange.Top, (double)tRange.Width, (double)tRange.Height, true);
            for (int j = 0; j < values.Count(); j++)
                tDropDown.AddItem(TranslationLookup.GetValue(values[j], values[j]), j + 1);
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
    }
}
