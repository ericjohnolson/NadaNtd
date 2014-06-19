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
using Nada.UI.AppLogic;
using Nada.UI.Base;
using Nada.UI.View;
using Nada.UI.View.Demography;
using Nada.UI.View.Wizard;

namespace Nada.UI
{
    public partial class About : BaseForm
    {
        public Action OnRestart { get; set; }

        public About()
            : base()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            WizardForm wiz = new WizardForm(new UpdateApp(OnRestart), Translations.Updates);
            wiz.OnFinish += () => { };
            wiz.ShowDialog();
        }

        private void About_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                h3bLabel1.SetMaxWidth(400);
                Localizer.TranslateControl(this);
            }

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                var myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                lblVersion.Text = string.Format("{0}.{1}.{2}.{3}", myVersion.Major, myVersion.Minor, myVersion.Build, myVersion.Revision);
            }
            else
                lblVersion.Text = "Not Network Deployed";
        }

    }
}
