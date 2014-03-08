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
        public Action<ReportOptions> OnRunReport { get; set; }
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
            hasUpdate = HasInternetConnection() && HasUpdate();
            hasInternet = HasInternetConnection();
            OnRestart = restart;
            InitializeComponent();
        }

        private void ImportStepResult_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);

                lblStatus.SetMaxWidth(500);
                if(hasInternet)
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
            if (result == Translations.UpdateComplete)
                success = true;

            OnSwitchStep(new UpdateDbResult(success, result, this));

            if (success == true)
            {
                MessageBox.Show(Translations.UpdateRestart);
                OnRestart();
            }
        }

        void updateWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Translations.UpdateNone;
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;
                try
                {
                    UpdateCheckInfo info = updateCheck.CheckForDetailedUpdate();
                    if (info.UpdateAvailable)
                    {
                        updateCheck.Update();
                        e.Result = Translations.UpdateComplete;
                    }
                }
                catch (Exception)
                {
                    e.Result = Translations.UpdateException;
                }
            }
        }

        public void DoFinish()
        {
            OnFinish();
        }

        public static bool HasInternetConnection()
        {
            try
            {
                string myAddress = "www.google.com";
                IPAddress[] addresslist = Dns.GetHostAddresses(myAddress);

                if (addresslist[0].ToString().Length > 6)
                {
                    return true;
                }
                else
                    return false;

            }
            catch
            {
                return false;
            }

        }

        public static bool HasUpdate()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;

                try
                {
                    UpdateCheckInfo info = updateCheck.CheckForDetailedUpdate();
                    return info.UpdateAvailable;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

       
    }
}
