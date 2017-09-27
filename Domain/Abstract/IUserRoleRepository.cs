using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IUserRoleRepository//TODO:
    {
        IQueryable<UserRole> UserRoles { get; }


        bool Create(UserRole role);

        bool Update(UserRole role);

        bool Remove(long id);


        UserRole GetById(int roleId);
    }
}
