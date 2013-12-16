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
    public partial class DashboardView : BaseControl
    {
        public Action<UserControl> LoadView = (i) => { };
        public Action<AdminLevel> LoadDashForAdminLevel = (i) => { };
        public Action<string> StatusChanged { get; set; }
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
            foreach (DropDownItem item in c1SplitButton1.Items)
            {
                if(item.Tag != null)
                    item.Text = Localizer.GetValue(item.Tag.ToString());
            }
        }

        #region Menu
        private void treeView_LoadView(IView v)
        {
            v.OnClose = () => { LoadDashForAdminLevel(null); };
            LoadView((UserControl)v);
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
                t_OnSelect(preloadedLevel);
            else if (t.Count > 0)
                t_OnSelect(t.FirstOrDefault());

            var root = t.FirstOrDefault();
            treeListView1.Expand(root);
        }

        private void treeListView1_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            t_OnSelect((AdminLevel)e.Model);
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

        void t_OnSelect(AdminLevel obj)
        {
            preloadedLevel = obj;
            AdminLevelType adminLevelType = null;
            if (adminLevelTypes.ContainsKey(obj.LevelNumber + 1))
                adminLevelType = adminLevelTypes[obj.LevelNumber + 1];

            var view = new DashboardTabs((AdminLevel)obj, adminLevelType);
            view.StatusChanged = (s) => { StatusChanged(s); };
            view.ReloadView = (a) => { LoadDashForAdminLevel(a); };
            view.LoadView = (v) => { LoadView(v); };
            view.OnSelect += t_OnSelect;
            divisionView = view;

            divisionView.Dock = DockStyle.Fill;
            c1SplitterPanel2.Controls.Clear();
            c1SplitterPanel2.Controls.Add(divisionView);
        }

        private void about_Click(object sender, EventArgs e)
        {
            var about = new About();
            about.ReloadTree = () => { LoadTree();  };
            about.ShowDialog();
        }

        private void reports_Click(object sender, EventArgs e)
        {
            ReportsDashboard view = new ReportsDashboard();
            treeView_LoadView(view);
        }

        private void c1SplitButton1_DropDownItemClicked(object sender, C1.Win.C1Input.DropDownItemClickedEventArgs e)
        {
            IImporter importer = null;
            if (e.ClickedItem.Tag.ToString() == "LfSentinelImport")
            {
                SurveyRepository repo = new SurveyRepository();
                SurveyType type = repo.GetSurveyType((int)StaticSurveyType.LfPrevalence);
                importer = new LfSentinelImporter(type);
            }
            else
            {
                IntvRepository repo = new IntvRepository();
                IntvType type = repo.GetIntvType((int)StaticIntvType.IvmAlbMda);
                importer = new LfMdaImporter(type);
            }
            WizardForm wiz = new WizardForm(new ImportStepType(importer), importer.ImportName);
            wiz.OnFinish = import_OnSuccess;
            wiz.ShowDialog();
        }

        private void import_OnSuccess()
        {
            LoadDashForAdminLevel(preloadedLevel);
        }

        private void AddAdminLevel_ClickOverride()
        {
            var adminLevelAdd = new AdminLevelAdd();
            adminLevelAdd.OnSave += adminLevelAdd_OnSave;
            adminLevelAdd.ShowDialog();
        }

        void adminLevelAdd_OnSave()
        {
            LoadTree();
        }
        #endregion


    }
}
