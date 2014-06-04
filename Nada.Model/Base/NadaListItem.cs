using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;

namespace Nada.Model.Base
{
    public class NadaListItem
    {
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public virtual string View
        {
            get
            {
                return Translations.ViewEdit;
            }
        }

        public virtual string Delete
        {
            get
            {
                return Translations.Delete;
            }
        }
    }
}
