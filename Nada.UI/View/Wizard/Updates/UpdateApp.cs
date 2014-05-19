using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Csv;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;
using OfficeOpenXml;

namespace Nada.UI.View.Wizard
{
    public partial class UpdateApp : BaseControl, IWizardStep
    {
        bool hasUpdate = false;
        bool hasInternet = false;
        public Action OnFinish { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action OnRestart { get; set; }
        public bool ShowNext { get { return hasUpdate; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return !hasUpdate; } }
        public bool EnableFinish { get { return true; } }
        public string StepTitle { get { return Translations.ApplicationUpdate; } }

        public UpdateApp()
            : base()
        {
            InitializeComponent();
        }

        public UpdateApp(Action restart)
            : base()
        {
            hasUpdate = Util.HasInternetConnection() && HasUpdate();
            hasInternet = Util.HasInternetConnection();
            OnRestart = restart;
            InitializeComponent();
        }

        private void ImportStepResult_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);

                lblStatus.SetMaxWidth(500);
                if (hasInternet)
                {
                    if (hasUpdate)
                    {
                        lblStatus.Text = Translations.UpdateFound;
                    }
                    else
                    {
                        lblStatus.Text = Translations.UpdateNone;
                    }
                }
                else
                {
                    lblStatus.Text = Translations.UpdateNoInternet;
                }
            }
        }

        public void DoPrev()
        {
        }

        public void DoNext()
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += updateWorker_DoWork;
            bgWorker.RunWorkerCompleted += updateWorker_RunWorkerCompleted;
            bgWorker.RunWorkerAsync();

            OnSwitchStep(new WorkingStep(Translations.Updating));
        }

        void updateWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool success = false;
            string result = e.Result.ToString();

            if (result != "exception")
            {
                success = true;
                result = Translations.UpdateComplete;
            }
            else
                result = Translations.UpdateException;

            OnSwitchStep(new UpdateDbResult(success, result, this, DoRestart));

            if (success == true)
            {
                MessageBox.Show(Translations.UpdateRestart);
                OnRestart();
            }
        }

        public void DoRestart()
        {
        }

        void updateWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = "NoUpdate"; 
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;
                try
                {
                    UpdateCheckInfo info = updateCheck.CheckForDetailedUpdate();
                    if (info.UpdateAvailable)
                    {
                        updateCheck.Update();
                        e.Result = "complete"; 
                    }
                }
                catch (Exception ex)
                {
                    Logger logger = new Logger();
                    logger.Error("Error updating application", ex);
                    e.Result = "exception";
                }
            }
        }

        public void DoFinish()
        {
            OnFinish();
        }

        

        public static bool HasUpdate()
        {
            var logger = new Logger();
            try
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;

                    UpdateCheckInfo info = updateCheck.CheckForDetailedUpdate();
                    return info.UpdateAvailable;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Exception checking HasInternetConnection", ex);
                return false;
            }
            return false;
        }


    }
}
