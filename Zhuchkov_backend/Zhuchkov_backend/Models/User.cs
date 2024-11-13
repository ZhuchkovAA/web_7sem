using System.ComponentModel.DataAnnotations;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using NuGet.Protocol.Plugins;
using NuGet.Common;
using Newtonsoft.Json.Linq;

namespace Zhuchkov_backend.Models
{
    public class User
    {
        [Key]
        public string IdTelegram { get; set; }

        public string TagTelegram { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public int IdStateTelegram { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateInsert { get; set; }
        public string passwordHash;

        private readonly string[] Admins = { "ZhuchkovAA" };

        public bool IsAdmin => Admins.Contains(TagTelegram);
        public bool IsSuperAdmin => TagTelegram == "ZhuchkovAA";

        private string GetHash(string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            var sb = new StringBuilder();
            foreach (var b in MD5.Create().ComputeHash(textBytes))
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        public void SetPassword(string password)
        {
            passwordHash = GetHash(password);
        }

        public bool CheckPassword(string password) => GetHash(password) == passwordHash;
    }
}

