using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public interface IImporter
    {
        string ImportName { get; }
        System.Data.DataTable GetDataTable();
        ImportResult ImportData(string filePath, int userId);
    }

    public class ImportResult
    {
        public ImportResult()
        {

        }
        public ImportResult(string error)
        {
            WasSuccess = false;
            ErrorMessage = error;
        }
        public bool WasSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public int Count { get; set; }
    }
}
