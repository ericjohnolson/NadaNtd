using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.Model;
using Nada.Model.Repositories;
using Nada.UI.Base;
using Nada.Globalization;

namespace Nada.UI.View.Demography
{
    public partial class AdminLevelSelectAdd : BaseForm
    {
        public event Action<AdminLevel> OnSelect = (e) => { };
        private int levelType;

        public AdminLevelSelectAdd(int t)
        {
            InitializeComponent();
            levelType = t;
        }

        private void StepCategory_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                LoadAdminLevels();
                treeAvailable.HyperlinkStyle.Over.Font = this.Font;
                treeAvailable.HyperlinkStyle.Normal.Font = this.Font;
                treeAvailable.HyperlinkStyle.Visited.Font = this.Font;
            }
        }

        private void LoadAdminLevels()
        {
            SettingsRepository settings = new SettingsRepository();
            DemoRepository repo = new DemoRepository();
            var levels = settings.GetAllAdminLevels();
            var t = repo.GetAdminLevelTree(levels.OrderByDescending(l => l.LevelNumber).ToArray()[1].Id, 0, true, true, levelType);
            treeAvailable.CanExpandGetter = m => ((AdminLevel)m).Children.Count > 0;
            treeAvailable.ChildrenGetter = delegate(object m)
            {
                return ((AdminLevel)m).Children;
            };
            treeAvailable.SetObjects(t);

            foreach (var l in t)
            {
                treeAvailable.Expand(l);
                foreach (var l2 in l.Children)
                    treeAvailable.Expand(l2);
            }
        }

        private void lnkNew_ClickOverride()
        {
            var adminLevelAdd = new AdminLevelAdd();
            adminLevelAdd.OnSave += adminLevelAdd_OnSave;
            adminLevelAdd.ShowDialog();
        }

        void adminLevelAdd_OnSave()
        {
            LoadAdminLevels();
        }

        private void treeAvailable_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            OnSelect((AdminLevel)e.Model);
            this.Close();
        }
    }
}
