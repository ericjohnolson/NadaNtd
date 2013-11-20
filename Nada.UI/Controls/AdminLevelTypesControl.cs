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
using Nada.UI.AppLogic;

namespace Nada.UI.Controls
{
    public partial class AdminLevelTypesControl : UserControl
    {
        private List<AdminLevelType> types = null;
        private SettingsRepository r = null;
        public AdminLevelTypesControl()
        {
            InitializeComponent();
        }

        private void AdminLevelTypesControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                r = new SettingsRepository();
                RefreshList();
            }
        }

        private void fieldLink1_OnClick()
        {
            int max = types.Max(t => t.LevelNumber);
            AdminLevelType type = new AdminLevelType { LevelNumber = max + 1 };
            AdminLevelTypeAdd add = new AdminLevelTypeAdd(type);
            add.OnSave += () => { RefreshList(); };
            add.ShowDialog();
        }

        private void lvLevels_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            if (e.Column.AspectName == "EditText")
            {
                AdminLevelTypeAdd add = new AdminLevelTypeAdd((AdminLevelType)e.Model);
                add.OnSave += () => { RefreshList(); };
                add.ShowDialog();
            }
            else if (e.Column.AspectName == "DeleteText")
            {
                r.Delete((AdminLevelType)e.Model, ApplicationData.Instance.GetUserId());
                RefreshList();
            }
        }

        public void RefreshList()
        {
            types = r.GetAllAdminLevels();
            lvLevels.SetObjects(types);
        }

        public bool HasDistrict()
        {
            return (types.FirstOrDefault(t => t.IsDistrict) != null);
        }
    }
}
