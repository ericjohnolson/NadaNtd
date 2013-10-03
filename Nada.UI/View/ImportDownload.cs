using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Csv;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using OfficeOpenXml;

namespace Nada.UI.View
{
    public partial class ImportDownload : Form
    {
        public event Action OnSuccess = () => { };
        private List<AdminLevel> available = new List<AdminLevel>();
        private List<AdminLevel> selected = new List<AdminLevel>();
        private IImporter importer;

        public ImportDownload()
        {
            InitializeComponent();
        }

        public ImportDownload(IImporter i)
        {
            importer = i;
            this.Text = i.ImportName;
            InitializeComponent();
        }

        private void ImportDemographyModal_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                openFileDialog1.Filter = "Excel files|*.xlsx;*.xls";
                saveFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog1.FileName = importer.ImportName;
                saveFileDialog1.DefaultExt = ".xlsx";
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataTable data = importer.GetDataTable();

                foreach (AdminLevel l in adminLevelMultiselect1.GetSelectedAdminLevels())
                {
                    DataRow row = data.NewRow();
                    row[Translations.Location + "#"] = l.Id;
                    row[Translations.Location] = l.Name;
                    data.Rows.Add(row);
                }
                using (ExcelPackage pck = new ExcelPackage())
                {
                    //Create the worksheet
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                    ws.Cells["A1"].LoadFromDataTable(data, true);
                    File.WriteAllBytes(saveFileDialog1.FileName, pck.GetAsByteArray());
                }
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DoImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                int userId = ApplicationData.Instance.GetUserId();
                var result = importer.ImportData(openFileDialog1.FileName, userId);
                if (!result.WasSuccess)
                    MessageBox.Show(result.ErrorMessage);
                else
                    MessageBox.Show(string.Format(Translations.ImportSuccess, result.Count));
            }
        }

    }
}
