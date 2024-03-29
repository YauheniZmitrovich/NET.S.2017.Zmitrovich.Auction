﻿using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete.Repositories
{
    public class EFUserRepository : IUserRepository
    {
        private readonly EFDbContext _context;

        public EFUserRepository(EFDbContext context)
        {
            _context = context;
        }

        public IQueryable<User> Users => _context.Users;


        public bool Create(User user)
        {
            _context.Users.Add(user);

            _context.SaveChanges();

            return true;
        }

        public bool Update(User user)
        {
            if (user == null)
                return false;

            var userToUpdate = _context.Users.FirstOrDefault(u => u.Id == user.Id);

            _context.Users.Attach(userToUpdate);//?//TODO:??????

            userToUpdate.RoleId = user.RoleId;

            userToUpdate.Email = user.Email;

            userToUpdate.Password = user.Password;

            _context.Entry(userToUpdate).State = System.Data.Entity.EntityState.Modified;//?

            return true;
        }

        public bool Remove(long id)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return false;


            _context.Users.Remove(user);

            _context.SaveChanges();


            return true;
        }


        public User GetUserByEmail(string email) => (from u in _context.Users
                                                     where u.Email == email
                                                     select u).FirstOrDefault();

        public User GetUserByName(string name) => _context.Users.FirstOrDefault(u => u.Name == name);

        public long GetUserIdByEmail(string email) => (from u in _context.Users
                                                       where u.Email == email
                                                       select u.Id).FirstOrDefault();

    }
}
