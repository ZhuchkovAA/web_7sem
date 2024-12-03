using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Zhuchkov_backend.Models
{
    public class TimeChunk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Time { get; set; }

        [JsonIgnore]
        public virtual ICollection<SubscribeRoom> SubscribeRooms { get; set; } = new List<SubscribeRoom>();
    }
}
