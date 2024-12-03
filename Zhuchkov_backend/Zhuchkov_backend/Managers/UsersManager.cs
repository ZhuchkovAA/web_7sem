﻿using System.Linq;
using Zhuchkov_backend.Data;
using Zhuchkov_backend.Models;

namespace Zhuchkov_backend.Managers 
{
    public class UsersManager
    {
        private readonly Zhuchkov_backendContext _context;

        public UsersManager(Zhuchkov_backendContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetUser(string id)
        {
            return _context.Users.Where(u => u.IdTelegram == id);
        }

        public IQueryable<User> GetUsers()
        {
            return _context.Users;
        }

        public IQueryable<User> GetActiveUsers()
        {
            return _context.Users.Where(user => user.IsActive);
        }
    }
}

