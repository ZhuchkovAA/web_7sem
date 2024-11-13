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
            public string TagTelegram { get; set; }
            public string Password { get; set; }
        }
        [HttpPost]
        public object GetToken ([FromBody] LoginData ld)
        {
            var user = _context.User.FirstOrDefault(u => u.TagTelegram == ld.TagTelegram);
            if (user==null)
            {
                Response.StatusCode = 401;
                return new { message = "wrong login/password" };
            }    
            return AuthOptions.GenerateToken(user.IsAdmin, user.IsSuperAdmin);
        }

        /*
        [HttpGet("token")]
        public object GetToken ()
        {
            return AuthOptions.GenerateToken();
        }
        [HttpGet("token/secret")]
        public object GetAdminToken ()
        {
            return AuthOptions.GenerateToken(true);
        }
        */
    }
}