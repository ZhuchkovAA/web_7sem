using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zhuchkov_backend.Models;

namespace Zhuchkov_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public struct LoginData
        {
            public string TagTelegram { get; set; }
            public string Password { get; set; }
        }
        [HttpPost]
        public object GetToken ([FromBody] LoginData ld)
        {
            var user = SharedData.Users.FirstOrDefault(u => u.TagTelegram == ld.TagTelegram && u.CheckPassword(ld.Password));
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