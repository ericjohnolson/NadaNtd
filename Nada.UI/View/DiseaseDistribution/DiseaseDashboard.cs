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

namespace Nada.UI.View.Demography
{
    public partial class DiseaseDashboard : UserControl
    {
        public Action<UserControl> LoadView = (i) => { };
        public Action<AdminLevel> ReloadView = (i) => { };
        public Action<string> StatusChanged = (e) => { };
        IFetchDiseaseActivities fetcher = null;
        AdminLevel adminLevel = null;

        public DiseaseDashboard()
        {
            InitializeComponent();
        }

        public DiseaseDashboard(IFetchDiseaseActivities f, AdminLevel l)
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
            LoadDiseaseDistros();
           
        }

        private void DoLoadView(IView view)
        {
            if (view == null)
                return;
            view.OnClose = () => { ReloadView(adminLevel); };
            view.StatusChanged = (s) => { StatusChanged(s); };
            LoadView((UserControl)view);
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
            var types = fetcher.GetIntvTypes();
            types.Insert(0, new IntvType { Id = -1, IntvTypeName = Translations.AddNewIntvType });
            types.Insert(0, new IntvType { Id = 0, IntvTypeName = Translations.PleaseSelect });
            cbIntvTypes.DataSource = types;
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
            var f = (IFetchDiseaseActivities)e.Argument;
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
            var types = fetcher.GetSurveyTypes();
            types.Insert(0, new SurveyType { Id = -1, SurveyTypeName = Translations.AddNewSurveyTypeLink });
            types.Insert(0, new SurveyType { Id = 0, SurveyTypeName = Translations.PleaseSelect });
            cbNewSurvey.DataSource = types;
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
            var f = (IFetchDiseaseActivities)e.Argument;
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
                DoLoadView(new SurveyTypeEdit(new SurveyType()));

            IView view = fetcher.NewSurvey((SurveyType)cbNewSurvey.SelectedItem);
            DoLoadView(view);
        }

        private void btnSurvey_Click(object sender, EventArgs e)
        {
            DoCollapse(btnSurvey, pnlSurvey);
        }
        #endregion

        #region Disease Distro
        private void btnDisease_Click(object sender, EventArgs e)
        {
            DoCollapse(btnDisease, pnlDisease);
        }

        private void h3Link1_ClickOverride()
        {
            IView view = fetcher.NewDiseaseDistro();
            DoLoadView(view);
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
            var f = (IFetchDiseaseActivities)e.Argument;
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

        #region Overview
        private void btnOverview_Click_1(object sender, EventArgs e)
        {
            DoCollapse(btnOverview, pnlOverview);
        }

        #endregion





    }
}
