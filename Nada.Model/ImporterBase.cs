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
    }
}
