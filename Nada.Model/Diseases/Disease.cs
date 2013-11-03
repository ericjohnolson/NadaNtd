using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Diseases
{
    public class Disease : NadaClass
    {
        public Disease()
        {
            EditText = Translations.Edit;
            DeleteText = Translations.Delete;
        }

        public string DisplayName { get; set; }
        public string DiseaseType { get; set; }
        public string EditText { get; set; }
        public string DeleteText { get; set; }
    }
}
