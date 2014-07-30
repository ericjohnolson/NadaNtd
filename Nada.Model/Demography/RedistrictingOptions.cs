using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Base;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Process;

namespace Nada.Model.Demography
{
    public enum SplittingType
    {
        Split = 1,
        Merge = 2,
        SplitCombine = 3
    }

    public class AdminLevelAllocation
    {
        public AdminLevel Unit { get; set; }
        public double Percent { get; set; }
    }

    public class RedistrictingOptions : NadaClass
    {
        public RedistrictingOptions()
        {
            SplitDestinations = new List<AdminLevelAllocation>();
            Surveys = new List<SurveyBase>();
            Intvs = new List<IntvBase>();
            Processes = new List<ProcessBase>();
            DistrosCm = new List<DiseaseDistroCm>();
            DistrosPc = new List<DiseaseDistroPc>();
            Saes = new List<ProcessBase>();
            RedistrictDate = DateTime.Now;
        }
        public DateTime RedistrictDate { get; set; }
        public object Dashboard { get; set; }
        public AdminLevel Source { get; set; }
        public List<AdminLevelAllocation> SplitDestinations { get; set; }
        public SplittingType SplitType { get; set; }
        public Nullable<int> SplitIntoNumber { get; set; }
        public List<AdminLevel> SplitChildren { get; set; }
        public List<AdminLevel> MergeSources { get; set; }
        public AdminLevel MergeDestination { get; set; }
        public List<DiseaseDistroPc> DistrosPc { get; set; }
        public List<DiseaseDistroCm> DistrosCm { get; set; }
        public List<SurveyBase> Surveys { get; set; }
        public List<IntvBase> Intvs { get; set; }
        public List<ProcessBase> Processes { get; set; }
        public List<ProcessBase> Saes { get; set; }
        public int YearStartMonth { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SplitType: " + SplitType.ToString() + " | ");
            if (Source != null)
                sb.Append("Source: " + Source.Name + " id: " + Source.Id + " | ");
            if (MergeDestination != null)
                sb.Append("MergeDestination: " + MergeDestination.Name + " id: " + MergeDestination.Id + " | ");
            if (SplitDestinations != null && SplitDestinations.Count > 0)
                foreach(var dest in SplitDestinations)
                    sb.Append("SplitDestination: " + dest.Unit.Name + " id: " + dest.Unit.Id + " | ");
            if (MergeSources != null && MergeSources.Count > 0)
                foreach (var dest in MergeSources)
                    sb.Append("MergeSource: " + dest.Name + " id: " + dest.Id + " | ");

            return sb.ToString();
        }

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "SplitIntoNumber":
                        if (!SplitIntoNumber.HasValue)
                            error = Translations.Required;
                        else if (SplitIntoNumber.Value < 0 || SplitIntoNumber.Value > 10)
                            error = string.Format(Translations.ValidNumberRange, 1, 10);
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
