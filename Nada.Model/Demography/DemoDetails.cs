using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Demography
{
    public class DemoDetails : NadaListItem
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int TotalPopulation { get; set; }
        public double GrowthRate { get; set; }
        public bool CanView { get; set; }
        public bool CanDelete { get; set; }
        public override string View
        {
            get
            {
                return CanView ? Translations.View : "";
            }
        }

        public override string Delete
        {
            get
            {
                return CanDelete? Translations.Delete : "";
            }
        }
     }
}
