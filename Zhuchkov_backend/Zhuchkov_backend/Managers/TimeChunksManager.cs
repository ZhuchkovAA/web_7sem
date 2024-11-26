using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Zhuchkov_backend.Data;
using Zhuchkov_backend.Models;

namespace Zhuchkov_backend.Managers
{
    public class TimeChunksManager
    {
        private readonly Zhuchkov_backendContext _context;

        public TimeChunksManager(Zhuchkov_backendContext context)
        {
            _context = context;
        }

        public IQueryable<TimeChunk> GetTimeChunk(int id)
        {
            return _context.TimeChunk.Where(tc => tc.Id == id);
        }

        public IQueryable<TimeChunk> GetTimeChunks()
        {
            return _context.TimeChunk;
        }

        public bool CheckTimeChanks(int[] IdTimeChunks)
        {
            if (IdTimeChunks.Length == 0) return false;

            foreach (var timeChunkId in IdTimeChunks)
            {
                var timeChunkExists =  _context.TimeChunk.Any(tc => tc.Id == timeChunkId);
                if (!timeChunkExists)
                    return false;
            }
            return true;
        }
    }
}
