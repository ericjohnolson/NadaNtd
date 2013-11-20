using System;
using System.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nada.Model.Repositories;

namespace Nada.Tests
{
    [TestClass]
    public class CrudTests : BaseTest
    {
        [TestMethod]
        public void CanUpdate()
        {
            SettingsRepository repo = new SettingsRepository();
            var admins = repo.GetAllAdminLevels();
            var al = admins[0];
            al.DisplayName = "update";
            repo.Save(al, 26);
            var updated = repo.GetAllAdminLevels();
            Assert.AreEqual(al.DisplayName, updated[0].DisplayName);
        }

        [TestMethod]
        public void CanAddUsersAndRoles()
        {
            MembershipCreateStatus result;

            Membership.CreateUser("admin", "@ntd1one!", "admin@iotaink.com", "Hood", "Seattle", true, out result);

            Roles.CreateRole("Administrator");
            Roles.CreateRole("Developer");
            Roles.CreateRole("DataEnterer");
            Roles.AddUserToRole("admin", "Developer");
            Roles.AddUserToRole("admin", "Administrator");
            Roles.AddUserToRole("admin", "DataEnterer");

            Assert.IsTrue(Roles.IsUserInRole("admin", "developer"));
            Assert.IsTrue(Membership.ValidateUser("admin", "@ntd1one!"));
        }
    }
}
