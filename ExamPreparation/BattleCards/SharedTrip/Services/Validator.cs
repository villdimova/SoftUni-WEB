namespace SharedTrip.Services
{
    using SharedTrip.Models.Trips;
    using SharedTrip.ViewModels.Users;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using static Data.DataConstants;
 
    public class Validator : IValidator
    {
        public ICollection<string> ValidateTrip(AddTripViewModel model)
        {
            var errors = new List<string>();
            if (model.Seats < SeatsMinValue || model.Seats > SeatsMaxValue)
            {
                errors.Add($"Username '{model.Seats}' is not valid. It must be between {SeatsMinValue} and {SeatsMaxValue} characters long.");
            }

            if (model.Description.Length >DescriptionMaxLength)
            {
                errors.Add($"Description is not valid. It must be maximum {DescriptionMaxLength} symbols long.");
            }
            return errors;
        }

        public ICollection<string> ValidateUserRegistration(RegisterUserViewModel model)
        {
            var errors = new List<string>();
            if (model.Username.Length < UsernameMinLength || model.Username.Length > UsernameMaxLength)
            {
                errors.Add($"Username '{model.Username}' is not valid. It must be between {UsernameMinLength} and {UsernameMaxLength} characters long.");
            }

            if (!Regex.IsMatch(model.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email {model.Email} is not a valid e-mail address.");
            }

            if (model.Password.Length < PasswordMinLength || model.Password.Length > PasswordMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {PasswordMinLength} and {PasswordMaxLength} characters long.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Password and its confirmation are different.");
            }

            return errors;
        }

      
    }
}
