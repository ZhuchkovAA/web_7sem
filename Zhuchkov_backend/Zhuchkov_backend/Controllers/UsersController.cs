using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zhuchkov_backend.Data;
using Zhuchkov_backend.Models;
using Zhuchkov_backend.Managers;

namespace Zhuchkov_backend.Controllers
{
    public class UpdateUserRequest
    {
        public string? TagTelegram { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsActive { get; set; }
        public string? Password { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Zhuchkov_backendContext _context;
        private readonly UsersManager _usersManager;

        public UsersController(Zhuchkov_backendContext context)
        {
            _context = context;
            _usersManager = new UsersManager(context);
        }

        [HttpGet("{id?}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(string id = null)
        {
            var isAdmin = User.IsInRole("admin");
            var userIdTelegram = User.Claims.FirstOrDefault(c => c.Type == "IdTelegram")?.Value;

            if (string.IsNullOrEmpty(userIdTelegram))
                return Unauthorized(new { message = "Идентификатор пользователя отсутствует." });

            if (!isAdmin)
                return Ok(_usersManager.GetUser(userIdTelegram));

            if (id == null)
                return Ok(await _usersManager.GetUsers().ToListAsync());

            var user = _usersManager.GetUser(id);

            if (user == null)
                return NotFound(new { message = "Пользователь не найден." });

            return Ok(new List<User> { user });
        }

        [HttpGet("active")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetActiveUsers()
        {    
            return await _usersManager.GetActiveUsers().ToListAsync();
        }

        public struct CreateUserRequest
        {
            public string IdTelegram { get; set; }
            public string TagTelegram { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("create")]
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
                IsActive = true,
                IsAdmin = false,
                DateInsert = DateTime.UtcNow
            };

            newUser.SetPassword(request.Password);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Пользователь успешно добавлен", user = newUser });
        }

        [HttpPost("changeActive/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangeActiveUser(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdTelegram == id);
            
            if (user == null)
                return NotFound(new { message = "Пользователь с таким IdTelegram не найден." });

            user.IsActive = !user.IsActive;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Статус пользователя успешно обновлён", user });
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var userResult = await FindUserAsync(id);
            if (userResult.Result is NotFoundObjectResult) return userResult.Result;

            var user = userResult.Value;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Пользователь успешно удален" });
        }

        private async Task<bool> HasUserAsync(string idTelegram)
        {
            return await _context.Users.AnyAsync(u => u.IdTelegram == idTelegram);
        }

        public class UpdateUserRequest
        {
            public string? TagTelegram { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public bool? IsAdmin { get; set; }
            public bool? IsActive { get; set; }
            public string? Password { get; set; }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserRequest request)
        {
            var userResult = await FindUserAsync(id);
            if (userResult.Result is NotFoundObjectResult) return userResult.Result;

            var user = userResult.Value;

            if (request.TagTelegram != null)
                user.TagTelegram = request.TagTelegram;

            if (request.FirstName != null)
                user.FirstName = request.FirstName;

            if (request.LastName != null)
                user.LastName = request.LastName;

            if (request.IsAdmin.HasValue)
                user.IsAdmin = request.IsAdmin.Value;

            if (request.IsActive.HasValue)
                user.IsActive = request.IsActive.Value;

            if (!string.IsNullOrEmpty(request.Password))
            {
                user.SetPassword(request.Password);
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Пользователь успешно обновлен", user });
        }

        private async Task<ActionResult<User>> FindUserAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "Пользователь не найден" });
            }
            return user;
        }
    }
}
