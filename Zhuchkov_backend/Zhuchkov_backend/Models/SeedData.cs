using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zhuchkov_backend.Data;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Zhuchkov_backend.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Zhuchkov_backendContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<Zhuchkov_backendContext>>()))
            {
                
                if (!context.User.Any())
                {
                    context.User.AddRange(
                        new User
                        {
                            IdTelegram = "394248224",
                            TagTelegram = "ZhuchkovAA",
                            FirstName = "Алексей",
                            LastName = "Жучков",
                            IsActive = true,
                            IdStateTelegram = 0
                        }
                    );
                }

                if (!context.TimeChunk.Any())
                {
                    context.TimeChunk.AddRange(
                        new() { Id = 0, Time = "8:30-9:15" },
                        new() { Id = 1, Time = "9:15-10:00" },
                        new() { Id = 2, Time = "10:15-11:00" },
                        new() { Id = 3, Time = "11:00-11:45" },
                        new() { Id = 4, Time = "12:00-12:45" },
                        new() { Id = 5, Time = "12:45-13:30" },
                        new() { Id = 6, Time = "14:00-14:45" },
                        new() { Id = 7, Time = "14:45-15:30" },
                        new() { Id = 8, Time = "15:45-16:30" },
                        new() { Id = 9, Time = "16:30-17:15" },
                        new() { Id = 10, Time = "17:20-18:05" },
                        new() { Id = 11, Time = "18:05-18:50" },
                        new() { Id = 12, Time = "18:55-19:40" },
                        new() { Id = 13, Time = "19:40-20:25" },
                        new() { Id = 14, Time = "20:30-21:15" },
                        new() { Id = 15, Time = "21:15-22:00" }
                    );
                }

                if (!context.StateTelegram.Any())
                {
                    context.StateTelegram.AddRange(
                        new() { Id = 0, Name = "Меню" },
                        new() { Id = 1, Name = "Поиск свобоной аудитории" },
                        new() { Id = 2, Name = "Профиль" },
                        new() { Id = 3, Name = "Мои пересдачи" }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}