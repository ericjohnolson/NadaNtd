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
    public partial class AdminUnitChooser : BaseControl
    {
        private AdminLevel selected = null;
        private int levelNumber = 0;

        public AdminUnitChooser(int levelNo)
            : base()
        {
            InitializeComponent();
            levelNumber = levelNo;
            h3Required1.SetMaxWidth(300);
            Localizer.TranslateControl(this);
        }

        public void Select(AdminLevel obj, bool hideSelect)
        {
            h3Required1.Text = obj.Name;
            selected = obj;
            fieldLink1.Visible = hideSelect;
        }

        public void HideNewLink()
        {
            fieldLink1.Hide();
        }

        public void Select(int id)
        {
            DemoRepository repo = new DemoRepository();
            AdminLevel level = repo.GetAdminLevelById(id);
            Select(level, false);
        }
        public AdminLevel GetSelected()
        {
            return selected;
        }
        private void fieldLink1_OnClick()
        {
            var adminLevelAdd = new AdminLevelAdd(levelNumber);
            adminLevelAdd.OnSave += picker_OnSelect;
            adminLevelAdd.ShowDialog();
        }

        void picker_OnSelect(Model.AdminLevel obj)
        {
            h3Required1.Text = obj.Name;
            selected = obj;
        }
    }
}
