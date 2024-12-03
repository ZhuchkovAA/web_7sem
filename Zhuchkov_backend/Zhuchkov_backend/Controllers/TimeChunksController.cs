using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Zhuchkov_backend.Models;
using Zhuchkov_backend.Data;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeChunkController : ControllerBase
    {
        private readonly Zhuchkov_backendContext _context;

        public TimeChunkController(Zhuchkov_backendContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeChunk>>> GetTimeChunks()
        {
            return await _context.TimeChunks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeChunk>> GetTimeChunk(int id)
        {
            var timeChunk = await _context.TimeChunks.FindAsync(id);

            if (timeChunk == null)
            {
                return NotFound();
            }

            return timeChunk;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TimeChunk>> CreateTimeChunk(TimeChunk timeChunk)
        {
            _context.TimeChunks.Add(timeChunk);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTimeChunk), new { id = timeChunk.Id }, timeChunk);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutTimeChunk(TimeChunk timeChunk)
        {
            _context.Entry(timeChunk).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTimeChunk(int id)
        {
            var timeChunk = await _context.TimeChunks.FindAsync(id);
            if (timeChunk == null)
            {
                return NotFound();
            }

            _context.TimeChunks.Remove(timeChunk);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TimeChunkExists(int id)
        {
            return _context.TimeChunks.Any(e => e.Id == id);
        }
    }
}