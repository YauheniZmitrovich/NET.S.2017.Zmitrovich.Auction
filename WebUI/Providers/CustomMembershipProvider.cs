using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using Domain.Abstract;
using Domain.Concrete.Repositories;
using Domain.Entities;

namespace WebUI.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        #region Repositories

        public IUserRepository UserRepository { get; } =
            (IUserRepository)System.Web.Mvc.DependencyResolver
            .Current.GetService(typeof(IUserRepository));

        public IUserRoleRepository UserRoleRepository { get; } =
            (IUserRoleRepository)System.Web.Mvc.DependencyResolver
            .Current.GetService(typeof(IUserRoleRepository));

        #endregion


        #region Implementations

        public override bool ValidateUser(string username, string password)
        {
            var user = UserRepository.GetUserByName(username);

            return user != null && Crypto.VerifyHashedPassword(user.Password, password);
        }

        public MembershipUser CreateUser(string name, string password, string email)
        {
            UserProfile userProfile = new UserProfile();

            User user = new User()
            {
                Profile = userProfile,
                Name = name,
                Email = email,
                Password = Crypto.HashPassword(password),
                RoleId = UserRoleRepository.UserRoles.FirstOrDefault(r => r.Name == "User").Id
            };

            UserRepository.Create(user);

            return GetUser(name, true);
        }

        public override MembershipUser GetUser(string name, bool userIsOnline)
        {
            var user = UserRepository.GetUserByName(name);

            if (user == null)
                return null;

            var memberUser = new MembershipUser("CustomMembershipProvider",
                user.Name, null, null, null, null,
                false, false, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }

        #endregion


        #region NotImplemented

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }



        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval { get; }
        public override bool EnablePasswordReset { get; }
        public override bool RequiresQuestionAndAnswer { get; }
        public override string ApplicationName { get; set; }
        public override int MaxInvalidPasswordAttempts { get; }
        public override int PasswordAttemptWindow { get; }
        public override bool RequiresUniqueEmail { get; }
        public override MembershipPasswordFormat PasswordFormat { get; }
        public override int MinRequiredPasswordLength { get; }
        public override int MinRequiredNonAlphanumericCharacters { get; }
        public override string PasswordStrengthRegularExpression { get; }

        #endregion
    }
}