using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.DA;

namespace Nada.Model.Repositories
{
    public class StartUpStatus
    {
        public StartUpStatus()
        {
            AdminLevelTypes = new List<AdminLevelTypeStatus>();
        }
        public bool HasEnteredCountrySettings { get; set; }
        public bool HasEnteredDiseaseDetails { get; set; }
        public List<AdminLevelTypeStatus> AdminLevelTypes { get; set; }
        public bool ShouldShowStartup()
        {
            return !HasEnteredDiseaseDetails || !HasEnteredCountrySettings || AdminLevelTypes.FirstOrDefault(a => !a.HasEntered) != null;
        }
    }

    public class AdminLevelTypeStatus
    {
        public string LevelName { get; set; }
        public bool HasEntered { get; set; }
        public int Level { get; set; }
    }

    public class SettingsRepository
    {
        #region Shared
        public StartUpStatus GetStartUpStatus()
        {
            var status = new StartUpStatus();

            int id = Convert.ToInt32(ConfigurationManager.AppSettings["CountryId"]);
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select DisplayName, MonthYearStarts, HasReviewedDiseases 
                    FROM ((Country INNER JOIN AdminLevels on Country.AdminLevelId = AdminLevels.ID)
                        INNER JOIN AdminLevelDemography d on d.AdminLevelId = Country.AdminLevelId) 
                    WHERE AdminLevels.ID = @id", connection);
                command.Parameters.Add(new OleDbParameter("@id", id));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        status.HasEnteredCountrySettings = !string.IsNullOrEmpty(reader.GetValueOrDefault<string>("DisplayName")) &&
                                reader.GetValueOrDefault<int>("MonthYearStarts") > 0;
                    }
                    reader.Close();
                } 

                command = new OleDbCommand(@"Select HasReviewedDiseases FROM Country;", connection);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        status.HasEnteredDiseaseDetails = reader.GetValueOrDefault<bool>("HasReviewedDiseases");
                    }
                    reader.Close();
                }

                var adminLevels = GetAllAdminLevels();
                foreach(var adminLevel in adminLevels)
                {
                    command = new OleDbCommand(@"Select Count(ID) from AdminLevels where AdminLevelTypeId = @id", connection);
                    command.Parameters.Add(new OleDbParameter("@id", adminLevel.Id));
                    int total = (int)command.ExecuteScalar();
                    status.AdminLevelTypes.Add(new AdminLevelTypeStatus
                    {
                        LevelName = adminLevel.DisplayName,
                        HasEntered = total > 0 ? true : false,
                        Level = adminLevel.LevelNumber
                    });
                }
            }
            return status;
        }

        public void SetDiseasesReviewedStatus()
        {
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Update Country set HasReviewedDiseases=@HasReviewedDiseases", connection);
                    command.Parameters.Add(new OleDbParameter("@HasReviewedDiseases", true));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool ShouldDemoUpdate()
        {
            bool shouldUpdate = false;
            int id = Convert.ToInt32(ConfigurationManager.AppSettings["CountryId"]);
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select DisplayName, MonthYearStarts, YearDemographyData 
                    FROM ((Country INNER JOIN AdminLevels on Country.AdminLevelId = AdminLevels.ID)
                        INNER JOIN AdminLevelDemography d on d.AdminLevelId = Country.AdminLevelId) 
                    WHERE AdminLevels.ID = @id ORDER BY YearDemographyData DESC", connection);
                command.Parameters.Add(new OleDbParameter("@id", id));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        int lastYear = reader.GetValueOrDefault<int>("YearDemographyData");
                        int month = reader.GetValueOrDefault<int>("MonthYearStarts");
                        if (DateTime.Now > new DateTime(lastYear + 1, month, 1))
                            shouldUpdate = true;
                    }
                    reader.Close();
                }

            }
            return shouldUpdate;
        }

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
                string sql = @"Select ID, DisplayName, AdminLevel, IsDistrict, IsAggregatingLevel from AdminLevelTypes 
                    WHERE AdminLevel > 0 AND IsDeleted=0
                    ORDER BY AdminLevel";
                OleDbCommand command = new OleDbCommand(sql, connection);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        lvls.Add(new AdminLevelType
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                            LevelNumber = reader.GetValueOrDefault<int>("AdminLevel"),
                            IsDistrict = reader.GetValueOrDefault<bool>("IsDistrict"),
                            IsAggregatingLevel = reader.GetValueOrDefault<bool>("IsAggregatingLevel"),
                        });
                    reader.Close();
                }
            }
            return lvls;
        }
        public AdminLevelType GetAdminLevelTypeByLevel(int level)
        {
            AdminLevelType al = null;
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName, AdminLevel, IsDistrict, IsAggregatingLevel from AdminLevelTypes 
                    WHERE AdminLevel > 0 AND IsDeleted=0 AND AdminLevel = @level", connection);
                command.Parameters.Add(new OleDbParameter("@level", level));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        al = new AdminLevelType
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                            LevelNumber = reader.GetValueOrDefault<int>("AdminLevel"),
                            IsDistrict = reader.GetValueOrDefault<bool>("IsDistrict"),
                            IsAggregatingLevel = reader.GetValueOrDefault<bool>("IsAggregatingLevel"),
                        };
                    }
                    reader.Close();
                }

                if (al == null)
                    return null;

                command = new OleDbCommand(@"Select AdminLevel from AdminLevelTypes 
                    WHERE IsAggregatingLevel = @IsAggLevel", connection);
                command.Parameters.Add(new OleDbParameter("@IsAggLevel", true));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        int aggLevel = reader.GetValueOrDefault<int>("AdminLevel");
                        if (al.LevelNumber >= aggLevel)
                            al.IsDemographyAllowed = true;

                    }
                    reader.Close();
                }
            }
            return al;
        }

        public AdminLevelType GetNextLevel(int levelNumber)
        {
            AdminLevelType al = null;
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName, AdminLevel, IsDistrict, IsAggregatingLevel from AdminLevelTypes 
                    WHERE AdminLevel > 0 AND IsDeleted=0 AND AdminLevel > @id
                    ORDER BY AdminLevel", connection);
                command.Parameters.Add(new OleDbParameter("@id", levelNumber));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        al = new AdminLevelType
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                            LevelNumber = reader.GetValueOrDefault<int>("AdminLevel"),
                            IsDistrict = reader.GetValueOrDefault<bool>("IsDistrict"),
                            IsAggregatingLevel = reader.GetValueOrDefault<bool>("IsAggregatingLevel"),
                        };
                    }
                    reader.Close();
                }

                if (al == null)
                    return null;

                command = new OleDbCommand(@"Select AdminLevel from AdminLevelTypes 
                    WHERE IsAggregatingLevel = @IsAggregatingLevel", connection);
                command.Parameters.Add(new OleDbParameter("@IsAggregatingLevel", true));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        int aggLevel = reader.GetValueOrDefault<int>("AdminLevel");
                        if (al.LevelNumber >= aggLevel)
                            al.IsDemographyAllowed = true;

                    }
                    reader.Close();
                }
            }
            return al;
        }

        public void Delete(AdminLevelType adminLevelType, int userId)
        {
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Update AdminLevelTypes set IsDeleted=1, UpdatedById=@updatedby, 
                        UpdatedAt=@updatedat WHERE ID = @id", connection);
                        command.Parameters.Add(new OleDbParameter("@updatedby", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@updatedat", DateTime.Now));
                        command.Parameters.Add(new OleDbParameter("@id", adminLevelType.Id));
                        command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void Save(AdminLevelType adminLevel, int byUserId)
        {
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = null;

                    if (adminLevel.IsDistrict)
                    {
                        command = new OleDbCommand(@"Update AdminLevelTypes set IsDistrict=0", connection);
                        command.ExecuteNonQuery();
                    }

                    if (adminLevel.IsAggregatingLevel)
                    {
                        command = new OleDbCommand(@"Update AdminLevelTypes set IsAggregatingLevel=0", connection);
                        command.ExecuteNonQuery();
                    }

                    if (adminLevel.Id > 0)
                    {
                        command = new OleDbCommand(@"Update AdminLevelTypes set DisplayName=@name, AdminLevel=@AdminLevel, IsDistrict=@IsDistrict, 
                        IsAggregatingLevel=@IsAggregatingLevel, UpdatedById=@updatedby, UpdatedAt=@updatedat WHERE ID = @id", connection);
                        command.Parameters.Add(new OleDbParameter("@name", adminLevel.DisplayName));
                        command.Parameters.Add(new OleDbParameter("@AdminLevel", adminLevel.LevelNumber));
                        command.Parameters.Add(new OleDbParameter("@IsDistrict", adminLevel.IsDistrict));
                        command.Parameters.Add(new OleDbParameter("@IsAggregatingLevel", adminLevel.IsAggregatingLevel));
                        command.Parameters.Add(new OleDbParameter("@updatedby", byUserId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@updatedat", DateTime.Now));
                        command.Parameters.Add(new OleDbParameter("@id", adminLevel.Id));
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        // INSERT 
                        command = new OleDbCommand(@"INSERT INTO AdminLevelTypes (DisplayName, AdminLevel, IsDistrict, IsAggregatingLevel,
                            UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES
                            (@DisplayName, @AdminLevel, @IsDistrict, @IsAggregatingLevel, @UpdatedBy, @UpdatedAt, @CreatedById, @CreatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@DisplayName", adminLevel.DisplayName));
                        command.Parameters.Add(new OleDbParameter("@AdminLevel", adminLevel.LevelNumber));
                        command.Parameters.Add(new OleDbParameter("@IsDistrict", adminLevel.IsDistrict));
                        command.Parameters.Add(new OleDbParameter("@IsAggregatingLevel", adminLevel.IsAggregatingLevel));
                        command.Parameters.Add(new OleDbParameter("@UpdatedById", byUserId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.Parameters.Add(new OleDbParameter("@CreatedById", byUserId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
                        command.ExecuteNonQuery();
                    }
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
