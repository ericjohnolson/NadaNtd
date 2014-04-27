using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.DA;
using Nada.Globalization;
using Nada.Model.Base;
using Nada.Model.Diseases;
using Nada.Model.Intervention;

namespace Nada.Model.Repositories
{
    public class IntvRepository : RepositoryBase
    {
        #region interventions
        public T CreateIntv<T>(int typeId) where T : IntvBase
        {
            var intv = (T)Activator.CreateInstance(typeof(T));
            IntvType t = GetIntvType((int)typeId);
            intv.IntvType = t;
            return intv;
        }

        public IntvBase CreateIntv(int typeId)
        {
            IntvBase Intv = new IntvBase();
            Intv.IntvType = GetIntvType((int)typeId);
            return Intv;
        }

        public void Delete(IntvDetails intv, int userId)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"UPDATE Interventions SET IsDeleted=@IsDeleted,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);

                    command.Parameters.Add(new OleDbParameter("@IsDeleted", true));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", intv.Id));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<IntvDetails> GetAllForAdminLevel(int adminLevel)
        {
            List<IntvDetails> interventions = new List<IntvDetails>();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select 
                        Interventions.ID, 
                        InterventionTypes.InterventionTypeName, 
                        Interventions.InterventionTypeId, 
                        Interventions.DateReported,
                        Interventions.StartDate, 
                        Interventions.EndDate, 
                        Interventions.UpdatedAt, 
                        aspnet_Users.UserName, AdminLevels.DisplayName,
                        created.UserName as CreatedBy, Interventions.CreatedAt
                        FROM ((((Interventions INNER JOIN InterventionTypes on Interventions.InterventionTypeId = InterventionTypes.ID)
                            INNER JOIN aspnet_Users on Interventions.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN AdminLevels on Interventions.AdminLevelId = AdminLevels.ID) 
                            INNER JOIN aspnet_Users created on Interventions.CreatedById = created.UserId)
                        WHERE Interventions.AdminLevelId=@AdminLevelId and Interventions.IsDeleted = 0
                        ORDER BY Interventions.EndDate DESC", connection);
                    command.Parameters.Add(new OleDbParameter("@AdminLevelId", adminLevel));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            interventions.Add(new IntvDetails
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                TypeName = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("InterventionTypeName"),
                                    reader.GetValueOrDefault<string>("InterventionTypeName")),
                                TypeId = reader.GetValueOrDefault<int>("InterventionTypeId"),
                                AdminLevel = reader.GetValueOrDefault<string>("DisplayName"),
                                DateReported = reader.GetValueOrDefault<DateTime>("DateReported"),
                                StartDate = reader.GetValueOrDefault<DateTime>("StartDate"),
                                EndDate = reader.GetValueOrDefault<Nullable<DateTime>>("EndDate"),
                                UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt"),
                                UpdatedBy = GetAuditInfo(reader)

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
            return interventions;
        }

        public List<IntvType> GetAllTypes()
        {
            List<IntvType> intv = new List<IntvType>();

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select InterventionTypes.ID, InterventionTypes.InterventionTypeName, Diseases.DiseaseType
                        FROM ((InterventionTypes INNER JOIN InterventionTypes_to_Diseases ON InterventionTypes.ID = InterventionTypes_to_Diseases.InterventionTypeId)
                            INNER JOIN Diseases ON Diseases.ID = InterventionTypes_to_Diseases.DiseaseId) 
                        WHERE Diseases.IsSelected = yes
                        GROUP BY InterventionTypes.ID, InterventionTypes.InterventionTypeName, Diseases.DiseaseType", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var name = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("InterventionTypeName"),
                                    reader.GetValueOrDefault<string>("InterventionTypeName"));
                            if (reader.GetValueOrDefault<string>("DiseaseType") == "Custom")
                                name = reader.GetValueOrDefault<string>("InterventionTypeName");
                                
                            intv.Add(new IntvType
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                IntvTypeName = name,
                                DiseaseType = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("DiseaseType"),
                                    reader.GetValueOrDefault<string>("DiseaseType"))
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
            return intv;
        }

        public IntvType GetIntvType(int id)
        {
            IntvType intv = new IntvType();
            DiseaseRepository repo = new DiseaseRepository();
            var selectedDiseases = repo.GetSelectedDiseases();
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select InterventionTypes.InterventionTypeName, InterventionTypes.UpdatedAt,
                        aspnet_users.UserName, created.UserName as CreatedBy, InterventionTypes.CreatedAt, Diseases.DiseaseType
                        FROM ((((InterventionTypes INNER JOIN aspnet_Users on InterventionTypes.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN aspnet_Users created on InterventionTypes.CreatedById = created.UserId)
                            INNER JOIN InterventionTypes_to_Diseases itod on InterventionTypes.ID = itod.InterventionTypeId)
                            INNER JOIN Diseases on itod.DiseaseId = Diseases.Id) 
                        WHERE InterventionTypes.ID=@id
                        GROUP BY InterventionTypes.InterventionTypeName, InterventionTypes.UpdatedAt,
                            aspnet_users.UserName, created.UserName, InterventionTypes.CreatedAt, Diseases.DiseaseType", connection);
                    command.Parameters.Add(new OleDbParameter("@id", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            var name = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("InterventionTypeName"),
                                    reader.GetValueOrDefault<string>("InterventionTypeName"));
                            if (reader.GetValueOrDefault<string>("DiseaseType") == "Custom")
                                name = reader.GetValueOrDefault<string>("InterventionTypeName");
                                
                            intv = new IntvType
                            {
                                Id = id,
                                IntvTypeName = name,
                                DiseaseType = reader.GetValueOrDefault<string>("DiseaseType"),
                                UpdatedBy = GetAuditInfo(reader)
                            };
                        }
                        reader.Close();
                    }

                    List<string> indicatorIds = new List<string>();
                    command = new OleDbCommand(@"Select 
                        InterventionIndicators.ID,   
                        InterventionIndicators.DataTypeId,
                        InterventionIndicators.DisplayName,
                        InterventionIndicators.IsRequired,
                        InterventionIndicators.IsDisabled,
                        InterventionIndicators.IsEditable,
                        InterventionIndicators.IsDisplayed,
                        IsCalculated,
                        CanAddValues,
                        InterventionIndicators.UpdatedAt, 
                        aspnet_users.UserName,
                        IndicatorDataTypes.DataType,
                        InterventionIndicators.IsMetaData
                        FROM (((InterventionIndicators INNER JOIN aspnet_users ON InterventionIndicators.UpdatedById = aspnet_users.UserId)
                        INNER JOIN IndicatorDataTypes ON InterventionIndicators.DataTypeId = IndicatorDataTypes.ID)
                        INNER JOIN InterventionTypes_to_Indicators ON InterventionTypes_to_Indicators.IndicatorId = InterventionIndicators.ID)
                        WHERE InterventionTypes_to_Indicators.InterventionTypeId=@InterventionTypeId AND IsDisabled=0 AND " +
                        "DiseaseId in (0," + String.Join(", ", selectedDiseases.Select(i => i.Id.ToString()).ToArray()) + ")" +
                        "ORDER BY SortOrder, InterventionIndicators.ID", connection);
                    command.Parameters.Add(new OleDbParameter("@InterventionTypeId", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            intv.Indicators.Add(reader.GetValueOrDefault<string>("DisplayName"),
                                new Indicator
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DataTypeId = reader.GetValueOrDefault<int>("DataTypeId"),
                                UpdatedBy = reader.GetValueOrDefault<DateTime>("UpdatedAt").ToShortDateString() + " by " +
                                    reader.GetValueOrDefault<string>("UserName"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                                IsRequired = reader.GetValueOrDefault<bool>("IsRequired"),
                                IsDisabled = reader.GetValueOrDefault<bool>("IsDisabled"),
                                IsEditable = reader.GetValueOrDefault<bool>("IsEditable"),
                                IsDisplayed = reader.GetValueOrDefault<bool>("IsDisplayed"),
                                IsCalculated = reader.GetValueOrDefault<bool>("IsCalculated"),
                                CanAddValues = reader.GetValueOrDefault<bool>("CanAddValues"),
                                DataType = reader.GetValueOrDefault<string>("DataType"),
                                IsMetaData = reader.GetValueOrDefault<bool>("IsMetaData")
                            });
                            indicatorIds.Add(reader.GetValueOrDefault<int>("ID").ToString());
                        }
                        reader.Close();
                    }

                    intv.IndicatorDropdownValues = GetIndicatorDropdownValues(connection, command, IndicatorEntityType.Intervention, indicatorIds);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return intv;
        }

        public IntvBase GetById(int id)
        {
            IntvBase intv = null;
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = null;
                    intv = GetIntv<IntvBase>(command, connection, id);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return intv;
        }

     

        public void Save(List<IntvBase> import, int userId)
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

                    foreach (var intv in import)
                    {
                        intv.MapIndicatorsToProperties();
                        SaveIntvBase(command, connection, intv, userId);
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

        public void SaveBase(IntvBase intv, int userId)
        {
            intv.MapIndicatorsToProperties();
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

                    SaveIntvBase(command, connection, intv, userId);

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

   
        public void Save(IntvType model, int userId)
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
                        command = new OleDbCommand(@"UPDATE InterventionTypes SET InterventionTypeName=@InterventionTypeName, UpdatedById=@UpdatedById, 
                            UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO InterventionTypes (InterventionTypeName, UpdatedById, 
                            UpdatedAt, CreatedById, CreatedAt) values (@InterventionTypeName, @UpdatedById, @UpdatedAt,
                            @CreatedById, @CreatedAt)", connection);
                    command.Parameters.Add(new OleDbParameter("@InterventionTypeName", model.IntvTypeName));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (model.Id > 0)
                        command.Parameters.Add(new OleDbParameter("@id", model.Id));
                    else
                    {
                        command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
                    }
                    command.ExecuteNonQuery();

                    if (model.Id <= 0)
                    {
                        command = new OleDbCommand(@"SELECT Max(ID) FROM InterventionTypes", connection);
                        model.Id = (int)command.ExecuteScalar();

                        // When inserting, assign custom disease to type
                        command = new OleDbCommand(@"INSERT INTO InterventionTypes_to_Diseases (InterventionTypeId, DiseaseId
                            ) values (@InterventionTypeId, @DiseaseId)", connection);
                        command.Parameters.Add(new OleDbParameter("@InterventionTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@DiseaseId", (int)DiseaseType.Custom));
                        command.ExecuteNonQuery();
                        // Add year reported
                        command = new OleDbCommand(@"INSERT INTO InterventionIndicators (InterventionTypeId, DataTypeId, AggTypeId, 
                        DisplayName, IsRequired, IsDisabled, IsEditable, IsDisplayed, SortOrder, UpdatedById, UpdatedAt) VALUES
                        (@InterventionTypeId, 4, 5, 'DateReported', -1, 0, 0, 0, -1, @UpdatedById, @UpdatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@InterventionTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@UpdateById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.ExecuteNonQuery();
                        command = new OleDbCommand(@"SELECT Max(ID) FROM InterventionIndicators", connection);
                        int yearId = (int)command.ExecuteScalar();

                        command = new OleDbCommand(@"INSERT INTO InterventionTypes_to_Indicators (InterventionTypeId, IndicatorId
                            ) values (@InterventionTypeId, @IndicatorId)", connection);
                        command.Parameters.Add(new OleDbParameter("@InterventionTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@IndicatorId", yearId));
                        command.ExecuteNonQuery();
                    }

                    foreach (var indicator in model.Indicators.Values.Where(i => i.Id > 0 && i.IsEdited))
                    {
                        command = new OleDbCommand(@"UPDATE InterventionIndicators SET InterventionTypeId=@InterventionTypeId, DataTypeId=@DataTypeId,
                        DisplayName=@DisplayName, IsRequired=@IsRequired, IsDisabled=@IsDisabled, 
                        IsEditable=@IsEditable, UpdatedById=@UpdateById, UpdatedAt=@UpdatedAt 
                        WHERE ID = @id", connection);
                        command.Parameters.Add(new OleDbParameter("@InterventionTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@DataTypeId", indicator.DataTypeId));
                        command.Parameters.Add(new OleDbParameter("@DisplayName", indicator.DisplayName));
                        command.Parameters.Add(new OleDbParameter("@IsRequired", indicator.IsRequired));
                        command.Parameters.Add(new OleDbParameter("@IsDisabled", indicator.IsDisabled));
                        command.Parameters.Add(new OleDbParameter("@IsEditable", true));
                        command.Parameters.Add(new OleDbParameter("@UpdateById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.Parameters.Add(new OleDbParameter("@id", indicator.Id));
                        command.ExecuteNonQuery();
                    }

                    foreach (var indicator in model.Indicators.Values.Where(i => i.Id <= 0 && i.IsEdited))
                    {
                        command = new OleDbCommand(@"INSERT INTO InterventionIndicators (InterventionTypeId, DataTypeId, 
                        DisplayName, IsRequired, IsDisabled, IsEditable, UpdatedById, UpdatedAt) VALUES
                        (@InterventionTypeId, @DataTypeId, @DisplayName, @IsRequired, @IsDisabled, @IsEditable, @UpdatedById, 
                         @UpdatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@InterventionTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@DataTypeId", indicator.DataTypeId));
                        command.Parameters.Add(new OleDbParameter("@DisplayName", indicator.DisplayName));
                        command.Parameters.Add(new OleDbParameter("@IsRequired", indicator.IsRequired));
                        command.Parameters.Add(new OleDbParameter("@IsDisabled", indicator.IsDisabled));
                        command.Parameters.Add(new OleDbParameter("@IsEditable", true));
                        command.Parameters.Add(new OleDbParameter("@UpdateById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                        command.ExecuteNonQuery();

                        command = new OleDbCommand(@"SELECT Max(ID) FROM InterventionIndicators", connection);
                        indicator.Id = (int)command.ExecuteScalar();

                        command = new OleDbCommand(@"INSERT INTO InterventionTypes_to_Indicators (InterventionTypeId, IndicatorId
                            ) values (@InterventionTypeId, @IndicatorId)", connection);
                        command.Parameters.Add(new OleDbParameter("@InterventionTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@IndicatorId", indicator.Id));
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
        #endregion

        #region Partners

        public List<Partner> GetPartners()
        {
            List<Partner> list = new List<Partner>();

            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName, aspnet_users.UserName, UpdatedAt, CreatedAt, c.UserName as CreatedBy from 
                        ((Partners INNER JOIN aspnet_users on Partners.UpdatedById = aspnet_users.userid)
                        INNER JOIN aspnet_users c on Partners.CreatedById = c.userid)
                        WHERE IsDeleted=0", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Partner
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                                UpdatedBy = GetAuditInfo(reader)
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

        public void Save(Partner partner, int userId)
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

                    if (partner.Id > 0)
                        command = new OleDbCommand(@"UPDATE Partners SET DisplayName=@DisplayName,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO Partners (DisplayName, UpdatedById, 
                            UpdatedAt, CreatedById, CreatedAt) values (@DisplayName, @UpdatedById, @UpdatedAt, @CreatedById,
                            @CreatedAt)", connection);

                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@DisplayName", partner.DisplayName));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (partner.Id > 0)
                        command.Parameters.Add(new OleDbParameter("@id", partner.Id));
                    else
                    {
                        command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
                        command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
                    }

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

        #endregion

        #region Private Methods
        public T GetIntv<T>(OleDbCommand command, OleDbConnection connection, int id) where T : IntvBase
        {
            var intv = (T)Activator.CreateInstance(typeof(T));
            int typeId = 0;

            try
            {
                command = new OleDbCommand(@"Select Interventions.AdminLevelId, DateReported, PcIntvRoundNumber, Interventions.StartDate, Interventions.EndDate, 
                        Interventions.Notes, Interventions.UpdatedById, Interventions.UpdatedAt, aspnet_Users.UserName, 
                        AdminLevels.DisplayName, Interventions.InterventionTypeId, Interventions.CreatedAt, c.UserName as CreatedBy
                        FROM (((Interventions INNER JOIN aspnet_Users on Interventions.UpdatedById = aspnet_Users.UserId)
                            LEFT OUTER JOIN AdminLevels on Interventions.AdminLevelId = AdminLevels.ID)
                            INNER JOIN aspnet_Users c on Interventions.CreatedById = c.UserId)
                        WHERE Interventions.ID=@id", connection);
                command.Parameters.Add(new OleDbParameter("@id", id));
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        intv.Id = id;
                        intv.AdminLevelId = reader.GetValueOrDefault<Nullable<int>>("AdminLevelId");
                        intv.DateReported = reader.GetValueOrDefault<DateTime>("DateReported");
                        intv.StartDate = reader.GetValueOrDefault<DateTime>("StartDate");
                        intv.EndDate = reader.GetValueOrDefault<DateTime>("EndDate");
                        intv.PcIntvRoundNumber = reader.GetValueOrDefault<Nullable<int>>("PcIntvRoundNumber");
                        intv.Notes = reader.GetValueOrDefault<string>("Notes");
                        intv.UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt");
                        intv.UpdatedBy = GetAuditInfo(reader);
                        typeId = reader.GetValueOrDefault<int>("InterventionTypeId");
                    }
                    reader.Close();
                }

                intv.IntvType = GetIntvType(typeId);
                GetIntvIndicatorValues(connection, intv);
                intv.MapIndicatorsToProperties();
            }
            catch (Exception)
            {
                throw;
            }

            return intv;
        }

        private void SaveIntvBase(OleDbCommand command, OleDbConnection connection, IntvBase intv, int userId)
        {
            if (intv.Id > 0)
                command = new OleDbCommand(@"UPDATE Interventions SET InterventionTypeId=@InterventionTypeId, AdminLevelId=@AdminLevelId, DateReported=@DateReported,
                           PcIntvRoundNumber=@PcIntvRoundNumber, StartDate=@StartDate, EndDate=@EndDate, Notes=@Notes, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
            else
                command = new OleDbCommand(@"INSERT INTO Interventions (InterventionTypeId, AdminLevelId, DateReported, PcIntvRoundNumber, StartDate, EndDate, Notes, 
                            UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
                            values (@InterventionTypeId, @AdminLevelId, @DateReported, @PcIntvRoundNumber, @StartDate, @EndDate, @Notes, @UpdatedById, @UpdatedAt,
                            @CreatedById, @CreatedAt)", connection); 
            command.Parameters.Add(new OleDbParameter("@InterventionTypeId", intv.IntvType.Id));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@AdminLevelId", intv.AdminLevelId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@DateReported", intv.DateReported));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@PcIntvRoundNumber", intv.PcIntvRoundNumber));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@StartDate", intv.StartDate));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@EndDate", intv.EndDate));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@Notes", intv.Notes));
            command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
            if (intv.Id > 0)
                command.Parameters.Add(new OleDbParameter("@id", intv.Id));
            else
            {
                command.Parameters.Add(new OleDbParameter("@CreatedById", userId));
                command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@CreatedAt", DateTime.Now));
            }
            command.ExecuteNonQuery();

            if (intv.Id <= 0)
            {
                command = new OleDbCommand(@"SELECT Max(ID) FROM Interventions", connection);
                intv.Id = (int)command.ExecuteScalar();
            }

            AddIntvIndicatorValues(connection, intv, userId);
        }

        private void AddIntvIndicatorValues(OleDbConnection connection, IntvBase intv, int userId)
        {
            OleDbCommand command = new OleDbCommand(@"DELETE FROM InterventionIndicatorValues WHERE InterventionId=@InterventionId", connection);
            command.Parameters.Add(new OleDbParameter("@InterventionId", intv.Id));
            command.ExecuteNonQuery();

            foreach (IndicatorValue val in intv.IndicatorValues)
            {
                command = new OleDbCommand(@"Insert Into InterventionIndicatorValues (IndicatorId, InterventionId, DynamicValue, UpdatedById, UpdatedAt) VALUES
                        (@IndicatorId, @InterventionId, @DynamicValue, @UpdatedById, @UpdatedAt)", connection);
                command.Parameters.Add(new OleDbParameter("@IndicatorId", val.IndicatorId));
                command.Parameters.Add(new OleDbParameter("@InterventionId", intv.Id));
                command.Parameters.Add(OleDbUtil.CreateNullableParam("@DynamicValue", val.DynamicValue));
                command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                command.ExecuteNonQuery();
            }
        }

        private void GetIntvIndicatorValues(OleDbConnection connection, IntvBase intv)
        {
            OleDbCommand command = new OleDbCommand(@"Select 
                        InterventionIndicatorValues.ID,   
                        InterventionIndicatorValues.IndicatorId,
                        InterventionIndicatorValues.DynamicValue,
                        InterventionIndicators.DisplayName
                        FROM InterventionIndicatorValues INNER JOIN InterventionIndicators on InterventionIndicatorValues.IndicatorId = InterventionIndicators.ID
                        WHERE InterventionIndicatorValues.InterventionId = @InterventionId", connection);
            command.Parameters.Add(new OleDbParameter("@InterventionId", intv.Id));
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (!intv.IntvType.Indicators.ContainsKey(reader.GetValueOrDefault<string>("DisplayName")))
                        continue;
                    intv.IndicatorValues.Add(new IndicatorValue
                    {
                        Id = reader.GetValueOrDefault<int>("ID"),
                        IndicatorId = reader.GetValueOrDefault<int>("IndicatorId"),
                        DynamicValue = reader.GetValueOrDefault<string>("DynamicValue"),
                        Indicator = intv.IntvType.Indicators[reader.GetValueOrDefault<string>("DisplayName")]
                    });
                }
                reader.Close();
            }
        }

        public void Delete(DistributionMethod distributionMethod, int userId)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"UPDATE InterventionDistributionMethods SET IsDeleted=@IsDeleted,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);

                    command.Parameters.Add(new OleDbParameter("@IsDeleted", true));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", distributionMethod.Id));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void Delete(Medicine medicine, int userId)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"UPDATE Medicines SET IsDeleted=@IsDeleted,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);

                    command.Parameters.Add(new OleDbParameter("@IsDeleted", true));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", medicine.Id));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void Delete(Partner partner, int userId)
        {
            OleDbConnection connection = new OleDbConnection(DatabaseData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"UPDATE Partners SET IsDeleted=@IsDeleted,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);

                    command.Parameters.Add(new OleDbParameter("@IsDeleted", true));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    command.Parameters.Add(new OleDbParameter("@id", partner.Id));
                    command.ExecuteNonQuery();
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
