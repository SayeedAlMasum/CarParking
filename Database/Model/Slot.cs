using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Model
{
    public class Slot : BaseModel
    {
        [Key]
        public int SlotId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string Locality { get; set; }
        [Required]
        public float LatestPrice { get; set; }
        [Required]
        public bool IsBooked { get; set; }
        [Required]
        public float Duration { get; set; }
    }
}
