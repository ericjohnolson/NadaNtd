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
            // dataTable.Columns.Add(new System.Data.DataColumn("Date of Intervention"));
        }

        public void CreateWorkbook(string filename, List<AdminLevel> rows)
        {

            //Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            //Microsoft.Office.Interop.Excel.Workbook xlsWorkbook;
            //Microsoft.Office.Interop.Excel.Worksheet xlsWorksheet;
            //object oMissing = System.Reflection.Missing.Value;

            ////Create new workbook
            //xlsWorkbook = xlsApp.Workbooks.Add(true);

            ////Get the first worksheet
            //xlsWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)(xlsWorkbook.Worksheets[1]);

            //string[] ddl_item = { "Answers", "Autos", "Finance", "Games", "Groups", "HotJobs", "Maps", "Mobile Web", "Movies", "Music", "Personals", "Real Estate", "Shopping", "Sports", "Tech", "Travel", "TV", "Yellow Pages" };

            //Microsoft.Office.Interop.Excel.Range xlsRange;
            //xlsRange = xlsWorksheet.get_Range("A1", "A1");

            //Microsoft.Office.Interop.Excel.DropDowns xlDropDowns;
            //Microsoft.Office.Interop.Excel.DropDown xlDropDown;
            //xlDropDowns = ((Microsoft.Office.Interop.Excel.DropDowns)(xlsWorksheet.DropDowns(oMissing)));
            //xlDropDown = xlDropDowns.Add((double)xlsRange.Left, (double)xlsRange.Top, (double)xlsRange.Width, (double)xlsRange.Height, true);

            ////Add item into drop down list
            //for (int i = 0; i < ddl_item.Length; i++)
            //{
            //    xlDropDown.AddItem(ddl_item[i], i + 1);
            //}

            //xlsApp.DisplayAlerts = false;
            //xlsWorkbook.Close(true, filename, null);
            //xlsApp.Quit();

            //xlsWorksheet = null;
            //xlsWorkbook = null;
            //xlsApp = null;
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
                    survey.AdminLevelId = Convert.ToInt32(row["Location Id"]);
                    survey.Notes = row["notes"].ToString();
                    survey.IndicatorValues = GetDynamicIndicatorValues(ds, row);
                    survey.MapIndicatorsToProperties();
                    surveysToSave.Add(survey);
                    if (!survey.IsValid())
                    {
                        // need to add errors to the excel sheet to send back to the user?
                        errors.Add(Convert.ToInt32(row["Location Id"]), survey.GetAllErrors());
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
    }

}
