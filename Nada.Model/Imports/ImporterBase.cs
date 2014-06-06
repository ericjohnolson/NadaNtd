using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Excel;
using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Imports;
using Nada.Model.Repositories;

namespace Nada.Model
{
    public class ImporterBase : IImporter
    {
        public ImporterBase()
        {
        }

        public virtual IndicatorEntityType EntityType { get { return IndicatorEntityType.Intervention; } }
        public Dictionary<string, Indicator> Indicators { get; set; }
        public List<IndicatorDropdownValue> DropDownValues { get; set; }
        protected Dictionary<string, Indicator> translatedIndicators = new Dictionary<string, Indicator>();
        protected List<Partner> partners = new List<Partner>();
        protected List<string> selectedDiseases = new List<string>();
        private List<IndicatorDropdownValue> ezs = new List<IndicatorDropdownValue>();
        private List<IndicatorDropdownValue> eus = new List<IndicatorDropdownValue>();
        private List<IndicatorDropdownValue> ess = new List<IndicatorDropdownValue>();
        private List<IndicatorDropdownValue> subdistricts = new List<IndicatorDropdownValue>();
        protected List<MonthItem> months = new List<MonthItem>();
        public virtual string ImportName { get { return ""; } }
        public virtual List<TypeListItem> GetAllTypes() { return new List<TypeListItem>(); }
        protected virtual void SetSpecificType(int id) { }
        private ImportOptions options = new ImportOptions();
        protected int validationRow = 1;
        protected string validationSheetName = "ValidationLists";
        protected Dictionary<string, string> validationRanges;
        protected IndicatorParser valueParser = new IndicatorParser();

        public virtual bool HasGroupedAdminLevels(ImportOptions opts)
        {
            return false;
        }
        public void SetType(int id)
        {
            translatedIndicators = new Dictionary<string, Indicator>();
            SetSpecificType(id);
            if (Indicators == null)
                throw new ArgumentException("Need to override SetSpecificType and set Indicators for import type");

            foreach (var keyValue in Indicators)
                translatedIndicators.Add(TranslationLookup.GetValue(keyValue.Key, keyValue.Key), keyValue.Value);
        }

        protected void LoadRelatedLists()
        {
            IntvRepository repo = new IntvRepository();
            partners = repo.GetPartners();
            months = GlobalizationUtil.GetAllMonths();
            SettingsRepository settings = new SettingsRepository();
            ezs = settings.GetEcologicalZones();
            eus = settings.GetEvaluationUnits();
            subdistricts = settings.GetEvalSubDistricts();
            ess = settings.GetEvalSites();
            DiseaseRepository diseases = new DiseaseRepository();
            selectedDiseases = diseases.GetSelectedDiseases().Select(d => d.DisplayName).ToList();
            valueParser.LoadRelatedLists();
        }
        protected virtual void ReloadDropdownValues()
        {

        }

        public virtual void CreateImportFile(string filename, List<AdminLevel> adminLevels, AdminLevelType adminLevelType, ImportOptions opts)
        {
            options = opts;
            ReloadDropdownValues();
            LoadRelatedLists();
            System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
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
            DemoRepository repo = new DemoRepository();
            List<string> names = repo.GetAdminLevelTypeNames(adminLevelType.Id);

            xlsWorksheet.Cells[1, 1] = TranslationLookup.GetValue("ID");
            for (int i = 0; i < names.Count; i++)
                xlsWorksheet.Cells[1, 2 + i] = names[i];
            int locationCount = names.Count + 1;
            int xlsColCount = names.Count + 1;
            xlsColCount += AddTypeSpecific(xlsWorksheet);
            int colCountAfterStatic = xlsColCount;

            foreach (var item in Indicators)
            {
                if (item.Value.DataTypeId == (int)IndicatorDataType.SentinelSite || item.Value.IsCalculated || item.Value.IsMetaData)
                    continue;
                string isReq = "";
                if (item.Value.IsRequired)
                    isReq = "* ";

                // if the filtered list still has more than 7 possible multiselect values, do some weird shit.
                if (options.IndicatorValuesSublist.ContainsKey(item.Value.DisplayName) && options.IndicatorValuesSublist[item.Value.DisplayName].Count > 6)
                {
                    int optionNumber = 1;
                    foreach (string opt in options.IndicatorValuesSublist[item.Value.DisplayName])
                    {
                        xlsColCount++;
                        xlsWorksheet.Cells[1, xlsColCount] = isReq + TranslationLookup.GetValue(item.Key, item.Key) + Translations.ImportSelectionOption + optionNumber;
                        optionNumber++;
                    }
                }
                else
                {
                    xlsColCount++;
                    xlsWorksheet.Cells[1, xlsColCount] = isReq + TranslationLookup.GetValue(item.Key, item.Key);
                }
            }
            //xlsWorksheet.Cells[1, xlsColCount + 1] = TranslationLookup.GetValue("Notes");

            // row 2+ admin levels
            int xlsRowCount = 2;
            foreach (AdminLevel l in adminLevels)
            {
                xlsWorksheet.Cells[xlsRowCount, 1] = l.Id;
                List<AdminLevel> parents = repo.GetAdminLevelParentNames(l.Id);
                int aCol = 2;
                foreach (AdminLevel adminlevel in parents)
                {
                    xlsWorksheet.Cells[xlsRowCount, aCol] = adminlevel.Name;
                    aCol++;
                }
                AddTypeSpecificLists(xlsWorksheet, xlsValidation, l.Id, xlsRowCount, oldCI, locationCount);
                int colCount = colCountAfterStatic;
                foreach (var key in Indicators.Keys)
                {
                    if (Indicators[key].DataTypeId == (int)IndicatorDataType.SentinelSite || Indicators[key].IsCalculated || Indicators[key].IsMetaData)
                        continue;
                    colCount++;
                    colCount = AddValueToCell(xlsWorksheet, xlsValidation, colCount, xlsRowCount, "", Indicators[key], oldCI, false);
                }
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
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }

        public void CreateUpdateFile(string filename, List<IHaveDynamicIndicatorValues> forms)
        {
            ReloadDropdownValues();
            LoadRelatedLists();
            System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
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

            xlsWorksheet.Cells[1, 1] = TranslationLookup.GetValue("ID");
            xlsWorksheet.Cells[1, 2] = TranslationLookup.GetValue("Location");
            int locationCount = 2;
            int xlsColCount = 2;
            xlsColCount += AddTypeSpecific(xlsWorksheet);
            int colCountAfterStatic = xlsColCount;

            foreach (var item in Indicators)
            {
                if (item.Value.DataTypeId == (int)IndicatorDataType.SentinelSite || item.Value.IsCalculated || item.Value.IsMetaData)
                    continue;
                string isReq = "";
                if (item.Value.IsRequired)
                    isReq = "* ";

                xlsColCount++;
                xlsWorksheet.Cells[1, xlsColCount] = isReq + TranslationLookup.GetValue(item.Key, item.Key);
            }
            //xlsWorksheet.Cells[1, xlsColCount + 1] = TranslationLookup.GetValue("Notes");

            // row 2+ admin levels
            DemoRepository repo = new DemoRepository();
            int xlsRowCount = 2;
            foreach (var form in forms)
            {
                var adminLevel = repo.GetAdminLevelById(form.GetFirstAdminLevelId());
                xlsWorksheet.Cells[xlsRowCount, 1] = form.Id;
                xlsWorksheet.Cells[xlsRowCount, 2] = adminLevel.Name;
                AddTypeSpecificLists(xlsWorksheet, xlsValidation, adminLevel.Id, xlsRowCount, oldCI, locationCount);
                int colCount = colCountAfterStatic;
                foreach (var key in Indicators.Keys)
                {
                    if (Indicators[key].DataTypeId == (int)IndicatorDataType.SentinelSite || Indicators[key].IsCalculated || Indicators[key].IsMetaData)
                        continue;

                    string value = "";
                    var iv = form.IndicatorValues.FirstOrDefault(x => x.IndicatorId == Indicators[key].Id);
                    if (iv != null)
                        value = iv.DynamicValue;
                    colCount++;
                    colCount = AddValueToCell(xlsWorksheet, xlsValidation, colCount, xlsRowCount, value, Indicators[key], oldCI, true);
                }
                xlsWorksheet.Cells[xlsRowCount, colCount] = form.Notes;
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
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }

        protected virtual int FillData(System.Globalization.CultureInfo oldCI, Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, Microsoft.Office.Interop.Excel.Worksheet xlsValidation, int locationCount, int colCountAfterStatic, int xlsRowCount)
        {
            return xlsRowCount;
        }

        protected virtual void AddTypeSpecificLists(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, Microsoft.Office.Interop.Excel.Worksheet xlsValidation,
            int adminLevelId, int r, CultureInfo currentCulture, int colCount)
        {

        }

        protected virtual int AddTypeSpecific(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet)
        {
            return 0;
        }

        protected int AddValueToCell(Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, Microsoft.Office.Interop.Excel.Worksheet validation, int c, int r,
            string value, Indicator indicator, CultureInfo currentCulture, bool isUpdate)
        {
            if (options.IndicatorValuesSublist.ContainsKey(indicator.DisplayName))
            {
                if (options.IndicatorValuesSublist[indicator.DisplayName].Count > 6)
                {
                    int optionNumber = 1;
                    foreach (string opt in options.IndicatorValuesSublist[indicator.DisplayName])
                    {
                        AddDataValidation(xlsWorksheet, validation, Util.GetExcelColumnName(c), r, "", "", options.IndicatorValuesSublist[indicator.DisplayName], currentCulture);
                        c++;
                        optionNumber++;
                    }
                    c--; // remove last increment
                }
                else
                    AddDataValidation(xlsWorksheet, validation, Util.GetExcelColumnName(c), r, "", "", Util.ProduceEnumeration(options.IndicatorValuesSublist[indicator.DisplayName]),
                        currentCulture);
            }
            else if (indicator.DataTypeId == (int)IndicatorDataType.Partners)
            {
                if (!isUpdate)
                    AddDataValidation(xlsWorksheet, validation, Util.GetExcelColumnName(c), r, "", "", Util.ProduceEnumeration(partners.Select(p => p.DisplayName).ToList()),
                        currentCulture);
            }
            else if (indicator.DataTypeId == (int)IndicatorDataType.EvaluationUnit)
                AddDataValidation(xlsWorksheet, validation, Util.GetExcelColumnName(c), r, "", "", eus.Select(p => p.DisplayName).ToList(), currentCulture);
            else if (indicator.DataTypeId == (int)IndicatorDataType.EvaluationSite)
                AddDataValidation(xlsWorksheet, validation, Util.GetExcelColumnName(c), r, "", "", ess.Select(p => p.DisplayName).ToList(), currentCulture);
            else if (indicator.DataTypeId == (int)IndicatorDataType.EvalSubDistrict)
                AddDataValidation(xlsWorksheet, validation, Util.GetExcelColumnName(c), r, "", "", subdistricts.Select(p => p.DisplayName).ToList(), currentCulture);
            else if (indicator.DataTypeId == (int)IndicatorDataType.EcologicalZone)
                AddDataValidation(xlsWorksheet, validation, Util.GetExcelColumnName(c), r, "", "", ezs.Select(p => p.DisplayName).ToList(), currentCulture);
            else if (indicator.DataTypeId == (int)IndicatorDataType.Multiselect)
            {
                if (!isUpdate)
                    AddDataValidation(xlsWorksheet, validation, Util.GetExcelColumnName(c), r, "", "",
                        Util.ProduceEnumeration(DropDownValues.Where(i => i.IndicatorId == indicator.Id).Select(p => p.DisplayName).ToList()), currentCulture);
            }
            else if (indicator.DataTypeId == (int)IndicatorDataType.Dropdown)
                AddDataValidation(xlsWorksheet, validation, Util.GetExcelColumnName(c), r, "", "",
                    DropDownValues.Where(i => i.IndicatorId == indicator.Id).Select(p => p.DisplayName).ToList(), currentCulture);
            else if (indicator.DataTypeId == (int)IndicatorDataType.Month)
                AddDataValidation(xlsWorksheet, validation, Util.GetExcelColumnName(c), r, "", "", months.Select(p => p.Name).ToList(), currentCulture);
            else if (indicator.DataTypeId == (int)IndicatorDataType.DiseaseMultiselect)
            {
                if (!isUpdate)
                    AddDataValidation(xlsWorksheet, validation, Util.GetExcelColumnName(c), r, "", "",
                       Util.ProduceEnumeration(DropDownValues.Where(i => i.IndicatorId == indicator.Id && selectedDiseases.Contains(i.DisplayName)).Select(p => p.DisplayName).ToList()),
                       currentCulture);
            }
            else
                xlsWorksheet.Cells[r, c] = value;


            if (!string.IsNullOrEmpty(value))
            {
                object parsedVal = valueParser.Parse(indicator.DataTypeId, indicator.Id, value);
                if(parsedVal != null)
                    xlsWorksheet.Cells[r, c] = parsedVal;
            }   

            return c;
        }

        public virtual ImportResult ImportData(string filePath, int userId)
        {
            LoadRelatedLists();
            try
            {
                DataSet ds = LoadDataFromFile(filePath);

                if (ds.Tables.Count == 0)
                    return new ImportResult(TranslationLookup.GetValue("NoDataFound"));
                return MapAndSaveObjects(ds, userId);
            }
            catch (Exception ex)
            {
                return new ImportResult(TranslationLookup.GetValue("UnexpectedException") + ex.Message);
            }
        }

        public ImportResult UpdateData(string filePath, int userId, List<IHaveDynamicIndicatorValues> existing)
        {
            LoadRelatedLists();
            try
            {
                DataSet ds = LoadDataFromFile(filePath);

                if (ds.Tables.Count == 0)
                    return new ImportResult(TranslationLookup.GetValue("NoDataFound"));
                string errorMessage = "";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row[TranslationLookup.GetValue("ID")] == null || row[TranslationLookup.GetValue("ID")].ToString().Length == 0)
                        continue;
                    string objerrors = "";
                    int id = Convert.ToInt32(row[TranslationLookup.GetValue("ID")]);
                    var form = existing.FirstOrDefault(f => f.Id == id);
                    form.Notes = row[TranslationLookup.GetValue("Notes")].ToString();
                    // Validation
                    List<IndicatorValue> values = GetDynamicIndicatorValues(ds, row, ref objerrors);
                    foreach (var val in values)
                    {
                        var existingVal = form.IndicatorValues.FirstOrDefault(v => v.IndicatorId == val.IndicatorId);
                        if (existingVal.DynamicValue != val.DynamicValue)
                        {
                            existingVal.CalcByRedistrict = false;
                            existingVal.DynamicValue = val.DynamicValue;
                        }
                    }

                    objerrors += !form.IsValid() ? form.GetAllErrors(true) : "";
                    errorMessage += GetObjectErrors(objerrors, row[TranslationLookup.GetValue("ID")].ToString());
                }

                if (!string.IsNullOrEmpty(errorMessage))
                    return new ImportResult(CreateErrorMessage(errorMessage));

                return new ImportResult
                {
                    WasSuccess = true,
                    Count = existing.Count,
                    Message = string.Format(TranslationLookup.GetValue("ImportSuccess"), existing.Count),
                    Forms = existing
                };
            }
            catch (Exception ex)
            {
                return new ImportResult(TranslationLookup.GetValue("UnexpectedException") + ex.Message);
            }
        }

        protected virtual ImportResult MapAndSaveObjects(DataSet ds, int userId)
        {
            throw new NotImplementedException();
        }

        protected DataSet LoadDataFromFile(string filePath)
        {
            DataSet ds = null;
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                if (filePath.EndsWith(".xlsx"))
                    using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                    {
                        excelReader.IsFirstRowAsColumnNames = true;
                        ds = excelReader.AsDataSet();
                        excelReader.Close();
                    }
                else
                    using (IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream))
                    {
                        excelReader.IsFirstRowAsColumnNames = true;
                        ds = excelReader.AsDataSet();
                        excelReader.Close();
                    }
            }
            return ds;
        }

        protected List<IndicatorValue> GetDynamicIndicatorValues(DataSet ds, DataRow row, ref string errors)
        {
            Dictionary<string, IndicatorValue> multicolumnIndicators = new Dictionary<string, IndicatorValue>();
            List<IndicatorValue> inds = new List<IndicatorValue>();
            foreach (DataColumn col in ds.Tables[0].Columns)
            {
                string indicatorName = col.ColumnName.Replace("* ", "");
                bool hasMultipleCols = false;
                if (indicatorName.Contains(Translations.ImportSelectionOption))
                {
                    hasMultipleCols = true;
                    indicatorName = indicatorName.Replace(Translations.ImportSelectionOption, "^").Split('^')[0];
                }
                if (translatedIndicators.ContainsKey(indicatorName))
                {
                    string val = row[col].ToString().Trim();
                    Indicator curInd = translatedIndicators[indicatorName];
                    errors += GetValueAndValidate(curInd, ref val, indicatorName);

                    IndicatorValue ival = new IndicatorValue
                        {
                            IndicatorId = curInd.Id,
                            DynamicValue = val,
                            Indicator = curInd
                        };

                    if (!hasMultipleCols || !multicolumnIndicators.ContainsKey(indicatorName))
                        inds.Add(ival);

                    if (hasMultipleCols)
                    {
                        if (multicolumnIndicators.ContainsKey(indicatorName))
                            multicolumnIndicators[indicatorName].DynamicValue = multicolumnIndicators[indicatorName].DynamicValue + "|" + val;
                        else
                            multicolumnIndicators.Add(indicatorName, ival);
                    }
                }
            }
            return inds;
        }

        

        protected string GetValueAndValidate(Indicator indicator, ref string val, string name)
        {
            double d = 0;
            int i = 0;
            DateTime dt = new DateTime();

            if (indicator.IsRequired && string.IsNullOrEmpty(val))
                return name + ": " + TranslationLookup.GetValue("IsRequired") + Environment.NewLine;

            switch (indicator.DataTypeId)
            {
                case (int)IndicatorDataType.Date:
                    if (val.Length > 0 && !DateTime.TryParse(val, out dt))
                        return name + ": " + TranslationLookup.GetValue("MustBeDate") + Environment.NewLine;
                    else
                        val = dt.ToString("MM/dd/yyyy");
                    break;
                case (int)IndicatorDataType.Number:
                    if (val.Length > 0 && !Double.TryParse(val, out d))
                        return name + ": " + TranslationLookup.GetValue("MustBeNumber") + Environment.NewLine;
                    break;
                case (int)IndicatorDataType.Year:
                    if (val.Length > 0 && (!int.TryParse(val, out i) || (i > 2100 || i < 1900)))
                        return name + ": " + TranslationLookup.GetValue("ValidYear") + Environment.NewLine;
                    break;
                case (int)IndicatorDataType.YesNo:
                    bool isChecked = false;
                    if (val.ToLower() == "no")
                        val = "false";
                    else if (val.ToLower() == "yes")
                        val = "true";
                    if (val.Length > 0 && !Boolean.TryParse(val, out isChecked))
                        return name + ": " + TranslationLookup.GetValue("MustBeYesNo") + Environment.NewLine;
                    val = isChecked.ToString();
                    break;
                case (int)IndicatorDataType.Multiselect:
                    List<string> translationKeys = new List<string>();
                    foreach (string displayName in val.Replace(Util.EnumerationDelinator, "|").Split('|'))
                    {
                        IndicatorDropdownValue idv = DropDownValues.FirstOrDefault(a => a.IndicatorId == indicator.Id && a.DisplayName == displayName);
                        if (idv != null)
                            translationKeys.Add(idv.TranslationKey);
                        else
                            translationKeys.Add(displayName);
                    }
                    if (!string.IsNullOrEmpty(val) && translationKeys.Count == 0)
                        return name + ": " + TranslationLookup.GetValue("ValidDropdown") + Environment.NewLine;
                    val = string.Join("|", translationKeys.ToArray());
                    break;
                case (int)IndicatorDataType.DiseaseMultiselect:
                    List<string> translationKeys2 = new List<string>();
                    foreach (string displayName in val.Replace(Util.EnumerationDelinator, "|").Split('|'))
                    {
                        IndicatorDropdownValue idv = DropDownValues.FirstOrDefault(a => a.IndicatorId == indicator.Id && a.DisplayName == displayName);
                        if (idv != null)
                            translationKeys2.Add(idv.TranslationKey);
                        else
                            translationKeys2.Add(displayName);
                    }
                    if (!string.IsNullOrEmpty(val) && translationKeys2.Count == 0)
                        return name + ": " + TranslationLookup.GetValue("ValidDropdown") + Environment.NewLine;
                    val = string.Join("|", translationKeys2.ToArray());
                    break;
                case (int)IndicatorDataType.Dropdown:
                    string selectedVal = val;
                    IndicatorDropdownValue ival = DropDownValues.FirstOrDefault(a => a.IndicatorId == indicator.Id && a.DisplayName == selectedVal);
                    if (!string.IsNullOrEmpty(val) && ival == null)
                        return name + ": " + TranslationLookup.GetValue("ValidDropdown") + Environment.NewLine;
                    val = ival == null ? null : ival.TranslationKey;
                    break;
                case (int)IndicatorDataType.Partners:
                    List<string> partnerIds = new List<string>();
                    string p = val.Replace(Util.EnumerationDelinator, "|");
                    string[] ps = p.Split('|');
                    foreach (var partner in partners.Where(v => ps.Contains(v.DisplayName)))
                        partnerIds.Add(partner.Id.ToString());
                    if (!string.IsNullOrEmpty(val) && partnerIds.Count == 0)
                        return name + ": " + TranslationLookup.GetValue("ValidDropdown") + Environment.NewLine;
                    val = string.Join("|", partnerIds.ToArray());
                    break;
                case (int)IndicatorDataType.Month:
                    string month = val;
                    var monthItem = months.FirstOrDefault(v => v.Name == month);
                    if (!string.IsNullOrEmpty(val) && monthItem == null)
                        return name + ": " + TranslationLookup.GetValue("ValidDropdown") + Environment.NewLine;
                    val = monthItem == null ? null : monthItem.Id.ToString();
                    break;
                case (int)IndicatorDataType.EvaluationUnit:
                    string euVal = val;
                    var eu = eus.FirstOrDefault(v => v.DisplayName == euVal);
                    if (!string.IsNullOrEmpty(val) && eu == null)
                        return name + ": " + TranslationLookup.GetValue("ValidDropdown") + Environment.NewLine;
                    val = eu == null ? null : eu.Id.ToString();
                    break;
                case (int)IndicatorDataType.EcologicalZone:
                    var evVal = val;
                    var ez = ezs.FirstOrDefault(v => v.DisplayName == evVal);
                    if (!string.IsNullOrEmpty(val) && ez == null)
                        return name + ": " + TranslationLookup.GetValue("ValidDropdown") + Environment.NewLine;
                    val = ez == null ? null : ez.Id.ToString();
                    break;
                case (int)IndicatorDataType.EvaluationSite:
                    var esVal = val;
                    var es = ess.FirstOrDefault(v => v.DisplayName == esVal);
                    if (!string.IsNullOrEmpty(val) && es == null)
                        return name + ": " + TranslationLookup.GetValue("ValidDropdown") + Environment.NewLine;
                    val = es == null ? null : es.Id.ToString();
                    break;
                case (int)IndicatorDataType.EvalSubDistrict:
                    var sdVal = val;
                    var sd = subdistricts.FirstOrDefault(v => v.DisplayName == sdVal);
                    if (!string.IsNullOrEmpty(val) && sd == null)
                        return name + ": " + TranslationLookup.GetValue("ValidDropdown") + Environment.NewLine;
                    val = sd == null ? null : sd.Id.ToString();
                    break;
            }

            // Values are manipulated in the above loop, double check if now they are required because invalid.
            if (indicator.IsRequired && string.IsNullOrEmpty(val))
                return name + ": " + TranslationLookup.GetValue("IsRequired") + Environment.NewLine;

            return "";
        }
        /// <summary>
        /// Adds a small Infobox and a Validation with restriction (only these values will be selectable) to the specified cell.
        /// </summary>
        /// <param name="worksheet">The excel-sheet</param>
        /// <param name="rowNr">1-based row index of the cell that will contain the validation</param>
        /// <param name="columnNr">1-based column index of the cell that will contain the validation</param>
        /// <param name="title">Title of the Infobox</param>
        /// <param name="message">Message in the Infobox</param>
        /// <param name="validationValues">List of available values for selection of the cell. No other value, than this list is allowed to be used.</param>
        /// <exception cref="Exception">Thrown, if an error occurs, or the worksheet was null.</exception>
        public void AddDataValidation(Microsoft.Office.Interop.Excel.Worksheet worksheet, Microsoft.Office.Interop.Excel.Worksheet validationWorksheet, string col, int row,
            string title, string message, List<string> validationValues, CultureInfo currentCulture)
        {
            if (validationValues == null || validationValues.Count == 0)
                return;
            //If the message-string is too long (more than 255 characters, prune it)
            if (message.Length > 255)
                message = message.Substring(0, 254);

            try
            {
                if (!validationRanges.ContainsKey(col))
                {
                    var valStart = validationRow;
                    foreach (var val in validationValues)
                    {
                        validationWorksheet.Cells[validationRow, "A"] = val;
                        validationRow++;
                    }
                    validationRanges.Add(col, string.Format("={0}!$A{1}:$A{2}", validationSheetName, valStart, validationRow - 1));
                }
                #region old validation
                //The validation requires a ';'-separated list of values, that goes as the restrictions-parameter.
                //Fold the list, so you can add it as restriction. (Result is "Value1;Value2;Value3")
                //If you use another separation-character (e.g in US) change the ; appropriately (e.g. to the ,)

                //string values = "";
                //if (currentCulture.TwoLetterISOLanguageName == "en")
                //    values = string.Join(",", validationValues.ToArray());
                //else
                //    values = string.Join(";", validationValues.ToArray());
                #endregion

                //Select the specified cell
                Microsoft.Office.Interop.Excel.Range cell = (Microsoft.Office.Interop.Excel.Range)worksheet.get_Range(col + row, col + row);
                //Delete any previous validation
                cell.Validation.Delete();
                //Add the validation, that only allowes selection of provided values.
                cell.Validation.Add(Microsoft.Office.Interop.Excel.XlDVType.xlValidateList,
                    Microsoft.Office.Interop.Excel.XlDVAlertStyle.xlValidAlertStop,
                    Type.Missing,
                    validationRanges[col], Type.Missing);
                cell.Validation.IgnoreBlank = true;
                //Optional put a message there
                cell.Validation.InputTitle = title;
                cell.Validation.InputMessage = message;

            }
            catch (Exception exception)
            {
                //This part should not be reached, but is used for stability-reasons
                throw new Exception(String.Format("Error when adding a Validation with restriction to the specified cell Row:{0}, Column:{1}, Message: {2}", row, col, message), exception);

            }
        }

        #region Data Table Specific

        //protected virtual DataTable GetDataTable()
        //{
        //    DataTable data = new System.Data.DataTable();
        //    data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("Location") + "#"));
        //    data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("Location")));
        //    AddDynamicIndicators(data);
        //    data.Columns.Add(new System.Data.DataColumn(TranslationLookup.GetValue("Notes")));
        //    return data;
        //}

        private void AddDynamicIndicators(DataTable dataTable)
        {
            AddSpecificRows(dataTable);
            foreach (var key in Indicators.Keys)
            {
                DataColumn col = null;
                if (Indicators[key].DataTypeId == (int)IndicatorDataType.Date)
                    col = new DataColumn(TranslationLookup.GetValue(key, key), typeof(DateTime));
                else
                    col = new System.Data.DataColumn(TranslationLookup.GetValue(key, key));
                dataTable.Columns.Add(col);
            }
        }

        protected virtual void AddSpecificRows(DataTable dataTable) { }

        //public void AddDataToWorksheet(DataTable data, Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet, List<AdminLevel> rows)
        //{
        //    // Add rows to data table
        //    foreach (AdminLevel l in rows)
        //    {
        //        DataRow row = data.NewRow();
        //        row[TranslationLookup.GetValue("Location") + "#"] = l.Id;
        //        row[TranslationLookup.GetValue("Location")] = l.Name;
        //        data.Rows.Add(row);
        //    }

        //    AddTableToWorksheet(data, xlsWorksheet);
        //}

        public void AddTableToWorksheet(DataTable data, Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet)
        {
            // Add columns
            int iCol = 0;
            foreach (DataColumn c in data.Columns)
            {
                iCol++;
                xlsWorksheet.Cells[1, iCol] = c.ColumnName;
            }

            // Add rows
            int iRow = 0;
            foreach (DataRow r in data.Rows)
            {
                iRow++;

                for (int i = 1; i < data.Columns.Count + 1; i++)
                {
                    if (iRow == 1)
                    {
                        // Add the header the first time through 
                        xlsWorksheet.Cells[iRow, i] = data.Columns[i - 1].ColumnName;
                    }

                    if (r[1].ToString() != "")
                    {
                        xlsWorksheet.Cells[iRow + 1, i] = r[i - 1].ToString();
                    }
                }
            }

            var last = xlsWorksheet.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            var range = xlsWorksheet.get_Range("A1", last);
            range.Columns.AutoFit();
        }

        protected string CreateErrorMessage(string errorMessage)
        {
            return TranslationLookup.GetValue("ImportErrorHeader") + Environment.NewLine + "--------" + Environment.NewLine + errorMessage;
        }

        protected string GetObjectErrors(string objerrors, string location)
        {
            if (!string.IsNullOrEmpty(objerrors))
                return Environment.NewLine + string.Format(TranslationLookup.GetValue("ImportErrors"), TranslationLookup.GetValue("ID") + " " + location) +
                    Environment.NewLine + "--------" + Environment.NewLine + objerrors;
            return "";
        }
        #endregion
    }
}
