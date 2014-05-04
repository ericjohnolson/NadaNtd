using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model.Demography
{
    public class RedistrictingResult
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class RedistrictingExpert
    {
        public RedistrictingResult Run(RedistrictingOptions options)
        {
            if (options.SplitType == SplittingType.Merge)
                return DoMerge(options);
            if (options.SplitType == SplittingType.Split)
                return DoSplit(options);

            return DoSplitCombine(options);
        }
        private RedistrictingResult DoSplit(RedistrictingOptions options)
        {
            // CREATE TRANSACTION! nees to be transactional duh?

            //Insert into redistricting table...

            // Move districts over to new parents
            // split all demography
            // split all surveys & other items
            return new RedistrictingResult();
        }
        private RedistrictingResult DoSplitCombine(RedistrictingOptions options)
        {
            return new RedistrictingResult();
        }
        private RedistrictingResult DoMerge(RedistrictingOptions options)
        {
            return new RedistrictingResult();
        }
    }
}
