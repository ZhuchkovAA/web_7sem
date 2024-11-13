using System.ComponentModel.DataAnnotations;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Zhuchkov_backend.Models
{
    public class SubscribeRoom
    {
        public int Id { get; set; }
        public string IdTelegram { get; set; }
        public DateTime Date { get; set; }
        public int IdRoom { get; set; }
        public int IdTimeChunks { get; set; }
    }
}
