﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;

namespace Nada.UI.View
{
    public partial class DiseaseList : BaseForm
    {
        public event Action OnSave = () => { };

        DiseaseRepository repo = null;
        public DiseaseList()
            : base()
        {
            InitializeComponent();
        }

        private void DistributionMethodList_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                repo = new DiseaseRepository();
                lvDiseases.SetObjects(repo.GetSelectedDiseases());
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            DiseaseAdd form = new DiseaseAdd();
            form.OnSave += form_OnSave;
            form.ShowDialog();
        }

        private void lvDistros_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            if (e.Column.AspectName == "EditText")
            {
                DiseaseAdd form = new DiseaseAdd((Disease)e.Model);
                form.OnSave += form_OnSave;
                form.ShowDialog();
            }
            else if (e.Column.AspectName == "DeleteText")
            {
                repo.Delete((Disease)e.Model, ApplicationData.Instance.GetUserId());
                lvDiseases.SetObjects(repo.GetSelectedDiseases());
                OnSave();
            }
        }

        void form_OnSave(Disease obj)
        {
            lvDiseases.SetObjects(repo.GetSelectedDiseases());
            OnSave();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
