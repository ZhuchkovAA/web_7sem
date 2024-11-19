using System.Linq;
using Zhuchkov_backend.Data;
using Zhuchkov_backend.Models;

namespace Zhuchkov_backend.Repositories {
    public class UserRepository
    {
        private readonly Zhuchkov_backendContext _context;

        public UserRepository(Zhuchkov_backendContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetUser(string id)
        {
            return _context.User.Where(u => u.IdTelegram == id);
        }

        public IQueryable<User> GetUsers()
        {
            return _context.User;
        }

        public IQueryable<User> GetActiveUsers()
        {
            return _context.User.Where(user => user.IsActive);
        }

        public IQueryable<User> GetUsersSubscribe()
        {
            return _context.User.Where(user => user.IsActive);
        }
    }
}

