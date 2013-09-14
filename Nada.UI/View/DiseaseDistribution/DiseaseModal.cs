using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;

namespace Nada.UI.View
{
    public partial class DiseaseModal : Form
    {
        private Disease disease = null;
        private List<Language> untranslatedLangs = null;
        public event Action OnSave = () => { };

        public DiseaseModal()
        {
            InitializeComponent();
        }

        public DiseaseModal(Disease d)
        {
            InitializeComponent();
            disease = d;
        }

        public DiseaseModal(int id)
        {
            InitializeComponent();
            DiseaseRepository repo = new DiseaseRepository();
            disease = repo.GetDiseaseById(id, Localizer.GetCultureName());
        }

        private void DiseaseModal_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                bsDisease.DataSource = disease;
                LoadLanguages();
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            bsDisease.EndEdit();
            DiseaseRepository r = new DiseaseRepository();
            if (disease.Id > 0)
                r.Update(disease, ApplicationData.Instance.GetUserId());
            else
                r.Insert(disease, ApplicationData.Instance.GetUserId());
            OnSave();
            this.Close();
        }

        private void lnkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TranslationModal modal = new TranslationModal(untranslatedLangs);
            modal.OnSave += translation_OnSave;
            modal.ShowDialog();
        }

        private void translation_OnSave(TranslatedValue value)
        {
            disease.TranslatedNames.Add(value);
            bsDisease.ResetBindings(false);
            LoadLanguages();
        }

        private void LoadLanguages()
        {
            untranslatedLangs = Localizer.SupportedLanguages.Where(l => !disease.TranslatedNames.Select(n => n.IsoCode).Contains(l.IsoCode)).ToList();
            if (untranslatedLangs == null || untranslatedLangs.Count == 0)
                lnkAdd.Visible = false;
        }
    }
}
