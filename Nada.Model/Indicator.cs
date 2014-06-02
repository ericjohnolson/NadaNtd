using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model
{
    [Serializable]
    public class Indicator : NadaClass
    {
        public Indicator()
        {
            DataType = Translations.Text;
        }

        public int DataTypeId { get; set; }
        public int NewYearTypeId { get; set; }
        public int RedistrictRuleId { get; set; }
        public int MergeRuleId { get; set; }
        public int AggRuleId { get; set; }
        public bool CanAddValues { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsEditable { get; set; }
        public bool IsRequired { get; set; }
        public bool IsDisplayed { get; set; }
        public bool IsCalculated { get; set; }
        public bool IsMetaData { get; set; }
        public string DisplayName { get; set; }

        // Display only props
        public string DataType { get; set; }
        public string EditText { get { return Translations.View; } }
        public string DeleteText { get { return Translations.Delete; } }
        public bool IsEdited { get; set; }

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
