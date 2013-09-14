using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Nada.Model
{
    [Serializable]
    public class AdminLevel : ICloneable
    {
        public AdminLevel()
        {
            Children = new List<AdminLevel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentId { get; set; }
        public List<AdminLevel> Children { get; set; }
        public string ViewText { get { return "Select"; } }
        public int LevelNumber { get; set; }
        public bool IsUrban { get; set; }
        public double LatWho { get; set; }
        public double LngWho { get; set; }
        public double LatOther { get; set; }
        public double LngOther { get; set; }


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
                IsUrban = this.IsUrban,
                LatWho = this.LatWho,
                LatOther = this.LatOther,
                LngOther = this.LngOther,
                LngWho = this.LngWho
            };
        }
    }
}
