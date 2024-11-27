using System.Collections.Generic;
using System;
using System.Linq;
using Zhuchkov_backend.Data;
using Zhuchkov_backend.Models;

namespace Zhuchkov_backend.Managers
{
    public class SubTimeChunkManager
    {
        private readonly Zhuchkov_backendContext _context;
        private readonly TimeChunksManager _timeChunksManager;

        public SubTimeChunkManager(Zhuchkov_backendContext context)
        {
            _context = context;
            _timeChunksManager = new TimeChunksManager(context);
        }

        public List<string> GetTimeSub(int id)
        {
            return _context.SubTimeChunk
                .Where(stc => stc.IdSub == id)
                .Join(_context.TimeChunk,
                    sub => sub.IdTimeChunk,
                    time => time.Id,
                    (sub, time) => time.Time)
                .ToList();
        }
    }
}
