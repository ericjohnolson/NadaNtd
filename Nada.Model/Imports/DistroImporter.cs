using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Excel;
using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Imports;
using Nada.Model.Repositories;

namespace Nada.Model
{
    public class DistroImporter : ImporterBase, IImporter
    {
        public override string ImportName { get { return Translations.DiseaseDistribution +" " + Translations.Import; } }
        private DiseaseDistroPc type = null;
        private DiseaseDistroCm cmType = null;
        DiseaseRepository repo = new DiseaseRepository();
        public override void SetType(int id)
        {
           var d = repo.GetDiseaseById(id);
           if (d.DiseaseType == "CM")
           {
               cmType = repo.CreateCm((DiseaseType)d.Id);
               Indicators = cmType.Indicators;
               DropDownValues = cmType.IndicatorDropdownValues;
           }
           else
           {
               type = repo.Create((DiseaseType)d.Id);
               Indicators = type.Indicators;
               DropDownValues = type.IndicatorDropdownValues;
           }
        }

        public override List<TypeListItem> GetAllTypes()
        {
            return repo.GetSelectedDiseases().Select(t => new TypeListItem
            {
                Id = t.Id,
                Name = t.DisplayName
            }).ToList();
        }

        protected override ImportResult MapAndSaveObjects(DataSet ds, int userId)
        {
            if (type != null)
                return MapAndSaveObjectsPc(ds, userId);
            return MapAndSaveObjectsCm(ds, userId);
        }

        private ImportResult MapAndSaveObjectsPc(DataSet ds, int userId)
        {
            string errorMessage = "";
            List<DiseaseDistroPc> objs = new List<DiseaseDistroPc>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var obj = repo.Create((DiseaseType)type.Disease.Id);
                obj.AdminLevelId = Convert.ToInt32(row[Translations.Location + "#"]);
                obj.Notes = row[Translations.Notes].ToString();
                obj.IndicatorValues = GetDynamicIndicatorValues(ds, row);

                var objerrors = !obj.IsValid() ? obj.GetAllErrors(true) : "";
                if (!string.IsNullOrEmpty(objerrors))
                    errorMessage += string.Format(Translations.ImportErrors, row[Translations.Location], "", objerrors) + Environment.NewLine;

                objs.Add(obj);
            }

            if (!string.IsNullOrEmpty(errorMessage))
                return new ImportResult(Translations.ImportErrorHeader + Environment.NewLine + errorMessage);

            foreach (var obj in objs)
                repo.Save(obj, userId);

            return new ImportResult
            {
                WasSuccess = true,
                Count = objs.Count,
                Message = string.Format(Translations.ImportSuccess, objs.Count)
            };
        }
        private ImportResult MapAndSaveObjectsCm(DataSet ds, int userId)
        {
            string errorMessage = "";
            List<DiseaseDistroCm> objs = new List<DiseaseDistroCm>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var obj = repo.CreateCm((DiseaseType)cmType.Disease.Id);

                obj.AdminLevelId = Convert.ToInt32(row[Translations.Location + "#"]);
                obj.Notes = row[Translations.Notes].ToString();
                obj.IndicatorValues = GetDynamicIndicatorValues(ds, row);

                var objerrors = !obj.IsValid() ? obj.GetAllErrors(true) : "";
                if (!string.IsNullOrEmpty(objerrors))
                    errorMessage += string.Format(Translations.ImportErrors, row[Translations.Location], "", objerrors) + Environment.NewLine;

                objs.Add(obj);
            }

            if (!string.IsNullOrEmpty(errorMessage))
                return new ImportResult(Translations.ImportErrorHeader + Environment.NewLine + errorMessage);

            foreach (var obj in objs)
                repo.Save(obj, userId);

            return new ImportResult
            {
                WasSuccess = true,
                Count = objs.Count,
                Message = string.Format(Translations.ImportSuccess, objs.Count)
            };
        }

    }
}
