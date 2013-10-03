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
    public class GlobalizationUtil
    {
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
            return GetValue(key, "No translation found");
        }

        public static string GetValue(string key, string def)
        {
            if (key != null && rm !=null && !string.IsNullOrEmpty(key.ToString()) && keys.Contains(key))
                return rm.GetString(key);
            return def;
        }
    }

}
