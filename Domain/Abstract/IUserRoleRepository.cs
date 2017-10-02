using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IUserRoleRepository
    {
        IQueryable<UserRole> UserRoles { get; }


        bool Create(UserRole role);

        bool Remove(long id);


        UserRole GetById(int roleId);
    }
}
