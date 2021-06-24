using CarShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Services
{
    public class UserService : IUserService
    {
        private readonly CarshopDbContext carshopDbContext;

        public UserService(CarshopDbContext carshopDbContext)
        {
            this.carshopDbContext = carshopDbContext;
        }

        public bool IsCarOwner(string carId, string userId)
        {
            var car = this.carshopDbContext.Cars
                            .FirstOrDefault(c => c.Id == carId);

            if (car.OwnerId!=userId)
            {
                return false;
            }

            return true;
        }

        public bool IsMechanic(string userId)
        {
            var currentUser = carshopDbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (!currentUser.IsMechanic)
            {
                return false;
            }

            return true;

        }
    }
}
