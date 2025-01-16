using Business.FormModel;
using Database.Context;
using Database.Model;
using Microsoft.AspNetCore.Identity;

namespace Business.Services
{
    public class UserInfoService
    {
        private readonly CarParkingContext _carParkingContext;

        public UserInfoService()
        {
            _carParkingContext = new CarParkingContext();
        }

        public Result Registration(UserForm user)
        {
            try
            {
                // Check if the email is already registered
                bool isEmailRegistered = _carParkingContext.UserInfo.Any(u => u.Email == user.Email);
                if (isEmailRegistered)
                    return new Result(false, "Email already registered!");

                // Map UserForm to UserInfo model
                var userInfo = new UserInfo
                {
                    FullName = user.FullName ?? throw new ArgumentNullException(nameof(user.FullName), "Full Name is required."),
                    Email = user.Email ?? throw new ArgumentNullException(nameof(user.Email), "Email is required."),
                    PasswordHash = new PasswordHasher<UserInfo>().HashPassword(null, user.Password ?? throw new ArgumentNullException(nameof(user.Password), "Password is required.")),
                    RoleId = user.RoleId == 0 ? 3 : user.RoleId, // Default to role 3 if RoleId is not provided
                    IsActive = true
                };

                // Add user to the database
                _carParkingContext.UserInfo.Add(userInfo);
                _carParkingContext.SaveChanges();

                return new Result(true, "Registered Successfully!", user);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public Result Login(string email, string password)
        {
            try
            {
                // Find the user by email
                var user = _carParkingContext.UserInfo.FirstOrDefault(u => u.Email == email);
                if (user == null)
                    return new Result(false, "User not found!");

                // Verify the hashed password
                var passwordHasher = new PasswordHasher<object>();
                var verificationResult = passwordHasher.VerifyHashedPassword(null, user.PasswordHash, password);

                return verificationResult == PasswordVerificationResult.Success
                    ? new Result(true, "Login successful!", user)
                    : new Result(false, "Invalid password!");
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public Result Update(UserForm user)
        {
            try
            {
                // Find the user by ID
                var existingUser = _carParkingContext.UserInfo.FirstOrDefault(u => u.UserInfoId == user.UserInfoId);
                if (existingUser == null)
                    return new Result(false, "User not found!");

                // Update user properties
                existingUser.FullName = user.FullName ?? existingUser.FullName;
                existingUser.Email = user.Email ?? existingUser.Email;
                existingUser.RoleId = user.RoleId != 0 ? user.RoleId : existingUser.RoleId;

                // Save changes
                _carParkingContext.SaveChanges();
                return new Result(true, "Updated Successfully!", existingUser);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public Result List()
        {
            try
            {
                // Retrieve all users
                var users = _carParkingContext.UserInfo.ToList();
                return users.Any()
                    ? new Result(true, "User list retrieved successfully.", users)
                    : new Result(false, "No users found.");
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
                // Find the user by ID
                var user = _carParkingContext.UserInfo.FirstOrDefault(u => u.UserInfoId == userInfoId);
                return user != null
                    ? new Result(true, "User retrieved successfully.", user)
                    : new Result(false, "User not found.");
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}
