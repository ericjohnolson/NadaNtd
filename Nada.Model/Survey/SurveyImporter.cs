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

        public ImportResult ImportData(string filePath, int userId)
        {
            try
            {
                DataSet ds = LoadDataFromFile(filePath);

                if (ds.Tables.Count == 0)
                    return new ImportResult(Translations.ImportNoDataError);

                SurveyRepository repo = new SurveyRepository();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    LfMfPrevalence survey = repo.CreateSurvey<LfMfPrevalence>(StaticSurveyType.LfPrevalence);
                    survey.AdminLevelId = Convert.ToInt32(row["Location Id"]);
                    survey.Notes = row["notes"].ToString();
                    double d = 0;

                    // HOW TO IMPORT Distros, funders, partners?
                    survey.CustomIndicatorValues = GetDynamicIndicatorValues(ds, row);
                    repo.Save(survey, userId);
                }

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
