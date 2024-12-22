using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Kosov_backend.Data;
using Kosov_backend.Models;

namespace Kosov_backend.Managers 
{
    public class UsersManager
    {
        private readonly Kosov_backendContext _context;

        public UsersManager(Kosov_backendContext context)
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

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public void Remove(User user) {
            _context.Users.Remove(user);
        }

        public async Task<bool> HasUserAsync(string idTelegram)
        {
            return await _context.Users.AnyAsync(u => u.IdTelegram == idTelegram);
        }

    }
}

