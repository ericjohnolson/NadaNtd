using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Repositories;
using Nada.UI.Base;
using Nada.UI.View.Demography;
using Nada.UI.AppLogic;
using Nada.Model.Process;
using Nada.UI.ViewModel;

namespace Nada.UI.View
{
    public partial class SaeMatcher : BaseControl
    {
        private ProcessBase sae = null;
        public List<AdminLevel> destinations = null;
        public AdminLevel source = null;

        public SaeMatcher()
        {
            InitializeComponent();
        }

        public SaeMatcher(ProcessBase e, List<AdminLevel> u, AdminLevel s)
            : base()
        {
            InitializeComponent();
            BindData(e, u, s);
        }

        public void BindData(ProcessBase e, List<AdminLevel> u, AdminLevel s)
        {
            source = s;
            Localizer.TranslateControl(this);
            sae = e;
            destinations = u;
            var idInd = e.IndicatorValues.FirstOrDefault(i => i.Indicator.DisplayName == "SaeId");
            lnkName.Text = e.DateReported.ToShortDateString();
            if (idInd != null && !string.IsNullOrEmpty(idInd.DynamicValue))
                lnkName.Text = idInd.DynamicValue + " - " + lnkName.Text;

            destinations.Insert(0, new AdminLevel { Id = -1, Name = "" });
            bindingSource1.DataSource = destinations;

            if (destinations.Count > 0)
                cbUnits.DropDownWidth = BaseForm.GetDropdownWidth(destinations.Select(a => a.Name));
        }

        public ProcessBase GetSae()
        {
            if (IsValid())
            {
                var selected = (AdminLevel)cbUnits.SelectedItem;
                if (sae.AdminLevelId == selected.Id)
                    return null;
                sae.AdminLevelId = selected.Id;
                return sae;
            }
            else return null;
        }

        public bool IsValid()
        {
            if(cbUnits.SelectedItem != null && ((AdminLevel)cbUnits.SelectedItem).Id > 0)
                return true;
            return false;
        }

        private void lnkName_ClickOverride()
        {
            var v = new DataEntryEdit(new ProcessBaseVm(source, sae, null));
            ViewForm form = new ViewForm(v);
            v.OnClose = () =>
            {
                form.Close();
            };
            form.Show();
        }
    }
}
