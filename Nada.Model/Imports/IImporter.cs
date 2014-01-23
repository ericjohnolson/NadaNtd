using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Nada.Model.Imports;

namespace Nada.Model
{
    public interface IImporter
    {
        void SetType(int id);
        string ImportName { get; }
        void CreateImportFile(string filename, List<AdminLevel> adminLevels);
        ImportResult ImportData(string filePath, int userId);
        List<TypeListItem> GetAllTypes();
        Dictionary<string, Indicator> Indicators { get; set; }
        List<IndicatorDropdownValue> DropDownValues { get; set; }
        IndicatorEntityType EntityType { get; }
    }

    public class ImportResult
    {
        public ImportResult()
        {

        }
        public ImportResult(string error)
        {
            WasSuccess = false;
            Message = error;
        }
        public bool WasSuccess { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
    }
}
