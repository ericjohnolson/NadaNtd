using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Excel;
using Nada.Globalization;
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
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.StartDateMda));
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.EndDateMda));
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
                    PcMda intv = repo.CreateIntv<PcMda>((int)StaticIntvType.IvmAlbMda);
                    intv.AdminLevelId = Convert.ToInt32(row[Translations.Location + "#"]);
                    intv.Notes = row[Translations.Notes].ToString();
                    double d = 0;
                    if (double.TryParse(row[Translations.StartDateMda].ToString(), out d))
                        intv.StartDate = DateTime.FromOADate(d);
                    double d2 = 0;
                    if (double.TryParse(row[Translations.EndDateMda].ToString(), out d2))
                        intv.EndDate = DateTime.FromOADate(d2);
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
            dataTable.Columns.Add(new System.Data.DataColumn(Translations.StartDateMda));
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
                    IntvBase intv = repo.CreateIntv<IntvBase>((int)intvType);
                    intv.AdminLevelId = Convert.ToInt32(row[Translations.Location + "#"]);
                    intv.Notes = row[Translations.Notes].ToString();

                    double d = 0;
                    if (double.TryParse(row[Translations.StartDateMda].ToString(), out d))
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
