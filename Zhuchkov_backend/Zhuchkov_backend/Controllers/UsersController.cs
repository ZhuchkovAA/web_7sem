using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zhuchkov_backend.Data;
using Zhuchkov_backend.Models;

namespace Zhuchkov_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Zhuchkov_backendContext _context;

        public UsersController(Zhuchkov_backendContext context)
        {
            _context = context;
        }

        [HttpGet("{id?}")]
        // [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(string id = null)
        {
            if (string.IsNullOrEmpty(id))
                return await _context.User.ToListAsync();

            var user = await _context.User.FirstOrDefaultAsync(u => u.IdTelegram == id);
            return user != null ? new List<User> { user } : new List<User>();
        }

        public class CreateUserRequest
        {
            public string IdTelegram { get; set; }
            public string TagTelegram { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool IsActive { get; set; }
            public int IdStateTelegram { get; set; }
            public string Password { get; set; }
        }

            [HttpPost("create")]
        // [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (await HasUserAsync(request.IdTelegram))
                return BadRequest("Пользователь с таким IdTelegram уже существует.");

            var newUser = new User
            {
                IdTelegram = request.IdTelegram,
                TagTelegram = request.TagTelegram,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IsActive = request.IsActive,
                IdStateTelegram = request.IdStateTelegram,
                DateInsert = DateTime.UtcNow
            };

            newUser.SetPassword(request.Password);

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Пользователь успешно добавлен", user = newUser });
        }

        private async Task<bool> HasUserAsync(string idTelegram)
        {
            return await _context.User.AnyAsync(u => u.IdTelegram == idTelegram);
        }
    }
}
