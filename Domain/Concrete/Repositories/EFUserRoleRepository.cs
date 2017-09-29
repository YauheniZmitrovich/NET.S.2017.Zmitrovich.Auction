using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete.Repositories
{
    public class EFUserRoleRepository : IUserRoleRepository
    {
        private readonly EFDbContext _context = new EFDbContext();


        public IQueryable<UserRole> UserRoles => _context.UserRoles;


        public bool Create(UserRole ur)
        {
            _context.UserRoles.Add(ur);

            _context.SaveChanges();

            return true;
        }

        public bool Update(UserRole e)//TODO:Implement and correct the same in UserRepository
        {
            throw new NotImplementedException();
        }

        public bool Remove(long e)
        {
            UserRole role = _context.UserRoles.FirstOrDefault(u => u.Id == e);

            if (role == null)
                return false;


            _context.UserRoles.Remove(role);

            _context.SaveChanges();


            return true;
        }


        public UserRole GetById(int key) => _context.UserRoles.Find(key);
    }
}
