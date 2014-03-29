using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model
{
    [Serializable]
    public class IndicatorDropdownValue : NadaClass
    {
        public IndicatorDropdownValue()
        {
        }

        public int IndicatorId { get; set; }
        public int SortOrder { get; set; }
        public string DisplayName { get; set; }
        public string TranslationKey { get; set; }
        public IndicatorEntityType EntityType { get; set; }


        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "DisplayName":
                        if (string.IsNullOrEmpty(DisplayName))
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
