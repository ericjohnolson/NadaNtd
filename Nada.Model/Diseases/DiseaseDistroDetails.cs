using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Diseases
{
    public class DiseaseDistroDetails : NadaListItem
    {
        public int Id { get; set; }
        public string AdminLevel { get; set; }
        public string TypeName { get; set; }
        public int TypeId { get; set; }
        public DateTime DateReported { get; set; }
     }
}
