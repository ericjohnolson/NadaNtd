using System;
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
using Nada.UI.Base;

namespace Nada.UI.View
{
    public partial class MedicineList : BaseForm
    {
        public event Action OnSave = () => { };

        IntvRepository repo = null;
        public MedicineList()
            : base()
        {
            InitializeComponent();
        }

        private void DistributionMethodList_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                repo = new IntvRepository();
                lvDistros.SetObjects(repo.GetMedicines());
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            MedicineAdd form = new MedicineAdd();
            form.OnSave += form_OnSave;
            form.ShowDialog();
        }

        private void lvDistros_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            if (e.Column.AspectName == "EditText")
            {
                MedicineAdd form = new MedicineAdd((Medicine)e.Model);
                form.OnSave += form_OnSave;
                form.ShowDialog(); 
            }
            else if (e.Column.AspectName == "DeleteText")
            {
                repo.Delete((Medicine)e.Model, ApplicationData.Instance.GetUserId());
                lvDistros.SetObjects(repo.GetMedicines());
                OnSave();
            }
        }

        void form_OnSave(Medicine obj)
        {
            lvDistros.SetObjects(repo.GetMedicines());
            OnSave();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
