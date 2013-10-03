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
                    dataTable.Columns.Add(new System.Data.DataColumn(key, typeof(DateTime)));
                else
                    dataTable.Columns.Add(new System.Data.DataColumn(key));
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
    }
}
