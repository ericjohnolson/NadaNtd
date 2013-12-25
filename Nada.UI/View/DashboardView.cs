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
using Nada.UI.AppLogic;
using Nada.UI.View.Reports;
using C1.Win.C1Input;
using Nada.Globalization;
using Nada.Model.Survey;
using Nada.Model.Intervention;
using Nada.UI.View.Wizard;
using Nada.UI.Base;

namespace Nada.UI.View.Demography
{
    public partial class DashboardView : BaseControl, IView
    {
        public Action OnClose { get; set; }
        public string Title { get { return ""; } }
        public void SetFocus() { }
        public Action<string> StatusChanged { get; set; }
        public Action<IView> LoadView = (i) => { };
        public Action<AdminLevel> LoadDashForAdminLevel = (i) => { };
        private UserControl divisionView = null;
        private Dictionary<int, AdminLevelType> adminLevelTypes = null;
        private AdminLevel preloadedLevel = null;

        public DashboardView()
            : base()
        {
            InitializeComponent();
        }

        public DashboardView(AdminLevel a)
            : base()
        {
            InitializeComponent();
            preloadedLevel = a;
        }

        private void DemographyView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                Translate();
                treeListView1.HyperlinkStyle.Normal.ForeColor = Color.Black;
                treeListView1.HyperlinkStyle.Over.ForeColor = Color.DimGray;

                LoadAdminLevelTypes();
                LoadTree();
            }
        }

        private void Translate()
        {
            //foreach (DropDownItem item in c1SplitButton1.Items)
            //{
            //    if (item.Tag != null)
            //        item.Text = Localizer.GetValue(item.Tag.ToString());
            //}
        }

        #region Menu
        private void treeView_LoadView(IView v)
        {
            v.OnClose = () => { LoadDashForAdminLevel(null); };
            LoadView(v);
        }

        public void LoadTree()
        {
            treeListView1.ClearObjects();
            DemoRepository r = new DemoRepository();
            var t = r.GetAdminLevelTree();
            treeListView1.CanExpandGetter = model => ((AdminLevel)model).
                                                          Children.Count > 0;
            treeListView1.ChildrenGetter = delegate(object model)
            {
                return ((AdminLevel)model).
                        Children;
            };
            treeListView1.SetObjects(t);

            if (preloadedLevel != null)
                LoadDashboard(preloadedLevel);
            else if (t.Count > 0)
                LoadDashboard(t.FirstOrDefault());

            var root = t.FirstOrDefault();
            treeListView1.Expand(root);
        }

        private void treeListView1_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            LoadDashboard((AdminLevel)e.Model);
            treeListView1.Expand(e.Model);
        }

        private void treeListView1_DoubleClick(object sender, EventArgs e)
        {
            //t_OnSelect((AdminLevel)e.Model);

        }
        private void LoadAdminLevelTypes()
        {
            adminLevelTypes = new Dictionary<int, AdminLevelType>();
            SettingsRepository r = new SettingsRepository();
            List<AdminLevelType> types = r.GetAllAdminLevels();
            foreach (var t in types)
                adminLevelTypes.Add(t.LevelNumber, t);
        }

        void LoadDashboard(AdminLevel obj)
        {
            preloadedLevel = obj;
            AdminLevelType adminLevelType = null;
            if (adminLevelTypes.ContainsKey(obj.LevelNumber + 1))
                adminLevelType = adminLevelTypes[obj.LevelNumber + 1];

            var view = new DashboardTabs((AdminLevel)obj, adminLevelType);
            view.StatusChanged = (s) => { StatusChanged(s); };
            view.ReloadView = (a) => { LoadDashForAdminLevel(a); };
            view.LoadView = (v) => { LoadView(v); };
            view.LoadForm = (v) => { LoadForm(v); };
            view.OnSelect += LoadDashboard;
            divisionView = view;

            divisionView.Dock = DockStyle.Fill;
            c1SplitterPanel2.Controls.Clear();
            c1SplitterPanel2.Controls.Add(divisionView);
        }

        private void LoadForm(IView v)
        {
            ViewForm form = new ViewForm(v);
            v.OnClose = () =>
            {
                LoadDashboard(preloadedLevel);
                form.Close();
            };
            form.Show();
        }

        private void import_OnSuccess()
        {
            LoadDashForAdminLevel(preloadedLevel);
        }

        void adminLevelAdd_OnSave()
        {
            LoadTree();
        }
        #endregion




    }
}
