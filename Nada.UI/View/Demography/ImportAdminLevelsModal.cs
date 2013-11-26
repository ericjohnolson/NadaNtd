using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using LINQtoCSV;
using Nada.Model;
using Nada.Model.Csv;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;

namespace Nada.UI.View.Demography
{
    public partial class ImportAdminLevelsModal : BaseForm
    {
        public event Action OnSuccess = () => { };
        private AdminLevel parent = null;
        private AdminLevelType childType = null;

        public ImportAdminLevelsModal()
            : base()
        {
            InitializeComponent();
        }

        public ImportAdminLevelsModal(AdminLevel p, AdminLevelType childType)
            : base()
        {
            parent = p;
            this.childType = childType;
            InitializeComponent();
        }

        private void ImportAdminLevelsModal_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                openFileDialog1.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog1.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog1.FileName = parent.Name + "_ChildrenImport";
                saveFileDialog1.DefaultExt = ".csv";
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                List<AdminLevelRow> rows = new List<AdminLevelRow>();
                CsvFileDescription fileDesc = new CsvFileDescription
                {
                    // FileCultureName = Localizer.GetCultureName(),
                    TextEncoding = Encoding.Unicode,
                    FirstLineHasColumnNames = true, // no column names in first record
                    EnforceCsvColumnAttribute = true,
                    // SeparatorChar = '\t',
                    // FileCultureName = "en-US" // use formats used in The Netherlands
                };

                CsvContext cc = new CsvContext();
                cc.Write(rows, saveFileDialog1.FileName, fileDesc);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CsvFileDescription fileDesc = new CsvFileDescription
                {
                    // FileCultureName = Localizer.GetCultureName(),
                    TextEncoding = Encoding.Unicode,
                    FirstLineHasColumnNames = true, // no column names in first record
                    EnforceCsvColumnAttribute = true,
                    // SeparatorChar = '\t', // tab delimited
                    // FileCultureName = "en-US" // use formats used in The Netherlands
                };
                CsvContext cc = new CsvContext();

                IEnumerable<AdminLevelRow> children = cc.Read<AdminLevelRow>(openFileDialog1.FileName, fileDesc);


                var toAdd = Convert(children.ToList());
                if (toAdd.Count == 0)
                    MessageBox.Show("There were no items to add.");
                else
                {
                    DemoRepository r = new DemoRepository();
                    r.AddChildren(parent, toAdd, childType, ApplicationData.Instance.GetUserId());
                    MessageBox.Show(string.Format("Successfully added {0} new records!", toAdd.Count));
                    OnSuccess();
                    this.Close();
                }
            }
        }

        private List<AdminLevel> Convert(List<AdminLevelRow> rows)
        {
            List<AdminLevel> levels = new List<AdminLevel>();
            double geoValue = 0;
            foreach (var child in rows.ToList())
                levels.Add(new AdminLevel
                {
                    Name = child.Name,
                    LngOther = !string.IsNullOrEmpty(child.Lng) && Double.TryParse(child.Lng, out geoValue) ? geoValue : 0,
                    LatOther = !string.IsNullOrEmpty(child.Lat) && Double.TryParse(child.Lat, out geoValue) ? geoValue : 0,
                    LatWho = !string.IsNullOrEmpty(child.LatWho) && Double.TryParse(child.LatWho, out geoValue) ? geoValue : 0,
                    LngWho = !string.IsNullOrEmpty(child.LngWho) && Double.TryParse(child.LngWho, out geoValue) ? geoValue : 0
                });

            return levels;
        }
    }
}
