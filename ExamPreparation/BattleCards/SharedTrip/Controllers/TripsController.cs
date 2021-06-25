namespace SharedTrip.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SharedTrip.Data;
    using SharedTrip.Data.Models;
    using SharedTrip.Models.Trips;
    using SharedTrip.Services;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class TripsController : Controller
    {
        private readonly IValidator validator;
        private readonly SharedTripDbContext data;


        public TripsController(IValidator validator, SharedTripDbContext data)
        {
            this.validator = validator;
            this.data = data;

        }
        [Authorize]
        public HttpResponse All()
        {
            var trips = this.data.Trips.
                    Select(t => new AllTripsViewModel
                    {
                        Id=t.Id,
                        StartPoint=t.StartPoint,
                        EndPoint=t.EndPoint,
                        DepartureTime=t.DepartureTime.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                        Seats=t.Seats
                    }).ToList();

            return View(trips);
        }

        [Authorize]
        public HttpResponse Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddTripViewModel model)
        {
            var modelErrors = this.validator.ValidateTrip(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var trip = new Trip
            {
               StartPoint=model.StartPoint,
               EndPoint=model.EndPoint,
               DepartureTime= DateTime.ParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
               Description=model.Description,
               ImagePath=model.ImagePath,
               Seats=model.Seats,
            };

            this.data.Trips.Add(trip);
            this.data.SaveChanges();

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse Details(string tripId)
        {
            var trip = this.data.Trips
                        .Where(t => t.Id == tripId)
                        .Select(x => new DetailsViewModel
                        {
                            Id=x.Id,
                            ImagePath=x.ImagePath,
                            StartPoint=x.StartPoint,
                            EndPoint=x.EndPoint,
                            DepartureTime = x.DepartureTime.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                            Description =x.Description,
                            Seats=x.Seats.ToString(),
                        })
                        .FirstOrDefault();

            if (trip==null)
            {
                return BadRequest();
            }

            return View(trip);
        }

        public HttpResponse AddUserToTrip(string  tripId)
        {
            if (!AnyFreeSeats(tripId))
            {
                return Error("No free seats");
            }

            if (IsUserJoinedTheTrip(tripId))
            {
                return Redirect($"/Trips/Details?tripId={tripId}");
            }

            var userTrip = new UserTrip
            {
                UserId = this.User.Id,
                TripId = tripId,
            };
            var trip = this.data.Trips.FirstOrDefault(t => t.Id == tripId);
            trip.UserTrips.Add(userTrip);
            trip.Seats = trip.Seats - 1;
            this.data.SaveChanges();
            return Redirect("/Trips/All");
        }

        private bool IsUserJoinedTheTrip(string tripId)
        {
            var tripUsers = this.GetTripUsers(tripId);
            var usersId = tripUsers.Select(x => x.UserId).ToList();
            if (!usersId.Contains(this.User.Id))
            {
                return false;
            }
            return true;
        }

        private bool AnyFreeSeats(string tripId)
        {
            var tripSeats = this.data.Trips
                                    .Where(t => t.Id == tripId)
                                    .Select(x => x.Seats)
                                    .FirstOrDefault();

            var tripUsers = this.GetTripUsers(tripId);

            var buisySeats = tripUsers.Count();

            if (buisySeats+1>tripSeats)
            {
                return false;
            }

            return true;
        }

        private ICollection<UserTrip> GetTripUsers(string tripId)
        {
            var tripUsers = this.data.Trips
               .Where(x => x.Id == tripId)
               .Select(t => t.UserTrips)
               .FirstOrDefault();

            return tripUsers;
        }
    }
}


