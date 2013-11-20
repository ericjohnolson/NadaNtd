using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model
{
    [Serializable]
    public class AdminLevel : NadaClass, ICloneable
    {
        public AdminLevel()
        {
            Children = new List<AdminLevel>();
            LevelNumber = -1;
        }
        public string Name { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int AdminLevelTypeId { get; set; }
        public int LevelNumber { get; set; }
        public Nullable<double> LatWho { get; set; }
        public Nullable<double> LngWho { get; set; }
        public Nullable<double> LatOther { get; set; }
        public Nullable<double> LngOther { get; set; }
        public string UrbanOrRural { get; set; }
        public AdminLevelDemography CurrentDemography { get; set; }
        public List<AdminLevel> Children { get; set; }
        public string ViewText { get { return "Select"; } }

        #region IDataErrorInfo Members
        public override string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            error = Translations.Required;
                        break;
                    case "LevelNumber":
                        if (LevelNumber < 0)
                            error = Translations.Required;
                        break;

                    default: error = "";
                        break;

                }
                return error;
            }
        }

        #endregion

        public object Clone()
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, this);
            ms.Position = 0;
            object obj = bf.Deserialize(ms);
            ms.Close();
            return obj;
        }

        public AdminLevel CopyTreeNode()
        {
            return new AdminLevel
            {
                Id = this.Id,
                Name = this.Name,
                ParentId = this.ParentId,
                LevelNumber = this.LevelNumber,
                UrbanOrRural = this.UrbanOrRural,
                LatWho = this.LatWho,
                LatOther = this.LatOther,
                LngOther = this.LngOther,
                LngWho = this.LngWho
            };
        }
    }
}
