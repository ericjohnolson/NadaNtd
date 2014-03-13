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
        public override IndicatorEntityType EntityType { get { return IndicatorEntityType.DiseaseDistribution; } }
        public override string ImportName { get { return TranslationLookup.GetValue("DiseaseDistribution") + " " + TranslationLookup.GetValue("Import"); } }
        private DiseaseDistroPc type = null;
        private DiseaseDistroCm cmType = null;
        DiseaseRepository repo = new DiseaseRepository();
        protected override void SetSpecificType(int id)
        {
           var d = repo.GetDiseaseById(id);
           if (d.DiseaseType == Translations.CM)
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
                string objerrors = "";
                var obj = repo.Create((DiseaseType)type.Disease.Id);
                obj.AdminLevelId = Convert.ToInt32(row[TranslationLookup.GetValue("ID")]);
                obj.Notes = row[TranslationLookup.GetValue("Notes")].ToString();
                // Validation
                obj.IndicatorValues = GetDynamicIndicatorValues(ds, row, ref objerrors);
                objerrors += !obj.IsValid() ? obj.GetAllErrors(true) : "";
                errorMessage += GetObjectErrors(objerrors, row[TranslationLookup.GetValue("ID")].ToString());
                objs.Add(obj);
            }

            if (!string.IsNullOrEmpty(errorMessage))
                return new ImportResult(CreateErrorMessage(errorMessage));

            repo.Save(objs, userId);

            return new ImportResult
            {
                WasSuccess = true,
                Count = objs.Count,
                Message = string.Format(TranslationLookup.GetValue("ImportSuccess"), objs.Count)
            };
        }
        private ImportResult MapAndSaveObjectsCm(DataSet ds, int userId)
        {
            string errorMessage = "";
            List<DiseaseDistroCm> objs = new List<DiseaseDistroCm>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string objerrors = "";
                var obj = repo.CreateCm((DiseaseType)cmType.Disease.Id);
                obj.AdminLevelId = Convert.ToInt32(row[TranslationLookup.GetValue("ID")]);
                obj.Notes = row[TranslationLookup.GetValue("Notes")].ToString();
                // Validation
                obj.IndicatorValues = GetDynamicIndicatorValues(ds, row, ref objerrors);
                objerrors += !obj.IsValid() ? obj.GetAllErrors(true) : "";
                errorMessage += GetObjectErrors(objerrors, row[TranslationLookup.GetValue("ID")].ToString());
                objs.Add(obj);
            }

            if (!string.IsNullOrEmpty(errorMessage))
                return new ImportResult(CreateErrorMessage(errorMessage));

            repo.Save(objs, userId);

            return new ImportResult
            {
                WasSuccess = true,
                Count = objs.Count,
                Message = string.Format(TranslationLookup.GetValue("ImportSuccess"), objs.Count)
            };
        }

    }
}
