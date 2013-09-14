using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public class Country
    {
        public Country()
        {
            Children = new List<AdminLevel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string IsoCode { get; set; }
        public int ParentId { get { return 0; } set { }}
        public List<AdminLevel> Children { get; set; }
        public bool IsCountry { get { return true; } }
        public string ViewText { get { return "View"; } }
    }
}
