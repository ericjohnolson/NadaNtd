﻿using System;
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
        public override string ImportName { get { return Translations.ProcessIndicators + " " + Translations.Import; } }
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

                string objerrors = "";
                var obj = repo.Create(type.Id);
                obj.AdminLevelId = Convert.ToInt32(row[Translations.Location + "#"]);
                obj.Notes = row[Translations.Notes].ToString();
                // Validation
                obj.IndicatorValues = GetDynamicIndicatorValues(ds, row, ref objerrors);
                objerrors += !obj.IsValid() ? obj.GetAllErrors(true) : "";
                errorMessage += GetObjectErrors(objerrors, row[Translations.Location].ToString());
                objs.Add(obj);
            }

            if (!string.IsNullOrEmpty(errorMessage))
                return new ImportResult(CreateErrorMessage(errorMessage));

            repo.Save(objs, userId);

            return new ImportResult
            {
                WasSuccess = true,
                Count = objs.Count,
                Message = string.Format(Translations.ImportSuccess, objs.Count)
            };
        }

    }
}
