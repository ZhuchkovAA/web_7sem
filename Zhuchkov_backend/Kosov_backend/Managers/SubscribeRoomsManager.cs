using System.Linq;
using Kosov_backend.Data;
using Kosov_backend.Models;
using System.Threading.Tasks;

namespace Kosov_backend.Managers
{
    public class SubscribeRoomsManager
    {
        private readonly Kosov_backendContext _context;

        public SubscribeRoomsManager(Kosov_backendContext context)
        {
            _context = context;
        }

        public IQueryable<SubscribeRoom> GetSub(int id)
        {
            return _context.SubscribeRooms.Where(s => s.Id == id);
        }

        public Task<SubscribeRoom?> FindAsync(int? id)
        {
            if (!id.HasValue)
                return null;

            return _context.SubscribeRooms.FindAsync(id).AsTask();
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
