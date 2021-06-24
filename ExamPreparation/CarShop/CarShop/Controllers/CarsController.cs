namespace CarShop.Controllers
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.Services;
    using CarShop.ViewModels.Cars;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Collections.Generic;
    using System.Linq;

    public class CarsController : Controller
    {
        private readonly IValidator validator;
        private readonly CarshopDbContext carshopDbContext;
        private readonly IUserService userService;

        public CarsController(IValidator validator, CarshopDbContext carshopDbContext, IUserService userService)
        {
            this.validator = validator;
            this.carshopDbContext = carshopDbContext;
            this.userService = userService;
        }

        [Authorize]
        public HttpResponse Add()
        {
            if (this.userService.IsMechanic(this.User.Id))
            {
                return Unauthorized();
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddCarViewModel input)
        {

            if (this.userService.IsMechanic(this.User.Id))
            {
                return Unauthorized();
            }
            var errors = validator.ValidateCar(input);

            if (errors.Any())
            {
                return View("./Error", errors);
            }

            var car = new Car
            {
                Model = input.Model,
                Year = input.Year,
                PictureUrl = input.Image,
                PlateNumber = input.PlateNumber,
                OwnerId = this.User.Id,
            };

            carshopDbContext.Cars.Add(car);
            carshopDbContext.SaveChanges();

            return Redirect("/Cars/All");
        }

        [Authorize]
        public HttpResponse All()
        {
            List<AllCarsViewModel> cars;

            if (this.userService.IsMechanic(this.User.Id))
            {
                cars = this.carshopDbContext.Cars
                    .Where(c => c.Issues.Any(i => !i.IsFixed))
                      .Select(c => new AllCarsViewModel
                      {
                          Id = c.Id,
                          Model = c.Model,
                          Year = c.Year,
                          PlateNumber = c.PlateNumber,
                          Image = c.PictureUrl,
                          FixedIssues = c.Issues.Where(i => i.IsFixed).Count(),
                          RemainingIssues = c.Issues.Where(i => !i.IsFixed).Count(),
                      })
                    .ToList();
            }
            else
            {
                 cars = this.carshopDbContext.Cars
              .Where(u => u.Owner.Id == this.User.Id)
              .Select(c => new AllCarsViewModel
              {
                  Id = c.Id,
                  Model = c.Model,
                  Year = c.Year,
                  PlateNumber = c.PlateNumber,
                  Image = c.PictureUrl,
                  FixedIssues = c.Issues.Where(i => i.IsFixed).Count(),
                  RemainingIssues = c.Issues.Where(i => !i.IsFixed).Count(),
              })
              .ToList();
            }

            return View(cars);

        }
    }
}
