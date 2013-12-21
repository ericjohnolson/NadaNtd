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
        public override string ImportName { get { return Translations.ProcessIndicators + " " + Translations.Import; } }
        private ProcessRepository repo = new ProcessRepository();
        private ProcessType type = null;
        public ProcessImporter()
        {

        }
        public override void SetType(int id)
        {
           type = repo.GetProcessType(id);
           Indicators = type.Indicators;
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
                var obj = repo.Create(type.Id);
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
