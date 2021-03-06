﻿using System;
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
            for (int i = 0; i < 12; i++)
            {
                months.Add(new MonthItem { Name = CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames[i], Id = i + 1 });
            }
            return months;
        }

        public static void UpdateResourceFile(Hashtable data, String path)
        {
            Hashtable resourceEntries = new Hashtable();

            //Add all changes...
            foreach (String key in data.Keys)
            {
                String value = data[key].ToString();
                if (value == null) value = "";
                resourceEntries.Add(key, value);
            }

            //Get existing resources
            ResXResourceReader reader = new ResXResourceReader(path);
            if (reader != null)
            {
                IDictionaryEnumerator id = reader.GetEnumerator();
                foreach (DictionaryEntry d in reader)
                {
                    if (!resourceEntries.ContainsKey(d.Key.ToString()))
                    {
                        if (d.Value == null)
                            resourceEntries.Add(d.Key.ToString(), "");
                        else
                            resourceEntries.Add(d.Key.ToString(), d.Value.ToString());
                    }
                }
                reader.Close();
            }

            //Write the combined resource file
            ResXResourceWriter resourceWriter = new ResXResourceWriter(path);

            foreach (String key in resourceEntries.Keys)
            {
                resourceWriter.AddResource(key, resourceEntries[key]);
            }
            resourceWriter.Generate();
            resourceWriter.Close();

        }
    }

    public static class TranslationLookup
    {
        private static ResourceManager rm = null;
        private static List<string> keys = null;
        private static CultureInfo initializedCulture = null;

        public static void Initialize()
        {
            rm = new ResourceManager("Nada.Globalization.Translations", typeof(GlobalizationUtil).Assembly);
            keys = new List<string>();

            initializedCulture = CultureInfo.CurrentCulture;
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
            if (key != null && rm != null && !string.IsNullOrEmpty(key.ToString()) && keys.Contains(key))
                return rm.GetString(key, initializedCulture);
            return def;
        }

    }

    public class TranslationLookupInstance
    {
        private static ResourceManager rm = null;
        private static List<string> keys = null;
        private static CultureInfo initializedCulture = null;

        public TranslationLookupInstance(CultureInfo culture)
        {
            rm = new ResourceManager("Nada.Globalization.Translations", typeof(GlobalizationUtil).Assembly);
            keys = new List<string>();

            initializedCulture = culture;
            ResourceSet set = rm.GetResourceSet(culture, true, true);
            foreach (DictionaryEntry o in set)
            {
                keys.Add((string)o.Key);
            }
        }

        public string GetValue(string key)
        {
            return GetValue(key, Translations.NoTranslationFound);
        }

        public string GetValue(string key, string def)
        {
            if (key != null && rm != null && !string.IsNullOrEmpty(key.ToString()) && keys.Contains(key))
                return rm.GetString(key, initializedCulture);

            return def;
        }

        public List<string> Keys { get { return keys; } }

    }
}
