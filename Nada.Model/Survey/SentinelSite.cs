using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Survey
{
    public class SentinelSite : NadaClass, IDataErrorInfo
    {
        public List<AdminLevel> AdminLevels { get; set; }
        public string SiteName { get; set; }
        public Nullable<double> Lat { get; set; }
        public Nullable<double> Lng { get; set; }
        public string Notes { get; set; }
        public string SelectText { get { return "Select"; } }

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "SiteName":
                        if (String.IsNullOrEmpty(SiteName))
                            error = Translations.Required;
                        break;
                    case "Lat":
                        if (Lat.HasValue && (Lat.Value > 90 || Lat.Value < -90))
                            error = Translations.ValidLatitude;
                        break;
                    case "Lng":
                        if (Lng.HasValue && (Lng.Value > 180 || Lng.Value < -180))
                            error = Translations.ValidLongitude;
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
