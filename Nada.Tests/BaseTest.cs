using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Nada.Model;

namespace Nada.Tests
{
    public class BaseTest
    {
        public BaseTest()
        {
            ModelData.Instance.AccessConnectionString = ConfigurationManager.ConnectionStrings["AccessFileName"].ConnectionString;
        }
    }
}
