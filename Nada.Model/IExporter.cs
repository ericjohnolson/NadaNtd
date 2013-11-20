using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public interface IExporter
    {
        string ExportName { get; }
        ExportResult ExportData(string filePath, int userId, int year);
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
        
    }
}
