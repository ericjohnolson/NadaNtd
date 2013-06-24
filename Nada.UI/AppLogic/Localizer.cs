using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using Nada.UI.Model;
using Nada.UI.Properties;

namespace Nada.UI.AppLogic
{
    public static class Localizer
    {
        private static ResourceManager rm = null;
        public static List<Language> SupportedLanguages { get; set; }

        public static void Initialize()
        {
            rm = new ResourceManager("Nada.UI.Translations", typeof(Localizer).Assembly);
            SupportedLanguages = new List<Language>
            {
                new Language { Name = "English", IsoCode = "en-US" },
                new Language { Name = "français", IsoCode = "fr-FR" }
            };
        }

        public static void SetCulture(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        public static string GetValue(string key)
        {
            return rm.GetString(key);
        }

    }

    public class CultureState
    {
        public CultureInfo Result { get; set; }
    }
}
