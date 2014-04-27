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
using Nada.Model.Process;
using Nada.Model.Repositories;

namespace Nada.Model
{
    public class ProcessImporter : ImporterBase, IImporter
    {
        public override IndicatorEntityType EntityType { get { return IndicatorEntityType.Process; } }
        public override string ImportName
        {
            get
            {

                if (type != null)
                    return TranslationLookup.GetValue("ProcessIndicators") + " " + TranslationLookup.GetValue("Import") + " - " + type.TypeName.RemoveIllegalPathChars();
                else
                    return TranslationLookup.GetValue("ProcessIndicators") + " " + TranslationLookup.GetValue("Import");
            }
        }
        private ProcessRepository repo = new ProcessRepository();
        private ProcessType type = null;
        public ProcessImporter()
        {

        }
        protected override void SetSpecificType(int id)
        {
            type = repo.GetProcessType(id);
            Indicators = type.Indicators;
            DropDownValues = type.IndicatorDropdownValues;
        }

        protected override void ReloadDropdownValues()
        {
            type = repo.GetProcessType(type.Id);
            DropDownValues = type.IndicatorDropdownValues;
        }

        public override List<TypeListItem> GetAllTypes()
        {
            return repo.GetProcessTypes().Select(t => new TypeListItem
            {
                Id = t.Id,
                Name = t.TypeName
            }).ToList();
        }

        protected override ImportResult MapAndSaveObjects(DataSet ds, int userId)
        {
            string errorMessage = "";
            List<ProcessBase> objs = new List<ProcessBase>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row[TranslationLookup.GetValue("ID")] == null || row[TranslationLookup.GetValue("ID")].ToString().Length == 0)
                    continue;
                string objerrors = "";
                var obj = repo.Create(type.Id);
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
