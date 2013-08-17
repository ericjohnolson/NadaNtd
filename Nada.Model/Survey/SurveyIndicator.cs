using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Base;

namespace Nada.Model.Survey
{
    [Serializable]
    public class SurveyIndicator : NadaClass, IDynamicIndicator
    {
        public SurveyIndicator()
        {
            DataType = "Text";
            IsEditable = true;
        }
        public int SurveyTypeId { get; set; }
        public int DataTypeId { get; set; }
        public string DisplayName { get; set; }
        public int SortOrder { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsEditable { get; set; }

        // Display only props
        public string DataType { get; set; }
        public string EditText { get { return "Edit"; } }
        public bool IsEdited { get; set; }
    }
}
