using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages
{
    // Represents the Razor Page Model for user registration
    public class RegisterModel : PageModel
    {
        // Binds the Full Name input field from the form to this property
        // Validation: Full Name is required
        [BindProperty]
        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; }

        // Binds the Email input field from the form to this property
        // Validation: Email is required and must follow a valid email format
        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        // Binds the Password input field from the form to this property
        // Validation: Password is required and displayed as a password input in the form
        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Handles the POST request when the user submits the registration form
        public IActionResult OnPostRegister()
        {
            // Check if the model state (form inputs) is valid
            if (ModelState.IsValid)
            {
                // Add registration logic here (e.g., save user data to the database)

                // Redirect to the Login page after successful registration
                return RedirectToPage("/Login");
            }

            // If the model state is invalid, redisplay the registration page with validation errors
            return Page();
        }
    }
}