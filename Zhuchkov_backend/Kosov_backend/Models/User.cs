using System.ComponentModel.DataAnnotations;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using NuGet.Protocol.Plugins;
using NuGet.Common;
using Newtonsoft.Json.Linq;

namespace Kosov_backend.Models
{
    public class User
    {
        [Key]
        public string IdTelegram { get; set; }

        public string TagTelegram { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateInsert { get; set; }

        public bool IsSuperAdmin => IdTelegram == "394248224"; // KosovAA

        public static string GetHash(string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            var sb = new StringBuilder();
            foreach (var b in MD5.Create().ComputeHash(textBytes))
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        public void SetPassword(string password)
        {
            PasswordHash = GetHash(password);
        }

        public bool CheckPassword(string password) => GetHash(password) == PasswordHash;
    }
}

