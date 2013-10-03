using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Excel;
using Nada.Model.Diseases;
using Nada.Model.Repositories;

namespace Nada.Model
{
    public class AdminLevelImporter : ImporterBase, IImporter
    {
        public AdminLevelImporter()
            : base()
        {
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

                DemoRepository repo = new DemoRepository();
                repo.BulkImportAdminLevels(ds, userId);

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
            get { throw new NotImplementedException(); }
        }
    }
}
