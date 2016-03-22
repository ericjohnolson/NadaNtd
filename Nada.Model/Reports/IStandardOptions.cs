using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Diseases;
using Nada.Model.Intervention;

namespace Nada.Model.Reports
{
    public interface IStandardOptions
    {
    }

    /// <summary>
    /// Report options for the standard report: Progress towards Elimination
    /// </summary>
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

    /// <summary>
    /// Report options for the standard report: Persons Treated and Coverage Report
    /// </summary>
    public class PersonsTreatedCoverageReportOptions : IStandardOptions
    {
        public PersonsTreatedCoverageReportOptions()
        {
            Diseases = new List<Disease>();
            DrugPackages = new List<IntvType>();
            AvailableDiseases = new List<Disease>();
            AvailableDrugPackages = new List<IntvType>();
            Years = new List<int>();
        }
        public AdminLevelType DistrictType { get; set; }
        public List<Disease> Diseases { get; set; }
        public List<IntvType> DrugPackages { get; set; }
        public List<Disease> AvailableDiseases { get; set; }
        public List<IntvType> AvailableDrugPackages { get; set; }
        public List<int> Years { get; set; }
        public bool isReportTypeDisease { get; set; }
    }
}
