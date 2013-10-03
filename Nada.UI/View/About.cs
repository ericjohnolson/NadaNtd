using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.UI.View.Demography;

namespace Nada.UI
{
    public partial class About : Form
    {
        public Action ReloadTree { get; set; }
        public About()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var updates = new Updates();
            updates.ShowDialog();
        }

        private void About_Load(object sender, EventArgs e)
        {
            if(!DesignMode)
                Localizer.TranslateControl(this);

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                var myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                lblVersion.Text = string.Format("{0}.{1}.{2}.{3}", myVersion.Major, myVersion.Minor, myVersion.Build, myVersion.Revision);
            }
        }

        private void fieldLink1_OnClick()
        {
            TestDemoImport import = new TestDemoImport();
            import.ReloadTree = () => { ReloadTree(); };
            import.ShowDialog();
        }
    }
}
