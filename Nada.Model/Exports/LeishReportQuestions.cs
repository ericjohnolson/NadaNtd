using Nada.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model.Exports
{
    public class LeishReportQuestions : NadaClass
    {
        public Nullable<int> LeishRepYearReporting { get; set; }
        public Nullable<int> LeishRepEndemicAdmin2Vl { get; set; }
        public Nullable<int> LeishRepEndemicAdmin2Cl { get; set; }
        public Nullable<int> LeishRepYearLncpEstablished { get; set; }
        public string LeishRepUrlLncp { get; set; }
        public Nullable<int> LeishRepYearLatestGuide { get; set; }
        public bool LeishRepIsNotifiable { get; set; }
        public bool LeishRepIsVectProg { get; set; }
        public bool LeishRepIsHostProg { get; set; }
        public Nullable<int> LeishRepNumHealthFac { get; set; }
        public bool LeishRepIsTreatFree { get; set; }
        public string LeishRepAntiMedInNml { get; set; }
        public string LeishRepRelapseDefVl { get; set; }
        public string LeishRepRelapseDefCl { get; set; }
        public string LeishRepFailureDefVl { get; set; }
        public string LeishRepFailureDefCl { get; set; }
    }
}
