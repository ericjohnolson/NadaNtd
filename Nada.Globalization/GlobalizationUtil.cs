using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;

namespace Nada.Globalization
{
    public class MonthItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class GlobalizationUtil
    {
        public static List<MonthItem> GetAllMonths()
        {
            var months = new List<MonthItem>();
            for (int i = 0; i < 12; i++) {
                months.Add(new MonthItem { Name = CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames[i], Id = i + 1 });
            }
            return months;
        }
    }

    public static class TranslationLookup
    {
        private static ResourceManager rm = null;
        private static List<string> keys = null;

        public static void Initialize()
        {
            rm = new ResourceManager("Nada.Globalization.Translations", typeof(GlobalizationUtil).Assembly);
            keys = new List<string>();

            ResourceSet set = rm.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            foreach (DictionaryEntry o in set)
            {
                keys.Add((string)o.Key);
            }
        }

        public static string GetValue(string key)
        {
            return GetValue(key, Translations.NoTranslationFound);
        }

        public static string GetValue(string key, string def)
        {
            if (key != null && rm !=null && !string.IsNullOrEmpty(key.ToString()) && keys.Contains(key))
                return rm.GetString(key);
            return def;
        }
    }

}
