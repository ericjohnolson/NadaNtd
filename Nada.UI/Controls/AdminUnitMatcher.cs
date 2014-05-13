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
            existing = e;
            taskForceUnits = u;
            h3Required1.SetMaxWidth(300);
            DemoRepository demo = new DemoRepository();
            var parentNames = demo.GetAdminLevelParentNames(existing.Id);
            foreach (var pname in parentNames)
                h3Required1.Text += pname.Name + " > ";
            h3Required1.Text += existing.Name;
            bindingSource1.DataSource = taskForceUnits;
            Localizer.TranslateControl(this);
        }

        public AdminLevel GetSelected()
        {
            TaskForceAdminUnit selected = (TaskForceAdminUnit)cbUnits.SelectedItem;
            existing.TaskForceId = selected.Id;
            existing.TaskForceName = selected.Name;
            return existing;
        }

        public bool IsValid()
        {
            if(cbUnits.SelectedItem != null)
                return true;
            return false;
        }

    }
}
