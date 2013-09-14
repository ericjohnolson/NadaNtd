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

namespace Nada.UI.Controls
{
    public partial class FundersControl : UserControl
    {
        private IntvRepository r = null;
        public FundersControl()
        {
            InitializeComponent();
        }

        private void FundersControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                r = new IntvRepository();
                partnerBindingSource.DataSource = r.GetPartners();
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

        public void LoadItems(List<Partner> partners)
        {
            foreach (var p in partners)
                lbPartners.SelectedItem = p;
        }
    }
}
