using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.DA;
using Nada.Model.Csv;
using Nada.Model.Demography;

namespace Nada.Model.Repositories
{
    public class DemoRepository
    {
        #region Country
        public Country GetCountry()
        {
            Country country = new Country();
            int id = Convert.ToInt32(ConfigurationManager.AppSettings["CountryId"]);
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select Country.ID, DisplayName, MonthYearStarts, aspnet_Users.UserName, Country.UpdatedAt 
                    FROM ((Country INNER JOIN AdminLevels on Country.AdminLevelId = AdminLevels.ID)
                            INNER JOIN aspnet_Users on Country.UpdatedById = aspnet_Users.UserId)
                    WHERE AdminLevels.ID = @id", connection);
                command.Parameters.Add(new OleDbParameter("@id", id));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        country = new Country
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            Name = reader.GetValueOrDefault<string>("DisplayName"),
                            MonthYearStarts = reader.GetValueOrDefault<int>("MonthYearStarts"),
                            UpdatedBy = Util.GetAuditInfoUpdate(reader)
                        };
                    }
                    reader.Close();
                }
            }

            return country;
        }

        public void UpdateCountry(Country country, int byUserId)
        {
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Update Country set MonthYearStarts=@MonthYearStarts,
                         UpdatedById=@updatedby, UpdatedAt=@updatedat WHERE ID = @id", connection);
                    command.Parameters.Add(new OleDbParameter("@MonthYearStarts", country.MonthYearStarts));
                    command.Parameters.Add(new OleDbParameter("@updatedby", byUserId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@updatedat", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", country.Id));
                    command.ExecuteNonQuery();

                    command = new OleDbCommand(@"Update AdminLevels set DisplayName=@DisplayName, UpdatedById=@updatedby, UpdatedAt=@updatedat WHERE ID = @id", connection);
                    command.Parameters.Add(new OleDbParameter("@DisplayName", country.Name));
                    command.Parameters.Add(new OleDbParameter("@updatedby", byUserId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@updatedat", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", country.Id));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public CountryDemography GetCountryDemoRecent()
        {
            CountryDemography demo = new CountryDemography();
            demo.AdminLevelId = Convert.ToInt32(ConfigurationManager.AppSettings["CountryId"]);
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    int demoId = 0;
                    OleDbCommand command = new OleDbCommand(@"Select ID FROM AdminLevelDemography WHERE AdminLevelId=@id
                        ORDER BY YearDemographyData DESC", connection);
                    command.Parameters.Add(new OleDbParameter("@id", demo.AdminLevelId));

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            demoId = reader.GetValueOrDefault<int>("ID");
                        }
                        reader.Close();
                    }

                    if (demoId == 0)
                        return demo;

                    // Get existing
                    GetDemoById(demo, demoId, connection, command);

                    command = new OleDbCommand(@"Select AgeRangePsac, AgeRangeSac, Percent6mos, PercentPsac, PercentSac,  Percent5yo,  
                        PercentFemale, PercentMale, PercentAdult
                        FROM CountryDemography WHERE AdminLevelDemographyId=@AdminLevelDemographyId", connection);
                    command.Parameters.Add(new OleDbParameter("@AdminLevelDemographyId", demo.Id));

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            demo.AgeRangePsac = reader.GetValueOrDefault<string>("AgeRangePsac");
                            demo.AgeRangeSac = reader.GetValueOrDefault<string>("AgeRangeSac");
                            demo.Percent6mos = reader.GetValueOrDefault<Nullable<double>>("Percent6mos");
                            demo.PercentPsac = reader.GetValueOrDefault<Nullable<double>>("PercentPsac");
                            demo.PercentSac = reader.GetValueOrDefault<Nullable<double>>("PercentSac");
                            demo.Percent5yo = reader.GetValueOrDefault<Nullable<double>>("Percent5yo");
                            demo.PercentFemale = reader.GetValueOrDefault<Nullable<double>>("PercentFemale");
                            demo.PercentMale = reader.GetValueOrDefault<Nullable<double>>("PercentMale");
                            demo.PercentAdult = reader.GetValueOrDefault<Nullable<double>>("PercentAdult");

                        }
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return demo;
        }

        public void Save(CountryDemography demo, int byUserId)
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

                    SaveCountryDemoTransactional(demo, byUserId, connection, command);

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

        private void SaveCountryDemoTransactional(CountryDemography demo, int byUserId, OleDbConnection connection, OleDbCommand command)
        {
            int countryAdminId = Convert.ToInt32(ConfigurationManager.AppSettings["CountryId"]);
            if (demo.Id > 0)
                command = new OleDbCommand(@"UPDATE AdminLevelDemography SET AdminLevelId=@AdminLevelId, YearDemographyData=@YearDemographyData,
                            YearCensus=@YearCensus,  YearProjections=@YearProjections, GrowthRate=@GrowthRate, PercentRural=@PercentRural, 
                            Notes=@Notes, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
            else
                command = new OleDbCommand(@"INSERT INTO AdminLevelDemography (AdminLevelId, YearDemographyData,
                            YearCensus,  YearProjections, GrowthRate, PercentRural, Notes, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
                            values (@AdminLevelId, @YearDemographyData, @YearCensus,  @YearProjections, @GrowthRate, @PercentRural, @Notes, 
                            @UpdatedById, @UpdatedAt, @CreatedById, @CreatedAt)", connection);
            command.Parameters.Add(new OleDbParameter("@AdminLevelId", demo.AdminLevelId));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@YearDemographyData", demo.YearDemographyData));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@YearCensus", demo.YearCensus));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@YearProjections", demo.YearProjections));
            command.Parameters.Add(new OleDbParameter("@GrowthRate", demo.GrowthRate));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@PercentRural", demo.PercentRural));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@Notes", demo.Notes));
            command.Parameters.Add(new OleDbParameter("@UpdatedById", byUserId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));

            if (demo.Id > 0)
                command.Parameters.Add(new OleDbParameter("@id", demo.Id));
            else
            {
                command.Parameters.Add(new OleDbParameter("@CreatedById", byUserId));
                command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
            }
            command.ExecuteNonQuery();

            if (demo.Id <= 0)
            {
                command = new OleDbCommand(@"SELECT Max(ID) FROM AdminLevelDemography", connection);
                demo.Id = (int)command.ExecuteScalar();
            }

            command = new OleDbCommand(@"Delete from CountryDemography WHERE AdminLevelDemographyId=@id", connection);
            command.Parameters.Add(new OleDbParameter("@id", demo.Id));
            command.ExecuteNonQuery();

            command = new OleDbCommand(@"INSERT INTO CountryDemography (AdminLevelDemographyId, AgeRangePsac,
                        AgeRangeSac, Percent6mos, PercentPsac, PercentSac,  Percent5yo,  PercentFemale,PercentMale, PercentAdult) 
                            values (@AdminLevelDemographyId, @AgeRangePsac,
                        @AgeRangeSac, @Percent6mos, @PercentPsac, @PercentSac, @Percent5yo, @PercentFemale, @PercentMale, 
                        @PercentAdult)", connection);
            command.Parameters.Add(new OleDbParameter("@AdminLevelDemographyId", demo.Id));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@AgeRangePsac", demo.AgeRangePsac));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@AgeRangeSac", demo.AgeRangeSac));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@Percent6mos", demo.Percent6mos));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@PercentPsac", demo.PercentPsac));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@PercentSac", demo.PercentSac));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@Percent5yo", demo.Percent5yo));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@PercentFemale", demo.PercentFemale));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@PercentMale", demo.PercentMale));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@PercentAdult", demo.PercentAdult));
            command.ExecuteNonQuery();
        }
        #endregion

        #region AdminLevel
        public List<AdminLevel> GetAdminLevelChildren(int id)
        {
            List<AdminLevel> list = new List<AdminLevel>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, IsUrban, LatWho, LngWho, LatOther, LngOther,
                    AdminLevelTypes.AdminLevel
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE ParentId = @ParentId", connection);
                command.Parameters.Add(new OleDbParameter("@ParentId", id));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AdminLevel
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            ParentId = reader.GetValueOrDefault<Nullable<int>>("ParentId"),
                            Name = reader.GetValueOrDefault<string>("DisplayName"),
                            LevelNumber = reader.GetValueOrDefault<int>("AdminLevel"),
                            UrbanOrRural = reader.GetValueOrDefault<string>("UrbanOrRural"),
                            LatWho = reader.GetValueOrDefault<double>("LatWho"),
                            LngWho = reader.GetValueOrDefault<double>("LngWho"),
                            LatOther = reader.GetValueOrDefault<double>("LatOther"),
                            LngOther = reader.GetValueOrDefault<double>("LngOther"),
                        });
                    }
                    reader.Close();
                }
            }
            return list;
        }

        public List<AdminLevel> GetAdminLevelByLevel(int levelNumber)
        {
            List<AdminLevel> list = new List<AdminLevel>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, AdminLevelTypes.AdminLevel
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE AdminLevelTypes.AdminLevel = @LevelNumber", connection);
                command.Parameters.Add(new OleDbParameter("@LevelNumber", levelNumber));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AdminLevel
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            ParentId = reader.GetValueOrDefault<Nullable<int>>("ParentId"),
                            Name = reader.GetValueOrDefault<string>("DisplayName"),
                            LevelNumber = reader.GetValueOrDefault<int>("AdminLevel")
                        });
                    }
                    reader.Close();
                }
            }
            return list;
        }

        public AdminLevel GetAdminLevelById(int id)
        {
            AdminLevel al = new AdminLevel();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, UrbanOrRural, LatWho, LngWho, LatOther, LngOther,
                    AdminLevelTypes.AdminLevel
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE AdminLevels.ID = @id", connection);
                command.Parameters.Add(new OleDbParameter("@id", id));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        al = new AdminLevel
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            ParentId = reader.GetValueOrDefault<Nullable<int>>("ParentId"),
                            Name = reader.GetValueOrDefault<string>("DisplayName"),
                            LevelNumber = reader.GetValueOrDefault<int>("AdminLevel"),
                            UrbanOrRural = reader.GetValueOrDefault<string>("UrbanOrRural"),
                            LatWho = reader.GetValueOrDefault<double>("LatWho"),
                            LngWho = reader.GetValueOrDefault<double>("LngWho"),
                            LatOther = reader.GetValueOrDefault<double>("LatOther"),
                            LngOther = reader.GetValueOrDefault<double>("LngOther"),
                        };
                    }
                    reader.Close();
                }
            }
            return al;
        }

        public List<AdminLevel> GetAdminLevelTree()
        {
            List<AdminLevel> list = new List<AdminLevel>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, UrbanOrRural, LatWho, LngWho, LatOther, LngOther,
                    AdminLevelTypes.AdminLevel
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE AdminLevels.IsDeleted = 0
                    ", connection); // WHERE ParentId > 0
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AdminLevel
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            ParentId = reader.GetValueOrDefault<Nullable<int>>("ParentId"),
                            Name = reader.GetValueOrDefault<string>("DisplayName"),
                            LevelNumber = reader.GetValueOrDefault<int>("AdminLevel"),
                            UrbanOrRural = reader.GetValueOrDefault<string>("UrbanOrRural"),
                            LatWho = reader.GetValueOrDefault<double>("LatWho"),
                            LngWho = reader.GetValueOrDefault<double>("LngWho"),
                            LatOther = reader.GetValueOrDefault<double>("LatOther"),
                            LngOther = reader.GetValueOrDefault<double>("LngOther"),
                        });
                    }
                    reader.Close();
                }
            }
            return MakeTreeFromFlatList(list, 0);
        }

        public List<AdminLevel> GetAdminLevelTree(int levelTypeId)
        {
            List<AdminLevel> list = new List<AdminLevel>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, UrbanOrRural, LatWho, LngWho, LatOther, LngOther,
                    AdminLevelTypes.AdminLevel
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE ParentId > 0 AND AdminLevelTypeId <= @AdminLevelTypeId AND AdminLevels.IsDeleted = 0", connection);
                command.Parameters.Add(new OleDbParameter("@AdminLevelTypeId", levelTypeId));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AdminLevel
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            ParentId = reader.GetValueOrDefault<Nullable<int>>("ParentId"),
                            Name = reader.GetValueOrDefault<string>("DisplayName"),
                            LevelNumber = reader.GetValueOrDefault<int>("AdminLevel"),
                            UrbanOrRural = reader.GetValueOrDefault<string>("UrbanOrRural"),
                            LatWho = reader.GetValueOrDefault<double>("LatWho"),
                            LngWho = reader.GetValueOrDefault<double>("LngWho"),
                            LatOther = reader.GetValueOrDefault<double>("LatOther"),
                            LngOther = reader.GetValueOrDefault<double>("LngOther"),
                        });
                    }
                    reader.Close();
                }
            }
            return MakeTreeFromFlatList(list, 1);
        }

        public List<AdminLevel> GetAdminLevelTreeForDemography(int level, int demoYear)
        {
            List<AdminLevel> list = new List<AdminLevel>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, UrbanOrRural, LatWho, LngWho, LatOther, LngOther,
                    AdminLevelTypes.AdminLevel, AdminLevelTypeId
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE AdminLevel <= @AdminLevel AND AdminLevels.IsDeleted = 0", connection);
                command.Parameters.Add(new OleDbParameter("@AdminLevel", level));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var al = new AdminLevel
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            ParentId = reader.GetValueOrDefault<Nullable<int>>("ParentId"),
                            Name = reader.GetValueOrDefault<string>("DisplayName"),
                            LevelNumber = reader.GetValueOrDefault<int>("AdminLevel"),
                            UrbanOrRural = reader.GetValueOrDefault<string>("UrbanOrRural"),
                            LatWho = reader.GetValueOrDefault<Nullable<double>>("LatWho"),
                            LngWho = reader.GetValueOrDefault<Nullable<double>>("LngWho"),
                            LatOther = reader.GetValueOrDefault<Nullable<double>>("LatOther"),
                            LngOther = reader.GetValueOrDefault<Nullable<double>>("LngOther"),
                            AdminLevelTypeId = reader.GetValueOrDefault<int>("AdminLevelTypeId")
                        };
                        al.CurrentDemography = GetDemoByAdminLevelIdAndYear(al.Id, demoYear);
                        list.Add(al);

                    }
                    reader.Close();
                }
            }
            return MakeTreeFromFlatList(list, 0);
        }

        private List<AdminLevel> MakeTreeFromFlatList(IEnumerable<AdminLevel> flatList, int minRoot)
        {
            var dic = flatList.ToDictionary(n => n.Id, n => n);
            var rootNodes = new List<AdminLevel>();
            foreach (var node in flatList)
            {
                if (node.ParentId.HasValue && node.ParentId.Value > minRoot)
                {
                    AdminLevel parent = dic[node.ParentId.Value];
                    parent.Children.Add(node);
                }
                else
                {
                    rootNodes.Add(node);
                }
            }
            return rootNodes;
        }

        public void AddChildren(AdminLevel parent, List<AdminLevel> children, AdminLevelType childType, int byUserId)
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

                    foreach (var child in children)
                    {
                        command = new OleDbCommand(@"Insert Into AdminLevels (DisplayName, AdminLevelTypeId, 
                        ParentId, UrbanOrRural, LatWho, LngWho, LatOther, LngOther, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES
                        (@DisplayName, @AdminLevelTypeId, @ParentId, @UrbanOrRural, @LatWho, @LngWho, 
                         @LatOther, @LngOther, @updatedby, @updatedat, @CreatedById, @CreatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@DisplayName", child.Name));
                        command.Parameters.Add(new OleDbParameter("@AdminLevelTypeId", childType.Id));
                        command.Parameters.Add(new OleDbParameter("@ParentId", parent.Id));
                        command.Parameters.Add(new OleDbParameter("@UrbanOrRural", child.UrbanOrRural));
                        command.Parameters.Add(new OleDbParameter("@LatWho", child.LatWho));
                        command.Parameters.Add(new OleDbParameter("@LngWho", child.LngWho));
                        command.Parameters.Add(new OleDbParameter("@LatOther", child.LatOther));
                        command.Parameters.Add(new OleDbParameter("@LngOther", child.LngOther));
                        command.Parameters.Add(new OleDbParameter("@updatedby", byUserId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@updatedat", DateTime.Now));
                        command.Parameters.Add(new OleDbParameter("@CreatedById", byUserId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
                        command.ExecuteNonQuery();
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

        public void BulkImportAdminLevelsForLevel(List<AdminLevel> levels, int typeId, int byUserId)
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

                    // DELETE ALL EXISTING (NEED TO PROMPT IF THEY WANT TO?)
//                    command = new OleDbCommand(@"Update AdminLevels set IsDeleted=1, UpdatedById=@UpdatedBy, 
//                    UpdatedAt=@UpdatedAt WHERE AdminLevelTypeId=@AdminLevelTypeId", connection);
//                    command.Parameters.Add(new OleDbParameter("@UpdatedBy", byUserId));
//                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
//                    command.Parameters.Add(new OleDbParameter("@AdminLevelTypeId", typeId));
//                    command.ExecuteNonQuery();

                    foreach (var adminLevel in levels)
                        adminLevel.Id = InsertAdminLevelHelper(command, adminLevel, connection, byUserId);

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

        public void BulkImportAdminLevels(DataSet ds, int byUserId)
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

                    command = new OleDbCommand(@"Update AdminLevels set IsDeleted=1, UpdatedBy=@UpdatedBy, 
                    UpdatedAt=@UpdatedAt WHERE ID > 1", connection);
                    command.Parameters.Add(new OleDbParameter("@UpdatedBy", byUserId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.ExecuteNonQuery();


                    Dictionary<string, int> regions = new Dictionary<string, int>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (!regions.ContainsKey(row["Region"].ToString()))
                        {
                            AdminLevel region = new AdminLevel
                            {
                                LevelNumber = 2,
                                Name = row["Region"].ToString(),
                                ParentId = 1
                            };
                            int id = InsertAdminLevelHelper(command, region, connection, byUserId);
                            regions.Add(row["Region"].ToString(), id);
                        }

                        AdminLevel district = new AdminLevel
                        {
                            LevelNumber = 3,
                            Name = row["District"].ToString(),
                            ParentId = regions[row["Region"].ToString()]
                        };
                        InsertAdminLevelHelper(command, district, connection, byUserId);
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

        public Dictionary<string, int> GetParentIds(AdminLevel filterBy, AdminLevelType parentType)
        {
            Dictionary<string, int> parentIds = new Dictionary<string, int>();
            if (filterBy == null && parentType == null)
                return parentIds;

            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand();

                    if (filterBy != null)
                    {
                        command = new OleDbCommand(@"Select AdminLevels.ID, AdminLevels.DisplayName
                            FROM AdminLevels WHERE ParentId=@ParentId AND IsDeleted=0", connection);
                        command.Parameters.Add(new OleDbParameter("@ParentId", filterBy.Id));
                    }
                    else if (parentType != null)
                    {
                        command = new OleDbCommand(@"Select AdminLevels.ID, AdminLevels.DisplayName
                            FROM AdminLevels  WHERE AdminLevelTypeId = @AdminLevelTypeId AND IsDeleted=0", connection);
                        command.Parameters.Add(new OleDbParameter("@AdminLevelTypeId", parentType.Id));
                    }

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            parentIds.Add(reader.GetValueOrDefault<string>("DisplayName"), reader.GetValueOrDefault<int>("ID"));
                        }
                        reader.Close();
                    }

                    return parentIds;

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private int InsertAdminLevelHelper(OleDbCommand command, AdminLevel adminLevel, OleDbConnection connection, int userId)
        {
            command = new OleDbCommand(@"Insert Into AdminLevels (DisplayName, AdminLevelTypeId, 
                        ParentId, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES
                        (@DisplayName, @AdminLevelTypeId, @ParentId, @UpdatedBy, @UpdatedAt, @CreatedById, @CreatedAt)", connection);
            command.Parameters.Add(new OleDbParameter("@DisplayName", adminLevel.Name));
            command.Parameters.Add(new OleDbParameter("@AdminLevelTypeId", adminLevel.AdminLevelTypeId));
            command.Parameters.Add(new OleDbParameter("@ParentId", adminLevel.ParentId));
            command.Parameters.Add(new OleDbParameter("@UpdatedBy", userId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
            command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
            command.ExecuteNonQuery();

            command = new OleDbCommand(@"SELECT Max(ID) FROM AdminLevels", connection);
            int id = (int)command.ExecuteScalar();

            if (adminLevel.CurrentDemography != null)
            {
                adminLevel.CurrentDemography.AdminLevelId = id;
                SaveAdminDemography(command, connection, adminLevel.CurrentDemography, userId);
            }
            return id;
        }
        #endregion

        #region Demography
        public void AggregateUp(AdminLevelType locationType, int yearDemo, int userId)
        {
            try
            {
                var tree = GetAdminLevelTreeForDemography(locationType.LevelNumber, yearDemo);
                var country = tree.FirstOrDefault();
                country.CurrentDemography = IndicatorAggregator.AggregateTree(country);
                BulkImportAggregatedDemo(tree, userId, locationType.LevelNumber);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ApplyGrowthRate(double growthRatePercent, int userId, AdminLevelType aggLevel, int maxLevels, int year)
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
                    var growthRateDemonminator = 1 + (growthRatePercent / 100);
                    var demo = GetCountryDemoRecent();
                    int recentYear = demo.YearDemographyData.Value;
                    demo.Id = 0;
                    demo.GrowthRate = growthRatePercent;
                    demo.YearDemographyData = year;
                    SaveCountryDemoTransactional(demo, userId, connection, command);

                    // Get Agg Level & Below and create new demo
                    for (int i = aggLevel.LevelNumber; i <= maxLevels; i++)
                    {
                        List<AdminLevelDemography> existing = GetRecentDemographyByLevel(i, recentYear, command, connection);
                        foreach (var d in existing)
                        {
                            d.Id = 0;
                            d.YearDemographyData = year;
                            d.GrowthRate = growthRatePercent;
                            if (d.Pop0Month.HasValue) d.Pop0Month = Convert.ToInt32(d.Pop0Month * growthRateDemonminator);
                            if (d.Pop5yo.HasValue) d.Pop5yo = Convert.ToInt32(d.Pop5yo * growthRateDemonminator);
                            if (d.PopAdult.HasValue) d.PopAdult = Convert.ToInt32(d.PopAdult * growthRateDemonminator);
                            if (d.PopFemale.HasValue) d.PopFemale = Convert.ToInt32(d.PopFemale * growthRateDemonminator);
                            if (d.PopMale.HasValue) d.PopMale = Convert.ToInt32(d.PopMale * growthRateDemonminator);
                            if (d.PopPsac.HasValue) d.PopPsac = Convert.ToInt32(d.PopPsac * growthRateDemonminator);
                            if (d.PopPsac.HasValue) d.PopSac = Convert.ToInt32(d.PopSac * growthRateDemonminator);
                            if (d.PopPsac.HasValue) d.TotalPopulation = Convert.ToInt32(d.TotalPopulation * growthRateDemonminator);
                            SaveAdminDemography(command, connection, d, userId);
                        }
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

        public void Delete(DemoDetails demo, int userId)
        {
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"UPDATE AdminLevelDemography SET IsDeleted=@IsDeleted,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    command.Parameters.Add(new OleDbParameter("@IsDeleted", true));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", demo.Id));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<DemoDetails> GetAdminLevelDemography(int id)
        {
            List<DemoDetails> demo = new List<DemoDetails>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select a.ID, a.YearDemographyData, a.GrowthRate, a.TotalPopulation, a.UpdatedAt, 
                    aspnet_Users.UserName, created.UserName as CreatedBy, a.CreatedAt
                    FROM ((AdminLevelDemography a INNER JOIN aspnet_Users on a.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN aspnet_Users created on a.CreatedById = created.UserId)
                    WHERE AdminLevelId = @id and IsDeleted = 0", connection);
                command.Parameters.Add(new OleDbParameter("@id", id));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        demo.Add(new DemoDetails
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            Year = reader.GetValueOrDefault<int>("YearDemographyData"),
                            GrowthRate = reader.GetValueOrDefault<double>("GrowthRate"),
                            TotalPopulation = reader.GetValueOrDefault<int>("TotalPopulation"),
                            UpdatedBy = Util.GetAuditInfo(reader)
                        });
                    }
                    reader.Close();
                }
            }
            return demo;
        }

        public List<AdminLevelDemography> GetRecentDemography(int level, int recentYear)
        {
            List<AdminLevelDemography> recent = new List<AdminLevelDemography>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand();
                    recent = GetRecentDemographyByLevel(level, recentYear, command, connection);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return recent;
        }

        private List<AdminLevelDemography> GetRecentDemographyByLevel(int level, int recentYear, OleDbCommand command, OleDbConnection connection)
        {
            List<AdminLevelDemography> demo = new List<AdminLevelDemography>();
            command = new OleDbCommand(@"SELECT MAX(AdminLevelDemography.ID) as MID, MAX(AdminLevels.DisplayName) as MName
                    FROM ((AdminLevelDemography INNER JOIN AdminLevels on AdminLevelDemography.AdminLevelId = AdminLevels.ID)
                            INNER JOIN AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID)
                    WHERE AdminLevelTypes.AdminLevel=@lvl AND AdminLevelDemography.YearDemographyData=@Year
                    GROUP BY AdminLevelDemography.AdminLevelId", connection);
            command.Parameters.Add(new OleDbParameter("@lvl", level));
            command.Parameters.Add(new OleDbParameter("@Year", recentYear));
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    AdminLevelDemography d = new AdminLevelDemography();
                    GetDemoById(d, reader.GetValueOrDefault<int>("MID"), connection, command);
                    d.NameDisplayOnly = reader.GetValueOrDefault<string>("MName");
                    demo.Add(d);
                }
                reader.Close();
            }
            return demo;
        }

        private AdminLevelDemography GetDemoByAdminLevelIdAndYear(int adminLevelid, int demoYear)
        {
            AdminLevelDemography demo = new AdminLevelDemography { YearDemographyData = demoYear, AdminLevelId = adminLevelid };
            int id = 0;
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    OleDbCommand command = new OleDbCommand(@"Select ID
                        FROM AdminLevelDemography
                        WHERE AdminLevelId = @id and IsDeleted = 0 AND YearDemographyData = @Year", connection);
                    command.Parameters.Add(new OleDbParameter("@id", adminLevelid));
                    command.Parameters.Add(new OleDbParameter("@Year", demoYear));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            id = reader.GetValueOrDefault<int>("ID");
                        }
                    }
                    if (id > 0)
                        GetDemoById(demo, id, connection, command);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return demo;
        }

        public AdminLevelDemography GetDemoById(int id)
        {
            AdminLevelDemography demo = new AdminLevelDemography();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    OleDbCommand command = new OleDbCommand();
                    GetDemoById(demo, id, connection, command);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return demo;
        }

        private void GetDemoById(AdminLevelDemography demo, int id, OleDbConnection connection, OleDbCommand command)
        {
            command = new OleDbCommand(@"Select a.AdminLevelId, a.YearDemographyData,
                            a.YearCensus,  a.YearProjections, a.GrowthRate, a.PercentRural, a.TotalPopulation, a.AdultPopulation, a.Pop0Month, a.PopPsac, 
                            a.PopSac, a.Pop5yo, a.PopAdult, a.PopFemale, a.PopMale, a.Notes, a.UpdatedById, a.UpdatedAt, aspnet_Users.UserName, 
                        AdminLevels.DisplayName, a.CreatedAt, c.UserName as CreatedBy
                        FROM (((AdminLevelDemography a INNER JOIN aspnet_Users on a.UpdatedById = aspnet_Users.UserId)
                            LEFT OUTER JOIN AdminLevels on a.AdminLevelId = AdminLevels.ID)
                            INNER JOIN aspnet_Users c on a.CreatedById = c.UserId)
                        WHERE a.ID=@id", connection);
            command.Parameters.Add(new OleDbParameter("@id", id));
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    demo.Id = id;
                    demo.AdminLevelId = reader.GetValueOrDefault<int>("AdminLevelId");
                    demo.YearDemographyData = reader.GetValueOrDefault<Nullable<int>>("YearDemographyData");
                    demo.YearCensus = reader.GetValueOrDefault<Nullable<int>>("YearCensus");
                    demo.YearProjections = reader.GetValueOrDefault<Nullable<int>>("YearProjections");
                    demo.GrowthRate = reader.GetValueOrDefault<double>("GrowthRate");
                    demo.PercentRural = reader.GetValueOrDefault<Nullable<double>>("PercentRural");
                    demo.TotalPopulation = reader.GetValueOrDefault<Nullable<int>>("TotalPopulation");
                    demo.Pop0Month = reader.GetValueOrDefault<Nullable<int>>("Pop0Month");
                    demo.PopPsac = reader.GetValueOrDefault<Nullable<int>>("PopPsac");
                    demo.PopSac = reader.GetValueOrDefault<Nullable<int>>("PopSac");
                    demo.Pop5yo = reader.GetValueOrDefault<Nullable<int>>("Pop5yo");
                    demo.PopAdult = reader.GetValueOrDefault<Nullable<int>>("PopAdult");
                    demo.PopFemale = reader.GetValueOrDefault<Nullable<int>>("PopFemale");
                    demo.PopMale = reader.GetValueOrDefault<Nullable<int>>("PopMale");
                    demo.Notes = reader.GetValueOrDefault<string>("Notes");
                    demo.UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt");
                    demo.UpdatedBy = Util.GetAuditInfo(reader);

                }
                reader.Close();
            }
        }

        public void BulkImportAggregatedDemo(List<AdminLevel> tree, int userId, int aggregatingLevel)
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

                    SaveChildDemo(command, connection, tree, userId, aggregatingLevel);

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

        private void SaveChildDemo(OleDbCommand command, OleDbConnection connection, List<AdminLevel> tree, int userId, int aggregatingLevel)
        {
            foreach (var child in tree)
            {
                if (child.Children.Count > 0)
                    SaveChildDemo(command, connection, child.Children, userId, aggregatingLevel);

                if (child.CurrentDemography != null && child.LevelNumber != aggregatingLevel)
                    SaveAdminDemography(command, connection, child.CurrentDemography, userId);
            }
        }

        public void Save(List<AdminLevelDemography> demos, int userId)
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

                    foreach(var demo in demos)
                        SaveAdminDemography(command, connection, demo, userId);

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

        public void Save(AdminLevelDemography demo, int userId)
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

                    SaveAdminDemography(command, connection, demo, userId);

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

        private void SaveAdminDemography(OleDbCommand command, OleDbConnection connection, AdminLevelDemography demo, int userId)
        {
            if (demo.Id > 0)
                command = new OleDbCommand(@"UPDATE AdminLevelDemography SET AdminLevelId=@AdminLevelId, YearDemographyData=@YearDemographyData,
                            YearCensus=@YearCensus,  YearProjections=@YearProjections, GrowthRate=@GrowthRate, PercentRural=@PercentRural, TotalPopulation=@TotalPopulation,
                            Pop0Month=@Pop0Month, PopPsac=@PopPsac, PopSac=@PopSac, Pop5yo=@Pop5yo, PopAdult=@PopAdult,
                            PopFemale=@PopFemale, PopMale=@PopMale, Notes=@Notes, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
            else
                command = new OleDbCommand(@"INSERT INTO AdminLevelDemography (AdminLevelId, YearDemographyData,
                            YearCensus,  YearProjections, GrowthRate, PercentRural, TotalPopulation, Pop0Month, PopPsac, 
                            PopSac, Pop5yo, PopAdult, PopFemale, PopMale, Notes, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
                            values (@AdminLevelId, @YearDemographyData, @YearCensus,  @YearProjections, @GrowthRate, @PercentRural, @TotalPopulation, 
                             @Pop0Month, @PopPsac, @PopSac, @Pop5yo, @PopAdult, @PopFemale, @PopMale, @Notes, @UpdatedById, @UpdatedAt,
                            @CreatedById, @CreatedAt)", connection);
            command.Parameters.Add(new OleDbParameter("@AdminLevelId", demo.AdminLevelId));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@YearDemographyData", demo.YearDemographyData));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@YearCensus", demo.YearCensus));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@YearProjections", demo.YearProjections));
            command.Parameters.Add(new OleDbParameter("@GrowthRate", demo.GrowthRate));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@PercentRural", demo.PercentRural));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@TotalPopulation", demo.TotalPopulation));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@Pop0Month", demo.Pop0Month));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@PopPsac", demo.PopPsac));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@PopSac", demo.PopSac));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@Pop5yo", demo.Pop5yo));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@PopAdult", demo.PopAdult));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@PopFemale", demo.PopFemale));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@PopMale", demo.PopMale));

            command.Parameters.Add(OleDbUtil.CreateNullableParam("@Notes", demo.Notes));
            command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));

            if (demo.Id > 0)
                command.Parameters.Add(new OleDbParameter("@id", demo.Id));
            else
            {
                command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
                command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
            }
            command.ExecuteNonQuery();

            if (demo.Id <= 0)
            {
                command = new OleDbCommand(@"SELECT Max(ID) FROM AdminLevelDemography", connection);
                demo.Id = (int)command.ExecuteScalar();
            }
        }
        #endregion


    }
}
