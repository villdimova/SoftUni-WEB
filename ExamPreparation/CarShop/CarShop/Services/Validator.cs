namespace CarShop.Services
{
    using CarShop.ViewModels.Cars;
    using CarShop.ViewModels.Issues;
    using CarShop.ViewModels.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using static Data.DataConstants;


    public class Validator : IValidator
    {
        public ICollection<string> ValidateCar(AddCarViewModel model)
        {
            var errors = new List<string>();

            if (model.Model == null || model.Model.Length < CarModelMinLength || model.Model.Length > DefaultMaxLength)
            {
                errors.Add($"Model '{model.Model}' is not valid. It must be between {CarModelMinLength} and {DefaultMaxLength} characters long.");
            }

            if (model.Year < CarYearMinValue || model.Year > CarYearMaxValue)
            {
                errors.Add($"Year '{model.Year}' is not valid. It must be between {CarYearMinValue} and {CarYearMaxValue}.");
            }

            if (model.Image == null || !Uri.IsWellFormedUriString(model.Image, UriKind.Absolute))
            {
                errors.Add($"Image '{model.Image}' is not valid. It must be a valid URL.");
            }

            if (model.PlateNumber == null || !Regex.IsMatch(model.PlateNumber, CarPlateNumberRegularExpression))
            {
                errors.Add($"Plate number '{model.PlateNumber}' is not valid. It should be in 'XX0000XX' format.");
            }

            return errors;
        }

        public ICollection<string> ValidateIssue(AddIssueViewModel model)
        {
            var errors = new List<string>();

            if (model.CarId == null)
            {
                errors.Add($"Car ID cannot be empty.");
            }

            if (model.Description.Length < IssueMinDescription)
            {
                errors.Add($"Description '{model.Description}' is not valid. It must have more than {IssueMinDescription} characters.");
            }

            return errors;
        }

        public ICollection<string> ValidateUser(RegisterUserViewModel model)
        {
            var errors = new List<string>();

            if (model.Username == null || model.Username.Length < UserMinUsername || model.Username.Length > DefaultMaxLength)
            {
                errors.Add($"Username '{model.Username}' is not valid. It must be between {UserMinUsername} and {DefaultMaxLength} characters long.");
            }

            if (model.Email == null || !Regex.IsMatch(model.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email '{model.Email}' is not a valid e-mail address.");
            }

            if (model.Password == null || model.Password.Length < UserMinPassword || model.Password.Length > DefaultMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {UserMinPassword} and {DefaultMaxLength} characters long.");
            }

            if (model.Password != null && model.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add("Password and its confirmation are different.");
            }

            if (model.UserType != UserTypeClient && model.UserType != UserTypeMechanic)
            {
                errors.Add($"The user can be either a '{UserTypeClient}' or a '{UserTypeMechanic}'.");
            }

            return errors;
        }
       
    }
}
