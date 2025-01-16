//PaymentService.cs
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class PaymentService
    {
        private readonly DbContext _context;

        public PaymentService(DbContext context)
        {
            _context = context;
        }

        public async Task<bool> ProcessPaymentAsync(string userInfoId, string slotId, string cardNumber, string expiryDate, string cvv)
        {
            // Validate payment details (e.g., card details and expiry date)
            if (string.IsNullOrWhiteSpace(cardNumber) || string.IsNullOrWhiteSpace(expiryDate) || string.IsNullOrWhiteSpace(cvv))
                return false;

            // Convert courseId to int
            if (!int.TryParse(slotId, out int courseIdInt))
                return false; // Invalid courseId format

            // Check if the course exists
            var slot = await _context.Set<Slot>().FindAsync(courseIdInt);
            if (slot == null)
                return false;

            // Create payment record
            var payment = new Payment
            {
                UserInfoId = userInfoId,
                SlotId = courseIdInt, // Use the converted int value
                PaymentStatus = "Success", // Assuming a successful payment
                CardNumber = cardNumber,
                ExpiryDate = DateTime.Parse(expiryDate), // Convert expiryDate to DateTime
                CVV = cvv
            };

            await _context.Set<Payment>().AddAsync(payment);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<List<Payment>> GetPaymentsForUserAsync(string userInfoId)
        {
            return await _context.Set<Payment>()
                .Where(p => p.UserInfoId == userInfoId)
                .ToListAsync();
        }
    }
}