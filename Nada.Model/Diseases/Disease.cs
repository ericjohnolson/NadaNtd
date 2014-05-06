using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Diseases
{
    [Serializable]
    public class Disease : NadaClass
    {
        public Disease()
        {
            EditText = Translations.Edit;
            DeleteText = Translations.Delete;
            IsSelected = true;
            DiseaseType = Translations.Custom;
        }

        public string DisplayName { get; set; }
        public string UserDefinedName { get; set; }
        public string DiseaseType { get; set; }
        public bool IsSelected { get; set; }
        public string EditText { get; set; }
        public string DeleteText { get; set; }

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
