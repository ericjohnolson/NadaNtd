using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Diseases;

namespace Nada.Model.Reports
{
    public interface IStandardOptions
    {
    }

    public class EliminationReportOptions : IStandardOptions
    {
        public EliminationReportOptions()
        {
            Diseases = new List<Disease>();
            IsPersons = true;
        }
        public AdminLevelType DistrictType { get; set; }
        public List<Disease> Diseases { get; set; }
        public bool IsPersons { get; set; }
    }
}
