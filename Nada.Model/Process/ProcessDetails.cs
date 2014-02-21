using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Process
{
    public class ProcessDetails : NadaListItem
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string AdminLevel { get; set; }
        public string TypeName { get; set; }
        public string CategoryName { get; set; }
        public DateTime DateReported { get; set; }
    }
}
