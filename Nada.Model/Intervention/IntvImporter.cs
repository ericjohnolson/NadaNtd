using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Excel;
using Nada.Model.Repositories;

namespace Nada.Model.Intervention
{
    public class LfMdaImporter : ImporterBase, IImporter
    {
        public LfMdaImporter(IntvType t) : base(t)
        {
        }

        protected override void AddSpecificRows(DataTable dataTable)
        {
            dataTable.Columns.Add(new System.Data.DataColumn("Date of Intervention"));
        }

        public ImportResult ImportData(string filePath, int userId)
        {
            try
            {
                DataSet ds = LoadDataFromFile(filePath);

                if (ds.Tables.Count == 0)
                    return new ImportResult("The file was an incorrect format or had no data");

                IntvRepository repo = new IntvRepository();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    PcMda intv = repo.CreateIntv<PcMda>(StaticIntvType.IvmAlbMda);
                    intv.AdminLevelId = Convert.ToInt32(row["Location Id"]);
                    intv.Notes = row["notes"].ToString();
                    double d = 0;
                    if (double.TryParse(row["Date of Intervention"].ToString(), out d))
                        intv.StartDate = DateTime.FromOADate(d);
                    // HOW TO IMPORT Distros, funders, partners?
                    intv.IndicatorValues = GetDynamicIndicatorValues(ds, row);
                    repo.Save(intv, userId);
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
            get { return "LF MDA Import"; }
        }
    }

    public class IntvImporter : ImporterBase, IImporter
    {
        StaticIntvType intvType;
        string importName;

        public IntvImporter(IHaveDynamicIndicators i, StaticIntvType t, string name)
            : base(i)
        {
            intvType = t;
            importName = name;
        }

        protected override void AddSpecificRows(DataTable dataTable)
        {
            dataTable.Columns.Add(new System.Data.DataColumn("Date of Intervention"));
        }

        public ImportResult ImportData(string filePath, int userId)
        {
            try
            {
                DataSet ds = LoadDataFromFile(filePath);

                if (ds.Tables.Count == 0)
                    return new ImportResult("The file was an incorrect format or had no data");

                IntvRepository repo = new IntvRepository();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    IntvBase intv = repo.CreateIntv<IntvBase>(intvType);
                    intv.AdminLevelId = Convert.ToInt32(row["Location Id"]);
                    intv.Notes = row["notes"].ToString();

                    double d = 0;
                    if (double.TryParse(row["Date of Intervention"].ToString(), out d))
                        intv.StartDate = DateTime.FromOADate(d);

                    intv.IndicatorValues = GetDynamicIndicatorValues(ds, row);
                    repo.SaveBase(intv, userId);
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
            get { return importName; }
        }
    }
}
