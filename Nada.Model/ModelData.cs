using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public class ModelData
    {
        private static ModelData instance;

        private ModelData() { }

        public string AccessConnectionString { get; set; }

        public static ModelData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ModelData();
                }
                return instance;
            }
        }
    }
}
