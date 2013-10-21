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
        public static List<Language> SupportedLanguages { get; set; }

        public static void Initialize()
        {
            TranslationLookup.Initialize();
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
            return TranslationLookup.GetValue(key);
        }

        public static void TranslateControl(Control parentCtrl)
        {
            if (parentCtrl.Tag != null && !string.IsNullOrEmpty(parentCtrl.Tag.ToString()))
                parentCtrl.Text = TranslationLookup.GetValue(parentCtrl.Tag.ToString(), parentCtrl.Text);

            foreach (Control c in parentCtrl.Controls)
            {
                if (c.Tag != null && !string.IsNullOrEmpty(c.Tag.ToString()))
                    c.Text = TranslationLookup.GetValue(c.Tag.ToString(), c.Text);

                if (c is ComboBox && ((ComboBox)c).DataSource == null)
                {
                    List<string> keys = new List<string>();
                    foreach (var item in ((ComboBox)c).Items)
                        keys.Add(item.ToString());

                    ((ComboBox)c).Items.Clear();
                    foreach(var key in keys)
                        ((ComboBox)c).Items.Add(TranslationLookup.GetValue(key, key));
                }

                TranslateControl(c);
            }
        }
    }

    public class CultureState
    {
        public CultureInfo Result { get; set; }
    }
}
