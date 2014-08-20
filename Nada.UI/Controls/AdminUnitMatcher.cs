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

namespace Nada.UI.View
{
    public partial class AdminUnitMatcher : BaseControl
    {
        private AdminLevel existing = null;
        private int levelNumber = 0;
        public List<TaskForceAdminUnit> taskForceUnits = null;

        public AdminUnitMatcher()
        {
            InitializeComponent();
        }

        public AdminUnitMatcher(AdminLevel e, List<TaskForceAdminUnit> u)
            : base()
        {
            InitializeComponent();
            BindData(e, u);
        }

        public void BindData(AdminLevel e, List<TaskForceAdminUnit> u)
        {
            Localizer.TranslateControl(this);
            existing = e;
            taskForceUnits = u;
            h3Required1.SetMaxWidth(300);
            DemoRepository demo = new DemoRepository();
            var parentNames = demo.GetAdminLevelParentNames(existing.Id);
            if (parentNames.Count == 0)
                h3Required1.Text = e.Name;
            else
                h3Required1.Text = "";
            foreach (var pname in parentNames)
                h3Required1.Text += pname.Name + " > ";

            if (h3Required1.Text.EndsWith(" > "))
                h3Required1.Text = h3Required1.Text.Substring(0, h3Required1.Text.LastIndexOf(" > "));

            taskForceUnits.Insert(0, new TaskForceAdminUnit { Id = -1, Name = "" });
            bindingSource1.DataSource = taskForceUnits;

            if (taskForceUnits.Count > 1)
                cbUnits.DropDownWidth = BaseForm.GetDropdownWidth(taskForceUnits.Select(a => a.Name));
        }

        public TaskForceAdminUnit GetSelected()
        {
            return (TaskForceAdminUnit)cbUnits.SelectedItem;
        }

        public AdminLevel GetAdminUnit()
        {
            return existing;
        }

        public bool IsValid(List<int> selectedIds)
        {
            if (cbUnits.SelectedItem != null && ((TaskForceAdminUnit)cbUnits.SelectedItem).Id > 0)
            {
                selectedIds.Add(((TaskForceAdminUnit)cbUnits.SelectedItem).Id);
                return true;
            }
            return false;
        }

    }
}
