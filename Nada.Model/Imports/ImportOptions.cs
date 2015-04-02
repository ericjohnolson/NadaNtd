using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Imports
{
    public class TypeListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ImportOptions : NadaClass, IDataErrorInfo
    {
        public ImportOptions()
        {
            IndicatorValuesSublist = new Dictionary<string, List<string>>();
        }
        public IImporter Importer { get; set; }
        public IndicatorEntityType EntityType { get; set; }
        public List<TypeListItem> Types { get; set; }
        public Nullable<int> TypeId { get; set; }
        public List<AdminLevel> AdminLevels { get; set; }
        public AdminLevelType AdminLevelType { get; set; }
        public Dictionary<string, List<string>> IndicatorValuesSublist { get; set; }
        public List<IndicatorDropdownValue> SurveyNames { get; set; }

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "TypeId":
                        if (!TypeId.HasValue)
                            error = Translations.Required;
                        break;

                    default: error = "";
                        break;

                }
                return error;
            }
        }
        #endregion
    }
}
