//Payment.cs
using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Payment
    {
        [Key]
        public string PaymentId { get; set; }= Guid.NewGuid().ToString();
        [Required]
        public int SlotId { get; set; }

        [Required]
        public string Amount { get; set; }
        [Required]
        public string UserInfoId { get; set; }

        [Required]
        public string PaymentStatus { get; set; } = "Pending";

        public string? CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string? CVV { get; set; }
    }
}