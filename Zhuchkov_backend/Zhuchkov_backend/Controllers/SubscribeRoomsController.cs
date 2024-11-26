using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zhuchkov_backend.Data;
using Zhuchkov_backend.Managers;
using Zhuchkov_backend.Models;

namespace Zhuchkov_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeRoomsController : ControllerBase
    {
        private readonly Zhuchkov_backendContext _context;
        private readonly TimeChunksManager _timeChunksManager;

        public SubscribeRoomsController(Zhuchkov_backendContext context)
        {
            _context = context;
            _timeChunksManager = new TimeChunksManager(context);
        }

        // GET: api/SubscribeRooms/{id?}
        [HttpGet("{id?}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<SubscribeRoom>>> GetSubscribeRooms(string? id = null)
        {
            var isAdmin = User.IsInRole("admin");
            var userIdTelegram = User.Claims.FirstOrDefault(c => c.Type == "IdTelegram")?.Value;

            if (string.IsNullOrEmpty(userIdTelegram))
                return Unauthorized(new { message = "Идентификатор пользователя отсутствует." });

            if (!isAdmin)
                return await _context.SubscribeRoom
                   .Where(s => s.IdTelegram == userIdTelegram)
                   .ToListAsync();

            if (id == null)
               return await _context.SubscribeRoom.ToListAsync();

            var subscribeRoom = await _context.SubscribeRoom.FindAsync(id);
            if (subscribeRoom == null)
                return NotFound(new { message = "Запись не найдена" });

            return new List<SubscribeRoom> { subscribeRoom };
        }

        public class CreateSubscribeRoomRequest
        {
            public DateTime Date { get; set; }
            public int IdRoom { get; set; }
            public int[] IdTimeChunks { get; set; }
        }

        [HttpPost("create/{idTelegram?}")]
        [Authorize]
        public async Task<IActionResult> CreateSubscribeRoom([FromBody] CreateSubscribeRoomRequest request, string? idTelegram = null)
        {
            var isAdmin = User.IsInRole("admin");
            var userIdTelegram = User.Claims.FirstOrDefault(c => c.Type == "IdTelegram")?.Value;

            var idTelegramCreate = isAdmin ? idTelegram ?? userIdTelegram : userIdTelegram;
            var user = await _context.User.FindAsync(idTelegramCreate);

            if (user == null)
                return NotFound(new { message = "Некорректный idTelegram" });
            
            if (!_timeChunksManager.CheckTimeChanks(request.IdTimeChunks))
                return NotFound(new { message = "Некорректный IdTimeChunks" });

            var newSubscribeRoom = new SubscribeRoom
            {
                IdTelegram = idTelegramCreate,
                Date = request.Date,
                IdRoom = request.IdRoom
            };

            _context.SubscribeRoom.Add(newSubscribeRoom);
            await _context.SaveChangesAsync();

            foreach (var timeChunkId in request.IdTimeChunks)
            {
                var subTimeChunk = new SubTimeChunk
                {
                    IdSub = newSubscribeRoom.Id,
                    IdTimeChunk = timeChunkId
                };
                _context.SubTimeChunk.Add(subTimeChunk);
            }
            await _context.SaveChangesAsync();

            return Ok(new { message = "Запись успешно создана", subscribeRoom = newSubscribeRoom });
        }

        // DELETE: api/SubscribeRooms/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteSubscribeRoom(int id)
        {
            var subscribeRoom = await _context.SubscribeRoom.FindAsync(id);
            if (subscribeRoom == null)
                return NotFound(new { message = "Запись не найдена" });

            _context.SubscribeRoom.Remove(subscribeRoom);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Запись успешно удалена" });
        }

        public class UpdateSubscribeRoomRequest
        {
            public string IdTelegram { get; set; }
            public DateTime Date { get; set; }
            public int IdRoom { get; set; }
            public int IdTimeChunks { get; set; }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateSubscribeRoom(int id, [FromBody] UpdateSubscribeRoomRequest request)
        {
            var subscribeRoom = await _context.SubscribeRoom.FindAsync(id);
            if (subscribeRoom == null)
                return NotFound(new { message = "Запись не найдена" });

            subscribeRoom.IdTelegram = request.IdTelegram;
            subscribeRoom.Date = request.Date;
            subscribeRoom.IdRoom = request.IdRoom;

            _context.SubscribeRoom.Update(subscribeRoom);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Запись успешно обновлена", subscribeRoom });
        }
    }
}
