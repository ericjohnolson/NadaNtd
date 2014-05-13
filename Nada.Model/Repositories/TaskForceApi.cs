using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Nada.Model.Repositories
{
    public class TaskForceAdminUnit
    {
        public int Level { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> ParentNames { get; set; }

    }
    public class TaskForceApi
    {
        public List<TaskForceAdminUnit> GetAllCountries()
        {
            List<TaskForceAdminUnit> list = new List<TaskForceAdminUnit>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select TaskForceId, Name
                    FROM TaskForceCountries ORDER BY Name", connection);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TaskForceAdminUnit
                        {
                            Id = reader.GetValueOrDefault<int>("TaskForceId"),
                            Name = reader.GetValueOrDefault<string>("Name"),
                        });
                    }
                    reader.Close();
                }
            }
            return list;
        }
    }
}
