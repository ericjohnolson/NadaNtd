using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Base;

namespace Nada.Model
{
    [Serializable]
    public class Indicator : NadaClass, IDynamicIndicator
    {
        public Indicator()
        {
            DataType = "Text";
            IsEditable = true;
        }
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
