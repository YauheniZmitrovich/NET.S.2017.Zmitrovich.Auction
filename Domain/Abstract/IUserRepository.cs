using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }


        bool Create(User user);

        bool Update(User user);

        bool Remove(long id);


        User GetUserByEmail(string email);

        User GetUserByName(string userName);
    }
}   
