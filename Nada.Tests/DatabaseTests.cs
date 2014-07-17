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
    public class DatabaseTests : BaseTest
    {
        [TestMethod]
        public void DoUpdateDatabase()
        {
            SettingsRepository repo = new SettingsRepository();
            List<string> filesToRun = new List<string> { @"C:\Users\jed\Source\Repos\NadaNTD\Nada.UI\DatabaseScripts\ScriptDiffTest.sql" };
            //List<string> filesToRun = new List<string> { @"C:\Development\Nada\NadaNtd\Nada.UI\DatabaseScripts\ScriptDiffTest.sql" };
            string result = repo.RunSchemaChangeScripts(filesToRun);

            Assert.IsTrue(result.Length == 0);
        }

        //[TestMethod]
        public void CanCreateBaselineDatabase()
        {
            SettingsRepository repo = new SettingsRepository();
            List<string> filesToRun = repo.GetSchemaChangeScripts(@"C:\Development\Nada\NadaNtd\Nada.UI\DatabaseScripts\Differentials\");
            string result = repo.RunSchemaChangeScripts(filesToRun);

            Assert.IsTrue(result.Length == 0);
        }

        //[TestMethod]
        public void DoCleanDataOld()
        {
            // DON"T RUN UNLESS NECESSARY
            Assert.IsTrue(false);

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                // DELETE Interventions
                OleDbCommand command = new OleDbCommand("Delete from Interventions", connection);
                command.ExecuteNonQuery();
                command = new OleDbCommand("Delete from InterventionIndicatorValues", connection);
                command.ExecuteNonQuery();
                // DELETE Surveys
                command = new OleDbCommand("Delete from Surveys", connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
