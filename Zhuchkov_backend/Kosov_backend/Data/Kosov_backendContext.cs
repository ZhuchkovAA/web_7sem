using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Kosov_backend.Models;

namespace Kosov_backend.Data
{
    public class Kosov_backendContext : DbContext
    {
        public Kosov_backendContext(DbContextOptions<Kosov_backendContext> options)
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
