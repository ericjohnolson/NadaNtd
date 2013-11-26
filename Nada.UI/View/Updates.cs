using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.UI.Base;

namespace Nada.UI
{
    public partial class Updates : BaseForm
    {
        public Updates()
            : base()
        {
            InitializeComponent();
        }

        private void Updates_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                BackgroundWorker bgWorker = new BackgroundWorker();
                bgWorker.DoWork += bgWorker_DoWork;
                bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
                bgWorker.RunWorkerAsync();
            }
        }

        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pnlUpdating.Visible = false;
            lblStatus.Text = e.Result.ToString(); 
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
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
                        MessageBox.Show("The application has been upgraded, and will now restart.");
                        Application.Restart();
                    }
                    
                }
                catch (Exception)
                {
                    e.Result = Translations.UpdateException;
                }
            }
        }
    }
}
