using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public class Disease
    {
        public Disease()
        {
            TranslationId = Guid.NewGuid().ToString();
            TranslatedNames = new List<TranslatedValue>();
            EditText = "Edit";
        }
        public string DisplayName { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
        public string TranslationId { get; set; }
        public List<TranslatedValue> TranslatedNames { get; set; }
        public string EditText { get; set; }
        public bool IsEnabled { get; set; }
        public string IsEnabledText
        {
            get
            {
                return IsEnabled ? "Yes" : "No";
            }
        }
    }
}
