using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;

namespace Nada.UI.View
{
    public partial class AdminLevelAdd : BaseForm
    {
        public event Action<AdminLevel> OnSave = (a) => { };
        private AdminLevel model = new AdminLevel();
        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();
        private AdminLevelType parentType;
        private int childLevel = 0;

        public AdminLevelAdd()
            : base()
        {
            InitializeComponent();
        }

        public AdminLevelAdd(AdminLevel m)
            : base()
        {
            model = m;
            InitializeComponent();
        }

        public AdminLevelAdd(int levelNumber)
            : base()
        {
            childLevel = levelNumber;
            InitializeComponent();
        }

        private void AdminLevelAdd_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                if (model.Id > 0)
                    adminUnitAdd1.LoadUnit(model);
                else
                    adminUnitAdd1.LoadUnit(childLevel);
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            var model = adminUnitAdd1.GetModel();
            if (model.Id == 0)
                return;
            OnSave(model);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
