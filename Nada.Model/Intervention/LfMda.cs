using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model.Intervention
{
    public class LfMda : IntvBase
    {
        public LfMda() : base()
        {
            Medicines = new List<Medicine>();
            Partners = new List<Partner>();
        }
        public DistributionMethod DistributionMethod { get; set; }
        public List<Medicine> Medicines { get; set; }
        public List<Partner> Partners { get; set; }
    }
}
