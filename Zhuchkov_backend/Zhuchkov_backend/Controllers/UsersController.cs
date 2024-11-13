﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zhuchkov_backend.Data;
using Zhuchkov_backend.Models;

namespace Zhuchkov_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("{id?}")]
        [Authorize(Roles = "admin")]
        public List<User> GetUsers(string id = null)
        {
            if (string.IsNullOrEmpty(id)) return SharedData.Users;

            var user = SharedData.Users.FirstOrDefault(u => u.IdTelegram == id);
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

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddUser([FromBody] CreateUserRequest request)
        {
            if (SharedData.Users.Exists(u => u.IdTelegram == request.IdTelegram))
            {
                return BadRequest("Пользователь с таким IdTelegram уже существует.");
            }

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

            SharedData.Users.Add(newUser);

            return Ok(new { message = "Пользователь успешно добавлен", user = newUser });
        }

       
    }
}