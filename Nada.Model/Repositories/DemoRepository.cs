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
                OleDbCommand command = new OleDbCommand(@"Select Country.ID, DisplayName, aspnet_Users.UserName, Country.UpdatedAt, Country.TaskForceName 
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
                            UpdatedBy = GetAuditInfoUpdate(reader),
                            TaskForceName = reader.GetValueOrDefault<string>("TaskForceName"),
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
                    OleDbCommand command = new OleDbCommand(@"Update Country set MonthYearStarts=1,
                         UpdatedById=@updatedby, UpdatedAt=@updatedat, TaskForceName=@TaskForceName WHERE ID = @id", connection);
                    command.Parameters.Add(new OleDbParameter("@updatedby", byUserId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@updatedat", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@TaskForceName", country.TaskForceName));
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

        public CountryDemography GetCountryLevelStatsRecent()
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();

                CountryDemography demo = new CountryDemography();

                OleDbCommand command = new OleDbCommand(@"Select AdminLevelDemographyId
                        FROM CountryDemography 
                        ORDER BY AdminLevelDemographyId DESC", connection);

                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        int id = reader.GetValueOrDefault<int>("AdminLevelDemographyId");
                        demo = GetCountryDemoById(command, connection, id);
                    }
                    reader.Close();
                }

                // Make sure the Country level is set
                if (demo.AdminLevelId <= 0)
                    demo.AdminLevelId = (int)CommonAdminLevelTypesKeys.Country;

                return demo;
            }
        }

        public CountryDemography GetCountryDemoById(int demoId)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = null;
                return GetCountryDemoById(command, connection, demoId);
            }
        }

        private CountryDemography GetCountryDemoById(OleDbCommand command, OleDbConnection connection, int demoId)
        {
            CountryDemography demo = new CountryDemography();
            // Get existing
            GetDemoById(demo, demoId, connection, command);

            command = new OleDbCommand(@"Select AgeRangePsac, AgeRangeSac, Percent6mos, PercentPsac, PercentSac,  Percent5yo,  
                        PercentFemale, PercentMale, PercentAdult, GrossDomesticProduct, CountryIncomeStatus,
                        LifeExpectBirthFemale, LifeExpectBirthMale
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
                    demo.GrossDomesticProduct = reader.GetValueOrDefault<Nullable<double>>("GrossDomesticProduct");
                    demo.CountryIncomeStatus = reader.GetValueOrDefault<string>("CountryIncomeStatus");
                    demo.LifeExpectBirthFemale = reader.GetValueOrDefault<Nullable<double>>("LifeExpectBirthFemale");
                    demo.LifeExpectBirthMale = reader.GetValueOrDefault<Nullable<double>>("LifeExpectBirthMale");
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

                    SaveCountryDemo(demo, byUserId, connection, command);

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

        private void SaveCountryDemo(CountryDemography demo, int byUserId, OleDbConnection connection, OleDbCommand command)
        {
            SaveAdminDemography(command, connection, demo, byUserId);

            command = new OleDbCommand(@"Delete from CountryDemography WHERE AdminLevelDemographyId=@id", connection);
            command.Parameters.Add(new OleDbParameter("@id", demo.Id));
            command.ExecuteNonQuery();

            command = new OleDbCommand(@"INSERT INTO CountryDemography (AdminLevelDemographyId, AgeRangePsac,
                        AgeRangeSac, Percent6mos, PercentPsac, PercentSac,  Percent5yo,  PercentFemale,PercentMale, PercentAdult,
                        GrossDomesticProduct, CountryIncomeStatus, LifeExpectBirthFemale, LifeExpectBirthMale)
                            values (@AdminLevelDemographyId, @AgeRangePsac,
                        @AgeRangeSac, @Percent6mos, @PercentPsac, @PercentSac, @Percent5yo, @PercentFemale, @PercentMale, 
                        @PercentAdult, @GrossDomesticProduct, @CountryIncomeStatus, @LifeExpectBirthFemale,
                        @LifeExpectBirthMale)", connection);
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
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@GrossDomesticProduct", demo.GrossDomesticProduct));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@CountryIncomeStatus", demo.CountryIncomeStatus));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@LifeExpectBirthFemale", demo.LifeExpectBirthFemale));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@LifeExpectBirthMale", demo.LifeExpectBirthMale));
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
                    WHERE ParentId=@ParentId AND AdminLevels.IsDeleted=0
                    ORDER BY AdminLevels.SortOrder, AdminLevels.DisplayName ", connection);
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
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, AdminLevelTypes.AdminLevel, AdminLevels.TaskForceName, AdminLevels.TaskForceId
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE AdminLevelTypes.AdminLevel = @LevelNumber AND AdminLevels.IsDeleted=0
                    ORDER BY AdminLevels.SortOrder, AdminLevels.DisplayName ", connection);
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
                            LevelNumber = reader.GetValueOrDefault<int>("AdminLevel"),
                            TaskForceName = reader.GetValueOrDefault<string>("TaskForceName"),
                            TaskForceId = reader.GetValueOrDefault<int>("TaskForceId"),
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
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, UrbanOrRural, LatWho, LngWho, AdminLevels.SortOrder,
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
                            SortOrder = reader.GetValueOrDefault<int>("SortOrder"),
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
                    AdminLevelTypes.AdminLevel, AdminLevels.SortOrder
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE AdminLevels.IsDeleted = 0
                    ORDER BY AdminLevelTypes.AdminLevel, AdminLevels.SortOrder, AdminLevels.DisplayName 
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
                            SortOrder = reader.GetValueOrDefault<int>("SortOrder"),
                        });
                    }
                    reader.Close();
                }
            }
            return MakeTreeFromFlatList(list, 0);
        }

        public List<AdminLevel> GetAdminLevelTree(int levelTypeId, bool redistrictedOnly)
        {
            return GetAdminLevelTree(levelTypeId, 1, false, false, levelTypeId, redistrictedOnly, string.Empty, new List<AdminLevel>());
        }

        public List<AdminLevel> GetAdminLevelTree(int levelTypeId, int lowestLevel, bool includeCountry)
        {
            return GetAdminLevelTree(levelTypeId, lowestLevel, includeCountry, false, 0);
        }

        public List<AdminLevel> GetAdminLevelTree(int levelTypeId, int lowestLevel, bool includeCountry, bool allowSelect, int levelToSelect)
        {
            return GetAdminLevelTree(levelTypeId, lowestLevel, includeCountry, allowSelect, levelToSelect, false, string.Empty, new List<AdminLevel>());
        }

        public List<AdminLevel> GetAdminLevelTree(int levelTypeId, int lowestLevel, bool includeCountry, bool allowSelect, int levelToSelect, bool onlyRedistricted, string viewText, List<AdminLevel> listReference)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                string redistrictedFilter = " AND AdminLevels.IsDeleted = 0";
                if (onlyRedistricted)
                    redistrictedFilter = " AND AdminLevels.RedistrictIdForMother>=0";
                connection.Open();
                string cmd = @"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, UrbanOrRural, LatWho, LngWho, AdminLevels.SortOrder, 
                    AdminLevelTypes.AdminLevel, AdminLevelTypes.ID as AdminLevelTypeId, AdminLevelTypes.DisplayName as LevelName, RedistrictIdForMother
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE AdminLevelTypeId <= @AdminLevelTypeId " + redistrictedFilter;
                if (!includeCountry)
                    cmd += " AND ParentId > 0";
                cmd += " ORDER BY AdminLevelTypes.AdminLevel, AdminLevels.SortOrder, AdminLevels.DisplayName ";
                OleDbCommand command = new OleDbCommand(cmd, connection);
                command.Parameters.Add(new OleDbParameter("@AdminLevelTypeId", levelTypeId));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listReference.Add(new AdminLevel
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
                            SortOrder = reader.GetValueOrDefault<int>("SortOrder"),
                        });
                    }
                    reader.Close();
                }
            }
            return MakeTreeFromFlatList(listReference, lowestLevel, allowSelect, levelToSelect, onlyRedistricted, viewText);
        }

        public List<AdminLevel> GetAdminLevelTreeForDemography(int level, DateTime startDate, DateTime endDate, ref List<AdminLevel> list)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, UrbanOrRural, LatWho, LngWho, 
                    AdminLevelTypes.AdminLevel, AdminLevels.SortOrder, AdminLevelTypeId
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE AdminLevel <= @AdminLevel AND AdminLevels.IsDeleted = 0 
                    ORDER BY AdminLevelTypes.AdminLevel, AdminLevels.SortOrder, AdminLevels.DisplayName ", connection);
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
                            AdminLevelTypeId = reader.GetValueOrDefault<int>("AdminLevelTypeId"),
                            SortOrder = reader.GetValueOrDefault<int>("SortOrder"),
                        };
                        al.CurrentDemography = GetDemoByAdminLevelIdAndYear(al.Id, startDate, endDate);
                        list.Add(al);

                    }
                    reader.Close();
                }
            }
            return MakeTreeFromFlatList(list, 0);
        }

        public List<AdminLevel> GetAdminLevelParentNames(int levelId)
        {
            return GetAdminLevelParentNames(levelId, false);
        }

        public List<AdminLevel> GetAdminLevelParentNames(int levelId, bool showDeleted)
        {
            string filter = " AND a.IsDeleted=0";
            if (showDeleted) filter = "";

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
                    WHERE a.ID = @id" + filter, connection);
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

        // TODO: Do i need to change this to do the sort properly? i feel like it could be jacked?
        private List<AdminLevel> MakeTreeFromFlatList(IEnumerable<AdminLevel> flatList, int minRoot, bool allowSelect, int levelToSelect, bool onlyRedistricted, string viewText)
        {
            var dic = flatList.ToDictionary(n => n.Id, n => n);
            var rootNodes = new List<AdminLevel>();
            foreach (var node in flatList)
            {
                if (!string.IsNullOrEmpty(viewText))
                    node.ViewText = viewText;
                if (allowSelect && (levelToSelect != -1 && node.AdminLevelTypeId != levelToSelect))
                    node.ViewText = "";
                if (node.LevelNumber == 0)
                    node.ViewText = "";

                if (node.ParentId.HasValue && node.ParentId.Value > minRoot)
                {
                    AdminLevel parent = dic[node.ParentId.Value];
                    node.Parent = parent;
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
            return MakeTreeFromFlatList(flatList, minRoot, false, 0, false, string.Empty);
        }

        public List<AdminLevel> GetAdminUnitsWithParentsAndChildren(int level, DateTime startDate, DateTime endDate)
        {
            List<AdminLevel> list = new List<AdminLevel>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(@"Select AdminLevels.ID, ParentId, AdminLevels.DisplayName, UrbanOrRural, LatWho, LngWho, 
                    AdminLevelTypes.AdminLevel, AdminLevels.SortOrder, AdminLevelTypeId, TaskForceName
                    FROM AdminLevels inner join AdminLevelTypes on AdminLevels.AdminLevelTypeId = AdminLevelTypes.ID
                    WHERE AdminLevels.IsDeleted = 0 
                    ORDER BY AdminLevels.SortOrder, AdminLevels.DisplayName ", connection);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var al = new AdminLevel
                        {
                            Id = reader.GetValueOrDefault<int>("ID"),
                            ParentId = reader.GetValueOrDefault<Nullable<int>>("ParentId"),
                            Name = reader.GetValueOrDefault<string>("DisplayName"),
                            TaskForceName = reader.GetValueOrDefault<string>("TaskForceName"),
                            LevelNumber = reader.GetValueOrDefault<int>("AdminLevel"),
                            UrbanOrRural = reader.GetValueOrDefault<string>("UrbanOrRural"),
                            LatWho = reader.GetValueOrDefault<Nullable<double>>("LatWho"),
                            LngWho = reader.GetValueOrDefault<Nullable<double>>("LngWho"),
                            AdminLevelTypeId = reader.GetValueOrDefault<int>("AdminLevelTypeId"),
                            SortOrder = reader.GetValueOrDefault<int>("SortOrder"),
                        };
                        al.CurrentDemography = GetDemoByAdminLevelIdAndYear(al.Id, startDate, endDate);
                        list.Add(al);

                    }
                    reader.Close();
                }
            }
            // Build tree
            var dic = list.ToDictionary(n => n.Id, n => n);
            var tree = new List<AdminLevel>();
            foreach (var node in list)
            {
                if (node.ParentId.HasValue)
                {
                    AdminLevel parent = dic[node.ParentId.Value];
                    node.Parent = parent;
                    parent.Children.Add(node);
                }
                else
                {
                    tree.Add(node);
                }
            }
            // only return required list and flatten children
            List<AdminLevel> reportingLevels = list.Where(i => i.LevelNumber == level).ToList();
            foreach (var u in reportingLevels)
                u.Children = u.Children.Flatten(c => c.Children).ToList();
            return reportingLevels;
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

                    command = new OleDbCommand(@"Update AdminLevels set IsDeleted=1, UpdatedById=@UpdatedBy, 
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

        public void ReorderAdminUnits(List<AdminLevel> toUpdate, int byUserId)
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

                    foreach (var unit in toUpdate)
                    {
                        command = new OleDbCommand(@"Update AdminLevels set SortOrder=@SortOrder, UpdatedById=@UpdatedBy, 
                            UpdatedAt=@UpdatedAt WHERE ID=@ID", connection);
                        command.Parameters.Add(new OleDbParameter("@SortOrder", unit.SortOrder));
                        command.Parameters.Add(new OleDbParameter("@UpdatedBy", byUserId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.Parameters.Add(new OleDbParameter("@ID", unit.Id));
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
                            FROM AdminLevels WHERE ParentId=@ParentId AND IsDeleted=0 ", connection);
                        command.Parameters.Add(new OleDbParameter("@ParentId", filterBy.Id));
                    }
                    else if (parentType != null)
                    {
                        command = new OleDbCommand(@"Select AdminLevels.ID, AdminLevels.DisplayName
                            FROM AdminLevels  WHERE AdminLevelTypeId = @AdminLevelTypeId AND IsDeleted=0 ", connection);
                        command.Parameters.Add(new OleDbParameter("@AdminLevelTypeId", parentType.Id));
                    }

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!parentIds.ContainsKey(reader.GetValueOrDefault<string>("DisplayName").ToLower()))
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

        public void UpdateTaskForceData(AdminLevel model, int userid)
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

                    command = new OleDbCommand(@"UPDATE AdminLevels set TaskForceName=@TaskForceName, TaskForceId=@TaskForceId, 
                        UpdatedById=@updatedby, UpdatedAt=@updatedat WHERE ID=@ID", connection);
                    command.Parameters.Add(new OleDbParameter("@TaskForceName", model.TaskForceName));
                    command.Parameters.Add(new OleDbParameter("@TaskForceId", model.TaskForceId));
                    command.Parameters.Add(new OleDbParameter("@updatedby", userid));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@updatedat", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@ID", model.Id));
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
                        command = new OleDbCommand(@"SELECT Max(ID) FROM AdminLevels", connection);
                        model.Id = (int)command.ExecuteScalar();
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
        public void AggregateUp(AdminLevelType locationType, DateTime demoDate, int userId, double? growthRate, int? countryDemoId)
        {
            try
            {
                var c = GetCountry();
                DateTime startDate = new DateTime(demoDate.Year, 1, 1);
                DateTime endDate = startDate.AddYears(1).AddDays(-1);

                List<AdminLevel> list = new List<AdminLevel>();
                var tree = GetAdminLevelTreeForDemography(locationType.LevelNumber, startDate, endDate, ref list);
                var country = tree.FirstOrDefault();
                CountryDemography countryDemo = null;
                if (countryDemoId.HasValue)
                    countryDemo = GetCountryDemoById(countryDemoId.Value);
                else
                    countryDemo = GetCountryDemoRecent();
                if (!growthRate.HasValue)
                    growthRate = countryDemo.GrowthRate;

                country.CurrentDemography = IndicatorAggregator.AggregateTree(country, growthRate);
                if (countryDemoId.HasValue)
                    country.CurrentDemography.Id = countryDemoId.Value;

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
                        if (d.Pop0Month.HasValue) d.Pop0Month = (int)Math.Round((double)d.Pop0Month * growthRateDemonminator) + d.Pop0Month;
                        if (d.Pop5yo.HasValue) d.Pop5yo = (int)Math.Round((double)d.Pop5yo * growthRateDemonminator) + d.Pop5yo;
                        if (d.PopAdult.HasValue) d.PopAdult = (int)Math.Round((double)d.PopAdult * growthRateDemonminator) + d.PopAdult;
                        if (d.PopFemale.HasValue) d.PopFemale = (int)Math.Round((double)d.PopFemale * growthRateDemonminator) + d.PopFemale;
                        if (d.PopMale.HasValue) d.PopMale = (int)Math.Round((double)d.PopMale * growthRateDemonminator) + d.PopMale;
                        if (d.PopPsac.HasValue) d.PopPsac = (int)Math.Round((double)d.PopPsac * growthRateDemonminator) + d.PopPsac;
                        if (d.PopPsac.HasValue) d.PopSac = (int)Math.Round((double)d.PopSac * growthRateDemonminator) + d.PopSac;
                        if (d.PopPsac.HasValue) d.TotalPopulation = (int)Math.Round((double)d.TotalPopulation * growthRateDemonminator) + d.TotalPopulation;
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

        public void Delete(AdminLevel unit, int userId)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"UPDATE AdminLevels SET IsDeleted=@IsDeleted,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    command.Parameters.Add(new OleDbParameter("@IsDeleted", true));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", unit.Id));
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
                            TotalPopulation = reader.GetValueOrDefault<int>("TotalPopulation"),
                            UpdatedBy = GetAuditInfoUpdate(reader)
                        });
                    }
                    reader.Close();
                }
            }
            return demo;
        }

        public AdminLevelDemography GetRecentDemography(int adminLevelId, DateTime? start, DateTime? end)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                return GetRecentDemography(adminLevelId, start, end, connection);
            }
        }

        public AdminLevelDemography GetRecentDemography(int adminLevelId, DateTime? start, DateTime? end, OleDbConnection connection)
        {
            AdminLevelDemography demog = new AdminLevelDemography { AdminLevelId = adminLevelId };
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

        public AdminLevelDemography GetDemoByAdminLevelIdAndYear(int adminLevelid, DateTime startDate, DateTime endDate)
        {
            AdminLevelDemography demo = new AdminLevelDemography { DateDemographyData = startDate, AdminLevelId = adminLevelid };
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
                        WHERE AdminLevelId = @id and IsDeleted = 0 " + CreateDemoYearRange(startDate, endDate)
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
            DateTime startDate = new DateTime(year, 1, 1);
            DateTime endDate = startDate.AddYears(1).AddDays(-1);
            return string.Format(" AND DateDemographyData >= cdate('{0}') AND DateDemographyData <= cdate('{1}') ",
                startDate.ToShortDateString(),
                endDate.ToShortDateString());
        }

        public static string CreateDemoYearRange(DateTime startDate, DateTime endDate)
        {
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
            command = new OleDbCommand(@"Insert Into RedistrictEvents (RedistrictTypeId, CreatedById, CreatedAt, RedistrictDate) VALUES
                        (@RedistrictTypeId,  @CreatedById, @CreatedAt, @RedistrictDate)", connection);
            command.Parameters.Add(new OleDbParameter("@RedistrictTypeId", (int)options.SplitType));
            command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@RedistrictDate", options.RedistrictDate));
            command.ExecuteNonQuery();

            command = new OleDbCommand(@"SELECT Max(ID) FROM RedistrictEvents", connection);
            int id = (int)command.ExecuteScalar();

            return id;
        }

        public void InsertRedistrictUnit(OleDbCommand command, OleDbConnection connection, int userId, AdminLevel unit, int redistrictId,
            RedistrictingRelationship relationship, double percent, bool isDeleted)
        {
            command = new OleDbCommand(@"Insert Into RedistrictUnits (AdminLevelUnitId, RelationshipId, Percentage, RedistrictEventId) VALUES
                        (@AdminLevelUnitId, @RelationshipId, @Percentage, @RedistrictEventId)", connection);
            command.Parameters.Add(new OleDbParameter("@AdminLevelUnitId", unit.Id));
            command.Parameters.Add(new OleDbParameter("@RelationshipId", (int)relationship));
            command.Parameters.Add(new OleDbParameter("@Percentage", percent));
            command.Parameters.Add(new OleDbParameter("@RedistrictEventId", redistrictId));
            command.ExecuteNonQuery();

            if (relationship == RedistrictingRelationship.Mother)
            {
                command = new OleDbCommand(@"Update AdminLevels SET RedistrictIdForMother=@RedistrictIdForMother, IsDeleted=@IsDeleted 
                        where ID = @AdminLevelId", connection);
                command.Parameters.Add(new OleDbParameter("@RedistrictIdForMother", redistrictId));
                command.Parameters.Add(new OleDbParameter("@IsDeleted", isDeleted));
                command.Parameters.Add(new OleDbParameter("@AdminLevelId", unit.Id));
                command.ExecuteNonQuery();

            }
            else // for daughters add the new admin levels
            {
                command = new OleDbCommand(@"Update AdminLevels SET RedistrictIdForDaughter=@RedistrictIdForDaughter
                        where ID = @AdminLevelId", connection);
                command.Parameters.Add(new OleDbParameter("@RedistrictIdForDaughter", redistrictId));
                command.Parameters.Add(new OleDbParameter("@AdminLevelId", unit.Id));
                command.ExecuteNonQuery();
                foreach (var child in unit.Children)
                {
                    command = new OleDbCommand(@"Update AdminLevels SET ParentId=@ParentId where ID = @AdminLevelId", connection);
                    command.Parameters.Add(new OleDbParameter("@ParentId", unit.Id));
                    command.Parameters.Add(new OleDbParameter("@AdminLevelId", child.Id));
                    command.ExecuteNonQuery();
                }
            }

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

        public string GetRedistrictingDaughterNote(int daughterId)
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
                    OleDbCommand command = new OleDbCommand(@"Select e.ID, e.RedistrictDate, a.DisplayName, e.RedistrictTypeId
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
                            createdAt = reader.GetValueOrDefault<DateTime>("RedistrictDate");
                            names.Add(reader.GetValueOrDefault<string>("DisplayName"));
                        }
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }

            if (type == SplittingType.Split)
                return string.Format(TranslationLookup.GetValue("RedistrictingSplitDesc"), string.Join(", ", names.ToArray()), createdAt.ToShortDateString());
            else if (type == SplittingType.Merge)
                return string.Format(TranslationLookup.GetValue("RedistrictingMergeDesc"), string.Join(", ", names.ToArray()), createdAt.ToShortDateString());
            else
                return string.Format(TranslationLookup.GetValue("RedistrictingSplitCombineDesc"), string.Join(", ", names.ToArray()), createdAt.ToShortDateString());
        }

        public string GetRedistrictingMotherNote(int motherId)
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
                    OleDbCommand command = new OleDbCommand(@"Select e.ID, e.RedistrictDate, a.DisplayName, e.RedistrictTypeId
                        FROM ((RedistrictEvents e INNER JOIN  RedistrictUnits u on e.ID = u.RedistrictEventId)
                            INNER JOIN AdminLevels a on a.ID = u.AdminLevelUnitId)
                        WHERE e.ID = @id AND u.RelationshipId = 2 ", connection);
                    command.Parameters.Add(new OleDbParameter("@id", motherId));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            type = (SplittingType)reader.GetValueOrDefault<int>("RedistrictTypeId");
                            createdAt = reader.GetValueOrDefault<DateTime>("RedistrictDate");
                            names.Add(reader.GetValueOrDefault<string>("DisplayName"));
                        }
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }

            if (type == SplittingType.Split)
                return string.Format(TranslationLookup.GetValue("RedistrictingSplitInto"), string.Join(", ", names.ToArray()), createdAt.ToShortDateString());
            else if (type == SplittingType.Merge)
                return string.Format(TranslationLookup.GetValue("RedistrictingMergeInto"), string.Join(", ", names.ToArray()), createdAt.ToShortDateString());
            else
                return string.Format(TranslationLookup.GetValue("RedistrictingSplitCombineInto"), string.Join(", ", names.ToArray()), createdAt.ToShortDateString());
        }

        public List<Indicator> GetCustomIndicatorsWithoutRedistrictingRules(SplittingType type)
        {
            List<Indicator> indicators = new List<Indicator>();

            string typeWhere = "";
            if (type == SplittingType.Merge)
                typeWhere = " AND MergeRuleId = 1";
            else if (type == SplittingType.Split)
                typeWhere = " AND RedistrictRuleId = 1";
            else
                typeWhere = " AND (MergeRuleId = 1 OR RedistrictRuleId = 1)";

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = command = new OleDbCommand(@"Select 
                        InterventionIndicators.ID,   
                        InterventionIndicators.DisplayName,
                        InterventionTypes.InterventionTypeName as TName,                  
                        InterventionIndicators.RedistrictRuleId,
                        InterventionIndicators.MergeRuleId,
                        InterventionIndicators.DataTypeId
                        FROM ((InterventionIndicators INNER JOIN InterventionTypes_to_Indicators ON InterventionTypes_to_Indicators.IndicatorId = InterventionIndicators.ID)
                            INNER JOIN InterventionTypes on InterventionTypes_to_Indicators.InterventionTypeId = InterventionTypes.Id)
                        WHERE IsEditable=-1 AND IsDisabled=0" + typeWhere, connection);
                    AddCustomIndicators(indicators, command, IndicatorEntityType.Intervention);

                    // process
                    command = command = new OleDbCommand(@"Select 
                        ProcessIndicators.ID,   
                        ProcessIndicators.DisplayName,
                        ProcessTypes.TypeName as TName,                  
                        ProcessIndicators.RedistrictRuleId,
                        ProcessIndicators.MergeRuleId,
                        ProcessIndicators.DataTypeId
                        FROM ProcessIndicators INNER JOIN ProcessTypes on ProcessIndicators.ProcessTypeId = ProcessTypes.Id
                        WHERE IsEditable=-1 AND IsDisabled=0" + typeWhere, connection);
                    AddCustomIndicators(indicators, command, IndicatorEntityType.Process);
                    // Survey
                    command = command = new OleDbCommand(@"Select 
                        SurveyIndicators.ID,   
                        SurveyIndicators.DisplayName,
                        SurveyTypes.SurveyTypeName as TName,                  
                        SurveyIndicators.RedistrictRuleId,
                        SurveyIndicators.MergeRuleId,
                        SurveyIndicators.DataTypeId
                        FROM SurveyIndicators INNER JOIN SurveyTypes on SurveyIndicators.SurveyTypeId = SurveyTypes.Id
                        WHERE IsEditable=-1 AND IsDisabled=0" + typeWhere, connection);
                    AddCustomIndicators(indicators, command, IndicatorEntityType.Survey);
                    // distro
                    command = command = new OleDbCommand(@"Select 
                        DiseaseDistributionIndicators.ID,   
                        DiseaseDistributionIndicators.DisplayName,
                        Diseases.DisplayName as TName,                  
                        DiseaseDistributionIndicators.RedistrictRuleId,
                        DiseaseDistributionIndicators.MergeRuleId,
                        DiseaseDistributionIndicators.DataTypeId
                        FROM DiseaseDistributionIndicators INNER JOIN Diseases on DiseaseDistributionIndicators.DiseaseId = Diseases.Id
                        WHERE IsEditable=-1 AND IsDisabled=0" + typeWhere, connection);
                    AddCustomIndicators(indicators, command, IndicatorEntityType.DiseaseDistribution);





                }
                catch (Exception)
                {
                    throw;
                }
            }

            return indicators;
        }

        private static void AddCustomIndicators(List<Indicator> indicators, OleDbCommand command, IndicatorEntityType entityType)
        {
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    indicators.Add(new Indicator
                    {
                        Id = reader.GetValueOrDefault<int>("ID"),
                        RedistrictRuleId = reader.GetValueOrDefault<int>("RedistrictRuleId"),
                        MergeRuleId = reader.GetValueOrDefault<int>("MergeRuleId"),
                        DisplayName = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("TName"),
                        reader.GetValueOrDefault<string>("TName")) + " > " + reader.GetValueOrDefault<string>("DisplayName"),
                        EntityId = (int)entityType,
                        DataTypeId = reader.GetValueOrDefault<int>("DataTypeId")
                    });
                }
                reader.Close();
            }
        }

        public void SaveCustomIndicatorRules(List<Indicator> inds)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    foreach (Indicator ind in inds)
                    {
                        OleDbCommand command = null;
                        if (ind.EntityId == (int)IndicatorEntityType.Intervention)
                            command = new OleDbCommand(@"Update InterventionIndicators SET RedistrictRuleId=@RedistrictRuleId, MergeRuleId=@MergeRuleId 
                                where ID = @ID", connection);
                        else if (ind.EntityId == (int)IndicatorEntityType.Process)
                            command = new OleDbCommand(@"Update ProcessIndicators SET RedistrictRuleId=@RedistrictRuleId, MergeRuleId=@MergeRuleId 
                                where ID = @ID", connection);
                        else if (ind.EntityId == (int)IndicatorEntityType.Survey)
                            command = new OleDbCommand(@"Update SurveyIndicators SET RedistrictRuleId=@RedistrictRuleId, MergeRuleId=@MergeRuleId 
                                where ID = @ID", connection);
                        else if (ind.EntityId == (int)IndicatorEntityType.DiseaseDistribution)
                            command = new OleDbCommand(@"Update DiseaseDistributionIndicators SET RedistrictRuleId=@RedistrictRuleId, MergeRuleId=@MergeRuleId 
                                where ID = @ID", connection);

                        command.Parameters.Add(new OleDbParameter("@RedistrictRuleId", ind.RedistrictRuleId));
                        command.Parameters.Add(new OleDbParameter("@MergeRuleId", ind.MergeRuleId));
                        command.Parameters.Add(new OleDbParameter("@ID", ind.Id));
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





    }
}
