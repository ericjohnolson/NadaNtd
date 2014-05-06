using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.DA;
using Nada.Globalization;
using Nada.Model.Csv;
using Nada.Model.Demography;

namespace Nada.Model.Repositories
{
    public class DemoRepository : RepositoryBase
    {
        #region Country
        public Country GetCountry()
        {
            Country country = new Country();
            int id = Convert.ToInt32(ConfigurationManager.AppSettings["CountryId"]);
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select Country.ID, DisplayName, ReportingYearStartDate, aspnet_Users.UserName, Country.UpdatedAt 
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
                            ReportingYearStartDate = reader.GetValueOrDefault<DateTime>("ReportingYearStartDate"),
                            UpdatedBy = GetAuditInfoUpdate(reader)
                        };
                    }
                    reader.Close();
                }
            }

            return country;
        }

        public void UpdateCountry(Country country, int byUserId)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Update Country set ReportingYearStartDate=@ReportingYearStartDate, MonthYearStarts=1,
                         UpdatedById=@updatedby, UpdatedAt=@updatedat WHERE ID = @id", connection);
                    command.Parameters.Add(new OleDbParameter("@ReportingYearStartDate", country.ReportingYearStartDate));
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

        public CountryDemography GetCountryDemoByYear(int year)
        {
            
            CountryDemography demo = new CountryDemography();
            demo.AdminLevelId = Convert.ToInt32(ConfigurationManager.AppSettings["CountryId"]);
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    int demoId = 0;
                    OleDbCommand command = new OleDbCommand(@"Select ID FROM AdminLevelDemography WHERE AdminLevelId=@id "
                        + CreateDemoYearRange(year) + @" ORDER BY DateDemographyData DESC ", connection);
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

                    demo = GetCountryDemoById(command, connection, demoId);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return demo;
        }

        public CountryDemography GetCountryDemoRecent()
        {
            CountryDemography demo = new CountryDemography();
            demo.AdminLevelId = Convert.ToInt32(ConfigurationManager.AppSettings["CountryId"]);
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    int demoId = 0;
                    OleDbCommand command = new OleDbCommand(@"Select ID FROM AdminLevelDemography WHERE AdminLevelId=@id
                        ORDER BY DateDemographyData DESC", connection);
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

                    demo = GetCountryDemoById(command, connection, demoId);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return demo;
        }

        private CountryDemography GetCountryDemoById(OleDbCommand command, OleDbConnection connection, int demoId)
        {
            CountryDemography demo = new CountryDemography();
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
            return demo;
        }

        public void Save(CountryDemography demo, int byUserId)
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
                command = new OleDbCommand(@"UPDATE AdminLevelDemography SET AdminLevelId=@AdminLevelId, DateDemographyData=@DateDemographyData,
                            YearCensus=@YearCensus,  GrowthRate=@GrowthRate, PercentRural=@PercentRural, 
                            Notes=@Notes, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
            else
                command = new OleDbCommand(@"INSERT INTO AdminLevelDemography (AdminLevelId, DateDemographyData,
                            YearCensus, GrowthRate, PercentRural, Notes, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
                            values (@AdminLevelId, @DateDemographyData, @YearCensus, @GrowthRate, @PercentRural, @Notes, 
                            @UpdatedById, @UpdatedAt, @CreatedById, @CreatedAt)", connection);
            command.Parameters.Add(new OleDbParameter("@AdminLevelId", demo.AdminLevelId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@DateDemographyData", demo.DateDemographyData));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@YearCensus", demo.YearCensus));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@GrowthRate", demo.GrowthRate));
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
        public List<AdminLevel> GetAdminLevelChildren(int parentId)
        {
            List<AdminLevel> list = new List<AdminLevel>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName,  AdminLevelTypes.AdminLevel
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE ParentId=@ParentId AND AdminLevels.IsDeleted=0 AND AdminLevels.RedistrictIdForMother=0", connection);
                command.Parameters.Add(new OleDbParameter("@ParentId", parentId));
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

        public List<AdminLevel> GetAdminLevelByLevel(int levelNumber)
        {
            List<AdminLevel> list = new List<AdminLevel>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, AdminLevelTypes.AdminLevel
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE AdminLevelTypes.AdminLevel = @LevelNumber AND AdminLevels.IsDeleted=0 AND AdminLevels.RedistrictIdForMother=0", connection);
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
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, UrbanOrRural, LatWho, LngWho,
                    AdminLevelTypes.AdminLevel, AdminLevelTypes.DisplayName as LevelName
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
                            LevelName = reader.GetValueOrDefault<string>("LevelName"),
                            UrbanOrRural = reader.GetValueOrDefault<string>("UrbanOrRural"),
                            LatWho = reader.GetValueOrDefault<double>("LatWho"),
                            LngWho = reader.GetValueOrDefault<double>("LngWho"),
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
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, UrbanOrRural, LatWho, LngWho,
                    AdminLevelTypes.AdminLevel
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE AdminLevels.IsDeleted = 0 AND AdminLevels.RedistrictIdForMother=0
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
                        });
                    }
                    reader.Close();
                }
            }
            return MakeTreeFromFlatList(list, 0);
        }

        public List<AdminLevel> GetAdminLevelTree(int levelTypeId)
        {
            return GetAdminLevelTree(levelTypeId, 1, false);
        }

        public List<AdminLevel> GetAdminLevelTree(int levelTypeId, bool redistrictedOnly)
        {
            return GetAdminLevelTree(levelTypeId, 1, false, false, levelTypeId, redistrictedOnly);
        }

        public List<AdminLevel> GetAdminLevelTree(int levelTypeId, int lowestLevel, bool includeCountry)
        {
            return GetAdminLevelTree(levelTypeId, lowestLevel, includeCountry, false, 0);
        }

        public List<AdminLevel> GetAdminLevelTree(int levelTypeId, int lowestLevel, bool includeCountry, bool allowSelect, int levelToSelect)
        {
            return GetAdminLevelTree(levelTypeId, lowestLevel, includeCountry, allowSelect, levelToSelect, false);
        }
        public List<AdminLevel> GetAdminLevelTree(int levelTypeId, int lowestLevel, bool includeCountry, bool allowSelect, int levelToSelect, bool onlyRedistricted)
        {
            List<AdminLevel> list = new List<AdminLevel>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                string redistrictedFilter = "AND AdminLevels.RedistrictIdForMother=0";
                if (onlyRedistricted)
                    redistrictedFilter = "AND AdminLevels.RedistrictIdForMother>=0";
                connection.Open();
                string cmd = @"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, UrbanOrRural, LatWho, LngWho, 
                    AdminLevelTypes.AdminLevel, AdminLevelTypes.ID as AdminLevelTypeId, AdminLevelTypes.DisplayName as LevelName, RedistrictIdForMother
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE AdminLevelTypeId <= @AdminLevelTypeId AND AdminLevels.IsDeleted = 0 " + redistrictedFilter;
                if (!includeCountry)
                    cmd += " AND ParentId > 0 ";
                OleDbCommand command = new OleDbCommand(cmd, connection);
                command.Parameters.Add(new OleDbParameter("@AdminLevelTypeId", levelTypeId));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AdminLevel
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            ParentId = reader.GetValueOrDefault<Nullable<int>>("ParentId"),
                            RedistrictIdForMother = reader.GetValueOrDefault<int>("RedistrictIdForMother"),
                            Name = reader.GetValueOrDefault<string>("DisplayName"),
                            LevelNumber = reader.GetValueOrDefault<int>("AdminLevel"),
                            LevelName = reader.GetValueOrDefault<string>("LevelName"),
                            AdminLevelTypeId = reader.GetValueOrDefault<int>("AdminLevelTypeId"),
                            UrbanOrRural = reader.GetValueOrDefault<string>("UrbanOrRural"),
                            LatWho = reader.GetValueOrDefault<double>("LatWho"),
                            LngWho = reader.GetValueOrDefault<double>("LngWho"),
                        });
                    }
                    reader.Close();
                }
            }
            return MakeTreeFromFlatList(list, lowestLevel, allowSelect, levelToSelect, onlyRedistricted);
        }

        public List<AdminLevel> GetAdminLevelTreeForDemography(int level, DateTime demoDate, ref List<AdminLevel> list)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, UrbanOrRural, LatWho, LngWho, 
                    AdminLevelTypes.AdminLevel, AdminLevelTypeId
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE AdminLevel <= @AdminLevel AND AdminLevels.IsDeleted = 0 AND AdminLevels.RedistrictIdForMother=0", connection);
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
                            AdminLevelTypeId = reader.GetValueOrDefault<int>("AdminLevelTypeId")
                        };
                        al.CurrentDemography = GetDemoByAdminLevelIdAndYear(al.Id, demoDate);
                        list.Add(al);

                    }
                    reader.Close();
                }
            }
            return MakeTreeFromFlatList(list, 0);
        }

        public List<AdminLevel> GetAdminLevelParentNames(int levelId)
        {
            Nullable<int> id = levelId;
            List<AdminLevel> list = new List<AdminLevel>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                bool hasParent = true;
                while (hasParent)
                {
                    OleDbCommand command = new OleDbCommand(@"Select a.ParentId, a.DisplayName, t.DisplayName as TypeName
                    FROM AdminLevels a inner join adminleveltypes t on t.id = a.adminleveltypeid
                    WHERE a.ID = @id AND a.IsDeleted=0", connection);
                    command.Parameters.Add(new OleDbParameter("@id", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            list.Insert(0, new AdminLevel
                            {
                                Name = reader.GetValueOrDefault<string>("DisplayName"),
                                LevelName = reader.GetValueOrDefault<string>("TypeName")
                            });
                            id = reader.GetValueOrDefault<Nullable<int>>("ParentId");
                            if (!id.HasValue || id <= 1)
                                hasParent = false;
                        }
                        else
                            hasParent = false;
                        reader.Close();
                    }
                }
            }
            return list;
        }

        public List<string> GetAdminLevelTypeNames(int levelTypeId)
        {
            List<string> list = new List<string>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"SELECT DisplayName FROM AdminLevelTypes WHERE ID > 1 AND ID <= @TypeId ORDER BY AdminLevel", connection);
                command.Parameters.Add(new OleDbParameter("@TypeId", levelTypeId));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetValueOrDefault<string>("DisplayName"));
                    }
                    reader.Close();
                }
            }
            return list;
        }

        private List<AdminLevel> MakeTreeFromFlatList(IEnumerable<AdminLevel> flatList, int minRoot, bool allowSelect, int levelToSelect, bool onlyRedistricted)
        {
            var dic = flatList.ToDictionary(n => n.Id, n => n);
            var rootNodes = new List<AdminLevel>();
            foreach (var node in flatList)
            {
                if (allowSelect && (levelToSelect != -1 && node.AdminLevelTypeId != levelToSelect))
                    node.ViewText = "";

                if (node.ParentId.HasValue && node.ParentId.Value > minRoot)
                {
                    AdminLevel parent = dic[node.ParentId.Value];
                    if (onlyRedistricted && node.AdminLevelTypeId == levelToSelect)
                    {
                        if (node.RedistrictIdForMother > 0)
                            parent.Children.Add(node);
                    }
                    else
                        parent.Children.Add(node);
                }
                else
                {
                    if (onlyRedistricted && node.AdminLevelTypeId == levelToSelect)
                    {
                        if (node.RedistrictIdForMother > 0)
                            rootNodes.Add(node);
                    }
                    else
                        rootNodes.Add(node);
                }
            }
            return rootNodes;
        }

        private List<AdminLevel> MakeTreeFromFlatList(IEnumerable<AdminLevel> flatList, int minRoot)
        {
            return MakeTreeFromFlatList(flatList, minRoot, false, 0, false);
        }
            
        public void AddChildren(AdminLevel parent, List<AdminLevel> children, AdminLevelType childType, int byUserId)
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

                    foreach (var child in children)
                    {
                        command = new OleDbCommand(@"Insert Into AdminLevels (DisplayName, AdminLevelTypeId, 
                        ParentId, UrbanOrRural, LatWho, LngWho,  UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES
                        (@DisplayName, @AdminLevelTypeId, @ParentId, @UrbanOrRural, @LatWho, @LngWho, 
                         @updatedby, @updatedat, @CreatedById, @CreatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@DisplayName", child.Name));
                        command.Parameters.Add(new OleDbParameter("@AdminLevelTypeId", childType.Id));
                        command.Parameters.Add(new OleDbParameter("@ParentId", parent.Id));
                        command.Parameters.Add(new OleDbParameter("@UrbanOrRural", child.UrbanOrRural));
                        command.Parameters.Add(new OleDbParameter("@LatWho", child.LatWho));
                        command.Parameters.Add(new OleDbParameter("@LngWho", child.LngWho));
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

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand();

                    if (filterBy != null)
                    {
                        command = new OleDbCommand(@"Select AdminLevels.ID, AdminLevels.DisplayName
                            FROM AdminLevels WHERE ParentId=@ParentId AND IsDeleted=0 AND RedistrictIdForMother=0", connection);
                        command.Parameters.Add(new OleDbParameter("@ParentId", filterBy.Id));
                    }
                    else if (parentType != null)
                    {
                        command = new OleDbCommand(@"Select AdminLevels.ID, AdminLevels.DisplayName
                            FROM AdminLevels  WHERE AdminLevelTypeId = @AdminLevelTypeId AND IsDeleted=0 AND RedistrictIdForMother=0", connection);
                        command.Parameters.Add(new OleDbParameter("@AdminLevelTypeId", parentType.Id));
                    }

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if(!parentIds.ContainsKey(reader.GetValueOrDefault<string>("DisplayName").ToLower()))
                                parentIds.Add(reader.GetValueOrDefault<string>("DisplayName").ToLower(), reader.GetValueOrDefault<int>("ID"));
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

        public void Save(AdminLevel model, int userid)
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

                    if (model.Id > 0)
                    {
                        command = new OleDbCommand(@"UPDATE AdminLevels set DisplayName=@DisplayName, UrbanOrRural=@UrbanOrRural, 
                        LatWho=@LatWho, LngWho=@LngWho, UpdatedById=@updatedby, UpdatedAt=@updatedat WHERE ID=@ID", connection);
                        command.Parameters.Add(new OleDbParameter("@DisplayName", model.Name));
                        command.Parameters.Add(OleDbUtil.CreateNullableParam("@UrbanOrRural", model.UrbanOrRural));
                        command.Parameters.Add(OleDbUtil.CreateNullableParam("@LatWho", model.LatWho));
                        command.Parameters.Add(OleDbUtil.CreateNullableParam("@LngWho", model.LngWho));
                        command.Parameters.Add(new OleDbParameter("@updatedby", userid));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@updatedat", DateTime.Now));
                        command.Parameters.Add(new OleDbParameter("@ID", model.Id));
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        command = new OleDbCommand(@"Insert Into AdminLevels (DisplayName, AdminLevelTypeId, 
                        ParentId, UrbanOrRural, LatWho, LngWho, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES
                        (@DisplayName, @AdminLevelTypeId, @ParentId, @UrbanOrRural, @LatWho, @LngWho, 
                         @updatedby, @updatedat, @CreatedById, @CreatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@DisplayName", model.Name));
                        command.Parameters.Add(new OleDbParameter("@AdminLevelTypeId", model.AdminLevelTypeId));
                        command.Parameters.Add(new OleDbParameter("@ParentId", model.ParentId));
                        command.Parameters.Add(OleDbUtil.CreateNullableParam("@UrbanOrRural", model.UrbanOrRural));
                        command.Parameters.Add(OleDbUtil.CreateNullableParam("@LatWho", model.LatWho));
                        command.Parameters.Add(OleDbUtil.CreateNullableParam("@LngWho", model.LngWho));
                        command.Parameters.Add(new OleDbParameter("@updatedby", userid));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@updatedat", DateTime.Now));
                        command.Parameters.Add(new OleDbParameter("@CreatedById", userid));
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
        public void AggregateUp(AdminLevelType locationType, DateTime demoDate, int userId, double? growthRate)
        {
            try
            {
                List<AdminLevel> list = new List<AdminLevel>();
                var tree = GetAdminLevelTreeForDemography(locationType.LevelNumber, demoDate, ref list);
                var country = tree.FirstOrDefault();
                if (!growthRate.HasValue)
                {
                    var demo = GetCountryDemoRecent();
                    growthRate = demo.GrowthRate;
                }
                country.CurrentDemography = IndicatorAggregator.AggregateTree(country, growthRate);
                BulkImportAggregatedDemo(tree, userId, locationType.LevelNumber);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ApplyGrowthRate(double growthRatePercent, int userId, AdminLevelType aggLevel, DateTime dateReported)
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
                    var growthRateDemonminator = growthRatePercent / 100;
                    
                    // Get Agg Level & Below and create new demo
                    List<AdminLevelDemography> mostRecent = GetRecentDemographyByLevel(aggLevel.LevelNumber, command, connection);
                    foreach (var d in mostRecent)
                    {
                        d.Id = 0;
                        d.DateDemographyData = dateReported;
                        d.GrowthRate = growthRatePercent;
                        if (d.Pop0Month.HasValue) d.Pop0Month = d.Pop0Month * growthRateDemonminator + d.Pop0Month;
                        if (d.Pop5yo.HasValue) d.Pop5yo = d.Pop5yo * growthRateDemonminator + d.Pop5yo;
                        if (d.PopAdult.HasValue) d.PopAdult = d.PopAdult * growthRateDemonminator + d.PopAdult;
                        if (d.PopFemale.HasValue) d.PopFemale = d.PopFemale * growthRateDemonminator + d.PopFemale;
                        if (d.PopMale.HasValue) d.PopMale = d.PopMale * growthRateDemonminator + d.PopMale;
                        if (d.PopPsac.HasValue) d.PopPsac = d.PopPsac * growthRateDemonminator + d.PopPsac;
                        if (d.PopPsac.HasValue) d.PopSac = d.PopSac * growthRateDemonminator + d.PopSac;
                        if (d.PopPsac.HasValue) d.TotalPopulation = d.TotalPopulation * growthRateDemonminator + d.TotalPopulation;
                        SaveAdminDemography(command, connection, d, userId);
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
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
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

        public List<DemoDetails> GetAdminLevelDemography(int adminLevelId)
        {
            List<DemoDetails> demo = new List<DemoDetails>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select a.ID, a.DateDemographyData, a.GrowthRate, a.TotalPopulation, a.UpdatedAt, 
                    aspnet_Users.UserName, created.UserName as CreatedBy, a.CreatedAt
                    FROM ((AdminLevelDemography a INNER JOIN aspnet_Users on a.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN aspnet_Users created on a.CreatedById = created.UserId)
                    WHERE AdminLevelId = @id and IsDeleted = 0", connection);
                command.Parameters.Add(new OleDbParameter("@id", adminLevelId));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        demo.Add(new DemoDetails
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            DateReported = reader.GetValueOrDefault<DateTime>("DateDemographyData"),
                            GrowthRate = reader.GetValueOrDefault<double>("GrowthRate"),
                            TotalPopulation = reader.GetValueOrDefault<double>("TotalPopulation"),
                            UpdatedBy = GetAuditInfo(reader)
                        });
                    }
                    reader.Close();
                }
            }
            return demo;
        }

        public AdminLevelDemography GetRecentDemography(int adminLevelId, DateTime? start, DateTime? end)
        {
            AdminLevelDemography demog = new AdminLevelDemography { AdminLevelId = adminLevelId };
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select a.ID
                    FROM AdminLevelDemography a 
                    WHERE AdminLevelId = @id and IsDeleted = 0 " + CreateDateRange(start, end)
                    + " ORDER BY DateDemographyData Desc", connection);
                command.Parameters.Add(new OleDbParameter("@id", adminLevelId));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int id = reader.GetValueOrDefault<int>("ID");
                        GetDemoById(demog, id, connection, command);
                    }
                    reader.Close();
                }
            }
            return demog;
        }
        
        public List<AdminLevelDemography> GetRecentDemography(int level, int recentYear)
        {
            List<AdminLevelDemography> recent = new List<AdminLevelDemography>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
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

        public AdminLevelDemography GetRecentDemography(int adminUnitId)
        {
            AdminLevelDemography demo = new AdminLevelDemography { AdminLevelId = adminUnitId };
            int id = 0;
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    OleDbCommand command = new OleDbCommand(@"Select ID
                        FROM AdminLevelDemography
                        WHERE AdminLevelId = @id and IsDeleted = 0 
                        ORDER BY DateDemographyData DESC", connection);
                    command.Parameters.Add(new OleDbParameter("@id", adminUnitId));
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

        private List<AdminLevelDemography> GetRecentDemographyByLevel(int level, int recentYear, OleDbCommand command, OleDbConnection connection)
        {
            List<AdminLevelDemography> demo = new List<AdminLevelDemography>();
            command = new OleDbCommand(@"SELECT MAX(AdminLevelDemography.ID) as MID, MAX(AdminLevels.DisplayName) as MName
                    FROM ((AdminLevelDemography INNER JOIN AdminLevels on AdminLevelDemography.AdminLevelId = AdminLevels.ID)
                            INNER JOIN AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID)
                    WHERE AdminLevelTypes.AdminLevel=@lvl AND DatePart('yyyy', [AdminLevelDemography.DateDemographyData])=@Year
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

        private List<AdminLevelDemography> GetRecentDemographyByLevel(int level, OleDbCommand command, OleDbConnection connection)
        {
            List<AdminLevelDemography> demo = new List<AdminLevelDemography>();
            command = new OleDbCommand(
                @"SELECT AdminLevels.Id as aid, MAX(a.DateDemographyData) as mdate
                    FROM ((AdminLevelDemography a INNER JOIN AdminLevels on a.AdminLevelId = AdminLevels.ID)
                            INNER JOIN AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID)
                    WHERE AdminLevelTypes.AdminLevel>=@lvl 
                    GROUP BY AdminLevels.Id", connection);
            
            command.Parameters.Add(new OleDbParameter("@lvl", level));
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    AdminLevelDemography d = new AdminLevelDemography();
                    GetDemoByUnitAndDate(d, reader.GetValueOrDefault<int>("aid"), reader.GetValueOrDefault<DateTime>("mdate"), connection, command);
                    demo.Add(d);
                }
                reader.Close();
            }
            return demo;
        }

        public AdminLevelDemography GetDemoByAdminLevelIdAndYear(int adminLevelid, DateTime demoDate)
        {
            var country = GetCountry();
            AdminLevelDemography demo = new AdminLevelDemography { DateDemographyData = demoDate, AdminLevelId = adminLevelid };
            int id = 0;
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    // START TRANS
                    OleDbCommand command = new OleDbCommand(@"Select ID
                        FROM AdminLevelDemography
                        WHERE AdminLevelId = @id and IsDeleted = 0 " + CreateDemoYearRange(demoDate, country.ReportingYearStartDate)
                    + @"ORDER BY DateDemographyData DESC", connection);
                    command.Parameters.Add(new OleDbParameter("@id", adminLevelid));
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

        private string CreateDateRange(DateTime? start, DateTime? end)
        {
            if (end.HasValue & start.HasValue)
                return string.Format("AND (DateDemographyData >= cdate('{0}') AND DateDemographyData <= cdate('{1}'))", start.Value.ToShortDateString(), end.Value.AddDays(1).ToShortDateString());
            else if (end.HasValue)
                return string.Format("AND DateDemographyData <= cdate('{0}')", end.Value.AddDays(1).ToShortDateString());
            else if (start.HasValue)
                return string.Format("AND DateDemographyData >= cdate('{0}')", start.Value.ToShortDateString());
            return "";
        }

        public string CreateDemoYearRange(int year)
        {
            DateTime countryReportingYearStart = GetCountry().ReportingYearStartDate;
            DateTime startDate = new DateTime(year, countryReportingYearStart.Month, countryReportingYearStart.Day);
            DateTime endDate = startDate.AddYears(1).AddDays(-1);
            return string.Format(" AND DateDemographyData >= cdate('{0}') AND DateDemographyData <= cdate('{1}') ",
                startDate.ToShortDateString(),
                endDate.ToShortDateString());
        }

        public static string CreateDemoYearRange(DateTime currentDate, DateTime countryReportingYearStart)
        {
            int year = Util.GetYearReported(countryReportingYearStart.Month, currentDate);
            DateTime startDate = new DateTime(year, countryReportingYearStart.Month, countryReportingYearStart.Day);
            DateTime endDate = startDate.AddYears(1).AddDays(-1);
            return string.Format(" AND DateDemographyData >= cdate('{0}') AND DateDemographyData <= cdate('{1}') ", 
                startDate.ToShortDateString(),
                endDate.ToShortDateString());
        }

        public AdminLevelDemography GetDemoById(int id)
        {
            AdminLevelDemography demo = new AdminLevelDemography();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
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
            command = new OleDbCommand(@"Select a.AdminLevelId, a.DateDemographyData,
                            a.YearCensus, a.GrowthRate, a.PercentRural, a.TotalPopulation, a.AdultPopulation, a.Pop0Month, a.PopPsac, 
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
                    demo.DateDemographyData = reader.GetValueOrDefault<DateTime>("DateDemographyData");
                    demo.YearCensus = reader.GetValueOrDefault<Nullable<int>>("YearCensus");
                    demo.GrowthRate = reader.GetValueOrDefault<Nullable<double>>("GrowthRate");
                    demo.PercentRural = reader.GetValueOrDefault<Nullable<double>>("PercentRural");
                    demo.TotalPopulation = reader.GetValueOrDefault<Nullable<double>>("TotalPopulation");
                    demo.Pop0Month = reader.GetValueOrDefault<Nullable<double>>("Pop0Month");
                    demo.PopPsac = reader.GetValueOrDefault<Nullable<double>>("PopPsac");
                    demo.PopSac = reader.GetValueOrDefault<Nullable<double>>("PopSac");
                    demo.Pop5yo = reader.GetValueOrDefault<Nullable<double>>("Pop5yo");
                    demo.PopAdult = reader.GetValueOrDefault<Nullable<double>>("PopAdult");
                    demo.PopFemale = reader.GetValueOrDefault<Nullable<double>>("PopFemale");
                    demo.PopMale = reader.GetValueOrDefault<Nullable<double>>("PopMale");
                    demo.Notes = reader.GetValueOrDefault<string>("Notes");
                    demo.UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt");
                    demo.UpdatedBy = GetAuditInfo(reader);

                }
                reader.Close();
            }
        }

        private void GetDemoByUnitAndDate(AdminLevelDemography demo, int adminLevelUnitId, DateTime dateReported, OleDbConnection connection, OleDbCommand command)
        {
            command = new OleDbCommand(@"Select a.Id, a.AdminLevelId, a.DateDemographyData,
                            a.YearCensus, a.GrowthRate, a.PercentRural, a.TotalPopulation, a.AdultPopulation, a.Pop0Month, a.PopPsac, 
                            a.PopSac, a.Pop5yo, a.PopAdult, a.PopFemale, a.PopMale, a.Notes, a.UpdatedById, a.UpdatedAt, aspnet_Users.UserName, 
                            AdminLevels.DisplayName, a.CreatedAt, c.UserName as CreatedBy
                        FROM (((AdminLevelDemography a INNER JOIN aspnet_Users on a.UpdatedById = aspnet_Users.UserId)
                            LEFT OUTER JOIN AdminLevels on a.AdminLevelId = AdminLevels.ID)
                            INNER JOIN aspnet_Users c on a.CreatedById = c.UserId)
                        WHERE AdminLevels.Id=@aid AND a.DateDemographyData=@DateReported
                        ORDER BY a.Id DESC", connection);
            command.Parameters.Add(new OleDbParameter("@aid", adminLevelUnitId));
            command.Parameters.Add(new OleDbParameter("@DateReported", dateReported));
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    demo.Id = reader.GetValueOrDefault<int>("Id");
                    demo.AdminLevelId = reader.GetValueOrDefault<int>("AdminLevelId");
                    demo.DateDemographyData = reader.GetValueOrDefault<DateTime>("DateDemographyData");
                    demo.YearCensus = reader.GetValueOrDefault<Nullable<int>>("YearCensus");
                    demo.GrowthRate = reader.GetValueOrDefault<Nullable<double>>("GrowthRate");
                    demo.PercentRural = reader.GetValueOrDefault<Nullable<double>>("PercentRural");
                    demo.TotalPopulation = reader.GetValueOrDefault<Nullable<double>>("TotalPopulation");
                    demo.Pop0Month = reader.GetValueOrDefault<Nullable<double>>("Pop0Month");
                    demo.PopPsac = reader.GetValueOrDefault<Nullable<double>>("PopPsac");
                    demo.PopSac = reader.GetValueOrDefault<Nullable<double>>("PopSac");
                    demo.Pop5yo = reader.GetValueOrDefault<Nullable<double>>("Pop5yo");
                    demo.PopAdult = reader.GetValueOrDefault<Nullable<double>>("PopAdult");
                    demo.PopFemale = reader.GetValueOrDefault<Nullable<double>>("PopFemale");
                    demo.PopMale = reader.GetValueOrDefault<Nullable<double>>("PopMale");
                    demo.Notes = reader.GetValueOrDefault<string>("Notes");
                    demo.UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt");
                    demo.UpdatedBy = GetAuditInfo(reader);

                }
                reader.Close();
            }
        }

        public void BulkImportAggregatedDemo(List<AdminLevel> tree, int userId, int aggregatingLevel)
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

                    foreach (var demo in demos)
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

        public void SaveAdminDemography(OleDbCommand command, OleDbConnection connection, AdminLevelDemography demo, int userId)
        {
            if (demo.Id > 0)
                command = new OleDbCommand(@"UPDATE AdminLevelDemography SET AdminLevelId=@AdminLevelId, DateDemographyData=@DateDemographyData,
                            YearCensus=@YearCensus, GrowthRate=@GrowthRate, PercentRural=@PercentRural, TotalPopulation=@TotalPopulation,
                            Pop0Month=@Pop0Month, PopPsac=@PopPsac, PopSac=@PopSac, Pop5yo=@Pop5yo, PopAdult=@PopAdult,
                            PopFemale=@PopFemale, PopMale=@PopMale, Notes=@Notes, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
            else
                command = new OleDbCommand(@"INSERT INTO AdminLevelDemography (AdminLevelId, DateDemographyData,
                            YearCensus,   GrowthRate, PercentRural, TotalPopulation, Pop0Month, PopPsac, 
                            PopSac, Pop5yo, PopAdult, PopFemale, PopMale, Notes, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
                            values (@AdminLevelId, @DateDemographyData, @YearCensus,  @GrowthRate, @PercentRural, @TotalPopulation, 
                             @Pop0Month, @PopPsac, @PopSac, @Pop5yo, @PopAdult, @PopFemale, @PopMale, @Notes, @UpdatedById, @UpdatedAt,
                            @CreatedById, @CreatedAt)", connection);
            command.Parameters.Add(new OleDbParameter("@AdminLevelId", demo.AdminLevelId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@DateDemographyData", demo.DateDemographyData));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@YearCensus", demo.YearCensus));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@GrowthRate", demo.GrowthRate));
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

        #region Redistricting

        public int InsertRedistrictingRecord(OleDbCommand command, OleDbConnection connection, RedistrictingOptions options, int userId)
        {
            command = new OleDbCommand(@"Insert Into RedistrictEvents (RedistrictTypeId, CreatedById, CreatedAt) VALUES
                        (@RedistrictTypeId,  @CreatedById, @CreatedAt)", connection);
            command.Parameters.Add(new OleDbParameter("@RedistrictTypeId", (int)options.SplitType));
            command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
            command.ExecuteNonQuery();

            command = new OleDbCommand(@"SELECT Max(ID) FROM RedistrictEvents", connection);
            int id = (int)command.ExecuteScalar();
            
            return id;
        }

        public void InsertRedistrictUnit(OleDbCommand command, OleDbConnection connection, int userId, AdminLevel unit, int redistrictId, RedistrictingRelationship relationship, double percent)
        {
            command = new OleDbCommand(@"Insert Into RedistrictUnits (AdminLevelUnitId, RelationshipId, Percentage, RedistrictEventId) VALUES
                        (@AdminLevelUnitId, @RelationshipId, @Percentage, @RedistrictEventId)", connection);
            command.Parameters.Add(new OleDbParameter("@AdminLevelUnitId", unit.Id));
            command.Parameters.Add(new OleDbParameter("@RelationshipId", (int)relationship));
            command.Parameters.Add(new OleDbParameter("@Percentage", percent));
            command.Parameters.Add(new OleDbParameter("@RedistrictEventId", redistrictId));
            command.ExecuteNonQuery();

            int motherId = 0, daughterId = 0;
            if (relationship == RedistrictingRelationship.Mother)
                motherId = redistrictId;
            else
            {
                daughterId = redistrictId;
                foreach (var child in unit.Children)
                {
                    command = new OleDbCommand(@"Update AdminLevels SET ParentId=@ParentId where ID = @AdminLevelId", connection);
                    command.Parameters.Add(new OleDbParameter("@ParentId", unit.Id));
                    command.Parameters.Add(new OleDbParameter("@AdminLevelId", child.Id));
                    command.ExecuteNonQuery();
                }
            }

            command = new OleDbCommand(@"Update AdminLevels SET RedistrictIdForDaughter=@RedistrictIdForDaughter, RedistrictIdForMother=@RedistrictIdForMother
                        where ID = @AdminLevelId", connection);
            command.Parameters.Add(new OleDbParameter("@RedistrictIdForDaughter", daughterId));
            command.Parameters.Add(new OleDbParameter("@RedistrictIdForMother", motherId));
            command.Parameters.Add(new OleDbParameter("@AdminLevelId", unit.Id));
            command.ExecuteNonQuery();
        }

        public void InsertRedistrictForm(OleDbCommand command, OleDbConnection connection, int userId, int redistrictId, int sourceId, int destId, IndicatorEntityType entityType)
        {
            command = new OleDbCommand(@"Insert Into RedistrictForms (EntityId, EntityTypeId, RelationshipId, RedistrictEventId) VALUES
                        (@EntityId, @EntityTypeId, @RelationshipId, @RedistrictEventId)", connection);
            command.Parameters.Add(new OleDbParameter("@EntityId", sourceId));
            command.Parameters.Add(new OleDbParameter("@EntityTypeId", (int)entityType));
            command.Parameters.Add(new OleDbParameter("@RelationshipId", (int)RedistrictingRelationship.Mother));
            command.Parameters.Add(new OleDbParameter("@RedistrictEventId", redistrictId));
            command.ExecuteNonQuery();

            command = new OleDbCommand(@"Insert Into RedistrictForms (EntityId, EntityTypeId, RelationshipId, RedistrictEventId) VALUES
                        (@EntityId, @EntityTypeId, @RelationshipId, @RedistrictEventId)", connection);
            command.Parameters.Add(new OleDbParameter("@EntityId", destId));
            command.Parameters.Add(new OleDbParameter("@EntityTypeId", (int)entityType));
            command.Parameters.Add(new OleDbParameter("@RelationshipId", (int)RedistrictingRelationship.Daughter));
            command.Parameters.Add(new OleDbParameter("@RedistrictEventId", redistrictId));
            command.ExecuteNonQuery();
        }

        public string GetRedistrictingNote(int daughterId)
        {
            SplittingType type = SplittingType.SplitCombine;
            List<string> names = new List<string>();
            DateTime createdAt = DateTime.Now;

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select e.ID, e.CreatedAt, a.DisplayName, e.RedistrictTypeId
                        FROM ((RedistrictEvents e INNER JOIN  RedistrictUnits u on e.ID = u.RedistrictEventId)
                            INNER JOIN AdminLevels a on a.ID = u.AdminLevelUnitId)
                        WHERE e.ID = @id AND u.RelationshipId = 1 ", connection);
                    command.Parameters.Add(new OleDbParameter("@id", daughterId));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            type = (SplittingType)reader.GetValueOrDefault<int>("RedistrictTypeId");
                            createdAt = reader.GetValueOrDefault<DateTime>("CreatedAt");
                            names.Add(reader.GetValueOrDefault<string>("DisplayName"));
                        }
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }
            
            if(type == SplittingType.Split)
                return string.Format(TranslationLookup.GetValue("RedistrictingSplitDesc"), string.Join(", ", names.ToArray()), createdAt.ToShortDateString());
            else if(type == SplittingType.Merge)
                return string.Format(TranslationLookup.GetValue("RedistrictingMergeDesc"), string.Join(", ", names.ToArray()), createdAt.ToShortDateString());
            else
                return string.Format(TranslationLookup.GetValue("RedistrictingSplitCombineDesc"), string.Join(", ", names.ToArray()), createdAt.ToShortDateString());
        }
        #endregion


    }
}
