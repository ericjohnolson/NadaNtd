using System;
using System.Configuration;
using System.Data.OleDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nada.Model;

namespace Nada.Tests
{
    [TestClass]
    public class CleanDatabase : BaseTest
    {
        [TestMethod]
        public void DoDelete()
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
                command = new OleDbCommand("Delete from InterventionLfMda", connection);
                command.ExecuteNonQuery();
                command = new OleDbCommand("Delete from InterventionIndicatorValues", connection);
                command.ExecuteNonQuery();
                command = new OleDbCommand("Delete from Interventions_to_Medicines", connection);
                command.ExecuteNonQuery();
                command = new OleDbCommand("Delete from Interventions_to_Partners", connection);
                command.ExecuteNonQuery();
                // DELETE Surveys
                command = new OleDbCommand("Delete from Surveys", connection);
                command.ExecuteNonQuery();
                command = new OleDbCommand("Delete from SurveyLfMf", connection);
                command.ExecuteNonQuery();
                command = new OleDbCommand("Delete from SurveyIndicatorValues", connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
