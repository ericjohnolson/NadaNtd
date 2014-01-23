using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Base;
using Nada.UI.View.Demography;

namespace Nada.UI.View.Wizard.DistrictSplitting
{
    public enum SplittingType
    {
        Split,
        Merge,
        SplitCombine
    }

    public class AdminLevelAllocation
    {
        public AdminLevel Unit { get; set; }
        public double Percent { get; set; }
    }

    public class SplittingOptions : NadaClass
    {
        public SplittingOptions()
        {
            SplitDestinations = new List<AdminLevelAllocation>();
        }
        public AdminLevel Source { get; set; }
        public List<AdminLevelAllocation> SplitDestinations { get; set; }
        public DiseaseDashboard Dashboard { get; set; }
        public SplittingType SplitType { get; set; }
        public Nullable<int> SplitIntoNumber { get; set; }
        public List<AdminLevel> SplitChildren { get; set; }
        public List<AdminLevel> MergeSources { get; set; }
        public AdminLevel MergeDestination { get; set; }

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
