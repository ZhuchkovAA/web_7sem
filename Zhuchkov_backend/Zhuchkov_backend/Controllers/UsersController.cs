using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zhuchkov_backend.Data;
using Zhuchkov_backend.Models;
using Zhuchkov_backend.Repositories;

namespace Zhuchkov_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Zhuchkov_backendContext _context;
        private readonly UserRepository _userRepository;

        public UsersController(Zhuchkov_backendContext context)
        {
            _context = context;
            _userRepository = new UserRepository(context);
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
                return Ok(await _userRepository.GetUser(userIdTelegram).ToListAsync());

            if (id == null)
                return Ok(await _userRepository.GetUsers().ToListAsync());

            var user = await _userRepository.GetUser(id).FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { message = "Пользователь не найден." });

            return Ok(new List<User> { user });
        }

        [HttpGet("active")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetActiveUsers()
        {    
            return await _userRepository.GetActiveUsers().ToListAsync();
        }

        public class CreateUserRequest
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
                IdStateTelegram = 0,
                IsAdmin = false,
                DateInsert = DateTime.UtcNow
            };

            newUser.SetPassword(request.Password);

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Пользователь успешно добавлен", user = newUser });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var userResult = await FindUserAsync(id);
            if (userResult.Result is NotFoundObjectResult) return userResult.Result;

            var user = userResult.Value;

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Пользователь успешно удален" });
        }

        private async Task<bool> HasUserAsync(string idTelegram)
        {
            return await _context.User.AnyAsync(u => u.IdTelegram == idTelegram);
        }

        public class UpdateUserRequest
        {
            public string TagTelegram { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool IsActive { get; set; }
            public int IdStateTelegram { get; set; }
            public string Password { get; set; }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserRequest request)
        {
            var userResult = await FindUserAsync(id);
            if (userResult.Result is NotFoundObjectResult) return userResult.Result;

            var user = userResult.Value;
            user.TagTelegram = request.TagTelegram;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.IsActive = request.IsActive;
            user.IdStateTelegram = request.IdStateTelegram;

            if (!string.IsNullOrEmpty(request.Password))
            {
                user.SetPassword(request.Password);
            }

            _context.User.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Пользователь успешно обновлен", user });
        }

        private async Task<ActionResult<User>> FindUserAsync(string id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "Пользователь не найден" });
            }
            return user;
        }
    }
}
