using System.Collections.Generic;
using Zhuchkov_backend.Models;

namespace Zhuchkov_backend
{
    public static class SharedData
    {
        public static HashSet<string> Summaries { get; } = new HashSet<string>
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public static List<User> Users { get; }

        static SharedData()
        {
            var userUser = new User { TagTelegram = "ZhuchkovAA_u" };
            userUser.SetPassword("ZhuchkovAA_u");

            var userAdmin = new User { TagTelegram = "ZhuchkovAA" };
            userAdmin.SetPassword("ZhuchkovAA");

            Users = new List<User> { userUser, userAdmin };
        }
    }
}
