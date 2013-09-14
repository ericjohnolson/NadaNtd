using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public class AdminLevelDemography
    {
        public int Id { get; set; }
        public int AdminLevelId { get; set; }
        public int YearReporting { get; set; }
        public int YearCensus { get; set; }
        public int YearProjections { get; set; }
        public double GrowthRate { get; set; }
        public double FemalePercent { get; set; }
        public double MalePercent { get; set; }
        public double AdultsPercent { get; set; }
        public int TotalPopulation { get; set; }
        public int AdultPopulation { get; set; }
        public string EditText { get { return "Edit"; } }
    }
}
