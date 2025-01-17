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
        // OnPost method for adding a new slot
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Set the slot creation date (optional)
                Slot.CreatedDate = DateTime.Now;

                // Save the slot to the database 
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
        // OnPost method for updating an existing slot
        public IActionResult OnPostUpdate()
        {
            if (ModelState.IsValid)
            {
                var result = new SlotService().UpdateSlot(Slot);
                if (result.Success)
                {
                    return RedirectToPage("/Slot"); // Redirect to the slot list after updating
                }

                // Handle update failure (e.g., display an error message)
                ModelState.AddModelError("", result.Message);
            }

            // If the model state is invalid, reload the page with errors
            OnGet();
            return Page();
        }

        // OnPost method for deleting a slot
        public IActionResult OnPostDelete(int id)
        {
            var result = new SlotService().DeleteSlot(id);
            if (result.Success)
            {
                return RedirectToPage("/Slot"); // Redirect to the course list after deletion
            }

            // Handle deletion failure (e.g., display an error message)
            ModelState.AddModelError("", result.Message);
            OnGet();
            return Page();
        }
    }
}