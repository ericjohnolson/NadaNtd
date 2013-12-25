using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada
{
    public class DatabaseData
    {
        private static DatabaseData instance;

        private DatabaseData() { }

        public string AccessConnectionString { get; set; }

        public static DatabaseData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseData();
                }
                return instance;
            }
        }
    }
}
