using System.Linq;
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

        public User GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.IdTelegram == id);
        }

        public IQueryable<object> GetUsers()
        {
            return _context.Users
                .Select(user => new
                {
                    user.IdTelegram,
                    user.TagTelegram,
                    user.FirstName,
                    user.LastName,
                    user.IsActive,
                    user.IsAdmin,
                    user.IsSuperAdmin
                });
        }

        public IQueryable<User> GetActiveUsers()
        {
            return _context.Users.Where(user => user.IsActive);
        }

        public bool checkActiveUser(User user)
        {
            if (user == null) return false;
            return user.IsActive;
        }

        
    }
}

