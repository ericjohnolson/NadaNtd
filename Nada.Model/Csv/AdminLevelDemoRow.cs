using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LINQtoCSV;

namespace Nada.Model.Csv
{
    public class AdminLevelDemoRow
    {
        [CsvColumn(FieldIndex = 1)]
        public string ID { get; set; }
        [CsvColumn(FieldIndex = 2)]
        public string YearReporting { get; set; }
        [CsvColumn(FieldIndex = 3)]
        public string YearCensus { get; set; }
        [CsvColumn(FieldIndex = 4)]
        public string YearProjections { get; set; }
        [CsvColumn(FieldIndex = 5)]
        public string GrowthRate { get; set; }
        [CsvColumn(FieldIndex = 6)]
        public string FemalePercent { get; set; }
        [CsvColumn(FieldIndex = 7)]
        public string MalePercent { get; set; }
        [CsvColumn(FieldIndex = 8)]
        public string AdultsPercent { get; set; }
        [CsvColumn(FieldIndex = 9)]
        public string TotalPop { get; set; }
        [CsvColumn(FieldIndex = 10)]
        public string AdultPop { get; set; }
    }
}
