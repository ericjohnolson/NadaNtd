using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Exports
{

    public class ExportCmJrfQuestions : NadaClass
    {
        public ExportCmJrfQuestions()
        {
            Contacts = new List<ExportContact>();
        }
        public Nullable<int> YearReporting { get; set; }
        public bool CmHaveMasterPlan { get; set; }
        public string CmYearsMasterPlan { get; set; }
        public Nullable<int> CmBuget { get; set; }
        public Nullable<double> CmPercentFunded { get; set; }
        public bool CmHaveAnnualOpPlan { get; set; }
        public string CmDiseaseSpecOrNtdIntegrated { get; set; }
        public bool CmBuHasPlan { get; set; }
        public bool CmGwHasPlan { get; set; }
        public bool CmHatHasPlan { get; set; }
        public bool CmLeishHasPlan { get; set; }
        public bool CmLeprosyHasPlan { get; set; }
        public bool CmYawsHasPlan { get; set; }
        public bool CmAnySupplyFunds { get; set; }
        public bool CmHasStorage { get; set; }
        public string CmStorageNtdOrCombined { get; set; }
        public string CmStorageSponsor1 { get; set; }
        public string CmStorageSponsor2 { get; set; }
        public string CmStorageSponsor3 { get; set; }
        public string CmStorageSponsor4 { get; set; }
        public bool CmHasTaskForce { get; set; }
        public bool CmHasMoh { get; set; }
        public bool CmHasMosw { get; set; }
        public bool CmHasMot { get; set; }
        public bool CmHasMoe { get; set; }
        public bool CmHasMoc { get; set; }
        public bool CmHasUni { get; set; }
        public bool CmHasNgo { get; set; }
        public bool CmHasAnnualForum { get; set; }
        public bool CmForumHasRegions { get; set; }
        public bool CmForumHasTaskForce { get; set; }
        public bool CmHasNtdReviewMeetings { get; set; }
        public bool CmHasDiseaseSpecMeetings { get; set; }
        public bool CmHasGwMeeting { get; set; }
        public bool CmHasLeprosyMeeting { get; set; }
        public bool CmHasHatMeeting { get; set; }
        public bool CmHasLeishMeeting { get; set; }
        public bool CmHasBuMeeting { get; set; }
        public bool CmHasYawsMeeting { get; set; }
        public bool CmHasWeeklyMech { get; set; }
        public bool CmHasMonthlyMech { get; set; }
        public bool CmHasQuarterlyMech { get; set; }
        public bool CmHasSemesterMech { get; set; }
        public string CmOtherMechs { get; set; }
        public List<ExportContact> Contacts { get; set; }

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "YearReporting":
                        if (!YearReporting.HasValue)
                            error = Translations.Required;
                        else if (YearReporting.Value > 2100 || YearReporting.Value < 1900)
                            error = Translations.ValidYear;
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
