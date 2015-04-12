using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nada.Model;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using OfficeOpenXml;
using System.Linq;
using System.Data.OleDb;
using Nada.Model.Process;
using Nada.Globalization;
using System.Globalization;

namespace Nada.Tests
{
    [TestClass]
    public class ExcelWorksheetGeneration : BaseTest
    {
        ///
        /// Moved to Nada.Utilities.DeveloperUtility
        ///
        //[TestMethod]
        //public void CanCreateTranslationKeys()
        //{
        //    DataTable table = new DataTable();
        //    Dictionary<string, DataRow> rowDict = new Dictionary<string, DataRow>();
        //    table.Columns.Add(new DataColumn("Key"));
        //    table.Columns.Add(new DataColumn("English"));
        //    table.Columns.Add(new DataColumn("French"));
        //    table.Columns.Add(new DataColumn("Portuguese"));
        //    table.Columns.Add(new DataColumn("Bahasa"));

        //    // id-ID;Bahasa|en-US;English|fr-FR;Français|pt-PT;Português"

        //    var english = new TranslationLookupInstance(new CultureInfo("en-US"));
        //    foreach(var key in english.Keys)
        //    {
        //        var dr = table.NewRow();
        //        dr["Key"] = key;
        //        dr["English"] = english.GetValue(key);
        //        table.Rows.Add(dr);
        //        rowDict.Add(key, dr);
        //    }

        //    var french = new TranslationLookupInstance(new CultureInfo("fr-FR"));
        //    foreach (var key in english.Keys)
        //    {
        //        var dr = rowDict[key];
        //        dr["French"] = french.GetValue(key);
        //    }
        //    var port = new TranslationLookupInstance(new CultureInfo("pt-PT"));
        //    foreach (var key in english.Keys)
        //    {
        //        var dr = rowDict[key];
        //        dr["Portuguese"] = port.GetValue(key);
        //    }
           
        //    var bahasa = new TranslationLookupInstance(new CultureInfo("id-ID"));
        //    foreach (var key in english.Keys)
        //    {
        //        var dr = rowDict[key];
        //        dr["Bahasa"] = bahasa.GetValue(key);
        //    }
            

        //    using (ExcelPackage pck = new ExcelPackage())
        //    {
        //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
        //        ws.Cells["A1"].LoadFromDataTable(table, true);
        //        File.WriteAllBytes(string.Format(@"C:\Users\Eric\Desktop\Iota Ink\TranslationKeys_{0}.xlsx", DateTime.Now.ToString("yyyyMMdd")), pck.GetAsByteArray());
        //    }
        //}

        //[TestMethod]
        //public void CanCreateIndicatorUpdateForm()
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add(new DataColumn("Indicator Id"));
        //    //table.Columns.Add(new DataColumn("IsDisabled"));
        //    table.Columns.Add(new DataColumn("Type Id"));
        //    table.Columns.Add(new DataColumn("Type Name"));
        //    table.Columns.Add(new DataColumn("Form Name"));
        //    table.Columns.Add(new DataColumn("Indicator Name"));
        //    table.Columns.Add(new DataColumn("Indicator Type"));
        //    table.Columns.Add(new DataColumn("Is Required"));
        //    table.Columns.Add(new DataColumn("Aggregation Rule"));
        //    table.Columns.Add(new DataColumn("Aggregation Rule ID"));
        //    table.Columns.Add(new DataColumn("Merge Rule"));
        //    table.Columns.Add(new DataColumn("Merge Rule ID"));
        //    table.Columns.Add(new DataColumn("Split Rule"));
        //    table.Columns.Add(new DataColumn("Split Rule ID"));
        //    ReportRepository repo = new ReportRepository();

        //    List<ReportIndicator> indicators = new List<ReportIndicator>();
        //    indicators = repo.GetDiseaseDistroIndicators();
        //    foreach (var cmpc in indicators)
        //        foreach (var cat in cmpc.Children)
        //            AddInds(table, cat.Children, cat.Name, IndicatorEntityType.DiseaseDistribution);
        //    indicators = repo.GetSurveyIndicators();
        //    foreach (var cmpc in indicators)
        //        foreach (var cat in cmpc.Children)
        //            AddInds(table, cat.Children, cat.Name, IndicatorEntityType.Survey);
        //    indicators = repo.GetIntvIndicators();
        //    foreach (var cmpc in indicators)
        //        foreach (var cat in cmpc.Children)
        //            AddInds(table, cat.Children, cat.Name, IndicatorEntityType.Intervention);
        //    indicators = repo.GetProcessIndicators();
        //    foreach (var cmpc in indicators)
        //        foreach (var cat in cmpc.Children)
        //            AddInds(table, cat.Children, cat.Name, IndicatorEntityType.Process);

        //    ProcessRepository prepo = new ProcessRepository();
        //    ProcessBase saes = prepo.Create(9);
        //    foreach (var i in saes.ProcessType.Indicators)
        //        indicators.Add(ReportRepository.CreateReportIndicator(saes.ProcessType.Id, i));
        //    AddInds(table, indicators, saes.ProcessType.TypeName, IndicatorEntityType.Process);

        //    using (ExcelPackage pck = new ExcelPackage())
        //    {
        //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
        //        ws.Cells["A1"].LoadFromDataTable(table, true);
        //        File.WriteAllBytes(string.Format(@"C:\Users\Eric\Desktop\Iota Ink\AllIndicatorRules_{0}.xlsx", DateTime.Now.ToString("yyyyMMdd")), pck.GetAsByteArray());
        //    }
        //}

        //[TestMethod]
        //public void CanCreateIndicatorOptionUpdateForm()
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add(new DataColumn("Indicator Id"));
        //    table.Columns.Add(new DataColumn("Type Id"));
        //    table.Columns.Add(new DataColumn("Type Name"));
        //    table.Columns.Add(new DataColumn("Form Name"));
        //    table.Columns.Add(new DataColumn("Indicator Name"));
        //    table.Columns.Add(new DataColumn("Indicator Option ID"));
        //    table.Columns.Add(new DataColumn("Indicator Option"));
        //    table.Columns.Add(new DataColumn("Weighted Ranking"));
        //    ReportRepository repo = new ReportRepository();
        //    List<ReportIndicator> indicators = repo.GetDiseaseDistroIndicators();
        //    foreach (var cmpc in indicators)
        //        foreach (var cat in cmpc.Children)
        //            AddOptions(table, cat.Children.Where(x => x.DataTypeId == (int)IndicatorDataType.Dropdown), cat.Name, IndicatorEntityType.DiseaseDistribution);
        //    indicators = repo.GetSurveyIndicators();
        //    foreach (var cmpc in indicators)
        //        foreach (var cat in cmpc.Children)
        //            AddOptions(table, cat.Children.Where(x => x.DataTypeId == (int)IndicatorDataType.Dropdown), cat.Name, IndicatorEntityType.Survey);
        //    indicators = repo.GetIntvIndicators();
        //    foreach (var cmpc in indicators)
        //        foreach (var cat in cmpc.Children)
        //            AddOptions(table, cat.Children.Where(x => x.DataTypeId == (int)IndicatorDataType.Dropdown), cat.Name, IndicatorEntityType.Intervention);
        //    indicators = repo.GetProcessIndicators();
        //    foreach (var cmpc in indicators)
        //        foreach (var cat in cmpc.Children)
        //            AddOptions(table, cat.Children.Where(x => x.DataTypeId == (int)IndicatorDataType.Dropdown), cat.Name, IndicatorEntityType.Process);

        //    ProcessRepository prepo = new ProcessRepository();
        //    ProcessBase saes = prepo.Create(9);
        //    foreach (var i in saes.ProcessType.Indicators)
        //        indicators.Add(ReportRepository.CreateReportIndicator(saes.ProcessType.Id, i));
        //    AddOptions(table, indicators.Where(x => x.DataTypeId == (int)IndicatorDataType.Dropdown), saes.ProcessType.TypeName, IndicatorEntityType.Process);

        //    using (ExcelPackage pck = new ExcelPackage())
        //    {
        //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
        //        ws.Cells["A1"].LoadFromDataTable(table, true);
        //        File.WriteAllBytes(string.Format(@"C:\Users\Eric\Desktop\Iota Ink\IndicatorDropdownWeightedRules_{0}.xlsx", DateTime.Now.ToString("yyyyMMdd")), pck.GetAsByteArray());
        //    }
        //}

        #region Extra utilities
        //[TestMethod]
        //public void CanCreateAggregationUpdateForm()
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add(new DataColumn("Indicator Id"));
        //    table.Columns.Add(new DataColumn("Type Id"));
        //    table.Columns.Add(new DataColumn("Type Name"));
        //    table.Columns.Add(new DataColumn("Form Name"));
        //    table.Columns.Add(new DataColumn("Indicator Name"));
        //    table.Columns.Add(new DataColumn("Indicator Type"));
        //    table.Columns.Add(new DataColumn("Aggregation Rule"));
        //    ReportRepository repo = new ReportRepository();

        //    List<ReportIndicator> indicators = new List<ReportIndicator>();
        //    indicators = repo.GetDiseaseDistroIndicators();
        //    foreach (var cmpc in indicators)
        //        foreach (var cat in cmpc.Children)
        //            AddInds(table, cat.Children, cat.Name, IndicatorEntityType.DiseaseDistribution);
        //    indicators = repo.GetSurveyIndicators();
        //    foreach (var cmpc in indicators)
        //        foreach (var cat in cmpc.Children)
        //            AddInds(table, cat.Children, cat.Name, IndicatorEntityType.Survey);
        //    indicators = repo.GetIntvIndicators();
        //    foreach (var cmpc in indicators)
        //        foreach (var cat in cmpc.Children)
        //            AddInds(table, cat.Children, cat.Name, IndicatorEntityType.Intervention);
        //    indicators = repo.GetProcessIndicators().Where(i => i.TypeId == 9).ToList();
        //    foreach (var cmpc in indicators)
        //        foreach (var cat in cmpc.Children)
        //            AddInds(table, cat.Children, cat.Name, IndicatorEntityType.Process);

        //    using (ExcelPackage pck = new ExcelPackage())
        //    {
        //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
        //        ws.Cells["A1"].LoadFromDataTable(table, true);
        //        File.WriteAllBytes("C:\\IndicatorAggregationRules.xlsx", pck.GetAsByteArray());
        //    }
        //}
        
        //[TestMethod]
        //public void CanCreateSaeUpdateForm()
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add(new DataColumn("Indicator Id"));
        //    table.Columns.Add(new DataColumn("Type Id"));
        //    table.Columns.Add(new DataColumn("Type Name"));
        //    table.Columns.Add(new DataColumn("Form Name"));
        //    table.Columns.Add(new DataColumn("Indicator Name"));
        //    table.Columns.Add(new DataColumn("Rule"));
        //    ProcessRepository repo = new ProcessRepository();

        //    List<ReportIndicator> indicators = new List<ReportIndicator>();
            
        //    ProcessBase saes = repo.Create(9);
        //    foreach (var i in saes.ProcessType.Indicators)
        //        indicators.Add(ReportRepository.CreateReportIndicator(saes.ProcessType.Id, i));

        //    AddInds(table, indicators, saes.ProcessType.TypeName, IndicatorEntityType.Process);

        //    using (ExcelPackage pck = new ExcelPackage())
        //    {
        //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
        //        ws.Cells["A1"].LoadFromDataTable(table, true);
        //        File.WriteAllBytes("C:\\SAE_SplittingIndicatorRules.xlsx", pck.GetAsByteArray());
        //    }
        //}

        //[TestMethod]
        //public void CanCreateSaeOptionUpdateForm()
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add(new DataColumn("Indicator Id"));
        //    table.Columns.Add(new DataColumn("Type Id"));
        //    table.Columns.Add(new DataColumn("Type Name"));
        //    table.Columns.Add(new DataColumn("Form Name"));
        //    table.Columns.Add(new DataColumn("Indicator Name"));
        //    table.Columns.Add(new DataColumn("Indicator Option ID"));
        //    table.Columns.Add(new DataColumn("Indicator Option"));
        //    table.Columns.Add(new DataColumn("Weighted Rankings"));
                
        //    ProcessRepository repo = new ProcessRepository();
        //    List<ReportIndicator> indicators = new List<ReportIndicator>();
        //    ProcessBase saes = repo.Create(9);
        //    foreach (var i in saes.ProcessType.Indicators)
        //        indicators.Add(ReportRepository.CreateReportIndicator(saes.ProcessType.Id, i));

        //    AddOptions(table, indicators.Where(x => x.DataTypeId == (int)IndicatorDataType.Dropdown), saes.ProcessType.TypeName, IndicatorEntityType.Process);

        //    using (ExcelPackage pck = new ExcelPackage())
        //    {
        //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
        //        ws.Cells["A1"].LoadFromDataTable(table, true);
        //        File.WriteAllBytes("C:\\SAE_IndicatorDropdownWeightedRules.xlsx", pck.GetAsByteArray());
        //    }
        //}
        
        #endregion

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
