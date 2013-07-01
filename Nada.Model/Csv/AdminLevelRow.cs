using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LINQtoCSV;

namespace Nada.Model.Csv
{
    // http://stackoverflow.com/questions/356464/localization-of-displaynameattribute 
    // localize name w/ source code changing to allow localizedname attribute.
    public class AdminLevelRow
    {
        [CsvColumn(Name = "Full Name", FieldIndex = 1)]
        public string Name { get; set; }
        [CsvColumn(Name = "Is Urban?", FieldIndex = 2)]
        public string IsUrban { get; set; }
        [CsvColumn(Name = "WHO Latitude", FieldIndex = 3)]
        public string LatWho { get; set; }
        [CsvColumn(Name = "WHO Longitude", FieldIndex = 4)]
        public string LngWho { get; set; }
        [CsvColumn(Name = "Other Latitude", FieldIndex = 5)]
        public string Lat { get; set; }
        [CsvColumn(Name = "Other Longitude", FieldIndex = 6)]
        public string Lng { get; set; }
    }
}
