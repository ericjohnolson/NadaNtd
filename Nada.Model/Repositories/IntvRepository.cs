using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Nada.DA;
using Nada.Model.Base;
using Nada.Model.Intervention;

namespace Nada.Model.Repositories
{
    public class IntvRepository
    {
        #region interventions
        public T CreateIntv<T>(StaticIntvType typeOfIntv) where T : IntvBase
        {
            var intv = (T)Activator.CreateInstance(typeof(T));
            IntvType t = GetIntvType((int)typeOfIntv);
            intv.IntvType = t;
            return intv;
        }

        public IntvBase CreateIntv(StaticIntvType typeOfIntv)
        {
            IntvBase Intv = new IntvBase();
            Intv.IntvType = GetIntvType((int)typeOfIntv);
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
                        aspnet_Users.UserName, AdminLevels.DisplayName
                        FROM (((Interventions INNER JOIN InterventionTypes on Interventions.InterventionTypeId = InterventionTypes.ID)
                            INNER JOIN aspnet_Users on Interventions.UpdatedById = aspnet_Users.UserId)
                            INNER JOIN AdminLevels on Interventions.AdminLevelId = AdminLevels.ID) 
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
            return interventions;
        }

        public List<IntvType> GetTypesByDisease(int diseaseId)
        {
            List<IntvType> intv = new List<IntvType>();

            OleDbConnection connection = new OleDbConnection(ModelData.Instance.AccessConnectionString);
            using (connection)
            {
                connection.Open();
                try
                {
                    OleDbCommand command = new OleDbCommand(@"Select InterventionTypes.ID, InterventionTypes.InterventionTypeName
                        FROM InterventionTypes", connection);
                    //command.Parameters.Add(new OleDbParameter("@DiseaseId", diseaseId));
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            intv.Add(new IntvType
                            {
                                Id = reader.GetValueOrDefault<int>("ID"),
                                IntvTypeName = reader.GetValueOrDefault<string>("InterventionTypeName")
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
                        aspnet_users.UserName
                        FROM (InterventionTypes INNER JOIN aspnet_Users on InterventionTypes.UpdatedById = aspnet_Users.UserId)
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
                                UpdatedBy = reader.GetValueOrDefault<string>("UserName") + " on " + reader.GetValueOrDefault<DateTime>("UpdatedAt").ToString("MM/dd/yyyy")
                            };
                        }
                        reader.Close();
                    }

                    command = new OleDbCommand(@"Select 
                        InterventionIndicators.ID,   
                        InterventionIndicators.DataTypeId,
                        InterventionIndicators.DisplayName,
                        InterventionIndicators.SortOrder,
                        InterventionIndicators.IsDisabled,
                        InterventionIndicators.IsEditable,
                        InterventionIndicators.UpdatedAt, 
                        aspnet_users.UserName,
                        IndicatorDataTypes.DataType
                        FROM ((InterventionIndicators INNER JOIN aspnet_users ON InterventionIndicators.UpdatedById = aspnet_users.UserId)
                        INNER JOIN IndicatorDataTypes ON InterventionIndicators.DataTypeId = IndicatorDataTypes.ID)
                        WHERE InterventionTypeId=@InterventionTypeId AND IsDisabled=0 
                        ORDER BY SortOrder", connection);
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
                                SortOrder = reader.GetValueOrDefault<int>("SortOrder"),
                                IsDisabled = reader.GetValueOrDefault<bool>("IsDisabled"),
                                IsEditable = reader.GetValueOrDefault<bool>("IsEditable"),
                                DataType = reader.GetValueOrDefault<string>("DataType")
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

        public PcMda GetPcMda(int id)
        {
            throw new NotImplementedException();
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
                    //command = new OleDbCommand(@"DELETE FROM Interventions_to_Medicines WHERE InterventionId=@IntvId", connection);
                    //command.Parameters.Add(new OleDbParameter("@IntvId", intv.Id));
                    //command.ExecuteNonQuery();
                    //foreach (var medicine in intv.Medicines)
                    //{
                    //    command = new OleDbCommand(@"INSERT INTO Interventions_to_Medicines (InterventionId, MedicineId) values (@id, @MedicineId)", connection);
                    //    command.Parameters.Add(new OleDbParameter("@id", intv.Id));
                    //    command.Parameters.Add(new OleDbParameter("@MedicineId", medicine.Id));
                    //    command.ExecuteNonQuery();
                    //}

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
                        command = new OleDbCommand(@"INSERT INTO InterventionTypes InterventionTypeName, UpdatedById, 
                            UpdatedAt) values (@InterventionTypeName, @UpdatedById, @UpdatedAt)", connection);
                    command.Parameters.Add(new OleDbParameter("@InterventionTypeName", model.IntvTypeName));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (model.Id > 0) command.Parameters.Add(new OleDbParameter("@id", model.Id));
                    command.ExecuteNonQuery();

                    if (model.Id <= 0)
                    {
                        command = new OleDbCommand(@"SELECT Max(ID) FROM InterventionTypes", connection);
                        model.Id = (int)command.ExecuteScalar();
                    }

                    foreach (var indicator in model.Indicators.Values.Where(i => i.Id > 0 && i.IsEdited))
                    {
                        command = new OleDbCommand(@"UPDATE InterventionIndicators SET InterventionTypeId=@InterventionTypeId, DataTypeId=@DataTypeId,
                        DisplayName=@DisplayName, SortOrder=@SortOrder, IsDisabled=@IsDisabled, 
                        IsEditable=@IsEditable, UpdatedById=@UpdateById, UpdatedAt=@UpdatedAt 
                        WHERE ID = @id", connection);
                        command.Parameters.Add(new OleDbParameter("@InterventionTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@DataTypeId", indicator.DataTypeId));
                        command.Parameters.Add(new OleDbParameter("@DisplayName", indicator.DisplayName));
                        command.Parameters.Add(new OleDbParameter("@SortOrder", indicator.SortOrder));
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
                        DisplayName, SortOrder, IsDisabled, IsEditable, UpdatedById, UpdatedAt) VALUES
                        (@InterventionTypeId, @DataTypeId, @DisplayName, @SortOrder, @IsDisabled, @IsEditable, @UpdatedById, 
                         @UpdatedAt)", connection);
                        command.Parameters.Add(new OleDbParameter("@InterventionTypeId", model.Id));
                        command.Parameters.Add(new OleDbParameter("@DataTypeId", indicator.DataTypeId));
                        command.Parameters.Add(new OleDbParameter("@DisplayName", indicator.DisplayName));
                        command.Parameters.Add(new OleDbParameter("@SortOrder", indicator.SortOrder));
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
                    OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName, UserName, UpdatedAt from InterventionDistributionMethods
                        INNER JOIN aspnet_users on InterventionDistributionMethods.UpdatedById = aspnet_users.userid WHERE IsDeleted=0", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            d.Add(new DistributionMethod
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
                            UpdatedAt) values (@DisplayName, @UpdatedById, @UpdatedAt)", connection); 
                    
                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@DisplayName", dm.DisplayName));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));               
                    if (dm.Id > 0) command.Parameters.Add(new OleDbParameter("@id", dm.Id));
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
                    OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName, UserName, UpdatedAt from Partners
                        INNER JOIN aspnet_users on Partners.UpdatedById = aspnet_users.userid WHERE IsDeleted=0", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Partner
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
                            UpdatedAt) values (@DisplayName, @UpdatedById, @UpdatedAt)", connection);

                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@DisplayName", partner.DisplayName));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (partner.Id > 0) command.Parameters.Add(new OleDbParameter("@id", partner.Id));
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
                    OleDbCommand command = new OleDbCommand(@"Select ID, DisplayName, UserName, UpdatedAt from Medicines
                        INNER JOIN aspnet_users on Medicines.UpdatedById = aspnet_users.userid WHERE IsDeleted=0", connection);
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
                            UpdatedAt) values (@DisplayName, @UpdatedById, @UpdatedAt)", connection);

                    command.Parameters.Add(OleDbUtil.CreateNullableParam("@DisplayName", med.DisplayName));
                    command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
                    command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
                    if (med.Id > 0) command.Parameters.Add(new OleDbParameter("@id", med.Id));
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
        private void SaveIntvBase(OleDbCommand command, OleDbConnection connection, IntvBase intv, int userId)
        {
            if (intv.Id > 0)
                command = new OleDbCommand(@"UPDATE Interventions SET InterventionTypeId=@InterventionTypeId, AdminLevelId=@AdminLevelId, StartDate=@StartDate,
                           EndDate=@EndDate, Notes=@Notes, UpdatedById=@UpdatedById, UpdatedAt=@UpdatedAt WHERE ID=@id", connection);
            else
                command = new OleDbCommand(@"INSERT INTO Interventions (InterventionTypeId, AdminLevelId, InterventionDate, Notes, UpdatedById, 
                            UpdatedAt) values (@InterventionTypeId, @AdminLevelId, @InterventionDate, @Notes, @UpdatedById, @UpdatedAt)", connection); 
            command.Parameters.Add(new OleDbParameter("@InterventionTypeId", intv.IntvType.Id));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@AdminLevelId", intv.AdminLevelId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@StartDate", intv.StartDate));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@EndDate", intv.EndDate));
            command.Parameters.Add(OleDbUtil.CreateNullableParam("@Notes", intv.Notes));
            command.Parameters.Add(new OleDbParameter("@UpdatedById", userId));
            command.Parameters.Add(OleDbUtil.CreateDateTimeOleDbParameter("@UpdatedAt", DateTime.Now));
            if (intv.Id > 0) command.Parameters.Add(new OleDbParameter("@id", intv.Id));
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
                command.Parameters.Add(new OleDbParameter("@DynamicValue", val.DynamicValue));
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
                        InterventionIndicatorValues.DynamicValue
                        FROM InterventionIndicatorValues
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
                        DynamicValue = reader.GetValueOrDefault<string>("DynamicValue")
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
