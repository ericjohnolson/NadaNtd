using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.DA;

namespace Nada.Model.Repositories
{
    public class SettingsRepository
    {
        #region Shared
        public List<Language> GetSupportedLanguages()
        {
            List<Language> languages = new List<Language>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                string sql = @"Select IsoCode, DisplayName from Languages order by DisplayName";
                OleDbCommand command = new OleDbCommand(sql, connection);
                connection.Open();

                //command.Parameters.Add(new OleDbParameter("@Pass", pass));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        languages.Add(new Language
                        {
                            IsoCode = reader.GetValueOrDefault<string>("IsoCode"),
                            Name = reader.GetValueOrDefault<string>("DisplayName")
                        });
                    reader.Close();
                }
            }
            return languages;
        }
        #endregion

        #region Admin Levels
        public List<AdminLevelType> GetAllAdminLevels()
        {
            List<AdminLevelType> lvls = new List<AdminLevelType>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                string sql = @"Select ID, DisplayName, AdminLevel from AdminLevelTypes WHERE AdminLevel > 0";
                OleDbCommand command = new OleDbCommand(sql, connection);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        lvls.Add(new AdminLevelType
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                            LevelNumber = reader.GetValueOrDefault<int>("AdminLevel"),
                        });
                    reader.Close();
                }
            }
            return lvls;
        }

        public void InsertAdminLevel(AdminLevelType adminLevel, int byUserId)
        {
            bool transWasStarted = false;
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    OleDbCommand command = new OleDbCommand("BEGIN TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = true;

                    // INSERT 
                    command = new OleDbCommand(@"INSERT INTO AdminLevelTypes (DisplayName, AdminLevel, UpdatedById, UpdatedAt, CreatedById,
                    CreatedAt) VALUES
                    (@DisplayName, @AdminLevel, @UpdatedBy, @UpdatedAt, @CreatedById, @CreatedAt)", connection);
                    command.Parameters.Add(new OleDbParameter("@DisplayName", adminLevel.DisplayName));
                    command.Parameters.Add(new OleDbParameter("@AdminLevel", adminLevel.LevelNumber));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", byUserId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@CreatedById", byUserId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
                    command.ExecuteNonQuery();

                    // COMMIT TRANS
                    command = new OleDbCommand("COMMIT TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = false;
                }
                catch (Exception)
                {
                    if (transWasStarted)
                    {
                        try
                        {
                            OleDbCommand cmd = new OleDbCommand("ROLLBACK TRANSACTION", connection);
                            cmd.ExecuteNonQuery();
                        }
                        catch { }
                    }
                    throw;
                }
            }
        }

        public void UpdateAdminLevel(AdminLevelType adminLevel, int byUserId)
        {
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Update AdminLevelTypes set DisplayName=@name, AdminLevel=@AdminLevel, UpdatedById=@updatedby, 
                        UpdatedAt=@updatedat WHERE ID = @id", connection);
                    command.Parameters.Add(new OleDbParameter("@name", adminLevel.DisplayName));
                    command.Parameters.Add(new OleDbParameter("@AdminLevel", adminLevel.LevelNumber));
                    command.Parameters.Add(new OleDbParameter("@updatedby", byUserId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@updatedat", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", adminLevel.Id));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        #endregion

        #region Pop Groups
        public List<PopGroup> GetAllPopGroups()
        {
            List<PopGroup> pops = new List<PopGroup>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                string sql = @"Select ID, DisplayName, Abbreviation from PopulationGroups";
                OleDbCommand command = new OleDbCommand(sql, connection);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        pops.Add(new PopGroup
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                            Abbreviation = reader.GetValueOrDefault<string>("Abbreviation"),
                        });
                    reader.Close();
                }
            }
            return pops;
        }

        public void InsertPopGroup(PopGroup popGroup, int byUserId)
        {
            bool transWasStarted = false;
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    OleDbCommand command = new OleDbCommand("BEGIN TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = true;

                    // INSERT DISEASE
                    command = new OleDbCommand(@"INSERT INTO PopulationGroups (DisplayName, Abbreviation, UpdatedBy, UpdatedAt) VALUES
                    (@DisplayName, @Abbreviation, @UpdatedBy, @UpdatedAt)", connection);
                    command.Parameters.Add(new OleDbParameter("@DisplayName", popGroup.DisplayName));
                    command.Parameters.Add(new OleDbParameter("@Abbreviation", popGroup.Abbreviation));
                    command.Parameters.Add(new OleDbParameter("@UpdatedBy", byUserId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.ExecuteNonQuery();

                    // COMMIT TRANS
                    command = new OleDbCommand("COMMIT TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = false;
                }
                catch (Exception)
                {
                    if (transWasStarted)
                    {
                        try
                        {
                            OleDbCommand cmd = new OleDbCommand("ROLLBACK TRANSACTION", connection);
                            cmd.ExecuteNonQuery();
                        }
                        catch { }
                    }
                    throw;
                }
            }
        }

        public void UpdatePopGroup(PopGroup popGroup, int byUserId)
        {
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // Update DISEASE
                    OleDbCommand cmd2 = new OleDbCommand(@"UPDATE PopulationGroups SET DisplayName=@DisplayName, Abbreviation=@Abbreviation, UpdatedBy=@UpdatedBy, UpdatedAt=@UpdatedAt WHERE ID=@ID", connection);
                    cmd2.Parameters.Add(new OleDbParameter("@DisplayName", popGroup.DisplayName));
                    cmd2.Parameters.Add(new OleDbParameter("@Abbreviation", popGroup.Abbreviation));
                    cmd2.Parameters.Add(new OleDbParameter("@UpdatedBy", byUserId));
                    cmd2.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    cmd2.Parameters.Add(new OleDbParameter("@ID", popGroup.Id));
                    cmd2.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        #endregion
    }
}
