﻿using System;
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
            table.Columns.Add(new DataColumn("Rule"));
            ReportRepository repo = new ReportRepository();
            AddInds(table, repo.GetDemographyIndicators(), "Demography", IndicatorEntityType.Demo);

            List<ReportIndicator> indicators = repo.GetDiseaseDistroIndicators();
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

            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                ws.Cells["A1"].LoadFromDataTable(table, true);
                File.WriteAllBytes("C:\\SplittingIndicatorRules.xlsx", pck.GetAsByteArray());
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