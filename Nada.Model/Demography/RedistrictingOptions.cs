using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Base;

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
        }
        public object Dashboard { get; set; }
        public AdminLevel Source { get; set; }
        public List<AdminLevelAllocation> SplitDestinations { get; set; }
        public SplittingType SplitType { get; set; }
        public Nullable<int> SplitIntoNumber { get; set; }
        public List<AdminLevel> SplitChildren { get; set; }
        public List<AdminLevel> MergeSources { get; set; }
        public AdminLevel MergeDestination { get; set; }

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
                        else if (SplitIntoNumber.Value < 0)
                            error = Translations.MustBeGreaterThanZero;
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
