using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zhuchkov_backend.Data;
using System;
using System.Linq;

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

                context.SaveChanges();
            }
        }
    }
}