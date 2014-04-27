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
    public partial class PartnerList : BaseForm
    {
        public event Action OnSave = () => { };

        List<Partner> partners = null;
        IntvRepository repo = null;
        public PartnerList()
            : base()
        {
            InitializeComponent();
        }

        private void DistributionMethodList_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                repo = new IntvRepository();
                partners = repo.GetPartners();
                lvDistros.SetObjects(partners);
            }
        }
        
        private void lvDistros_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            if (e.Column.AspectName == "EditText")
            {
                PartnerAdd form = new PartnerAdd(partners, (Partner)e.Model);
                form.OnSave += form_OnSave;
                form.ShowDialog();
            }
            else if (e.Column.AspectName == "DeleteText")
            {
                DeleteConfirm confirm = new DeleteConfirm();
                if (confirm.ShowDialog() == DialogResult.OK)
                {
                    repo.Delete((Partner)e.Model, ApplicationData.Instance.GetUserId());
                    lvDistros.SetObjects(repo.GetPartners());
                    OnSave();
                }
            }
        }

        void form_OnSave(Partner obj)
        {
            partners = repo.GetPartners();
            lvDistros.SetObjects(partners);
            OnSave();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fieldLink1_OnClick()
        {
            PartnerAdd form = new PartnerAdd(partners);
            form.OnSave += form_OnSave;
            form.ShowDialog();
        }


    }
}
