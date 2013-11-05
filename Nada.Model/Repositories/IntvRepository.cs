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
    public class IntvRepository
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
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
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
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select 
                        Interventions.ID, 
                        InterventionTypes.InterventionTypeName, 
                        Interventions.InterventionTypeId, 
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
                                TypeName = reader.GetValueOrDefault<string>("InterventionTypeName"),
                                TypeId = reader.GetValueOrDefault<int>("InterventionTypeId"),
                                AdminLevel = reader.GetValueOrDefault<string>("DisplayName"),
                                StartDate = reader.GetValueOrDefault<DateTime>("StartDate"),
                                EndDate = reader.GetValueOrDefault<DateTime>("EndDate"),
                                UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt"),
                                UpdatedBy = Util.GetAuditInfo(reader)

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

            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select InterventionTypes.ID, InterventionTypes.InterventionTypeName, DiseaseType
                        FROM InterventionTypes", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            intv.Add(new IntvType
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                IntvTypeName = TranslationLookup.GetValue(reader.GetValueOrDefault<string>("InterventionTypeName"),
                                    reader.GetValueOrDefault<string>("InterventionTypeName")),
                                DiseaseType = reader.GetValueOrDefault<string>("DiseaseType")
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

            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select InterventionTypes.InterventionTypeName, InterventionTypes.UpdatedAt,
                        aspnet_users.UserName, created.UserName as CreatedBy, InterventionTypes.CreatedAt, DiseaseType
                        FROM ((InterventionTypes INNER JOIN aspnet_Users on InterventionTypes.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN aspnet_Users created on InterventionTypes.CreatedById = created.UserId)
                        WHERE InterventionTypes.ID=@id", connection);
                    command.Parameters.Add(new OleDbParameter("@id", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            intv = new IntvType
                            {
                                Id = id,
                                IntvTypeName = reader.GetValueOrDefault<string>("InterventionTypeName"),
                                DiseaseType = reader.GetValueOrDefault<string>("DiseaseType"),
                                UpdatedBy = Util.GetAuditInfo(reader)
                            };
                        }
                        reader.Close();
                    }

                    command = new OleDbCommand(@"Select 
                        InterventionIndicators.ID,   
                        InterventionIndicators.DataTypeId,
                        InterventionIndicators.DisplayName,
                        InterventionIndicators.IsRequired,
                        InterventionIndicators.IsDisabled,
                        InterventionIndicators.IsEditable,
                        InterventionIndicators.IsDisplayed,
                        InterventionIndicators.UpdatedAt, 
                        aspnet_users.UserName,
                        IndicatorDataTypes.DataType
                        FROM ((InterventionIndicators INNER JOIN aspnet_users ON InterventionIndicators.UpdatedById = aspnet_users.UserId)
                        INNER JOIN IndicatorDataTypes ON InterventionIndicators.DataTypeId = IndicatorDataTypes.ID)
                        WHERE InterventionTypeId=@InterventionTypeId AND IsDisabled=0 ", connection);
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
                                UpdatedBy = reader.GetValueOrDefault<DateTime>("UpdatedAt").ToString("MM/dd/yyyy") + " by " +
                                    reader.GetValueOrDefault<string>("UserName"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                                IsRequired = reader.GetValueOrDefault<bool>("IsRequired"),
                                IsDisabled = reader.GetValueOrDefault<bool>("IsDisabled"),
                                IsEditable = reader.GetValueOrDefault<bool>("IsEditable"),
                                IsDisplayed = reader.GetValueOrDefault<bool>("IsDisplayed"),
                                DataType = reader.GetValueOrDefault<string>("DataType")
                            });
                        }
                        reader.Close();
                    }

                    command = new OleDbCommand(@"Select
                        IndicatorId,
                        DropdownValue
                        FROM (InterventionIndicatorDropdownValues INNER JOIN InterventionIndicators ON 
                            InterventionIndicators.Id = InterventionIndicatorDropdownValues.IndicatorId)
                        WHERE InterventionTypeId=@InterventionTypeId AND IsDisabled=0
                        ORDER BY InterventionIndicatorDropdownValues.ID", connection);
                    command.Parameters.Add(new OleDbParameter("@InterventionTypeId", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            intv.IndicatorDropdownValues.Add(new KeyValuePair<int, string>(reader.GetValueOrDefault<int>("IndicatorId"), 
                                reader.GetValueOrDefault<string>("DropdownValue")));
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

        public IntvBase GetById(int id)
        {
            IntvBase intv = null;
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
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

        public PcMda GetPcMda(int id)
        {
            PcMda intv = null;
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = null;

                    intv = GetIntv<PcMda>(command, connection, id);

                    command = new OleDbCommand(@"Select Diseases.ID, Diseases.DisplayName
                        FROM Diseases INNER JOIN Interventions_to_Diseases on Diseases.ID = Interventions_to_Diseases.DiseaseId
                        WHERE Interventions_to_Diseases.InterventionId=@id", connection);
                    command.Parameters.Add(new OleDbParameter("@id", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            intv.DiseasesTargeted.Add(new Disease
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName")
                            });
                        }
                        reader.Close();
                    }

                    command = new OleDbCommand(@"Select Partners.ID, Partners.DisplayName
                        FROM Partners INNER JOIN Interventions_to_Partners on Partners.ID = Interventions_to_Partners.PartnerId
                        WHERE Interventions_to_Partners.InterventionId=@id", connection);
                    command.Parameters.Add(new OleDbParameter("@id", id));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            intv.Partners.Add(new Partner
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName")
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

        public void SaveBase(IntvBase intv, int userId)
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

        public void Save(PcMda intv, int userId)
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

                    SaveIntvBase(command, connection, intv, userId);
                    
                    // Save related lists
                    command = new OleDbCommand(@"DELETE FROM Interventions_to_Diseases WHERE InterventionId=@IntvId", connection);
                    command.Parameters.Add(new OleDbParameter("@IntvId", intv.Id));
                    command.ExecuteNonQuery();
                    foreach (var Disease in intv.DiseasesTargeted)
                    {
                        command = new OleDbCommand(@"INSERT INTO Interventions_to_Diseases (InterventionId, DiseaseId) values (@id, @DiseaseId)", connection);
                        command.Parameters.Add(new OleDbParameter("@id", intv.Id));
                        command.Parameters.Add(new OleDbParameter("@DiseaseId", Disease.Id));
                        command.ExecuteNonQuery();
                    }

                    command = new OleDbCommand(@"DELETE FROM Interventions_to_Partners WHERE InterventionId=@IntvId", connection);
                    command.Parameters.Add(new OleDbParameter("@IntvId", intv.Id));
                    command.ExecuteNonQuery();
                    foreach (var partner in intv.Partners)
                    {
                        command = new OleDbCommand(@"INSERT INTO Interventions_to_Partners (InterventionId, PartnerId) values (@id, @PartnerId)", connection);
                        command.Parameters.Add(new OleDbParameter("@id", intv.Id));
                        command.Parameters.Add(new OleDbParameter("@PartnerId", partner.Id));
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

        public void Save(IntvType model, int userId)
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

        #region Related Objects
        public List<DistributionMethod> GetDistributionMethods()
        {
            List<DistributionMethod> d = new List<DistributionMethod>();

            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName from InterventionDistributionMethods
                         WHERE IsDeleted=0", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            d.Add(new DistributionMethod
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName")
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
            return d;
        }

        public void Save(DistributionMethod dm, int userId)
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

                    if (dm.Id > 0)
                        command = new OleDbCommand(@"UPDATE InterventionDistributionMethods SET DisplayName=@DisplayName,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO InterventionDistributionMethods (DisplayName, UpdatedById, 
                            UpdatedAt, CreatedById, CreatedAt) values (@DisplayName, @UpdatedById, @UpdatedAt, @CreatedById, @CreatedAt)", connection); 
                    
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@DisplayName", dm.DisplayName));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (dm.Id > 0)
                        command.Parameters.Add(new OleDbParameter("@id", dm.Id));
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

        public List<Partner> GetPartners()
        {
            List<Partner> list = new List<Partner>();

            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
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
                                UpdatedBy = Util.GetAuditInfo(reader)
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

        public List<Medicine> GetMedicines()
        {
            List<Medicine> list = new List<Medicine>();
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName, Medicines.UserName, UpdatedAt, CreatedAt, c.UserName as CreatedBy
                        FROM ((Medicines INNER JOIN aspnet_users on Medicines.UpdatedById = aspnet_users.userid)
                         INNER JOIN aspnet_users c on Medicines.CreatedById = c.userid) WHERE IsDeleted=0", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Medicine
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                DisplayName = reader.GetValueOrDefault<string>("DisplayName"),
                                UpdatedBy = reader.GetValueOrDefault<string>("UserName") + " on " + reader.GetValueOrDefault<DateTime>("UpdatedAt").ToString("MM/dd/yyyy")
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

        public void Save(Medicine med, int userId)
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

                    if (med.Id > 0)
                        command = new OleDbCommand(@"UPDATE Medicines SET DisplayName=@DisplayName,
                           UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
                    else
                        command = new OleDbCommand(@"INSERT INTO Medicines (DisplayName, UpdatedById, 
                            UpdatedAt, CreatedByID, CreatedAt) values (@DisplayName, @UpdatedById, 
                            @UpdatedAt, @CreatedByID, @CreatedAt)", connection);

                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@DisplayName", med.DisplayName));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (med.Id > 0)
                        command.Parameters.Add(new OleDbParameter("@id", med.Id));
                    else
                    {
                        command.Parameters.Add(new OleDbParameter("@CreatedByID", userId));
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
                command = new OleDbCommand(@"Select Interventions.AdminLevelId, Interventions.StartDate, Interventions.EndDate, 
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
                        intv.StartDate = reader.GetValueOrDefault<DateTime>("StartDate");
                        intv.EndDate = reader.GetValueOrDefault<DateTime>("EndDate");
                        intv.Notes = reader.GetValueOrDefault<string>("Notes");
                        intv.UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt");
                        intv.UpdatedBy = Util.GetAuditInfo(reader);
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
                command = new OleDbCommand(@"UPDATE Interventions SET InterventionTypeId=@InterventionTypeId, AdminLevelId=@AdminLevelId, StartDate=@StartDate,
                           EndDate=@EndDate, Notes=@Notes, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
            else
                command = new OleDbCommand(@"INSERT INTO Interventions (InterventionTypeId, AdminLevelId, StartDate, EndDate, Notes, 
                            UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
                            values (@InterventionTypeId, @AdminLevelId, @StartDate, @EndDate, @Notes, @UpdatedById, @UpdatedAt,
                            @CreatedById, @CreatedAt)", connection); 
            command.Parameters.Add(new OleDbParameter("@InterventionTypeId", intv.IntvType.Id));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@AdminLevelId", intv.AdminLevelId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@StartDate", intv.StartDate));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@EndDate", intv.EndDate));
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
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
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
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
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
            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
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
