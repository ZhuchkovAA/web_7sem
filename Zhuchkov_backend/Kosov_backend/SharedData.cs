using System.Collections.Generic;
using Kosov_backend.Models;

namespace Kosov_backend
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
            var userUser = new User { TagTelegram = "KosovAA_u" };
            userUser.SetPassword("KosovAA_u");

            var userAdmin = new User { TagTelegram = "KosovAA" };
            userAdmin.SetPassword("KosovAA");

            Users = new List<User> { userUser, userAdmin };
        }
    }
}
