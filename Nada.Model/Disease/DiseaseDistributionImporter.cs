using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Excel;
using Nada.Model.Repositories;

namespace Nada.Model
{
    public class DiseaseDistributionImporter : ImporterBase, IImporter
    {
        private DiseaseDistribution type = null;
        public DiseaseDistributionImporter(DiseaseDistribution t)
            : base(t)
        {
            type = t;
        }

        protected override void AddSpecificRows(DataTable dataTable)
        {
        }

        public ImportResult ImportData(string filePath, int userId)
        {
            try
            {
                DataSet ds = LoadDataFromFile(filePath);

                if (ds.Tables.Count == 0)
                    return new ImportResult("The file was an incorrect format or had no data");

                DiseaseRepository repo = new DiseaseRepository();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DiseaseDistribution dd = repo.Create((DiseaseType)type.Disease.Id);
                    dd.AdminLevelId = Convert.ToInt32(row["Location Id"]);
                    dd.Notes = row["notes"].ToString();
                    dd.CustomIndicatorValues = GetDynamicIndicatorValues(ds, row);
                    repo.Save(dd, userId);
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
            get { return type.Disease.DisplayName + " Distribution Import"; }
        }
    }
}
