﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.UI.ViewModel
{
    public class DemoUpdateViewModel : NadaClass
    {
        public Nullable<double> GrowthRate { get; set; }
        public Nullable<int> Year { get; set; }

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {

                    case "GrowthRate":
                        if (!GrowthRate.HasValue)
                            error = Translations.Required;
                        break;
                    case "Year":
                        if (!Year.HasValue)
                            error = Translations.Required;
                        else if (Year.Value > 2100 || Year.Value < 1900)
                            error = Translations.ValidYear;
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
