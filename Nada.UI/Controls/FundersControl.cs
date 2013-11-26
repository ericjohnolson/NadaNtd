using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.View;
using Nada.Model.Repositories;
using Nada.Model;
using Nada.UI.Base;

namespace Nada.UI.Controls
{
    public partial class FundersControl : BaseControl
    {
        private List<Partner> funders = null;
        private IntvRepository r = null;
        public FundersControl()
            : base()
        {
            InitializeComponent();
        }

        private void FundersControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                r = new IntvRepository();
                funders = r.GetPartners();
                partnerBindingSource.DataSource = funders;
            }
        }

        private void fieldLink1_OnClick()
        {
            PartnerList list = new PartnerList();
            list.OnSave += () => { partnerBindingSource.DataSource = r.GetPartners(); };
            list.ShowDialog();
        }

        public List<Partner> GetSelected()
        {
            List<Partner> partners = new List<Partner>();
            foreach (var p in lbPartners.SelectedItems)
                partners.Add(p as Partner);
            return partners;
        }

        public void LoadItems(List<Partner> selected)
        {
            lbPartners.ClearSelected();
            foreach (var p in funders.Where(v => selected.Select(i => i.Id).Contains(v.Id)))
                lbPartners.SelectedItems.Add(p);
        }
    }
}
