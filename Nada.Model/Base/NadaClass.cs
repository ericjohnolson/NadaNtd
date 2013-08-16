using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model.Base
{
    [Serializable]
    public class NadaClass
    {
        public NadaClass()
        {
            UpdatedBy = "Not Saved";
        }
        public int Id { get; set; }
        public int UpdatedById { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
