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
    public partial class TaskForceAdminLevel : BaseControl, IWizardStep
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
        private AdminLevelType adminLevelType;
        private List<TaskForceAdminLevel> units;


        public TaskForceAdminLevel()
            : base()
        {
            InitializeComponent();
        }

        public TaskForceAdminLevel(AdminLevelType t, List<TaskForceAdminLevel> u)
            : base()
        {
            InitializeComponent();
            title = string.Format(Translations.RtiMatchLevel, t.DisplayName);
            adminLevelType = t;
            units = u;
            
        }


        private void TaskForceAdminLevelStep_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                // Get all admin levels units for level that don't have a taskforce name/id
                // try to reconcile units
                // NEED TO ONLY SHOW IN BOX ONES WITH SAME PARENT (parent should be reconciled by now)
                // all that don't match put in matchers
            }
        }

        public void DoPrev()
        {
        }

        public void DoNext()
        {
            // foreach matcher, make sure they are all valid

            // VALIDATE
            //if (!adminUnitMatcher1.IsValid())
            //{
            //    MessageBox.Show(Translations.RtiErrorMustMatchAll, Translations.ValidationErrorTitle);
            //    return;
            //}

            // SAVE!
            //var unit = adminUnitMatcher1.GetSelected();
            //country.TaskForceName = unit.TaskForceName;
            //var userId = ApplicationData.Instance.GetUserId();
            //demo.UpdateCountry(country, userId);
        }

        public void DoFinish()
        {
            OnFinish();
        }
    }
}
