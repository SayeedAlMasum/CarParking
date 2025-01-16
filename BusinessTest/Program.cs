using Business;
using Business.FormModel;
using Business.Services;
using Database.Model;
using Microsoft.AspNetCore.Identity;

namespace BusinessTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RegistrationTest();
            //LoginTest();
            //UserListTest();
        }

        static void RegistrationTest()
        {
            UserForm userForm = new UserForm();
            userForm.FullName = Console.ReadLine();
            userForm.Email = Console.ReadLine();
            userForm.Password = Console.ReadLine();
            Result result = new UserInfoService().Registration(userForm);
            Console.WriteLine(result.Message);
        }
        static void LoginTest()
        {
            UserLoginForm loginForm = new UserLoginForm();
            Console.WriteLine("Email");
            loginForm.Email = Console.ReadLine();
            Console.WriteLine("Password");
            loginForm.Password = Console.ReadLine();
            Result result = new UserInfoService().Login(loginForm.Email, loginForm.Password);
            Console.WriteLine(result.Message);
        }
        static void UserListTest()
        {
            Result result = new UserInfoService().List();

        }
        static void UserTest()
        {
            Result result = new UserInfoService().Single("UserId");

        }
    }
}
