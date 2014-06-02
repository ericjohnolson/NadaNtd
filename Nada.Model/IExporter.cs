using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using Nada.Globalization;
using Nada.Model.Exports;
using Nada.Model.Intervention;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using excel = Microsoft.Office.Interop.Excel;

namespace Nada.Model
{
    public interface IExporter
    {
        string ExportName { get; }
        string Extension { get; }
        string GetYear(ExportType type);
        ExportResult DoExport(string fileName, int userId, ExportType exportType);

    }

    public class ExportParams
    {
        public ExportJrfQuestions Questions { get; set; }
        public ExportCmJrfQuestions CmQuestions { get; set; }
        public int Year { get; set; }
        public string FileName { get; set; }
    }


    public class ExportResult
    {
        public ExportResult()
        {

        }
        public ExportResult(string error)
        {
            WasSuccess = false;
            ErrorMessage = error;
        }
        public bool WasSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ExporterBase
    {
        protected List<IndicatorDropdownValue> dropdownValues = new List<IndicatorDropdownValue>();

        protected void AddValueToRange(excel.Worksheet xlsWorksheet, excel.Range rng, string cell, object value)
        {
            object missing = System.Reflection.Missing.Value;
            rng = xlsWorksheet.get_Range(cell, missing);
            rng.Value = value;
            
        }

        public virtual string Extension
        {
            get { return "xlsx"; }
        }

        public virtual string GetYear(ExportType exportType)
        {
            return "";
        }

        private class IntVal
        {
            public IntVal()
            {
                Value = 0;
                TypeNames = new List<string>();
            }
            public List<string> TypeNames { get; set; }
            public int Value { get; set; }
        }

        protected Dictionary<int, DataRow> GetIntvsAggregatedToReportingLevel(DateTime start, DateTime end, List<AdminLevel> units)
        {
            IntvRepository iRepo = new IntvRepository();
            ReportOptions options = new ReportOptions
            {
                MonthYearStarts = start.Month,
                StartDate = start,
                EndDate = end,
                IsCountryAggregation = false,
                IsByLevelAggregation = true,
                IsAllLocations = false,
                IsNoAggregation = false,
                IsGroupByRange = true,

            };
            options.SelectedAdminLevels = units;
            IntvReportGenerator gen = new IntvReportGenerator();
            var intvIds = new List<int> { 2, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
            foreach (int id in intvIds)
                AddIntvIndicators(options, id, iRepo);
            ReportResult ddResult = gen.Run(new SavedReport { ReportOptions = options });
            Dictionary<int, DataRow> intvData = new Dictionary<int, DataRow>();
            foreach (DataRow dr in ddResult.DataTableResults.Rows)
            {
                int id = 0;
                if (int.TryParse(dr["ID"].ToString(), out id))
                {
                    if (intvData.ContainsKey(id))
                        intvData[id] = dr;
                    else
                        intvData.Add(id, dr);
                }
            }
            return intvData;
        }

        protected void AddIntvIndicators(ReportOptions options, int typeId, IntvRepository iRepo)
        {
            IntvType iType = iRepo.GetIntvType(typeId);
            if (typeId == 10)
                dropdownValues = iType.IndicatorDropdownValues;
            foreach (var indicator in iType.Indicators.Where(i => i.Value.DataTypeId != (int)IndicatorDataType.Calculated))
                options.SelectedIndicators.Add(ReportRepository.CreateReportIndicator(iType.Id, indicator));
        }


        protected string GetIntFromRow(string indicatorName, List<string> intvTypes, DataRow dr, int startRound, int endRound, string diseaseType, List<string> typeNames, string albIndicator)
        {
            List<IntVal> intVals = new List<IntVal>();
            for (int i = startRound; i <= endRound; i++)
            {
                IntVal iVal = new IntVal();

                foreach (var tname in typeNames)
                {
                    string diseaseCol = TranslationLookup.GetValue("PcIntvDiseases") + " - " + TranslationLookup.GetValue(tname) + string.Format(" - {0} ", Translations.Round) + i;
                    string indicatorCol = "";
                    if (tname.Contains("Alb"))
                        indicatorCol = TranslationLookup.GetValue(albIndicator) + " - " + TranslationLookup.GetValue(tname) + string.Format(" - {0} ", Translations.Round) + i;
                    else
                        indicatorCol = TranslationLookup.GetValue(indicatorName) + " - " + TranslationLookup.GetValue(tname) + string.Format(" - {0} ", Translations.Round) + i;

                    if (dr.Table.Columns.Contains(diseaseCol))
                    {
                        // make sure disease treated has LF
                        if (dr[diseaseCol].ToString().Contains(TranslationLookup.GetValue(diseaseType)) && dr.Table.Columns.Contains(indicatorCol))
                        {
                            int v = 0;
                            if (int.TryParse(dr[indicatorCol].ToString(), out v))
                            {
                                iVal.Value += v;
                                iVal.TypeNames.Add(tname);
                            }
                        }
                    }
                }
                if (iVal.TypeNames.Count > 0)
                    intVals.Add(iVal);
            }

            // MAX between rounds
            var val = intVals.OrderByDescending(i => i.Value).FirstOrDefault();
            if (val == null)
                return "";
            intvTypes = intvTypes.Union(val.TypeNames).ToList();
            return val.Value.ToString();
        }

        protected string GetIntFromRow(string indicatorName, List<string> intvTypes, DataRow dr, int startRound, int endRound, string diseaseType, List<string> typeNames)
        {
            List<IntVal> intVals = new List<IntVal>();
            for (int i = startRound; i <= endRound; i++)
            {
                IntVal iVal = new IntVal();

                foreach (var tname in typeNames)
                {
                    string diseaseCol = TranslationLookup.GetValue("PcIntvDiseases") + " - " + TranslationLookup.GetValue(tname) + string.Format(" - {0} ", Translations.Round) + i;
                    string indicatorCol = TranslationLookup.GetValue(indicatorName) + " - " + TranslationLookup.GetValue(tname) + string.Format(" - {0} ", Translations.Round) + i;
                    if (dr.Table.Columns.Contains(diseaseCol))
                    {
                        // make sure disease treated has LF
                        if ((string.IsNullOrEmpty(diseaseType) || dr[diseaseCol].ToString().Contains(TranslationLookup.GetValue(diseaseType))) 
                            && dr.Table.Columns.Contains(indicatorCol))
                        {
                            int v = 0;
                            if (int.TryParse(dr[indicatorCol].ToString(), out v))
                            {
                                iVal.Value += v;
                                iVal.TypeNames.Add(tname);
                            }
                        }
                    }
                }
                if (iVal.TypeNames.Count > 0)
                    intVals.Add(iVal);
            }

            // MAX between rounds
            var val = intVals.OrderByDescending(i => i.Value).FirstOrDefault();
            if (val == null)
                return "";
            intvTypes = intvTypes.Union(val.TypeNames).ToList();
            return val.Value.ToString();
        }

        protected DateTime? GetDateFromRow(string indicatorName, List<string> intvTypes, DataRow dr, bool isGreaterThan, int startRound, int endRound, string diseaseType, List<string> typeNames)
        {
            string valueType = "";
            DateTime? value = null;
            for (int i = startRound; i <= endRound; i++)
            {
                foreach (var tname in typeNames)
                {
                    string diseaseCol = TranslationLookup.GetValue("PcIntvDiseases") + " - " + TranslationLookup.GetValue(tname) + string.Format(" - {0} ", Translations.Round) + i;
                    string indicatorCol = TranslationLookup.GetValue(indicatorName) + " - " + TranslationLookup.GetValue(tname) + string.Format(" - {0} ", Translations.Round) + i;
                    if (dr.Table.Columns.Contains(diseaseCol))
                    {
                        // make sure disease treated has LF
                        if ((string.IsNullOrEmpty(diseaseType) || dr[diseaseCol].ToString().Contains(TranslationLookup.GetValue(diseaseType))) 
                            && dr.Table.Columns.Contains(indicatorCol) && !string.IsNullOrEmpty(dr[indicatorCol].ToString()))
                        {
                            DateTime newDt = DateTime.ParseExact(dr[indicatorCol].ToString(), "M/d/yyyy", CultureInfo.InvariantCulture);
                            if (value == null || (newDt > value.Value && isGreaterThan) || (newDt < value.Value && !isGreaterThan))
                            {
                                value = newDt;
                                valueType = tname;
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(valueType))
                intvTypes = intvTypes.Union(new List<string> { valueType }).ToList();
            return value;
        }

        protected string GetDropdownFromRow(string indicatorName, List<string> intvTypes, DataRow dr, int indId, int startRound, int endRound, string diseaseType, List<string> typeNames)
        {
            string valueType = "";
            IndicatorDropdownValue value = null;
            List<IndicatorDropdownValue> possibleValues = dropdownValues.Where(i => i.IndicatorId == indId).ToList();
            for (int i = startRound; i <= endRound; i++)
            {
                foreach (var tname in typeNames)
                {
                    string diseaseCol = TranslationLookup.GetValue("PcIntvDiseases") + " - " + TranslationLookup.GetValue(tname) + string.Format(" - {0} ", Translations.Round) + i;
                    string indicatorCol = TranslationLookup.GetValue(indicatorName) + " - " + TranslationLookup.GetValue(tname) + string.Format(" - {0} ", Translations.Round) + i;
                    if (dr.Table.Columns.Contains(diseaseCol))
                    {
                        // make sure disease treated has LF
                        if (dr[diseaseCol].ToString().Contains(TranslationLookup.GetValue(diseaseType)) && dr.Table.Columns.Contains(indicatorCol))
                        {
                            // determin worst case for the current and the 
                            IndicatorDropdownValue v2 = possibleValues.FirstOrDefault(v => v.DisplayName == dr[indicatorCol].ToString());
                            if (v2 != null && (value == null || v2.WeightedValue > value.WeightedValue))
                            {
                                value = v2;
                                valueType = tname;
                            }
                        }
                    }
                }
            }

            if (value == null || string.IsNullOrEmpty(valueType))
                return "";
            intvTypes = intvTypes.Union(new List<string> { valueType }).ToList();
            return TranslationLookup.GetValue(value.TranslationKey);
        }

        protected string GetCombineFromRow(string indicatorName, List<string> intvTypes, DataRow dr, int startRound, int endRound, string diseaseType, List<string> typeNames)
        {
            List<string> valueTypes = new List<string>();
            List<string> values = new List<string>();
            for (int i = startRound; i <= endRound; i++)
            {
                foreach (var tname in typeNames)
                {
                    string diseaseCol = TranslationLookup.GetValue("PcIntvDiseases") + " - " + TranslationLookup.GetValue(tname) + string.Format(" - {0} ", Translations.Round) + i;
                    string indicatorCol = TranslationLookup.GetValue(indicatorName) + " - " + TranslationLookup.GetValue(tname) + string.Format(" - {0} ", Translations.Round) + i;
                    if (dr.Table.Columns.Contains(diseaseCol))
                    {
                        // make sure disease treated has LF
                        if (dr[diseaseCol].ToString().Contains(TranslationLookup.GetValue(diseaseType)) && dr.Table.Columns.Contains(indicatorCol))
                        {

                            if (!string.IsNullOrEmpty(dr[indicatorCol].ToString()))
                            {
                                if (!values.Contains(dr[indicatorCol].ToString()))
                                    values.Add(dr[indicatorCol].ToString());
                                valueTypes.Add(tname);
                            }
                        }
                    }
                }
            }

            if (values.Count == 0)
                return "";
            intvTypes = intvTypes.Union(valueTypes).ToList();
            return string.Join(", ", values.ToArray());
        }

    }
}
