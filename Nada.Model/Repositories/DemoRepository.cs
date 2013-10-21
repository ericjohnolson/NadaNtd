using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.DA;
using Nada.Model.Csv;

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
                OleDbCommand command = new OleDbCommand(@"Select Country.ID, DisplayName, CountryCode, IsoCode from Country 
                    inner join AdminLevels on Country.AdminLevelId = AdminLevels.ID where Country.ID = @id", connection);
                command.Parameters.Add(new OleDbParameter("@id", id));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        country = new Country
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            CountryCode = reader.GetValueOrDefault<string>("CountryCode"),
                            Name = reader.GetValueOrDefault<string>("DisplayName"),
                            IsoCode = reader.GetValueOrDefault<string>("IsoCode")
                        };
                    }
                    reader.Close();
                }
            }
            country.Name = string.IsNullOrEmpty(country.Name) ? "Country Not Set" : country.Name;
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
                    OleDbCommand command = new OleDbCommand(@"Update Country set CountryCode=@CountryCode, 
                        IsoCode = @IsoCode, UpdatedBy=@updatedby, UpdatedAt=@updatedat WHERE ID = @id", connection);
                    command.Parameters.Add(new OleDbParameter("@CountryCode", country.CountryCode));
                    command.Parameters.Add(new OleDbParameter("@IsoCode", country.IsoCode));
                    command.Parameters.Add(new OleDbParameter("@updatedby", byUserId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@updatedat", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", country.Id));
                    command.ExecuteNonQuery();

                    command = new OleDbCommand(@"Update AdminLevels set DisplayName=@DisplayName, UpdatedBy=@updatedby, UpdatedAt=@updatedat WHERE ID = @id", connection);
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

        public List<CountryDemography> GetCountryDemography()
        {
            List<CountryDemography> countries = new List<CountryDemography>();
            int id = Convert.ToInt32(ConfigurationManager.AppSettings["CountryId"]);
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select ID,
                    YearReporting, YearCensus, YearProjections, GrowthRate, FemalePercent, MalePercent, AdultsPercent 
                    FROM CountryDemography WHERE CountryId = @CountryId", connection);
                command.Parameters.Add(new OleDbParameter("@CountryId", id));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        countries.Add(new CountryDemography
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            YearReporting = reader.GetValueOrDefault<int>("YearReporting"),
                            YearCensus = reader.GetValueOrDefault<int>("YearCensus"),
                            YearProjections = reader.GetValueOrDefault<int>("YearProjections"),
                            AdultsPercent = reader.GetValueOrDefault<double>("AdultsPercent"),
                            FemalePercent = reader.GetValueOrDefault<double>("FemalePercent"),
                            MalePercent = reader.GetValueOrDefault<double>("MalePercent"),
                            GrowthRate = reader.GetValueOrDefault<double>("GrowthRate")
                        });
                    }
                    reader.Close();
                }
            }
            return countries;
        }

        public void InsertCountryDemography(CountryDemography demo, int byUserId)
        {
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            int countryId = Convert.ToInt32(ConfigurationManager.AppSettings["CountryId"]); 
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Insert Into CountryDemography (CountryId, YearReporting, 
                        YearCensus, YearProjections, GrowthRate, FemalePercent, MalePercent, AdultsPercent, UpdatedBy, UpdatedAt) VALUES
                        (@CountryId, @YearReporting, @YearCensus, @YearProjections, @GrowthRate, @FemalePercent, 
                         @MalePercent, @AdultsPercent, @updatedby, @updatedat)", connection);
                    command.Parameters.Add(new OleDbParameter("@CountryId", countryId));
                    command.Parameters.Add(new OleDbParameter("@YearReporting", demo.YearReporting));
                    command.Parameters.Add(new OleDbParameter("@YearCensus", demo.YearCensus));
                    command.Parameters.Add(new OleDbParameter("@YearProjections", demo.YearProjections));
                    command.Parameters.Add(new OleDbParameter("@GrowthRate", demo.GrowthRate));
                    command.Parameters.Add(new OleDbParameter("@FemalePercent", demo.FemalePercent));
                    command.Parameters.Add(new OleDbParameter("@MalePercent", demo.MalePercent));
                    command.Parameters.Add(new OleDbParameter("@AdultsPercent", demo.AdultsPercent));
                    command.Parameters.Add(new OleDbParameter("@updatedby", byUserId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@updatedat", DateTime.Now));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void UpdateCountryDemography(CountryDemography demo, int byUserId)
        {
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Update CountryDemography SET YearReporting=@YearReporting, 
                        YearCensus=@YearCensus, YearProjections=@YearProjections, GrowthRate=@GrowthRate, FemalePercent=@FemalePercent, 
                        MalePercent=@MalePercent, AdultsPercent=@AdultsPercent, UpdatedBy=@updatedby, UpdatedAt=@updatedat
                        WHERE ID=@id", connection);
                    command.Parameters.Add(new OleDbParameter("@YearReporting", demo.YearReporting));
                    command.Parameters.Add(new OleDbParameter("@YearCensus", demo.YearCensus));
                    command.Parameters.Add(new OleDbParameter("@YearProjections", demo.YearProjections));
                    command.Parameters.Add(new OleDbParameter("@GrowthRate", demo.GrowthRate));
                    command.Parameters.Add(new OleDbParameter("@FemalePercent", demo.FemalePercent));
                    command.Parameters.Add(new OleDbParameter("@MalePercent", demo.MalePercent));
                    command.Parameters.Add(new OleDbParameter("@AdultsPercent", demo.AdultsPercent));
                    command.Parameters.Add(new OleDbParameter("@updatedby", byUserId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@updatedat", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", demo.Id));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
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
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, IsUrban, LatWho, LngWho, Latitude, Longitude,
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
                            IsUrban = reader.GetValueOrDefault<bool>("IsUrban"),
                            LatWho = reader.GetValueOrDefault<double>("LatWho"),
                            LngWho = reader.GetValueOrDefault<double>("LngWho"),
                            LatOther = reader.GetValueOrDefault<double>("Latitude"),
                            LngOther = reader.GetValueOrDefault<double>("Longitude"),
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
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, IsUrban, LatWho, LngWho, Latitude, Longitude,
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
                            IsUrban = reader.GetValueOrDefault<bool>("IsUrban"),
                            LatWho = reader.GetValueOrDefault<double>("LatWho"),
                            LngWho = reader.GetValueOrDefault<double>("LngWho"),
                            LatOther = reader.GetValueOrDefault<double>("Latitude"),
                            LngOther = reader.GetValueOrDefault<double>("Longitude"),
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
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, IsUrban, LatWho, LngWho, Latitude, Longitude,
                    AdminLevelTypes.AdminLevel
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE AdminLevels.IsDeleted = 0
                    ", connection); // WHERE ParentId > 0
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new AdminLevel
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            ParentId = reader.GetValueOrDefault<Nullable<int>>("ParentId"),
                            Name = reader.GetValueOrDefault<string>("DisplayName"),
                            LevelNumber = reader.GetValueOrDefault<int>("AdminLevel"),
                            IsUrban = reader.GetValueOrDefault<bool>("IsUrban"),
                            LatWho = reader.GetValueOrDefault<double>("LatWho"),
                            LngWho = reader.GetValueOrDefault<double>("LngWho"),
                            LatOther = reader.GetValueOrDefault<double>("Latitude"),
                            LngOther = reader.GetValueOrDefault<double>("Longitude"),
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
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, IsUrban, LatWho, LngWho, Latitude, Longitude,
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
                            IsUrban = reader.GetValueOrDefault<bool>("IsUrban"),
                            LatWho = reader.GetValueOrDefault<double>("LatWho"),
                            LngWho = reader.GetValueOrDefault<double>("LngWho"),
                            LatOther = reader.GetValueOrDefault<double>("Latitude"),
                            LngOther = reader.GetValueOrDefault<double>("Longitude"),
                        });
                    }
                    reader.Close();
                }
            }
            return MakeTreeFromFlatList(list, 1);
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
                        ParentId, IsUrban, LatWho, LngWho, Latitude, Longitude, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES
                        (@DisplayName, @AdminLevelTypeId, @ParentId, @IsUrban, @LatWho, @LngWho, 
                         @Latitude, @Longitude, @updatedby, @updatedat, @CreatedById, @CreatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@DisplayName", child.Name));
                        command.Parameters.Add(new OleDbParameter("@AdminLevelTypeId", childType.Id));
                        command.Parameters.Add(new OleDbParameter("@ParentId", parent.Id));
                        command.Parameters.Add(new OleDbParameter("@IsUrban", child.IsUrban));
                        command.Parameters.Add(new OleDbParameter("@LatWho", child.LatWho));
                        command.Parameters.Add(new OleDbParameter("@LngWho", child.LngWho));
                        command.Parameters.Add(new OleDbParameter("@Latitude", child.LatOther));
                        command.Parameters.Add(new OleDbParameter("@Longitude", child.LngOther));
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

        public void BulkAddDemography(List<AdminLevelDemography> children, int byUserId)
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
                        command = new OleDbCommand(@"Insert Into AdminLevelDemography (AdminLevelId, 
                        YearReporting, YearCensus, YearProjections, GrowthRate, FemalePercent, MalePercent, 
                        AdultsPercent, TotalPopulation, AdultPopulation, UpdatedBy, UpdatedAt) VALUES
                        (@AdminLevelId, @YearReporting, @YearCensus, @YearProjections, @GrowthRate, 
                         @FemalePercent, @MalePercent, @AdultsPercent, @TotalPopulation, @AdultPopulation, 
                        @updatedby, @updatedat)", connection);
                        command.Parameters.Add(new OleDbParameter("@AdminLevelId", child.AdminLevelId));
                        command.Parameters.Add(new OleDbParameter("@YearReporting", child.YearReporting));
                        command.Parameters.Add(new OleDbParameter("@YearCensus", child.YearCensus));
                        command.Parameters.Add(new OleDbParameter("@YearProjections", child.YearProjections));
                        command.Parameters.Add(new OleDbParameter("@GrowthRate", child.GrowthRate));
                        command.Parameters.Add(new OleDbParameter("@FemalePercent", child.FemalePercent));
                        command.Parameters.Add(new OleDbParameter("@MalePercent", child.MalePercent));
                        command.Parameters.Add(new OleDbParameter("@AdultsPercent", child.AdultsPercent));
                        command.Parameters.Add(new OleDbParameter("@TotalPopulation", child.TotalPopulation));
                        command.Parameters.Add(new OleDbParameter("@AdultPopulation", child.AdultPopulation));
                        command.Parameters.Add(new OleDbParameter("@updatedby", byUserId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@updatedat", DateTime.Now));
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

        private int InsertAdminLevelHelper(OleDbCommand command, AdminLevel adminLevel, OleDbConnection connection, int userId)
        {
            command = new OleDbCommand(@"Insert Into AdminLevels (DisplayName, AdminLevelTypeId, 
                        ParentId, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES
                        (@DisplayName, @AdminLevelTypeId, @ParentId, @UpdatedBy, @UpdatedAt, @CreatedById, @CreatedAt)", connection);
            command.Parameters.Add(new OleDbParameter("@DisplayName", adminLevel.Name));
            command.Parameters.Add(new OleDbParameter("@AdminLevelTypeId", adminLevel.LevelNumber));
            command.Parameters.Add(new OleDbParameter("@ParentId", adminLevel.ParentId));
            command.Parameters.Add(new OleDbParameter("@UpdatedBy", userId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
            command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
            command.ExecuteNonQuery();

            command = new OleDbCommand(@"SELECT Max(ID) FROM AdminLevels", connection);
            return (int)command.ExecuteScalar();
        }

        public List<AdminLevelDemography> GetAdminLevelDemography(int id)
        {
            List<AdminLevelDemography> demo = new List<AdminLevelDemography>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select ID, YearReporting, YearCensus, YearProjections, 
                    GrowthRate, FemalePercent, MalePercent, AdultsPercent, TotalPopulation, AdultPopulation
                    FROM AdminLevelDemography WHERE AdminLevelId = @id", connection);
                command.Parameters.Add(new OleDbParameter("@id", id));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        demo.Add(new AdminLevelDemography
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            YearReporting = reader.GetValueOrDefault<int>("YearReporting"),
                            YearCensus = reader.GetValueOrDefault<int>("YearCensus"),
                            YearProjections = reader.GetValueOrDefault<int>("YearProjections"),
                            MalePercent = reader.GetValueOrDefault<double>("MalePercent"),
                            GrowthRate = reader.GetValueOrDefault<double>("GrowthRate"),
                            AdultsPercent = reader.GetValueOrDefault<double>("AdultsPercent"),
                            FemalePercent = reader.GetValueOrDefault<double>("FemalePercent"),
                            TotalPopulation = reader.GetValueOrDefault<int>("TotalPopulation"),
                            AdultPopulation = reader.GetValueOrDefault<int>("AdultPopulation"),
                        });
                    }
                    reader.Close();
                }
            }
            return demo;
        }
        #endregion


    }
}
