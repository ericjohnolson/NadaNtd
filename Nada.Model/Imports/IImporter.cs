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
        bool HasGroupedAdminLevels(ImportOptions opts);
        void SetType(int id);
        string ImportName { get; }
        void CreateImportFile(string filename, List<AdminLevel> adminLevels, AdminLevelType adminLevelType, ImportOptions opts);
        ImportResult ImportData(string filePath, int userId);
        ImportResult ImportWithMulitpleAdminUnits(string filePath, int userId, Dictionary<string, List<AdminLevel>> namesToAdminUnits);
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
        public List<IHaveDynamicIndicatorValues> Forms { get; set; }
    }
}
