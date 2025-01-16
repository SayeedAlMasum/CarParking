using Business.FormModel;
using Database.Context;
using Database.Model;
using Microsoft.AspNetCore.Identity;

namespace Business.Services
{
    public class UserInfoService
    {

        public Result Registration(UserForm user)
        {
            // Initialize the database context
            CarParkingContext carParkingContext = new CarParkingContext();

            // Check if the email is already registered
            bool x = carParkingContext.UserInfo.Any(x => x.Email == user.Email);
            if (x) return new Result(false, "Email already registered!");

            // Create a new user object and populate its properties
                UserInfo userInfo = new UserInfo();
                userInfo.FullName = user.FullName;
                userInfo.Email = user.Email;

                // Hash the user's password for security
                userInfo.PasswordHash = new PasswordHasher<object>().HashPassword(user, user.Password);

                // Assign a default role ID if none is provided
                userInfo.RoleId = user.RoleId == 0 ? 3 : user.RoleId;

                // Mark the user as active
                userInfo.IsActive = true;

                // Add the new user to the database
                carParkingContext.UserInfo.Add(userInfo);
                try
                {
                    // Save changes to the database
                    carParkingContext.SaveChanges();
                    return new Result(true, "Registered Successfully!", user);
                }
                catch (Exception ex)
                {
                    // Return an error if something goes wrong during database save
                    return new Result(false, ex.Message);
                }
            }

        // Handles user login
        public Result Login(string email, string password)
        {
            // Initialize the database context
            using var context = new CarParkingContext();

            // Find the user by email
            var user = context.UserInfo.FirstOrDefault(u => u.Email == email);
            if (user == null) return new Result(false, "User not found!");

            // Verify the hashed password
            var passwordHasher = new PasswordHasher<object>();
            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            // Return success or failure based on password verification
            return verificationResult == PasswordVerificationResult.Success
                ? new Result(true, "Login successful!", user)
                : new Result(false, "Invalid password!");
        }
        public Result List()
        {
            try
            {
                using var context = new CarParkingContext();
                var users = context.UserInfo.ToList();

                if (users.Count == 0)
                    return new Result(false, "No users found.");

                return new Result(true, "User list retrieved successfully.", users);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public Result Single(string userInfoId)
        {
            try
            {
                using var context = new CarParkingContext();
                var user = context.UserInfo.FirstOrDefault(u => u.UserInfoId == userInfoId);

                if (user == null)
                    return new Result(false, "User not found.");

                return new Result(true, "User retrieved successfully.", user);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

    }
}