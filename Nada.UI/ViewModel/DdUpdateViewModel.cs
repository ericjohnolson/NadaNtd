using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;
using Nada.Model.Diseases;

namespace Nada.UI.ViewModel
{
    public class DdUpdateViewModel : NadaClass
    {
        public Nullable<double> GrowthRate { get; set; }
        public DateTime DateReported { get; set; }
        public List<Disease> Diseases { get; set; }
        public int DiseaseStepNumber { get; set; }

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


                    default: error = "";
                        break;

                }
                return error;
            }
        }
        #endregion
    }
}
