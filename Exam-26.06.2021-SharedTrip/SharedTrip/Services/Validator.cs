namespace SharedTrip.Services
{
    using SharedTrip.Models.Trips;
    using SharedTrip.ViewModels.Users;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using static Data.DataConstants;
 
    public class Validator : IValidator
    {
        public ICollection<string> ValidateTrip(AddTripViewModel model)
        {
            var errors = new List<string>();

            if (model.StartPoint==null)
            {
                errors.Add("StartPoint can't be null value");
            }

            if (model.EndPoint == null)
            {
                errors.Add("EndPoint can't be null value");
            }

            if (model.DepartureTime == null)
            {
                errors.Add("DepartureTime can't be null value");
            }

            if (model.Description == null)
            {
                errors.Add("Description can't be null value");
            }

            if (model.Seats < SeatsMinValue || model.Seats > SeatsMaxValue)
            {
                errors.Add($"Username '{model.Seats}' is not valid. It must be between {SeatsMinValue} and {SeatsMaxValue} characters long.");
            }

            if (model.Description.Length > DescriptionMaxLength)
            {
                errors.Add($"Description is not valid. It must be maximum {DescriptionMaxLength} symbols long.");
            }
            return errors;
        }

        public ICollection<string> ValidateUser(RegisterUserViewModel user)
        {
            var errors = new List<string>();

            if (user.Username == null || user.Username.Length < UsernameMinLength || user.Username.Length > UsernameMaxLength)
            {
                errors.Add($"Username '{user.Username}' is not valid. It must be between {UsernameMinLength} and {UsernameMaxLength} characters long.");
            }

            if (user.Email == null || !Regex.IsMatch(user.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email '{user.Email}' is not a valid e-mail address.");
            }

            if (user.Password == null || user.Password.Length < PasswordMinLength || user.Password.Length > PasswordMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {PasswordMinLength} and {PasswordMaxLength} characters long.");
            }

            if (user.Password != null && user.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (user.Password != user.ConfirmPassword)
            {
                errors.Add("Password and its confirmation are different.");
            }
            return errors;
        }
    }
}
