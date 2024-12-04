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
            return _context.SubscribeRooms.Where(s => s.Id == id);
        }

        public IQueryable<SubscribeRoom> GetSubs()
        {
            return _context.SubscribeRooms;
        }

        public IQueryable<SubscribeRoom> GetSubsUser(string idTelegram)
        {
            return _context.SubscribeRooms.Where(s => s.IdTelegram == idTelegram);
        }

        public bool HasAlreadySub(SubscribeRoom sub)
        {
            var timeChunks = sub.TimeChunks;
            var currentSubs = _context.SubscribeRooms.Where(s => (s.Date == sub.Date) && (s.IdRoom == sub.IdRoom)).ToList();
            foreach (var currentSub in currentSubs) {
                var commonTimeChunks = timeChunks.Intersect(currentSub.TimeChunks).ToArray();
                if (commonTimeChunks.Any()) return true;
            }
            return false;

        }
    }
}
