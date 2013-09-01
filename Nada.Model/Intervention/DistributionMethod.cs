using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Base;

namespace Nada.Model.Intervention
{
    public class DistributionMethod : NadaClass
    {
        public string DisplayName { get; set; }
        public string EditText { get { return "Edit"; } }
        public string DeleteText { get { return "Delete"; } }
    }
}
