using CarShop.ViewModels.Cars;
using CarShop.ViewModels.Issues;
using CarShop.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Services
{
   public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(RegisterUserViewModel model);

        ICollection<string> ValidateAddCars(AddCarViewModel model);

        ICollection<string> ValidateAddIssue(AddIssueViewModel model);
    }
}
