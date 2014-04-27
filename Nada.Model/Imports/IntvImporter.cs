using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Excel;
using Nada.Globalization;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Repositories;

namespace Nada.Model.Imports
{
    public class IntvImporter : ImporterBase, IImporter
    {
        public override IndicatorEntityType EntityType { get { return IndicatorEntityType.Intervention; } }
        IntvRepository repo = new IntvRepository();
        private IntvType iType = null;
        public IntvImporter() { }
        public override string ImportName
        {
            get
            {
                if (iType != null)
                    return TranslationLookup.GetValue("Interventions") + " " + TranslationLookup.GetValue("Import") + " - " + iType.IntvTypeName.RemoveIllegalPathChars();
                else
                    return TranslationLookup.GetValue("Interventions") + " " + TranslationLookup.GetValue("Import");
            }
        }

        public override List<TypeListItem> GetAllTypes()
        {
            return repo.GetAllTypes().Select(t => new TypeListItem
            {
                Id = t.Id,
                Name = t.IntvTypeName
            }).ToList();
        }

        protected override void SetSpecificType(int id)
        {
            iType = repo.GetIntvType(id);
            Indicators = iType.Indicators;
            DropDownValues = iType.IndicatorDropdownValues;
        }

        protected override void ReloadDropdownValues()
        {
            iType = repo.GetIntvType(iType.Id);
            DropDownValues = iType.IndicatorDropdownValues;
        }

        protected override ImportResult MapAndSaveObjects(DataSet ds, int userId)
        {
            string errorMessage = "";
            List<IntvBase> objs = new List<IntvBase>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row[TranslationLookup.GetValue("ID")] == null || row[TranslationLookup.GetValue("ID")].ToString().Length == 0)
                    continue;
                string objerrors = "";
                IntvBase obj = repo.CreateIntv(iType.Id);
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
