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
