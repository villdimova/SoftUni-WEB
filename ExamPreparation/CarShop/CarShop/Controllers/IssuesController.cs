using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Services;
using CarShop.ViewModels.Issues;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace CarShop.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IUserService userService;
        private readonly IValidator validator;
        private readonly CarshopDbContext carshopDbContext;


        public IssuesController(IUserService userService, CarshopDbContext carshopDbContext, IValidator validator)
        {
            this.userService = userService;
            this.carshopDbContext = carshopDbContext;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse CarIssues(string carId)
        {
            if (!this.userService.IsMechanic(this.User.Id))
            {
                var ownCar = this.carshopDbContext.Cars
                     .Where(c => c.Id == carId && c.OwnerId == this.User.Id);

                if (ownCar==null)
                {
                    return Error("No access to this car info!");
                }
            }

            var carForRepair = this.carshopDbContext.Cars
                .Where(c => c.Id == carId)
                .Select(i => new CarIssuesViewModel
                {
                    Id=i.Id,
                    Year=i.Year,
                    Model=i.Model,
                    Issues= i.Issues.Select(x=>new IssueViewModel
                    {
                        Id=x.Id,
                        Description=x.Description,
                        IsFixed=x.IsFixed,
                    })
                }).FirstOrDefault();

            if (carForRepair==null)
            {
                return Error("No car with this Id.");
            }
            return View(carForRepair);
        }

        [Authorize]
        public HttpResponse Add(string carId)
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(string carId, AddIssueViewModel input)
        {
            var errors = this.validator.ValidateIssue(input);

            if (errors.Any())
            {
                return View("./Error", errors);
            }

            if (!this.userService.IsMechanic(this.User.Id))
            {
                return Unauthorized();
            }
            if (userService.IsCarOwner(carId,this.User.Id))
            {
                return Unauthorized();
            }
            var issue = new Issue
            {
                CarId = carId,
                IsFixed = false,
                Description = input.Description,
            };

            this.carshopDbContext.Issues.Add(issue);
            this.carshopDbContext.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }

        [Authorize]
        public HttpResponse Fix(string issueId,string carId)
        {
            if (!this.userService.IsMechanic(this.User.Id))
            {
                return Unauthorized();
            }

            var issue = this.carshopDbContext.Issues.Where(i => i.Id == issueId).FirstOrDefault();

            if (issue==null)
            {
                return BadRequest();
            }

            issue.IsFixed = true;
            carshopDbContext.SaveChanges();

            return this.Redirect($"CarIssues?CarId={carId}");
        }

        [Authorize]
        public HttpResponse Delete(string issueId, string carId)
        {
            var issue = this.carshopDbContext.Issues.Where(i => i.Id == issueId).FirstOrDefault();

            if (issue == null)
            {
                return BadRequest();
            }

            carshopDbContext.Issues.Remove(issue);
            carshopDbContext.SaveChanges();

            return this.Redirect($"CarIssues?CarId={carId}");
        }
    }
}
