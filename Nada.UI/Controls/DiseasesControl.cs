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
using Nada.Model.Diseases;
using Nada.UI.Base;

namespace Nada.UI.Controls
{
    public partial class DiseasesControl : BaseControl
    {
        private List<Disease> diseases = null;
        private DiseaseRepository r = null;
        public DiseasesControl()
            : base()
        {
            InitializeComponent();
        }

        private void DiseasesControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                r = new DiseaseRepository();
                diseases = r.GetSelectedDiseases();
                diseaseBindingSource.DataSource = diseases;
            }
        }

        private void fieldLink1_OnClick()
        {

        }

        public List<Disease> GetSelected()
        {
            List<Disease> diseases = new List<Disease>();
            foreach (var p in lbDiseases.SelectedItems)
                diseases.Add(p as Disease);
            return diseases;
        }

        public void LoadItems(List<Disease> selected)
        {
            lbDiseases.ClearSelected();
            foreach (var p in diseases.Where(v => selected.Select(i => i.Id).Contains(v.Id)))
                lbDiseases.SelectedItems.Add(p);
        }
    }
}
