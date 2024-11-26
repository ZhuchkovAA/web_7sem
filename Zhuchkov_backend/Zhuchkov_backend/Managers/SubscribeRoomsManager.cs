using System.Linq;
using Zhuchkov_backend.Data;
using Zhuchkov_backend.Models;

namespace Zhuchkov_backend.Managers
{
    public class SubscribeRoomsManager
    {
        private readonly Zhuchkov_backendContext _context;

        public SubscribeRoomsManager(Zhuchkov_backendContext context)
        {
            _context = context;
        }

        public IQueryable<SubscribeRoom> GetSub(int id)
        {
            return _context.SubscribeRoom.Where(s => s.Id == id);
        }

        public IQueryable<SubscribeRoom> GetSubs()
        {
            return _context.SubscribeRoom;
        }

        public IQueryable<SubscribeRoom> GetSubsUser(string idTelegram)
        {
            return _context.SubscribeRoom.Where(s => s.IdTelegram == idTelegram);
        }
    }
}
