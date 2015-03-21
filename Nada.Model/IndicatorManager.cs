using Nada.Globalization;
using Nada.Model.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using OfficeOpenXml;
using ICSharpCode.SharpZipLib.Zip;
using Nada.Model.Repositories;

namespace Nada.Model
{
    public class IndicatorManager
    {
        protected int validationRow = 1;
        protected string validationSheetName = "ValidationLists";
        protected Dictionary<string, string> validationRanges;

        public void CreateUpdateZip(IndicatorUpdateResult indicatorUpdateResult, string filename)
        {
            // create directory
            string path = Path.GetDirectoryName(filename);
            string tmpDir = path + @"\tmpUpdateZip";
            Directory.CreateDirectory(tmpDir);
            // sql file
            var sb = new StringBuilder();
            foreach (var s in indicatorUpdateResult.SqlStatements)
            {
                sb.AppendLine(s);
            }
            File.WriteAllText(tmpDir + @"\sql.txt", sb.ToString());
            // move original file to zip
            System.IO.File.Copy(indicatorUpdateResult.OriginalFile, tmpDir + @"\source.xlsx", true);
            // create translation update file
            CreateTranslationsFile(indicatorUpdateResult.IndicatorTranslations, tmpDir + @"\translations.xlsx");
            // zip
            FastZip fastZip = new FastZip();
            fastZip.CreateZip(filename, tmpDir, true, null);
            // delete old directory
            System.IO.Directory.Delete(tmpDir, true);
        }

        private void CreateTranslationsFile(List<IndicatorUpdate> indicators, string filename)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Key");
            table.Columns.Add("English");
            table.Columns.Add("French");
            table.Columns.Add("Portuguese");
            table.Columns.Add("Bahasa");
            foreach (var ind in indicators)
            {
                var dr = table.NewRow();
                dr["Key"] = ind.Key;
                dr["English"] = ind.English;
                dr["French"] = ind.French;
                dr["Portuguese"] = ind.Portuguese;
                dr["Bahasa"] = ind.Bahasa;
                table.Rows.Add(dr);
            }

            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                ws.Cells["A1"].LoadFromDataTable(table, true);
                File.WriteAllBytes(filename, pck.GetAsByteArray());
            }
        }

        public void CreateImportFile(IndicatorManagerOptions indicatorManagerOptions, string filename)
        {
            ImporterBase importer = new ImporterBase();

            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook xlsWorkbook;
            Microsoft.Office.Interop.Excel.Workbooks xlsWorkbooks;
            Microsoft.Office.Interop.Excel.Sheets xlsWorksheets;
            Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet;
            Microsoft.Office.Interop.Excel.Worksheet xlsValidation;
            object oMissing = System.Reflection.Missing.Value;
            validationRanges = new Dictionary<string, string>();

            //Create new workbook
            xlsWorkbooks = xlsApp.Workbooks;
            xlsWorkbook = xlsWorkbooks.Add(true);
            xlsWorksheets = xlsWorkbook.Worksheets;

            //Get the first worksheet
            xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)(xlsWorkbook.Worksheets[1]);

            // add hidden validation worksheet
            xlsValidation = (Microsoft.Office.Interop.Excel.Worksheet)xlsWorksheets.Add(oMissing, xlsWorksheet, oMissing, oMissing);
            xlsValidation.Name = validationSheetName;
            xlsValidation.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden;

            // row 1 column headers
            xlsWorksheet.Cells[1, 1] = "Indicator ID";
            xlsWorksheet.Cells[1, 2] = "Type ID";
            xlsWorksheet.Cells[1, 3] = "Type Name";
            xlsWorksheet.Cells[1, 4] = "Form Name"; // Drop down
            xlsWorksheet.Cells[1, 5] = "Indicator Name English";
            xlsWorksheet.Cells[1, 6] = "Indicator Name French";
            xlsWorksheet.Cells[1, 7] = "Indicator Name Portuguese";
            xlsWorksheet.Cells[1, 8] = "Indicator Name Bahasa";
            xlsWorksheet.Cells[1, 9] = "Indicator Type"; // Drop down
            xlsWorksheet.Cells[1, 10] = "Is Required";
            xlsWorksheet.Cells[1, 11] = "Aggregation Rule"; // Drop down
            xlsWorksheet.Cells[1, 12] = "Merge Rule"; // Drop down
            xlsWorksheet.Cells[1, 13] = "Split Rule"; // Drop down
            xlsWorksheet.Cells[1, 14] = "Sort Order"; 

            // row 2+ indicators
            int xlsRowCount = 2;
            foreach (var ind in indicatorManagerOptions.SelectedIndicators)
            {
                xlsWorksheet.Cells[xlsRowCount, 1] = ind.ID;
                xlsWorksheet.Cells[xlsRowCount, 2] = (int)indicatorManagerOptions.EntityType;
                xlsWorksheet.Cells[xlsRowCount, 3] = indicatorManagerOptions.EntityType.ToString();
                importer.AddDataValidation(xlsWorksheet, xlsValidation, Util.GetExcelColumnName(4), xlsRowCount, "", "", indicatorManagerOptions.FormTypes, System.Threading.Thread.CurrentThread.CurrentCulture);
                xlsWorksheet.Cells[xlsRowCount, 4] = ind.FormName;

                var english = new TranslationLookupInstance(new CultureInfo("en-US"));
                xlsWorksheet.Cells[xlsRowCount, 5] = english.GetValue(ind.Key, ind.Key);
                var french = new TranslationLookupInstance(new CultureInfo("fr-FR"));
                xlsWorksheet.Cells[xlsRowCount, 6] = french.GetValue(ind.Key, ind.Key);
                var port = new TranslationLookupInstance(new CultureInfo("pt-PT"));
                xlsWorksheet.Cells[xlsRowCount, 7] = port.GetValue(ind.Key, ind.Key);
                var bahasa = new TranslationLookupInstance(new CultureInfo("id-ID"));
                xlsWorksheet.Cells[xlsRowCount, 8] = bahasa.GetValue(ind.Key, ind.Key);

                importer.AddDataValidation(xlsWorksheet, xlsValidation, Util.GetExcelColumnName(9), xlsRowCount, "", "", Enum.GetNames(typeof(IndicatorDataType)).ToList(), System.Threading.Thread.CurrentThread.CurrentCulture);
                xlsWorksheet.Cells[xlsRowCount, 9] = ((IndicatorDataType)ind.DataTypeId).ToString();
                xlsWorksheet.Cells[xlsRowCount, 10] = ind.IsRequired.ToString();
                importer.AddDataValidation(xlsWorksheet, xlsValidation, Util.GetExcelColumnName(11), xlsRowCount, "", "", Enum.GetNames(typeof(IndicatorAggType)).ToList(), System.Threading.Thread.CurrentThread.CurrentCulture);
                xlsWorksheet.Cells[xlsRowCount, 11] = ((IndicatorAggType)ind.AggregationRuleId).ToString();
                importer.AddDataValidation(xlsWorksheet, xlsValidation, Util.GetExcelColumnName(12), xlsRowCount, "", "", Enum.GetNames(typeof(MergingRule)).ToList(), System.Threading.Thread.CurrentThread.CurrentCulture);
                xlsWorksheet.Cells[xlsRowCount, 12] = ((MergingRule)ind.MergeRule).ToString();
                importer.AddDataValidation(xlsWorksheet, xlsValidation, Util.GetExcelColumnName(13), xlsRowCount, "", "", Enum.GetNames(typeof(RedistrictingRule)).ToList(), System.Threading.Thread.CurrentThread.CurrentCulture);
                xlsWorksheet.Cells[xlsRowCount, 13] = ((RedistrictingRule)ind.SplitRule).ToString();
                xlsWorksheet.Cells[xlsRowCount, 14] = ind.SortOrder;
                xlsRowCount++;
            }

            // Auto fit
            var last = xlsWorksheet.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            var range = xlsWorksheet.get_Range("A1", last);
            range.Columns.AutoFit();

            // Save and display
            xlsApp.DisplayAlerts = false;
            xlsWorkbook.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, oMissing,
                oMissing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true,
                oMissing, oMissing, oMissing);
            xlsApp.Visible = true;
            Marshal.ReleaseComObject(xlsWorksheets);
            Marshal.ReleaseComObject(xlsWorksheet);
            Marshal.ReleaseComObject(xlsValidation);
            Marshal.ReleaseComObject(xlsWorkbooks);
            Marshal.ReleaseComObject(xlsWorkbook);
            Marshal.ReleaseComObject(xlsApp);
        }

        public IndicatorUpdateResult ImportData(string filename)
        {
            try
            {
                DataSet ds = ImporterBase.LoadDataFromFile(filename);

                if (ds.Tables.Count == 0)
                    return new IndicatorUpdateResult(TranslationLookup.GetValue("NoDataFound"));

                return CreateSqlForFile(ds, filename);
            }
            catch (Exception ex)
            {
                return new IndicatorUpdateResult(TranslationLookup.GetValue("UnexpectedException") + ex.Message);
            }
        }

        private IndicatorUpdateResult CreateSqlForFile(DataSet ds, string filename)
        {
            string errorMessage = "";
            List<string> sqlStatements = new List<string>();
            List<IndicatorUpdate> inds = new List<IndicatorUpdate>();
            bool transWasStarted = false;
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    string sql = "BEGIN TRANSACTION";
                    OleDbCommand command = new OleDbCommand(sql, connection);
                    command.ExecuteNonQuery();
                    transWasStarted = true;
                    int rowCount = 1;
                    List<KeyValuePair<string, int>> formTypes = new List<KeyValuePair<string,int>>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (row[0] == "Indicator ID")
                            continue;

                        if (formTypes.Count() == 0)
                            formTypes = GetFormTypes(row);

                        string rowerrors = "";

                        var indicator = ParseAndValidate(row, ref rowerrors, formTypes);
                        if (rowerrors.Length > 0)
                            errorMessage += Environment.NewLine + string.Format(TranslationLookup.GetValue("ImportErrors"), "Row # " + rowCount) + Environment.NewLine + "--------" + Environment.NewLine + rowerrors;
                        else
                        {
                            inds.Add(indicator);
                            string tableName = GetTableName(indicator);
                            try
                            {
                                if(indicator.Id > 0) 
                                   DoUpdate(indicator, sqlStatements, connection, tableName);
                                else 
                                    DoInsert(indicator, sqlStatements, connection, tableName);
                            }
                            catch (Exception ex)
                            {
                                errorMessage += Environment.NewLine + "SQL Exception Row # " + rowCount + Environment.NewLine + "--------" + Environment.NewLine + ex.Message;
                            }
                        }

                        rowCount++;
                    }


                    // COMMIT TRANS
                    OleDbCommand cmd = new OleDbCommand("ROLLBACK TRANSACTION", connection);
                    cmd.ExecuteNonQuery();
                    transWasStarted = false;
                }
                catch (Exception)
                {
                    if (transWasStarted)
                    {
                        try
                        {
                            OleDbCommand cmd = new OleDbCommand("ROLLBACK TRANSACTION", connection);
                            cmd.ExecuteNonQuery();
                        }
                        catch { }
                    }
                    throw;
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
                return new IndicatorUpdateResult(CreateErrorMessage(errorMessage));

            return new IndicatorUpdateResult
            {
                WasSuccess = true,
                SqlStatements = sqlStatements,
                IndicatorTranslations = inds,
                OriginalFile = filename,
                Message = "Indicator changes were successful. Please click finish to download a zip file of the changes."
            };
        }

        private void DoInsert(IndicatorUpdate ind, List<string> sqlStatements, OleDbConnection connection, string tableName)
        {
            string translationKey = Guid.NewGuid().ToString();
            ind.Key = translationKey;
            string insertSql = "";

            switch (ind.EntityType)
            {
                    // 
                case IndicatorEntityType.DiseaseDistribution:
                    insertSql = string.Format("insert into DiseaseDistributionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId, RedistrictRuleId, MergeRuleId) values ({0}, {1}, {2}, {3}, 26, NOW(), 0, 0, {4}, 0, 0, 0, 0, {5}, {6}, {7});;",
                        ind.DataTypeId, ind.Key, ind.AggTypeId, ind.SortOrder, ind.IsRequired ? -1 : 0, ind.FormId, ind.SplitRuleId, ind.MergeRuleId);
                    break;
                case IndicatorEntityType.Intervention:
                    insertSql = string.Format("insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId) values ({0}, {1}, {2}, {3}, 26, NOW(), 0, 0, {4}, 0, 0, 0, 0, {5}, {6}, {7});;",
                        ind.DataTypeId, ind.Key, ind.AggTypeId, ind.SortOrder, ind.IsRequired ? -1 : 0, ind.SplitRuleId, ind.MergeRuleId);
                    break;
                case IndicatorEntityType.Survey:
                    insertSql = string.Format("insert into SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId, RedistrictRuleId, MergeRuleId) values ({0}, {1}, {2}, {3}, 26, NOW(), 0, 0, {4}, 0, 0, 0, 0, {5}, {6}, {7});;",
                        ind.DataTypeId, ind.Key, ind.AggTypeId, ind.SortOrder, ind.IsRequired ? -1 : 0, ind.FormId, ind.SplitRuleId, ind.MergeRuleId);
                    break;
                case IndicatorEntityType.Process:
                    insertSql = string.Format("insert into ProcessIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, ProcessTypeId, RedistrictRuleId, MergeRuleId) values ({0}, {1}, {2}, {3}, 26, NOW(), 0, 0, {4}, 0, 0, 0, 0, {5}, {6}, {7});;",
                        ind.DataTypeId, ind.Key, ind.AggTypeId, ind.SortOrder, ind.IsRequired ? -1 : 0, ind.FormId, ind.SplitRuleId, ind.MergeRuleId);
                    break;
            }

            if (string.IsNullOrEmpty(insertSql))
                throw new Exception("Invalid indicator type for ID# " + ind.Id);

            OleDbCommand updateCmd = new OleDbCommand(insertSql, connection);
            updateCmd.ExecuteNonQuery();
            sqlStatements.Add(insertSql);

            if(ind.EntityType == IndicatorEntityType.Intervention)
            {
                string joinToForm = string.Format("insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT {0}, ID FROM interventionindicators where displayname = {1};", ind.FormId, ind.Key);
                OleDbCommand joinCmd = new OleDbCommand(joinToForm, connection);
                updateCmd.ExecuteNonQuery();
                sqlStatements.Add(insertSql);
            }
        }

        private void DoUpdate(IndicatorUpdate ind, List<string> sqlStatements, OleDbConnection connection, string tableName)
        {
            string translationKey = GetTranslationKey(ind, tableName, connection);
            ind.Key = translationKey;
            string updateSql = "";

            switch (ind.EntityType)
            {
                case IndicatorEntityType.DiseaseDistribution:
                    updateSql = string.Format("UPDATE DiseaseDistributionIndicators set AggTypeId={1}, RedistrictRuleId={2}, MergeRuleId={3}, IsRequired={4}, SortOrder={5} where displayname = '{6}' and DiseaseId={0};",
                        ind.FormId, ind.AggTypeId, ind.SplitRuleId, ind.MergeRuleId, ind.IsRequired ? -1 : 0, ind.SortOrder, translationKey);
                    break;
                case IndicatorEntityType.Intervention:
                    updateSql = string.Format("UPDATE InterventionIndicators set AggTypeId={0}, RedistrictRuleId={1}, MergeRuleId={2}, IsRequired={3}, SortOrder={4} where displayname = '{5}';",
                       ind.AggTypeId, ind.SplitRuleId, ind.MergeRuleId, ind.IsRequired ? -1 : 0, ind.SortOrder, translationKey);
                    break;
                case IndicatorEntityType.Survey:
                    updateSql = string.Format("UPDATE SurveyIndicators set AggTypeId={1}, RedistrictRuleId={2}, MergeRuleId={3}, IsRequired={4}, SortOrder={5} where displayname = '{6}' and SurveyTypeId={0};",
                        ind.FormId, ind.AggTypeId, ind.SplitRuleId, ind.MergeRuleId, ind.IsRequired ? -1 : 0, ind.SortOrder, translationKey);
                    break;
                case IndicatorEntityType.Process:
                    updateSql = string.Format("UPDATE ProcessIndicators set AggTypeId={1}, RedistrictRuleId={2}, MergeRuleId={3}, IsRequired={4}, SortOrder={5} where displayname = '{6}' and ProcessTypeId={0};",
                        ind.FormId, ind.AggTypeId, ind.SplitRuleId, ind.MergeRuleId, ind.IsRequired ? -1 : 0, ind.SortOrder, translationKey);
                    break;
            }

            if (string.IsNullOrEmpty(updateSql))
                throw new Exception("Invalid indicator type for ID# " + ind.Id);

            OleDbCommand updateCmd = new OleDbCommand(updateSql, connection);
            updateCmd.ExecuteNonQuery();
            sqlStatements.Add(updateSql);
        }

        private string GetTranslationKey(IndicatorUpdate indicator, string tableName, OleDbConnection connection)
        {
            string translationKey = "";
            OleDbCommand command = new OleDbCommand("Select DisplayName FROM " + tableName + " WHERE ID=" + indicator.Id + ";", connection);
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    translationKey = reader.GetValueOrDefault<string>("DisplayName");
                }
                reader.Close();
            }

            if (string.IsNullOrEmpty(translationKey))
                throw new Exception("Error could not find translation key for indicator ID " + indicator.Id);

            return translationKey;
        }

        private string GetTableName(IndicatorUpdate indicator)
        {
            switch (indicator.EntityType)
            {
                case IndicatorEntityType.DiseaseDistribution:
                    return "DiseaseDistributionIndicators";
                case IndicatorEntityType.Intervention:
                    return "InterventionIndicators";
                case IndicatorEntityType.Survey:
                    return "SurveyIndicators";
                case IndicatorEntityType.Process:
                    return "ProcessIndicators";
                default:
                    return "ErrorTableName";
            }
        }

        private List<KeyValuePair<string, int>> GetFormTypes(DataRow dr)
        {
            var result = new List<KeyValuePair<string, int>>();
            if (!string.IsNullOrEmpty(dr["Type ID"].ToString()))
            {
                var entityType = (IndicatorEntityType)Convert.ToInt32(dr["Type ID"]);

                switch (entityType)
                {
                    case IndicatorEntityType.DiseaseDistribution:
                        DiseaseRepository typeRepo = new DiseaseRepository();
                        result = typeRepo.GetSelectedDiseases().Select(d => new KeyValuePair<string, int>(d.DisplayName, d.Id)).ToList();
                        break;
                    case IndicatorEntityType.Intervention:
                        IntvRepository intv = new IntvRepository();
                        result = intv.GetAllTypes().Select(d => new KeyValuePair<string, int>(d.IntvTypeName, d.Id)).ToList();
                        break;
                    case IndicatorEntityType.Survey:
                        SurveyRepository sRepo = new SurveyRepository();
                        result = sRepo.GetSurveyTypes().Select(d => new KeyValuePair<string, int>(d.SurveyTypeName, d.Id)).ToList();
                        break;
                    case IndicatorEntityType.Process:
                        ProcessRepository pRepo = new ProcessRepository();
                        result = pRepo.GetProcessTypes().Select(d => new KeyValuePair<string, int>(d.TypeName, d.Id)).ToList();
                        break;
                }        
            }
            return result;
        }

        private string CreateErrorMessage(string errorMessage)
        {
            return TranslationLookup.GetValue("ImportErrorHeader") + Environment.NewLine + "--------" + Environment.NewLine + errorMessage;
        }

        private IndicatorUpdate ParseAndValidate(DataRow dr, ref string error, List<KeyValuePair<string, int>> formTypes)
        {
            IndicatorUpdate indicator = new IndicatorUpdate();
            int indicatorId = 0;
            if (!string.IsNullOrEmpty(dr["Indicator Id"].ToString()))
                if (!int.TryParse(dr["Indicator ID"].ToString(), out indicatorId))
                    error += "Indicator ID: " + TranslationLookup.GetValue("MustBeNumber") + Environment.NewLine;
                else
                    indicator.Id = indicatorId;

            if (string.IsNullOrEmpty(dr["Type ID"].ToString()))
                error += "Type ID: " + TranslationLookup.GetValue("IsRequired") + Environment.NewLine;
            else
                indicator.EntityType = (IndicatorEntityType)Convert.ToInt32(dr["Type ID"]);

            if (string.IsNullOrEmpty(dr["Sort Order"].ToString()))
                error += "Sort Order: " + TranslationLookup.GetValue("IsRequired") + Environment.NewLine;
            else
                indicator.SortOrder = Convert.ToInt32(dr["Sort Order"]);

            // Enum Drop downs
            if (string.IsNullOrEmpty(dr["Aggregation Rule"].ToString()))
                error += "Aggregation Rule: " + TranslationLookup.GetValue("IsRequired") + Environment.NewLine;
            else
                indicator.AggTypeId = (int)Enum.Parse(typeof(IndicatorAggType), dr["Aggregation Rule"].ToString());

            if (string.IsNullOrEmpty(dr["Merge Rule"].ToString()))
                error += "Merge Rule: " + TranslationLookup.GetValue("IsRequired") + Environment.NewLine;
            else
                indicator.MergeRuleId = (int)Enum.Parse(typeof(MergingRule), dr["Merge Rule"].ToString());

            if (string.IsNullOrEmpty(dr["Split Rule"].ToString()))
                error += "Split Rule: " + TranslationLookup.GetValue("IsRequired") + Environment.NewLine;
            else
                indicator.SplitRuleId = (int)Enum.Parse(typeof(RedistrictingRule), dr["Split Rule"].ToString());

            if (string.IsNullOrEmpty(dr["Indicator Type"].ToString()))
                error += "Indicator Type: " + TranslationLookup.GetValue("IsRequired") + Environment.NewLine;
            else
                indicator.DataTypeId = (int)Enum.Parse(typeof(IndicatorDataType), dr["Indicator Type"].ToString());

            // TRANSLATIONS
            if (string.IsNullOrEmpty(dr["Indicator Name English"].ToString()))
                error += "Indicator Name English: " + TranslationLookup.GetValue("IsRequired") + Environment.NewLine;
            else
                indicator.English = dr["Indicator Name English"].ToString();

            if (string.IsNullOrEmpty(dr["Indicator Name French"].ToString()))
                error += "Indicator Name French: " + TranslationLookup.GetValue("IsRequired") + Environment.NewLine;
            else
                indicator.French = dr["Indicator Name French"].ToString();
            if (string.IsNullOrEmpty(dr["Indicator Name Portuguese"].ToString()))
                error += "Indicator Name Portuguese: " + TranslationLookup.GetValue("IsRequired") + Environment.NewLine;
            else
                indicator.Portuguese = dr["Indicator Name Portuguese"].ToString();
            if (string.IsNullOrEmpty(dr["Indicator Name Bahasa"].ToString()))
                error += "Indicator Name Bahasa: " + TranslationLookup.GetValue("IsRequired") + Environment.NewLine;
            else
                indicator.Bahasa = dr["Indicator Name Bahasa"].ToString();

            // Is Required
            if (string.IsNullOrEmpty(dr["Is Required"].ToString()))
                error += "Is Required: " + TranslationLookup.GetValue("IsRequired") + Environment.NewLine;
            else
            {
                bool isRequired = true;

                string val = dr["Is Required"].ToString();
                if (dr["Is Required"].ToString().ToLower() == "true")
                    isRequired = true;
                else if (dr["Is Required"].ToString().ToLower() == "false")
                    isRequired = false;
                else
                    error += "Is Required: " + "Must be TRUE or FALSE " + Environment.NewLine;
            
                indicator.IsRequired = isRequired;
            }

            // Form ID
            if (indicator.Id > 0 && indicator.EntityType == IndicatorEntityType.Intervention)
            {
                indicator.FormId = 0; // not necessary for updates and interventions
            }
            else if (string.IsNullOrEmpty(dr["Form Name"].ToString()))
            {
                error += "Form Name: " + TranslationLookup.GetValue("IsRequired") + Environment.NewLine;
            }
            else
            {
                string val = dr["Form Name"].ToString();
                KeyValuePair<string, int>? form = formTypes.FirstOrDefault(f => f.Key == val);
                if (form.HasValue)
                    indicator.FormId = form.Value.Value;
                else
                    error += "Form Name: " + "Must be a value from the drop down list " + Environment.NewLine;
            }
            
            return indicator;
        }
    }

    public class IndicatorManagerOptions
    {
        public IndicatorManagerOptions()
        {
            AvailableIndicators = new List<ReportIndicator>();
            SelectedIndicators = new List<ReportIndicator>();
            Columns = new Dictionary<string, AggregateIndicator>();
        }
        public List<ReportIndicator> AvailableIndicators { get; set; }
        public List<ReportIndicator> SelectedIndicators { get; set; }
        public Dictionary<string, AggregateIndicator> Columns { get; set; }
        public IndicatorEntityType EntityType { get; set; }
        public List<string> FormTypes { get; set; }

    }

    public class IndicatorUpdate
    {
        public int Id { get; set; }
        public IndicatorEntityType EntityType { get; set; }
        public int FormId { get; set; }
        public int DataTypeId { get; set; }
        public bool IsRequired { get; set; }
        public int AggTypeId { get; set; }
        public int MergeRuleId { get; set; }
        public int SplitRuleId { get; set; }
        public int SortOrder { get; set; }
        public string Key { get; set; }
        public string English { get; set; }
        public string French { get; set; }
        public string Portuguese { get; set; }
        public string Bahasa { get; set; }
    }

    public class IndicatorUpdateResult
    {
        public IndicatorUpdateResult()
        {

        }
        public IndicatorUpdateResult(string error)
        {
            WasSuccess = false;
            Message = error;
        }
        public bool WasSuccess { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
        public List<string> SqlStatements { get; set; }
        public List<IndicatorUpdate> IndicatorTranslations { get; set; }
        public string OriginalFile { get; set; }
    }
}
