using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Repositories;
using Nada.Model;
using Nada.Model.Diseases;
using Nada.UI.AppLogic;
using Nada.UI.Base;

namespace Nada.UI.View
{
    public partial class DiseasePickerControl : BaseControl
    {
        private DiseaseRepository repo = null;
        private List<Disease> available = new List<Disease>();
        private List<Disease> selected = new List<Disease>();
        bool isStartUp = false;

        public DiseasePickerControl()
            : base()
        {
            InitializeComponent();
        }

        private void DiseasePickerControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                repo = new DiseaseRepository();
            }
        }

        public List<Disease> GetSelectedItems()
        {
            return selected;
        }

        public List<Disease> GetUnselectedItems()
        {
            return available;
        }
        
        public void LoadLists(bool startUp)
        {
            isStartUp = startUp;
            if (isStartUp)
            {
                available = repo.GetAvailableDiseases();
                selected = repo.GetSelectedDiseases();
                foreach (var item in selected)
                    available.RemoveAll(a => a.Id == item.Id);
            }
            else
            {
                available = repo.GetAvailableDiseases();
                selected = new List<Disease>();
            }
            lstAvailable.SetObjects(available.OrderBy(i => i.DisplayName).ToList());
            lstSelected.SetObjects(selected.OrderBy(i => i.DisplayName).ToList());
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (var item in available)
                selected.Add(item);
            available.Clear();
            ReloadLists();
        }

        private void treeAvailable_DoubleClick(object sender, EventArgs e)
        {
            SelectItems();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectItems();
        }

        private void SelectItems()
        {
            foreach (var item in lstAvailable.SelectedObjects.Cast<Disease>())
            {
                selected.Add(item);
                available.Remove(item);
            }
            ReloadLists();
        }

        private void btnDeselect_Click(object sender, EventArgs e)
        {
            DeselectItems();
        }

        private void treeSelected_DoubleClick(object sender, EventArgs e)
        {
            DeselectItems();
        }

        private void DeselectItems()
        {
            foreach (var item in lstSelected.SelectedObjects.Cast<Disease>())
            {
                available.Add(item);
                selected.Remove(item);
            }
            ReloadLists();
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            foreach (var item in selected)
                available.Add(item);
            selected.Clear();
            ReloadLists();
        }

        private void ReloadLists()
        {
            lstAvailable.ClearObjects();
            lstAvailable.SetObjects(available.OrderBy(i => i.DisplayName).ToList());
            lstSelected.ClearObjects();
            lstSelected.SetObjects(selected.OrderBy(i => i.DisplayName).ToList());
        }

        private void lnkAddDisease_ClickOverride()
        {
            DiseaseAdd form = new DiseaseAdd();
            form.OnSave += form_OnSave;
            form.ShowDialog();
        }

        void form_OnSave(Disease obj)
        {
            selected.Add(obj);
            ReloadLists();
        }

    }
}
