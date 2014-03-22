using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.Globalization;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.Model;
using Nada.UI.Base;
using Nada.Model.Imports;
using Nada.UI.View.Wizard.DistrictSplitting;

namespace Nada.UI.View.Wizard
{
    public partial class SplittingSource : BaseControl, IWizardStep
    {
        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();
        private SplittingOptions options = null;
        public Action OnFinish { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return false; } }
        public bool EnableNext { get { return false; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.SplittingSelectSource; } }

        public SplittingSource(SplittingOptions o)
            : base()
        {
            options = o;
            InitializeComponent();
        }
        
        public void DoPrev()
        {
        }
        public void DoNext()
        {
        }
        public void DoFinish()
        {
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
            var levels = settings.GetAllAdminLevels();
            var t = repo.GetAdminLevelTree(levels.OrderByDescending(l => l.LevelNumber).ToArray()[1].Id, 0, true, true, -1);
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
            options.Source = (AdminLevel)e.Model;
            options.Source.CurrentDemography = repo.GetRecentDemography(options.Source.Id);
            options.SplitChildren = repo.GetAdminLevelChildren(options.Source.Id);
            OnSwitchStep(new SplittingIntoNumber(options));
        }
    }
}
