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
        public RedistrictingResult DoSplit(RedistrictingOptions options)
        {
            return new RedistrictingResult();
        }
        public RedistrictingResult DoSplitCombine(RedistrictingOptions options)
        {
            return new RedistrictingResult();
        }
        public RedistrictingResult DoMerge(RedistrictingOptions options)
        {
            return new RedistrictingResult();
        }
    }
}
