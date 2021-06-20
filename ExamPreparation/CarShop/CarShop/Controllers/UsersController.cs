using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Services;
using CarShop.ViewModels.Users;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Controllers
{
   public class UsersController: Controller
    {
        private readonly IValidator validator;
        private readonly CarshopDbContext carshopDbContext;
        private readonly IPasswordHasher passwordHasher;

        public UsersController(IValidator validator, CarshopDbContext carshopDbContext, IPasswordHasher passwordHasher)
        {
            this.validator = validator;
            this.carshopDbContext = carshopDbContext;
            this.passwordHasher = passwordHasher;
        }
        public HttpResponse Register()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserViewModel input)
        {
            var modelErrors = validator.ValidateUserRegistration(input);

            if (this.carshopDbContext.Users.Any(u=>u.Username==input.Username))
            {
                modelErrors.Add("There is already regitered user with this username.");
            }

            if (this.carshopDbContext.Users.Any(u => u.Email == input.Email))
            {
                modelErrors.Add("There is already regitered user with this email.");
            }
            if (modelErrors.Any())
            {
                return View("./Error", modelErrors);         
                    
            }
            var user = new User
            {
                Username = input.Username,
                Email = input.Email,
                Password = passwordHasher.HashPassword(input.Password),
                IsMechanic = input.UserType == "Mechanic",
            };

            carshopDbContext.Users.Add(user);
            carshopDbContext.SaveChanges();

            return Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserViewModel input)
        {
            var hashedPassword = this.passwordHasher.HashPassword(input.Password);
            var userId = carshopDbContext.Users
                .Where(u => u.Username == input.Username&& u.Password==hashedPassword)
                .Select(u=>u.Id)
                .FirstOrDefault();
            if (userId==null)
            {
                return Error("Wrong username or password!");
            }

            this.SignIn(userId);

            return Redirect("/Cars/All");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");

        }
    }
}
