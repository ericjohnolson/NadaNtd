using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Exports;
using excel = Microsoft.Office.Interop.Excel;

namespace Nada.Model
{
    public interface IExporter
    {
        string ExportName { get; }
        ExportResult DoExport(string fileName, int userId, ExportType exportType);
    }

    public class ExportParams
    {
        public ExportJrfQuestions Questions { get; set; }
        public ExportCmJrfQuestions CmQuestions { get; set; }
        public int Year { get; set; }
        public string FileName { get; set; }
    }


    public class ExportResult
    {
        public ExportResult()
        {

        }
        public ExportResult(string error)
        {
            WasSuccess = false;
            ErrorMessage = error;
        }
        public bool WasSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ExporterBase
    {
        protected void AddValueToRange(excel.Worksheet xlsWorksheet, excel.Range rng, string cell, object value)
        {
            object missing = System.Reflection.Missing.Value;
            rng = xlsWorksheet.get_Range(cell, missing);
            rng.Value = value;
        }

        
    }
}
