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
            IsEditable = true;
        }

        public int DataTypeId { get; set; }
        public bool CanAddValues { get; set; }
        public string DisplayName { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsEditable { get; set; }
        public bool IsRequired { get; set; }
        public bool IsDisplayed { get; set; }

        // Display only props
        public string DataType { get; set; }
        public string EditText { get { return Translations.View; } }
        public string DeleteText { get { return Translations.Delete; } }
        public bool IsEdited { get; set; }
    }
}
