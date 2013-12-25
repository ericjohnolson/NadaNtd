using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;
using Nada.Model.Repositories;

namespace Nada.Model
{
    public class Member : NadaClass, IDataErrorInfo
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<MemberRole> SelectedRoles { get; set; }
        public virtual string View
        {
            get
            {
                return Translations.View;
            }
        }

        public virtual string Delete
        {
            get
            {
                return Translations.Delete;
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
                    case "Username":
                        if (string.IsNullOrEmpty(Username))
                            error = Translations.Required;
                        break;
                    case "Password":
                        if (string.IsNullOrEmpty(Password))
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
