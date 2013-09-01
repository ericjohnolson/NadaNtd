using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public interface IHaveDynamicIndicators
    {
        List<Indicator> Indicators { get; set; }
    }
}
