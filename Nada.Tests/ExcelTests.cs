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

namespace Nada.Tests
{
    [TestClass]
    public class ExcelTests : BaseTest
    {
        [TestMethod]
        public void CanCreateIndicatorUpdateForm()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Indicator Id"));
            table.Columns.Add(new DataColumn("Type Id"));
            table.Columns.Add(new DataColumn("Type Name"));
            table.Columns.Add(new DataColumn("Form Name"));
            table.Columns.Add(new DataColumn("Indicator Name"));
            table.Columns.Add(new DataColumn("Indicator Type"));
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
                File.WriteAllBytes("C:\\AllIndicatorRules.xlsx", pck.GetAsByteArray());
            }
        }

        [TestMethod]
        public void CanCreateAggregationUpdateForm()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Indicator Id"));
            table.Columns.Add(new DataColumn("Type Id"));
            table.Columns.Add(new DataColumn("Type Name"));
            table.Columns.Add(new DataColumn("Form Name"));
            table.Columns.Add(new DataColumn("Indicator Name"));
            table.Columns.Add(new DataColumn("Indicator Type"));
            table.Columns.Add(new DataColumn("Aggregation Rule"));
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
            indicators = repo.GetProcessIndicators().Where(i => i.TypeId == 9).ToList();
            foreach (var cmpc in indicators)
                foreach (var cat in cmpc.Children)
                    AddInds(table, cat.Children, cat.Name, IndicatorEntityType.Process);

            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                ws.Cells["A1"].LoadFromDataTable(table, true);
                File.WriteAllBytes("C:\\IndicatorAggregationRules.xlsx", pck.GetAsByteArray());
            }
        }
        
        [TestMethod]
        public void CanCreateSaeUpdateForm()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Indicator Id"));
            table.Columns.Add(new DataColumn("Type Id"));
            table.Columns.Add(new DataColumn("Type Name"));
            table.Columns.Add(new DataColumn("Form Name"));
            table.Columns.Add(new DataColumn("Indicator Name"));
            table.Columns.Add(new DataColumn("Rule"));
            ProcessRepository repo = new ProcessRepository();

            List<ReportIndicator> indicators = new List<ReportIndicator>();
            
            ProcessBase saes = repo.Create(9);
            foreach (var i in saes.ProcessType.Indicators)
                indicators.Add(ReportRepository.CreateReportIndicator(saes.ProcessType.Id, i));

            AddInds(table, indicators, saes.ProcessType.TypeName, IndicatorEntityType.Process);

            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                ws.Cells["A1"].LoadFromDataTable(table, true);
                File.WriteAllBytes("C:\\SAE_SplittingIndicatorRules.xlsx", pck.GetAsByteArray());
            }
        }

        [TestMethod]
        public void CanCreateSaeOptionUpdateForm()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Indicator Id"));
            table.Columns.Add(new DataColumn("Type Id"));
            table.Columns.Add(new DataColumn("Type Name"));
            table.Columns.Add(new DataColumn("Form Name"));
            table.Columns.Add(new DataColumn("Indicator Name"));
            table.Columns.Add(new DataColumn("Indicator Option ID"));
            table.Columns.Add(new DataColumn("Indicator Option"));
            table.Columns.Add(new DataColumn("Weighted Rankings"));
                
            ProcessRepository repo = new ProcessRepository();
            List<ReportIndicator> indicators = new List<ReportIndicator>();
            ProcessBase saes = repo.Create(9);
            foreach (var i in saes.ProcessType.Indicators)
                indicators.Add(ReportRepository.CreateReportIndicator(saes.ProcessType.Id, i));

            AddOptions(table, indicators.Where(x => x.DataTypeId == (int)IndicatorDataType.Dropdown), saes.ProcessType.TypeName, IndicatorEntityType.Process);

            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                ws.Cells["A1"].LoadFromDataTable(table, true);
                File.WriteAllBytes("C:\\SAE_IndicatorDropdownWeightedRules.xlsx", pck.GetAsByteArray());
            }
        }

        [TestMethod]
        public void CanCreateIndicatorOptionUpdateForm()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Indicator Id"));
            table.Columns.Add(new DataColumn("Type Id"));
            table.Columns.Add(new DataColumn("Type Name"));
            table.Columns.Add(new DataColumn("Form Name"));
            table.Columns.Add(new DataColumn("Indicator Name"));
            table.Columns.Add(new DataColumn("Indicator Option ID"));
            table.Columns.Add(new DataColumn("Indicator Option"));
            table.Columns.Add(new DataColumn("Weighted Rankings"));
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

            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                ws.Cells["A1"].LoadFromDataTable(table, true);
                File.WriteAllBytes("C:\\IndicatorDropdownWeightedRules.xlsx", pck.GetAsByteArray());
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
                    var dr = table.NewRow();
                    dr["Indicator Id"] = ind.ID;
                    dr["Type Id"] = (int)type;
                    dr["Type Name"] = type.ToString();
                    dr["Form Name"] = formName;
                    dr["Indicator Name"] = ind.Name;
                    dr["Indicator Option ID"] = val.Id;
                    dr["Indicator Option"] = val.DisplayName;
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
