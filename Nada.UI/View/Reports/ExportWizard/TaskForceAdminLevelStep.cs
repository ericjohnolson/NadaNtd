using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.Model.Reports;
using Nada.Globalization;
using Nada.Model;
using Nada.UI.View.Wizard;
using System.Threading;
using Nada.UI.Base;
using Nada.Model.Repositories;
using Nada.Model.Exports;

namespace Nada.UI.View.Reports
{
    public partial class TaskForceAdminLevelStep : BaseControl, IWizardStep
    {
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return title; } }
        private DemoRepository demo = new DemoRepository();
        private string title = "";
        private List<TaskForceAdminUnit> units;
        private List<AdminLevelType> types;
        private int typeIndex = 0;
        private List<AdminUnitMatcher> matchers = new List<AdminUnitMatcher>();

        public TaskForceAdminLevelStep()
            : base()
        {
            InitializeComponent();
        }

        public TaskForceAdminLevelStep(List<AdminLevelType> t, int i, List<TaskForceAdminUnit> u)
            : base()
        {
            InitializeComponent();
            types = t;
            typeIndex = i;
            units = u;
            title = string.Format(Translations.RtiMatchLevel, types[typeIndex].DisplayName);
        }

        private void TaskForceAdminLevelStep_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);

                DemoRepository demo = new DemoRepository();
                var country = demo.GetCountry();
                var adminLevels = demo.GetAdminLevelByLevel(types[typeIndex].LevelNumber);

                foreach (var u in adminLevels)
                {
                    List<TaskForceAdminUnit> available = new List<TaskForceAdminUnit>();
                    if (u.ParentId == country.Id)
                        available = units.Where(x => x.LevelIndex == 0).ToList();
                    else
                        available = units.Where(x => x.LevelIndex == typeIndex && x.Parent.NadaId == u.ParentId).ToList();

                    // has value
                    if (!string.IsNullOrEmpty(u.TaskForceName))
                    {
                        var existing = available.FirstOrDefault(f => f.Id == u.TaskForceId);
                        existing.NadaId = u.Id;
                        continue;
                    }

                    var match = available.FirstOrDefault(a => a.Name == u.Name);
                    if (match != null)
                    {
                        SaveMatch(match, u, demo);
                        continue;
                    }
                    else
                    {
                        var index = tblNewUnits.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                        var chooser = new AdminUnitMatcher(u, available);
                        chooser.Margin = new Padding(0, 5, 10, 5);
                        tblNewUnits.Controls.Add(chooser, 0, index);
                        matchers.Add(chooser);
                    }
                }

                if (matchers.Count == 0)
                    DoNextStep();
            }
        }

        public void DoPrev()
        {
        }

        public void DoNext()
        {
            var invalid = matchers.FirstOrDefault(m => !m.IsValid());
            if (invalid != null)
            {
                MessageBox.Show(Translations.RtiErrorMustMatchAll, Translations.ValidationErrorTitle);
                return;
            }

            DemoRepository demo = new DemoRepository();
            foreach (AdminUnitMatcher m in matchers)
                SaveMatch(m.GetSelected(), m.GetAdminUnit(), demo);

            DoNextStep();
        }

        private void DoNextStep()
        {
            int maxIndex = units.Max(u => u.LevelIndex);
            if (typeIndex == maxIndex)
            {
                OnSwitchStep(new RtiExport());
                return;
            }

            OnSwitchStep(new TaskForceAdminLevelStep(types, typeIndex + 1, units));
        }

        public void DoFinish()
        {
        }

        private void SaveMatch(TaskForceAdminUnit match, AdminLevel u, DemoRepository demo)
        {
            match.NadaId = u.Id;
            var adminLevel = demo.GetAdminLevelById(u.Id);
            adminLevel.TaskForceId = match.Id;
            adminLevel.TaskForceName = match.Name;
            demo.UpdateTaskForceData(adminLevel, ApplicationData.Instance.GetUserId());
        }
    }
}
