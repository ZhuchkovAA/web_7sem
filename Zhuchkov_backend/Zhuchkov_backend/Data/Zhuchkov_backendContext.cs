using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Zhuchkov_backend.Models;

namespace Zhuchkov_backend.Data
{
    public class Zhuchkov_backendContext : DbContext
    {
        public Zhuchkov_backendContext(DbContextOptions<Zhuchkov_backendContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<TimeChunk> TimeChunks { get; set; }
        public DbSet<SubscribeRoom> SubscribeRooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SubscribeRoom>()
                .HasMany(sr => sr.TimeChunks)
                .WithMany(tc => tc.SubscribeRooms)
                .UsingEntity<Dictionary<string, object>>(
                    "SubscribeRoomTimeChunk",
                    join => join
                        .HasOne<TimeChunk>()
                        .WithMany()
                        .HasForeignKey("TimeChunkId"),
                    join => join
                        .HasOne<SubscribeRoom>()
                        .WithMany()
                        .HasForeignKey("SubscribeRoomId")
                );
        }
    }
}
