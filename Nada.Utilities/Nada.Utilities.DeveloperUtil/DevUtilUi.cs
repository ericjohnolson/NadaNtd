using Nada.Globalization;
using Nada.Model;
using Nada.Model.Process;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nada.Utilities.DeveloperUtil
{
    public partial class DevUtilUi : Form
    {
        public DevUtilUi()
        {
            InitializeComponent();

            var thread = new Thread(
                s => ((CultureState)s).Result = Thread.CurrentThread.CurrentCulture);
            var state = new CultureState();
            thread.Start(state);
            thread.Join();
            CultureInfo culture = state.Result;

            Localizer.SetCulture(culture);
            Localizer.Initialize();
            DatabaseData.Instance.AccessConnectionString = ConfigurationManager.ConnectionStrings["AccessFileName"].ConnectionString;
        }

        private void updateTranslations_ClickOverride()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                loading.Visible = true;
                lnkUpdateTrans.Visible = false;
                BackgroundWorker worker = new BackgroundWorker();
                worker.RunWorkerCompleted += (s, e) =>
                {
                    loading.Visible = false;
                    lnkUpdateTrans.Visible = true;
                };
                worker.DoWork += (s, e) => { DoUpdateTranslations(openFileDialog1.FileName); };
                worker.RunWorkerAsync();
            }
        }

        private void createLists_ClickOverride()
        {
            loading.Visible = true;
            lnkCreateLists.Visible = false;
            BackgroundWorker worker = new BackgroundWorker();
            worker.RunWorkerCompleted += (s, e) =>
            {
                loading.Visible = false;
                lnkCreateLists.Visible = true;
            };
            worker.DoWork += (s, e) => { DoCreateLists(); };
            worker.RunWorkerAsync();
        }

        private class WorkerPayload
        {
            public Action DoAction { get; set; }
        }


        public void DoUpdateTranslations(string filePath)
        {
            string savePath = ConfigurationManager.AppSettings["TranslationsResourcePath"];
            DataSet ds = ImporterBase.LoadDataFromFile(filePath);
            Hashtable englishUpdates = new Hashtable();
            Hashtable frenchUpdates = new Hashtable();
            Hashtable portUpdates = new Hashtable();
            Hashtable bahasaUpdates = new Hashtable();

            foreach (DataRow dr in ds.Tables[0].Rows)
                AddTranslationKvp(dr, englishUpdates, frenchUpdates, portUpdates, bahasaUpdates);

            GlobalizationUtil.UpdateResourceFile(englishUpdates, savePath + "Translations.resx");
            GlobalizationUtil.UpdateResourceFile(englishUpdates, savePath + "Translations.en-US.resx");
            GlobalizationUtil.UpdateResourceFile(frenchUpdates, savePath + "Translations.fr-FR.resx");
            GlobalizationUtil.UpdateResourceFile(bahasaUpdates, savePath + "Translations.id-ID.resx");
            GlobalizationUtil.UpdateResourceFile(portUpdates, savePath + "Translations.pt-PT.resx");
        }

        private void AddTranslationKvp(DataRow dr, Hashtable englishUpdates, Hashtable frenchUpdates, Hashtable portUpdates, Hashtable bahasaUpdates)
        {
            string key = dr["Key"].ToString();
            if (!englishUpdates.ContainsKey(key))
                englishUpdates.Add(key, dr["English"].ToString());
            if (!frenchUpdates.ContainsKey(key))
                frenchUpdates.Add(key, dr["French"].ToString());
            if (!portUpdates.ContainsKey(key))
                portUpdates.Add(key, dr["Portuguese"].ToString());
            if (!bahasaUpdates.ContainsKey(key))
                bahasaUpdates.Add(key, dr["Bahasa"].ToString());
        }

        public void DoCreateLists()
        {
            CreateTranslationKeys();
            CreateIndicatorUpdateForm();
            CreateIndicatorOptionUpdateForm();
        }

        public void CreateTranslationKeys()
        {
            string path = ConfigurationManager.AppSettings["AutomatedListsSavePath"];

            DataTable table = new DataTable();
            Dictionary<string, DataRow> rowDict = new Dictionary<string, DataRow>();
            table.Columns.Add(new DataColumn("Key"));
            table.Columns.Add(new DataColumn("English"));
            table.Columns.Add(new DataColumn("French"));
            table.Columns.Add(new DataColumn("Portuguese"));
            table.Columns.Add(new DataColumn("Bahasa"));

            // id-ID;Bahasa|en-US;English|fr-FR;Français|pt-PT;Português"

            var english = new TranslationLookupInstance(new CultureInfo("en-US"));
            foreach (var key in english.Keys)
            {
                var dr = table.NewRow();
                dr["Key"] = key;
                dr["English"] = english.GetValue(key);
                table.Rows.Add(dr);
                rowDict.Add(key, dr);
            }

            var french = new TranslationLookupInstance(new CultureInfo("fr-FR"));
            foreach (var key in english.Keys)
            {
                var dr = rowDict[key];
                dr["French"] = french.GetValue(key);
            }
            var port = new TranslationLookupInstance(new CultureInfo("pt-PT"));
            foreach (var key in english.Keys)
            {
                var dr = rowDict[key];
                dr["Portuguese"] = port.GetValue(key);
            }

            var bahasa = new TranslationLookupInstance(new CultureInfo("id-ID"));
            foreach (var key in english.Keys)
            {
                var dr = rowDict[key];
                dr["Bahasa"] = bahasa.GetValue(key);
            }


            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                ws.Cells["A1"].LoadFromDataTable(table, true);
                File.WriteAllBytes(path + string.Format("TranslationKeys_{0}.xlsx", DateTime.Now.ToString("yyyyMMdd")), pck.GetAsByteArray());
            }
        }

        public void CreateIndicatorUpdateForm()
        {
            string path = ConfigurationManager.AppSettings["AutomatedListsSavePath"];

            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Indicator Id"));
            //table.Columns.Add(new DataColumn("IsDisabled"));
            table.Columns.Add(new DataColumn("Type Id"));
            table.Columns.Add(new DataColumn("Type Name"));
            table.Columns.Add(new DataColumn("Form Name"));
            table.Columns.Add(new DataColumn("Indicator Name"));
            table.Columns.Add(new DataColumn("Indicator Type"));
            table.Columns.Add(new DataColumn("Is Required"));
            table.Columns.Add(new DataColumn("Aggregation Rule"));
            table.Columns.Add(new DataColumn("Aggregation Rule ID"));
            table.Columns.Add(new DataColumn("Merge Rule"));
            table.Columns.Add(new DataColumn("Merge Rule ID"));
            table.Columns.Add(new DataColumn("Split Rule"));
            table.Columns.Add(new DataColumn("Split Rule ID"));
            ReportRepository repo = new ReportRepository();

            List<ReportIndicator> indicators = new List<ReportIndicator>();
            indicators = repo.GetDiseaseDistroIndicators();
            foreach (var cmpc in indicators)
                foreach (var cat in cmpc.Children)
                    AddInds(table, cat.Children, cat.Name, IndicatorEntityType.DiseaseDistribution);
            indicators = repo.GetSurveyIndicators();
            foreach (var cmpc in indicators)
                foreach (var cat in cmpc.Children)
                    AddInds(table, cat.Children, cat.Name, IndicatorEntityType.Survey);
            indicators = repo.GetIntvIndicators();
            foreach (var cmpc in indicators)
                foreach (var cat in cmpc.Children)
                    AddInds(table, cat.Children, cat.Name, IndicatorEntityType.Intervention);
            indicators = repo.GetProcessIndicators();
            foreach (var cmpc in indicators)
                foreach (var cat in cmpc.Children)
                    AddInds(table, cat.Children, cat.Name, IndicatorEntityType.Process);

            ProcessRepository prepo = new ProcessRepository();
            ProcessBase saes = prepo.Create(9);
            foreach (var i in saes.ProcessType.Indicators)
                indicators.Add(ReportRepository.CreateReportIndicator(saes.ProcessType.Id, i));
            AddInds(table, indicators, saes.ProcessType.TypeName, IndicatorEntityType.Process);

            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                ws.Cells["A1"].LoadFromDataTable(table, true);
                File.WriteAllBytes(path + string.Format("AllIndicatorRules_{0}.xlsx", DateTime.Now.ToString("yyyyMMdd")), pck.GetAsByteArray());
            }
        }

        public void CreateIndicatorOptionUpdateForm()
        {
            string path = ConfigurationManager.AppSettings["AutomatedListsSavePath"];

            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Indicator Id"));
            table.Columns.Add(new DataColumn("Type Id"));
            table.Columns.Add(new DataColumn("Type Name"));
            table.Columns.Add(new DataColumn("Form Name"));
            table.Columns.Add(new DataColumn("Indicator Name"));
            table.Columns.Add(new DataColumn("Indicator Option ID"));
            table.Columns.Add(new DataColumn("Indicator Option"));
            table.Columns.Add(new DataColumn("Weighted Ranking"));
            ReportRepository repo = new ReportRepository();
            List<ReportIndicator> indicators = repo.GetDiseaseDistroIndicators();
            foreach (var cmpc in indicators)
                foreach (var cat in cmpc.Children)
                    AddOptions(table, cat.Children.Where(x => x.DataTypeId == (int)IndicatorDataType.Dropdown), cat.Name, IndicatorEntityType.DiseaseDistribution);
            indicators = repo.GetSurveyIndicators();
            foreach (var cmpc in indicators)
                foreach (var cat in cmpc.Children)
                    AddOptions(table, cat.Children.Where(x => x.DataTypeId == (int)IndicatorDataType.Dropdown), cat.Name, IndicatorEntityType.Survey);
            indicators = repo.GetIntvIndicators();
            foreach (var cmpc in indicators)
                foreach (var cat in cmpc.Children)
                    AddOptions(table, cat.Children.Where(x => x.DataTypeId == (int)IndicatorDataType.Dropdown), cat.Name, IndicatorEntityType.Intervention);
            indicators = repo.GetProcessIndicators();
            foreach (var cmpc in indicators)
                foreach (var cat in cmpc.Children)
                    AddOptions(table, cat.Children.Where(x => x.DataTypeId == (int)IndicatorDataType.Dropdown), cat.Name, IndicatorEntityType.Process);

            ProcessRepository prepo = new ProcessRepository();
            ProcessBase saes = prepo.Create(9);
            foreach (var i in saes.ProcessType.Indicators)
                indicators.Add(ReportRepository.CreateReportIndicator(saes.ProcessType.Id, i));
            AddOptions(table, indicators.Where(x => x.DataTypeId == (int)IndicatorDataType.Dropdown), saes.ProcessType.TypeName, IndicatorEntityType.Process);

            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                ws.Cells["A1"].LoadFromDataTable(table, true);
                File.WriteAllBytes(path + string.Format("IndicatorDropdownWeightedRules_{0}.xlsx", DateTime.Now.ToString("yyyyMMdd")), pck.GetAsByteArray());
            }
        }

        private void AddInds(DataTable table, List<ReportIndicator> indicators, string formName, IndicatorEntityType type)
        {
            foreach (var ind in indicators)
            {
                var dr = table.NewRow();
                dr["Indicator Id"] = ind.ID;
                dr["Type Id"] = (int)type;
                dr["Type Name"] = type.ToString();
                dr["Form Name"] = formName;
                dr["Indicator Name"] = ind.Name;
                if (table.Columns.Contains("Indicator Type"))
                    dr["Indicator Type"] = ((IndicatorDataType)ind.DataTypeId).ToString();
                if (table.Columns.Contains("IsDisabled"))
                    dr["IsDisabled"] = ind.IsDisabled.ToString();
                if (table.Columns.Contains("Is Required"))
                    dr["Is Required"] = ind.IsRequired.ToString();

                if (table.Columns.Contains("Aggregation Rule"))
                {
                    dr["Aggregation Rule"] = ((IndicatorAggType)ind.AggregationRuleId).ToString();
                    dr["Aggregation Rule ID"] = (ind.AggregationRuleId).ToString();
                }
                if (table.Columns.Contains("Merge Rule"))
                {
                    dr["Merge Rule"] = ((MergingRule)ind.MergeRule).ToString();
                    dr["Merge Rule ID"] = (ind.MergeRule).ToString();
                }
                if (table.Columns.Contains("Split Rule"))
                {
                    dr["Split Rule"] = ((RedistrictingRule)ind.SplitRule).ToString();
                    dr["Split Rule ID"] = (ind.SplitRule).ToString();
                }
                table.Rows.Add(dr);
            }
        }

        private void AddOptions(DataTable table, IEnumerable<ReportIndicator> indicators, string formName, IndicatorEntityType type)
        {
            foreach (var ind in indicators)
            {
                foreach (var val in GetValues(type, ind.ID))
                {
                    if (ind.ID == 0)
                        continue;

                    var dr = table.NewRow();
                    dr["Indicator Id"] = ind.ID;
                    dr["Type Id"] = (int)type;
                    dr["Type Name"] = type.ToString();
                    dr["Form Name"] = formName;
                    dr["Indicator Name"] = ind.Name;
                    dr["Indicator Option ID"] = val.Id;
                    dr["Indicator Option"] = val.DisplayName;
                    dr["Weighted Ranking"] = val.WeightedValue;
                    table.Rows.Add(dr);
                }
            }
        }

        private List<IndicatorDropdownValue> GetValues(IndicatorEntityType indType, int indId)
        {
            RepositoryBase repo = new RepositoryBase();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                return repo.GetIndicatorDropdownValues(connection, command, indType, new List<string> { indId.ToString() });
            }
        }

    }
}
