using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Diseases;
using Nada.Model.Survey;
using System.Threading;
using Nada.UI.AppLogic;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Intervention;
using Nada.UI.View.Intervention;
using Nada.UI.View.Survey;
using Nada.Model.Demography;
using Nada.Model.Repositories;
using Nada.Model.Process;
using Nada.UI.View.Process;
using Nada.UI.Base;
using System.Web.Security;

namespace Nada.UI.View.Demography
{
    public partial class DiseaseDashboard : BaseControl
    {
        public Action<IView> LoadView = (i) => { };
        public Action<IView> LoadForm = (i) => { };
        public Action<AdminLevel> ReloadView = (i) => { };
        public Action<string> StatusChanged = (e) => { };
        IFetchActivities fetcher = null;
        AdminLevel adminLevel = null;

        public DiseaseDashboard()
            : base()
        {
            InitializeComponent();
        }

        public DiseaseDashboard(IFetchActivities f, AdminLevel l)
            : base()
        {
            InitializeComponent();
            fetcher = f;
            adminLevel = l;
        }

        public void LoadContent()
        {
            Localizer.TranslateControl(this);
            LoadSurveyTypes();
            LoadSurveys();
            LoadIntvTypes();
            LoadInterventions();
            LoadDiseases();
            LoadDiseaseDistros();
            LoadDemography();
            LoadProcessTypes();
            LoadProcesses();
            if (!Roles.IsUserInRole(ApplicationData.Instance.CurrentUser.UserName, "RoleDataEnterer") &&
                !Roles.IsUserInRole(ApplicationData.Instance.CurrentUser.UserName, "RoleAdmin"))
            {
                tblEditDd.Visible = false;
                tblEditIntv.Visible = false;
                tblEditProcess.Visible = false;
                tblEditSurveys.Visible = false;

                lvDemo.AllColumns[5].IsVisible = false; 
                lvDemo.RebuildColumns();
                lvDiseaseDistro.AllColumns[4].IsVisible = false;
                lvDiseaseDistro.RebuildColumns();
                lvIntv.AllColumns[4].IsVisible = false;
                lvIntv.RebuildColumns();
                lvProcess.AllColumns[4].IsVisible = false;
                lvProcess.RebuildColumns();
                lvSurveys.AllColumns[4].IsVisible = false;
                lvSurveys.RebuildColumns();
            }
        }
        
        private void DoLoadView(IView view)
        {
            if (view == null)
                return;
            view.OnClose = () => { ReloadView(adminLevel); };
            //view.StatusChanged = (s) => { StatusChanged(s); };
            LoadForm(view);
        }

        private void DoCollapse(Button toggleBtn, Panel collapsible)
        {
            if (collapsible.Visible)
            {
                toggleBtn.Image = global::Nada.UI.Properties.Resources.ExpanderPlusIcon16x16;
                collapsible.Visible = false;
            }
            else
            {
                toggleBtn.Image = global::Nada.UI.Properties.Resources.ExpanderMinusIcon16x16;
                collapsible.Visible = true;
            }
        }

        #region Interventions
        private void LoadIntvTypes()
        {
            var types = fetcher.GetIntvTypes().OrderBy(t => t.IntvTypeName).ToList();
            types.Insert(0, new IntvType { Id = -1, IntvTypeName = Translations.AddNewIntvType });
            types.Insert(0, new IntvType { Id = 0, IntvTypeName = Translations.PleaseSelect });
            cbIntvTypes.DataSource = types;
            
            cbIntvTypes.DropDownWidth = BaseForm.GetDropdownWidth(types.Select(t => t.IntvTypeName));
        }

        private void LoadInterventions()
        {
            BackgroundWorker intvWorker = new BackgroundWorker();
            intvWorker.DoWork += intvWorker_DoWork;
            intvWorker.RunWorkerCompleted += intvWorker_RunWorkerCompleted;
            pnlIntvDetails.Visible = false;
            loadingIntvs.Visible = true;
            intvWorker.RunWorkerAsync(fetcher);
        }

        void intvWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var existing = (List<IntvDetails>)e.Result;
            lvIntv.SetObjects(existing);
            loadingIntvs.Visible = false;
            pnlIntvDetails.Visible = true;
        }

        void intvWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var f = (IFetchActivities)e.Argument;
            e.Result = f.GetIntvs();
        }

        private void lvIntvs_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            if (e.Column.AspectName == "View")
            {
                IView view = fetcher.GetIntv((IntvDetails)e.Model);
                if (view == null)
                    return;
                DoLoadView(view);
            }
            else if (e.Column.AspectName == "Delete")
            {
                DeleteConfirm confirm = new DeleteConfirm();
                if (confirm.ShowDialog() == DialogResult.OK)
                {
                    fetcher.Delete((IntvDetails)e.Model, ApplicationData.Instance.GetUserId());
                    LoadInterventions();
                }
            }
        }

        private void cbIntvTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIntvTypes.SelectedItem == null)
                return;
            if (((IntvType)cbIntvTypes.SelectedItem).Id == -1)
                DoLoadView(new IntvTypeEdit(new IntvType()));

            IView view = fetcher.NewIntv((IntvType)cbIntvTypes.SelectedItem);
            DoLoadView(view);
        }

        private void btnIntervention_Click(object sender, EventArgs e)
        {
            DoCollapse(btnIntervention, pnlIntv);
        }
        
        #endregion

        #region Surveys
        private void LoadSurveyTypes()
        {
            var types = fetcher.GetSurveyTypes().OrderBy(t => t.SurveyTypeName).ToList();
            types.Insert(0, new SurveyType { Id = -1, SurveyTypeName = Translations.AddNewSurveyTypeLink });
            types.Insert(0, new SurveyType { Id = 0, SurveyTypeName = Translations.PleaseSelect });
            cbNewSurvey.DataSource = types;

            cbNewSurvey.DropDownWidth = BaseForm.GetDropdownWidth(types.Select(t => t.SurveyTypeName));
        }

        private void LoadSurveys()
        {
            BackgroundWorker surveyWorker = new BackgroundWorker();
            surveyWorker.DoWork += surveyWorker_DoWork;
            surveyWorker.RunWorkerCompleted += surveyWorker_RunWorkerCompleted;
            pnlSurveyDetails.Visible = false;
            loadingSurveys.Visible = true;
            surveyWorker.RunWorkerAsync(fetcher);
        }

        void surveyWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var existing = (List<SurveyDetails>)e.Result;
            lvSurveys.SetObjects(existing);
            loadingSurveys.Visible = false;
            pnlSurveyDetails.Visible = true;
        }

        void surveyWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var f = (IFetchActivities)e.Argument;
            e.Result = f.GetSurveys();
        }

        private void lvSurveys_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            if (e.Column.AspectName == "View")
            {
                IView survey = fetcher.GetSurvey((SurveyDetails)e.Model);
                if (survey == null)
                    return;
                DoLoadView(survey);
            }
            else if (e.Column.AspectName == "Delete")
            {
                DeleteConfirm confirm = new DeleteConfirm();
                if (confirm.ShowDialog() == DialogResult.OK)
                {
                    fetcher.Delete((SurveyDetails)e.Model, ApplicationData.Instance.GetUserId());
                    LoadSurveys();
                }
            }

        }

        private void cbNewSurvey_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNewSurvey.SelectedItem == null)
                return;
            if (((SurveyType)cbNewSurvey.SelectedItem).Id == -1)
                DoLoadView(new SurveyTypeEdit(new SurveyType { DiseaseId = (int)DiseaseType.Custom }));

            IView view = fetcher.NewSurvey((SurveyType)cbNewSurvey.SelectedItem);
            DoLoadView(view);
        }

        private void btnSurvey_Click(object sender, EventArgs e)
        {
            DoCollapse(btnSurvey, pnlSurvey);
        }
        #endregion

        #region Disease Distro
        private void LoadDiseases()
        {
            var types = fetcher.GetDiseases().OrderBy(t => t.DisplayName).ToList();
            types.Insert(0, new Disease { Id = 0, DisplayName = Translations.PleaseSelect });
            cbNewDiseaseDistro.DataSource = types;
            cbNewDiseaseDistro.DropDownWidth = BaseForm.GetDropdownWidth(types.Select(t=>t.DisplayName));
        }

        private void cbNewDiseaseDistro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNewDiseaseDistro.SelectedItem == null)
                return;

            IView view = fetcher.NewDiseaseDistro((Disease)cbNewDiseaseDistro.SelectedItem);
            DoLoadView(view);
        }

        private void btnDisease_Click(object sender, EventArgs e)
        {
            DoCollapse(btnDisease, pnlDisease);
        }

        private void LoadDiseaseDistros()
        {
            BackgroundWorker DiseaseDistroWorker = new BackgroundWorker();
            DiseaseDistroWorker.DoWork += DiseaseDistroWorker_DoWork;
            DiseaseDistroWorker.RunWorkerCompleted += DiseaseDistroWorker_RunWorkerCompleted;
            pnlDistroDetails.Visible = false;
            loadingDistros.Visible = true;
            DiseaseDistroWorker.RunWorkerAsync(fetcher);
        }

        void DiseaseDistroWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var existing = (List<DiseaseDistroDetails>)e.Result;
            lvDiseaseDistro.SetObjects(existing);
            loadingDistros.Visible = false;
            pnlDistroDetails.Visible = true;
        }

        void DiseaseDistroWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var f = (IFetchActivities)e.Argument;
            e.Result = f.GetDiseaseDistros();
        }

        private void lvDiseaseDistros_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            if (e.Column.AspectName == "View")
            {
                IView DiseaseDistro = fetcher.GetDiseaseDistro((DiseaseDistroDetails)e.Model);
                if (DiseaseDistro == null)
                    return;
                DoLoadView(DiseaseDistro);
            }
            else if (e.Column.AspectName == "Delete")
            {
                DeleteConfirm confirm = new DeleteConfirm();
                if (confirm.ShowDialog() == DialogResult.OK)
                {
                    fetcher.Delete((DiseaseDistroDetails)e.Model, ApplicationData.Instance.GetUserId());
                    LoadDiseaseDistros();
                }
            }

        }
        #endregion

        #region Demography
        private void btnOverview_Click_1(object sender, EventArgs e)
        {
            DoCollapse(btnOverview, pnlDemo);
        }

        private void LoadDemography()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += DemoWorker_DoWork;
            worker.RunWorkerCompleted += DemoWorker_RunWorkerCompleted;
            pnlDemo.Visible = false;
            loadingDemos.Visible = true;
            worker.RunWorkerAsync(fetcher);
        }

        void DemoWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DemoPayload result = (DemoPayload)e.Result;
            lvDemo.SetObjects(result.DemoList);


            lnkAddDemo.Visible = result.AllowAdd;
            loadingDemos.Visible = false;
            pnlDistroDetails.Visible = true;

            if (!Roles.IsUserInRole(ApplicationData.Instance.CurrentUser.UserName, "RoleDataEnterer") &&
                !Roles.IsUserInRole(ApplicationData.Instance.CurrentUser.UserName, "RoleAdmin"))
            {
                lnkAddDemo.Visible = false;
            }
        }

        void DemoWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var f = (IFetchActivities)e.Argument;
            var demo = f.GetDemography();
            SettingsRepository repo = new SettingsRepository();
            var adminType = repo.GetAdminLevelTypeByLevel(adminLevel.LevelNumber);
            bool canCrud = false;
            if (adminType != null)
                canCrud = adminType.IsDemographyAllowed;
            if (canCrud)
            {
                foreach (var d in demo)
                {
                    d.CanView = true;
                    d.CanDelete = true;
                }
            }
            e.Result = new DemoPayload
            {
                DemoList = demo,
                AllowAdd = canCrud
            };
        }
        public class DemoPayload
        {
            public List<DemoDetails> DemoList { get; set; }
            public bool AllowAdd { get; set; }
        }

        private void lvDemo_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            if (e.Column.AspectName == "View")
            {
                IView demo = fetcher.GetDemo((DemoDetails)e.Model);
                if (demo == null)
                    return;
                DoLoadView(demo);
            }
            else if (e.Column.AspectName == "Delete")
            {
                DeleteConfirm confirm = new DeleteConfirm();
                if (confirm.ShowDialog() == DialogResult.OK)
                {
                    fetcher.Delete((DemoDetails)e.Model, ApplicationData.Instance.GetUserId());
                    LoadDemography();
                }
            }

        }

        private void h3Link1_ClickOverride()
        {
            IView view = fetcher.NewDemo();
            DoLoadView(view);
        }
        #endregion

        #region Processes
        private void LoadProcesses()
        {
            BackgroundWorker processWorker = new BackgroundWorker();
            processWorker.DoWork += processWorker_DoWork;
            processWorker.RunWorkerCompleted += processWorker_RunWorkerCompleted;
            pnlProcessDetails.Visible = false;
            loadingProcess.Visible = true;
            processWorker.RunWorkerAsync(fetcher);
        }

        private void LoadProcessTypes()
        {
            var types = fetcher.GetProcessTypes().OrderBy(t => t.TypeName).ToList();
            types.Insert(0, new ProcessType { Id = -1, TypeName = Translations.AddNewType });
            types.Insert(0, new ProcessType { Id = 0, TypeName = Translations.PleaseSelect });
            cbProcessTypes.DataSource = types;
            cbProcessTypes.DropDownWidth = BaseForm.GetDropdownWidth(types.Select(t => t.TypeName));
        }

        void processWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var existing = (List<ProcessDetails>)e.Result;
            lvProcess.SetObjects(existing);
            loadingProcess.Visible = false;
            pnlProcessDetails.Visible = true;
        }

        void processWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var f = (IFetchActivities)e.Argument;
            e.Result = f.GetProcesses();
        }

        private void lvProcess_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            if (e.Column.AspectName == "View")
            {
                IView process = fetcher.GetProcess((ProcessDetails)e.Model);
                if (process == null)
                    return;
                DoLoadView(process);
            }
            else if (e.Column.AspectName == "Delete")
            {
                DeleteConfirm confirm = new DeleteConfirm();
                if (confirm.ShowDialog() == DialogResult.OK)
                {
                    fetcher.Delete((ProcessDetails)e.Model, ApplicationData.Instance.GetUserId());
                    LoadProcesses();
                }
            }
        }

        private void cbProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProcessTypes.SelectedItem == null)
                return;
            if (((ProcessType)cbProcessTypes.SelectedItem).Id == -1)
                DoLoadView(new ProcessTypeEdit(new ProcessType()));

            IView view = fetcher.NewProcess((ProcessType)cbProcessTypes.SelectedItem);
            DoLoadView(view);
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            DoCollapse(btnProcess, pnlProcess);
        }
        #endregion

    }
}
