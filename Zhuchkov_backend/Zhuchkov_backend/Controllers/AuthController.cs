using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zhuchkov_backend.Data;
using Zhuchkov_backend.Models;

namespace Zhuchkov_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly Zhuchkov_backendContext _context;

        public AuthController(Zhuchkov_backendContext context)
        {
            _context = context;
        }

        public struct LoginData
        {
            public string IdTelegram { get; set; }
            public string Password { get; set; }
        }
        [HttpPost]
        public object GetToken ([FromBody] LoginData ld)
        {
            var hashedPassword = Models.User.GetHash(ld.Password);
            var user = _context.User.FirstOrDefault(u => u.IdTelegram == ld.IdTelegram && u.PasswordHash == hashedPassword);
            if (user == null)
            {
                Response.StatusCode = 401;
                return new { message = "wrong login/password" };
            }    
            return AuthOptions.GenerateToken(user.IdTelegram, user.IsAdmin, user.IsSuperAdmin);
        }
    }
}