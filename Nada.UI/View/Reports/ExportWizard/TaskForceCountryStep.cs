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
                    DoNextStep();
                TaskForceApi api = new TaskForceApi();
                var taskForceCountries = api.GetAllCountries();
                var tfCountry = taskForceCountries.FirstOrDefault(c => c.Name == country.Name);
                if (tfCountry != null)
                {
                    country.TaskForceName = tfCountry.Name;
                    var userId = ApplicationData.Instance.GetUserId();
                    demo.UpdateCountry(country, userId);
                    DoNextStep();
                }
                adminUnitMatcher1.BindData(new AdminLevel { Name = country.Name }, taskForceCountries);
            }
        }

        public void DoPrev()
        {
        }
        public void DoNext()
        {
            if (!adminUnitMatcher1.IsValid(new List<int>()))
            {
                MessageBox.Show(Translations.RtiErrorMustMatchAll, Translations.ValidationErrorTitle);
                return;
            }

            var unit = adminUnitMatcher1.GetSelected();
            country.TaskForceName = unit.Name;

            DoNextStep();

        }

        private void DoNextStep()
        {
            // Nada "V3" story 14, Turn off admin unit matching until issues resolved with Geoconnect API
            if (TrySkip())
                return;
            OnFinish();

            /*if (Util.HasInternetConnection())
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.RunWorkerAsync(country.TaskForceName);
                OnSwitchStep(new WorkingStep(Translations.RtiFetchingTaskForceNames));
            }
            else
            {
                if (TrySkip())
                    return;
                OnFinish();
            }*/
        }


        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                TaskForceApi api = new TaskForceApi();
                e.Result = api.GetAllDistricts((string)e.Argument);
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Error fetching country information from task force api. TaskForceCountryStep (worker_DoWork). ", ex);
                e.Result = new TaskForceApiResult { WasSuccessful = false, ErrorMsg = Translations.UnexpectedException + " " + ex.Message };
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TaskForceApiResult result = (TaskForceApiResult)e.Result;
            if (!result.WasSuccessful)
            {
                MessageBox.Show(result.ErrorMsg, Translations.ErrorOccured, MessageBoxButtons.OK, MessageBoxIcon.Error);
                OnFinish();
                return;
            }
            if (result.Units.Count == 0)
            {
                MessageBox.Show(Translations.RtiNoData, Translations.ErrorOccured, MessageBoxButtons.OK, MessageBoxIcon.Error);
                OnSwitchStep(this);

                if (TrySkip())
                    return;
                return;
            }

            var userId = ApplicationData.Instance.GetUserId();
            demo.UpdateCountry(country, userId);
            SettingsRepository settings = new SettingsRepository();
            List<AdminLevelType> levels = settings.GetAllAdminLevels();
            OnSwitchStep(new TaskForceAdminLevelStep(levels, 0, result.Units));
        }

        public void DoFinish()
        {

        }

        private bool TrySkip()
        {
            TaskForceNoInternet confirm = new TaskForceNoInternet();
            if (confirm.ShowDialog() == DialogResult.OK)
            {
                if (confirm.IsSkipping())
                {
                    OnSwitchStep(new RtiExport());
                    return true;
                }
            }
            return false;
        }
    }
}
