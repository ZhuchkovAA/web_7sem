﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Kosov_backend.Data;
using Kosov_backend.Models;

namespace Kosov_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly Kosov_backendContext _context;

        public AuthController(Kosov_backendContext context)
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
            var user = _context.Users.FirstOrDefault(u => u.IdTelegram == ld.IdTelegram && u.PasswordHash == hashedPassword);
            if (user == null)
            {
                Response.StatusCode = 401;
                return new { message = "wrong login/password" };
            }    
            return AuthOptions.GenerateToken(user.IdTelegram, user.IsAdmin, user.IsSuperAdmin);
        }
    }
}