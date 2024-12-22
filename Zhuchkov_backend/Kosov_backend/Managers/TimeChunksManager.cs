using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Kosov_backend.Data;
using Kosov_backend.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Kosov_backend.Managers
{
    public class TimeChunksManager
    {
        private readonly Kosov_backendContext _context;

        public TimeChunksManager(Kosov_backendContext context)
        {
            _context = context;
        }

        public TimeChunk GetTimeChunk(int id)
        {
            return _context.TimeChunks.FirstOrDefault(tc => tc.Id == id);
        }

        public IQueryable<TimeChunk> GetTimeChunks()
        {
            return _context.TimeChunks;
        }

        public Task<List<TimeChunk>> GetTimeChunksRequest(int[] IdTimeChunks)
        {
            return _context.TimeChunks
                .Where(tc => IdTimeChunks.Contains(tc.Id))
                .ToListAsync();
        }

        public bool CheckTimeChanks(int[] IdTimeChunks)
        {
            if (IdTimeChunks == null || IdTimeChunks.Length == 0) return false;

            foreach (var timeChunkId in IdTimeChunks)
            {
                var timeChunkExists =  _context.TimeChunks.Any(tc => tc.Id == timeChunkId);
                if (!timeChunkExists)
                    return false;
            }
            return true;
        }
    }
}
