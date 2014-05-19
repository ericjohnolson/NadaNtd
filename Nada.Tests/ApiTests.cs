using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nada.Model;
using Nada.Model.Repositories;

namespace Nada.Tests
{
    [TestClass]
    public class ApiTests : BaseTest
    {
        [TestMethod]
        public void CanGetDistricts()
        {
            TaskForceApi api = new TaskForceApi();

            var result = api.GetAllDistricts("burkina faso");
            
            Assert.IsTrue(result.WasSuccessful);
        }

    }
}
