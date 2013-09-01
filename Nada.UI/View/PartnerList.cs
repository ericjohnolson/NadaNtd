﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;

namespace Nada.UI.View
{
    public partial class PartnerList : Form
    {
        public event Action OnSave = () => { };

        IntvRepository repo = null;
        public PartnerList()
        {
            InitializeComponent();
        }

        private void DistributionMethodList_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                repo = new IntvRepository();
                lvDistros.SetObjects(repo.GetPartners());
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            PartnerAdd form = new PartnerAdd();
            form.OnSave += form_OnSave;
            form.ShowDialog();
        }

        private void lvDistros_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            if (e.Column.AspectName == "EditText")
            {
                PartnerAdd form = new PartnerAdd((Partner)e.Model);
                form.OnSave += form_OnSave;
                form.ShowDialog();
            }
            else if (e.Column.AspectName == "DeleteText")
            {
                repo.Delete((Partner)e.Model, ApplicationData.Instance.GetUserId());
                lvDistros.SetObjects(repo.GetMedicines());
                OnSave();
            }
        }

        void form_OnSave(Partner obj)
        {
            lvDistros.SetObjects(repo.GetPartners());
            OnSave();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
