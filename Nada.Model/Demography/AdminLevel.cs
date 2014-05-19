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
            ViewText = Translations.Select;
        }
        public string Name { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int AdminLevelTypeId { get; set; }
        public Nullable<double> LatWho { get; set; }
        public Nullable<double> LngWho { get; set; }
        public string BindingLat { get; set; }
        public string BindingLng { get; set; }
        public string UrbanOrRural { get; set; }
        public AdminLevelDemography CurrentDemography { get; set; }
        public List<AdminLevel> Children { get; set; }
        public int RedistrictIdForMother { get; set; }
        public int TaskForceId { get; set; }
        private string _taskForceName = "";
        public string TaskForceName
        {
            get
            {
                return _taskForceName;
            }
            set
            {
                if (value != null)
                    _taskForceName = value;
            }
        }
        // Display Only
        public string LevelName { get; set; }
        public int LevelNumber { get; set; }
        public string ViewText { get; set; }

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
                    case "AdminLevelTypeId":
                        if (AdminLevelTypeId < 0)
                            error = Translations.Required;
                        break;
                    case "LatWho":
                        if (LatWho.HasValue && (LatWho.Value > 90 || LatWho.Value < -90))
                            error = Translations.ValidLatitude;
                        break;
                    case "LngWho":
                        if (LngWho.HasValue && (LngWho.Value > 180 || LngWho.Value < -180))
                            error = Translations.ValidLongitude;
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
                LngWho = this.LngWho
            };
        }

        public IEnumerable<AdminLevel> GetNodeAndDescendants() // Note that this method is lazy
        {
             return new[] { this }
                    .Concat(Children.SelectMany(child => child.GetNodeAndDescendants()));    
        }

        public static AdminLevel Find(List<AdminLevel> roots, int id)
        {
            foreach (var level in roots)
            {
                var result = level.GetNodeAndDescendants().FirstOrDefault(i => i.Id == id);
                if (result != null)
                    return result;
            }
            return null;
        }
    }
}
