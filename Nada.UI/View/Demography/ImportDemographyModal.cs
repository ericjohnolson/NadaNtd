using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LINQtoCSV;
using Nada.Model;
using Nada.Model.Csv;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;

namespace Nada.UI.View.Demography
{
    public partial class ImportDemographyModal : Form
    {
        public event Action OnSuccess = () => { };
        private AdminLevel parent;
        private List<AdminLevel> available = new List<AdminLevel>();
        private List<AdminLevel> selected = new List<AdminLevel>();

        public ImportDemographyModal()
        {
            InitializeComponent();
        }

        public ImportDemographyModal(AdminLevel p)
        {
            parent = p;
            InitializeComponent();
        }

        private void ImportDemographyModal_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                openFileDialog1.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog1.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog1.FileName = parent.Name + "_ChildDemographyImport";
                saveFileDialog1.DefaultExt = ".csv";
                available = parent.Children.Clone().ToList();
                bsAvailable.DataSource = available;
                bsSelected.DataSource = selected;
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                List<AdminLevelDemoRow> rows = new List<AdminLevelDemoRow>();
                foreach (var item in selected)
                    rows.Add(new AdminLevelDemoRow { ID = item.Id.ToString() });

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

                IEnumerable<AdminLevelDemoRow> children = cc.Read<AdminLevelDemoRow>(openFileDialog1.FileName, fileDesc);

                var demoToAdd = Convert(children.ToList());
                if (demoToAdd.Count == 0)
                    MessageBox.Show("There were no items to add.");
                else
                {
                    DemoRepository r = new DemoRepository();
                    //r.BulkAddDemography(demoToAdd, ApplicationData.Instance.GetUserId());
                    MessageBox.Show(string.Format("Successfully added {0} new records!", demoToAdd.Count));
                    OnSuccess();
                    this.Close();
                }
            }
        }

        private List<AdminLevelDemography> Convert(List<AdminLevelDemoRow> rows)
        {
            List<AdminLevelDemography> demos = new List<AdminLevelDemography>();
            double doubleValue = 0;
            int intValue = 0;
            foreach (var child in rows.ToList())
            {
                int id = 0;
                if (string.IsNullOrEmpty(child.ID) || !int.TryParse(child.ID, out id))
                    throw new ArgumentOutOfRangeException("ID", "ID must be a valid number.");

                demos.Add(new AdminLevelDemography
                {
                    //AdminLevelId = id,
                    //AdultPopulation = !string.IsNullOrEmpty(child.AdultPop) && int.TryParse(child.AdultPop, out intValue) ? intValue : 0,
                    //TotalPopulation = !string.IsNullOrEmpty(child.TotalPop) && int.TryParse(child.TotalPop, out intValue) ? intValue : 0,
                    //YearCensus = !string.IsNullOrEmpty(child.YearCensus) && int.TryParse(child.YearCensus, out intValue) ? intValue : 0,
                    //YearReporting = !string.IsNullOrEmpty(child.YearReporting) && int.TryParse(child.YearReporting, out intValue) ? intValue : 0,
                    //YearProjections = !string.IsNullOrEmpty(child.YearProjections) && int.TryParse(child.YearProjections, out intValue) ? intValue : 0,
                    //GrowthRate = !string.IsNullOrEmpty(child.GrowthRate) && Double.TryParse(child.GrowthRate, out doubleValue) ? doubleValue : 0,
                    //AdultsPercent = !string.IsNullOrEmpty(child.AdultsPercent) && Double.TryParse(child.AdultsPercent, out doubleValue) ? doubleValue : 0,
                    //FemalePercent = !string.IsNullOrEmpty(child.FemalePercent) && Double.TryParse(child.FemalePercent, out doubleValue) ? doubleValue : 0,
                    //MalePercent = !string.IsNullOrEmpty(child.MalePercent) && Double.TryParse(child.MalePercent, out doubleValue) ? doubleValue : 0
                });
            }
            return demos;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (var item in available)
                selected.Add(item);
            available.Clear();
            bsAvailable.ResetBindings(false);
            bsSelected.ResetBindings(false);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            foreach (var item in lbAvailable.SelectedItems)
            {
                selected.Add((AdminLevel)item);
                available.Remove((AdminLevel)item);
            }

            bsAvailable.ResetBindings(false);
            bsSelected.ResetBindings(false);
        }

        private void btnDeselect_Click(object sender, EventArgs e)
        {
            foreach (var item in lbSelected.SelectedItems)
            {
                selected.Remove((AdminLevel)item);
                available.Add((AdminLevel)item);
            }

            bsAvailable.ResetBindings(false);
            bsSelected.ResetBindings(false);
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            foreach (var item in selected)
                available.Add(item);
            selected.Clear();
            bsAvailable.ResetBindings(false);
            bsSelected.ResetBindings(false);
        }
    }
}
