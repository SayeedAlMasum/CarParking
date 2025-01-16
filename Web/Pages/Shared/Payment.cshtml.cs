using Database.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class PaymentModel : PageModel
    {
        [BindProperty]
        public Slot Slot { get; set; }

        [BindProperty]
        public string CardNumber { get; set; }

        [BindProperty]
        public string ExpiryDate { get; set; }

        [BindProperty]
        public string CVV { get; set; }

        public IActionResult OnGet(Slot slot)
        {
            Slot = slot;
            return Page();
        }

        public IActionResult OnPostPay()
        {
            if (ModelState.IsValid)
            {
                // Process payment (dummy logic for now)
                return RedirectToPage("/Slot");
            }

            return Page();
        }
    }
}