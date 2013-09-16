using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Repositories;
using Nada.UI.Properties;

namespace Nada.UI.AppLogic
{
    public static class Localizer
    {
        private static ResourceManager rm = null;
        public static List<Language> SupportedLanguages { get; set; }
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

        public static void TranslateControl(Control parentCtrl)
        {
            foreach (Control c in parentCtrl.Controls)
            {
                if (c.Tag != null && !string.IsNullOrEmpty(c.Tag.ToString()) && keys.Contains(c.Tag.ToString()))
                    c.Text = rm.GetString(c.Tag.ToString());

                TranslateControl(c);
            }
        }

    }

    public class CultureState
    {
        public CultureInfo Result { get; set; }
    }
}
