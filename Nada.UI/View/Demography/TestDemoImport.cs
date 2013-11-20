using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;

namespace Nada.UI.View.Demography
{
    public partial class TestDemoImport : Form
    {
        public Action ReloadTree { get; set; }
        public TestDemoImport()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbCountryName.Text))
            {
                MessageBox.Show("Country name required");
                return;
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                btnUpload.Text = "Uploading...";
                btnUpload.Enabled = false;

                DemoRepository r = new DemoRepository();
                var country = r.GetCountry();
                country.Name = tbCountryName.Text;
                r.UpdateCountry(country, ApplicationData.Instance.GetUserId());
                AdminLevelImporter importer = new AdminLevelImporter();
                var result = importer.ImportData(openFileDialog1.FileName, ApplicationData.Instance.GetUserId());

                if (!result.WasSuccess)
                    MessageBox.Show(result.Message);
                else
                {
                    MessageBox.Show(string.Format("Successfully added {0} new districts!", result.Count));
                    ReloadTree();
                    this.Close();
                }
            }
        }
    }
}
