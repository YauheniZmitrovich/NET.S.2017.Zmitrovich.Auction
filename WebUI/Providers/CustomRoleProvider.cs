using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Domain.Abstract;
using Domain.Entities;

namespace WebUI.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        #region Repositories

        public IUserRoleRepository UserRoleRepository { get; } =
            (IUserRoleRepository)System.Web.Mvc.DependencyResolver
            .Current.GetService(typeof(IUserRoleRepository));

        public IUserRepository UserRepository { get; } =
            (IUserRepository)System.Web.Mvc.DependencyResolver
            .Current.GetService(typeof(IUserRepository));

        #endregion


        #region RoleProvider implementation

        public override bool IsUserInRole(string userName, string roleName)
        {
            var user = UserRepository.GetUserByName(userName);

            if (user == null)
                return false;

            var userRole = UserRoleRepository.UserRoles.FirstOrDefault(r => r.Name == roleName);

            return user.RoleId == userRole?.Id;
        }

        public override string[] GetRolesForUser(string userName)
        {
            var role = new string[] { };

            var user = UserRepository.GetUserByName(userName);

            if (user == null)
                return role;


            var userRole = UserRoleRepository.UserRoles.FirstOrDefault(r => r.Id == user.RoleId);

            if (userRole != null)
                role = new[] { userRole.Name };

            return role;
        }

        public override void CreateRole(string roleName)
        {
            UserRoleRepository.Create(new UserRole() { Name = roleName });
        }

        #endregion


        #region NotImplemented

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] userNames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] userNames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string userNameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }

        #endregion
    }
}