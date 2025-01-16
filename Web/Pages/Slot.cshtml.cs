// Slot.cshtml.cs
using System.Collections.Generic;
using Business.Services;
using Database.Context;
using Database.Model;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class SlotModel : PageModel
    {
        public List<Slot> Slots { get; set; } = new List<Slot>();

        [BindProperty]
        public Slot Slot { get; set; } = new Slot();

        // OnGet method to load all Slots for displaying in the list
        public void OnGet()
        {
            var result = new SlotService().List();  // Get Slots from the service
            if (result.Success)
            {
                Slots = (List<Slot>)result.Data;
            }
        }

        // OnPost method for adding a new Slot
        // OnPost method for adding a new slot
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Set the slot creation date (optional)
                Slot.CreatedDate = DateTime.Now;

                // Save the slot to the database (both premium and non-premium)
                using (var context = new CarParkingContext())
                {
                    context.Slot.Add(Slot);  // Add slot to the database
                    context.SaveChanges();  // Save changes to the database
                }

                return RedirectToPage("/Slot");  // Redirect to the slot page after saving
            }

            // If model state is invalid, reload the page with the errors
            OnGet();
            return Page();
        }

    }
}