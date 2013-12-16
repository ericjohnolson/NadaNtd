using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Exports
{
    public class ExportContact : NadaClass
    {
        public string CmContactName { get; set; }
        public string CmContactPost { get; set; }
        public string CmContactTele { get; set; }
        public string CmContactEmail { get; set; }

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "CmContactName":
                        if (string.IsNullOrEmpty(CmContactName))
                            error = Translations.Required;
                        break;
                    default: error = "";
                        break;
                }
                return error;
            }
        }

        #endregion
    }
}
