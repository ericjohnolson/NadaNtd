using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.UI.AppLogic;

namespace Nada.UI.View
{
    public partial class TranslationModal : Form
    {
        private List<Language> languages = null;
        private TranslatedValue model = null;
        public event Action<TranslatedValue> OnSave = (e) => { };

        public TranslationModal()
        {
            InitializeComponent();
        }

        public TranslationModal(List<Language> unusedLanguages)
        {
            InitializeComponent();
            languages = unusedLanguages;
        }

        private void TranslationModal_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                model = new TranslatedValue { IsoCode = "en-US" };
                languageBindingSource.DataSource = languages;
                bsTranslation.DataSource = model;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (cbLang.SelectedItem != null)
                model.Language = ((Language)cbLang.SelectedItem).Name;
            else
                model.Language = languages[0].Name;

            OnSave(model);
            this.Close();
        }

    }
}
