using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nada.Model.Base
{
    [Serializable]
    public class NadaClass : IDataErrorInfo
    {
        private readonly List<string> propertyNames = new List<string>();
        public NadaClass()
        {
            UpdatedBy = "Not Saved";
            propertyNames = GetPropertyNames();
        }

        public int Id { get; set; }
        public int UpdatedById { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }


        private List<string> GetPropertyNames()
        {
            List<string> props = new List<string>();
            PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in properties)
            {
                if (!p.CanWrite || !p.CanRead)
                {
                    continue;
                }
                props.Add(p.Name);
            }
            return props;
        }

        public Boolean IsValid()
        {
            Boolean isValid = true;
            foreach (string prop in propertyNames)
            {
                if (!string.IsNullOrEmpty(this[prop]))
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        public string GetAllErrors()
        {
            string allErrors = string.Empty;
            foreach (string prop in propertyNames)
            {
                if (!string.IsNullOrEmpty(this[prop]))
                {
                    allErrors += this[prop] + Environment.NewLine;
                }
            }
            return allErrors;
        }

        protected string _lastError = "";

        public string Error
        {
            get { return _lastError; }
        }

        public virtual string this[string columnName]
        {
            get { return _lastError; }
        }
    }
}
