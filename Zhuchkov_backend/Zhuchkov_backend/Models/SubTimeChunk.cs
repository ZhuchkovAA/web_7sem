using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhuchkov_backend.Models
{
    public class SubTimeChunk
    {
        [Key]
        public int Id { get; set; }
        public int IdSub { get; set; }
        public int IdTimeChunk { get; set; }
    }
}
