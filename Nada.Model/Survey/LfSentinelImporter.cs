using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Excel;
using Nada.Globalization;
using Nada.Model.Repositories;
namespace Nada.Model.Survey
{
    public class LfSentinelImporter : ImporterBase, IImporter
    {
        public LfSentinelImporter(SurveyType t)
            : base(t)
        {
        }

        protected override void AddSpecificRows(DataTable dataTable)
        {
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.SiteType));
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.SpotCheckSiteName));
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.Latitude));
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.Longitude));
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.TimingType));
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.TestType));
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.StartDateSurvey));
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.EndDateSurvey));
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.Vectors));
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.Partners));
        }

        public ImportResult ImportData(string filePath, int userId)
        {
            try
            {
                DataSet ds = LoadDataFromFile(filePath);

                if (ds.Tables.Count == 0)
                    return new ImportResult(Translations.ImportNoDataError);

                SurveyRepository repo = new SurveyRepository();
                List<LfMfPrevalence> surveysToSave = new List<LfMfPrevalence>();
                Dictionary<int, string> errors = new Dictionary<int, string>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    LfMfPrevalence survey = repo.CreateSurvey<LfMfPrevalence>(StaticSurveyType.LfPrevalence);
                    survey.AdminLevelId = Convert.ToInt32(row[Translations.Location + "#"]);
                    survey.Notes = row[Translations.Notes].ToString();
                    survey.IndicatorValues = GetDynamicIndicatorValues(ds, row);
                    survey.MapIndicatorsToProperties();
                    surveysToSave.Add(survey);
                    if (!survey.IsValid())
                    {
                        // need to add errors to the excel sheet to send back to the user?
                        errors.Add(Convert.ToInt32(row[Translations.Location + "#"]), survey.GetAllErrors());
                    }
                }

                // HAS ERRORS, report them back?
                //if (errors.Keys.Count > 0)
                //    return new ImportResult
                //    {
                //        WasSuccess = false,
                //        Count = 0,
                //        ErrorMessage = string.Empty
                //    };

                // NO ERRORS, do save.
                foreach (var survey in surveysToSave)
                    repo.Save(survey, userId);

                int rec = ds.Tables[0].Rows.Count;
                return new ImportResult
                {
                    WasSuccess = true,
                    Count = rec,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new ImportResult("An unexpected exception occurred: " + ex.Message);
            }
        }

        public string ImportName
        {
            get { return Translations.LfSentinelImport; }
        }

        public void CreateImportFile(string filename, List<AdminLevel> levels)
        {
            // TODO Sentinel site/Spotcheck site indicators???

            SurveyRepository repo = new SurveyRepository();
            List<Vector> vectors = repo.GetVectors();
            IntvRepository repo2 = new IntvRepository();
            List<Partner> partners = repo2.GetPartners();
            LfMfPrevalence model = new LfMfPrevalence();
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook xlsWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet;
            Microsoft.Office.Interop.Excel.DropDowns xlDropDowns;
            object oMissing = System.Reflection.Missing.Value;

            //Create new workbook
            xlsWorkbook = xlsApp.Workbooks.Add(true);

            //Get the first worksheet
            xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)(xlsWorkbook.Worksheets[1]);

            // Load data into excel worksheet
            DataTable data = GetDataTable();
            CreateImportExcel(data, xlsWorksheet, levels);

            // Set up all the different drop downs and multiselects
            xlDropDowns = ((Microsoft.Office.Interop.Excel.DropDowns)(xlsWorksheet.DropDowns(oMissing)));

            string casCol = GetExcelColumnName(data.Columns[Translations.CasualAgent].Ordinal + 1);
            string timingCol = GetExcelColumnName(data.Columns[Translations.TimingType].Ordinal + 1);
            string testCol = GetExcelColumnName(data.Columns[Translations.TestType].Ordinal + 1);
            string partnersCol = GetExcelColumnName(data.Columns[Translations.Partners].Ordinal + 1);
            string vectorsCol = GetExcelColumnName(data.Columns[Translations.Vectors].Ordinal + 1);
            for (int i = 1; i <= data.Rows.Count; i++)
            {
                AddDropdown(xlsWorksheet, xlDropDowns, model.TimingTypeValues, timingCol, i + 1);
                AddDropdown(xlsWorksheet, xlDropDowns, model.TestTypeValues, testCol, i + 1);
                AddDropdown(xlsWorksheet, xlDropDowns, model.CasualAgentValues, casCol, i + 1);
                AddDropdown(xlsWorksheet, xlDropDowns, Util.ProduceEnumeration(partners.Select(p => p.DisplayName).ToList()), partnersCol, i + 1);
                AddDropdown(xlsWorksheet, xlDropDowns, Util.ProduceEnumeration(vectors.Select(p => p.DisplayName).ToList()), vectorsCol, i + 1);
            }

            xlsApp.DisplayAlerts = false;
            xlsWorkbook.Close(true, filename, null);
            xlsApp.Quit();
            xlsWorksheet = null;
            xlsWorkbook = null;
            xlsApp = null;
        }

    }

}
