using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Intervention
{
    public class DistributionMethod : NadaClass
    {
        public string DisplayName { get; set; }
        public string EditText { get { return Translations.View; } }
        public string DeleteText { get { return Translations.Delete; } }
    }
}
