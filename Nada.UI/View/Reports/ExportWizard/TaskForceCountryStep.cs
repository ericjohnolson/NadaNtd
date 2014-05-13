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
    public partial class TaskForceCountryStep : BaseControl, IWizardStep
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
        public string StepTitle { get { return string.Format(Translations.RtiMatchLevel, Translations.Country); } }
        private Country country = null;
        private DemoRepository demo = new DemoRepository();

        public TaskForceCountryStep()
            : base()
        {
            InitializeComponent();
        }

        private void TaskForceCountryStep_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                country = demo.GetCountry();
                if (!string.IsNullOrEmpty(country.TaskForceName))
                    DoNext();
                TaskForceApi api = new TaskForceApi();
                var taskForceCountries = api.GetAllCountries();
                var tfCountry = taskForceCountries.FirstOrDefault(c => c.Name == country.Name);
                if (tfCountry != null)
                {
                    country.TaskForceName = tfCountry.Name;
                    var userId = ApplicationData.Instance.GetUserId();
                    demo.UpdateCountry(country, userId);
                    DoNext();
                }
                adminUnitMatcher1 = new AdminUnitMatcher(new AdminLevel { Name = country.Name }, taskForceCountries);
            }
        }

        public void DoPrev()
        {
        }
        public void DoNext()
        {
            if (!adminUnitMatcher1.IsValid())
            {
                MessageBox.Show(Translations.RtiErrorMustMatchAll, Translations.ValidationErrorTitle);
                return;
            }

            var unit = adminUnitMatcher1.GetSelected();
            country.TaskForceName = unit.TaskForceName;
            var userId = ApplicationData.Instance.GetUserId();
            demo.UpdateCountry(country, userId);

            // Show working dial while getting all the units


        }
        public void DoFinish()
        {

        }
    }
}
