//LogIn.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    // Represents the Razor Page Model for user login
    public class LogInModel : PageModel
    {
        // Binds the email input field from the form to this property
        [BindProperty]
        public string Email { get; set; }

        // Binds the password input field from the form to this property
        [BindProperty]
        public string Password { get; set; }

        // Handles GET requests to display the login page
        public IActionResult OnGet()
        {
            return Page(); // Returns the login page
        }

        // Handles POST requests to process login form submission
        public IActionResult OnPost()
        {
            // Check if the model state (form input) is valid
            if (!ModelState.IsValid)
            {
                return Page(); // Return the same page with validation errors
            }

            // Example: Replace this with actual login validation logic
            if (Email == "admin@example.com" && Password == "password123")
            {
                // If login is successful, redirect to a protected page (e.g., Dashboard)
                return RedirectToPage("/Dashboard");
            }

            // If login fails, add a model error and redisplay the login page
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return Page();
        }
    }
}