using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using Nada.DA;
using Nada.Globalization;

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

    public class SettingsRepository : RepositoryBase
    {
        Logger logger = new Logger();
        public SettingsRepository()
        {

        }
        #region Shared
        public StartUpStatus GetStartUpStatus()
        {
            var status = new StartUpStatus();

            int id = Convert.ToInt32(ConfigurationManager.AppSettings["CountryId"]);
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select MonthYearStarts
                    FROM Country ", connection);
                command.Parameters.Add(new OleDbParameter("@id", id));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        status.HasEnteredCountrySettings = reader.GetValueOrDefault<int>("MonthYearStarts") > 0;
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
                foreach (var adminLevel in adminLevels)
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
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
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
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select DisplayName, MonthYearStarts, DateDemographyData 
                    FROM ((Country INNER JOIN AdminLevels on Country.AdminLevelId = AdminLevels.ID)
                        INNER JOIN AdminLevelDemography d on d.AdminLevelId = Country.AdminLevelId) 
                    WHERE AdminLevels.ID = @id ORDER BY DateDemographyData DESC", connection);
                command.Parameters.Add(new OleDbParameter("@id", id));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        DateTime lastDate = reader.GetValueOrDefault<DateTime>("DateDemographyData");
                        int month = reader.GetValueOrDefault<int>("MonthYearStarts");
                        if (DateTime.Now > new DateTime(lastDate.Year + 1, month, 1))
                            shouldUpdate = true;
                    }
                    reader.Close();
                }

            }
            return shouldUpdate;
        }
        #endregion

        #region Admin Levels
        public List<AdminLevelType> GetAllAdminLevels()
        {
            List<AdminLevelType> lvls = new List<AdminLevelType>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
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

        public AdminLevelType GetDistrictAdminLevel()
        {
            AdminLevelType lvl = new AdminLevelType();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                string sql = @"Select ID, DisplayName, AdminLevel, IsDistrict, IsAggregatingLevel from AdminLevelTypes 
                    WHERE AdminLevel > 0 AND IsDeleted=0 AND IsDistrict=-1";
                OleDbCommand command = new OleDbCommand(sql, connection);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        lvl = new AdminLevelType
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                            LevelNumber = reader.GetValueOrDefault<int>("AdminLevel"),
                            IsDistrict = reader.GetValueOrDefault<bool>("IsDistrict"),
                            IsAggregatingLevel = reader.GetValueOrDefault<bool>("IsAggregatingLevel"),
                        };
                    reader.Close();
                }
            }
            return lvl;
        }

        public AdminLevelType GetAdminLevelTypeByLevel(int level)
        {
            AdminLevelType al = null;
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
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
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
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
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
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
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
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

                        command = new OleDbCommand(@"SELECT Max(ID) FROM AdminLevelTypes", connection);
                        adminLevel.Id = (int)command.ExecuteScalar();

                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        #endregion

        #region Related Entities

        public List<IndicatorDropdownValue> GetEvaluationUnits()
        {
            List<IndicatorDropdownValue> list = new List<IndicatorDropdownValue>();

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName, aspnet_users.UserName, UpdatedAt, CreatedAt, c.UserName as CreatedBy from 
                        ((EvaluationUnits INNER JOIN aspnet_users on EvaluationUnits.UpdatedById = aspnet_users.userid)
                        INNER JOIN aspnet_users c on EvaluationUnits.CreatedById = c.userid)
                        WHERE IsDeleted=0", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new IndicatorDropdownValue
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                                UpdatedBy = GetAuditInfo(reader),
                                EntityType = IndicatorEntityType.EvaluationUnit
                            });
                        }
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return list;
        }

        public void SaveEu(IndicatorDropdownValue eu, int userId)
        {
            bool transWasStarted = false;
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    OleDbCommand command = new OleDbCommand("BEGIN TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = true;

                    if (eu.Id > 0)
                        command = new OleDbCommand(@"UPDATE EvaluationUnits SET DisplayName=@DisplayName,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO EvaluationUnits (DisplayName, UpdatedById, 
                            UpdatedAt, CreatedById, CreatedAt) values (@DisplayName, @UpdatedById, @UpdatedAt, @CreatedById,
                            @CreatedAt)", connection);

                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@DisplayName", eu.DisplayName));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (eu.Id > 0)
                        command.Parameters.Add(new OleDbParameter("@id", eu.Id));
                    else
                    {
                        command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
                    }

                    command.ExecuteNonQuery();

                    if (eu.Id <= 0)
                    {
                        command = new OleDbCommand(@"SELECT Max(ID) FROM EvaluationUnits", connection);
                        eu.Id = (int)command.ExecuteScalar();
                    }

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


        public List<IndicatorDropdownValue> GetEcologicalZones()
        {
            List<IndicatorDropdownValue> list = new List<IndicatorDropdownValue>();

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName, aspnet_users.UserName, UpdatedAt, CreatedAt, c.UserName as CreatedBy from 
                        ((EcologicalZones INNER JOIN aspnet_users on EcologicalZones.UpdatedById = aspnet_users.userid)
                        INNER JOIN aspnet_users c on EcologicalZones.CreatedById = c.userid)
                        WHERE IsDeleted=0", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new IndicatorDropdownValue
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                                UpdatedBy = GetAuditInfo(reader),
                                EntityType = IndicatorEntityType.EcologicalZone
                            });
                        }
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return list;
        }

        public void SaveEz(IndicatorDropdownValue ez, int userId)
        {
            bool transWasStarted = false;
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    OleDbCommand command = new OleDbCommand("BEGIN TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = true;

                    if (ez.Id > 0)
                        command = new OleDbCommand(@"UPDATE EcologicalZones SET DisplayName=@DisplayName,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO EcologicalZones (DisplayName, UpdatedById, 
                            UpdatedAt, CreatedById, CreatedAt) values (@DisplayName, @UpdatedById, @UpdatedAt, @CreatedById,
                            @CreatedAt)", connection);

                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@DisplayName", ez.DisplayName));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (ez.Id > 0)
                        command.Parameters.Add(new OleDbParameter("@id", ez.Id));
                    else
                    {
                        command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
                    }

                    command.ExecuteNonQuery();

                    if (ez.Id <= 0)
                    {
                        command = new OleDbCommand(@"SELECT Max(ID) FROM EcologicalZones", connection);
                        ez.Id = (int)command.ExecuteScalar();
                    }

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

        public List<IndicatorDropdownValue> GetEvalSites()
        {
            List<IndicatorDropdownValue> list = new List<IndicatorDropdownValue>();

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName, aspnet_users.UserName, UpdatedAt, CreatedAt, c.UserName as CreatedBy from 
                        ((EvaluationSites INNER JOIN aspnet_users on EvaluationSites.UpdatedById = aspnet_users.userid)
                        INNER JOIN aspnet_users c on EvaluationSites.CreatedById = c.userid)
                        WHERE IsDeleted=0", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new IndicatorDropdownValue
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                                UpdatedBy = GetAuditInfo(reader),
                                EntityType = IndicatorEntityType.EvalSite
                            });
                        }
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return list;
        }

        public void SaveEvalSite(IndicatorDropdownValue ez, int userId)
        {
            bool transWasStarted = false;
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    OleDbCommand command = new OleDbCommand("BEGIN TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = true;

                    if (ez.Id > 0)
                        command = new OleDbCommand(@"UPDATE EvaluationSites SET DisplayName=@DisplayName,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO EvaluationSites (DisplayName, UpdatedById, 
                            UpdatedAt, CreatedById, CreatedAt) values (@DisplayName, @UpdatedById, @UpdatedAt, @CreatedById,
                            @CreatedAt)", connection);

                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@DisplayName", ez.DisplayName));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (ez.Id > 0)
                        command.Parameters.Add(new OleDbParameter("@id", ez.Id));
                    else
                    {
                        command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
                    }

                    command.ExecuteNonQuery();

                    if (ez.Id <= 0)
                    {
                        command = new OleDbCommand(@"SELECT Max(ID) FROM EvaluationSites", connection);
                        ez.Id = (int)command.ExecuteScalar();
                    }

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

        public List<IndicatorDropdownValue> GetEvalSubDistricts()
        {
            List<IndicatorDropdownValue> list = new List<IndicatorDropdownValue>();

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName, aspnet_users.UserName, UpdatedAt, CreatedAt, c.UserName as CreatedBy from 
                        ((EvalSubDistricts INNER JOIN aspnet_users on EvalSubDistricts.UpdatedById = aspnet_users.userid)
                        INNER JOIN aspnet_users c on EvalSubDistricts.CreatedById = c.userid)
                        WHERE IsDeleted=0", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new IndicatorDropdownValue
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                                UpdatedBy = GetAuditInfo(reader),
                                EntityType = IndicatorEntityType.EvalSubDistrict
                            });
                        }
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return list;
        }

        public void SaveEvalSubDistrict(IndicatorDropdownValue ez, int userId)
        {
            bool transWasStarted = false;
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    OleDbCommand command = new OleDbCommand("BEGIN TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = true;

                    if (ez.Id > 0)
                        command = new OleDbCommand(@"UPDATE EvalSubDistricts SET DisplayName=@DisplayName,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO EvalSubDistricts (DisplayName, UpdatedById, 
                            UpdatedAt, CreatedById, CreatedAt) values (@DisplayName, @UpdatedById, @UpdatedAt, @CreatedById,
                            @CreatedAt)", connection);

                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@DisplayName", ez.DisplayName));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (ez.Id > 0)
                        command.Parameters.Add(new OleDbParameter("@id", ez.Id));
                    else
                    {
                        command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
                    }

                    command.ExecuteNonQuery();

                    if (ez.Id <= 0)
                    {
                        command = new OleDbCommand(@"SELECT Max(ID) FROM EvalSubDistricts", connection);
                        ez.Id = (int)command.ExecuteScalar();
                    }

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
        #endregion

        #region Database Updates
        public string RunSchemaChangeScripts(List<string> files)
        {
            bool transWasStarted = false;

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);

            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    OleDbCommand command = new OleDbCommand("BEGIN TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = true;

                    foreach (string fileName in files)
                    {
                        FileInfo file = new FileInfo(fileName);
                        string script = file.OpenText().ReadToEnd();
                        string[] commands = script.Split(';');
                        foreach (string cmdText in commands)
                        {
                            if (cmdText.Trim().Length == 0)
                                continue;

                            command = new OleDbCommand(cmdText, connection);
                            command.ExecuteNonQuery();
                        }
                        file.OpenText().Close();
                    }

                    // COMMIT TRANS
                    command = new OleDbCommand("COMMIT TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    transWasStarted = false;
                    return "";
                }
                catch (Exception ex)
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
                    logger.Error("Error RunSchemaChangeScripts: " + ex.Message + "(" + String.Join(", ", files.ToArray()) + ")", ex); 
                    return TranslationLookup.GetValue("DatabaseScriptException") + ": " + ex.Message;
                }
            }
        }

        public List<string> GetSchemaChangeScripts(string scriptsDirectory)
        {
            List<string> filesToRun = new List<string>();
            string lastFileUpdated = "";
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand("Select top 1 ScriptName from SchemaChangeLog Order By ID DESC", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            lastFileUpdated = reader.GetValueOrDefault<string>("ScriptName");
                        }
                        reader.Close();
                    }
                    var files = from file in Directory.GetFiles(scriptsDirectory)
                                orderby file ascending
                                select file;

                    filesToRun = files.ToList();
                    filesToRun.RemoveAll(n => String.Compare(n, scriptsDirectory + lastFileUpdated) <= 0);
                    return filesToRun;
                }
                catch (OleDbException ex)
                {
                    if (ex.Message.Contains("SchemaChangeLog"))
                    {
                        RunSchemaChangeScripts(new List<string> { scriptsDirectory + "00SchemaChangeLog.sql" });
                        return GetSchemaChangeScripts(scriptsDirectory);
                    }
                    logger.Error("Error GetSchemaChangeScripts: " + ex.Message, ex); 
                }
                return new List<string>();
            }
        }
        #endregion
    }
}
