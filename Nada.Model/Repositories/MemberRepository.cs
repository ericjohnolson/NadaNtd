using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Nada.Globalization;
using Nada.Model.Base;

namespace Nada.Model.Repositories
{
    public class MemberRole
    {
        public string RoleName { get; set; }
        public string DisplayName { get; set; }
    }

    /// <summary>
    /// Performs database queries for Users
    /// </summary>
    public class MemberRepository
    {
        /// <summary>
        /// Backup username for admin requested by story 262
        /// </summary>
        private static string BackupUser = "NaDa";

        public bool Authenticate(string uid, string pwd)
        {
            if (Membership.ValidateUser(uid, pwd))
            {
                ApplicationData.Instance.CurrentUser = Membership.GetUser(uid);
                return true;
            }
            return false;
        }

        public List<Member> GetAllUsers()
        {
            List<Member> users = new List<Member>();
            foreach (MembershipUser user in Membership.GetAllUsers())
            {
                if (user.IsApproved && user.UserName != BackupUser)
                    users.Add(new Member
                        {
                            Id = 9999,
                            Username = user.UserName,
                            UpdatedBy = user.Comment,
                            Password = "x"
                        });
            }
            return users;
        }

        public List<MemberRole> GetAllRoles()
        {
            return Roles.GetAllRoles().OrderBy(s => s).Select(s => new MemberRole
            {
                RoleName = s,
                DisplayName = TranslationLookup.GetValue(s, s)
            }).ToList();
        }

        public void Save(Member model, int userid)
        {
            if (model.Id > 0)
            {
                MembershipUser user = Membership.GetUser(model.Username);
                user.Comment = Translations.LastUpdated + " " + DateTime.Now.ToShortDateString();
                Membership.UpdateUser(user);
                // update roles!
                var roles = Roles.GetRolesForUser(model.Username);
                if(roles.Count() > 0)
                    Roles.RemoveUserFromRoles(model.Username, roles);
                if(model.SelectedRoles.Count > 0)
                    Roles.AddUserToRoles(model.Username, model.SelectedRoles.Select(s => s.RoleName).ToArray());
            }
            else
            {
                Membership.CreateUser(model.Username, model.Password);
                MembershipUser user = Membership.GetUser(model.Username);
                user.Comment = Translations.LastUpdated + " " + DateTime.Now.ToShortDateString();
                Membership.UpdateUser(user);
                // add roles!
                if (model.SelectedRoles.Count > 0)
                    Roles.AddUserToRoles(model.Username, model.SelectedRoles.Select(s => s.RoleName).ToArray());
            }
        }

        public void ChangePassword(Member model)
        {
            MembershipUser user = Membership.GetUser(model.Username);
            string pwd = user.ResetPassword();
            user.ChangePassword(pwd, model.Password);
        }

        public void Delete(Member model)
        {
            MembershipUser user = Membership.GetUser(model.Username);
            user.IsApproved = false;
            Membership.UpdateUser(user);
        }
    }
}
