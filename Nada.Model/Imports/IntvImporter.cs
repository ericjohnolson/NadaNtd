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
        IntvRepository repo = new IntvRepository();
        private IntvType iType = null;
        public IntvImporter() {  }
        public override string ImportName { get { return Translations.Interventions + " " + Translations.Import; } }

        public override List<TypeListItem> GetAllTypes()
        {
            return repo.GetAllTypes().Select(t => new TypeListItem
            {
                Id = t.Id,
                Name = t.IntvTypeName
            }).ToList();
        }

        public override void SetType(int id)
        {
            iType = repo.GetIntvType(id);
            Indicators = iType.Indicators;
            DropDownValues = iType.IndicatorDropdownValues;
        }

        protected override ImportResult MapAndSaveObjects(DataSet ds, int userId)
        {
            string errorMessage = "";
            List<IntvBase> intvs = new List<IntvBase>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                IntvBase intv = repo.CreateIntv(iType.Id);
                intv.AdminLevelId = Convert.ToInt32(row[Translations.Location + "#"]);
                intv.Notes = row[Translations.Notes].ToString();
                intv.IndicatorValues = GetDynamicIndicatorValues(ds, row);
            
                var objerrors = !intv.IsValid() ? intv.GetAllErrors(true) : "";
                if (!string.IsNullOrEmpty(objerrors))
                    errorMessage += string.Format(Translations.ImportErrors, row[Translations.Location], "", objerrors) + Environment.NewLine;
                
                intvs.Add(intv);
            }

            if (!string.IsNullOrEmpty(errorMessage))
                return new ImportResult(Translations.ImportErrorHeader + Environment.NewLine + errorMessage);

            foreach (IntvBase obj in intvs)
                repo.SaveBase(obj, userId);

            return new ImportResult
            {
                WasSuccess = true,
                Count = intvs.Count,
                Message = string.Format(Translations.ImportSuccess, intvs.Count)
            };
        }

    }
}
