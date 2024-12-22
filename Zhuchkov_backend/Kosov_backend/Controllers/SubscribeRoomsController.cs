using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private readonly UsersManager _usersManager;
        private readonly SubscribeRoomsManager _subscribeRoomsManager;

        public SubscribeRoomsController(Zhuchkov_backendContext context)
        {
            _context = context;
            _timeChunksManager = new TimeChunksManager(context);
            _subscribeRoomsManager = new SubscribeRoomsManager(context);
            _usersManager = new UsersManager(context);
        }

        // GET: api/SubscribeRooms/{id?}
        [HttpGet("{idSub?}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<SubscribeRoom>>> GetSubscribeRooms(int? idSub = null)
        {
            var isAdmin = User.IsInRole("admin");
            var userIdTelegram = User.Claims.FirstOrDefault(c => c.Type == "IdTelegram")?.Value;

            if (string.IsNullOrEmpty(userIdTelegram))
                return Unauthorized(new { message = "Идентификатор пользователя отсутствует." });

            if (!isAdmin)
                return Ok(await _subscribeRoomsManager.GetSubsUser(userIdTelegram).ToListAsync());

            if (idSub == null)
               return Ok(await _subscribeRoomsManager.GetSubs().ToListAsync());

            var subscribeRoom = await _subscribeRoomsManager.FindAsync(idSub);
            if (subscribeRoom == null)
                return NotFound(new { message = "Запись не найдена" });

            return Ok(subscribeRoom);
        }

        public struct CreateSubscribeRoomRequest
        {
            [Required(ErrorMessage = "Поле Date обязательно для заполнения")]
            public DateTime Date { get; set; }

            [Required(ErrorMessage = "Поле IdRoom обязательно для заполнения")]
            [Range(1, int.MaxValue, ErrorMessage = "IdRoom должен быть больше 0")]
            public int IdRoom { get; set; }

            [Required(ErrorMessage = "Поле IdTimeChunks обязательно для заполнения")]
            public int[] IdTimeChunks { get; set; }
        }

        [HttpPost("create/{idTelegram?}")]
        [Authorize]
        public async Task<IActionResult> CreateSubscribeRoom([FromBody] CreateSubscribeRoomRequest request, string? idTelegram = null)
        {
            var isAdmin = User.IsInRole("admin");
            var userIdTelegram = User.Claims.FirstOrDefault(c => c.Type == "IdTelegram")?.Value;

            var idTelegramCreate = isAdmin ? idTelegram ?? userIdTelegram : userIdTelegram;
            var user = _usersManager.GetUser(idTelegramCreate);

            if (user == null)
                return NotFound(new { message = "Некорректный idTelegram" });

            if (!_usersManager.checkActiveUser(user))
                return NotFound(new { message = "User is not active" });

            if (!_timeChunksManager.CheckTimeChanks(request.IdTimeChunks))
                return NotFound(new { message = "Некорректный IdTimeChunks" });

            var timeChunks = await _timeChunksManager.GetTimeChunksRequest(request.IdTimeChunks);

            if (!timeChunks.Any())
                return NotFound(new { message = "Некорректные IdTimeChunks" });

            var newSubscribeRoom = new SubscribeRoom
            {
                IdTelegram = idTelegramCreate,
                Date = request.Date,
                IdRoom = request.IdRoom,
                TimeChunks = timeChunks
            };

            if (_subscribeRoomsManager.HasAlreadySub(newSubscribeRoom)) return BadRequest(new {message = "Room is already sub"});


            _context.SubscribeRooms.Add(newSubscribeRoom);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Запись успешно создана", subscribeRoom = newSubscribeRoom });
        }


        // DELETE: api/SubscribeRooms/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteSubscribeRoom(int id)
        {
            var subscribeRoom = await _context.SubscribeRooms.FindAsync(id);
            if (subscribeRoom == null)
                return NotFound(new { message = "Запись не найдена" });

            _context.SubscribeRooms.Remove(subscribeRoom);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Запись успешно удалена" });
        }

        public class UpdateSubscribeRoomRequest
        {
            public string? IdTelegram { get; set; }
            public DateTime? Date { get; set; }
            public int? IdRoom { get; set; }
            public int[]? IdTimeChunks { get; set; }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateSubscribeRoom(int id, [FromBody] UpdateSubscribeRoomRequest request)
        {
            var subscribeRoom = await _context.SubscribeRooms.FindAsync(id);
            if (subscribeRoom == null)
                return NotFound(new { message = "Запись не найдена" });

            if (request.IdTelegram != null)
                subscribeRoom.IdTelegram = request.IdTelegram;

            if (request.Date.HasValue)
                subscribeRoom.Date = request.Date.Value;

            if (request.IdRoom.HasValue)
                subscribeRoom.IdRoom = request.IdRoom.Value;

            if (request.IdTimeChunks != null)
            {
                if (!_timeChunksManager.CheckTimeChanks(request.IdTimeChunks))
                    return NotFound(new { message = "Некорректный IdTimeChunks" });
                
                var timeChunks = new List<TimeChunk> { };
                foreach (var timeChunkId in request.IdTimeChunks)
                {
                    var timeChunk = _context.TimeChunks.FirstOrDefault(tc => tc.Id == timeChunkId);
                    if (timeChunk != null)
                        timeChunks.Append(timeChunk);
                }
                subscribeRoom.TimeChunks = timeChunks;
            }

            _context.SubscribeRooms.Update(subscribeRoom);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Запись успешно обновлена", subscribeRoom });
        }
    }
}
