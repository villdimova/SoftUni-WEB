namespace SharedTrip.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SharedTrip.Data;
    using SharedTrip.Data.Models;
    using SharedTrip.Services;
    using SharedTrip.ViewModels.Users;
    using System.Linq;

    public class UsersController : Controller
    {
        private readonly IValidator validator;
        private readonly SharedTripDbContext data;
        private readonly IPasswordHasher passwordHasher;

        public UsersController(IValidator validator, SharedTripDbContext data, IPasswordHasher passwordHasher = null)
        {
            this.validator = validator;
            this.data = data;
            this.passwordHasher = passwordHasher;
        }

        public HttpResponse Register()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserViewModel model)
        {
            var modelErrors = this.validator.ValidateUserRegistration(model);

            if (this.data.Users.Any(u => u.Username == model.Username))
            {
                modelErrors.Add($"User with '{model.Username}' username already exists.");
            }

            if (this.data.Users.Any(u => u.Email == model.Email))
            {
                modelErrors.Add($"User with '{model.Email}' e-mail already exists.");
            }

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var user = new User
            {
                Username = model.Username,
                Password = this.passwordHasher.HashPassword(model.Password),
                Email = model.Email,
            };

            data.Users.Add(user);

            data.SaveChanges();

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
            var userId = data.Users
                .Where(u => u.Username == input.Username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();
            if (userId == null)
            {
                return Error("Wrong username or password!");
            }

            this.SignIn(userId);

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");

        }
    }
}
