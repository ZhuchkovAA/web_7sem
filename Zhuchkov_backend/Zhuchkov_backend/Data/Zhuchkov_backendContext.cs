﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhuchkov_backend.Models;

namespace Zhuchkov_backend.Data
{
    public class Zhuchkov_backendContext : DbContext
    {
        public Zhuchkov_backendContext (DbContextOptions<Zhuchkov_backendContext> options)
            : base(options)
        {
        }

        public DbSet<Zhuchkov_backend.Models.User> User { get; set; } = default!;
    }
}