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
        ICollection<string> ValidateUser(RegisterUserViewModel model);

        ICollection<string> ValidateCar(AddCarViewModel model);

        ICollection<string> ValidateIssue(AddIssueViewModel model);
    }
}
