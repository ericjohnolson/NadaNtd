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
        public DateTime DateReported { get; set; }
        public double TotalPopulation { get; set; }
        public double GrowthRate { get; set; }
        public bool CanViewEdit { get; set; }
        public bool CanDelete { get; set; }
        public override string View
        {
            get
            {
                if (CanViewEdit)
                    return Translations.ViewEdit;
                else
                    return Translations.View;
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
