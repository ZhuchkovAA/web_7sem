using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Zhuchkov_backend.Models
{
    public class SubscribeRoom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string IdTelegram { get; set; }
        public DateTime Date { get; set; }
        public int IdRoom { get; set; }

        public virtual ICollection<TimeChunk> TimeChunks { get; set; } = new List<TimeChunk>();
    }
}
