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
    public partial class AdminUnitDelete : BaseForm
    {
        public event Action<AdminLevel> OnSave = (a) => { };
        private AdminLevel model = new AdminLevel();
        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();
        private DemoRepository demo = new DemoRepository();

        public AdminUnitDelete()
            : base()
        {
            InitializeComponent();
        }


        private void AdminLevelAdd_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                LoadAdminUnits();
            }
        }

        private void LoadAdminUnits()
        {
            var levels = settings.GetAllAdminLevels();
            var units = new List<AdminLevel>();
            var t = demo.GetAdminLevelTree(levels.OrderByDescending(l => l.LevelNumber).First().Id, 0, true, true, -1, false, Translations.Delete, units);

            foreach (var u in units)
            {
                if (u.Children.Count > 0)
                    u.ViewText = "";
            }

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

        private void treeAdminUnits_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            int userId = ApplicationData.Instance.GetUserId();

            DeleteConfirm confirm = new DeleteConfirm();
            if (confirm.ShowDialog() == DialogResult.OK)
            {
                AdminLevel al = (AdminLevel)e.Model;
                AdminLevelType alt = settings.GetAdminLevelTypeByLevel(al.LevelNumber);
                foreach (var d in demo.GetAdminLevelDemography(al.Id))
                {
                    demo.Delete(d, userId);
                    if (alt.IsAggregatingLevel)
                        demo.AggregateUp(alt, d.DateReported, userId, null, null);
                }
                demo.Delete(al, userId);
                LoadAdminUnits();
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            OnSave(null);
            this.Close();
        }
    }
}
