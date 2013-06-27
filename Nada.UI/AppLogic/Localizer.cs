using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using Nada.Model;
using Nada.Model.Repositories;
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
            SettingsRepository repo = new SettingsRepository();
            SupportedLanguages = repo.GetSupportedLanguages();
        }

        public static void SetCulture(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        public static string GetCultureName()
        {
            return Thread.CurrentThread.CurrentUICulture.Name;
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
