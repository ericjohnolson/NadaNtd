using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BrightIdeasSoftware;
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
            List<string> langz = ConfigurationManager.AppSettings["SupportedLanguages"].Split('|').ToList();
            SupportedLanguages = langz.Select(l => new Language
            {
                IsoCode = l.Split(';')[0],
                Name = l.Split(';')[1]
            }).ToList();
        }

        public static void SetCulture(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            TranslationLookup.Initialize();
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
                    c.Text = TranslationLookup.GetValue(c.Tag.ToString(), c.Tag.ToString());

                if (c is ComboBox && ((ComboBox)c).DataSource == null)
                {
                    List<string> keys = new List<string>();
                    foreach (var item in ((ComboBox)c).Items)
                        keys.Add(item.ToString());

                    ((ComboBox)c).Items.Clear();
                    foreach (var key in keys)
                        ((ComboBox)c).Items.Add(TranslationLookup.GetValue(key, key));
                }

                if (c is ObjectListView)
                {
                    foreach (var col in ((ObjectListView)c).AllColumns)
                    {
                        if (col.Tag != null && !string.IsNullOrEmpty(col.Tag.ToString()))
                            col.Text = TranslationLookup.GetValue(col.Tag.ToString(), col.Tag.ToString());
                    }
                }
                if (c is TabControl)
                {
                    foreach (TabPage tab in ((TabControl)c).TabPages)
                    {
                        if (tab.Tag != null && !string.IsNullOrEmpty(tab.Tag.ToString()))
                            tab.Text = TranslationLookup.GetValue(tab.Tag.ToString(), tab.Tag.ToString());
                    }
                }
                if (c is MenuStrip)
                {
                    foreach (ToolStripMenuItem item in ((MenuStrip)c).Items)
                    {
                        if (item.Tag != null && !string.IsNullOrEmpty(item.Tag.ToString()))
                            item.Text = TranslationLookup.GetValue(item.Tag.ToString(), item.Tag.ToString());
                        foreach (var child in item.DropDownItems)
                            if (child is ToolStripMenuItem && ((ToolStripMenuItem)child).Tag != null && !string.IsNullOrEmpty(((ToolStripMenuItem)child).Tag.ToString()))
                                (child as ToolStripMenuItem).Text = TranslationLookup.GetValue((child as ToolStripMenuItem).Tag.ToString(), (child as ToolStripMenuItem).Tag.ToString());
                    }
                }
                if (c is DataGridView)
                {
                    var grid = (DataGridView)c;
                    foreach (DataGridViewColumn col in grid.Columns)
                    {
                        if (col.HeaderText != null && !string.IsNullOrEmpty(col.HeaderText.ToString()))
                            col.HeaderText = TranslationLookup.GetValue(col.HeaderText.ToString(), col.HeaderText.ToString());
                    }
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
