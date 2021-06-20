using CarShop.ViewModels.Cars;
using CarShop.ViewModels.Issues;
using CarShop.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarShop.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateAddCars(AddCarViewModel model)
        {
           var errors= new List<string>();
            var currentYear = int.Parse(DateTime.UtcNow.Year.ToString());

            if (model.Model.Length < 5 || model.Model.Length > 20)
            {
                errors.Add("Model must be between 5 and 20 symbols");
            }

            if (model.Year<1950 || model.Year>currentYear)
            {
                errors.Add("The written year is not valid.");
            }

            if (!Regex.IsMatch(model.PlateNumber, @"^[A-Z]{1}[\d]{4}[A-Z]{2}$"))
            {
                errors.Add("The written Plate Number is not valid.");
            }

            return errors;
        }

        public ICollection<string> ValidateAddIssue(AddIssueViewModel model)
        {
            var errors = new List<string>();

            if (model.Description.Length < 5)
            {
                errors.Add($"The issue description hat to contain minimus 5 symbols");
            }

            return errors;
        }

        public ICollection<string> ValidateUserRegistration(RegisterUserViewModel model)
        {
            var errors = new List<string>();
            if (model.Username.Length < 4 || model.Username.Length > 20)
            {
                errors.Add("Username must be between 4 and 20 symbols");
            }

            if (model.Password.Length < 5 || model.Password.Length > 20)
            {
                errors.Add("Password must be between 5 and 20 symbols");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add("Password and ConfirmPassword didn't match.");
            }

            if (model.UserType != "Mechanic" && model.UserType != "Client")
            {
                errors.Add("UserType has to be Mechanic or Client.");
            }
            return errors;
        }
       
        
    }
}
