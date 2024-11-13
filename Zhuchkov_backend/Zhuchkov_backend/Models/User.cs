using System.ComponentModel.DataAnnotations;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

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
        private byte[] passwordHash;
        private byte[] salt;

        private readonly string[] Admins = { "ZhuchkovAA" };

        public bool IsAdmin => Admins.Contains(TagTelegram);
        public bool IsSuperAdmin => TagTelegram == "ZhuchkovAA";

        public void SetPassword(string password)
        {
            using var rng = new RNGCryptoServiceProvider();
            salt = new byte[16];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            passwordHash = pbkdf2.GetBytes(32);
        }

        public bool CheckPassword(string password)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            var computedHash = pbkdf2.GetBytes(32);

            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != computedHash[i])
                    return false;
            }

            return true;
        }
    }
}
