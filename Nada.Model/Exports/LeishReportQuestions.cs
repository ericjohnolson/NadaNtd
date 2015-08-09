using Nada.Globalization;
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

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "LeishRepYearReporting":
                        if (!LeishRepYearReporting.HasValue)
                            error = Translations.Required;
                        else if (LeishRepYearReporting.Value > 2100 || LeishRepYearReporting.Value < 1900)
                            error = Translations.ValidYear;
                        break;
                    case "LeishRepEndemicAdmin2Vl":
                        if (!LeishRepEndemicAdmin2Vl.HasValue)
                            error = Translations.Required;
                        break;
                    case "LeishRepEndemicAdmin2Cl":
                        if (!LeishRepEndemicAdmin2Cl.HasValue)
                            error = Translations.Required;
                        break;
                    case "LeishRepYearLncpEstablished":
                        if (!LeishRepYearLncpEstablished.HasValue)
                            error = Translations.Required;
                        else if (LeishRepYearLncpEstablished.Value > 2100 || LeishRepYearLncpEstablished.Value < 1900)
                            error = Translations.ValidYear;
                        break;
                    case "LeishRepUrlLncp":
                        if (string.IsNullOrEmpty(LeishRepUrlLncp))
                            error = Translations.Required;
                        break;
                    case "LeishRepYearLatestGuide":
                        if (!LeishRepYearLatestGuide.HasValue)
                            error = Translations.Required;
                        else if (LeishRepYearLatestGuide.Value > 2100 || LeishRepYearLatestGuide.Value < 1900)
                            error = Translations.ValidYear;
                        break;
                    case "LeishRepNumHealthFac":
                        if (!LeishRepNumHealthFac.HasValue)
                            error = Translations.Required;
                        break;
                    case "LeishRepAntiMedInNml":
                        if (string.IsNullOrEmpty(LeishRepAntiMedInNml))
                            error = Translations.Required;
                        break;
                    case "LeishRepRelapseDefVl":
                        if (string.IsNullOrEmpty(LeishRepRelapseDefVl))
                            error = Translations.Required;
                        break;

                    case "LeishRepRelapseDefCl":
                        if (string.IsNullOrEmpty(LeishRepRelapseDefCl))
                            error = Translations.Required;
                        break;
                    case "LeishRepFailureDefVl":
                        if (string.IsNullOrEmpty(LeishRepFailureDefVl))
                            error = Translations.Required;
                        break;
                    case "LeishRepFailureDefCl":
                        if (string.IsNullOrEmpty(LeishRepFailureDefCl))
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
