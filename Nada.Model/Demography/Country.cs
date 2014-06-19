using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model
{
    public class Country : NadaClass
    {
        public Country()
        {
        }
        public string Name { get; set; }
        private string _taskForceName = "";
        public string TaskForceName 
        {
            get
            {
                return _taskForceName;
            }
            set
            {
                if (value != null)
                    _taskForceName = value;
            }
        }
        
        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
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
