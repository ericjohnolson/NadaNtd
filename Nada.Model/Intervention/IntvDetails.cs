using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Intervention
{
    public class IntvDetails : NadaListItem
    {
        public int Id { get; set; }
        public string AdminLevel { get; set; }
        public string TypeName { get; set; }
        public int TypeId { get; set; }
        public DateTime DateReported { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
     }
}
